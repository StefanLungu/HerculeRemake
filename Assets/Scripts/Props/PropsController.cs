using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;
    public int smashed = 0;
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();

    }

    void Update()
    {
        if(smashed >= 3)
        {
            StartCoroutine(DestroyRutine());
        }
    }

    public void IsSmashed()
    {
        _animator.SetTrigger("smashed");
        smashed++;
    }

    public IEnumerator DestroyRutine()
    {
        
        _animator.SetTrigger("destroy");
        Destroy(gameObject.GetComponent<CapsuleCollider2D>());
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
