using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent boid, List<Transform> orignals)
    {
        List<Transform> filtered = new List<Transform>();

        foreach(Transform item in orignals)
        {
            FlockAgent itemAgent = item.GetComponent<FlockAgent>();

            if(itemAgent!=null && itemAgent.AgentFlock == boid.AgentFlock)
            {
                filtered.Add(item);
            }
        }

        return filtered;
    }
}
