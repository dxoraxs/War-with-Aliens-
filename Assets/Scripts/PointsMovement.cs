using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsMovement : MonoBehaviour
{
    private List<Vector3> points = new List<Vector3>();

    public Vector3 GetPoint(int id)
    {
        if (id < points.Count)
            return points[id];
        return new Vector3();
    }
    
    private void Awake()
    {
        foreach (Transform point in transform)
        {
            points.Add(point.position);
        }
    }
}
