using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _rigidbody;//como va por fisicas usamos rigidbody
    private float minForce = 12, 
    maxForce = 20,
    maxTorque = 10,
    xRange = 4,
    ySpanwPos = -6;


    // Start is called before the first frame update
    void Start()
    {
       _rigidbody = GetComponent<Rigidbody>();

         //añadimos fuerza y torque aleatorios
       _rigidbody.AddForce(RandomForce(),
       ForceMode.Impulse);

       _rigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(),
        ForceMode.Impulse);

       //spawn aleatorio
       transform.position = RandomSpawnPos();

    }

    /// <summary>
    /// Genera un vector aleatorio en 3D
    /// </summary>
    /// <returns>Fuerza aleatoria hacia arriba</returns>
     private Vector3 RandomForce()
    {
        return Vector3.up*Random.Range(minForce, maxForce);
    }

    /// <summary>
    /// Genera un número aleatorio
    /// </summary>
    /// <returns>Valor aleatorio entre -maxTorque y maxTorque</returns>
    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    /// <summary>
    /// Genera una posición aleatoria
    /// </summary>
    /// <returns>Posición aleatoria en 3D, con coordenada z=0</returns>
    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpanwPos);//z=0
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
