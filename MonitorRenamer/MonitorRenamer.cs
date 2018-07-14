using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Management.Instrumentation;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using Microsoft.Win32;

namespace MonitorRenamer
{
    public partial class MonitorRenamer : Form
    {
        public MonitorRenamer() {
            InitializeComponent();
        }


        private void MonitorRenamer_Load(object sender, EventArgs e)
        {
            // check if started as admin
            bool isStartedAsAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            if (!isStartedAsAdmin)
            {
                MessageBox.Show("You have to start this tool as administrator to be able to change display names!",
                    "Warning");
                this.Text += " (NOT ADMIN)";
            }


            // enumerate all present displays
            // win32 api call to get driver IDs we need to match displays from CurrentControlSet\Enum\DISPLAY
            var device = new DISPLAY_DEVICE();
            device.cb = Marshal.SizeOf(device);
            try {
                monitorComboBox.ValueMember = "DeviceID";
                monitorComboBox.DisplayMember = "DeviceName";

                for (uint id = 0; EnumDisplayDevices(null, id, ref device, 0); id++) {
                    device.cb = Marshal.SizeOf(device);
                    EnumDisplayDevices(device.DeviceName, 0, ref device, 0);
                    device.cb = Marshal.SizeOf(device);

                    if (!String.IsNullOrEmpty(device.DeviceName))
                    {
                        monitorComboBox.Items.Add(new MonitorItem(device.DeviceName, device.DeviceID));
                    }
                }

                monitorComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }


        private void renameButton_Click(object sender, EventArgs e)
        {
            // find registry key for selected display and set device description and friendly name
            string displayName = displayNameTextBox.Text;
            if (displayName.Length != 0)
            {
                try
                {
                    var monitorItem = (MonitorItem) monitorComboBox.SelectedItem;
                    RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(getRegistryKeyToMonitor(monitorItem.DeviceID), RegistryKeyPermissionCheck.ReadWriteSubTree);
                    registryKey.SetValue("DeviceDesc", displayName, RegistryValueKind.String);
                    registryKey.SetValue("FriendlyName", displayName, RegistryValueKind.String);

                    MessageBox.Show(monitorItem.DeviceName + " renamed to " + displayName + ".", "Success");
                }
                catch (SecurityException)
                {
                    MessageBox.Show("Access denied! Start this tool as administrator.", "Error");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Writing to registry failed! " + ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Enter a display name.", "Error");
            }
        }

        private void aboutLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("biosmanager\ngithub@biosmanager.com", "About");
        }

        private void monitorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // show display name of currently selected monitor
            var monitorItem = (MonitorItem) monitorComboBox.SelectedItem;
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(getRegistryKeyToMonitor(monitorItem.DeviceID), RegistryKeyPermissionCheck.ReadSubTree);
            string desc = (string)registryKey.GetValue("DeviceDesc");

            if (!String.IsNullOrEmpty(desc)) {
                if (desc == "@monitor.inf,%pnpmonitor.devicedesc%;Generic PnP Monitor") {
                    displayNameTextBox.Text = "Generic PnP Monitor";
                } else {
                    displayNameTextBox.Text = desc;
                }
            }
        }

        private string getRegistryKeyToMonitor(string deviceId)
        {
            // find registry key for disply by its device id
            if (deviceId.Length != 0)
            {
                string[] splitted = deviceId.Split('\\');
                string hardwareId = splitted[1];
                string driver = splitted[2] + '\\' + splitted[3];

                string hardwareIdRegKey = "SYSTEM\\CurrentControlSet\\Enum\\DISPLAY\\" + hardwareId;
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(hardwareIdRegKey, RegistryKeyPermissionCheck.ReadSubTree);
                var subKeyNames = registryKey.GetSubKeyNames();

                string foundSubKeyName = "";
                foreach (var subKeyName in subKeyNames)
                {
                    var subKey = registryKey.OpenSubKey(subKeyName, RegistryKeyPermissionCheck.ReadSubTree);
                    if ((string) subKey.GetValue("Driver") == driver)
                    {
                        foundSubKeyName = subKeyName;
                    }
                }

                return hardwareIdRegKey + '\\' + foundSubKeyName;
            }

            return null;
        }

        private void MonitorRenamer_Move(object sender, EventArgs e)
        {
            // find display the window is currently showed on
            Screen currentScreen = Screen.FromControl(this);

            foreach (MonitorItem monitor in monitorComboBox.Items)
            {
                if (monitor.DeviceName.Contains(currentScreen.DeviceName))
                {
                    monitorComboBox.SelectedItem = monitor;
                }
            }
        }

        [DllImport("user32.dll")]
        public static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

        [Flags()]
        public enum DisplayDeviceStateFlags : int
        {
            /// <summary>The device is part of the desktop.</summary>
            AttachedToDesktop = 0x1,
            MultiDriver = 0x2,
            /// <summary>The device is part of the desktop.</summary>
            PrimaryDevice = 0x4,
            /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
            MirroringDriver = 0x8,
            /// <summary>The device is VGA compatible.</summary>
            VGACompatible = 0x16,
            /// <summary>The device is removable; it cannot be the primary display.</summary>
            Removable = 0x20,
            /// <summary>The device has more display modes than its output devices support.</summary>
            ModesPruned = 0x8000000,
            Remote = 0x4000000,
            Disconnect = 0x2000000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DISPLAY_DEVICE
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;
            [MarshalAs(UnmanagedType.U4)]
            public DisplayDeviceStateFlags StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }

        private class MonitorItem
        {
            public MonitorItem(string deviceName, string deviceID) {
                DeviceName = deviceName;
                DeviceID = deviceID;
            }

            public string DeviceName { get; set; }
            public string DeviceID { get; set; }
        }
    }
}
