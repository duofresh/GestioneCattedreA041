using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace a041.Model
{
    public class TriplaNecessita
    {
        private GestioneOrario gestioneOrario;
        private Dictionary<string, string> assegnazioneProfessori; // Mappa materia -> professore
        private Dictionary<string, int> oreProfessoriDisponibili;  // Mappa professore -> ore disponibili
        private HashSet<string> classiAssegnate; // Traccia delle classi già assegnate

        public TriplaNecessita(GestioneOrario gestioneOrario)
        {
            this.gestioneOrario = gestioneOrario;
            assegnazioneProfessori = new Dictionary<string, string>();
            oreProfessoriDisponibili = new Dictionary<string, int>();
            classiAssegnate = new HashSet<string>();

            
            foreach (var docente in gestioneOrario.Docenti)     //ore disponibili per ogni professore
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

        // metodo per avviare l'assegnazione dei professori
        public string AssegnaProf(int index)
        {
            if (index >= gestioneOrario.Classi.Count)
            {
                return "Assegnazione completata!";
            }

            Classe currentClass = gestioneOrario.Classi[index];
            StringBuilder assegnazioni = new StringBuilder();

            assegnazioni.AppendLine($"Classe {currentClass.Nome}:");
            
            if (AssegnaProfessoriPerClasse(currentClass))   // prova ad assegnare professori alle discipline per la classe corrente
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

                // ricorsione per assegnare professori alla classe successiva
                assegnazioni.Append(AssegnaProf(index + 1));
            }
            else
            {
                assegnazioni.AppendLine($"Assegnazione fallita per la classe {currentClass.Nome}");
            }

            return assegnazioni.ToString();
        }

        // metodo per assegnare professori ad una singola classe
        private bool AssegnaProfessoriPerClasse(Classe currentClass)
        {
            // reset delle assegnazioni per ogni nuova classe
            assegnazioneProfessori.Clear();

            var discipline = currentClass.GetOrePerDisciplina();

            foreach (var materia in discipline.Keys)
            {
                bool assegnato = false;
                int oreMateria = discipline[materia]; // ore necessarie per la materia
        
                var professoriOrdinati = gestioneOrario.Docenti
                    //.Where(docente => docente.Nome != "Supplente1" && docente.Nome != "Supplente2")  // Esclude i supplenti
                    .OrderByDescending(docente => oreProfessoriDisponibili[docente.Nome])   // ordina i professori in base alle ore disponibili, per distribuire equamente
                    .ToList();

                foreach (var docente in professoriOrdinati)
                {
                    // verifica se il professore ha abbastanza ore disponibili e non è già stato assegnato alla materia
                    if (oreProfessoriDisponibili[docente.Nome] >= oreMateria && !assegnazioneProfessori.ContainsKey(materia))
                    {
                        // assegna
                        assegnazioneProfessori[materia] = docente.Nome;
                        oreProfessoriDisponibili[docente.Nome] -= oreMateria;  // Scala le ore del professore
                        assegnato = true;
                        break;
                    }
                }

                // fine loop se fallisce
                if (!assegnato)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
