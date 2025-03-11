using System;
using UnityEngine;

public class Movimiento : MonoBehaviour{
    private Vector2 _moveDir = Vector2.zero;
    private Rigidbody2D rb;
    private bool frozen = false;
    private int direction = 0;
    private Melee_colision colision;
    private Animator animator;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        colision = GameObject.Find("MeleeAttack").GetComponent<Melee_colision>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        
    }

    private void FixedUpdate() {
        GetInput();
    }

    private void Update(){
        
    }

    private void GetInput(){
        _moveDir.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * 20 * 30;
        _moveDir.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * 20 * 30;

        if (!frozen){
            Move();
        }
    }

    private void Move(){
        Vector2 vector2d = new Vector2(_moveDir.x,_moveDir.y);
        float diferenciaAxis = Mathf.Abs(vector2d.x) - Mathf.Abs(vector2d.y);
        if (vector2d.x == 0 && vector2d.y == 0){
            animator.SetBool("Moving",false);
        } else {
            animator.SetBool("Moving",true);
        }
        
        if (diferenciaAxis >= 0){
            if (_moveDir.x > 0){
                animator.SetFloat("Direction",1);
                direction = 1;
            } else if(_moveDir.x < 0){
                animator.SetFloat("Direction",3);
                direction = 3;
            }
            rb.velocity = new Vector2(_moveDir.x, 0);
        } else {
            if (_moveDir.y > 0){
                animator.SetFloat("Direction",2);
                direction = 2;
            } else if(_moveDir.y < 0){
                animator.SetFloat("Direction",0);
                direction = 0;
            }
            rb.velocity = new Vector2(0, _moveDir.y);
        }
        colision.ChangeDirection();
    }

    public void SetFrozen(bool option){
        frozen = option;
        if (frozen){
            animator.SetBool("Moving",false);
            rb.velocity = new Vector2(0,0);
        }
    }

    public bool GetFrozen(){
        return frozen;
    }

    public int GetDirection(){
        return direction;
    }

    public void CameraAutoMove(String direction){
        switch (direction){
                case "x":
                    animator.SetFloat("Direction",1);
                    rb.velocity = new Vector2(0.7f,0);
                    break;
                case "-x":
                    animator.SetFloat("Direction",3);
                    rb.velocity = new Vector2(-0.7f,0);
                    break;
                case "y":
                    animator.SetFloat("Direction",2);
                    rb.velocity = new Vector2(0,1);
                    break;
                case "-y":
                    animator.SetFloat("Direction",0);
                    rb.velocity = new Vector2(0,-1.2f);
                    break;
            }
        
    }
}