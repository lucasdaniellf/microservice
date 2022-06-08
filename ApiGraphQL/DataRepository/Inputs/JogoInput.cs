using Models;

namespace DataRepository.Inputs
{
    public record AddJogoInput(string Nome, string Descricao, Rating ClassificacaoESBR, int? IdEstudio);
    public record UpdateJogoInput(int Id, string? Nome, string? Descricao, Rating? ClassificacaoESBR);
    public record UpdateJogoIdEstudioInput(int? IdEstudio);
    public record UpdateJogoGeneroInput(int JogoId, int[] GeneroIds);
    public record DeleteJogoInput(int Id);
}
