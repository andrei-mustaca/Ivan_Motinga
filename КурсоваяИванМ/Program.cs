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
                Console.WriteLine("3. Удаление");
                Console.WriteLine("4. Записи на текущую неделю");
                Console.WriteLine("5. Самая востребованная услуга");
                Console.WriteLine("6. Раписание мастеров");
                Console.WriteLine("7. Финансовая статистика");
                Console.WriteLine("8. Выход");

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
                                    appointments.Insert(appointments.Count,AppointmentManager.AddAppointment(clients,services,masters));
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
                        Console.Clear();
                        Console.WriteLine("Записи на текущую неделю:");
                        DateTime today = DateTime.Today;
                        DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
                        DateTime endOfWeek = startOfWeek.AddDays(6);
                        var weekAppointments = appointments
                            .Where(a => a.Date >= startOfWeek && a.Date <= endOfWeek)
                            .OrderBy(a => a.Date);
                        foreach (var appointment in weekAppointments)
                        {
                            var client = clients.FirstOrDefault(c => c.Id == appointment.Client_id);
                            var master = masters.FirstOrDefault(m => m.Id == appointment.Master_id);
                            var service = services.FirstOrDefault(s => s.Id == appointment.Service_id);
                            Console.WriteLine($"ID: {appointment.Id}, Клиент: {client?.FullName}, Мастер: {master?.FullName}, Услуга: {service?.Name}, Дата: {appointment.Date.ToShortDateString()}, Статус: {appointment.Status}");
                        }
                        break;

                    case "5":
                        Console.Clear();
                        Console.WriteLine("Самые востребованные услуги:");
                        var serviceCounts = appointments
                            .GroupBy(a => a.Service_id)
                            .Select(g => new { ServiceId = g.Key, Count = g.Count() })
                            .OrderByDescending(g => g.Count)
                            .Take(5);
                        foreach (var item in serviceCounts)
                        {
                            var service = services.FirstOrDefault(s => s.Id == item.ServiceId);
                            Console.WriteLine($"Услуга: {service?.Name}, Количество записей: {item.Count}");
                        }
                        break;

                    case "6":
                        Console.Clear();
                        Console.WriteLine("Расписание мастеров:");
                        foreach (var master in masters)
                        {
                            Console.WriteLine($"Мастер: {master.FullName}, Специализация: {master.Specialization}");
                            Console.WriteLine($"График: {master.Grafic[0]}, {master.Grafic[1]}, {master.Grafic[2]}");
                            Console.WriteLine();
                        }
                        break;

                    case "7":
                        Console.Clear();
                        Console.WriteLine("Финансовая статистика:");
                        // Средний чек клиента
                        var clientPayments = payments
                            .GroupBy(p => p.Client_id)
                            .Select(g => new { ClientId = g.Key, Average = g.Average(p => p.Price) });
                        foreach (var cp in clientPayments)
                        {
                            var client = clients.FirstOrDefault(c => c.Id == cp.ClientId);
                            Console.WriteLine($"Клиент: {client?.FullName}, Средний чек: {cp.Average:F2}");
                        }
                        // Общая выручка за месяц
                        Console.Write("Введите год (например, 2025): ");
                        int year = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Введите месяц (1-12): ");
                        int month = Convert.ToInt32(Console.ReadLine());
                        var monthlyRevenue = payments
                            .Where(p => p.Date.Year == year && p.Date.Month == month && p.Status.ToLower() == "оплачено")
                            .Sum(p => p.Price);
                        Console.WriteLine($"Общая выручка за {month}/{year}: {monthlyRevenue}");
                        break;
                    case "8":return;
                    default:
                        Console.WriteLine("Такого пункта нет, введите еще раз");
                        break;
                }
            }
        }
    }
}
