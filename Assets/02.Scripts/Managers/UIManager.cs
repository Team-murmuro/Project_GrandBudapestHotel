using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    private Canvas canvas;
    private Camera cctvCam;

    public Sprite[] cctvSprits;
    private GameObject cctvPhanel;
    public List<Image> cctvImages;
    private List<Button> cctvButtons;

    public Vector3[] cctvPos;

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
        cctvButtons = cctvPhanel.transform.GetChild(1).GetComponentsInChildren<Button>().ToList();

        foreach (var button in cctvButtons)
        {
            cctvImages.Add(button.transform.parent.GetComponent<Image>());
        }
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
        foreach (var image in cctvImages)
        {
            image.sprite = cctvSprits[0];
        }

        cctvImages[_floor].sprite = cctvSprits[1];
        cctvCam.transform.position = cctvPos[_floor];
        cctvCam.Render();
    }
}