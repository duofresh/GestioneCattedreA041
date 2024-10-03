using System.Text;

namespace a041.Model
{
    public class GestioneOrario
    {
        internal List<Disciplina> Discipline { get; set; }
        internal List<Docente> Docenti { get; set; }
        internal List<Classe> Classi { get; set; } 

        public GestioneOrario(string pathdiscipline, string pathdocenti, string pathclassi)
        {
            Discipline = new List<Disciplina>();
            Docenti = new List<Docente>();
            Classi = new List<Classe>(); 
            leggiDiscipline(pathdiscipline);
            leggiDocenti(pathdocenti);
            leggiClassi(pathclassi, pathdiscipline); 
        }

        public List<Docente> getDocenti()
        {
            return Docenti;
        }

        public void leggiDiscipline(string path)
        {
            StreamReader sr = new StreamReader(path);
            string line;
            string[] info;
            line = sr.ReadLine();
            while (line != null)
            {
                info = line.Split(";");
                Discipline.Add(new Disciplina(info[0], int.Parse(info[1]), int.Parse(info[2])));
                line = sr.ReadLine();
            }
        }

        public void leggiDocenti(string path)
        {
            StreamReader sr = new StreamReader(path);
            string line;
            string[] info;
            line = sr.ReadLine();
            while (line != null)
            {
                info = line.Split(";");
                Docenti.Add(new Docente (info[0],int.Parse(info[1])));
                line = sr.ReadLine();
            }
        }

        internal void leggiClassi(string path, string pathdiscipline)
        {
            StreamReader sr = new StreamReader(path);
            string line;
            string[] info;
    
            while ((line = sr.ReadLine()) != null)
            {
                info = line.Split(";");
                for (int i = 0; i < info.Length; i++)
                {
                    if (info[i] != ";")
                    {
                        string nomeClasse = info[i];
                        Classe classe = new Classe(nomeClasse);
                        classe.calcolaOre(pathdiscipline);
                        Classi.Add(classe); 
                    }
                }
            }
        }


        public string stampaDiscipline()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Disciplina d in Discipline)
            {
                sb.AppendLine(d.ToString());
            }
            return sb.ToString();
        }

        public string stampaDocenti()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Docente d in Docenti)
            {
                sb.AppendLine(d.ToString());
            }
            return sb.ToString();
        }

        public string stampaClassi()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Classe c in Classi)
            {
                sb.AppendLine(c.ToString());
            }
            return sb.ToString();
        }
    }
}
