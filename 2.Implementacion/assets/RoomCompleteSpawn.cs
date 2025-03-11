using System.Collections;
using UnityEngine;

public class RoomCompleteSpawn : MonoBehaviour{
    private Camera mainCamera;
    private bool checkEnemies = false;
    void Start() {
        mainCamera = Camera.main;
    }
    
    void Update() {
        if (IsVisible(gameObject)){
            StartCoroutine(EnableCheck());
            if (checkEnemies){
                int enemiesOnScreen = CountEnemiesOnScreen();
                if (enemiesOnScreen == 0){
                    SpawnItem();
                }
            }
        } else {
            DisableCheck();
        }
    }

    private int CountEnemiesOnScreen() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int count = 0;

        foreach (GameObject enemy in enemies){
            if (IsVisible(enemy)){
                count++;
            }
        }

        return count;
    }

    private bool IsVisible(GameObject obj){
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(obj.transform.position);
        bool isVisible = viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
            viewportPoint.y >= 0 && viewportPoint.y <= 1;
        return isVisible;
    }

    private void SpawnItem(){
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private IEnumerator EnableCheck(){
        yield return new WaitForSeconds(1f);
        checkEnemies = true;
    }

    private void DisableCheck(){
        checkEnemies = false;
    }
}
