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
        public string AssegnaProf(int index)
        {
            if (index >= gestioneOrario.Classi.Count)
            {
                return "Assegnazione completata!";
            }

            Classe currentClass = gestioneOrario.Classi[index];
            StringBuilder assegnazioni = new StringBuilder();
            
            // Prova ad assegnare professori alle discipline per la classe corrente
            if (AssegnaProfessoriPerClasse(currentClass, 0))
            {
                assegnazioni.AppendLine($"Classe {currentClass.Nome}:");

                foreach (var disciplina in currentClass.GetOrePerDisciplina().Keys)
                {
                    string prof = assegnazioneProfessori[disciplina];
                    assegnazioni.AppendLine($"{disciplina}: {prof}");
                }

                // Ricorsione: Passa alla classe successiva
                assegnazioni.Append(AssegnaProf(index + 1));
            }
            else
            {
                assegnazioni.AppendLine($"Assegnazione fallita per la classe {currentClass.Nome}");
            }

            return assegnazioni.ToString();
        }

        private bool AssegnaProfessoriPerClasse(Classe currentClass, int disciplinaIndex)
        {
            var discipline = new List<string>(currentClass.GetOrePerDisciplina().Keys);

            if (disciplinaIndex >= discipline.Count)
            {
                return true; // Tutte le materie sono state assegnate
            }

            string currentDisciplina = discipline[disciplinaIndex];

            foreach (var docente in gestioneOrario.Docenti)
            {
                // Verifica se il docente non è già assegnato a questa materia
                if (!assegnazioneProfessori.ContainsKey(currentDisciplina) &&
                    !assegnazioneProfessori.ContainsValue(docente.Nome))
                {
                    // Assegna il docente alla materia
                    assegnazioneProfessori[currentDisciplina] = docente.Nome;

                    // Ricorsione per la materia successiva
                    if (AssegnaProfessoriPerClasse(currentClass, disciplinaIndex + 1))
                    {
                        return true;
                    }

                    // Se non ha funzionato, rimuovi l'assegnazione e riprova con un altro professore
                    assegnazioneProfessori.Remove(currentDisciplina);
                }
            }

            // Se nessun professore è stato trovato per questa materia, torna false
            return false;
        }
    }
}