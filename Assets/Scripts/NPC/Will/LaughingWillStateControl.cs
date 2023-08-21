using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LaughingWillStateControl : MonoBehaviour
{
    public Animator animator;
    int isWalkingHash;
    int isTalkingHash;
    int isWorkingHash;
    int isLaughingHash;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
        isTalkingHash = Animator.StringToHash("IsTalking");
        isWorkingHash = Animator.StringToHash("IsWorking");
        isLaughingHash = Animator.StringToHash("IsLaughing");

        animator.SetBool(isLaughingHash, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
