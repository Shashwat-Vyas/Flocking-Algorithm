using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentBoid : MonoBehaviour
{
    public InitBoids IB;

    private Vector3 velocity;

    private int maxStreeing = 10;
    private int maxSpeed = 5;

    //private float position;

    private void Start()
    {
        print("Start");
        velocity = gameObject.GetComponent<Rigidbody>().velocity;

        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1.5f, 1.5f),
                                                                    0,
                                                                   Random.Range(-1.5f, 1.5f));

    }

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 25, gameObject.transform.position.z);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x,
                                                                    0,
                                                                    gameObject.GetComponent<Rigidbody>().velocity.z);

        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.zero);

        var velo = gameObject.GetComponent<Rigidbody>().velocity;

        if (gameObject.transform.position.x >= 450)
            gameObject.transform.position = new Vector3(-400, 25, gameObject.transform.position.z);
        else if (gameObject.transform.position.x <= -450)
            gameObject.transform.position = new Vector3(400, 25, gameObject.transform.position.z);

        if (gameObject.transform.position.z >= 220)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 25, -220);
        else if (gameObject.transform.position.z <= -250)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 25, 220);

        gameObject.GetComponent<Rigidbody>().velocity = velo;

        if (IB.BoidsList.Count == IB.boidCount)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(TotslStreeingForce());
            // gameObject.GetComponent<ConstantForce>().force = TotslStreeingForce();
        }


    }

    private Vector3 TotslStreeingForce()
    {
        Vector3 totalStreeing = Vector3.zero;

        Vector3 alignStreeing = Vector3.zero;
        float alignPerception = 500;
        int alignTotal = 0;

        Vector3 cohesionStreeing = Vector3.zero;
        float cohesionPerception = 30;
        int cohesionTotal = 0;

        Vector3 seperationStreeing = Vector3.zero;
        float seperationPerception = 20;
        int seperationTotal = 0;


        for (int i = 0; i < IB.boidCount; i++)
        {
            float d = Vector3.Distance(gameObject.transform.position, IB.BoidsList[i].transform.position);

            if (IB.BoidsList[i] != gameObject && d < alignPerception)
            {
                alignStreeing = alignStreeing + IB.BoidsList[i].GetComponent<Rigidbody>().velocity;
                alignTotal++;
            }

            if (IB.BoidsList[i] != gameObject && d < cohesionPerception)
            {
                cohesionStreeing = cohesionStreeing + IB.BoidsList[i].transform.position;
                cohesionTotal++;
            }

            if (IB.BoidsList[i] != gameObject && d < seperationPerception)
            {
                var diff = gameObject.transform.position - IB.BoidsList[i].transform.position;

                if (d > 0)
                {
                    //  print("in");
                    diff = diff / d;
                    seperationStreeing = seperationStreeing + diff;
                }
                seperationTotal++;
            }
        }

        // print(seperationTotal);
        if (seperationTotal > 0)
        {
            seperationStreeing = cohesionStreeing / seperationTotal;
            seperationStreeing = seperationStreeing - gameObject.GetComponent<Rigidbody>().position;
            seperationStreeing.y = 0;


            seperationStreeing = seperationStreeing / 30;


            //  gameObject.GetComponent<Rigidbody>().velocity = seperationStreeing;

            //if (gameObject.GetComponent<Rigidbody>().velocity.x >= maxSpeed || gameObject.GetComponent<Rigidbody>().velocity.z >= maxSpeed)
            //{
            //    gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
            //}
            //else if (gameObject.GetComponent<Rigidbody>().velocity.x <= -maxSpeed || gameObject.GetComponent<Rigidbody>().velocity.z <= -maxSpeed)
            //{
            //    gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
            //}
            //else
            //{
            //    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(seperationStreeing.x * 20, 0,
            //                                                                seperationStreeing.z * 20);
            //}

        }

        if (cohesionTotal > 0)
        {
            cohesionStreeing = cohesionStreeing / cohesionTotal;
            cohesionStreeing = cohesionStreeing - gameObject.GetComponent<Rigidbody>().position;
            cohesionStreeing.y = 0;

            cohesionStreeing = cohesionStreeing * 2;
        }

        if (alignTotal > 0)
        {
            alignStreeing = alignStreeing / alignTotal;
            alignStreeing = alignStreeing - gameObject.GetComponent<Rigidbody>().velocity;

            if (alignStreeing.x > maxStreeing)
                alignStreeing.x = maxStreeing;
            else if (alignStreeing.x < -maxStreeing)
                alignStreeing.x = -maxStreeing;

            if (alignStreeing.z > maxStreeing)
                alignStreeing.z = maxStreeing;
            else if (alignStreeing.z < -maxStreeing)
                alignStreeing.z = -maxStreeing;

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
        }

        totalStreeing = seperationStreeing + alignStreeing + cohesionStreeing;

        return totalStreeing;
    }

    private Vector3 alignment()
    {
        int total = 0;
        Vector3 streeing = Vector3.zero;
        float perception = 30;

        for (int i = 0; i < IB.boidCount; i++)
        {
            float d = Vector3.Distance(gameObject.transform.position, IB.BoidsList[i].transform.position);
            //print(d);

            if (IB.BoidsList[i] != gameObject && d < perception)
            {
                streeing = streeing + IB.BoidsList[i].GetComponent<Rigidbody>().velocity;
                total++;
            }
        }

        if (total > 0)
        {
            streeing = streeing / total;
            streeing = streeing - gameObject.GetComponent<Rigidbody>().velocity;

            if (streeing.x > maxStreeing)
                streeing.x = maxStreeing;
            else if (streeing.x < -maxStreeing)
                streeing.x = -maxStreeing;

            if (streeing.z > maxStreeing)
                streeing.z = maxStreeing;
            else if (streeing.z < -maxStreeing)
                streeing.z = -maxStreeing;

            streeing.y = 0;

            //streeing = streeing * 2;
            //gameObject.GetComponent<Rigidbody>().AddForce(streeing);
            //print(gameObject.GetComponent<Rigidbody>().velocity.x);
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
        }
        return streeing;
    }

    private Vector3 cohesion()
    {
        int total = 0;
        Vector3 streeing = Vector3.zero;
        float perception = 15;

        for (int i = 0; i < IB.boidCount; i++)
        {
            float d = Vector3.Distance(gameObject.transform.position, IB.BoidsList[i].transform.position);

            if (IB.BoidsList[i] != gameObject && d < perception)
            {
                streeing = streeing + IB.BoidsList[i].transform.position;
                total++;
            }
        }

        if (total > 0)
        {
            streeing = streeing / total;
            streeing = streeing - gameObject.GetComponent<Rigidbody>().position;

            streeing.y = 0;

        }
        return streeing;
    }

    private Vector3 Separation()
    {
        Vector3 streeing = Vector3.zero;


        return streeing;
    }
}
