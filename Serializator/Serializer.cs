using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Serializator
{
    public class Serializer<T>
    {
        public static void staticSerialize() { }
        public static void Serialize(T collection, string filePath)
        {
            using (TextWriter writer = new StreamWriter(filePath))
            {
                XmlSerializer x = new XmlSerializer(typeof(T));
                x.Serialize(writer, collection);
            }
        }

        public static T DeSerialize(string filePath)
        {
            T myObject;
            if (!File.Exists(filePath)) { throw new FileNotFoundException(); }
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (FileStream myFileStream = new FileStream(filePath, FileMode.Open))
            {
                myObject = (T)mySerializer.Deserialize(myFileStream);
                return myObject;
            }
        }
    }
}
