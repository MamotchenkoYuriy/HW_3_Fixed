using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibraryItems
{
    [Serializable] 
    public class Book: LibraryItem
    {
        [XmlAttribute]
        public string Author { get; set; }

        public Book() : base() { }

        public Book(int id, string name, int year, string author):base(id, name, year)
        {
            Author = author;
        }

        public override string ToString()
        {
            return String.Format("Id --> {1}{0}Name --> {2}{0}Author --> {3}{0}Year -->{4}{0}",
                Environment.NewLine, Id, Name, Author, Year);
        }
    }
}
