using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public string item;
    public string itemType;
    public Animator _animator;
    private bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
       
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenChest()
    {
        
        isOpen = true;
        _animator.SetBool("isOpened", isOpen);

    }
}
