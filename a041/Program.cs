using System;
using a041.Model;

namespace a041
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            
            string pathDiscipline = Path.Combine(projectRoot, "res", "discipline.txt");
            string pathDocenti = Path.Combine(projectRoot, "res", "docenti.txt");
            string pathClassi = Path.Combine(projectRoot, "res", "classi.txt");
            
            if (!File.Exists(pathDiscipline) || !File.Exists(pathDocenti) || !File.Exists(pathClassi))
            {
                Console.WriteLine("Uno o più file mancano nella cartella 'res'.");
                return;
            }
            
            GestioneOrario gestioneOrario = new GestioneOrario(pathDiscipline, pathDocenti, pathClassi);
            TriplaNecessita triplaNecessita = new TriplaNecessita(gestioneOrario);
            

            Console.WriteLine("Discipline:");
            Console.WriteLine(gestioneOrario.stampaDiscipline()+"\n");


            Console.WriteLine("Docenti:");
            Console.WriteLine(gestioneOrario.stampaDocenti()+"\n");


            Console.WriteLine("Classi:");
            Console.WriteLine(gestioneOrario.stampaClassi()+"\n");

            Console.WriteLine("Tripla Necessità:");
            Console.WriteLine(triplaNecessita.StampaNecessita()+"\n");
            
            Console.WriteLine("Assegnazione Docenti:");
            Console.WriteLine(triplaNecessita.AssegnaProf(0)+"\n");

            Console.WriteLine("Premi un tasto...");
            Console.ReadKey();
        }
    }
}