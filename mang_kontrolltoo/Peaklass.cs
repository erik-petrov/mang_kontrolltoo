using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mang_kontrolltoo
{
    internal static class Peaklass
    {
        static string itemFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/items.txt";
        static string usedNameFile = "../../../usedName.txt";
        static string leaderboardFile = "../../../leaderboard.txt";

        public static Random rnd = new Random();
        static List<string> usedNames = new List<string>();
        static Dictionary<string, int> leaderboard = new Dictionary<string, int>();
        public static List<Ese> LoeEsemed()
        {
            List<Ese> list = new List<Ese>();
            using(StreamReader sr = new StreamReader(itemFile))
            {
                while (!sr.EndOfStream)
                {
                    string[] info = sr.ReadLine().Split(";");
                    Ese item = new Ese(stringToInt(info[1]), info[0]);
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<string> GetUsedNames()
        {
            using (StreamReader sr = new StreamReader(usedNameFile))
            {
                while (!sr.EndOfStream)
                {
                    string name = sr.ReadLine();
                    usedNames.Add(name);
                }
            }
            return usedNames;
        }

        public static Dictionary<string ,int> GetLeaderboard()
        {
            using (StreamReader sr = new StreamReader(leaderboardFile))
            {
                while (!sr.EndOfStream)
                {
                    string[] plr = sr.ReadLine().Split(':');
                    leaderboard.Add(plr[0], stringToInt(plr[1]));
                }
            }
            return leaderboard;
        }

        static int stringToInt(string s)
        {
            s = Regex.Replace(s, @"\s+", "");
            int y = 0;
            int total = 0;
            for (int i = 0; i < s.Length; i++)
                y = y * 10 + (s[i] - '0');
            total += y;
            return total;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        static string genName()
        {
            int len = rnd.Next(4, 9);
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[rnd.Next(consonants.Length)].ToUpper();
            Name += vowels[rnd.Next(vowels.Length)];
            int b = 2;
            while (b < len)
            {
                Name += consonants[rnd.Next(consonants.Length)];
                b++;
                Name += vowels[rnd.Next(vowels.Length)];
                b++;
            }
            return Name;
        }

        static string getName()
        {
            string name = genName();
            if (!usedNames.Any()) return name;
            while (usedNames.Contains(name))
                name = genName();
            return name;
        }

        static void FillLeaderboard(Tegelane[] arr)
        {
            Dictionary<string,int> dict = GetLeaderboard();
            for (int i = 0; i < arr.Length; i++)
            {
                dict.Add(arr[i].nimi, arr[i].PuntkideArv());
            }
            var sortedDict = from entry in dict orderby entry.Value descending select entry;
            using (StreamWriter sw = new StreamWriter("../../../leaderboard.txt"))
            {
                foreach (KeyValuePair<string, int> item in sortedDict)
                {
                    sw.WriteLine($"{item.Key}:{item.Value}");
                }
            }
        }

        static Tegelane[] populatePlayers(int plrCount)
        {
            Tegelane[] plrs = new Tegelane[plrCount];
            using (StreamWriter sw = new StreamWriter("../../../usedNames.txt", append: true))
            {
                if (plrCount < 4) throw new Exception();
                for (int i = 0; i < plrCount; i++)
                {
                    Tegelane plr = new Tegelane(getName());
                    sw.WriteLine(plr.nimi);
                    plrs[i] = plr;
                }
                sw.Dispose();
            }
            return giveOutItems(plrs);
        }

        static Tegelane[] giveOutItems(Tegelane[] plrs)
        {
            List<Ese> itemList = LoeEsemed();
            if (itemList.Count <= 0) throw new ArgumentOutOfRangeException();
            foreach (Tegelane plr in plrs)
            {
                Shuffle(itemList);
                int amount = rnd.Next(2, 10);
                for (int i = 0; i < amount; i++)
                {
                    plr.Equip(itemList[i]);
                }
            }
            return plrs;
        }

        static public void PrintLeaderboard(int rows)
        {
            int total = TotalLines(leaderboardFile);
            using (StreamReader sr = new StreamReader(leaderboardFile))
            {
                if (total < rows)
                    rows = total;
                for (int i = 0; i < rows; i++)
                {
                    Console.WriteLine(sr.ReadLine());
                }
            }
        }

        static int TotalLines(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                int i = 0;
                while (r.ReadLine() != null) { i++; }
                return i;
            }
        }

        static public void PlayGame(int plrCount)
        {
            Tegelane[] plrs = populatePlayers(plrCount);
            Mang mang = new Mang(plrs);
            foreach(Tegelane winner in mang.SuurimaEsemeteArvuga())
            {
                Console.WriteLine(winner.Info());
            }
            Tegelane win = mang.SuurimaPunktideArvuga();
            Console.WriteLine(win.Info());
            Console.WriteLine("Игрок имел следующие предметы: ");
            win.väljastaEsemed();
            FillLeaderboard(plrs);
        }
    }
}
