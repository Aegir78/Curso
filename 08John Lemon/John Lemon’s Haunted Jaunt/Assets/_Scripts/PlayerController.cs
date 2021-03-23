using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;

    private Animator _animator;

    private Rigidbody _rigidbody;

    [SerializeField]
    private float turnSpeed;

    private Quaternion rotation = Quaternion.identity;


    // Start is called before the first frame update
    void Start()
    {
        //Llamamos la componente de la animaci�n
        _animator = GetComponent<Animator>();

        //Llamamos la componente de rigidbody
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        //movimiento (vector3 normalizado para que siempre sea igual de largo)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement.Set(horizontal, 0, vertical);
        movement.Normalize();

        //en este caso la animaci�n ya tiene movimiento
        //si horizontal vale aproximadamente 0 la negaci�n(!) es falsa y no hay movimiento
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0);
        bool isWalking = hasHorizontalInput || hasVerticalInput;//si hay mov vertical o/y horizontal,
                                                                //esa ser� el valor de isWalking

        //a cada frame hay k ver si estamos caminando o no
        _animator.SetBool("IsWalking", isWalking);

        //para hacer un movimiento m�s suave entre un comienzo y un destino,
        // en este caso hay k rotar(vector)
        //direcci�n deseada = donde miro ahora, donde quiero mirar,
        //velocidad giro*tiempo, magnitud m�xima del cambio
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward,
            movement, turnSpeed * Time.deltaTime, 0f);

        //Rotamos con Quaternion
        rotation = Quaternion.LookRotation(desiredForward);
    }


    private void OnAnimatorMove()
    {
        //truco para cuando la animaci�n ya aporta movimiento
        //S = S0 + V*T
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(rotation);
    }
}
