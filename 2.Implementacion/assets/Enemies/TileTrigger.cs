using System.Collections;
using UnityEngine;

public class TileTrigger : MonoBehaviour{
    void OnTriggerStay2D(Collider2D other){
        Vector3 playerPosition = GameObject.Find("Player").transform.position;

        float offsetY = -0.8f;
        Vector3 playerLowerPoint = playerPosition + new Vector3(0, offsetY, 0);

        float tileSize = 1f;

        Vector3 center = transform.position;
        int numPoints = 10;
        float step = tileSize / (numPoints - 1);
        float minDistance = float.MaxValue;

        for (int i = 0; i < numPoints; i++)
        {
            float x = center.x - (tileSize / 2) + (i * step);
            Vector3 point = new Vector3(x, center.y, center.z);
            float distance = Vector2.Distance(new Vector2(playerLowerPoint.x, playerLowerPoint.y), 
                                            new Vector2(point.x, point.y));
            minDistance = Mathf.Min(minDistance, distance);
        }

        for (int i = 0; i < numPoints; i++){
            float y = center.y - (tileSize / 2) + (i * step);
            Vector3 point = new Vector3(center.x, y, center.z);
            float distance = Vector2.Distance(new Vector2(playerLowerPoint.x, playerLowerPoint.y), 
                                            new Vector2(point.x, point.y));
            minDistance = Mathf.Min(minDistance, distance);
        }

        Vector3 targetPosition = transform.position - new Vector3(0, offsetY, 0);

        if (minDistance < 0.55f){
            float attractionStrength = Mathf.Lerp(8f, 0.4f, minDistance / 0.8f);
            attractionStrength = Mathf.Max(attractionStrength, 1f);
            GameObject.Find("Player").transform.position = 
                Vector3.MoveTowards(playerPosition, targetPosition, attractionStrength * Time.deltaTime);
        }

        if (minDistance < 0.12f)
        {
            StartCoroutine(Fall(other));
        }
        
        // if (other.gameObject.tag == "Enemy"){
        //     if (Vector2.Distance(other.transform.position, gameObject.transform.position) < 0.8f){
        //         Destroy(other.gameObject);
        //     }
        // }
    }

    private IEnumerator Fall(Collider2D other){
        GetComponent<BoxCollider2D>().enabled = false;
        other.gameObject.GetComponent<DetectarColision>().SetFalling(true);
        StartCoroutine(GameObject.Find("Main Camera").GetComponent<Coordenates>().Respawn());
        other.gameObject.GetComponent<HealthUIManager>().LoseHealth(1f);
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
