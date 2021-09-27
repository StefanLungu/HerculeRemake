using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lvl1Boss : MonoBehaviour
{
    private Animator _animator;
    public PlayerController _player;
    public float initTimer;
    public float timer;
    public bool cooling;
    public int health=100;
    public bool attacking;
    public bool playerFound = false;
    public Vector2 rangeOffsetLeft;
    public float chaseRange;

    public GameObject healthPanel;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        healthPanel = GameObject.FindGameObjectWithTag("Boss1HPPanel");
        healthBar = GameObject.FindGameObjectWithTag("Boss1HP").GetComponent<Image>();
        healthPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInChaseRange();
        if (playerFound && health > 0)
        {
            int combo = Random.Range(1, 3);

            Cooldown();
            if (!cooling)
            {
                if (combo == 1)
                {
                    timer = initTimer;
                    _animator.SetTrigger("Combo1");
                }
                else if (combo == 2)
                {
                    timer = initTimer;

                    _animator.SetTrigger("Combo2");

                }

            }
        }
        else if(health <= 0)
        {
            StartCoroutine(DieRoutine());
        }
       
       
    }

    private void CheckInChaseRange()
    {
        RaycastHit2D range;


        range = Physics2D.Raycast((Vector2)transform.position + rangeOffsetLeft, Vector2.left, chaseRange);
        Debug.DrawRay((Vector2)transform.position + rangeOffsetLeft, Vector2.left * chaseRange, Color.green);

        

        if (range.collider != null && !range.collider.isTrigger)
        {
            if (range.collider.gameObject.CompareTag("Player"))
            {
                playerFound = true;
                _player = range.collider.gameObject.GetComponent<PlayerController>();
                healthPanel.SetActive(true);
                healthBar.fillAmount = (float)health / 100;
            }
        }
    }

    private void Cooldown()
    {
        cooling = true;
        attacking = false;
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling)
        {
            cooling = false;
            attacking = true;
        }
    }

    public void TakeSwordHit()
    {
        health -= 5;
        _animator.SetTrigger("Hit");
        healthBar.fillAmount = (float)health / 100;
    }
    public void TakePunchHit()
    {

        health -= 3;
        _animator.SetTrigger("Hit");
        healthBar.fillAmount = (float)health / 100;

    }

    public IEnumerator DieRoutine()
    {
        
        _animator.SetTrigger("Death");       
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
