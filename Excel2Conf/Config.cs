using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Excel2Conf
{
    public class Config
    {
        public static string DesignDir = "";
        public static string ServerDir = "";
        public static string ClientDir = "";

        public static void ReadConfig()
        {
            string fileName = "setting.xml";
            if (!File.Exists(fileName))
            {
                fileName = "setting_template.xml";
                if (!File.Exists(fileName))
                {
                    fileName = "";
                }
            }

            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }
            XmlReaderSettings settings = new XmlReaderSettings();
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            XmlDataDocument xmlDoc = new XmlDataDocument();
            xmlDoc.Load(fileStream);

            XmlNodeList designNode = xmlDoc.GetElementsByTagName("design");
            XmlNodeList serverNode = xmlDoc.GetElementsByTagName("server");
            XmlNodeList clientNode = xmlDoc.GetElementsByTagName("client");

            foreach(XmlNode node in designNode)
            {
                DesignDir = node.InnerText;
                break;
            }
            foreach (XmlNode node in serverNode)
            {
                ServerDir = node.InnerText;
                break;
            }
            foreach (XmlNode node in clientNode)
            {
                ClientDir = node.InnerText;
                break;
            }
        }

        public static void WriteConfig()
        {
            // save path
        }
    }
}
