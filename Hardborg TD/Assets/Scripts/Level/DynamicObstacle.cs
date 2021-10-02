using UnityEngine;
using Pathfinding;

public class DynamicObstacle : MonoBehaviour
{
    public Collider NavCollider;

    void Start()
    {
        Bounds bounds = NavCollider.bounds;
        AstarPath.active.UpdateGraphs(bounds);
    }
}
