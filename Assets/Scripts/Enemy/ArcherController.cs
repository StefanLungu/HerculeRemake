using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    public GameObject leftChaseLimit;
    public GameObject rightChaseLimit;
    public GameObject arrow;
    public float speed;
    public Vector2 rangeOffsetRight;
    public Vector2 rangeOffsetLeft;
    public float chaseRange;

    private Vector2 initPosition;
    private Animator _animator;
    private BoxCollider2D _triggerArea;
    public bool targetFound;
    public GameObject _target;
    public bool facingRight;
    public bool isInRange = false;
    public float attackRange;
    public float initTimer;
    private float timer;
    private bool cooling;
    public int hits = 0;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = gameObject.transform.position;
        _animator = gameObject.GetComponent<Animator>();
        _triggerArea = gameObject.GetComponent<BoxCollider2D>();
        arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hits < 6)
        {
            if (targetFound && InsideBoundries())
            {
                if (!isInRange)
                {
                    Chase();
                    _animator.SetInteger("AnimState", 2);
                }
                else
                {
                    if (_target == null)
                    {
                        targetFound = false;
                        isInRange = false;
                    }

                    _animator.SetInteger("AnimState", 0);
                    Attack();
                }
            }
            else
            {
                targetFound = false;
                isInRange = false;
                _target = null;
                GoBack();

                if (initPosition == (Vector2)transform.position)
                {
                    _animator.SetInteger("AnimState", 0);
                }
            }
        }

        if (hits >= 6)
        {
            StartCoroutine(DieRoutine());
        }


    }

    void FixedUpdate()
    {
        CheckInChaseRange();
    }

    private void Chase()
    {

        Vector2 tgPosition = new Vector2(_target.transform.position.x, transform.position.y);

        if (tgPosition.x <= transform.position.x)
        {
            if (facingRight)
            {
                Flip();
            }
        }
        else
        {
            if (!facingRight)
            {
                Flip();
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, tgPosition, speed * Time.deltaTime);

        CheckAttackRange();
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private bool InsideBoundries()
    {
        if (transform.position.x >= leftChaseLimit.transform.position.x && transform.position.x <= rightChaseLimit.transform.position.x)
        {
            return true;
        }

        return false;
    }

    private void CheckInChaseRange()
    {
        RaycastHit2D range;


        if (facingRight)
        {
            range = Physics2D.Raycast((Vector2)transform.position + rangeOffsetRight, Vector2.right, chaseRange);
            Debug.DrawRay((Vector2)transform.position + rangeOffsetRight, Vector2.right * chaseRange, Color.green);

        }
        else
        {

            range = Physics2D.Raycast((Vector2)transform.position + rangeOffsetLeft, Vector2.left, chaseRange);
            Debug.DrawRay((Vector2)transform.position + rangeOffsetLeft, Vector2.left * chaseRange, Color.green);

        }

        if (range.collider != null)
        {
            if (range.collider.gameObject.CompareTag("Player"))
            {
                targetFound = true;
                _target = range.collider.gameObject;

            }
        }

        if (_target != null)
        {

            if (Vector2.Distance(transform.position, _target.transform.position) > (chaseRange + 1))
            {
                targetFound = false;
                _target = null;
            }

        }

    }

    private void CheckAttackRange()
    {
        RaycastHit2D range;

        if (facingRight)
        {
            range = Physics2D.Raycast((Vector2)transform.position + rangeOffsetRight, Vector2.right, attackRange);
            Debug.DrawRay((Vector2)transform.position + rangeOffsetRight, Vector2.right * attackRange, Color.red);

        }
        else
        {
            range = Physics2D.Raycast((Vector2)transform.position + rangeOffsetLeft, Vector2.left, attackRange);
            Debug.DrawRay((Vector2)transform.position + rangeOffsetLeft, Vector2.left * attackRange, Color.red);

        }

        if (range.collider != null)
        {
            if (range.collider.gameObject.CompareTag("Player"))
            {
                isInRange = true;
            }
        }
    }

    private void GoBack()
    {
        _animator.SetInteger("AnimState", 2);
        if (initPosition.x < transform.position.x)
        {
            if (facingRight)
            {
                Flip();
            }
        }
        else if (initPosition.x > transform.position.x)
        {
            if (!facingRight)
            {
                Flip();
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, initPosition, speed * Time.deltaTime);
    }

    private void Attack()
    {
        if (_target != null)
        {
            if (Vector2.Distance(transform.position, _target.transform.position) > (attackRange + 1))
            {
                isInRange = false;
            }

        }

        Cooldown();

        if (!cooling)
        {
            timer = initTimer;
            _animator.SetInteger("AnimState", 0);

            if (facingRight)
            {
                _animator.SetTrigger("AttackRight");
            }
            else
            {
                _animator.SetTrigger("AttackLeft");
            }

            //_target.GetComponent<PlayerController>().TakeHit();
        }

        
    }

    private void Cooldown()
    {
        
        cooling = true;
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling)
        {
            arrow.SetActive(true);
            cooling = false;
        }
    }

   
    public void TakeSwordHit()
    {
        hits += 2;
        _animator.SetTrigger("Hurt");
    }

    public void TakePunch()
    {
        hits++;
        _animator.SetTrigger("Hurt");
    }

    public IEnumerator DieRoutine()
    {
        Destroy(gameObject.GetComponent<CapsuleCollider2D>());
        Destroy(arrow);
        _animator.SetTrigger("Death");
        yield return new WaitForSeconds(3);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
