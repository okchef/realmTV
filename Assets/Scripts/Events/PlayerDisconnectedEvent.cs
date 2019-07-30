[System.Serializable]
[RealmEventData(RealmEventType.PLAYER_DISCONNECTED)]
public class PlayerDisconnectedEvent : RealmEventBase
{
    public string playerSessionId;
    public string gameSessionId;
}
