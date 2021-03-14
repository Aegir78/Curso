using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player; //Puede sre player, objeto a seguir, etc

    private Vector3 offset = new Vector3(0, 5, -6);

    private void Update()
    {
        transform.position = player.transform.position + offset; /*transforma la posicion de
                                                        player a la cámara*/
    }
}
