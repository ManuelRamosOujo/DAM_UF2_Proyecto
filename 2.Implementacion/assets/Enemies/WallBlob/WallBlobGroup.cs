using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlobGroup : MonoBehaviour{
    private GameObject pointA;
    private GameObject pointB;
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    [SerializeField] float attackDuration;

    void Start(){
        pointA = gameObject.transform.GetChild(0).gameObject;
        pointB = gameObject.transform.GetChild(1).gameObject;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        
        edgeCollider = GetComponent<EdgeCollider2D>();

        lineRenderer.enabled = false;
        edgeCollider.enabled = false;

        StartCoroutine(LineCycle());

        UpdateLine();
    }

    void Update(){
        UpdateLine();
    }

    void UpdateLine(){
        if (pointA == null || pointB == null) return;

        Vector3 worldPosA = pointA.transform.position;
        Vector3 worldPosB = pointB.transform.position;
        lineRenderer.SetPositions(new Vector3[] { worldPosA, worldPosB });

        Vector2 localPosA = pointA.transform.localPosition;
        Vector2 localPosB = pointB.transform.localPosition;
        edgeCollider.SetPoints(new List<Vector2> { localPosA, localPosB });
    }

    private IEnumerator LineCycle(){
        while (true){
            lineRenderer.enabled = true;
            edgeCollider.enabled = true;

            yield return new WaitForSeconds(attackDuration); 

            lineRenderer.enabled = false;
            edgeCollider.enabled = false;

            yield return new WaitForSeconds(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.name == "Player"){
            collision.GetComponent<HealthUIManager>().LoseHealth(1.5f);
        }
    }
}
