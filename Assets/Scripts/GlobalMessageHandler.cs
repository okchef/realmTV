using UnityEngine;

public class GlobalMessageHandler : MonoBehaviour, IGlobalMessageTarget
{
    public GameObject PlayerAvatar;

    private static GlobalMessageHandler _instance;

    public static GlobalMessageHandler Instance { get { return _instance; } }

    public static GameObject GameObject { get { return _instance.gameObject; } }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    void IGlobalMessageTarget.Move(Vector3 destination) {
        PlayerAvatar.transform.position = destination;
    }

    void IGlobalMessageTarget.MoveRandom() {
        PlayerAvatar.GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
