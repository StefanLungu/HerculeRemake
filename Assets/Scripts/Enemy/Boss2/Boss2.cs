using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss2 : MonoBehaviour
{
    public GameObject witch;
    public GameObject flame;
    public PlayerController player;
    public float initTimer;
    public float timer;
    public bool cooling;
    public bool playerFound = false;
    public Vector2 rangeOffsetLeft;
    public float chaseRange;
    public GameObject healthPanel;
    public int health = 100;
    public Image healthBar;
    public bool attacking;
    public float distance;
    public float flameSpeed;
    public bool rangeAttack;
    public Vector2 playerPos;
    public Vector2 flameInitPos;
    public bool flameDone;
    public bool playerHit;
    // Start is called before the first frame update
    void Start()
    {
        flameDone = false;
        rangeAttack = false;
        flame.SetActive(false);
        healthPanel = GameObject.FindGameObjectWithTag("Boss1HPPanel");
        healthBar = GameObject.FindGameObjectWithTag("Boss1HP").GetComponent<Image>();
        healthPanel.SetActive(false);
        flameInitPos = flame.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInChaseRange();
        if (playerFound && health>0)
        {
            Cooldown();
            if (!cooling)
            {
                if (isClose())
                {
                    timer = initTimer;
                    witch.GetComponent<Animator>().SetTrigger("Combo2");
                }
                else if (!isClose())
                {
                    timer = initTimer;
                    witch.GetComponent<Animator>().SetTrigger("Combo1");
                    rangeAttack = true;
                    flameDone = false;
                    playerHit = false;
                    playerPos = player.transform.position;
                }
            }

            if(rangeAttack && !flameDone)
            {
                RangeAttack(playerPos);
            }
        }
        else if(health<=0)
        {
            StartCoroutine(DieRoutine());
        }
    }

    private void CheckInChaseRange()
    {
        RaycastHit2D range;


        range = Physics2D.Raycast((Vector2)witch.transform.position + rangeOffsetLeft, Vector2.left, chaseRange);
        Debug.DrawRay((Vector2)witch.transform.position + rangeOffsetLeft, Vector2.left * chaseRange, Color.green);



        if (range.collider != null && !range.collider.isTrigger)
        {
            if (range.collider.gameObject.CompareTag("Player"))
            {
                playerFound = true;
                player = range.collider.gameObject.GetComponent<PlayerController>();
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
        witch.GetComponent<Animator>().SetTrigger("Hit");
        healthBar.fillAmount = (float)health / 100;
    }
    public void TakePunchHit()
    {

        health -= 3;
        witch.GetComponent<Animator>().SetTrigger("Hit");
        healthBar.fillAmount = (float)health / 100;

    }

    private bool isClose()
    {
        if ((distance = Vector2.Distance(witch.transform.position,player.transform.position)) <= 1)
        {
            return true;
        }

        return false;
    }

    private void RangeAttack(Vector2 playerPos)
    {
        
        flame.SetActive(true);
        flame.transform.position = Vector2.MoveTowards(flame.transform.position, playerPos, flameSpeed * Time.deltaTime);
        
        if ((Vector2)flame.transform.position == playerPos || playerHit)
        {
            flame.SetActive(false);
            flame.transform.position = flameInitPos;
            flameDone = true;
        }

    }

    public IEnumerator DieRoutine()
    {
        witch.GetComponent<Animator>().SetTrigger("Death");
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
