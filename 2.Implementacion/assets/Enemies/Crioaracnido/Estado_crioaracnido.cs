using UnityEngine;

public class Estado_crioaracnido : MonoBehaviour{
    private float maxHealth = 3;
    private float health = 3;
    private bool angry = false; 
    private float attackPower = 0.5f;

    void Start(){
        
    }

    void Update(){
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    public void RestoreHealth(){
        health = maxHealth;
    }
    public float GetHealth(){
        return health;
    }
    public bool GetAnger(){
        return angry;
    }
    public float GetAtackPower(){
        return attackPower;
    }
    public float ReduceHealth(float damage){
        health -= damage;
        if (health <= maxHealth / 3){
            angry = true;
        }

        if (health <= 0){
            if(Random.Range(0,10) > 7){
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
