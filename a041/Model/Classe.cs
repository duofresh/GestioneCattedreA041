using System;
using System.Collections.Generic;
using System.IO;

namespace a041.Model
{
    public class Classe
    {
        private StreamReader scanner;
        private Dictionary<string, int> orePerDisciplina;
        public string Nome { get; private set; }

        public Classe(string nome)
        {
            Nome = nome;
            orePerDisciplina = new Dictionary<string, int>();
        }

        public override string ToString()
        {
            return Nome;
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
                        string disciplina = materie[0];
                        int ore = int.Parse(materie[2]);
                        
                        if (orePerDisciplina.ContainsKey(disciplina))
                        {
                            orePerDisciplina[disciplina] += ore;
                        }
                        else
                        {
                            orePerDisciplina[disciplina] = ore;
                        }
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
                scanner?.Close();
            }
        }
        
        public Dictionary<string, int> GetOrePerDisciplina()
        {
            return orePerDisciplina;
        }
    }
}
