using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NeedsHelpWillStateControl : MonoBehaviour
{
    public Animator animator;
    int isWavingHash;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        isWavingHash = Animator.StringToHash("IsWaving");
        animator.SetBool(isWavingHash, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update is called once per frame
    public void StopWaving()
    {
        animator.SetBool(isWavingHash, false);
    }

}
