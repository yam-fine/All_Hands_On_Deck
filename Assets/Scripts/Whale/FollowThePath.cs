using UnityEngine;

public class FollowThePath : MonoBehaviour {

    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] patrolWaypoints;

    [SerializeField]
    private Transform[] colideWaypoints;

    public bool shouldColide = false;

    // Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed = 2f;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    public bool colidedTarget = false;
    private bool attacked = false;

	// Use this for initialization
	private void Start () {

        // Set position of Enemy as position of the first waypoint
        transform.position = patrolWaypoints[waypointIndex].transform.position;
	}
	
	// Update is called once per frame
	private void Update () {

        // Move Enemy
        if(shouldColide) {
            Move(colideWaypoints);
        }else {
            Move(patrolWaypoints);
        }
        
	}

    // Method that actually make Enemy walk
    private void Move(Transform[] waypoints)
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector3.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            var _direction = (waypoints[waypointIndex].transform.position - transform.position);
            _direction = Vector3.RotateTowards(transform.forward, -_direction, 0.1f*moveSpeed * Time.deltaTime, 0);

            //create the rotation we need to be in to look at the target
            transform.rotation = Quaternion.LookRotation(_direction);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
            if (shouldColide) {
                attacked = true;
            }
        } else if(!shouldColide || !attacked) {
            waypointIndex=0;
        }
    }


    public void Attack() {
        Debug.Log("Attacking");
        shouldColide = true;
        moveSpeed*=2;
    }
}
