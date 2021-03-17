using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetPrefabs; //mejor hacer List que array
    private float spawnRate = 1.0f;

    public TextMeshProUGUI scoreText;

    private int _score;//esta variable tendrá el valor
    private int score
    {
        set
        {
            _score = Mathf.Max(value, 0);
        }
        get
        {
            return _score;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());

        score = 0;
        UpdateScore(0);
    }

    private IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);//espera 1 seg
            int index = Random.Range(0, targetPrefabs.Count);//elige uno aleatoriamente
            Instantiate(targetPrefabs[index]);//lo saca en pantalla
        }
    }

    /// <summary>
    /// Actualiza la puntuación y lo muestra por pantalla
    /// </summary>
    /// <param name="scoreToAdd">Número de puntos a añadir a la puntuación global</param>
    public void UpdateScore(int scoreToAdd) //publico para k se pueda usar en otros scripts
    {
        score += scoreToAdd;//la puntuación se suma a la puntuación k se ha de añadir
        scoreText.text = "Score\n" + score;//pintamos el score
    }
}
