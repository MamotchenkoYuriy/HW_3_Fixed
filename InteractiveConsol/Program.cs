using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArrayList;
using LibraryItems;
using Serializator;
using System.Reflection;



namespace InteractiveConsol
{
    class Program
    {
        static void Main(string[] args)
        {
            //IArrayList<LibraryItem> list = new ArrayList<LibraryItem>();

            //for (int i = 0; i < 10; i++)
            //{
            //    list.Add(new Book() { Author = "Test", Name = "Test", Id = i, Year = 2000 + i });
            //}
            //for (int i = 0; i < 10; i++)
            //{
            //    list.Add(new Journal() { Number = i, Name = "Test", Id = i, Year = 2000 + i });
            //}

            //foreach (var item in list)
            //{
            //    Console.WriteLine(item.ToString()); 
            //}
            //Console.WriteLine("Сериализуем все --->");
            //Serializer<IArrayList<LibraryItem>>.Serialize(list, "./SerialiseList.xml");
            //var deserializeList = Serializer<IArrayList<LibraryItem>>.DeSerialize("./SerialiseList.xml");
            //foreach (var item in deserializeList)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //Console.ReadKey();
            new InteractiveConsol.InteractiveConsole().Start();
            TestMethos<Book>();
        }
        public static void TestMethos<T>() where T : LibraryItem
        {
            string FILESERIALISE = "./SerializeFile.xml";
            ArrayList<LibraryItem> list = Serializer<ArrayList<LibraryItem>>.DeSerialize(FILESERIALISE);

            //int Id = 25094;
            //var item = list.GetOnly<T>().Where(m => m.Id == Id).FirstOrDefault();
            //int position = list.IndexOf(item);
            //if (item == null)
            //{
            //    Console.WriteLine("Запись с ID = " + Id + " не найденна!!!");
            //}
            //else
            //{
            //    var type = typeof(T);
            //    var constractor = type.GetConstructors().Where(m => m.GetParameters().Length > 0).FirstOrDefault();
            //    var constractorAttributes = constractor.GetParameters().Where(m => m.Name != "id");
            //    List<object> listParameters = new List<object>();
            //    listParameters.Add(Id);
            //    //var mmm = typeof(T).GetType().GetMethod("").Invoke(typeof(T), new object[2]);
            //    foreach (var attribut in constractorAttributes)
            //    {
            //        if (attribut.ParameterType == typeof(string))
            //        {
            //            Console.WriteLine("Введите значение атрибута " + attribut.Name.ToUpper() + " -->");
            //            listParameters.Add(Console.ReadLine());
            //        }
            //        else if (attribut.ParameterType == typeof(Int32))
            //        {
            //            Console.WriteLine("Введите значение атрибута " + attribut.Name.ToUpper() + "-->");
            //            listParameters.Add(ReadIntValueFromConsole());
            //        }
            //    }

            //    Assembly assembly = Assembly.GetAssembly(typeof(T));
            //    Object o = assembly.CreateInstance(typeof(T).FullName, false,
            //        BindingFlags.ExactBinding,
            //        null, listParameters.ToArray(), null, null);
            //    var addItem = (T)o;
            //    list.Edit(position, addItem);
            //    Console.ReadKey();

                //var attr = typeof(T).GetProperties().ToArray();
                //Assembly asembly = Assembly.GetAssembly(typeof(T));
                //var ent = asembly.CreateInstance(typeof(T).FullName);
                ////создали массив для 
                //object[] listObjectToCreate = new object[attr.Length];



                //Object o = asembly.CreateInstance(typeof(T).FullName, false,
                //    BindingFlags.ExactBinding,
                //    null, new object[] {10, "10", 10, "10"}, null, null);
                //var useActivator = Activator.CreateInstance(typeof(T));
                //foreach (var item1 in attr)
                //{
                //    Console.WriteLine(item1.Name);
                //}
                //int indexOfEntity = list.IndexOf(item);
                //Console.WriteLine("");
            //}
        }
        public static int ReadIntValueFromConsole()
        {
            string errMessage = string.Empty;
            while (true)
            {
                Console.WriteLine(errMessage);
                string strValue = Console.ReadLine();
                try
                {
                    return Convert.ToInt32(strValue);
                }
                catch (Exception err) { errMessage = "Вы ввели не корректное значение!!!"; }
            }
        }
    }
}
