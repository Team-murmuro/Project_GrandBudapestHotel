using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

    private bool isGameOver = false;
    public bool IsGameOver { get { return isGameOver; } }

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

    private void Init()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 65;
        Screen.SetResolution(1920, 1080, true);
    }

    // 게임 나가기
    private void OnExit()
    {
        // 게임 데이터 저장

    }

    // 게임 종료하기
    private void OnQuit()
    {
        Application.Quit();
    }
}