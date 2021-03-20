using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _rigidbody;//como va por fisicas usamos rigidbody
    private float minForce = 12,
    maxForce = 20,
    maxTorque = 10,
    xRange = 6.8f,
    ySpanwPos = -6;

    private GameManager gameManager;

    [Range(-100, 100)]
    public int pointValue;

    public ParticleSystem explosionParticle;

   



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

        //gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); buscamos por nombre
        gameManager = FindObjectOfType<GameManager>();// o buscamos por tipo, no hace falta poner GameObject
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

    private void OnMouseOver()//cuandop se pasa el mouse por encima
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
            
        }
        
    }

    private void OnTriggerEnter(Collider other)//para destruir objeto al colisionar abajo
    {
        if (other.CompareTag("Kill Zone"))
        {
            Destroy(gameObject);

            if (gameObject.CompareTag("Good"))
                { 
                gameManager.GameOver();
                }
            
        }
        
    }
}
