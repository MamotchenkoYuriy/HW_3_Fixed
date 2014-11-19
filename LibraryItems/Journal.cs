using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibraryItems
{
    [Serializable]
    public class Journal : LibraryItem
    {
        [XmlAttribute]
        public int Number { get; set; }

        public Journal() : base() { }

        public Journal(int id, string name, int year, int number)
            : base(id, name, year)
        {
            Number = number;
        }


        public override string ToString()
        {
            return String.Format("Id --> {1}{0}Name --> {2}{0}Number --> {3}{0}Year -->{4}{0}",
                Environment.NewLine, Id, Name, Number, Year);
        }
    }
}
