namespace a041.Model;

public class Disciplina
{
    internal string Codice {get; set;}
    private string Name {get; set;}
   
    private int Classe {get; set;}
    internal int Ore {get; set;}
    

    public Disciplina(string s, int parse, int i)
    {
        this.Codice = s;
        this.Classe = parse;
        this.Ore = i;
    }

    public string trovaDisciplina(string codice)
    {
        this.Codice = codice;

        switch (codice)
        {
            case "TI":
                return "Tecnologie Informatiche";
            case "STA":
                return "Scienze e Teconlogie Applicate";
            case "INF":
                return "Informatica";
            case "SR":
                return  "Sistemi e Reti";
            case "TPS":
                return  "Tecnologie e Progettazione di Sistemi Informatici e di Telecomunicazioni";
            case "GPOI":
                return "Gestione di Progetto e Organizzazione di Impresa";
        }

        return null;
    }
    public override string ToString()
    {
        return $"{Codice} {Classe} {Ore} {trovaDisciplina(Codice)}";
    }
}