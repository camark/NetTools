using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace NetTools.Util
{
    public class RegistryUtil
    {
        string s_regKey = String.Empty;

        public RegistryUtil(string rootKey = @"pleasure", bool bEmz = true)
        {
            string defaultRoot = @"software\emz\";

            string TargetKey = rootKey;
            if (bEmz && !rootKey.StartsWith(defaultRoot))
            {
                TargetKey = defaultRoot + rootKey;
            }
            s_regKey = TargetKey;
        }

        public string ReadCfg(string keycode)
        {
            using(RegistryKey regKey = Registry.CurrentUser.CreateSubKey(s_regKey))
            {

                if (regKey.GetValueNames().Contains(keycode))
                {
                    return regKey.GetValue(keycode).ToString();
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public void WriteCfg(string key,string value)
        {
            using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey(s_regKey))
            {
                regKey.SetValue(key, value);
            }
        }

        public void DelCfg(string value)
        {
            using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey(s_regKey))
            {

                if (regKey.GetValueNames().Contains(value))
                {
                    regKey.DeleteValue(value);
                }
            }
        }
    
    }
}
