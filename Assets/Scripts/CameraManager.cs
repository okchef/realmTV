using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null) {
            Vector3 targetPosition = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, 0.1f);
        }
    }
}
