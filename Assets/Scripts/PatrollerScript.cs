using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerScript : MonoBehaviour
{
    public List<WaypointScript> Waypoints = new List<WaypointScript>();
    public float Speed = 1.0f;
    public int DestinationWaypoint = 1;

    private Vector3 Destination;
    private bool Forwards = true;
    private float TimePassed = 0f;

    public PlayerController Player;

    public bool IsChasingPlayer = false;
    public bool HasCaughtPlayer = false;

    Vector3 defaultPos;

    // Start is called before the first frame update
    void Start()
    {
        this.Destination = this.Waypoints[DestinationWaypoint].transform.position;
        defaultPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // first check if we have to chase the player by checking the distance
        CheckDistanceToPlayer();

        //if: enemy is near player, then chase player
        if (IsChasingPlayer)
        {
            ChasePlayer();
        }
        //else: enemy is not chasing player, then resume waypoint functions
        else
        {
            StopAllCoroutines();
            StartCoroutine(MoveTo());
        }
    }

    void CheckDistanceToPlayer()
    {
        float dist = Vector3.Distance(Player.transform.position, this.transform.position);
        if (dist < 2)
        {
            IsChasingPlayer = true;
        }
        else
        {
            IsChasingPlayer = false;
        }
    }

    void ChasePlayer()
    {
        Debug.Log("I am chasing the player.");
        // current position of enemy move towards the position of the player
        this.transform.position = Vector3.MoveTowards(this.transform.position, Player.transform.position, .02f);

        //if caught player
        if (HasCaughtPlayer)
        {
            //reset player back to start
            Player.ResetPlayer();
            //reset patroller back to it's original position so it doesn't get stuck to the player
            ResetPatroller();
            //reset caught and chasing player to false
            HasCaughtPlayer = false;
            IsChasingPlayer = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            HasCaughtPlayer = true;
            Debug.Log("Patroller has caught player");
        }
    }

    void ResetPatroller()
    {
        this.gameObject.transform.position = defaultPos;
    }

    IEnumerator MoveTo()
    {
        while ((transform.position - this.Destination).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                this.Destination, this.Speed * Time.deltaTime);
            yield return null;
        }

        if ((transform.position - this.Destination).sqrMagnitude <= 0.01f)
        {
            if (this.Waypoints[DestinationWaypoint].IsSentry)
            {
                while(this.TimePassed < this.Waypoints[DestinationWaypoint].PauseTime)
                {
                    this.TimePassed += Time.deltaTime;
                    yield return null;
                }

                this.TimePassed = 0;
            }
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (this.Waypoints[DestinationWaypoint].IsEndpoint)
        {
            if (this.Forwards)
                this.Forwards = false;
            else
                this.Forwards = true;
        }

        if (this.Forwards)
        {
            ++DestinationWaypoint;
        }
        else
        {
            --DestinationWaypoint;
        }

        if (DestinationWaypoint >= this.Waypoints.Count)
            DestinationWaypoint = 0;

        this.Destination = this.Waypoints[DestinationWaypoint].transform.position;
    }

}
