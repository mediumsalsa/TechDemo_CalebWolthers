using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldScript : MonoBehaviour
{



    void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this);
        }
    }



    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime);
    }
}
