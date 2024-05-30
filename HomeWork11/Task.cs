using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork11
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public void Print() // Метод вывода объета в консоль
        {
            Console.WriteLine($"Номер задания: {Id}\nТема: {Title}\nОписание: {Description}\n" +
                $"Срок сдачи: {DueDate.ToShortDateString()}\nПриоритет: {Priority}.\n");
        }
    }
}
