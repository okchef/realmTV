using UnityEngine;
using System;
using Newtonsoft.Json;

public class NetworkMessageFactory
{
    public static NetworkMessage<RealmEventBase> GetNetworkMessage(string json) {
        return GetNetworkMessage<RealmEventBase>(json);
    }

    public static NetworkMessage<T> GetNetworkMessage<T>(string json) where T : RealmEventBase {
        return JsonConvert.DeserializeObject<NetworkMessage<T>>(json);
    }

    public static object GetNetworkMessage(string json, Type networkMessageType) {
        return JsonConvert.DeserializeObject(json, networkMessageType);
    }

}
