using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]//se suele poner para verificar k se le ha asignado rigidbody
public class PlayerController : MonoBehaviour
{
    private const string speedMultiplier = "Speed Multiplier";
    private const string speedF = "Speed_f";
    private const string jumpTrig = "Jump_trig";
    private const string deathB = "Death_b";
    private const string deathTypeInt = "DeathType_int";

    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityMultiplier;
    public bool isOnTheGround = true;//al principio del juego el pj está en el suelo = verdadero

    private bool _gameOver = false;
    public bool GameOver{ get => _gameOver; }

    private Animator _animator;

    public ParticleSystem explosion, dirt;

    public AudioClip jumpSound, crashSound;
    private AudioSource _audioSource;
    [Range(0, 1)]
    public float audioVolume = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMultiplier;
        _animator = GetComponent<Animator>();
        _animator.SetFloat(speedF, 0.1f);
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat(speedF, Time.time/3);
        _animator.SetFloat(speedMultiplier, 1+Time.time/10);//el multiplicador de la velocidad de animación será igual
                                                            //al tiempo k lleva jugando
        
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround
            && !_gameOver)//salta si pulsa espacio y está en el suelo y no está muerto
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //F = m*a
            isOnTheGround = false; //después de saltar
            _animator.SetTrigger(jumpTrig);//hace la animación de salto
            dirt.Stop();
            _audioSource.PlayOneShot(jumpSound);
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
            dirt.Play();
        }else if (other.gameObject.CompareTag("Obstacle")) //si lo de arriba es false se ejecuta este else
        {
            _gameOver = true;
            Debug.Log("GAME OVER!!!");

            explosion.Play();
            _animator.SetBool(deathB, true);
            _animator.SetInteger(deathTypeInt, Random.Range(1,3));//el random range es para aleatorizar entre la
            dirt.Stop();                                          //muerte 1 o 2. El 3 no entra
            _audioSource.PlayOneShot(crashSound);
            
        }


    }


  
}
