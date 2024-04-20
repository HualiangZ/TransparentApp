using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    public GameObject[] spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn = GameObject.FindGameObjectsWithTag("Spawn");
        Instantiate(enemy);

       StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        for(; ; )
        {
            yield return new WaitForSeconds(3);
            int rng = Random.Range(0, spawn.Length);
            int spawnrng = Random.Range(0, 100);
            if(spawnrng <= 50)
            {
                Instantiate(enemy, spawn[rng].transform.position, spawn[rng].transform.rotation);
            }
            
        }
    }
    
}
