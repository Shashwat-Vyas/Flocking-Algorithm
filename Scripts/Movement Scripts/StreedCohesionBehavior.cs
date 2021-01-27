using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/StreedCohesion")]
public class StreedCohesionBehavior : FilteredMovement
{
    Vector2 currentVelocity;
    public float agentsSmoothrime = 0.5f;

    public override Vector2 CalcMove(FlockAgent boid, List<Transform> nearbyObj, Flock flock)
    {
        if (nearbyObj.Count == 0)
            return Vector2.zero;

        Vector2 stcohesionMove = Vector2.zero;

        List<Transform> filteredContext = (filter == null) ? nearbyObj : filter.Filter(boid, nearbyObj);

        foreach (Transform item in filteredContext)
        {
            stcohesionMove += (Vector2)item.position;
        }
        stcohesionMove = stcohesionMove / nearbyObj.Count;

        stcohesionMove -= (Vector2)boid.transform.position;
        stcohesionMove = Vector2.SmoothDamp(boid.transform.up, stcohesionMove, ref currentVelocity, agentsSmoothrime);

        return stcohesionMove;
    }

}
