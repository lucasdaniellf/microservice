namespace DataRepository.Inputs
{
    public record AddEstudioInput(string nome);
    public record UpdateEstudioInput(int id, string nome);
    public record DeleteEstudioInput(int id);
}
