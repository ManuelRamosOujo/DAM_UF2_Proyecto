using System.Collections;
using UnityEngine;

public class Movimiento_anillo : MonoBehaviour{
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0.3f,0.3f));
    }
}
