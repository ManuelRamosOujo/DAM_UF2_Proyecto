using System.Collections;
using UnityEngine;

public class Coordenates : MonoBehaviour{
    Vector3 initialPosition = new Vector3(-4.6f,0f,-10f);
    public int positionX = 0;
    public int positionY = 0;
    private bool inOverworld = true;
    private EnemySpawn enemySpawn;
    private Vector2 playerSpawnPosition;

    void Start(){
        enemySpawn = gameObject.GetComponent<EnemySpawn>();
    }

    void Update(){
        
    }

    public IEnumerator Teleport(int x, int y, bool overworld){
        positionX = x;
        positionY = y;
        inOverworld = overworld;
        Vector3 newPosition = new Vector3(
            initialPosition.x+(x*20),initialPosition.y+(y*11),initialPosition.z);
        gameObject.transform.position = newPosition;
        gameObject.GetComponent<CameraController>().SetPosicionInicial(newPosition);
        DestroyEnemies();
        yield return new WaitForSeconds(0.1f);
        SpawnEnemies();
    }

    public void SpawnEnemies(){
        enemySpawn.Spawn();
    }

    public void DestroyEnemies(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies){
            Destroy(enemy);
        }
    }

    public int GetPositionX(){
        return positionX;
    }

    public void SetPositionX(int newPosition){
        positionX = newPosition;
    }

    public int GetPositionY(){
        return positionY;
    }

    public void SetPositionY(int newPosition){
        positionY = newPosition;
    }

    public bool IsInOverworld(){
        return inOverworld;
    }

    public Vector2 GetPlayerSpawnPosition(){
        return playerSpawnPosition;
    }

    public void SetPlayerSpawnPosition(Vector2 newPosition){
        playerSpawnPosition = newPosition;
    }

    public IEnumerator Respawn(){
        GameObject player = GameObject.Find("Player");
        player.GetComponent<Movimiento>().SetFrozen(true);
        player.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Transform>().position = playerSpawnPosition;
        player.GetComponent<DetectarColision>().SetFalling(false);
        player.GetComponent<Movimiento>().SetFrozen(false);
        player.GetComponent<SpriteRenderer>().enabled = true;
    }
}
