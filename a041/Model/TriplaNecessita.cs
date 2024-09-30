using System;
using System.Collections.Generic;
using System.Text;

namespace a041.Model
{
    public class TriplaNecessita
    {
        private GestioneOrario gestioneOrario;
        private Dictionary<string, string> assegnazioneProfessori; // Mappa materia -> professore

        public TriplaNecessita(GestioneOrario gestioneOrario)
        {
            this.gestioneOrario = gestioneOrario;
            assegnazioneProfessori = new Dictionary<string, string>();
        }
        public string StampaNecessita()
        {
            StringBuilder result = new StringBuilder();
            foreach (var classe in gestioneOrario.Classi)
            {
                result.AppendLine($"Classe {classe.ToString()}: ");
                
                var orePerDisciplina = classe.GetOrePerDisciplina();
                foreach (var disciplina in orePerDisciplina)
                {
                    result.AppendLine($"{disciplina.Key} {disciplina.Value} ore");
                }

                result.AppendLine();
            }
            return result.ToString();
        }

        // Metodo per avviare l'assegnazione dei professori
        public string AssegnaProf(int index)
        {
            if (index >= gestioneOrario.Classi.Count)
            {
                return "Assegnazione completata!";
            }

            Classe currentClass = gestioneOrario.Classi[index];
            StringBuilder assegnazioni = new StringBuilder();

            assegnazioni.AppendLine($"Classe {currentClass.Nome}:");

            // Prova ad assegnare professori alle discipline per la classe corrente
            if (AssegnaProfessoriPerClasse(currentClass))
            {
                foreach (var disciplina in currentClass.GetOrePerDisciplina().Keys)
                {
                    if (assegnazioneProfessori.ContainsKey(disciplina))
                    {
                        string prof = assegnazioneProfessori[disciplina];
                        assegnazioni.AppendLine($"{disciplina}: {prof}");
                    }
                }

                // Ricorsione per assegnare professori alla classe successiva
                assegnazioni.Append(AssegnaProf(index + 1));
            }
            else
            {
                assegnazioni.AppendLine($"Assegnazione fallita per la classe {currentClass.Nome}");
            }

            return assegnazioni.ToString();
        }

        // Metodo per assegnare professori ad una singola classe
        private bool AssegnaProfessoriPerClasse(Classe currentClass)
        {
            // Reset delle assegnazioni per ogni nuova classe
            assegnazioneProfessori.Clear();
            HashSet<string> professoriUsati = new HashSet<string>();

            var discipline = new List<string>(currentClass.GetOrePerDisciplina().Keys);

            foreach (var disciplina in discipline)
            {
                bool assegnato = false;

                foreach (var docente in gestioneOrario.Docenti)
                {
                    // Verifica se il professore non è già stato assegnato alla materia o alla classe corrente
                    if (!professoriUsati.Contains(docente.Nome) && !assegnazioneProfessori.ContainsKey(disciplina))
                    {
                        // Assegna il professore alla materia
                        assegnazioneProfessori[disciplina] = docente.Nome;
                        professoriUsati.Add(docente.Nome);
                        assegnato = true;
                        break;
                    }
                }

                // Se non riusciamo ad assegnare un professore alla materia corrente, fallisce
                if (!assegnato)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
