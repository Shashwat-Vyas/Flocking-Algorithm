using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredMovement
{
    public override Vector2 CalcMove(FlockAgent boid, List<Transform> nearbyObj, Flock flock)
    {
        if (nearbyObj.Count == 0)
            return Vector2.zero;

        Vector2 cohesionMove = Vector2.zero;

        List<Transform> filteredContext = (filter == null) ? nearbyObj : filter.Filter(boid, nearbyObj);

        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove = cohesionMove / nearbyObj.Count;

        cohesionMove -= (Vector2)boid.transform.position;
        return cohesionMove;
    }

}
