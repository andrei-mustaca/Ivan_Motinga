using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace КурсоваяИванМ
{
    class MasterManager
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public string[] Grafic = new string[3];

        public MasterManager(int id, string fullname, string specialization,string[] grafic)
        {
            Id = id;
            FullName = fullname;
            Specialization = specialization;
            Grafic = grafic;
        }

        public static List<MasterManager> LoadMasters(string filePath)
        {
            var masters = new List<MasterManager>();
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] elem = lines[i].Split(';');
                    masters.Add(new MasterManager(Convert.ToInt32(elem[0]), elem[1], elem[2],new string[] { elem[3],elem[4],elem[5]}));
                }
            }
            return masters;
        }

        public static void SaveMasters(List<MasterManager> masters, string filePath)
        {
            List<string> list = new List<string>(masters.Count);
            for (int i = 0; i < list.Capacity; i++)
            {
                string line = masters[i].Id + ";" + masters[i].FullName + ";" + masters[i].Specialization+";"+masters[i].Grafic[0]+";"+masters[i].Grafic[1]+";"+masters[i].Grafic[2];
                list.Add(line);
            }
            File.WriteAllLines(filePath, list);
        }

        public static MasterManager AddMaster()
        {
            Console.Write("Введите идентификатор мастера:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите ФИО мастера:");
            string name = Console.ReadLine();
            Console.Write("Введите специализацию мастера:");
            string specialization = Console.ReadLine();
            Console.Write("Введите первый рабочий день:");
            string one = Console.ReadLine();
            Console.Write("Введите второй рабочий день:");
            string two = Console.ReadLine();
            Console.Write("Введите третий рабочий день:");
            string three = Console.ReadLine();
            return new MasterManager(id, name, specialization,new string[] { one,two,three});
        }
        public void Print()
        {
            Console.WriteLine($"ID:{Id};ФИО:{FullName};Специализация:{Specialization};График:{Grafic[0]},{Grafic[1]},{Grafic[2]}");
        }
    }
}
