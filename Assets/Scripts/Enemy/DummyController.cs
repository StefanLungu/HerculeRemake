using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    private Animator _animator;
    public int hits = 0;
    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hits >= 6)
        {
            Destroy(gameObject);
        }
    }

    public void takeSwordHit()
    {
        hits += 2;
        _animator.SetTrigger("takeHit");
        
    }

    public void takePunch()
    {
        hits++;
        _animator.SetTrigger("takeHit");
    }
}
