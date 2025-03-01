using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associations
{
    internal class Graphe
    {
        public Noeud[] sommets; //liste des sommets
        public Lien[] liens; //liste des arrêtes

        public Graphe(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            //trouver première ligne donnée
            int i = 0;
            bool estLigne = false;
            while (!estLigne)
            {
                if (lines[i][0] != '%')
                {
                    estLigne = true;
                }
                else
                {
                    i++;
                }
            }
            
            Noeud[] listeN = new Noeud[Int32.Parse(lines[i].Substring(0, lines[i].IndexOf(' ')))];
            Lien[] listeL = new Lien[lines.Length - i - 1];

            for (int membre = 0; membre < listeN.Length; membre++)
            {
                listeN[membre] = new Noeud(membre + 1);
            }
            Sommets = listeN;

            i++;
            int relation = i;   //ici, lines[relation] est la première relation
            

            while (relation < lines.Length)
            {
                listeL[relation-i] = new Lien(Sommets[Convert.ToInt32(lines[relation].Split(' ')[0])-1], Sommets[Convert.ToInt32(lines[relation].Split(' ')[1])-1]);
                relation++;
            }
            Liens = listeL;

            AfficheSommets();
            AfficheLiens();
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


        public Dictionary<Noeud,List<Noeud>> GenererListeAdjacence(){
            Dictionary<Noeud,List<Noeud>> lAdjacence = new Dictionary<Noeud,List<Noeud>>();
            List<Noeud> noeudsAdjacents = new List<Noeud>();
            for (int i = 0; i<sommets.Length;i++)
            {
                foreach (Lien l in liens)
                {
                    if(l.n1 == sommets[i])
                    {
                        noeudsAdjacents.Add(l.n2);
                    }
                    else if(l.n2 == sommets[i])
                    {
                        noeudsAdjacents.Add(l.n1);
                    }
                }
                lAdjacence.Add(sommets[i],noeudsAdjacents);
                noeudsAdjacents.Clear();
            }

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
