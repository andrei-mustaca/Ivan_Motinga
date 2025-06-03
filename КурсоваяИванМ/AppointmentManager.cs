using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace КурсоваяИванМ
{
    class AppointmentManager
    {
        public int Id { get; set; }
        public int Client_id { get; set; }
        public int Master_id { get; set; }
        public int Service_id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public AppointmentManager() { }
        public AppointmentManager(int id,int client,int master,int service,DateTime date,string status)
        {
            Id = id;
            Client_id = client;
            Master_id = master;
            Service_id = service;
            Date = date;
            Status = status;
        }

        public static List<AppointmentManager> LoadAppointments(string filePath)
        {
            var appointments = new List<AppointmentManager>();
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                for(int i=0;i<lines.Length;i++)
                {
                    string[] elem = lines[i].Split(';');
                    appointments.Add(new AppointmentManager(Convert.ToInt32(elem[0]),Convert.ToInt32(elem[1]),Convert.ToInt32(elem[2]),Convert.ToInt32(elem[3]),Convert.ToDateTime(elem[4]),elem[5]));
                }
            }
            return appointments;
        }

        public static void SaveAppointments(List<AppointmentManager> appointments,string filePath)
        {
            List<string> list = new List<string>(appointments.Count);
            for (int i = 0; i < list.Capacity; i++)
            {
                string line = appointments[i].Id+";"+appointments[i].Client_id + ";" + appointments[i].Master_id +";"+appointments[i].Service_id+";"+appointments[i].Date+";"+appointments[i].Status;
                list.Add(line);
            }
            File.WriteAllLines(filePath, list);
        }

        public static AppointmentManager AddAppointment(List<ClientManager>clients,List<ServiceManager>services,List<MasterManager> masters)
        {
            try
            {
                Console.Write("Введите идентификатор записи:");
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
                Console.WriteLine("Введите имя мастера:");
                string name_master = Console.ReadLine();
                int master_id = 0;
                foreach (var elem in masters)
                {
                    if (name_master == elem.FullName)
                    {
                        master_id = elem.Id;
                    }
                }
                Console.Write("Введите название услуги:");
                string services_name = Console.ReadLine();
                int services_id = 0;
                foreach (var elem in services)
                {
                    if (services_name == elem.Name)
                    {
                        services_id = elem.Id;
                    }
                }
                Console.WriteLine("Введите дату:");
                DateTime date = Convert.ToDateTime(Console.ReadLine());
                Console.Write("Введите статус:");
                string status = Console.ReadLine();
                return new AppointmentManager(id, client_id, master_id, services_id, date, status);
            }
            catch
            {
                Console.Write("Произошла ошибка при добавлении, выйдете из программы и повторите ввод заново");
                return new AppointmentManager();
            }
        }
        public void Print()
        {
            Console.WriteLine($"ID:{Id};ID-клиента:{Client_id};ID-мастера:{Master_id};ID-Services:{Service_id};Дата:{Date.ToShortDateString()};Статус:{Status}");
        }
    }
}
