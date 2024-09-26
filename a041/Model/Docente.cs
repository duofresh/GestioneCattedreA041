namespace a041.Model;

public class Docente
{
    private string Nome {  get; set; }
    private int Ore { get; set; }

    public Docente()
    {
        
    }
    public Docente(string nome, int ore)
    {
        this.Nome = nome;
        this.Ore = ore;
    }

    public override string ToString()
    {
        return $"{Nome}, {Ore} Ore";
    }
}