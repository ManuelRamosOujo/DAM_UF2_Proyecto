using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawn : MonoBehaviour{
    private Coordenates coordenates;
    private List<Vector3> spawnPositions = new List<Vector3>();
    private Camera mainCamera;
    public Tilemap fondoTilemap;
    public Tilemap paredTilemap;
    private Transform playerPosition;

    private HashSet<Vector2> restrictedPositions = new HashSet<Vector2>{
        new Vector2(0, 4),
        new Vector2(0, 0)
    };

    void Awake(){
        mainCamera = gameObject.GetComponent<Camera>();
        coordenates = gameObject.GetComponent<Coordenates>();
        playerPosition = GameObject.Find("Player").transform;
    }

    void Update(){
        
    }

    public void Spawn(){
        if (!restrictedPositions.Contains(new Vector2(coordenates.positionX,coordenates.positionY))){
            if (coordenates.IsInOverworld()){
                SpawnEnemies();
            }
        }
    }

    public void SpawnEnemies(){
        ChoosePosition();
        string[] enemy = {"Crioaracnido","Topomba"};
        if (spawnPositions.Count != 0){
            for (int i = 0; i < 4; i++){
                int rng = UnityEngine.Random.Range(0,enemy.Length);
                GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/" + enemy[rng]),
                spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Count)], new Quaternion());
                obj.name = enemy[rng];
            }
        }
    }

    private void ChoosePosition(){
        spawnPositions.Clear();
        Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));
        
        foreach (var position in fondoTilemap.cellBounds.allPositionsWithin){
            if (position.x >= bottomLeft.x + 0.5f && position.x <= topRight.x - 0.5f &&
                position.y >= bottomLeft.y + 0.5f && position.y <= topRight.y - 0.5f){
                if (fondoTilemap.HasTile(position) && !paredTilemap.HasTile(position)){
                    Vector3 worldPosition = fondoTilemap.CellToWorld(position) + fondoTilemap.tileAnchor;
                    worldPosition = new Vector3(worldPosition.x,worldPosition.y,-2);
                    if (Vector3.Distance(worldPosition, playerPosition.position) >= 5){
                        spawnPositions.Add(worldPosition);
                    }
                }
            }
        }
    }
}
