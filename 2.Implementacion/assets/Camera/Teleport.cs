using UnityEngine;
using UnityEngine.Tilemaps;

public class Teleport : MonoBehaviour{
    void Start(){   

    }
    void Update(){
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player"){
            CheckEntrance(other);
        }
    }

    private void CheckEntrance(Collider2D other){
        switch (gameObject.name){
            case "StairsLake":
                StartCoroutine(GameObject.Find("Main Camera").GetComponent<Coordenates>().Teleport(0,0,true));
                Vector3 destination = new Vector3(3.73f, 0.5f,-2f);
                other.gameObject.transform.position = destination;
                GameObject.Find("Main Camera").GetComponent<Coordenates>().SetPlayerSpawnPosition(destination);
                break;
            case "StairsToLake":
                StartCoroutine(GameObject.Find("Main Camera").GetComponent<Coordenates>().Teleport(0,3,true));
                destination = new Vector3(0.75f, 30f,-2f);
                other.gameObject.transform.position = destination;
                GameObject.Find("Main Camera").GetComponent<Coordenates>().SetPlayerSpawnPosition(destination);
                break;
            case "Level-1":
                StartCoroutine(GameObject.Find("Main Camera").GetComponent<Coordenates>().Teleport(-20,-20,false));
                destination = new Vector3(-404.60f, -220f,-2f);
                other.gameObject.transform.position = destination;
                GameObject.Find("Main Camera").GetComponent<Coordenates>().SetPlayerSpawnPosition(destination);
                Color azul = new Color(0f, 0.501960784f, 0.533333333f);
                GameObject.Find("Fondo").GetComponent<Tilemap>().color = azul;
                GameObject.Find("Paredes").GetComponent<Tilemap>().color = azul;
                GameObject.Find("Decoración").GetComponent<Tilemap>().color = azul;
                GameObject.Find("Agujeros").GetComponent<Tilemap>().color = azul;
                break;
            case "Level-1_exit":
                StartCoroutine(GameObject.Find("Main Camera").GetComponent<Coordenates>().Teleport(0,4,true));
                destination = new Vector3(-5.08f, 44f,-2f);
                other.gameObject.transform.position = destination;
                GameObject.Find("Main Camera").GetComponent<Coordenates>().SetPlayerSpawnPosition(destination);
                GameObject.Find("Fondo").GetComponent<Tilemap>().color = Color.white;
                GameObject.Find("Paredes").GetComponent<Tilemap>().color = Color.white;
                GameObject.Find("Decoración").GetComponent<Tilemap>().color = Color.white;
                GameObject.Find("Agujeros").GetComponent<Tilemap>().color = Color.white;
                break;
        }
    }
}
