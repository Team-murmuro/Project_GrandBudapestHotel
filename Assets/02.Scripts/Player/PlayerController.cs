using TMPro;
using UnityEngine;
using Utils.EnumType;

public class PlayerController : MonoBehaviour
{
    // 플레이어의 현재 상태
    private PlayerState playerState = PlayerState.Idle;

    private PlayerMovement playerMovement;
    private GameObject interactionObject;
    private TextMeshProUGUI interactionText;

    private RaycastHit2D hit;
    private Vector2 dir;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        interactionObject = transform.GetChild(0).GetChild(0).gameObject;
        interactionText = interactionObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (playerMovement.moveDir != Vector2.zero)
            dir = playerMovement.moveDir;

        OnDirection();
        OnInteraction();
    }

    // 대화, 문열기, 각종 상호작용 실행
    public void OnInteraction()
    {
        if(hit.collider != null)
        {
            if (hit.collider.CompareTag("Customer"))
            {
                if(hit.collider.GetComponent<CustomerController>()?.zone == ZoneType.Infomation)
                {
                    InteractionHandler("체그인 진행");

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        RoomManager.Instance.AssignRoom(hit.collider.GetComponent<CustomerController>());
                    }
                }
            }
            if(hit.collider.CompareTag("Room"))
            {
                if (!hit.collider.isTrigger)
                {
                    Debug.Log(":: 문 열기 ::");
                    InteractionHandler("문 열기");

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        hit.collider.isTrigger = true;
                    }
                }
                else
                    interactionObject.SetActive(false);
            }
            if (hit.collider.CompareTag("CCTV"))
            {
                Debug.Log(":: CCTV 보기 ::");
                InteractionHandler("CCTV 보기");
            }
        }
    }

    // 상호작용 UI 변경
    public void InteractionHandler(string _text)
    {
        interactionText.text = _text;
        interactionObject.SetActive(true);
    }

    // 방향 확인
    public void OnDirection()
    {
        if (hit.collider == null)
            interactionObject.SetActive(false);

        hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), dir, 2f, (1 << 7) + (1 << 8) + (1 << 11));
    }
}