using UnityEngine;

public class Dañar_crioaracnido : MonoBehaviour{
    void Start(){
        
    }

    void Update(){
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "Player"){
           GameObject.Find("Player").GetComponent<HealthUIManager>()
           .LoseHealth(gameObject.GetComponent<Estado_crioaracnido>().GetAtackPower());
        }
    }
}
