using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VisualFrameworkLibrary
{
    public static class XMLExtended
    {
        public static object XMLRead(string way, Type type)
        {
            FileStream fs = new FileStream(way, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            XmlSerializer xmlSerializer = new XmlSerializer(type);
            object obj = xmlSerializer.Deserialize(sr);
            sr.Close();
            fs.Close();
            return obj;
        }

        public static void XMLRead(string way, Type type, object obj)
        {
            FileStream fs = new FileStream(way, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            XmlSerializer xmlSerializer = new XmlSerializer(type);
            xmlSerializer.Serialize(sw, obj);
            sw.Close();
            fs.Close();
        }
    }
}
