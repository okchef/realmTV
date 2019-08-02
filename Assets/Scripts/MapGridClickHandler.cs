using UnityEngine;
using UnityEngine.EventSystems;

public class MapGridClickHandler : MonoBehaviour
{
    public Grid grid;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = grid.WorldToCell(world);
            Vector3 cellCenterWorld = grid.CellToWorld(cellPosition);

            Debug.Log(cellPosition);

            //EventEmitter.Move(cellCenterWorld);
        }
    }
}
