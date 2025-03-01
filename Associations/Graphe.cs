using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public Dictionary<Noeud, List<Noeud>> ListeAdj;


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
                listeL[relation - i] = new Lien(Sommets[Convert.ToInt32(lines[relation].Split(' ')[0]) - 1], Sommets[Convert.ToInt32(lines[relation].Split(' ')[1]) - 1]);
                relation++;
            }
            Liens = listeL;

            this.ListeAdj = GenererListeAdjacence();

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


        public Dictionary<Noeud, List<Noeud>> GenererListeAdjacence()
        {
            Dictionary<Noeud, List<Noeud>> lAdjacence = new Dictionary<Noeud, List<Noeud>>();
            List<Noeud> noeudsAdjacents = new List<Noeud>();
            for (int i = 0; i < sommets.Length; i++)
            {
                foreach (Lien l in liens)
                {
                    if (l.n1 == sommets[i])
                    {
                        noeudsAdjacents.Add(l.n2);
                    }
                    else if (l.n2 == sommets[i])
                    {
                        noeudsAdjacents.Add(l.n1);
                    }
                }
                lAdjacence.Add(sommets[i], noeudsAdjacents.ToList());

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

        public int[][] GenererMatriceAdjacence()
        {
            int[][] mAdj = new int[this.Sommets.Length][];
            for (int i = 0; i < this.Sommets.Length; i++)
            {
                int[] ligneActuelle = new int[this.Sommets.Length];
                for (int j = 0; j < this.Sommets.Length; j++)
                {
                    ligneActuelle[j] = 0;
                }
                mAdj[i] = ligneActuelle;
            }

            foreach (Lien l in liens)
            {
                int sommet1Index = l.n1.num-1; //-1 car sommets commencent à 1
                int sommet2Index = l.n2.num-1;
                mAdj[sommet1Index][sommet2Index] = 1;
                mAdj[sommet2Index][sommet1Index] = 1;
            }

            return mAdj;
        }

        public List<Noeud> ParcoursEnLargeur(Noeud NoeudDepart)
        {
            List<Noeud> Visitees = new List<Noeud>();
            Queue<Noeud> fileNoeuds = new Queue<Noeud>();
            Visitees.Add(NoeudDepart);
            fileNoeuds.Enqueue(NoeudDepart);

            while (fileNoeuds.Count > 0) 
            { 
                Noeud element = fileNoeuds.Dequeue();
                foreach (Noeud n in this.ListeAdj[element])
                {
                    if (!Visitees.Contains(n))
                    {
                        fileNoeuds.Enqueue(n);
                        Visitees.Add(n);
                    }
                }
            }
            return Visitees;
        }
        public List<Noeud> ParcoursEnProfondeur(Noeud NoeudDepart)
        {
            List<Noeud> Visitees = new List<Noeud>();
            Stack<Noeud> pileNoeuds = new Stack<Noeud>();
            Visitees.Add(NoeudDepart);
            pileNoeuds.Push(NoeudDepart);

            while (pileNoeuds.Count > 0)
            {
                Noeud element = pileNoeuds.Pop();
                foreach (Noeud n in this.ListeAdj[element])
                {
                    if (!Visitees.Contains(n))
                    {
                        pileNoeuds.Push(n);
                        Visitees.Add(n);
                    }
                }
            }
            return Visitees;
        }



    }
}
