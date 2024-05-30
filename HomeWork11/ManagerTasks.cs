using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HomeWork11
{
    public class ManagerTasks
    {
        private List<Task> tasks_ = new List<Task>();
        public List<Task> Tasks { get { return tasks_; } } // Доступ к полю для чтения данных (инкапсуляция не нарушена)
        public void AddTask(Task task) { tasks_.Add(task); } // Метод добавления нового задания
        public void RemoveTask(int id) { tasks_.RemoveAll(t => t.Id == id); } // Метод удаления задания по Id номеру
        public void CleanAll() { tasks_.Clear(); } // Метод очисти списка полностью
        public int Size() { return tasks_.Count; } // Метод возврата размера поля tasks (кол-ва заданий в списке)
        public void Change(int id, Task obj) // Метод изменения задания с номером id
        {
            tasks_.RemoveAll(t => t.Id == id); // Удаляем из списка заданий задание с Id
            tasks_.Add(obj); // Добавляем в список обновлённое задание
        }
        public void CreateXML() // Метод записи списка задач в файл формата xml
        {
            XmlDocument doc_xml = new XmlDocument(); // Создаём новый объект типа XmlDocument
            XmlElement root = doc_xml.CreateElement("Tasks"); // Создаём корневой каталог документа
            doc_xml.AppendChild(root); // Закрепляем корневой каталог в документе            
            foreach (Task el in tasks_) // Цикл создания атрибутов и итертекста вложенного каталога
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

        public void CreateJSON() // Метод записи списка задач в файл формата json
        {
            string json = JsonConvert.SerializeObject(tasks_); // Создаём строку json, в которую заносим всю коллекцию задач                        
            File.WriteAllText(@"Tasks.json", json); // Записываем эту строку в файл
        }

        public void CreateCSV() // Метод записи списка задач в файл формата csv
        {
            string filePath = "Tasks.csv"; // Путь к файлу
            // Создаём новый файловый поток (путь к файлу, создать новый, доступ для записи)
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                // Мы должны создать буфер байтов (fstream работает с бинарными файлами)
                string text = ""; // Инициализируем строку, в которую будем записывать информацию для записи в файл
                foreach (Task el in tasks_) // Цикл формирования строки (данные разграничиваем подстрокой "$$$",
                                         // которая гарантированно не встретится в данных, в отличии от запятой или пробела)                    
                    text += el.Id.ToString() + "$$$" + el.Title + "$$$" + el.Description + "$$$" + el.DueDate.ToString()
                        + "$$$" + el.Priority + '\n';
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(text); // Разбиваем текст на байты и формируем массив байтов
                fileStream.Write(buffer, 0, buffer.Length); // Записываем в поток массив байтов
            }
        }
        public IEnumerable<Task> DownloadXML() // Метод считывания задач из xml-файла
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
            return result;
        }

        public IEnumerable <Task>DownloadJSON() // Метод считывания задач из json-файла
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
            return result;
        }

        public IEnumerable<Task> DownloadCSV() // Метод считывания задач из csv-файла
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
                return result;
            }
        }
        public IEnumerable<Task> FiltrMonth(int mounth) // Метод фильтрации задач со сроком выполнения в конкретном месяце
        {
            return tasks_.Where(t => t.DueDate.Month == mounth);
        }        
        public IEnumerable<Task> GroupByPriority(string priority) // Метод группировки списка задач по приоритету
        {
            return tasks_.Where(t => t.Priority.ToLower() == priority.ToLower()).ToList();
        }
        public IEnumerable<Task> DateOrderBy() // Метод сортировки списка задач по возрастанию срока сдачи
        {
            return tasks_.OrderBy(t => t.DueDate);
        }
        public IEnumerable<Task> DateOrderByDescending() // Метод сортировки списка задач по убыванию срока сдачи
        {
            return tasks_.OrderByDescending(t => t.DueDate);
        }
        public IEnumerable<Task> PriorityOrderByDescending() // Метод сортировки списка задач по возрастанию приоритета
        {
            return tasks_.OrderByDescending(t => t.Priority[0]).ToList();
        }
        public IEnumerable<Task> PriorityOrderBy() // Метод сортировки списка задач по убыванию приоритета
        {
            return tasks_.OrderBy(t => t.Priority[0]).ToList();
        }
        public void SortId() // Метод сортировки задний в списке по возрастанию Id
        {
            tasks_ = tasks_.OrderBy(t => t.Id).ToList();
        }
    }
}
