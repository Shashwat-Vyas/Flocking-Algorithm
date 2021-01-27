using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitBoids : MonoBehaviour
{
    public Vector3 alignForce;
    public Vector3 cohesioForce;
    public Vector3 sepForce;

    #region Public Fields
    public GameObject prefab;
    public GameObject boids;
    //public GameObject GM;
    public List<GameObject> BoidsList;
    public MovmentBoid MB;
    public int boidCount = 100;
    public bool allOut = false;
    #endregion

    private bool _bool = true;
    private InitBoids ib;

    public InitBoids()
    {
        alignForce = Vector3.zero;
        cohesioForce = Vector3.zero;
        sepForce = Vector3.zero;
    }

    private void Start()
    {
        for (int i = 0; i < boidCount; i++)
        {
            var boid = Instantiate(prefab, new Vector3(Random.Range(-300, 300), 25, Random.Range(-150, 150)), Quaternion.identity, boids.transform);
            // boid.AddComponent<MovmentBoid>();

            boid.GetComponent<Collider>().IB = gameObject.GetComponent<InitBoids>();
            BoidsList.Add(boid);
            //boid.GetComponent<Renderer>().material.color = new Color(Random.Range(0.25f, 0.75f), 
            //                                                         Random.Range(0.25f, 0.75f), 
            //                                                         Random.Range(0.25f, 0.75f));

            if (_bool)
            {
                _bool = !_bool;
            }
            
        }
        //StartCoroutine("initBoids");
        allOut = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Mian");
        }
    }
}
