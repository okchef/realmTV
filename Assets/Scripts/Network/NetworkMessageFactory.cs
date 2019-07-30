using UnityEngine;
using System;

public class NetworkMessageFactory
{
    public static NetworkMessage<RealmEventBase> GetNetworkMessage(string json) {
        return GetNetworkMessage<RealmEventBase>(json);
    }

    public static NetworkMessage<T> GetNetworkMessage<T>(string json) where T : RealmEventBase {
        return JsonUtility.FromJson<NetworkMessage<T>>(json);
    }

    public static object GetNetworkMessage(string json, Type networkMessageType) {
        return JsonUtility.FromJson(json, networkMessageType);
    }

}
