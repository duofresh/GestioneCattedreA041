using System.Text;

namespace a041.Model
{
    public class TriplaNecessita
    {
        private GestioneOrario gestioneOrario;

        public TriplaNecessita(GestioneOrario gestioneOrario)
        {
            this.gestioneOrario = gestioneOrario;
        }

        public string stampaNecessita()
        {
            StringBuilder result = new StringBuilder();

            // Loop through each class
            foreach (var classe in gestioneOrario.Classi)
            {
                result.AppendLine($"Classe: {classe.ToString()}");

                // Loop through each subject (Disciplina)
                foreach (var disciplina in gestioneOrario.Discipline)
                {
                    // Here you would need to establish the connection between classes and subjects
                    // For now, I am assuming that the subject is part of the class name (as inferred from previous logic)
                    if (classe.ToString().Contains(disciplina.trovaDisciplina(disciplina.Codice))) // Example logic
                    {
                        result.AppendLine($"  Disciplina: {disciplina.trovaDisciplina(disciplina.Codice)} - Ore: {classe.GetOreTotali()}");
                    }
                }
            }

            return result.ToString();
        }
    }
}