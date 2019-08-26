public interface IReadRealmState
{
    IReadPlayerState GetPlayerState(string playerId);

    IReadMapState GetMapState();
}
