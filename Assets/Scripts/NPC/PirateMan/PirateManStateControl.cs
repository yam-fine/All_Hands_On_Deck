using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PirateManStateControl : MonoBehaviour
{
    public Animator animator;
    int isWalkingHash;
    int isTalkingHash;
    int isWorkingHash;
    int isLaughingHash;
    int isCPRingHash;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
        isTalkingHash = Animator.StringToHash("IsTalking");
        isWorkingHash = Animator.StringToHash("IsWorking");
        isLaughingHash = Animator.StringToHash("IsLaughing");

        isCPRingHash = Animator.StringToHash("IsCPRing");

        animator.SetBool(isLaughingHash, true);

        // StartCPR();
        // animator.SetBool(isCPRingHash, true);
    }

    public void StartCPR() {
        animator.SetBool(isCPRingHash, true);
    }

}
