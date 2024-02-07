using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{


    public static GameObject PreFab;


    static GameObject newObject;


    private static Vector3 spawnLocation = new Vector3(-26, 0, 26);


    void Start()
    {



    }





    void Update()
    {

       


    }



    public static void SpawnCube()
    {
        //newObject = GameObject.Instantiate(PreFab);
        newObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newObject.transform.position = spawnLocation;
        newObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        newObject.AddComponent<Rigidbody>();
    }




        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Killbox")
        {

            Destroy(newObject);

        }
    }




}
