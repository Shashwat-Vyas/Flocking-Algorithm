using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class Flock : MonoBehaviour
{
    public int boidCount = 250;
    const float density = 0.08f;
  
    public float nearRadius = 1.5f;

    public FlockAgent prefab;
    public GameObject boids;
    public GameObject other;
    public GameObject mouseColl;

    public List<FlockAgent> BoidsList;

    public Movement move;
    //public Movement behavior;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidRadiusMulti = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SuqareAdoivRadius { get { return squareAvoidanceRadius; } }


    private void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidRadiusMulti * avoidRadiusMulti;

        for (int i = 0; i < boidCount; i++)
        {
            FlockAgent boid = Instantiate(prefab, Random.insideUnitCircle * boidCount * density, 
                                            Quaternion.Euler(Vector3.forward * Random.Range(0, 360)),
                                            boids.transform);
            boid.name = "Boid " + i;

            boid.init(this);
            BoidsList.Add(boid);
          
        }
    }

    private void Update()
    {
        foreach (FlockAgent boid in BoidsList)
        {
            List<Transform> nearbyObj = GetNearbyObj(boid);
            //boid.GetComponentInChildren<SpriteRenderer>().material.color = Color.Lerp(Color.cyan, Color.red, nearbyObj.Count / 6f);

            Vector2 movement = move.CalcMove(boid, nearbyObj, this);
            movement *= driveFactor;

            if (movement.sqrMagnitude > squareMaxSpeed)
            {
                movement = movement.normalized * maxSpeed;
            }
            boid.Move(movement);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Mian");
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            other.SetActive(true);
        }
    }

    List<Transform> GetNearbyObj(FlockAgent boid)
    {
        List<Transform> objs = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(boid.transform.position, nearRadius);

        foreach(Collider2D c in contextColliders)
        {
            if(c!= boid.GetComponent<SphereCollider>())
            {
                objs.Add(c.transform);
            }
        }
        return objs;
    }
}
