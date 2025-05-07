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
        public ClientManager Client { get; set; }
        public ServiceManager Service { get; set; }
        public DateTime Date { get; set; }
        public AppointmentManager(ClientManager client,ServiceManager service,DateTime date)
        {
            Client = client;
            Service = service;
            Date = date;
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
                    appointments.Add(new AppointmentManager(
                        new ClientManager(elem[0],elem[1],elem[2]),
                        new ServiceManager(elem[3],Convert.ToInt32(elem[4])),
                        Convert.ToDateTime(elem[5])));
                }
            }
            return appointments;
        }

        public static void SaveAppointments(List<AppointmentManager> appointments,string filePath)
        {
            List<string> list = new List<string>(appointments.Count);
            for (int i = 0; i < list.Capacity; i++)
            {
                string line = appointments[i].Client.Name + ";" + appointments[i].Client.Surname + ";" + appointments[i].Client.Phone+
                    ";"+appointments[i].Service.Name+";"+appointments[i].Service.Price+";"+appointments[i].Date;
                list.Add(line);
            }
            File.WriteAllLines(filePath, list);
        }

        public static AppointmentManager AddAppointment(List<ClientManager>clients,List<ServiceManager>services)
        {
            Console.Write("Введите имя клиента:");
            string name = Console.ReadLine();
            Console.Write("Введите фамилию клиента:");
            string surname = Console.ReadLine();
            Console.Write("Введите номер телефона клиента:");
            string phone = Console.ReadLine();
            int count = 0;
            foreach(var elem in clients)
            {
                if(name==elem.Name&surname==elem.Surname&&phone==elem.Phone)
                {
                    count++;
                }
            }
            if(count==0)
            {
                clients.Add(new ClientManager(name,surname,phone));
                ClientManager.SaveClients(clients, @"C:\Users\andre\OneDrive\Рабочий стол\Учеба\Ivan_Motinga\clients.txt");
            }
            n1:
            Console.WriteLine("Введите название услуги:");
            string name_s = Console.ReadLine();
            int count_s = 0;
            int position = 0;
            for (int i=0;i<services.Count;i++)
            {
                if(name_s==services[i].Name)
                {
                    count_s++;
                    position = i;
                }
            }
            if(count_s==0)
            {
                Console.Write("Такой услуги нет, попробуйте ввести еще раз");
                goto n1;
            }
            Console.WriteLine("Введите дату:");
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            return new AppointmentManager(new ClientManager(name,surname,phone),services[position],date);
        }
        public void Print()
        {
            Console.WriteLine("Дата:"+Date.ToShortDateString());
            Client.Print();
            Service.Print();
        }
    }
}
