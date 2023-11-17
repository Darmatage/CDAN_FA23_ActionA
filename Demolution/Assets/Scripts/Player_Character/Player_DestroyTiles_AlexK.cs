using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTiles_AlexK : MonoBehaviour{

	public Tilemap destructableTilemap;
	private List<Vector3> tileWorldLocations;
	private List<bool> tileWorldGround, tileWorldStable;
	public float rangeDestroy = 2f;
	public bool canExplode = true;
	public GameObject boomVFX;
	//public AudioSource boomSFX;

	public int ground = 0;
	
	private Transform hitPoint;
	public Transform hitPunch; // press 1
	public Transform hitKick; // press 2
	public Transform hitStomp; // press 3
	public Transform hitPunchUp; // press 4

	void Start(){
		TileMapInit();
	}

	void Update(){
		if (canExplode == true){
			if (Input.GetKeyDown("1")){
				hitPoint = hitPunch;
				destroyTileArea();
			} else if (Input.GetKeyDown("2")){
				hitPoint = hitKick;
				destroyTileArea();
			} else if (Input.GetKeyDown("3")){
				hitPoint = hitStomp;
				destroyTileArea();
			} else if (Input.GetKeyDown("4")){
				hitPoint = hitPunchUp;
				destroyTileArea();
			}
		}
		rangeDestroy = (rangeDestroy + GameHandler_PlayerManager.playerSize) /2;
	}

       void TileMapInit(){
              tileWorldLocations = new List<Vector3>();
              tileWorldGround = new List<bool>();

              foreach (var pos in destructableTilemap.cellBounds.allPositionsWithin){
                     Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                     Vector3 place = destructableTilemap.CellToWorld(localPlace) + new Vector3(0.5f, 0.5f, 0f);

                     if (destructableTilemap.HasTile(localPlace)){
                            tileWorldLocations.Add(place);
                            tileWorldGround.Add(place.y == ground + 0.5f);
                     }
              }
              tileWorldStable = new List<bool>(tileWorldGround);
       }

       void destroyTileArea(){
              for (int i = 0; i < tileWorldLocations.Count; i++) {
                     Vector3 tile = tileWorldLocations[i];

                     if (Vector2.Distance(tile, hitPoint.position) <= rangeDestroy){
                            destroyTile(i);
                            i--;
                     }
              }
		
		//look through the tile list again to find remaining "ground" tiles
              tileWorldStable = new List<bool>(tileWorldGround);
              for (int i = 0; i < tileWorldLocations.Count; i++) {
                     if (tileWorldGround[i]) {
                            // Debug.Log("Found Ground");
                            setNeighborsGround(tileWorldLocations[i]);
                     }
              }
		
		//look through the list a final time to find tiles that are not grounded.
              for (int i = 0; i < tileWorldLocations.Count; i++) {
                     if (!tileWorldStable[i]){
                            destroyTile(i);
                            i--;
                     }
              }
       }

	//FUNCTIONS FOR DETERMINING GROUNDED-NESS:
       private void setNeighborsGround (Vector3 tile) {
              setNeighborsCheck(tile + Vector3.up);
              setNeighborsCheck(tile + Vector3.down);
              setNeighborsCheck(tile + Vector3.left);
              setNeighborsCheck(tile + Vector3.right);
       }

       private void setNeighborsCheck (Vector3 tile) {
              int currTile = tileWorldLocations.IndexOf(tile);
              if (currTile != -1 && !tileWorldStable[currTile]) {
                     tileWorldStable[currTile] = true;
                     setNeighborsGround(tile);
              }
       }

	private void destroyTile(int i) {
		Vector3 tile = tileWorldLocations[i];
		// Debug.Log("Destoryed Tile: " + tile.x + ", " + tile.y +  ", " + tile.x);
		Vector3Int localPlace = destructableTilemap.WorldToCell(tile);
		if (destructableTilemap.HasTile(localPlace)){
			GameObject.FindWithTag("GameHandler").GetComponent<GameHandler_PlayerManager>().playerAddScore(1);
			StartCoroutine(BoomVFX(tile));
			destructableTilemap.SetTile(destructableTilemap.WorldToCell(tile), null);
		}

		tileWorldLocations.RemoveAt(i);
		tileWorldGround.RemoveAt(i);
		tileWorldStable.RemoveAt(i);
	}

	IEnumerator BoomVFX(Vector3 tilePos){
		//boomSFX.Play();
		GameObject tempVFX = Instantiate(boomVFX, tilePos, Quaternion.identity);
		yield return new WaitForSeconds(1f);
		Destroy(tempVFX);
	}

	//NOTE: To help see the attack sphere in editor:
	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position, rangeDestroy);
	}
}

//grounded ness functionality by Alex Koppel
