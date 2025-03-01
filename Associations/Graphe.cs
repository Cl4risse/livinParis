using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SkiaSharp;


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

        public bool contientCycle(int[][] matriceAdj,Noeud noeud)
        {
            int n = matriceAdj.Length;
            Queue<Noeud> fileNoeuds = new Queue<Noeud>();
            bool[] visitees = new bool[n];
            visitees[noeud.num] = true;

            Noeud[] parent = new Noeud[n];

            parent[noeud.num] = noeud;

            fileNoeuds.Enqueue(noeud);
            while (fileNoeuds.Count()>0)
            {
                Noeud courant = fileNoeuds.Dequeue();
                visitees[courant.num] = true;
                for(int i = 0; i < n; i++)
                {
                    if (matriceAdj[courant.num][i] > 0 && visitees[i] == false)
                    {
                        fileNoeuds.Enqueue(sommets[i]);
                        visitees[i] = true;

                        //parent de i et le noeud courant
                        parent[sommets[i].num] = courant;
                    }
                    else if (matriceAdj[courant.num][i]>0 && visitees[i]==true && parent[courant.num] != sommets[i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void InitialiserCoordonnees(int maxX, int maxY)
        {
            Random random = new Random();
            foreach(Noeud n in sommets)
            {
                n.AjouterCoords(random.Next(4000, maxX), random.Next(4000, maxY));
            }
        }

        public void InitialiserValeursConnues()
        {
            List<int> l1 = new List<int>
            {
            4447, 5218, 5527, 5156, 5403, 4507, 5317, 4679, 4057, 4619, 4704, 5175,
            4542, 5794, 5142, 4747, 4334, 5987, 5561, 4994, 5235, 4843, 4205, 4340,
            4383, 5906, 5118, 5868, 5352, 5441, 5229, 4391, 4978, 4123
            };

            // List l2 with given values
            List<int> l2 = new List<int>
            {
            4118, 4234, 4175, 4738, 4673, 5816, 4515, 5378, 4758, 4634, 5182, 5333,
            4683, 4921, 5460, 5745, 4668, 4567, 4408, 4424, 4342, 4149, 5691, 5591,
            4469, 4729, 4662, 5379, 4460, 4235, 5231, 5792, 5053, 4484
            };

            foreach (Noeud n in sommets)
            {
                n.AjouterCoords(l1[n.num-1], l2[n.num-1]);
            }
        }

        public void CoordsParForceDirigee(int E, int K, int l, double D)
        {
            int t = 1;
            Dictionary<Noeud, int[]> Forces = new Dictionary<Noeud, int[]>();
            int maxforce = E + 1;

            while (t < K && maxforce > E)
            {
                maxforce = 1;
                foreach(Noeud n in sommets) 
                {
                    int[] SfRep = sumForceRepulsives(n, l);
                    int[] SfAttr = sumForcesAttractives(n, l);
                    Forces[n] = new int[] { SfRep[0]+SfAttr[0], SfRep[1]+SfAttr[1] };
                    int Fx = Forces[n][0];
                    int Fy = Forces[n][1];
                    int F = (int)Math.Sqrt(Fx * Fx + Fy * Fy);
                    if ( F > maxforce) { maxforce = F; }
                }
                foreach(Noeud n in sommets)
                {
                    D = D * D;
                    n.AjouterCoords(n.x + (int)((double)Forces[n][0]*D)/maxforce, n.y + (int)((double)Forces[n][1]*D)/maxforce);
                }
                t++;
            }
        }

        public int[] sumForceRepulsives(Noeud u, int l)
        {
            double forceX = 0;
            double forceY = 0;
            foreach (Noeud v in sommets)
            {
                if (v != u && !ListeAdj[u].Contains(v))
                {
                    //Coords de pv - pu
                    int pv_puX = (u.x - v.x);
                    int pv_puY = (u.y - v.y);
                    //Distance 
                    double dist = Math.Sqrt(pv_puX * pv_puX + pv_puY * pv_puY);

                    forceX += (l * l / dist) * pv_puX;
                    forceY += (l * l / dist) * pv_puY;
                }
            }
            return new int[] { (int)forceX, (int)forceY };
        }

        public int[] sumForcesAttractives(Noeud u, int l)
        {
            double forceX = 0;
            double forceY = 0;
            foreach (Noeud v in ListeAdj[u])
            {
                if (v != u)
                {
                    //Coords de pv - pu
                    int pv_puX = (v.x - u.x);
                    int pv_puY = (v.y - u.y);
                    //Distance 
                    double dist = Math.Sqrt(pv_puX * pv_puX + pv_puY * pv_puY);

                    forceX += (dist*dist / l) * pv_puX;
                    forceY += (dist*dist / l) * pv_puY; 
                }
            }
            return new int[] { (int)forceX, (int)forceY };
        }
    }
}
