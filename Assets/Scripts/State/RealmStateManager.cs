using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealmStateManager : MonoBehaviour
{
    private RealmState realmState = new RealmState();

    private static RealmStateManager stateManager;

    public static RealmStateManager instance {
        get {
            if (!stateManager) {
                stateManager = FindObjectOfType(typeof(RealmStateManager)) as RealmStateManager;

                if (!stateManager) {
                    Debug.LogError("There needs to be one active RealmStateManager script on a GameObject in your scene.");
                } else {
                    stateManager.Init();
                }
            }

            return stateManager;
        }
    }

    public void Init() {

    }

    public static void UpdateState(RealmState updatedState) {
        instance.realmState.UpdateWith(updatedState);
    }

    public static IReadRealmState GetRealmState() {
        return instance.realmState;
    }
}
