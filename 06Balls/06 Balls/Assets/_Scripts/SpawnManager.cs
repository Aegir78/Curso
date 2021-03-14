using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    public GameObject[] enemyPrefabs;
    private int enemyIndex;

    public GameObject powerUpPrefab;
  

    private float spawnRange = 9;
    private float spawnRangey = 20;

    public int enemyCount;
    public int enemyWave = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(enemyWave);
        
    }

    private void Update()
    {
        enemyCount = GameObject.FindObjectsOfType<Enemy>().Length;//encuentra los objetos enemigos que quedan
        if (enemyCount == 0)
        {
            enemyWave++;
            SpawnEnemyWave(enemyWave);//cuando no hay enemigos spawnea enemigos

            int numberOfPowerUps = Random.Range(0, 3);
            for (int i = 0; i < numberOfPowerUps; i++)
            {
                Instantiate(powerUpPrefab, GenerateSpawnPosition2(), powerUpPrefab.transform.rotation);
            }


        }

    }

    //este método genera una posición aleatoria
    /// <summary>
    /// Genera una posición aleatoria dentro del juego
    /// </summary>
    /// <returns>Devuelve una posición aleatoria en la zona de juego</returns>
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosx = Random.Range(-spawnRange, spawnRange);
        float spawnPosz = Random.Range(-spawnRange, spawnRange);
        float spawnPosy = Random.Range(0, spawnRangey);
        Vector3 randomPos = new Vector3(spawnPosx, spawnPosy, spawnPosz);
        return randomPos;
    }

    private Vector3 GenerateSpawnPosition2()//para los power up

    {
        float spawnPosx = Random.Range(-spawnRange, spawnRange);
        float spawnPosz = Random.Range(-spawnRange, spawnRange);
        
        Vector3 randomPos = new Vector3(spawnPosx, 0.5f, spawnPosz);
        return randomPos;
    }

    /// <summary>
    /// Método que genera un número de enemigos en pantalla
    /// <param name="numberOfEnemies"/>Número de enemigos a crear</param>
    /// </summary>
    private void SpawnEnemyWave(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], GenerateSpawnPosition(),
                enemyPrefabs[enemyIndex].transform.rotation);
        }

    }
    
}
