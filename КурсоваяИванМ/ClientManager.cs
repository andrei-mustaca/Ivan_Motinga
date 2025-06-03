using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace КурсоваяИванМ
{
    class ClientManager
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }

        public ClientManager(int id,string fullname,string phone)
        {
            Id = id;
            FullName = fullname;
            Phone = phone;
        }

        public static List<ClientManager> LoadClients(string filePath)
        {
            var clients = new List<ClientManager>();
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                for(int i=0;i<lines.Length;i++)
                {
                    string[] elem = lines[i].Split(';');
                    clients.Add(new ClientManager(Convert.ToInt32(elem[0]), elem[1], elem[2]));
                }
            }
            return clients;
        }

        public static void SaveClients(List<ClientManager> clients,string filePath)
        {
            List<string> list = new List<string>(clients.Count);
            for(int i=0;i<list.Capacity;i++)
            {
                string line = clients[i].Id+";"+clients[i].FullName+ ";" + clients[i].Phone;
                list.Add(line);
            }
            File.WriteAllLines(filePath, list);
        }

        public static ClientManager AddClient()
        {
            Console.Write("Введите идентификатор пользователя:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите ФИО клиента:");
            string name = Console.ReadLine();
            Console.Write("Введите номер телефона клиента:");
            string phone = Console.ReadLine();
            return new ClientManager(id,name,phone);
        }
        public void Print()
        {
            Console.WriteLine($"ID:{Id};ФИО:{FullName};Номер телефона:{Phone}");
        }
    }
}
