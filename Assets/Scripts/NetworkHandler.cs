using UnityEngine;
using System.Threading;

public class NetworkHandler : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        await NetworkConnection.Connect();

        while (NetworkConnection.IsOpen()) {
            string message = await NetworkConnection.Receive();
            //EventEmitter.MoveRandom();
            Debug.Log(message);
        }
    }

    // Update is called once per frame
    void OnDestroy()
    {
        NetworkConnection.Close();
    }
}
