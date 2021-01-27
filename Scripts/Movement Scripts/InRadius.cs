using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/In Radius")]
public class InRadius : Movement
{
    public Vector2 center;
    public float radius = 15f;

    public override Vector2 CalcMove(FlockAgent boid, List<Transform> nearbyObj, Flock flock)
    {
        Vector2 centerOffset = center - (Vector2)boid.transform.position;
        float t = centerOffset.magnitude / radius;

        if(t<0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * t * t;
    }

}
