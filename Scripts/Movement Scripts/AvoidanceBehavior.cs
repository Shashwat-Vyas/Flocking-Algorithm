using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredMovement
{
    public override Vector2 CalcMove(FlockAgent boid, List<Transform> nearbyObj, Flock flock)
    {
        if (nearbyObj.Count == 0)
            return Vector2.zero;

        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? nearbyObj : filter.Filter(boid, nearbyObj);

        foreach (Transform item in filteredContext)
        {
            if(Vector2.SqrMagnitude(item.position - boid.transform.position) < flock.SuqareAdoivRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(boid.transform.position - item.position);
            }
        }

        if (nAvoid > 0)
            avoidanceMove /= nAvoid;

        return avoidanceMove;
    }
}
