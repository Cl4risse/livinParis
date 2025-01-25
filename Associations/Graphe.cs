using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associations
{
    internal class Graphe
    {
        public Noeud[] sommets;
        public Lien[] liens;

        public Graphe() { }
        public Graphe(string filename)
        {
            ReadFile(filename);
        }

#region Proprietes
        public Noeud[] Sommets
        {
            get { return sommets; }
            set { sommets = value; }
        }


        public Lien[] Liens
        {
            get { return liens; }
            set { liens = value; }
        }
#endregion

        public Graphe ReadFile(string filename)
        {
            Graphe graphe = new Graphe();
            Noeud noeud = new Noeud();
            string[] lines = null;
            if (filename != null)
            {
                lines = File.ReadAllLines(filename);
            }

            //trouver première ligne donnée
            int i;
            bool estLigne = false;
            for (i = 0; i < lines.Length && !estLigne; i++)
            {
                if (lines[i][0] != '%')
                {
                    estLigne = true;
                }
            }
            
            Noeud[] listeN = new Noeud[Int32.Parse(lines[i-1].Substring(0, lines[i-1].IndexOf(' ')))];
            Lien[] listeL = new Lien[lines.Length - i];

            for (int membre = 0; membre < listeN.Length; membre++)
            {
                listeN[membre] = new Noeud(membre + 1);
            }
            graphe.Sommets = listeN;

            int relation=i;

            for (relation = i; relation < lines.Length; relation++)
            {
                listeL[relation - i] = new Lien(lines[relation].Substring(0, lines[relation].IndexOf(' ')), lines[relation].Substring(lines[relation].IndexOf(' ') + 1, lines[relation].Length - lines[relation].IndexOf(' ') - 1));
            }

            graphe.AfficheSommets();

            graphe.Liens = listeL;

            graphe.AfficheLiens();

            
            return graphe;
        }


        public Dictionary<Noeud,List<Noeud>> GenererListeAdjacence(){
             Dictionary<Noeud,List<Noeud>> lAdjacence =null;
             return lAdjacence;
            }


        public bool NoeudExiste()
        {
            return false;
        }

        public bool EstLigne()
        {
            return false;
        }

        public void AfficheSommets()
        {
            foreach (Noeud n in sommets)
            {
                n.AfficheN();
                Console.WriteLine();
            }
        }


        public void AfficheLiens()
        {
            foreach (Lien n in liens)
            {
                n.AfficheL();
                Console.WriteLine();
            }
        }
    }
}
