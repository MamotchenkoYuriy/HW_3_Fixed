using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibraryItems
{
    [Serializable]
    [XmlInclude(typeof(Book)), XmlInclude(typeof(Journal))]
    public class LibraryItem
    {
        [XmlAttribute]
        public int Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int Year { get; set; }

        public LibraryItem() { }

        public LibraryItem(int Id, string Name, int Year)
        {
            this.Id = Id;
            this.Name = Name;
            this.Year = Year;
        }

        public override string ToString()
        {
            return String.Format("Id --> {1}{0}Name --> {2}{0}Year -->{3}{0}",
                Environment.NewLine, Id, Name, Year);
        }
    }
}
