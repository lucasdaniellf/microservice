namespace DataRepository.Inputs
{
    public record AddGeneroInput(string nome);
    public record UpdateGeneroInput(int id, string nome);
    public record DeleteGeneroInput(int id);
}
