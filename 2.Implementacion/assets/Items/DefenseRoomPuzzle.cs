using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DefenseRoomPuzzle : MonoBehaviour{
    [SerializeField] Tilemap tilemap;
    private Vector3Int[] posiciones= new Vector3Int[]{
            new Vector3Int(-425, -204, 0),
            new Vector3Int(-424, -204, 0),
            new Vector3Int(-426, -204, 0),
            new Vector3Int(-425, -203, 0),
            new Vector3Int(-424, -203, 0),
            new Vector3Int(-426, -203, 0)
        };
    void Start(){
        
    }

    void Update(){
        if (transform.position.x < -431.6f){
            transform.position = new Vector3(-431.6f,transform.position.y,-2);
        }

        if (transform.position.x >= -427.6f){
            transform.position = new Vector3(-427.6f,transform.position.y,-2);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            StartCoroutine(OpenPath());
        }
    }

    private IEnumerator OpenPath(){
        for (int i = 0; i < 3; i++){
            tilemap.SetTile(posiciones[i], null);
        }
        yield return new WaitForSeconds(0.4f);

        for (int i = 3; i < 6; i++){
            tilemap.SetTile(posiciones[i], null);
        }
    }
}
