using UnityEngine;

public class CombatController : MonoBehaviour
{
    private AudioController audioController;
    private PlayerController _player;
    private Animator _animator;
    private string attackType;
    public bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        audioController = gameObject.GetComponent<AudioController>();
        _player = gameObject.GetComponent<PlayerController>();
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Attack"))
        {
            audioController.PlayAttack();
            _animator.SetBool("isAttacking",true);
            attackType = "sword";
            canAttack = true;
        }

        if(Input.GetButtonDown("Punch"))
        {
            _animator.SetBool("isPunching", true);
            attackType = "punch";
            canAttack = true;
        }

        if(Input.GetButtonDown("SuperPunch"))
        {
            _animator.SetTrigger("superPunch");
            attackType = "superPunch";
            canAttack = true;
        }

        if(Input.GetButtonUp("Attack"))
        {
            _animator.SetBool("isAttacking", false);
            audioController.StopAttack();
        }

        if (Input.GetButtonUp("Punch"))
        {
            _animator.SetBool("isPunching", false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            switch (collision.gameObject.tag)
            {
                case "Dummy":
                    DummyCombat(collision.gameObject);
                    break;
                case "Bandit":
                    BanditCombat(collision.gameObject);
                    break;
                case "Stones":
                    SmashStones(collision.gameObject);
                    break;
                case "Archer":
                    ArcherCombat(collision.gameObject);
                    break;
                case "Boss1":
                    Boss1Combat(collision.gameObject);
                    break;
                case "Boss2":
                    Boss2Combat(collision.gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canAttack = false;
    }

    private void DummyCombat(GameObject dummy)
    {
        if(attackType == "sword")
        {
            dummy.GetComponent<DummyController>().takeSwordHit();
        }

        if(attackType == "punch")
        {
            dummy.GetComponent<DummyController>().takePunch();
        }
        
    }

    private void BanditCombat(GameObject bandit)
    {
        if (attackType == "sword")
        {
            bandit.GetComponent<BanditController>().TakeSwordHit();
        }

        if (attackType == "punch")
        {
            bandit.GetComponent<BanditController>().TakePunch();
        }
    }

    private void ArcherCombat(GameObject archer)
    {
        if (attackType == "sword")
        {
            archer.GetComponent<ArcherController>().TakeSwordHit();
        }

        if (attackType == "punch")
        {
            archer.GetComponent<ArcherController>().TakePunch();
        }
    }

    private void SmashStones(GameObject stones)
    {
        if(attackType == "superPunch")
        {
            stones.GetComponent<PropsController>().IsSmashed();
        }
    }

    private void Boss1Combat(GameObject boss1)
    {
        if (attackType == "sword")
        {
            boss1.GetComponent<Lvl1Boss>().TakeSwordHit();
        }

        if (attackType == "punch")
        {
            boss1.GetComponent<Lvl1Boss>().TakePunchHit();
        }
    }

    private void Boss2Combat(GameObject boss2)
    {
        if (attackType == "sword")
        {
            boss2.GetComponentInParent<Boss2>().TakeSwordHit();
        }

        if (attackType == "punch")
        {
            boss2.GetComponentInParent<Boss2>().TakePunchHit();
        }
    }

}
