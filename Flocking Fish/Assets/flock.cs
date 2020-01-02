using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour
{
    public float speed = 0.001f;
    float rotationSpeed = 4.0f;//This is how fast the fish will turn when they need to turn
    Vector3 averageHeading;//This is the average heading of the group
    Vector3 averagePosition;//This is the average position of the group
    float neighbourDistance = 4.0f;//This is the maximum distance they need to be to flock if they are more than this they wont take notice of each other

    bool turning = false;

    void Start()
    {
        speed = Random.Range(0.5f, 1);//Runs the algorithm 1 in 5 times randomly 
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) >= globalFlock.tankSize)
            turning = true;
        else
            turning = false;

        if (turning)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                     Quaternion.LookRotation(direction),
                                     rotationSpeed * Time.deltaTime);
            speed = Random.Range(0.5f, 1);
        }
        else
        {
            if (Random.Range(0, 5) < 1)
                ApplyRules();
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void ApplyRules()
    {
        GameObject[] gos;//gos = gameobjects
        gos = globalFlock.allFish;//The public static allfish array in globalflock script, this is here now so that each fish knows where all other fish are

        Vector3 vcentre = Vector3.zero;//Calculate the centre of the group
        Vector3 vavoid = Vector3.zero;//Avoidance vector for fish not to hit each other while trying to get to center of the group
        float gSpeed = 0.1f;//Group Speed

        Vector3 goalPos = globalFlock.goalPos; 

        float dist;

        int groupSize = 0;
        foreach(GameObject go in gos)
        {
            if(go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if(dist <= neighbourDistance)//Group size is based on which fish are within the neighbour distance
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if(dist < 1.0f)//If the fish get in a small distance they should avoid
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    flock anotherFlock = go.GetComponent<flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if(groupSize > 0)
        {
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if(direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                     Quaternion.LookRotation(direction),
                                     rotationSpeed * Time.deltaTime);
            
        }
    }
}
