using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Collider : MonoBehaviour
{
    public InitBoids IB;
    public List<GameObject> inRange;

   // private int maxStreeing = 10;
    private int maxSpeed = 20;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.tag == "Boid")// && gameObject.GetComponent<Renderer>().material.color == other.gameObject.GetComponent<Renderer>().material.color)
        {
            inRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(UnityEngine.Collider other)
    {
        if (other.gameObject.tag == "Boid")
        {
            inRange.Remove(other.gameObject);
        }
    }

    private void Alignment()
    {
        gameObject.GetComponent<SphereCollider>().radius = 10;

        int alignTotal = 0;
        Vector3 alignStreeing = Vector3.zero;

        for (int i = 0; i < inRange.Count; i++)
        {
            alignStreeing = alignStreeing + inRange[i].GetComponent<Rigidbody>().velocity;
            alignTotal++;
        }

        if (alignTotal > 0)
        {
            alignStreeing = alignStreeing / alignTotal;
            alignStreeing = alignStreeing - gameObject.GetComponent<Rigidbody>().velocity;

            //if (alignStreeing.x > maxStreeing)
            //    alignStreeing.x = maxStreeing;
            //else if (alignStreeing.x < -maxStreeing)
            //    alignStreeing.x = -maxStreeing;

            //if (alignStreeing.z > maxStreeing)
            //    alignStreeing.z = maxStreeing;
            //else if (alignStreeing.z < -maxStreeing)
            //    alignStreeing.z = -maxStreeing;

            alignStreeing.y = 0;

            // alignStreeing = alignStreeing * 2;

            if (gameObject.GetComponent<Rigidbody>().velocity.x >= maxSpeed || gameObject.GetComponent<Rigidbody>().velocity.z >= maxSpeed)
            {
                gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
            }
            else if (gameObject.GetComponent<Rigidbody>().velocity.x <= -maxSpeed || gameObject.GetComponent<Rigidbody>().velocity.z <= -maxSpeed)
            {
                gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x * 2, 0,
                                                                            gameObject.GetComponent<Rigidbody>().velocity.z * 2);
            }
            alignStreeing = alignStreeing * 2;
        }
        IB.alignForce = alignStreeing;
    }

    private void Cohesion()
    {
        // gameObject.GetComponent<SphereCollider>().radius = 1.5f;
        int cohesionTotal = 0;
        Vector3 cohesionStreeing = Vector3.zero;

        for (int i = 0; i < inRange.Count; i++)
        {
            cohesionStreeing = cohesionStreeing + inRange[i].transform.position;
            cohesionTotal++;
        }

        if (cohesionTotal > 0)
        {
            cohesionStreeing = cohesionStreeing / cohesionTotal;
            cohesionStreeing = cohesionStreeing - gameObject.transform.position;
            cohesionStreeing.y = 0;

            cohesionStreeing = cohesionStreeing / 25;
        }
        IB.cohesioForce = cohesionStreeing;
    }

    private void Seperation()
    {
        //gameObject.GetComponent<SphereCollider>().radius = 1.5f;

        int seperationTotal = 0;
        Vector3 seperationStreeing = Vector3.zero;

        for (int i = 0; i < inRange.Count; i++)
        {
            //seperationStreeing = seperationStreeing + inRange[i].transform.position;

            var diff = gameObject.transform.position - inRange[i].transform.position;
            var d = Vector3.Distance(gameObject.transform.position, inRange[i].transform.position);

            diff = diff / d;
            //print(diff);

            seperationStreeing = seperationStreeing + diff;
            seperationTotal++;

            if (Vector3.Distance(gameObject.transform.position, inRange[i].transform.position) <= 3)
            {
                //gameObject.GetComponent<Rigidbody>().AddForce(gameObject.GetComponent<Rigidbody>().velocity * -1);
            }
        }

        if (seperationTotal > 0)
        {
            seperationStreeing = seperationStreeing / seperationTotal;
            seperationStreeing = seperationStreeing - gameObject.transform.position;
            seperationStreeing.y = 0;

            seperationStreeing = IB.cohesioForce * (-1.5f);
        }
        IB.sepForce = seperationStreeing;
        // gameObject.GetComponent<Rigidbody>().velocity = seperationStreeing;

        // print(IB.cohesioForce - seperationStreeing);
    }

    private void Update()
    {
        //if (gameObject.GetComponent<Rigidbody>().velocity.x > maxSpeed || gameObject.GetComponent<Rigidbody>().velocity.z > maxSpeed)
        //{
        //    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x / 100, 0,
        //                                                                gameObject.GetComponent<Rigidbody>().velocity.z / 100);

        //}

        if (gameObject.transform.position.x >= 500)
            gameObject.transform.position = new Vector3(-400, 25, gameObject.transform.position.z);
        //gameObject.GetComponent<Rigidbody>().AddForce(Vector3.Reflect(gameObject.transform.position, Vector3.right) * 5);
        //gameObject.GetComponent<Rigidbody>().velocity = (gameObject.GetComponent<Rigidbody>().velocity * -1);
        else if (gameObject.transform.position.x <= -500)
            gameObject.transform.position = new Vector3(400, 25, gameObject.transform.position.z);
        //gameObject.GetComponent<Rigidbody>().AddForce(Vector3.Reflect(gameObject.transform.position, Vector3.left) * 5);
        // gameObject.GetComponent<Rigidbody>().AddForce(gameObject.GetComponent<Rigidbody>().velocity * -100);
        // gameObject.GetComponent<Rigidbody>().velocity = (gameObject.GetComponent<Rigidbody>().velocity * -1);

        if (gameObject.transform.position.z >= 300)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 25, -220);
        // gameObject.GetComponent<Rigidbody>().AddForce(gameObject.GetComponent<Rigidbody>().velocity * -100);
        // gameObject.GetComponent<Rigidbody>().velocity = (gameObject.GetComponent<Rigidbody>().velocity * -1);
        else if (gameObject.transform.position.z <= -300)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 25, 220);
        // gameObject.GetComponent<Rigidbody>().AddForce(gameObject.GetComponent<Rigidbody>().velocity * -100);
        //gameObject.GetComponent<Rigidbody>().velocity = (gameObject.GetComponent<Rigidbody>().velocity * -1);

        gameObject.GetComponent<SphereCollider>().radius = 10;
        Alignment();
        gameObject.GetComponent<SphereCollider>().radius = 1.5f;
        Cohesion();
        Seperation();

        gameObject.GetComponent<Rigidbody>().AddForce(IB.alignForce + IB.sepForce);//+ IB.cohesioForce 
    }
}

