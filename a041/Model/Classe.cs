namespace a041.Model
{
    public class Classe
    {
        private StreamReader scanner;
        private int oreTotali;
        private string Nome { get; set; }

        public Classe(string nome)
        {
            Nome = nome;
            oreTotali = 0;
        }

        public override string ToString()
        {
            return $"{Nome} ha {oreTotali} Ore";
        }

        public void calcolaOre(string path)
        {
            if (string.IsNullOrEmpty(Nome))
            {
                throw new InvalidOperationException("Nome is not set.");
            }

            try
            {
                scanner = new StreamReader(path); 
                string file = scanner.ReadLine();
                string[] materie;

                while (file != null)
                {
                    materie = file.Split(";");
                    
                    if (materie.Length >= 3 && materie[1] == Nome[0].ToString())
                    {
                        oreTotali += int.Parse(materie[2]);
                    }

                    file = scanner.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante la lettura del file: {ex.Message}");
            }
            finally
            {
                scanner?.Close(); // chiudi lo streamreader .. rip memoria
            }
        }
    }
}