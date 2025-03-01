using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace Associations
{
    internal class Noeud
    {
        public int num;
        public int x;
        public int y;
        public Noeud() { }
        public Noeud(int b) { this.num = b; }
        public Noeud(string a)
        {
            this.num = Int32.Parse(a);
        }

        public void AjouterCoords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void AfficheN()
        {
            Console.Write(num);
        }

    }
}
