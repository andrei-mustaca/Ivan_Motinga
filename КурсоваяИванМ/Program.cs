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
            string mastersFile = @"C:\Users\andre\OneDrive\Рабочий стол\Учеба\Ivan_Motinga\masters.txt";
            string paymentFile = @"C:\Users\andre\OneDrive\Рабочий стол\Учеба\Ivan_Motinga\payment.txt";



            // Загружаем данные
            List<ClientManager> clients = ClientManager.LoadClients(clientsFile);
            List<ServiceManager> services = ServiceManager.LoadServices(servicesFile);
            List<AppointmentManager> appointments = AppointmentManager.LoadAppointments(appointmentsFile);
            List<MasterManager> masters = MasterManager.LoadMasters(mastersFile);
            List<PaymentManager> payments = PaymentManager.LoadPayments(paymentFile);

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавление");
                Console.WriteLine("2. Просмотр");
                Console.WriteLine("7. Выход");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        bool case1 = true;
                        while (case1)
                        {
                            Console.WriteLine("\nМеню добавления:");
                            Console.WriteLine("1. Добавление клиента");
                            Console.WriteLine("2. Добавление мастера");
                            Console.WriteLine("3. Добавление услуги");
                            Console.WriteLine("4. Добавление записи");
                            Console.WriteLine("5. Добавление оплаты");
                            Console.Write("Выберите действие: ");
                            string choice1 = Console.ReadLine();
                            switch (choice1)
                            {
                                case "1":
                                    Console.Clear();
                                    clients.Add(ClientManager.AddClient());
                                    ClientManager.SaveClients(clients, clientsFile);
                                    break;
                                case "2":
                                    Console.Clear();
                                    masters.Add(MasterManager.AddMaster());
                                    MasterManager.SaveMasters(masters, mastersFile);
                                    break;
                                case "3":
                                    Console.Clear();
                                    services.Add(ServiceManager.AddService());
                                    ServiceManager.SaveServices(services, servicesFile);
                                    break;
                                case "4":
                                    Console.Clear();
                                    var appointment = AppointmentManager.AddAppointment(clients,services,masters);
                                    AppointmentManager.SaveAppointments(appointments,appointmentsFile);
                                    break;
                                case "5":
                                    Console.Clear();
                                    payments.Add(PaymentManager.AddPayment(clients, services));
                                    PaymentManager.SavePayments(payments,paymentFile);
                                    break;
                                default:case1 = false;
                                    break;
                            }
                        }
                        break;

                    case "2":
                        bool case2 = true;
                        while (case2)
                        {
                            Console.WriteLine("\nМеню просмотра:");
                            Console.WriteLine("1. Просмотреть клиентов");
                            Console.WriteLine("2. Просмотреть мастеров");
                            Console.WriteLine("3. Просмотреть услуги");
                            Console.WriteLine("4. Просмотреть записи");
                            Console.WriteLine("5. Просмотреть оплаты");
                            Console.Write("Выберите действие: ");
                            string choice2 = Console.ReadLine();
                            switch (choice2)
                            {
                                case "1":
                                    Console.Clear();
                                    foreach(var elem in clients)
                                    {
                                        elem.Print();
                                    }
                                    break;
                                case "2":
                                    Console.Clear();
                                    foreach (var elem in masters)
                                    {
                                        elem.Print();
                                    }
                                    break;
                                case "3":
                                    Console.Clear();
                                    foreach (var elem in services)
                                    {
                                        elem.Print();
                                    }
                                    break;
                                case "4":
                                    Console.Clear();
                                    foreach (var elem in appointments)
                                    {
                                        elem.Print();
                                    }
                                    break;
                                case "5":
                                    Console.Clear();
                                    foreach (var elem in payments)
                                    {
                                        elem.Print();
                                    }
                                    break;
                                default:
                                    case2 = false;
                                    break;
                            }
                        }
                        break;

                    case "3":
                        Console.Write("Введите идентификатор записи, которую хотите удалить:");
                        int index = Convert.ToInt32(Console.ReadLine());
                        appointments.RemoveAll(x=>x.Id==index);
                        AppointmentManager.SaveAppointments(appointments,appointmentsFile);
                        break;

                    case "4":
                        
                        break;

                    case "5":
                        
                        break;

                    case "6":
                        
                        break;

                    case "7":
                       
                        return;

                    default:
                       
                        break;
                }
            }
        }
    }
}
