using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class RealmEventRegistry : MonoBehaviour
{
    private Dictionary<string, Type> eventDataTypes;

    private static RealmEventRegistry realmEventRegistry;

    public static RealmEventRegistry instance {
        get {
            if (!realmEventRegistry) {
                realmEventRegistry = FindObjectOfType(typeof(RealmEventRegistry)) as RealmEventRegistry;

                if (!realmEventRegistry) {
                    Debug.LogError("There needs to be one active RealmEventDataRegistry script on a GameObject in your scene.");
                }
            }

            return realmEventRegistry;
        }
    }

    public void Awake() {
        if (eventDataTypes == null) {
            eventDataTypes = new Dictionary<string, Type>();

            IEnumerable<Type> types = typeof(RealmEventRegistry).Assembly.GetTypes().Where(type => type.IsDefined(typeof(RealmEventDataAttribute), false));
            foreach (Type type in types) {
                if (!typeof(RealmEventBase).IsAssignableFrom(type)) {
                    Debug.LogError("Class tagged with RealmEventDataAttribute does not inherit from " + typeof(RealmEventBase));
                }
                var attributes = type.GetCustomAttributes(typeof(RealmEventDataAttribute), false);
                if (attributes.Length > 0) {
                    string eventType = (attributes[0] as RealmEventDataAttribute).eventType.ToString();
                    eventDataTypes.Add(eventType, type);
                }
            }

        }
    }

    public static T ParseEvent<T>(string json) where T : RealmEventBase {
        return JsonUtility.FromJson<T>(json);
    }

    public static RealmEventBase ParseEvent(string json, Type type) {
        if (!typeof(RealmEventBase).IsAssignableFrom(type)) {
            Debug.LogError("Tried to parse eventData using invalid type: " + type);
        }
        return JsonUtility.FromJson(json, type) as RealmEventBase;
    }

    public static Type GetEventDataType(string eventType) {
        Type type;
        if (instance.eventDataTypes.TryGetValue(eventType, out type)) {
            if (!typeof(RealmEventBase).IsAssignableFrom(type)) {
                Debug.LogError("Invalid eventDataType " + type + " for eventType: " + eventType);
            }
            return type;
        } else {
            Debug.LogError("No registered RealmEventDataType found for eventType: " + eventType);
            return null;
        }
    }
}
