using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace КурсоваяИванМ
{
    class PaymentManager
    {
        public int Id { get; set; }
        public int Client_id { get; set; }
        public int Services_id { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public string Status { get; set; }

        public PaymentManager(int id, int client_id, int services_id,DateTime date,int price,string status)
        {
            Id = id;
            Client_id = client_id;
            Services_id = services_id;
            Date = date;
            Price = price;
            Status = status;
        }

        public static List<PaymentManager> LoadPayments(string filePath)
        {
            var payments = new List<PaymentManager>();
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] elem = lines[i].Split(';');
                    payments.Add(new PaymentManager(Convert.ToInt32(elem[0]), Convert.ToInt32(elem[1]), Convert.ToInt32(elem[2]),Convert.ToDateTime(elem[3]),Convert.ToInt32(elem[4]),elem[5]));
                }
            }
            return payments;
        }

        public static void SavePayments(List<PaymentManager> payments, string filePath)
        {
            List<string> list = new List<string>(payments.Count);
            for (int i = 0; i < list.Capacity; i++)
            {
                string line = payments[i].Id + ";" + payments[i].Client_id + ";" + payments[i].Services_id+";"+payments[i].Date+";"+payments[i].Price+";"+payments[i].Status;
                list.Add(line);
            }
            File.WriteAllLines(filePath, list);
        }

        public static PaymentManager AddPayment(List<ClientManager> clients,List<ServiceManager> services)
        {
            Console.Write("Введите идентификатор:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите имя клиента:");
            string name = Console.ReadLine();
            int client_id = 0;
            foreach (var elem in clients)
            {
                if (name == elem.FullName)
                {
                    client_id = elem.Id;
                }
            }
            Console.Write("Введите название услуги:");
            string services_name = Console.ReadLine();
            int services_id = 0;
            int price = 0;
            foreach (var elem in services)
            {
                if (services_name == elem.Name)
                {
                    services_id = elem.Id;
                    price = elem.Price;
                }
            }
            Console.WriteLine("Введите дату:");
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Введите статус:");
            string status = Console.ReadLine();
            return new PaymentManager(id, client_id,services_id,date,price, status);
        }
        public void Print()
        {
            Console.WriteLine($"ID:{Id};ID-Клиента:{Client_id};ID-Услуги:{Services_id};Дата:{Date.ToShortDateString()};Статус:{Status};Цена:{Price}");
        }
    }
}
