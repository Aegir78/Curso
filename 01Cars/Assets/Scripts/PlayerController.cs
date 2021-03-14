using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Propiedades
    [Range(0, 100), Tooltip("Velocidad lineal máxima del coche")]
    private float speed = 10f; //aceleracion

    [Range(0, 100), Tooltip("Velocidad de giro máximo del coche")]
    private float turnSpeed = 50f; //giro
    private float horizontalInput, verticalInput;








    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Tenemos que mover el vehículo hacia adelante
        /* esto es para comentar
         * con más lineas
         */

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // S = S0 + V*t*(dirección)
        
        transform.Translate(speed*Time.deltaTime*Vector3.forward*verticalInput);

        transform.Rotate(turnSpeed*Time.deltaTime*Vector3.up*horizontalInput);
    }
}
