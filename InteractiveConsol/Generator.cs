using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArrayList;
using LibraryItems;

namespace InteractiveConsol
{
    public class Generator
    {
        public static int Generate(ArrayList<LibraryItem> arr)
        {
            Random rnd = new Random();
            while (true)
            {
                int rndValue = rnd.Next(10000, 99999);
                if (arr.ToList() == null) { return rndValue; }
                else if (arr.ToList().Where(m => m.Id == rndValue).FirstOrDefault() == null)
                {
                    return rndValue;
                }
            }
        }
    }
}
