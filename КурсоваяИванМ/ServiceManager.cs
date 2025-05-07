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
        public string Name { get; set; }
        public int Price { get; set; }

        public ServiceManager(string name,int price)
        {
            Name = name;
            Price = price;
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
                    services.Add(new ServiceManager(elem[0], Convert.ToInt32(elem[1])));
                }
            }
            return services;
        }

        public static void SaveServices(List<ServiceManager> services,string filePath)
        {
            List<string> list = new List<string>(services.Count);
            for (int i = 0; i < list.Capacity; i++)
            {
                string line = services[i].Name + ";" + Convert.ToString(services[i].Price);
                list.Add(line);
            }
            File.WriteAllLines(filePath, list);
        }

        public static ServiceManager AddService()
        {
            Console.Write("Введите название услуги:");
            string name = Console.ReadLine();
            Console.Write("Введите стоимость услуги:");
            int price = Convert.ToInt32(Console.ReadLine());
            return new ServiceManager(name,price);
        }
        public void Print()
        {
            Console.WriteLine($"Название услуги:{Name};Стоимость услуги:{Price}");
        }
    }
}
