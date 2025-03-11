using System.Collections;
using UnityEngine;

public class Melee_colision : MonoBehaviour
{
    private Movimiento movimiento_jugador;
    private Collider2D colision;
    private Rigidbody2D rb;
    private Animator animator;
    private int direccion;
    private bool isBeam = false;
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        movimiento_jugador = GameObject.Find("Player").GetComponent<Movimiento>();
        colision = gameObject.GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    public void ChangeDirection()
    {
        direccion = movimiento_jugador.GetDirection();
        switch (direccion)
        {
            case 0:
                transform.localPosition = new Vector3(0, -1.2f, 0);
                transform.localScale = new Vector3(2f, -1.6f, 0);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 1:
                transform.localPosition = new Vector3(0.7f, -0.2f, 0);
                transform.localScale = new Vector3(1.6f, 1.5f, 1);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 2:
                transform.localPosition = new Vector3(0, 1.2f, 0);
                transform.localScale = new Vector3(2f, -1.6f, 0);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 3:
                transform.localPosition = new Vector3(-0.7f, -0.2f, 0);
                transform.localScale = new Vector3(1.6f, 1.5f, 1);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }

    private void BeamDirection(){
        switch (direccion)
        {
            case 0:
                rb.velocity = new Vector2(0,-20);
                break;
            case 1:
                rb.velocity = new Vector2(20,0);
                break;
            case 2:
                rb.velocity = new Vector2(0,20);
                break;
            case 3:
                rb.velocity = new Vector2(-20,0);
                break;
        }
    }

    public IEnumerator EnableColision(){
        Movimiento movimiento = GameObject.Find("Player").GetComponent<Movimiento>();
        movimiento.SetFrozen(true);
        ChangeDirection();
        colision.enabled = true;
        colision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        if (!isBeam){
            colision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            colision.enabled = false;
        }
        movimiento.SetFrozen(false);
    }

    public IEnumerator ShootEnergyBeam()
    {
        GameObject beam = Instantiate(Resources.Load<GameObject>("Prefabs/MeleeAttack"),
        new Vector3(0, 0, 0), new Quaternion(),GameObject.Find("Player").transform);
        // animator.SetBool("Beam",true);
        beam.name = "SwordBeam";
        
        Melee_colision beamColision = beam.GetComponent<Melee_colision>();
        beamColision.ChangeDirection();
        beam.transform.localScale = new Vector3(1.6f, 1.5f, 0);
        beam.transform.rotation = Quaternion.Euler(0, 0, 0);
        beamColision.isBeam = true;
        beamColision.BeamDirection();
        beamColision.GetComponent<SpriteRenderer>().enabled = true;
        beamColision.GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(beam);
    }

    public int GetDirection(){
        return direccion;
    }
}
