using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TalkingWillStateControl : MonoBehaviour
{
    public Animator animator;
    int isWalkingHash;
    int isTalkingHash;
    int isWorkingHash;
    int isLaughingHash;
    int isGettingCPRHash;

    public Transform cpredBy;
    public Transform transform;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
        isTalkingHash = Animator.StringToHash("IsTalking");
        isWorkingHash = Animator.StringToHash("IsWorking");
        isLaughingHash = Animator.StringToHash("IsLaughing");

        isGettingCPRHash = Animator.StringToHash("GettingCPR");

        animator.SetBool(isTalkingHash, true);
        
        // animator.SetBool(isGettingCPRHash, true);
        // StartGettingCPR();
    }

    public void StartGettingCPR() {
        animator.SetBool(isGettingCPRHash, true);
        float yRotation = 180f;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
        transform.position = new Vector3(cpredBy.position.x+0.3f, cpredBy.position.y+0.1, cpredBy.position.z-0.5f);
    }

}
