using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlock : MonoBehaviour
{
    //prefab for fish
    public GameObject fishPrefab;
    public GameObject goalPrefab;
    public int goalPosResetFrequency = 10500;
    public static int tankSize = 5; //public and static to be accessed outside the script
    static int numberOfFish = 50;
    public static GameObject[] allFish = new GameObject[numberOfFish];// made static to be accesible by other scripts
    public static Vector3 goalPos = Vector3.zero;

    void Start()
    {
        RenderSettings.fogColor = Camera.main.backgroundColor;
        RenderSettings.fogDensity = 0.03f;
        RenderSettings.fog = true;
        for(int i = 0; i <numberOfFish; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0, goalPosResetFrequency) < 50)
        {
            goalPos = new Vector3(Random.Range(-tankSize, tankSize),
                                  Random.Range(-tankSize, tankSize),
                                  Random.Range(-tankSize, tankSize));
            goalPrefab.transform.position = goalPos;
        }
    }
}
