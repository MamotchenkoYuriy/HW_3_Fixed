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
    public class InteractiveConsole
    {
        private const string FILESERIALISE = "./SerializeFile.xml";
        ArrayList<LibraryItem> list = new ArrayList<LibraryItem>();

        private void Menu(string path)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(path);
                Console.WriteLine("Выберите действие: ");
                Console.WriteLine("1 - Работа с книгами");
                Console.WriteLine("2 - Работа с журналами");
                Console.WriteLine("3 - Выход");
                int consoleCommand = ReadIntValueFromConsole(1, 3);
                if (consoleCommand == 1)
                {
                    //BookMenu();
                    EntityMenu<Book>(path + "->Книги" );
                }
                else if (consoleCommand == 2)
                {
                    //JournalMenu();
                    EntityMenu<Journal>(path + "->Журналы");
                }
                else if (consoleCommand == 3)
                {
                    OutSerialiseMenu();
                    return;
                }
            }
        }

        public void Start()
        {
            Console.WriteLine("Восстановить все??? ");
            Console.WriteLine("1 - Да");
            Console.WriteLine("2 - Нет");
            int number = ReadIntValueFromConsole(1, 2);
            if (number == 1)
            {
                list = Serializer<ArrayList<LibraryItem>>.DeSerialize(FILESERIALISE);
            }
            Menu("Меню");
        }
        private void OutSerialiseMenu()
        {
            Console.WriteLine("Сохранить все??? ");
            Console.WriteLine("1 - Да");
            Console.WriteLine("2 - Нет");
            int number = ReadIntValueFromConsole(1, 2);
            if (number == 1)
            {
                Serializer<ArrayList<LibraryItem>>.Serialize(list, FILESERIALISE);
            }
        }

        public void EntityMenu<T>(string path) where T: LibraryItem
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(path);
                Console.WriteLine("Выберите действие: ");
                Console.WriteLine("1 - Добавить запись");
                Console.WriteLine("2 - Просмотреть все записи");
                Console.WriteLine("3 - Удалить запись");
                Console.WriteLine("4 - Редактировать запись");
                Console.WriteLine("5 - Выход");
                int consoleCommand = ReadIntValueFromConsole(1, 4);
                if (consoleCommand == 1)
                {
                    AddEntityMenu<T>(path + "->Добавление");
                }
                else if (consoleCommand == 2)
                {
                    ShowAll<T>(path + "->Все записи");
                }
                else if (consoleCommand == 3)
                {
                    DeleteEntityMenu<T>(path + "->Удаление");
                }
                else if (consoleCommand == 4)
                {
                    EditEntityMenu<T>(path + "->Редактирование");
                }
                else if (consoleCommand == 5) break;
            }
        }

        private void EditEntityMenu<T>(string path) where T : LibraryItem
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(path);
                T entity = null;
                Console.WriteLine("1 - Редактировать по ID -->");
                Console.WriteLine("2 - Редактировать № -->");
                Console.WriteLine("3 - Выход -->");
                int consoleCommand = ReadIntValueFromConsole();
                if (consoleCommand == 1)
                {
                    Console.WriteLine("Введите Id --> ");
                    int id = ReadIntValueFromConsole();
                    entity = (T)list.GetOnly<T>().Where(m => m.Id == id).FirstOrDefault();
                    if (entity == null) { Console.Write("Сущьность с Id = " + id + " не найденна!!!"); break; }
                }
                else if (consoleCommand == 2)
                {
                    Console.WriteLine("Введите номер записи --> ");
                    int pos = ReadIntValueFromConsole();
                    if (pos >= list.GetOnly<T>().Count) { Console.WriteLine("Вы ввели не корректный номер!!!"); break; }
                    entity = (T)list.GetOnly<T>()[pos];
                }
                else if (consoleCommand == 3) { break; }

                int position = list.IndexOf(entity);
                var type = typeof(T);
                var constractor = type.GetConstructors().Where(m => m.GetParameters().Length > 0).FirstOrDefault();
                var constractorAttributes = constractor.GetParameters().Where(m => m.Name != "id");
                List<object> listParameters = new List<object>();
                listParameters.Add(entity.Id);
                //var mmm = typeof(T).GetType().GetMethod("").Invoke(typeof(T), new object[2]);
                foreach (var attribut in constractorAttributes)
                {
                    if (attribut.ParameterType == typeof(string))
                    {
                        Console.WriteLine("Введите значение атрибута " + attribut.Name.ToUpper() + " -->");
                        listParameters.Add(Console.ReadLine());
                    }
                    else if (attribut.ParameterType == typeof(Int32))
                    {
                        Console.WriteLine("Введите значение атрибута " + attribut.Name.ToUpper() + "-->");
                        listParameters.Add(ReadIntValueFromConsole());
                    }
                }

                Assembly assembly = Assembly.GetAssembly(typeof(T));
                Object o = assembly.CreateInstance(typeof(T).FullName, false,
                    BindingFlags.ExactBinding,
                    null, listParameters.ToArray(), null, null);
                var addItem = (T)o;
                list.Edit(position, addItem);
                Console.WriteLine("Изменения внесены!!!");
                Console.ReadKey();
            }
        }

        private void DeleteEntityMenu<T>(string path)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(path);
                Console.WriteLine("Выбирите дейтвие: ");
                Console.WriteLine("1 - Удалить по ID: ");
                Console.WriteLine("2 - Удалить по номеру: ");
                Console.WriteLine("3 - Выход");
                int commandNumber = ReadIntValueFromConsole(1, 3);
                if (commandNumber == 1)
                {
                    Console.WriteLine("Введите ID книги: ");
                    int Id = ReadIntValueFromConsole();
                    var item = list.GetOnly<T>().Where(m => m.Id == Id).FirstOrDefault();
                    if (item != null)
                    {
                        list.Remove(item);
                        Console.WriteLine("Запись удаленна");
                    }
                    Console.WriteLine("Запись с ID = " + Id + "не найденна");
                }
                else if (commandNumber == 2)
                {
                    Console.WriteLine("Введите номер записи в списке");
                    int number = ReadIntValueFromConsole();
                    var itemsOfType = list.GetOnly<T>();
                    if (itemsOfType.Count <= number)
                    {
                        Console.WriteLine("Нету записи под номером " + number + "!!!");
                    }
                    else
                    {
                        list.Remove(itemsOfType[number]);
                        Console.WriteLine("Запись удалена");
                    }
                }
                else if (commandNumber == 3)
                {
                    break;
                }
            }
        }

        private void AddEntityMenu<T>(string path) where T:LibraryItem
        {
            Console.Clear();
            Console.WriteLine(path);
            try
            {
                var constractor = typeof(T).GetConstructors().Where(m => m.GetParameters().Length > 0).FirstOrDefault();
                var constractorAttributes = constractor.GetParameters().Where(m => m.Name != "id").ToList();
                List<object> listParameters = new List<object>();
                listParameters.Add(Generator.Generate(list));
                foreach (var attribut in constractorAttributes)
                {
                    if (attribut.ParameterType == typeof(string))
                    {
                        Console.WriteLine("Введите значение атрибута " + attribut.Name.ToUpper() + " -->");
                        listParameters.Add(Console.ReadLine());
                    }
                    else if (attribut.ParameterType == typeof(Int32))
                    {
                        Console.WriteLine("Введите значение атрибута " + attribut.Name.ToUpper() + "-->");
                        listParameters.Add(ReadIntValueFromConsole());
                    }
                }

                Assembly assembly = Assembly.GetAssembly(typeof(T));
                Object o = assembly.CreateInstance(typeof(T).FullName, false,
                    BindingFlags.ExactBinding,
                    null, listParameters.ToArray(), null, null);
                var addItem = (T)o;
                list.Add(addItem);
                Console.WriteLine("Изменения внесены!!!");
                Console.ReadKey();
            }
            catch (Exception err) { Console.WriteLine("Возникла ошибка при добавлении записи "); }
        } 

        private void ShowAll<T>(string messsage)
        {
            Console.Clear();
            Console.WriteLine(messsage);
            foreach (var item in list.GetOnly<T>())
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }

        #region Получение int значение со строки

        private int ReadIntValueFromConsole()
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

        private int ReadIntValueFromConsole(int min, int max)
        {
            while (true)
            {
                int value = ReadIntValueFromConsole();
                if (Enumerable.Range(1, 10).Contains(value)) { return value; }
            }
        }


        #region То что точно не буду использовать пока то, но пускай на всякий случай остается
        //private void AddBookMenu()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Введите следующие данные: ");
        //    Console.WriteLine("Название книги: ");
        //    string name = Console.ReadLine();
        //    Console.WriteLine("Автор книги: ");
        //    string author = Console.ReadLine();
        //    Console.WriteLine("Год издания: ");
        //    int year = ReadIntValueFromConsole();
        //    list.Add(new Book() { Name = name, Author = author, Year = year, Id = Generator.Generate(list) });
        //}

        //private void JournalMenu()
        //{
        //    while (true)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Выберите действие: ");
        //        Console.WriteLine("1 - Добавить журнал");
        //        Console.WriteLine("2 - Просмотреть все");
        //        Console.WriteLine("3 - Удалить журнал");
        //        Console.WriteLine("4 - Редактировать книгу");
        //        Console.WriteLine("5 - Выход");
        //        int consoleCommand = ReadIntValueFromConsole(1, 4);
        //        if (consoleCommand == 1)
        //        {
        //            //AddJornalMenu();
        //            //AddEntityMenu<Journal>(path + "->AddEntity");
        //        }
        //        else if (consoleCommand == 2)
        //        {
        //            ShowAll<Journal>("Все журналы -->");
        //        }
        //        else if (consoleCommand == 3)
        //        {
        //            DeleteEntityMenu<Book>();
        //        }
        //        else if (consoleCommand == 4)
        //        {
        //            EditEntityMenu<Book>();
        //        }
        //        else if (consoleCommand == 5) break;
        //    }
        //}

        //private void AddJornalMenu()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Введите следующие данные: ");
        //    Console.WriteLine("Название журнала: ");
        //    string name = Console.ReadLine();
        //    Console.WriteLine("Номер выпуска: ");
        //    int number = ReadIntValueFromConsole();
        //    Console.WriteLine("Год издания: ");
        //    int year = ReadIntValueFromConsole();
        //    list.Add(new Journal() { Name = name, Number = number, Year = year, Id = Generator.Generate(list) });
        //}

        //private void BookMenu()
        //{
        //    while (true)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Выберите действие: ");
        //        Console.WriteLine("1 - Добавить книку");
        //        Console.WriteLine("2 - Просмотреть все");
        //        Console.WriteLine("3 - Удалить книгу");
        //        Console.WriteLine("4 - Редактировать книгу");
        //        Console.WriteLine("5 - Выход");
        //        int consoleCommand = ReadIntValueFromConsole(1, 4);
        //        if (consoleCommand == 1)
        //        {
        //            //AddBookMenu();
        //            //AddEntityMenu<Book>();
        //        }
        //        else if (consoleCommand == 2)
        //        {
        //            ShowAll<Book>("Все книги -->");
        //        }
        //        else if (consoleCommand == 3)
        //        {
        //            DeleteEntityMenu<Book>();
        //        }
        //        else if (consoleCommand == 4)
        //        {
        //            EditEntityMenu<Book>();
        //        }
        //        else if (consoleCommand == 5) break;
        //    }
        //}


        #endregion 


        #endregion



    }
}
