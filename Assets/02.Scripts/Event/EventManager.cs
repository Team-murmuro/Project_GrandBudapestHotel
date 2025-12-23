using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;
    public static EventManager Instance {  get { return instance; } }

    private void Awake()
    {
        if(instance != null &&  instance != this)
        {
            Destroy(instance);
            return;
        }

        instance = this;
    }
}