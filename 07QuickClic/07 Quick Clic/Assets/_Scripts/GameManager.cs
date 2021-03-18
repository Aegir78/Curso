using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        loading,
        inGame,
        gameOver
    }

    public GameState gameState;


    public List<GameObject> targetPrefabs; //mejor hacer List que array
    private float spawnRate = 1.0f;

    public TextMeshProUGUI scoreText;

    public Button restartButton;

    private int _score;//esta variable tendrá el valor
    private int score
    {
        set
        {
            _score = Mathf.Clamp(value, 0, 99999);
        }
        get
        {
            return _score;
        }
    }

    public TextMeshProUGUI gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.inGame;

        StartCoroutine(SpawnTarget());

        score = 0;
        UpdateScore(0);
    }

    private IEnumerator SpawnTarget()
    {
        while (gameState == GameState.inGame)
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

    public void GameOver()
    {
        gameState = GameState.gameOver;

        gameOverText.gameObject.SetActive(true);

        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //recarga la escena actual
    }
}
