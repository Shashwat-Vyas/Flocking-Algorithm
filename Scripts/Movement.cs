using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : ScriptableObject
{
    public abstract Vector2 CalcMove(FlockAgent boid, List<Transform> nearbyObj, Flock flock );
  
}
