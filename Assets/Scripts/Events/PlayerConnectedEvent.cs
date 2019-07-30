[System.Serializable]
[RealmEventData(RealmEventType.PLAYER_CONNECTED)]
public class PlayerConnectedEvent : RealmEventBase
{
    public string playerSessionId;
    public string gameSessionId;
}
