using System.ComponentModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace HomeWork11
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            List<Task> tasks = new List<Task>()
            {
                new Task()
                {
                    Id = 1,
                    Title = "Введение в платформу Microsoft.NET.",
                    Description = "Решение четырёх задач.",
                    DueDate = new DateTime(2024, 5, 1),
                    Priority = "3. низкий"
                },
                new Task()
                {
                    Id= 2,
                    Title = "Массивы и строки.",
                    Description = "Решение семи заданий.",
                    DueDate = new DateTime(2024, 5, 9),
                    Priority = "2. средний"
                },
                new Task()
                {
                    Id= 3,
                    Title = "Классы, структуры, свойства. Обработка исключений.",
                    Description = "Решения шести заданий.",
                    DueDate = new DateTime(2024, 5, 27),
                    Priority = "1. высокий"
                },
                new Task()
                {
                    Id= 4,
                    Title = "Наследование и полиморфизм. Пространство имён.",
                    Description = "Разработка игры \'Крестики-нолики\'. Азбука Морзе. Решение ещё четырёх задач.",
                    DueDate = new DateTime(2024, 5, 27),
                    Priority = "3. низкий"
                },
                new Task()
                {
                    Id= 5,
                    Title = "Перегрузка операторов.",
                    Description = "Приложение \'Список книг для прочтения\'. Операции с матрицами.",
                    DueDate = new DateTime(2024, 5, 22),
                    Priority = "2. средний"
                },
                new Task()
                {
                    Id= 6,
                    Title = "Интерфейсы",
                    Description = "Программа поэтапного строительства дома.",
                    DueDate = new DateTime(2024, 5, 23),
                    Priority = "1. высокий"
                },
                new Task()
                {
                    Id=7,
                    Title = "Generics.",
                    Description = "Приложение \'Покупка недвижимости\'.",
                    DueDate = new DateTime(2024, 6, 3),
                    Priority = "3. низкий"
                },
                new Task()
                {
                    Id= 8,
                    Title = "Делегаты, события и LINQ.",
                    Description = "Решение трёх задач.",
                    DueDate = new DateTime(2024, 6, 5),
                    Priority = "2. средний"
                },
                new Task()
                {
                    Id= 9,
                    Title = "Обработка исключений.",
                    Description = "Приложение \'Управление саиском контактов\'.",
                    DueDate = new DateTime(2024, 6, 6),
                    Priority = "1. высокий"
                },
                new Task()
                {
                    Id= 10,
                    Title = "Подготовка к экзамену.",
                    Description = "Приложение \'Система учёта товаров в магазине\'.",
                    DueDate = new DateTime(2024, 6, 3),
                    Priority = "3. низкий"
                },
                new Task()
                {
                    Id= 11,
                    Title = "Сиреализация и работа с файлами.",
                    Description = "Приложение \'Система управления задачами\'.",
                    DueDate = new DateTime(2024, 6, 12),
                    Priority = "2. средний"
                },
            }; // Список задач                        
            Create_XML(tasks); // Сохранение задач в файле формата XML
            Create_JSON(tasks); // Сохранение задач в файле формата JSON
            Create_CSV(tasks); // Сохранение задач в файле формате CSV
            Download_XML(); // Загрузка списка задач из xml-файла
            Download_JSON(); // Загрузка списка задач из json-файла
            Download_CSV(); // Загрузка списка задач из csv-файла

            // ------------- Фильтрация задач со сроком выполнения в мае -------------------            
            List<Task> filtr = tasks.Where(el => el.DueDate.Month == 5).ToList();
            Print(filtr, "\nФильтрация задач со сроком выполнения в мае:\n");

            // ------------- Фильтрация задач со сроком выполнения в июне -------------------            
            filtr = tasks.Where(el => el.DueDate.Month == 6).ToList();
            Print(filtr, "\nФильтрация задач со сроком выполнения в июне:\n");

            // ------------- Группировка задач по приоритету (1. высокий) -------------------
            Print(tasks.Where(el => el.Priority == "1. высокий").ToList(), "\nГруппировка задач по приоритету (1. высокий):\n");

            // ------------- Группировка задач по приоритету (2. средний) -------------------
            Print(tasks.Where(el => el.Priority == "2. средний").ToList(), "\nГруппировка задач по приоритету (2. средний):\n");

            // ------------- Группировка задач по приоритету (3. низккий) -------------------
            Print(tasks.Where(el => el.Priority == "3. низкий").ToList(), "\nГруппировка задач по приоритету (3. низкий):");
                
            // ------------- Сортировка задач по возрастанию срока сдачи -------------------
            Print(tasks.OrderBy(el => el.DueDate).ToList(), "\nСортировка задач по возрастанию срока сдачи:\n");

            // ------------- Сортировка задач по возрастанию срока сдачи -------------------
            Print(tasks.OrderByDescending(el => el.DueDate).ToList(), "\nСортировка задач по убыванию срока сдачи:\n");

            // ------------- Сортировка задач по возрастанию приоретета -------------------
            Print(tasks.OrderByDescending(el => el.Priority[0]).ToList(), "\nСортировка задач по возрастанию приоретета:\n");

            // ------------- Сортировка задач по убыванию приоретета -------------------
            Print(tasks.OrderBy(el => el.Priority[0]).ToList(), "\nСортировка задач по убыванию приоретета:\n");

            // ------------ Добавление новой задачи ---------------
            Task task = new Task();
            Console.Write("Введите тему задания -> ");
            task.Title = Console.ReadLine();
            Console.Write("Введите описание задания -> ");
            task.Description = Console.ReadLine();
            Console.Write("Введите приоритет задания -> ");
            task.Priority = Console.ReadLine();
            Console.Write("Добавление нового задания в список\nВведите номер задания -> ");
            try
            {
                if (Int32.TryParse(Console.ReadLine(), out int num))
                    task.Id = num;
                else
                    throw new InvalidExcrption("Некорректно введённые данные!");
            }
            catch (InvalidExcrption ex)
            {
                Console.WriteLine($"\nОшибка добавления нового задания: {ex.Message}");
            }            
            Console.Write("Введите срок выполнения задания -> ");
            try
            {
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    task.DueDate = date;
                else
                    throw new InvalidExcrption("Некорректно введённые данные!");
            }
            catch (InvalidExcrption ex)
            {
                Console.WriteLine($"\nОшибка добавления нового задания: {ex.Message}");
            }
        }

        static void Create_XML(List<Task> obj)
        {
            XmlDocument doc_xml = new XmlDocument(); // Создаём новый объект типа XmlDocument
            XmlElement root = doc_xml.CreateElement("Tasks"); // Создаём корневой каталог документа
            doc_xml.AppendChild(root); // Закрепляем корневой каталог в документе            
            foreach (Task el in obj) // Цикл создания атрибутов и итертекста вложенного каталога
            {
                XmlElement task = doc_xml.CreateElement("Task"); // Создаём вложенный каталог
                task.SetAttribute("Id", el.Id.ToString());
                root.AppendChild(task);
                XmlElement title = doc_xml.CreateElement("Title");
                title.InnerText = el.Title;
                task.AppendChild(title);
                XmlElement disctiption = doc_xml.CreateElement("Discription");
                disctiption.InnerText = el.Description;
                task.AppendChild(disctiption);
                XmlElement dueDate = doc_xml.CreateElement("DueDate");
                dueDate.InnerText = el.DueDate.ToShortDateString();
                task.AppendChild(dueDate);
                XmlElement priority = doc_xml.CreateElement("Priority");
                priority.InnerText = el.Priority;
                task.AppendChild(priority);
            }
            doc_xml.Save("Tasks.xml"); // Сохраняем созданный файл
        }

        static void Create_JSON(List<Task> obj)
        {
            string json = JsonConvert.SerializeObject(obj); // Создаём строку json, в которую заносим всю коллекцию задач                        
            File.WriteAllText(@"Tasks.json", json); // Записываем эту строку в файл
        }

        static void Create_CSV(List<Task> obj)
        {
            string filePath = "Tasks.csv"; // Путь к файлу
            // Создаём новый файловый поток (путь к файлу, создать новый, доступ для записи)
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                // Мы должны создать буфер байтов (fstream работает с бинарными файлами)
                string text = ""; // Инициализируем строку, в которую будем записывать информацию для записи в файл
                foreach (Task el in obj) // Цикл формирования строки (данные разграничиваем подстрокой "$$$",
                                         // которая гарантированно не встретится в данных, в отличии от запятой или пробела)                    
                    text += el.Id.ToString() + "$$$" + el.Title + "$$$" + el.Description + "$$$" + el.DueDate.ToString() 
                        + "$$$" + el.Priority + '\n';
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(text); // Разбиваем текст на байты и формируем массив байтов
                fileStream.Write(buffer, 0, buffer.Length); // Записываем в поток массив байтов
            }
        }

        static void Download_XML()
        {
            XmlDocument doc_xml = new XmlDocument();
            List<Task> result = new List<Task>(); // Список задач, выгруженных из xml-файла
            doc_xml.Load("Tasks.xml"); // Открываем файл Tasks.xml
            XmlNode r = doc_xml.DocumentElement; // Вытаскиваем корневой каталог (Tasks)
            foreach (XmlNode taskNode in r.ChildNodes) // Цикл перебора задач внутри корневого каталога
            {
                Task task = new Task
                {
                    Id = Convert.ToInt32(taskNode.Attributes["Id"].Value),
                    Title = taskNode["Title"].InnerText,
                    Description = taskNode["Discription"].InnerText,
                    DueDate = Convert.ToDateTime(taskNode["DueDate"].InnerText),
                    Priority = taskNode["Priority"].InnerText
                };
                result.Add(task);
            }
            Print(result, "Загрузка задач из файла .xml:\n");
        }

        static void Download_JSON()
        {
            List<Task> result = new List<Task>(); // Список задач, выгруженных из json-файла            
            string json;
            using (StreamReader reader = new StreamReader("Tasks.json"))
            {
                json = reader.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var el in array) // Цикл формирования массива с данными, считанными из json-файла
                {
                    Task task = new Task
                    {
                        Id = Convert.ToInt32(el.Id),
                        Title = el.Title,
                        Description = el.Description,
                        DueDate = Convert.ToDateTime(el.DueDate),
                        Priority = el.Priority,
                    };
                    result.Add(task);
                }
            }
            Print(result, "\nЗагрузка задач из файла .json:\n");
        }

        static void Download_CSV()
        {
            List<Task> result = new List<Task>(); // Список задач, выгруженных из csv-файла
            using (StreamReader reader = new StreamReader("Tasks.csv"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split("$$$"); // Массив строк из строки, распарсенной по подстроке "$$$"
                    if (parts.Length == 5)
                    {
                        Task task = new Task
                        {
                            Id = Convert.ToInt32(parts[0]),
                            Title = parts[1],
                            Description = parts[2],
                            DueDate = Convert.ToDateTime(parts[3]),
                            Priority = parts[4],
                        };
                        result.Add(task);
                    }
                }
                Print(result, "\nЗагрузка задач из файла .csv:\n");
            }
        }

        static void Print(List<Task> obj, string str)
        {            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Task el in obj) 
                el.Print();
            Console.Write("Для продолжения нажмите любую клавишу -> ");
            Console.ReadKey(true);
            Console.WriteLine();
        }
    }
}