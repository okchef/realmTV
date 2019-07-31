using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

public class EventManager : MonoBehaviour
{
    //private delegate void RealmAction<T>(T eventData) where T : RealmEvent;

    private class RealmEventTopic : UnityEvent<RealmEventBase> {}

    private IDictionary<string, RealmEventTopic> realmEventDictionary;

    private static EventManager eventManager;

    public static EventManager instance {
        get {
            if (!eventManager) {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager) {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                } else {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    private void Init() {
        if (realmEventDictionary == null) {
            realmEventDictionary = new Dictionary<string, RealmEventTopic>();
        }
    }

    public static void StartListening<T>(UnityAction<RealmEventBase> listener) where T : RealmEventBase {
        StartListening(GetEventType<T>(), listener);
    }

    /*public static void StartListening<T>(UnityAction<T> listener) where T : RealmEvent {
        UnityAction<RealmEvent> callback = (RealmEvent eventData) => {
            listener.Invoke((T)Convert.ChangeType(eventData, typeof(T)));
        };

        StartListening(GetEventType<T>(), callback);
    }*/

    public static void StartListening(string eventName, UnityAction<RealmEventBase> listener) {
        RealmEventTopic thisEvent = null;
        if (instance.realmEventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        } else {
            RealmEventTopic myEvent = new RealmEventTopic();
            myEvent.AddListener(listener);
            instance.realmEventDictionary.Add(eventName, myEvent);
        }
    }

    public static void StopListening<T>(UnityAction<RealmEventBase> listener) where T : RealmEventBase {
        StopListening(GetEventType<T>(), listener);
    }

    public static void StopListening(string eventName, UnityAction<RealmEventBase> listener) {
        if (eventManager == null) return;
        RealmEventTopic thisEvent = null;
        if (instance.realmEventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        } else {
            Debug.Log("Tried to remove non-existent listener on event " + eventName);
        }
    }

    public static void TriggerEvent<T>(T eventData) where T : RealmEventBase {
        TriggerEvent(GetEventType<T>(), eventData);
    }

    public static void TriggerEvent(RealmEventBase eventData) {
        TriggerEvent(GetEventType(eventData), eventData);
    }

    public static void TriggerEvent(string eventType, RealmEventBase eventData) {
        RealmEventTopic thisEvent = null;
        if (instance.realmEventDictionary.TryGetValue(eventType, out thisEvent)) {
            thisEvent.Invoke(eventData);
        }
    }

    /*private static async void InvokeAsync(RealmEventTopic eventTopic, RealmEventBase eventData) {
        await Task.Run(() => {
            System.Threading.Thread.Sleep(5000);
            eventTopic.Invoke(eventData);
        });
    }*/

    public static void TriggerEvent(string eventType, string eventJson) {
        Type eventDataType = RealmEventRegistry.GetEventDataType(eventType);
        TriggerEvent(eventType, RealmEventRegistry.ParseEvent(eventJson, eventDataType));
    }

    private static string GetEventType<T>() where T : RealmEventBase {
        var attribute = typeof(T).GetCustomAttributes(typeof(RealmEventDataAttribute), false).FirstOrDefault() as RealmEventDataAttribute;
        if (attribute != null) {
            return attribute.eventType;
        } else {
            Debug.LogError("Missing attribute RealmEvent on class " + typeof(T));
            return null;
        }
    }

    private static string GetEventType(RealmEventBase realmEventData) {
        var attribute = realmEventData.GetType().GetCustomAttributes(typeof(RealmEventDataAttribute), false).FirstOrDefault() as RealmEventDataAttribute;
        if (attribute != null) {
            return attribute.eventType;
        } else {
            Debug.LogError("Missing attribute RealmEvent on class " + realmEventData.GetType());
            return null;
        }
    }
}