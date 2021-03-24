using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;

    public float displayImageDuration = 3f;

    private bool isPlayerAtExit, isPlayerCought;

    public GameObject player;

    public CanvasGroup exitBackgroundImageCanvasGroup;

    public CanvasGroup coughtBackgroundImageCanvasGroup;

    private float timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    private void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false);
        }
        else if (isPlayerCought)
        {
            EndLevel(coughtBackgroundImageCanvasGroup, true);
        }
    }


    /// <summary>
    /// Lanza la imagen del fin de partida
    /// </summary>
    /// <param name="imageCanvasGroup">Imagen de fin de partida correspondiente</param>
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart)
    {
        timer += Time.deltaTime;
        imageCanvasGroup.alpha = timer / fadeDuration;

        if (timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene("Main Scene");
            }
            else
            {
                Application.Quit();
            }
            
        }
        
    }

    public void CatchPlayer ()
    {
        isPlayerCought = true;
    }
}
