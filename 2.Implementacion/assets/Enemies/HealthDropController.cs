using System.Collections;
using UnityEngine;

public class HealthDropController : MonoBehaviour{
    void Start(){
        StartCoroutine(DestroyTimer());
    }

    void Update(){
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "MeleeAttack"){
            collision.gameObject.GetComponent<HealthUIManager>().Heal(1);
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyTimer(){
        yield return new WaitForSeconds(10);
        GetComponent<Animator>().speed = 2.5f;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
