using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int deaths = 0;
    public int lettersFound = 0;
    public GameObject playerPrefab;
    public float delayPlayerRespawn;
    public Vector2 lastCheckPointPos;
    public CinemachineVirtualCameraBase cam;
    public float time = 0;
    
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            DontDestroyOnLoad(cam);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        time += Time.deltaTime;       
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
