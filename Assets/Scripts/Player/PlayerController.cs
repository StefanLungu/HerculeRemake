using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int side;
    public bool facingRight;
    public float fallMultiplyer;
    public float lowjumpMultiplyer;
    private LevelManager _lvlMng;
    public bool isCrouched;
 
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Collision _collision;
    
    public int health = 5;
    public bool smashed;
    public bool _chestFound = false;
    public ChestController _chest;

    private void Start()
    {
        _lvlMng = GameObject.FindGameObjectWithTag("LVLMNG").GetComponent<LevelManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collision = GetComponent<Collision>();
        facingRight = true;
        smashed = false;
        side = 1;
        transform.position = _lvlMng.lastCheckPointPos;

    }
    // Update is called once per frame
    void Update()
    {        
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplyer * Time.deltaTime;
        }

        if (_rigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
        {

            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * lowjumpMultiplyer * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && _collision.onGround && !isCrouched && health > 0)
        {
            Jump();
        }

        if (Input.GetButton("Crouch") && _collision.onGround && health > 0)
        {
            _animator.SetFloat("Horizontal", 0);
            _animator.SetBool("isCrouch", true);
            isCrouched = true;

        }

        if (Input.GetButtonUp("Crouch") && health > 0)
        {
            _animator.SetBool("isCrouch", false);
            _animator.SetBool("smash", false);
            isCrouched = false;
        }

        if (_chestFound)
        {
            if (Input.GetButtonDown("Interact") && health > 0)
            {
                _chest.OpenChest();
                UseItem(_chest.item, _chest.itemType);
            }
        }


        if (health <= 0)
        {
            StartCoroutine(DieRoutine());
        }

    }

    void FixedUpdate()
    {
        _animator.SetBool("onGround", _collision.onGround);

        float x = Input.GetAxis("Horizontal");

        if (!isCrouched && health > 0) 
        { 
            _animator.SetFloat("Horizontal", x);
            Run(x);
        }
        

        if (facingRight == false && x > 0)
        {
            Flip();

        }
        else if (facingRight == true && x < 0)
        {

            Flip();

        }

        if(_collision.onBridge)
        {
            if (Input.GetButtonDown("Crouch") && !_collision.onGround && health > 0)
            {
                SmashGround();
            }

            if(_collision.onGround && smashed == true)
            {
                _collision.checkBridge.GetComponent<PropsController>().IsSmashed();
                smashed = false;
            }
        }

        if (!_collision.onGround)
        {
            if (_collision.isTouchingLedgeRight && x > 0 && facingRight)
            {
                GrabLedge();
                ClimbLedge();

            }
            else if (_collision.isTouchingLedgeLeft && x < 0 && !facingRight)
            {
                GrabLedge();
                ClimbLedge();
            }
            else
            {
                _rigidbody.gravityScale = 1;
                _collision.isGrabbed = false;
                _animator.SetBool("isClimbing", false);
            }
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void Run(float x)
    {
        if (facingRight)
        {
            if (!_collision.wallRight)
            {

                _rigidbody.velocity = new Vector2(x * speed, _rigidbody.velocity.y);
            }
        }
        else
        {
            if (!_collision.wallLeft)
            {


                _rigidbody.velocity = new Vector2(x * speed, _rigidbody.velocity.y);
            }
        }
        
    }

    public void Jump()
    {
        _rigidbody.velocity = Vector2.up * jumpForce;
    }

    public void GrabLedge()
    {
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _collision.isGrabbed = true;
        _collision.onGround = true;
    }

    public void ClimbLedge()
    {
        _collision.isGrabbed = false;
        _rigidbody.velocity = Vector2.up * jumpForce / 2;
        _animator.SetBool("isClimbing", true);
    }
    public void SmashGround()
    {
        _animator.SetFloat("Horizontal", 0);
        _animator.SetBool("smash", true);
        isCrouched = true;

        smashed = true;
    }

    public void TakeHit()
    {
        health--;
        _animator.SetTrigger("Hit");      
    }

    public void Death()
    {
        health = 0;
    }
    public IEnumerator DieRoutine()
    {
        _animator.SetTrigger("Death");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        LevelManager.instance.deaths++;
        Debug.Log(LevelManager.instance.deaths);
        LevelManager.instance.ReloadScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chest")
        {
            _chest = collision.gameObject.GetComponent<ChestController>();
            _chestFound = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _chest = null;
        _chestFound = false;
    }

    void UseItem(string item, string itemType)
    {

        if(itemType == "letter")
        {
            StartCoroutine(Letters.instance.LetterFound(item));
        }

        if(itemType == "health")
        {
            health += 1;
        }
    }
}
