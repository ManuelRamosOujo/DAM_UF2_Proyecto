using UnityEngine;
using UnityEngine.Tilemaps;

public class GrassScript : MonoBehaviour{
    public Tilemap grassTilemap; // Asigna aquí tu Tilemap de hierba
    public Sprite grassSprite; // Usa la misma textura de la hierba

    void Start()
    {
        SpawnGrassObjects();
    }

    void Update() {
    }

    void SpawnGrassObjects()
    {
        BoundsInt bounds = grassTilemap.cellBounds;
        TileBase[] allTiles = grassTilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null) // Si hay un tile de hierba
                {
                    Vector3Int cellPosition = new Vector3Int(bounds.x + x, bounds.y + y, 0);
                    Vector3 worldPosition = grassTilemap.GetCellCenterWorld(cellPosition);

                    CreateGrassObject(worldPosition);
                }
            }
        }
        
        // Desactiva el Tilemap de hierba para evitar que se dibuje dos veces
        grassTilemap.gameObject.SetActive(false);
    }

    void CreateGrassObject(Vector3 position)
    {
        GameObject grassObject = new GameObject("Grass");
        SpriteRenderer sr = grassObject.AddComponent<SpriteRenderer>();

        sr.sprite = grassSprite; // Asigna la imagen de la hierba
        sr.sortingLayerName = "Objects"; // Asegura que esté en la misma capa que los personajes

        grassObject.transform.position = position;
        grassObject.AddComponent<GrassMovement>();
    }
}
