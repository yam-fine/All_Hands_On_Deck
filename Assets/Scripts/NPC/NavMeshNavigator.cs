using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshNavigator : MonoBehaviour
{
    [SerializeField] private Transform target;

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = target.position;
    }

    public void SetTarget(Transform transform) {
        target = transform;
    }

    private void setNavMeshStatus(bool status) {
        navMeshAgent.enabled = status;
    }

    public void StopNavMeshAgent() {
        setNavMeshStatus(false);
    }

    public void StartNavMesh() {
        setNavMeshStatus(true);
    }
}
