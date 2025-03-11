using System.Collections;
using UnityEngine;

public class Dañar_bomba : MonoBehaviour{
    private float attackPower = 1.5f;
    private bool exploding = false;

    void Start(){
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(Explosion());
    }

    void Update(){
        if (exploding){
            GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100 + 180);
        } else {
            GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100 - 65);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player"){
           other.gameObject.GetComponent<HealthUIManager>().LoseHealth(attackPower);
        }
    }

    private IEnumerator Explosion(){
        yield return new WaitForSeconds(1.3f);
        GetComponent<CircleCollider2D>().enabled = true;
        transform.localScale = new Vector3(2f,2f,2f);
        exploding = true;
        yield return new WaitForSeconds(0.38f);
        GetComponent<CircleCollider2D>().enabled = false;
        Invoke("Despawn",1.4f);
    }

    private void Despawn(){
        Destroy(gameObject);
    }

    public float GetAttackPower(){
        return attackPower;
    }
}
