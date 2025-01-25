using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associations
{
    internal class Noeud
    {
        public int num;
        public Noeud() { }
        public Noeud(int b) { this.num = b; }
        public Noeud(string a) 
        { 
            this.num = Int32.Parse(a);
        }


        public void AfficheN()
        {
            Console.Write(num);
        }
    }
}
