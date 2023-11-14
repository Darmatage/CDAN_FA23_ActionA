using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_DestroyTiles : MonoBehaviour{

	public Tilemap destructableTilemap;
	private List<Vector3> tileWorldLocations;
	public float rangeDestroy = 2f;
	public bool canExplode = true;
	public GameObject boomVFX;
	//public AudioSource boomSFX;

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

		foreach (var pos in destructableTilemap.cellBounds.allPositionsWithin){
			Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
			Vector3 place = destructableTilemap.CellToWorld(localPlace) + new Vector3(0.5f, 0.5f, 0f);

			if (destructableTilemap.HasTile(localPlace)){
				tileWorldLocations.Add(place);
			}
		}
	}

	void destroyTileArea(){
		foreach (Vector3 tile in tileWorldLocations){
			if (Vector2.Distance(tile, hitPoint.position) <= rangeDestroy){
				//Debug.Log("in range");
				Vector3Int localPlace = destructableTilemap.WorldToCell(tile);
				if (destructableTilemap.HasTile(localPlace)){
					GameObject.FindWithTag("GameHandler").GetComponent<GameHandler_PlayerManager>().playerAddScore(1);
					StartCoroutine(BoomVFX(tile));
					destructableTilemap.SetTile(destructableTilemap.WorldToCell(tile), null);
				}
				//tileWorldLocations.Remove(tile);
			}
		}
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