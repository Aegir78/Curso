using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisions : MonoBehaviour
{
    //On es un evento
    //OnTriggerEnter se llamará automáticamente cuando
    // un objeto físico entre dentro del trigger del
    //game object
    private void OnTriggerEnter(Collider other)

    {
        if (other.CompareTag("Enemy"))
        {
            //la bala choca contra un enemigo
            Destroy(this.gameObject);//destruye la bala
            Destroy(other.gameObject);//destruye con lo k choca
        }
        
    }
}
