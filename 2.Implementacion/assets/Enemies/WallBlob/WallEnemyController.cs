using System.Collections;
using UnityEngine;

public class WallEnemyController : MonoBehaviour{
    public enum InitialDirection { Right, Left }
    public InitialDirection initialDirection = InitialDirection.Right;
    public float speed = 2.5f;
    private float detectionRange = 0.3f;
    [SerializeField] LayerMask wallLayers;
    private Rigidbody2D rb;
    private Vector2 currentDirection;
    private bool canTurn = true;
    private float turnCooldown = 0.18f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D no encontrado en el enemigo.");
        }
        
        SetInitialDirection();
        rb.velocity = currentDirection * speed;
    }

    void Update()
    {
        FollowWall();
    }

    void SetInitialDirection()
    {
        switch (initialDirection)
        {
            case InitialDirection.Right:
                currentDirection = Vector2.right;
                break;
            case InitialDirection.Left:
                currentDirection = Vector2.left;
                break;
        }
    }

    void FollowWall()
    {
        if (!canTurn) return;
        
        Vector3 enemyPosition = transform.position;
        Vector2 forward = currentDirection;
        Vector2 down = GetDownDirection();
        Vector3 backPosition = enemyPosition - (Vector3)forward * 0.3f;

        bool wallAhead = IsWallAt(enemyPosition, forward, 1);
        bool noWallBelow = !IsWallAt(backPosition, down, 1.2f);
        bool noWallBelowMiddle = !IsWallAt(enemyPosition, down, 1.2f);
        
        if (wallAhead) {
            StartCoroutine(TurnCooldown(1));
            if (initialDirection == InitialDirection.Right) {
                RotateLeft();
            } else {
                RotateRight();
            }
        } else if (noWallBelow && noWallBelowMiddle) {
            StartCoroutine(TurnCooldown(1.2f));
            if (initialDirection == InitialDirection.Left) {
                RotateLeft();
            } else {
                RotateRight();
            }
        } else {
            rb.velocity = currentDirection * speed;
        }
    }

    IEnumerator TurnCooldown(float extra){
        canTurn = false;
        yield return new WaitForSeconds(turnCooldown * extra);
        canTurn = true;
    }

    void RotateLeft(){
        currentDirection = new Vector2(-currentDirection.y, currentDirection.x);
        rb.velocity = currentDirection * speed;
    }

    void RotateRight(){
        currentDirection = new Vector2(currentDirection.y, -currentDirection.x);
        rb.velocity = currentDirection * speed;
    }

    Vector2 GetDownDirection(){
        if (initialDirection == InitialDirection.Right){
            if (currentDirection == Vector2.right)
            return Vector2.down;
            if (currentDirection == Vector2.up)
                return Vector2.right;
            if (currentDirection == Vector2.left)
                return Vector2.up;
            if (currentDirection == Vector2.down)
                return Vector2.left;
            return Vector2.down; 
        } else {
            if (currentDirection == Vector2.right)
            return Vector2.up;
            if (currentDirection == Vector2.up)
                return Vector2.left;
            if (currentDirection == Vector2.left)
                return Vector2.down;
            if (currentDirection == Vector2.down)
                return Vector2.right;
            return Vector2.down; 
        }
    }

    bool IsWallAt(Vector3 position, Vector2 direction, float multiplier){
        float boxSize = 0.1f;
        RaycastHit2D hit = Physics2D.BoxCast(position, new Vector2(boxSize, boxSize), 0f, direction, detectionRange * multiplier, wallLayers);
        return hit.collider != null;
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Vector3 enemyPosition = transform.position;
        Vector2 forward = currentDirection;
        Vector2 down = GetDownDirection();
        Vector3 backPosition = enemyPosition - (Vector3)forward * 0.3f;
        
        Gizmos.DrawRay(enemyPosition, forward * detectionRange);
        Gizmos.DrawRay(backPosition, down * detectionRange * 1.2f);
    }
}
