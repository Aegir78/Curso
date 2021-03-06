//#if UNITY_ANDROID
    //#define USING_MOBILE

//#endif



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;

    private Animator _animator;

    private Rigidbody _rigidbody;

    private AudioSource _audiosource;

    [SerializeField]
    private float turnSpeed;

    private Quaternion rotation = Quaternion.identity;


    // Start is called before the first frame update
    void Start()
    {
        //Llamamos la componente de la animación
        _animator = GetComponent<Animator>();

        //Llamamos la componente de rigidbody
        _rigidbody = GetComponent<Rigidbody>();

        _audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
//#if USING_MOBILE //directiva de precompilado
            //float horizontal = Input.GetAxis("MOUSE X");
            //float vertical = Input.GetAxis("MOUSE Y");
            //if (Input.touchCount > 0)
	            //{
                //horizontal = Input.touches[0].deltaPosition.x;
                //vertical = Input.touches[0].deltaPosition.y;
	            //}
//#else

        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

//#endif

        //movimiento (vector3 normalizado para que siempre sea igual de largo)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement.Set(horizontal, 0, vertical);
        movement.Normalize();

        //en este caso la animación ya tiene movimiento
        //si horizontal vale aproximadamente 0 la negación(!) es falsa y no hay movimiento
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0);
        bool isWalking = hasHorizontalInput || hasVerticalInput;//si hay mov vertical o/y horizontal,
                                                                //esa será el valor de isWalking

        //a cada frame hay k ver si estamos caminando o no
        _animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!_audiosource.isPlaying)
            {
                _audiosource.Play();
            }
        }
        else
        {
            _audiosource.Stop();
        }

        //para hacer un movimiento más suave entre un comienzo y un destino,
        // en este caso hay k rotar(vector)
        //dirección deseada = donde miro ahora, donde quiero mirar,
        //velocidad giro*tiempo, magnitud máxima del cambio
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward,
            movement, turnSpeed * Time.fixedDeltaTime, 0f);

        //Rotamos con Quaternion
        rotation = Quaternion.LookRotation(desiredForward);
    }


    private void OnAnimatorMove()
    {
        //truco para cuando la animación ya aporta movimiento
        //S = S0 + V*T
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(rotation);
    }
}
