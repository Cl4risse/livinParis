using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associations
{
    internal class Lien
    {
        public Noeud n1;
        public Noeud n2;
        public Lien() { }

        public Lien(string a, string b)
        {
            n1 = new Noeud(a);
            n2 = new Noeud(b);
        }

        public void AfficheL()
        {
            Console.Write("(");
            n1.AfficheN();
            Console.Write(",");
            n2.AfficheN();
            Console.Write(")");

        }
    }
}
