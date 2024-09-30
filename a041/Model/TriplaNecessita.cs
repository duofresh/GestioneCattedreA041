using System;
using System.Collections.Generic;
using System.Text;

namespace a041.Model
{
    public class TriplaNecessita
    {
        private GestioneOrario gestioneOrario;
        private Dictionary<string, string> assegnazioneProfessori; // Mappa materia -> professore
        private Dictionary<string, int> oreProfessoriDisponibili;  // Mappa professore -> ore disponibili

        public TriplaNecessita(GestioneOrario gestioneOrario)
        {
            this.gestioneOrario = gestioneOrario;
            assegnazioneProfessori = new Dictionary<string, string>();
            oreProfessoriDisponibili = new Dictionary<string, int>();

            // Inizializza le ore disponibili per ogni professore
            foreach (var docente in gestioneOrario.Docenti)
            {
                oreProfessoriDisponibili[docente.Nome] = docente.Ore;
            }
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
                foreach (var disciplina in currentClass.GetOrePerDisciplina())
                {
                    string materia = disciplina.Key;
                    if (assegnazioneProfessori.ContainsKey(materia))
                    {
                        string prof = assegnazioneProfessori[materia];
                        assegnazioni.AppendLine($"{materia}: {prof} - {disciplina.Value} ore");
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

            var discipline = currentClass.GetOrePerDisciplina();

            foreach (var materia in discipline.Keys)
            {
                bool assegnato = false;
                int oreMateria = discipline[materia]; // Ore necessarie per la materia

                foreach (var docente in gestioneOrario.Docenti)
                {
                    // Verifica se il professore ha abbastanza ore disponibili e non è già stato assegnato alla materia
                    if (oreProfessoriDisponibili[docente.Nome] >= oreMateria && !assegnazioneProfessori.ContainsKey(materia))
                    {
                        // Assegna il professore alla materia
                        assegnazioneProfessori[materia] = docente.Nome;
                        oreProfessoriDisponibili[docente.Nome] -= oreMateria;  // Scala le ore del professore
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
