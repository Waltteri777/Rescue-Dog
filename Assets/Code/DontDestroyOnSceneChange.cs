using UnityEngine;

public class DontDestroyOnSceneChange : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
