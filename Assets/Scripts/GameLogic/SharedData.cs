using UnityEngine;

public class SharedData : MonoBehaviour
{
    public static SharedData Instance { get; private set; }

    public int data;

    private void Awake()
    {
        // This is the simplest method for the sake of demo
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
