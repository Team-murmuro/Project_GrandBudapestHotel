using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D playerCollider;

    private Vector2 moveDir;
    private float moveSpeed = 2.5f;
    private const float moveThreshold = 0.01f;

    public bool isMove = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Move();
    }

    // 플레이어 이동
    public void Move()
    {
        moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.MovePosition(rb.position + (moveDir.normalized * moveSpeed * Time.deltaTime));

        //if (moveDir.y > 0)
        //    SetDirection(Direction.Back);
        //else if (moveDir.y < 0)
        //    SetDirection(Direction.Front);
        //else if (moveDir.x != 0)
        //    SetDirection(moveDir.x > 0 ? Direction.Right : Direction.Left);

        isMove = moveDir.sqrMagnitude > moveThreshold;
    }
}
