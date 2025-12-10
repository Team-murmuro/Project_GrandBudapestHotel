using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRb;

    public Vector2 moveDir;
    private float moveSpeed = 5.0f;
    private const float moveThreshold = 0.01f;

    private Vector2 minPlayerBoundary = new Vector2(-17.9f, -9.7f);
    private Vector2 maxPlayerBoundary = new Vector2(17.9f, 9.7f);

    public bool isMove = false;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    // 플레이어 이동
    public void Move()
    {
        moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        playerRb.MovePosition(playerRb.position + (moveDir.normalized * moveSpeed * Time.deltaTime));
        isMove = moveDir.sqrMagnitude > moveThreshold;

        // 맵 밖으로 이동 제한
        if (transform.position.x < minPlayerBoundary.x)
            transform.position = new Vector3(minPlayerBoundary.x, transform.position.y, transform.position.z);
        else if (transform.position.x > maxPlayerBoundary.x)
            transform.position = new Vector3(maxPlayerBoundary.x, transform.position.y, transform.position.z);
        else if (transform.position.y < minPlayerBoundary.y)
            transform.position = new Vector3(transform.position.x, minPlayerBoundary.y, transform.position.z);
        else if (transform.position.y > maxPlayerBoundary.y)
            transform.position = new Vector3(transform.position.x, maxPlayerBoundary.y, transform.position.z);
    }

    private void OnTriggerStay2D(Collider2D _coll)
    {
        if (_coll.CompareTag("Room"))
        {
            _coll.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D _coll)
    {
        if (_coll.CompareTag("Room"))
        {
            _coll.isTrigger = false;
            _coll.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}