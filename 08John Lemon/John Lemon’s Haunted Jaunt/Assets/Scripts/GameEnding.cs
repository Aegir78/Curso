using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;

    public float displayImageDuration = 3f;

    private bool isPlayerAtExit;

    public GameObject player;

    public CanvasGroup exitBackgroundImageCanvasGroup;

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
            timer += Time.deltaTime;
            exitBackgroundImageCanvasGroup.alpha = timer / fadeDuration;

            if (timer > fadeDuration + displayImageDuration)
            {
                EndLevel();
            }
        }
    }

    void EndLevel()
    {
        Application.Quit();
    }
}
