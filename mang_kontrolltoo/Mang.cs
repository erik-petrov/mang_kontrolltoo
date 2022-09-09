using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mang_kontrolltoo
{
    internal class Mang
    {
        Tegelane[] players;

        public Mang(Tegelane[] players)
        {
            this.players = players;
        }

        public List<Tegelane> SuurimaEsemeteArvuga()
        {
            List<Tegelane> winners = new List<Tegelane>();
            Tegelane comparable = players[0];
            foreach (Tegelane plr in players)
            {
                int num = comparable.CompareTo(plr);
                if (num < 0)
                {
                    comparable = plr;
                    winners.Clear();
                }
                if(num == 0) winners.Add(plr);
            }
            winners.Add(comparable);
            return winners;
        }
        public Tegelane SuurimaPunktideArvuga()
        {
            int highest = 0;
            Tegelane winner = players[0];
            foreach (Tegelane plr in players)
            {
                int arv = plr.PuntkideArv();
                if (arv > highest) { highest = arv; winner = plr; }
            }
            return winner;
        }
    }
}
