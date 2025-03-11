using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] int selection;
    private string[] enemy = {"Crioaracnido","Topomba"};
    GameObject obj;
    private Camera mainCamera;
    private bool checkEnemies = false;
    private bool canSpawn = true;
    private bool InCooldown = false;
    private bool enemyKilled = false;
    private Coordenates coordenates;
    [SerializeField] Vector2 posicionSpawner;

    void Start() {
        mainCamera = Camera.main;
        coordenates = GameObject.Find("Main Camera").GetComponent<Coordenates>();
    }
    
    void Update() {
        Vector2 playerPosition = new Vector2(coordenates.GetPositionX(),coordenates.GetPositionY());
        if (playerPosition == posicionSpawner){
            StartCoroutine(EnableCheck());
            if (checkEnemies){
                if (obj == null ) {
                    enemyKilled = true;
                }
                if (canSpawn && enemyKilled ){
                    StartCoroutine(SpawnEnemy());
                }
            }
        } else {
            if (enemyKilled) {
                StartCoroutine(StartCooldown());
            } else {
                canSpawn = true;
                InCooldown = false;
            }
            DisableCheck();
        }
    }

    private IEnumerator SpawnEnemy(){
        canSpawn = false;
        yield return new WaitForSeconds(0.5f);
        obj = Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/" + enemy[selection]),
                gameObject.transform.position, new Quaternion());
        obj.name = enemy[selection];
        enemyKilled = false;
    }

    private IEnumerator StartCooldown(){
        InCooldown = true;
        yield return new WaitForSeconds(30f);
        if (InCooldown){
            canSpawn = true;
            InCooldown = false;
        }
    }

    private IEnumerator EnableCheck(){
        yield return new WaitForSeconds(0.6f);
        checkEnemies = true;
    }

    private void DisableCheck(){
        checkEnemies = false;
    }
}
