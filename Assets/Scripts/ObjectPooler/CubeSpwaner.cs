using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpwaner : MonoBehaviour
{
    ObjectPooler objectPooler;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        InvokeRepeating("Spawn", 5,5);
    }

    void FixedUpdate(){
        
    }

    void Spawn(){
        objectPooler.SpawnFromPool("Cube", transform.position,Quaternion.identity);
    }
}
