using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Movimiento_topomba : MonoBehaviour{
    private Animator animator;
    private bool frozen = false;
    private bool searching = true;
    private float vision = 1f;
    //private GameObject visionCircle;
    private new BoxCollider2D collider;

    private void Awake() {
        animator = gameObject.GetComponent<Animator>();
        //visionCircle = transform.Find("Vision").gameObject;
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Start() {
    }

    private void FixedUpdate() {
        if (!frozen && searching){
            SearchPlayer();
        }
    }

    private void Update(){
        
    }

    private IEnumerator MoveToPlayer(){
        vision = 1f;
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        //visionCircle.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        //visionCircle.transform.localScale = new Vector3(0,0,-3f);
        renderer.enabled = false;
        collider.enabled = false;
        
        SetFrozen(true);
        yield return new WaitForSeconds(2f);

        Vector2 playerPosition = GameObject.Find("Player").transform.position;
        transform.position = new Vector3(playerPosition.x + UnityEngine.Random.Range(1.2f,2f) * (UnityEngine.Random.Range(0,2) > 0 ? 1 : -1),
        playerPosition.y + UnityEngine.Random.Range(1.2f,2f) * (UnityEngine.Random.Range(0,2) > 0 ? 1 : -1),-3);
        renderer.enabled = true;
        collider.enabled = true;
        StartCoroutine(Attack());
    }

    public void SetFrozen(bool option){
        frozen = option;
    }

    public void SearchPlayer(){
        Vector2 playerPosition = GameObject.Find("Player").transform.position;
        float distance = Vector2.Distance(playerPosition,transform.position);
        if (vision >= distance && !frozen){
            searching = false;
            StartCoroutine(MoveToPlayer());
        } else if (!frozen){
            //visionCircle.GetComponent<SpriteRenderer>().enabled = true;
            vision *= 1.025f;
            //visionCircle.transform.localScale = new Vector3(vision * 2,vision * 2,-3f);
        }
    }

    private IEnumerator Attack(){
        yield return new WaitForSeconds(0.8f);
        GameObject bomb = Instantiate(Resources.Load<GameObject>("Prefabs/Bomb"),
        gameObject.transform.position, new Quaternion());
        bomb.name = "Bomb";
        yield return new WaitForSeconds(0.1f);
        SetFrozen(false);
        searching = true;
    }
}
