using System.Collections.Generic;
using System;
using Newtonsoft.Json;

[Serializable]
public class RealmState
{
    public String gameId;

    public Dictionary<String, PlayerState> players = new Dictionary<string, PlayerState>();

    public void UpdateWith(RealmState other) {
        foreach (KeyValuePair<String, PlayerState> element in other.players) {
            PlayerState currentPlayerState;
            if (this.players.TryGetValue(element.Key, out currentPlayerState)) {
                currentPlayerState.UpdateWith(element.Value);
            } else {
                this.players.Add(element.Key, element.Value);
            }
        }
    }
}
