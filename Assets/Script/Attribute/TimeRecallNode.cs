using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRecallNode
{
    private TimeRecallNode(Vector3 position, Vector3 eularAngles, Vector3 velocity)
    {
        this.position = position;
        this.eularAngles = eularAngles;
        this.velocity = velocity;
    }

    public Vector3 position { private set; get; }
    public Vector3 eularAngles { private set; get; }
    public Vector3 velocity { private set; get; }

    public static TimeRecallNode CreateNode(Vector3 position, Vector3 eularAngles, Vector3 velocity)
    {
        return new TimeRecallNode(position, eularAngles, velocity);
    }
}
