using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput, verticalInput;
    public float speed = 10.0f;

    public float xRange = 15.0f; //rango en el eje x
   

    public GameObject projectilePrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   //Movimiento del personaje
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        
        if (transform.position.x < -xRange) //para hacer k player no vaya más allá de x=-15
        { transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange ) //para hacer k player no vaya más allá de x=15
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < 0) //para hacer k player no vaya más allá de z=0
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        if (transform.position.z > 15) //para hacer k player no vaya más allá de z=15
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 15);
        }

        //Acciones del personaje
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position,
                projectilePrefab.transform.rotation);
        }

    }
}
