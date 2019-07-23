using UnityEngine;
using UnityEngine.EventSystems;

public class EventEmitter
{
    public static void Move(Vector3 destination) {
        ExecuteEvents.Execute<IGlobalMessageTarget>(GlobalMessageHandler.GameObject, null, (target, eventData) => target.Move(destination));
    }

    public static void MoveRandom() {
        ExecuteEvents.Execute<IGlobalMessageTarget>(GlobalMessageHandler.GameObject, null, (target, eventData) => target.MoveRandom());
    }
}
