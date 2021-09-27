using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private LevelManager _lvlMng;
    public GameObject fire;

    void Start()
    {
        _lvlMng = GameObject.FindGameObjectWithTag("LVLMNG").GetComponent<LevelManager>();
        fire.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _lvlMng.lastCheckPointPos = transform.position;
            fire.SetActive(true);
        }
    }
}
