using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0,180)]
    public float moveSpeed, rotateSpeed, force;

    public bool usePhysicsEngine;

    private Rigidbody _rigidbody;

    private float verticalInput, horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();//para usar físicas
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        MovePlayer();

        KeepPlayerInBounds();

        

    }

    void MovePlayer()
    {
        if (usePhysicsEngine)
        {
            //si se utiliza la física
            //AddForce sobre el rigidbody
            //AddTorque sobre el rigidbody
            _rigidbody.AddForce(Vector3.forward * Time.deltaTime * force * verticalInput, ForceMode.Force);
            _rigidbody.AddTorque(Vector3.up * force * Time.deltaTime * horizontalInput, ForceMode.Force);
        }
        else
        {
            //si no utilizáis fisica
            //Translate sobre el transform -> para mover
            //Rotate sobre el transform -> para rotar
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * verticalInput);
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * horizontalInput);
        }

    }

    void KeepPlayerInBounds()
    {
        //para k no salga del mapa
        //TODO: rafactorizar la posición límite en una variable
        if (Mathf.Abs(transform.position.x) >= 48 || Mathf.Abs(transform.position.z) >= 48)
        {
            _rigidbody.velocity = Vector3.zero;
            if (transform.position.x > 48)
            {
                transform.position = new Vector3(48, transform.position.y, transform.position.z);
            }
            if (transform.position.x < -48)
            {
                transform.position = new Vector3(-48, transform.position.y, transform.position.z);
            }
            if (transform.position.z > 48)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 48);
            }
            if (transform.position.z < -48)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -48);
            }

        }
    }

}
