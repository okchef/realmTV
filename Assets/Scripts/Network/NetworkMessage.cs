[System.Serializable]
public class NetworkMessage<T> where T : RealmEventBase
{
    public string tid;
    public string gameSessionId;
    public string playerSessionId;
    public T realmEvent;
    public string realmEventType;
    public RealmState realmStateFragment;
}
