using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SHDocVw;
using MSHTML;
using Microsoft.Win32;
using System.Windows.Forms;

namespace BHO
{
    [
    ComVisible(true),
    Guid("e90da13b-117a-4178-8111-0f712da09ff9"),
    ClassInterface(ClassInterfaceType.None)
    ]
    public class BHOMain : IObjectWithSite
    {
        SHDocVw.WebBrowser webBrowser = null;
        
        private void OnDocumentComplete(object pDisp, ref object Url)
        {
            IHTMLDocument2  document = (IHTMLDocument2)webBrowser.Document;
            IHTMLElement ele = document.createElement("button");
            ele.innerText = "Show Dialog";
            (document.body as IHTMLDOMNode).appendChild(ele as IHTMLDOMNode);
            ((HTMLButtonElementEvents2_Event)ele).onclick
               += new HTMLButtonElementEvents2_onclickEventHandler(Button_onclick);
        }
       
        private bool Button_onclick(IHTMLEventObj e)
        {
            Form1 login = new Form1();
            //HTMLDocument document = (HTMLDocument)webBrowser.Document;
            //foreach (IHTMLInputElement element in document.getElementsByTagName("INPUT"))
            //{
            //    if (element.name.Equals("csrf"))
            //    {
            //        login.csrf = element.value;
            //    }
            //    else if (element.name.Equals("jds"))
            //    {
            //        login.jds = element.value;
            //    }
            //    else if (element.name.Equals("jns"))
            //    {
            //        login.jns = element.value;
            //    }
            //    else if (element.name.Equals("jsd"))
            //    {
            //        login.jsd = element.value;
            //    }
            //    else if (element.name.Equals("jdd"))
            //    {
            //        login.jdd = element.value;
            //    }
            //    else if (element.name.Equals("cms"))
            //    {
            //        login.cms = element.value;
            //    }
            //}           
            login.ShowDialog();
            return false;
        }

        public int GetSite(ref Guid guid, out IntPtr ppvSite)
        {
            IntPtr punk = Marshal.GetIUnknownForObject(webBrowser);
            int hr = Marshal.QueryInterface(punk, ref guid, out ppvSite);
            Marshal.Release(punk);
            return hr;
        }

        public int SetSite([MarshalAs(UnmanagedType.IUnknown)] object site)
        {
            if (site != null)
            {
                webBrowser = (SHDocVw.WebBrowser)site;
                webBrowser.DocumentComplete+=new DWebBrowserEvents2_DocumentCompleteEventHandler(OnDocumentComplete);
             }
            else
            {              
                webBrowser.DocumentComplete -= new DWebBrowserEvents2_DocumentCompleteEventHandler(OnDocumentComplete);
                webBrowser = null;              
            }
            return 0;
        }

        public static string BHOKEYNAME = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Browser Helper Objects";
        [ComRegisterFunction]
        public static void RegisterBHO(Type type)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(BHOKEYNAME, true);
            if (registryKey == null)
            {
                registryKey = Registry.LocalMachine.CreateSubKey(BHOKEYNAME);
            }
            string guid = type.GUID.ToString("B");
            RegistryKey bhoKey = registryKey.OpenSubKey(guid, true);
            if (bhoKey == null)
            {
                bhoKey = registryKey.CreateSubKey(guid);
            }
            // NoExplorer: dword = 1 prevents the BHO to be loaded by Explorer.exe
            bhoKey.SetValue("NoExplorer", 1);
            bhoKey.Close();
            registryKey.Close();
        }
        [ComUnregisterFunction]
        public static void UnregisterBHO(Type type)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(BHOKEYNAME, true);
            string guid = type.GUID.ToString("B");
            if (registryKey != null)
                registryKey.DeleteSubKey(guid, false);
        }
    }
}
