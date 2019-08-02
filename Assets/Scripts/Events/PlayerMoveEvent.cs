[System.Serializable]
[RealmEventData(RealmEventType.PLAYER_MOVE)]
public class PlayerMoveEvent : RealmEventBase
{
    public string direction;
}
