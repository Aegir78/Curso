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

    private int _score;//esta variable tendr� el valor
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

    public GameObject titleScreen;// El panel del UI tambi�n es un objeto

    public int difficulty;//asignamos dificultad

    private int numberOfLives = 4;
    public List<GameObject> lives;

    private void Start()
    {
        ShowMaxScore();
    }

    public ParticleSystem explosionMaxScore;

    /// <summary>
    /// M�todo que inicia la partida cambiando el valor del estado del juego
    /// </summary>
    /// <param name="difficulty">N�mero entero que indica el grado de dificultad del juego</param>
    public void StartGame(int difficulty)
    {
        gameState = GameState.inGame;
        titleScreen.gameObject.SetActive(false);

        spawnRate /= difficulty; // es lo mismo k spawnRate = spawnRate/difficulty
        numberOfLives -= difficulty;

        for (int i = 0; i < numberOfLives; i++)
        {
            lives[i].SetActive(true);
        }

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
    /// Actualiza la puntuaci�n y lo muestra por pantalla
    /// </summary>
    /// <param name="scoreToAdd">N�mero de puntos a a�adir a la puntuaci�n global</param>
    public void UpdateScore(int scoreToAdd) //publico para k se pueda usar en otros scripts
    {
        score += scoreToAdd;//la puntuaci�n se suma a la puntuaci�n k se ha de a�adir
        scoreText.text = "Score\n" + score;//pintamos el score
    }

    private const string MAX_SCORE = "MAX_SCORE";
    public void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        scoreText.text = "Max Score\n" + maxScore;
    }


    private void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        if (score > maxScore)
        {
            PlayerPrefs.SetInt(MAX_SCORE, score);
            Instantiate(explosionMaxScore, explosionMaxScore.transform.position, explosionMaxScore.transform.rotation);
        }
    }
    public void GameOver()
    {
        numberOfLives--;

        if (numberOfLives>=0)
        {
            Image heartImage = lives[numberOfLives].GetComponent<Image>();
            var tempColor = heartImage.color;
            tempColor.a = 0.3f;
            heartImage.color = tempColor;
        }
        

        if (numberOfLives <= 0)
        {
            SetMaxScore();

            gameState = GameState.gameOver;

            gameOverText.gameObject.SetActive(true);

            restartButton.gameObject.SetActive(true);
        }
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //recarga la escena actual
    }
}
