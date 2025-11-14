using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

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
}
