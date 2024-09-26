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
    }
}