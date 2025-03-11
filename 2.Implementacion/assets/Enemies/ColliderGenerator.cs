using UnityEngine;
using UnityEngine.Tilemaps;

public class ColliderGenerator : MonoBehaviour{
    private Tilemap tilemap;
    private GameObject tilePrefab;
    private void Awake() {
        tilemap = gameObject.GetComponent<Tilemap>();
        tilePrefab = Resources.Load<GameObject>("Prefabs/HoleTrigger");
    }

    void Start(){
        GenerateTileColliders();
    }

    void GenerateTileColliders(){
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++){
            for (int y = bounds.yMin; y < bounds.yMax; y++){
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(tilePosition);

                if (tile != null){
                    Vector3 worldPos = tilemap.GetCellCenterWorld(tilePosition);
                    GameObject newTile = Instantiate(tilePrefab, worldPos, Quaternion.identity);
                    newTile.name = "Tile_" + tile.name;
                }
            }
        }
    }
}
