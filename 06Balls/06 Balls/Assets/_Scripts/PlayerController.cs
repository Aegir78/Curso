using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float moveForce;

    public GameObject focalPoint;

    public bool hasPowerUp;
    public float powerUpForce;
    public float powerUpTime;

    public GameObject[] powerUpIndicators;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //es mejor utilizar en el futuro
        //focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(focalPoint.transform.forward*moveForce*forwardInput, 
            ForceMode.Force);

        foreach (GameObject indicator in powerUpIndicators) //bucle para k no rote con el player si es su hijo
        {                                                   //el objeto estar� en la posici�n del player
            indicator.transform.position = this.transform.position;
        }

        //TODO arreglar esto para k si la bola muere reinicie el juego
        //if (this.transform.position.y < -10)
        //{
            //SceneManager.LoadScene("Prototype 4");
       // }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdown()); //llamamos a la corutina
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();//llamamos al rigid del enemigo
            Vector3 awayFromPlayer = collision.gameObject.transform.position  //el efecto del powerup(repele) es un vector=enemigo-jugador
                - this.transform.position;

            enemyRigidbody.AddForce(awayFromPlayer*powerUpForce, ForceMode.Impulse);//Impulse repulsi�n inmediata

            Debug.Log("El jugador ha colisionado contra " + 
                collision.gameObject + " y tiene el power up a " + hasPowerUp);
        }
    }


    IEnumerator PowerUpCountdown()
    {
        foreach (GameObject indicator in powerUpIndicators) //bucle: para cada GameObject llamado indicador dentro de la
                                                            //coleccion powerUpIndicator, haz:
        {
            indicator.gameObject.SetActive(true);//el primer anillo se activa
            yield return new WaitForSeconds(powerUpTime / powerUpIndicators.Length); //espera powerruptime entre la longitud de indicadores
            indicator.gameObject.SetActive(false);
        }
        
        hasPowerUp = false;
    }
}
