using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace КурсоваяИванМ
{
    class Program
    {
        static void Main(string[] args)
        {
            string clientsFile = @"C:\Users\andre\OneDrive\Рабочий стол\Учеба\Ivan_Motinga\clients.txt";
            string servicesFile = @"C:\Users\andre\OneDrive\Рабочий стол\Учеба\Ivan_Motinga\services.txt";
            string appointmentsFile = @"C:\Users\andre\OneDrive\Рабочий стол\Учеба\Ivan_Motinga\appointments.txt";



            // Загружаем данные
            List<ClientManager> clients = ClientManager.LoadClients(clientsFile);
            List<ServiceManager> services = ServiceManager.LoadServices(servicesFile);
            List<AppointmentManager> appointments = AppointmentManager.LoadAppointments(appointmentsFile);

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить клиента");
                Console.WriteLine("2. Добавить услугу");
                Console.WriteLine("3. Записать клиента на прием");
                Console.WriteLine("4. Просмотр клиентов");
                Console.WriteLine("5. Просмотр услуг");
                Console.WriteLine("6. Просмотр записей на прием");
                Console.WriteLine("7. Выход");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        clients.Add(ClientManager.AddClient());
                        ClientManager.SaveClients(clients,clientsFile);
                        Console.Write("Клиент успешно добавлен!");
                        break;

                    case "2":
                        Console.Clear();
                        services.Add(ServiceManager.AddService());
                        ServiceManager.SaveServices(services,servicesFile);
                        Console.Write("Услуга успешно добавлена!");
                        break;

                    case "3":
                        Console.Clear();
                        appointments.Add(AppointmentManager.AddAppointment(clients,services));
                        AppointmentManager.SaveAppointments(appointments, appointmentsFile);
                        Console.WriteLine($"Запись на прием добавлена.");
                        break;

                    case "4":
                        Console.Clear();
                        clients = ClientManager.LoadClients(clientsFile);
                        Console.WriteLine("Клиенты:");
                        foreach (var client in clients)
                        {
                            client.Print();
                        }
                        break;

                    case "5":
                        Console.Clear();
                        services = ServiceManager.LoadServices(servicesFile);
                        Console.WriteLine("Услуги:");
                        foreach (var service in services)
                        {
                            service.Print();
                        }
                        break;

                    case "6":
                        Console.Clear();
                        Console.WriteLine("Записи на прием:");
                        foreach (var appointment in appointments)
                        {
                            appointment.Print();
                        }
                        break;

                    case "7":
                        Console.WriteLine("Выход из программы.");
                        return;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
    }
}
