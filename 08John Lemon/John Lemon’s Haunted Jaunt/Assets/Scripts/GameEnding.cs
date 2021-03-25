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

    public AudioSource exitAudio, coughtAudio;
    bool hasAudioPlayed;

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
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (isPlayerCought)
        {
            EndLevel(coughtBackgroundImageCanvasGroup, true, coughtAudio);
        }
    }


    /// <summary>
    /// Lanza la imagen del fin de partida
    /// </summary>
    /// <param name="imageCanvasGroup">Imagen de fin de partida correspondiente</param>
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }

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
