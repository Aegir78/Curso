using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 30f;
    private float lowerBound = -10f;

    void Update()
    {

        //AND x && y => se debe cumplir x e y a la vez
        //OR x || y => se debe cumplir uno u otro o los dos
        if ((this.transform.position.z > topBound) || 
            (this.transform.position.z < lowerBound))
        {
            Destroy(this.gameObject);//destruye lo k excede
        }

        if (this.transform.position.z < lowerBound)//si algo excede el límite inferior
        {
            Debug.Log("GAME OVER");//Fin de la partida
            Destroy(this.gameObject);//destruye lo k excede

            Time.timeScale = 0;//se para el juego
        }
    }
}
