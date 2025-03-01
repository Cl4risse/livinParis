using System.Security;
using SkiaSharp;

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

            //L'idée est d'utiliser un tableau pour garder trace du parent de chaque sommet.
            //Lorsqu'on explore les sommets, on vérifie si on revient à un sommet déjà visité, et si ce
            //sommet n'est pas le parent du sommet actuel, si c'est le cas, on a trouvé un cycle.
            //Puisqu'on explore tout les sommets (graphe connexe) on peut prendre un sommet arbitraire, ici on prend 1.
            Noeud noeud = amisKarate.sommets[1];
            Console.WriteLine(amisKarate.contientCycle(matriceAdj, noeud));


            //AFFICHAGE (Fonctionnement expliqué dans le rapport)

            //Generation des coords des sommets
            int width = 10000;
            int height = 10000;

            int l = 3000;
            int maxIter = 1000;
            int threshold = 10;
            double delta = 0.99;
            //amisKarate.InitialiserCoordonnees(width-4000, height-4000);

            amisKarate.InitialiserValeursConnues(); //coordonnées initialement randomisés mais utilisés pour toutes les experiences pour avoir un input qui est la meme a chaque iteration
            amisKarate.CoordsParForceDirigee(threshold, maxIter, l, delta);

            // Step 1: Create a bitmap (image) with a given width and height

            using (var bitmap = new SKBitmap(width, height))
            {
                // Step 2: Create a canvas to draw on the bitmap
                using (var canvas = new SKCanvas(bitmap))
                {
                    canvas.Clear(SKColors.Black);  // Set background to white

                    // Step 3: Set up SKPaint for drawing shapes
                    using (var paint = new SKPaint())
                    {
                        paint.Color = SKColors.White;
                        paint.StrokeWidth = 7;
                        foreach (Lien arete in amisKarate.Liens)
                        {
                            //Console.WriteLine(arete.n1.x + ' ' + arete.n1.y + ' ' + arete.n2.x + ' ' + arete.n2.y);
                            canvas.DrawLine(arete.n1.x, arete.n1.y, arete.n2.x, arete.n2.y, paint);
                        }
                        foreach (Noeud n in amisKarate.sommets)
                        {
                            paint.Color = SKColors.Red;
                            canvas.DrawCircle(n.x, n.y, 25, paint);
                        }

                        // Step 4: Draw a line (example: from (100, 100) to (300, 300))

                        // Step 5: Draw a circle (example: center (200, 200), radius 50)
                    }
                }

                // Step 6: Save the bitmap to a file
                Console.WriteLine(amisKarate.sommets.Count());
                Console.Write("Nommez le fichier : ");
                string nom = Console.ReadLine() + ".png";
                using (var stream = new FileStream(nom, FileMode.Create))
                {
                    bitmap.Encode(SKEncodedImageFormat.Png, 100).SaveTo(stream);
                }
            }
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
