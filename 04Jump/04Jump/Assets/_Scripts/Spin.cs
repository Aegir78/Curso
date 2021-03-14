using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{

    public float rotateSpeed = 60;
    public float translateSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.left*translateSpeed*Time.deltaTime);
        transform.localPosition += Vector3.left*translateSpeed*Time.deltaTime;

        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
