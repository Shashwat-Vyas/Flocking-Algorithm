using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : Movement
{
    public Movement[] movements;
    public float[] weights;

    public override Vector2 CalcMove(FlockAgent boid, List<Transform> nearbyObj, Flock flock)
    {
        if(weights.Length!= movements.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        Vector2 move = Vector2.zero;

        for (int i = 0; i < movements.Length; i++)
        {
            Vector2 partialMove = movements[i].CalcMove(boid, nearbyObj, flock) * weights[i];

            if(partialMove != Vector2.zero)
            {
                if(partialMove.sqrMagnitude > weights[i]*weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }

        return move;
    }
}
