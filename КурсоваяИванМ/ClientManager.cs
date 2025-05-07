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
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }

        public ClientManager(string name,string surname,string phone)
        {
            Name = name;
            Surname = surname;
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
                    clients.Add(new ClientManager(elem[0], elem[1], elem[2]));
                }
            }
            return clients;
        }

        public static void SaveClients(List<ClientManager> clients,string filePath)
        {
            List<string> list = new List<string>(clients.Count);
            for(int i=0;i<list.Capacity;i++)
            {
                string line = clients[i].Surname + ";" +clients[i].Name+ ";" + clients[i].Phone;
                list.Add(line);
            }
            File.WriteAllLines(filePath, list);
        }

        public static ClientManager AddClient()
        {
            Console.Write("Введите имя клиента:");
            string name = Console.ReadLine();
            Console.Write("Введите фамилию клиента:");
            string surname = Console.ReadLine();
            Console.Write("Введите номер телефона клиента:");
            string phone = Console.ReadLine();
            return new ClientManager(name,surname,phone);
        }
        public void Print()
        {
            Console.WriteLine($"Фамилия:{Surname};Имя:{Name};Номер телефона:{Phone}");
        }
    }
}
