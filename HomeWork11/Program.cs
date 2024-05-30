using System.ComponentModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace HomeWork11
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            ManagerTasks managerTasks = new ManagerTasks(); // Объявляем и инициализируем задачами объект класса "Управление задачами"
            managerTasks.AddTask(
                new Task
                {
                    Id = 1,
                    Title = "Введение в платформу Microsoft.NET.",
                    Description = "Решение четырёх задач.",
                    DueDate = new DateTime(2024, 5, 1),
                    Priority = "3. низкий"
                });
            managerTasks.AddTask(
                new Task()
                {
                    Id = 2,
                    Title = "Массивы и строки.",
                    Description = "Решение семи заданий.",
                    DueDate = new DateTime(2024, 5, 9),
                    Priority = "2. средний"
                });
            managerTasks.AddTask(
                new Task()
                {
                    Id = 3,
                    Title = "Классы, структуры, свойства. Обработка исключений.",
                    Description = "Решения шести заданий.",
                    DueDate = new DateTime(2024, 5, 27),
                    Priority = "1. высокий"
                });
            managerTasks.AddTask(
                new Task()
                {
                    Id = 4,
                    Title = "Наследование и полиморфизм. Пространство имён.",
                    Description = "Разработка игры \'Крестики-нолики\'. Азбука Морзе. Решение ещё четырёх задач.",
                    DueDate = new DateTime(2024, 5, 27),
                    Priority = "3. низкий"
                });
            managerTasks.AddTask(
                new Task()
                {
                    Id = 5,
                    Title = "Перегрузка операторов.",
                    Description = "Приложение \'Список книг для прочтения\'. Операции с матрицами.",
                    DueDate = new DateTime(2024, 5, 22),
                    Priority = "2. средний"
                });
            managerTasks.AddTask(
                new Task()
                {
                    Id = 6,
                    Title = "Интерфейсы",
                    Description = "Программа поэтапного строительства дома.",
                    DueDate = new DateTime(2024, 5, 23),
                    Priority = "1. высокий"
                });
            managerTasks.AddTask(
                new Task()
                {
                    Id = 7,
                    Title = "Generics.",
                    Description = "Приложение \'Покупка недвижимости\'.",
                    DueDate = new DateTime(2024, 6, 3),
                    Priority = "3. низкий"
                });
            managerTasks.AddTask(
                new Task()
                {
                    Id = 8,
                    Title = "Делегаты, события и LINQ.",
                    Description = "Решение трёх задач.",
                    DueDate = new DateTime(2024, 6, 5),
                    Priority = "2. средний"
                });
            managerTasks.AddTask(
                new Task()
                {
                    Id = 9,
                    Title = "Обработка исключений.",
                    Description = "Приложение \'Управление саиском контактов\'.",
                    DueDate = new DateTime(2024, 6, 6),
                    Priority = "1. высокий"
                });
            managerTasks.AddTask(
                new Task()
                {
                    Id = 10,
                    Title = "Подготовка к экзамену.",
                    Description = "Приложение \'Система учёта товаров в магазине\'.",
                    DueDate = new DateTime(2024, 6, 3),
                    Priority = "3. низкий"
                });
            managerTasks.AddTask(
                new Task()
                {
                    Id = 11,
                    Title = "Сиреализация и работа с файлами.",
                    Description = "Приложение \'Система управления задачами\'.",
                    DueDate = new DateTime(2024, 6, 12),
                    Priority = "2. средний"
                });
            managerTasks.SortId(); // Сортируем список заданий по возрастанию Id
            managerTasks.CreateXML(); // Сохранение списка задач в файле формата XML
            managerTasks.CreateJSON(); // Сохранение списка задач в файле формата JSON
            managerTasks.CreateCSV(); // Сохранение списка задач в файле формате CSV
            managerTasks.CleanAll(); // Чистим список заданий полностью
            foreach (var el in managerTasks.DownloadXML()) // Цикл записи в список заданий считанной из xml-файла информации
                managerTasks.AddTask(el);
            Print(managerTasks, "Загрузка списка задач из файла .xml:\n");
            managerTasks.CleanAll();
            foreach (var el in managerTasks.DownloadJSON()) // Цикл записи в список заданий считанной из json-файла информации
                managerTasks.AddTask(el);
            Print(managerTasks.DownloadJSON(), "\nЗагрузка списка задач из файла .json:\n");
            managerTasks.CleanAll();
            foreach (var el in managerTasks.DownloadCSV()) // Цикл записи в список заданий считанной из csv-файла информации
                managerTasks.AddTask(el);
            Print(managerTasks.DownloadCSV(), "\nЗагрузка списка задач из файла .csv:\n");
            
            // ---------------- Фильтрация сроков выполнения задач из списка ------------------
            Print(managerTasks.FiltrMonth(5), "\nФильтрация задач со сроком выполнения в мае:\n");                        
            Print(managerTasks.FiltrMonth(6), "\nФильтрация задач со сроком выполнения в июне:\n");
            
            // ---------------- Группировка списка задач по приоритету выполения ----------------------
            Print(managerTasks.GroupByPriority("1. высокий"), "\nГруппировка задач по приоритету (1. высокий):\n");                        
            Print(managerTasks.GroupByPriority("2. средний"), "\nГруппировка задач по приоритету (2. средний):\n");                       
            Print(managerTasks.GroupByPriority("3. низкий"), "\nГруппировка задач по приоритету (3. низкий):\n");
                
            // ------------- Сортировка списка задач по сроку сдачи -------------------
            Print(managerTasks.DateOrderBy(), "\nСортировка задач по возрастанию срока сдачи:\n");            
            Print(managerTasks.DateOrderByDescending(), "\nСортировка задач по убыванию срока сдачи:\n");

            // ------------- Сортировка списка задач по приоретету -------------------
            Print(managerTasks.PriorityOrderByDescending(), "\nСортировка задач по возрастанию приоретета:\n");            
            Print(managerTasks.PriorityOrderBy(), "\nСортировка задач по убыванию приоретета:\n");

            // ------------ Добавление новой задачи пользователем ---------------
            Task task = new Task();
            bool key_mistake = false; // Код ошибки (true - если невозможно внести новую задачу в список)
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nДобавление новой задачи пользователем:\n");
            Console.ForegroundColor= ConsoleColor.White;
            Console.Write("Введите тему задания -> ");
            task.Title = Console.ReadLine();
            Console.Write("Введите описание задания -> ");
            task.Description = Console.ReadLine();
            Console.Write("Введите приоритет задания -> ");
            task.Priority = Console.ReadLine();
            Console.Write("Введите номер задания -> ");
            try
            {
                if (Int32.TryParse(Console.ReadLine(), out int num) && num > 0)
                {
                    foreach (Task el in managerTasks.Tasks)
                        if (el.Id == num)
                        {
                            key_mistake = true;
                            throw new InvalidExcrption("Задание с таким Id уже усть!");                            
                        }                       
                    task.Id = num;
                }
                else
                {
                    key_mistake = true;
                    throw new InvalidExcrption("Некорректно введённые данные!");
                }
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
                {
                    key_mistake = true;
                    throw new InvalidExcrption("Некорректно введённые данные!");
                }
            }
            catch (InvalidExcrption ex)
            {
                Console.WriteLine($"\nОшибка добавления нового задания: {ex.Message}");
            }
            if (!key_mistake) // Если данные пользователем введены корректно, то добавляем новое задание в список заданий
                managerTasks.AddTask(task);
            
            // ------------------ Удаление задания по Id из списка заданий ----------------
            key_mistake = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nУдаление задания из списка заданий.\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Введите Id номер задания -> ");
            try
            {
                if (Int32.TryParse(Console.ReadLine(), out int num) && num > 0)
                {
                    foreach (var el in managerTasks.Tasks)
                        if (el.Id == num)
                        {
                            managerTasks.RemoveTask(num);
                            key_mistake = true;
                            break;
                        }
                    if (!key_mistake)
                        throw new InvalidExcrption("Такого Id номера задания нет в списке!");
                }
                else
                    throw new InvalidExcrption("Некорректно введённое значение!");
            }
            catch (InvalidExcrption ex)
            {
                Console.WriteLine($"\nОшибка удаления задания: {ex.Message}");
            }

            // ------------------- Редактирование задания -----------------------
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nРедактирование задания.\n");
            Console.ForegroundColor = ConsoleColor.White;
            key_mistake = false;
            bool flag = false;
            Task task2 = new Task();
            Console.Write("Введите Id номер задания -> ");
            try
            {
                if (Int32.TryParse(Console.ReadLine(), out int num) && num > 0)
                {
                    foreach (var el in managerTasks.Tasks)
                        if (el.Id == num)
                        {
                            task2.Id = num;
                            flag = true;
                            break;
                        }
                    if (!flag)
                    {
                        key_mistake = true;
                        throw new InvalidExcrption("Такого Id номера задания нет в списке!");
                    }
                }
                else
                {
                    key_mistake = true;
                    throw new InvalidExcrption("Некорректно введённое значение!");
                }
            }
            catch (InvalidExcrption ex)
            {
                Console.WriteLine($"\nОшибка редактирования задания: {ex.Message}");
            }
            if (!key_mistake)
            {
                Console.Write("Обновите срок выполнения задания -> ");
                try
                {
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                        task2.DueDate = date;
                    else
                    {
                        key_mistake = true;
                        throw new InvalidExcrption("Некорректно введённые данные!");
                    }
                }
                catch (InvalidExcrption ex)
                {
                    Console.WriteLine($"\nОшибка редактирования задания: {ex.Message}");
                }
            }
            if (!key_mistake)
            {
                Console.Write("Введите тему задания -> ");
                task2.Title = Console.ReadLine();
                Console.Write("Введите описание задания -> ");
                task2.Description = Console.ReadLine();
                Console.Write("Введите приоритет задания -> ");
                task2.Priority = Console.ReadLine();
                managerTasks.Change(task2.Id, task2);
            }
            managerTasks.SortId(); // Сортируем обновлённый список заданий по возрастанию Id            
            managerTasks.CreateXML(); // Сохранение обновлённого списка задач в файле формата XML
            managerTasks.CreateJSON(); // Сохранение обнослённого списка задач в файле формата JSON
            managerTasks.CreateCSV(); // Сохранение обсновлённого списка задач в файле формате CSV
            managerTasks.CleanAll(); // Чистим список заданий полностью
            foreach (var el in managerTasks.DownloadXML()) // Цикл записи в список заданий считанной из xml-файла информации
                managerTasks.AddTask(el);
            Print(managerTasks, "\nЗагрузка списка задач из файла .xml:\n");
            managerTasks.CleanAll();
            foreach (var el in managerTasks.DownloadJSON()) // Цикл записи в список заданий считанной из json-файла информации
                managerTasks.AddTask(el);
            Print(managerTasks.DownloadJSON(), "\nЗагрузка списка задач из файла .json:\n");
            managerTasks.CleanAll();
            foreach (var el in managerTasks.DownloadCSV()) // Цикл записи в список заданий считанной из csv-файла информации
                managerTasks.AddTask(el);
            Print(managerTasks.DownloadCSV(), "\nЗагрузка списка задач из файла .csv:\n");
        }
        static void Print(IEnumerable<Task> obj, string str) // Перегруженный метод вывода списка задач в консоль
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
            if (obj.Count() > 0)
                foreach (Task el in obj)
                    el.Print();
            else 
                Console.WriteLine("Файл пустой!");
            Console.Write("Для продолжения нажмите любую клавишу -> ");
            Console.ReadKey(true);
            Console.WriteLine();
        }
        static void Print(ManagerTasks obj, string str) 
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
            if (obj.Size() > 0)
                foreach (Task el in obj.Tasks)
                    el.Print();
            else
                Console.WriteLine("Файл пустой!");
            Console.Write("Для продолжения нажмите любую клавишу -> ");
            Console.ReadKey(true);
            Console.WriteLine();
        }
    }
}