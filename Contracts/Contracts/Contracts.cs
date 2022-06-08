namespace Contracts
{
    public record GameCreated (int gameId, string name);
    public record GameUpdated (int gameId, string name);
    public record GameDeleted (int gameId);
}