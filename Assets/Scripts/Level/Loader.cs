using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public TextMeshProUGUI time;
    public TextMeshProUGUI deaths;
    public TextMeshProUGUI letters;
    public string level;

    // Start is called before the first frame update
    void Start()
    {
        time.SetText(LevelManager.instance.time.ToString()+" sec");
        deaths.SetText(LevelManager.instance.deaths.ToString());
        letters.SetText(LevelManager.instance.lettersFound.ToString());
    }

    public void Continue()
    {
        Destroy(GameObject.FindGameObjectWithTag("LVLMNG"));
        Destroy(GameObject.FindGameObjectWithTag("Letters"));

        if(level == "level1")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if(level == "level2")
        {
            SceneManager.LoadScene(0);
        }
        
    }
}
