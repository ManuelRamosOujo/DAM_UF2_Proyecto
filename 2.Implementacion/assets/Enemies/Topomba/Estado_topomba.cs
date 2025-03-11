using UnityEngine;

public class Estado_topomba : MonoBehaviour{
    private float maxHealth = 5;
    private float health = 5;

    void Start(){
        
    }

    void Update(){
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100 - 68);
    }

    public void RestoreHealth(){
        health = maxHealth;
    }
    public float GetHealth(){
        return health;
    }
    public float ReduceHealth(float damage){
        health -= damage;

        if (health <= 0){
            if(Random.Range(0,10) > 4){
                DropLoot();
            }
            Destroy(gameObject);
        }
        return health;
    }

    private void DropLoot(){
        GameObject vida = Instantiate(Resources.Load<GameObject>("Prefabs/HealthDrop"),
        transform.position, new Quaternion());
        vida.transform.SetParent(null);
        vida.name = "HealthDrop";
    } 
}
