using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour {
    private Camera mainCamera;
    private bool checkEnemies = false;
    private bool opening = false;
    void Start() {
        mainCamera = Camera.main;
    }

    void Update() {
        if (IsVisible(gameObject) && !opening){
            StartCoroutine(EnableCheck());
            if (checkEnemies){
                int enemiesOnScreen = CountEnemiesOnScreen();
                if (enemiesOnScreen == 0){
                    StartCoroutine(OpenDoorAnimation());
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
            viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
        return isVisible;
    }

    private IEnumerator OpenDoorAnimation(){
        opening = true;
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/puerta_candado");
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    private IEnumerator EnableCheck(){
        yield return new WaitForSeconds(0.2f);
        checkEnemies = true;
    }

    private void DisableCheck(){
        checkEnemies = false;
    }
}
