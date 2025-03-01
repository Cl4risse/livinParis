using System.Security;

namespace Associations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graphe amisKarate = new Graphe("soc-karate.mtx");
            
            //LISTES D'ADJACENCE 
            Dictionary<Noeud, List<Noeud>> listeAdj = amisKarate.GenererListeAdjacence();

            //Affichage pour chaque sommet de sa liste d'adjacence
            for (int i = 0; i < amisKarate.sommets.Length; i++) 
            {
                Noeud sommetActuel = amisKarate.sommets[i];
                sommetActuel.AfficheN();
                Console.Write(" : | ");
                foreach(Noeud adj in listeAdj[sommetActuel])
                {
                    adj.AfficheN();
                    Console.Write(" | ");
                }
                Console.WriteLine("");
            }

            //MATRICE D'ADJACENCE
            //(la matrice d'adjacence suivra l'ordre des noeuds dans les 'sommets' dans 'Graphe') 
            //L'affichage est commenté car il prends beaucoup de place, décommentez que si besoin

            int[][] matriceAdj = amisKarate.GenererMatriceAdjacence();
            //afficherMatrice(matriceAdj);


            //PARCOURS BFS et DFS
            //L'affichage est commenté car il prends beaucoup de place, décommentez que si besoin
            foreach (Noeud n in amisKarate.sommets)
            {
                //afficherListeNoeuds(amisKarate.ParcoursEnLargeur(n));
                //afficherListeNoeuds(amisKarate.ParcoursEnProfondeur(n));
            }

            //VERIFICATION DE SI LE GRAPHE EST CONNEXE :
            //Vu que le graph est non orienté un parcours quelconque (ici largeur) 
            //atteindras tout les sommets du graphe a partir de tout sommet.

            string EstConnexe = "est connexe";
            foreach (Noeud n in amisKarate.sommets)
            {
                if(amisKarate.ParcoursEnLargeur(n).Count() != amisKarate.Sommets.Length)
                {
                    EstConnexe = "n'est pas connexe";
                }
            }
            
            Console.WriteLine("Le Graphe " + EstConnexe);
        
            
            //EXISTENT-T-ILS DES CIRCUITS?
            
        }

        public static void afficherListeNoeuds(List<Noeud> l1)
        {
            Console.Write("| ");
            foreach(Noeud i in l1)
            {
                i.AfficheN();
                Console.Write(" |");
            }
            Console.WriteLine("");
        }

        public static void afficherMatrice(int[][] mat1)
        {
            foreach (int[] ligne in mat1)
            {
                string ligneStr = "| ";
                foreach (int ind in ligne)
                {
                    ligneStr += ind + " | ";
                }
                Console.WriteLine(ligneStr);
            }
        }
    }
}
