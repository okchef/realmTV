using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerId { get; set; }

    private Grid mapGrid;

    public void OnEnable() {
        mapGrid = Component.FindObjectOfType<Grid>();
        EventManager.StartListening<PlayerMoveEvent>(HandlePlayerMove);
    }

    public void OnDisable() {
        mapGrid = null;
        EventManager.StartListening<PlayerMoveEvent>(HandlePlayerMove);
    }

    public void HandlePlayerMove(RealmEventBase baseEvent) {
        PlayerMoveEvent playerMoveEvent = baseEvent as PlayerMoveEvent;

        if (playerMoveEvent.playerId.Equals(playerId)) {
            Vector3Int currentCell = mapGrid.WorldToCell(transform.position);
            Vector3Int destinationCell = currentCell;

            switch(playerMoveEvent.direction) {
                case "1":
                    destinationCell.x = currentCell.x + 1;
                    destinationCell.y = currentCell.y;
                    break;
                case "2":
                    if (currentCell.y % 2 == 0) {
                        destinationCell.x = currentCell.x;
                    } else {
                        destinationCell.x = currentCell.x + 1;
                    }
                    destinationCell.y = currentCell.y - 1;
                    break;
                case "3":
                    if (currentCell.y % 2 == 0) {
                        destinationCell.x = currentCell.x - 1;
                    } else {
                        destinationCell.x = currentCell.x;
                    }
                    destinationCell.y = currentCell.y - 1;
                    break;
                case "4":
                    destinationCell.x = currentCell.x - 1;
                    destinationCell.y = currentCell.y;
                    break;
                case "5":
                    if (currentCell.y % 2 == 0) {
                        destinationCell.x = currentCell.x - 1;
                    } else {
                        destinationCell.x = currentCell.x;
                    }
                    destinationCell.y = currentCell.y + 1;
                    break;
                case "6":
                    if (currentCell.y % 2 == 0) {
                        destinationCell.x = currentCell.x;
                    } else {
                        destinationCell.x = currentCell.x + 1;
                    }
                    destinationCell.y = currentCell.y + 1;
                    break;
                default:
                    break;
            }

            this.transform.position = mapGrid.CellToWorld(destinationCell);
        }
    }
}
