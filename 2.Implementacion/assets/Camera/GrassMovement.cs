using UnityEngine;

public class GrassMovement : MonoBehaviour{
    private float speed = 1f;
    private float strength = 0.0001f;
    void Update(){
        transform.position += new Vector3(Mathf.Sin(Time.time * speed) * strength, 0, 0);
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100 - 64);
    }
}
