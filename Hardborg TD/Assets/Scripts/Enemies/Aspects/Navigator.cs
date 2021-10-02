using UnityEngine;
using UnityEngine.Events;
using Pathfinding;

public class Navigator : UnitComponent
{
    public Transform TargetTransform;

    public Transform MyTransform;

    private Seeker MySeeker;

    public Path MyPath;

    public float BaseSpeed = 2;
    public float CurrentSpeed = 2;

    public float NextWaypointDistanceSq = 9;

    private int CurrentWaypoint = 0;

    public bool ReachedEndOfPath;
    public bool CanMove = false;

    public UnityEvent UE_ReachedDestination;
    public UnityEvent UE_PathComplete;
    public UnityEvent UE_UpdateMoveSpeed;

    public void Start()
    {
        MySeeker = GetComponent<Seeker>();
        CurrentSpeed = BaseSpeed;
    }

    public void SetTarget(Transform target)
    {
        TargetTransform = target;
        MySeeker.StartPath(transform.position, TargetTransform.position, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            MyPath = p;
            CurrentWaypoint = 0;
            UE_PathComplete.Invoke();
            for (int i = 0; i < MyPath.vectorPath.Count; i ++)
            {
                MyPath.vectorPath[i] = new Vector3(Mathf.Round(MyPath.vectorPath[i].x - 0.5f) + 0.5f, MyPath.vectorPath[i].y, Mathf.Round(MyPath.vectorPath[i].z + 0.5f) - 0.5f);
            }
        }
    }

    public void Update()
    {
        if (MyPath == null || CanMove == false)
        {
            return;
        }

        ReachedEndOfPath = false;
        float sqDistanceToWaypoint;
        while (true)
        {
            Vector3 delta = MyPath.vectorPath[CurrentWaypoint] - transform.position;
            Vector2 dxz = new Vector2(delta.x, delta.z);
            sqDistanceToWaypoint = dxz.sqrMagnitude;
            if (sqDistanceToWaypoint < NextWaypointDistanceSq)
            {
                if (CurrentWaypoint + 1 < MyPath.vectorPath.Count)
                {
                    CurrentWaypoint++;
                }
                else
                {
                    ReachedEndOfPath = true;
                    UE_ReachedDestination.Invoke();
                    break;
                }
            }
            else
            {
                break;
            }
        }
        Vector3 dir = (MyPath.vectorPath[CurrentWaypoint] - transform.position).normalized;

        Vector3 velocity = dir * CurrentSpeed * Time.deltaTime;
        //TAG:TODO : Counter overshooting
        MyTransform.position += velocity;
    }

    public void UpdateSpeed()
    {
        CurrentSpeed = BaseSpeed;
        UE_UpdateMoveSpeed.Invoke();
    }
}