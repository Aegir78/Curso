using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody _rigidbody;
    public float moveForce;

    private GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - this.transform.position).normalized;  //la direccion es punto de destino menos punto de origen
                                                                                                //es un vector normalizado = magnitud 1, sino cuanto
                                                                                                //más lejos del player más fuerza se le aplica y más correrá
        _rigidbody.AddForce(moveForce*lookDirection, ForceMode.Force);//la fuerza es el vector k une el centro del enemigo con el player

        if (this.transform.position.y < -10)
        {
            Destroy(gameObject);
        }
                                
    }
}
