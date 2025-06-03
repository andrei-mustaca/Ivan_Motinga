using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace КурсоваяИванМ
{
    class ServiceManager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public TimeSpan Duration { get; set; }
        public string Category { get; set; }

        public ServiceManager(int id,string name,int price,TimeSpan duration,string category)
        {
            Id = id;
            Name = name;
            Price = price;
            Duration = duration;
            Category = category;
        }

        public static List<ServiceManager> LoadServices(string filePath)
        {
            var services = new List<ServiceManager>();
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] elem = lines[i].Split(';');
                    services.Add(new ServiceManager(Convert.ToInt32(elem[0]),elem[1], Convert.ToInt32(elem[2]),TimeSpan.Parse(elem[3]),elem[4]));
                }
            }
            return services;
        }

        public static void SaveServices(List<ServiceManager> services,string filePath)
        {
            List<string> list = new List<string>(services.Count);
            for (int i = 0; i < list.Capacity; i++)
            {
                string line = Convert.ToString(services[i].Id)+";"+services[i].Name + ";" + Convert.ToString(services[i].Price)+";"+Convert.ToString(services[i].Duration)+";"+services[i].Category;
                list.Add(line);
            }
            File.WriteAllLines(filePath, list);
        }

        public static ServiceManager AddService()
        {
            Console.Write("Введите идентификатор услуги:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите название услуги:");
            string name = Console.ReadLine();
            Console.Write("Введите стоимость услуги:");
            int price = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите часы: ");
            int hours = int.Parse(Console.ReadLine());
            Console.Write("Введите минуты: ");
            int minutes = int.Parse(Console.ReadLine());
            TimeSpan time = new TimeSpan(hours, minutes, 0);
            Console.Write("Введите категорию услуги:");
            string category = Console.ReadLine();
            return new ServiceManager(id,name,price,time,category);
        }
        public void Print()
        {
            Console.WriteLine($"ID:{Id};Название услуги:{Name};Стоимость услуги:{Price};Продолжительность:{Duration};Категория:{Category}");
        }
    }
}
