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

        public Lien(Noeud a, Noeud b)
        {
            n1 = a;
            n2 = b;
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
