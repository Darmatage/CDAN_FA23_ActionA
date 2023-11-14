using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTile : MonoBehaviour{
       public Tile newTile;
       public Tilemap targetTilemap;

       private Vector3Int previous;

       // Make these changes in LateUpdate() so player has option to move in Update()
       private void LateUpdate(){
              // Get the current grid location
              Vector3Int currentCell = targetTilemap.WorldToCell(transform.position);
              // Add one in a direction (change this as needed to match your directional control)
              currentCell.x += 1;

              // If the position has changed
              if (currentCell != previous){
                     // erase previous
                     targetTilemap.SetTile(previous, null);

                     // save the new position for next frame
                     previous = currentCell;
              }
       }
}