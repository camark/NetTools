using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;

namespace NetTools.Util
{
    public class HardwareUtil
    {
        public string GethostName()
        {
            return Dns.GetHostName();
        }

        public static string GetCPUSerialNumber()
        {
            string empty = string.Empty;
            ManagementClass managementClass = new ManagementClass("Win32_Processor");
            ManagementObjectCollection instances = managementClass.GetInstances();
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
            {
                if (enumerator.MoveNext())
                    empty = enumerator.Current["ProcessorId"].ToString();
            }
            managementClass.Dispose();
            instances.Dispose();
            return empty;
        }

        public static string GetDiskSerialNumber()
        {
            ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementObjectSearcher()
            {
                Query = ((ObjectQuery)new SelectQuery("Win32_DiskDrive", "", new string[2]
                {
                "PNPDeviceID",
                "Signature"
                }))
            }.Get().GetEnumerator();
            enumerator.MoveNext();
            return enumerator.Current.Properties["signature"].Value.ToString().Trim();
        }

        public static string GetMoAddress()
        {
            string str = " ";
            using (ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                foreach (ManagementObject instance in managementClass.GetInstances())
                {
                    if ((bool)instance["IPEnabled"])
                        str = instance["MacAddress"].ToString();
                    instance.Dispose();
                }
            }
            return str.ToString();
        }
    }
}
