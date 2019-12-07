using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private AudioSource collect;
    protected Animator animator;
    [SerializeField] private bool isCerry;
    // Start is called before the first frame update
    void Start()
    {
        collect = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public bool Getting()
    {
        animator.SetTrigger("Collected");
        collect.Play();
        return isCerry;
    }
    private void Collected()
    {
        Destroy(this.gameObject);
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
