using UnityEngine;
using UnityEngine.EventSystems;

public interface IGlobalMessageTarget : IEventSystemHandler
{
    void Move(Vector3 destination);

    void MoveRandom();
}
