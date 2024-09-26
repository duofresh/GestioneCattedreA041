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

            foreach (var classe in gestioneOrario.Classi)
            {
                result.AppendLine($"Classe: {classe.ToString()}");
                foreach (var disciplina in gestioneOrario.Discipline)
                {
                    if (classe.ToString().Contains(disciplina.trovaDisciplina(disciplina.Codice)))
                    {
                        result.AppendLine($"  Disciplina: {disciplina.trovaDisciplina(disciplina.Codice)} - Ore: {classe.GetOreTotali()}");
                    }
                }
            }

            return result.ToString();
        }
    }
}