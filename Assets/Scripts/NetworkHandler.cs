using UnityEngine;
using System;
using System.Reflection;
using UnityEngine.Events;

public class NetworkHandler : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        await NetworkConnection.Connect();

        while (NetworkConnection.IsOpen()) {
            string message = await NetworkConnection.Receive();

            if (!string.IsNullOrEmpty(message)) {
                try {
                    NetworkMessage<RealmEventBase> networkMessage = NetworkMessageFactory.GetNetworkMessage(message);
                    if (networkMessage != null && networkMessage.realmEvent != null && !string.IsNullOrEmpty(networkMessage.realmEventType)) {
                        // Re-parse with the appropriate event type. This is ugly.
                        Type eventType = RealmEventRegistry.GetEventDataType(networkMessage.realmEventType);
                        Type networkMessageType = typeof(NetworkMessage<>).MakeGenericType(eventType);

                        object genericMessage = NetworkMessageFactory.GetNetworkMessage(message, networkMessageType);
                        object realmEvent = networkMessageType.GetField("realmEvent").GetValue(genericMessage);

                        // TODO: Fire of this event and continue. Don't wait for a result.
                        EventManager.TriggerEvent(networkMessage.realmEventType, realmEvent as RealmEventBase);
                    }
                } catch(Exception e) {
                    Debug.Log("Exception in NetworkHandler: " + e.Message);
                    Debug.Log(e.StackTrace);
                }
                
            }
        }
    }

    // Update is called once per frame
    void OnDestroy()
    {
        NetworkConnection.Close();
    }
}
