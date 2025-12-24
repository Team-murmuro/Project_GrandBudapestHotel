using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    private Canvas canvas;
    private Camera cctvCam;

    private GameObject cctvPhanel;
    private Button[] cctvButtons;
    public Sprite[] cctvSprits;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Init();
        instance = this;
    }

    private void Start()
    {
        canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        cctvCam = GameObject.Find("CCTV Camera").GetComponent<Camera>();

        cctvPhanel = canvas.transform.GetChild(1).gameObject;
        cctvButtons = cctvPhanel.transform.GetChild(1).GetComponentsInChildren<Button>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (cctvPhanel.activeSelf)
            {
                cctvCam.enabled = false;
                cctvPhanel.SetActive(false);
            }
        }
    }

    private void Init()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnCCTV()
    {
        cctvCam.enabled = true;
        cctvPhanel.SetActive(true);
    }

    public void OnCCTVButton(int _floor)
    {

    }
}