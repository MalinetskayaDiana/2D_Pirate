using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform shotPosition;
    public GameObject Bullet;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(Bullet, shotPosition.transform.position, transform.rotation);
        }
    }
    /*private void Awake()
    {
        GameObject clone = (GameObject)Instantiate(Bullet, transform.position, Quaternion.identity);
        Destroy(clone, 1.0f);
    }*/
}
