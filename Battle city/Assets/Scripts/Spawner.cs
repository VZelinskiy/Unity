using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    public int PlayerSpawnIndex = 3;

    public Transform randomSpawnPoint
    {
        get
        {
            int randIndex = Random.Range(0, transform.childCount - 1);
            return transform.GetChild(randIndex);
        }
    }

    public Transform playerSpawnPoint
    {
        get
        {
            return transform.GetChild(PlayerSpawnIndex);
        }
    }

    public void Spawn(bool isPlayer, GameObject Tank)
    {
        if (isPlayer)
        {
            Instantiate(Tank, playerSpawnPoint.position, Quaternion.Euler(0 , 180, 0));
        }
        else
        {
            GameObject tank = Instantiate(Tank, randomSpawnPoint.position, Quaternion.identity);
        }
        
    }
}
