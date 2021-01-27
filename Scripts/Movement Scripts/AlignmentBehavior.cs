using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredMovement
{
    public override Vector2 CalcMove(FlockAgent boid, List<Transform> nearbyObj, Flock flock)
    {
        if (nearbyObj.Count == 0)
            return boid.transform.up;

        Vector2 alignmentMove = Vector2.zero;

        List<Transform> filteredContext = (filter == null) ? nearbyObj : filter.Filter(boid, nearbyObj);

        foreach (Transform item in filteredContext)
        {
            alignmentMove += (Vector2)item.transform.up;
        }
        alignmentMove /= nearbyObj.Count;
        
        return alignmentMove;
    }
}
