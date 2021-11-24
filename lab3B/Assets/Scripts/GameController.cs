using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;

    public GameObject powerUp;

    public Vector3 Enemy1SpawnValues;

    public float Enemy2SpawnValuesZmin;
    public float Enemy2SpawnValuesZmax;

    public float Enemy3SpawnValuesZmin;
    public float Enemy3SpawnValuesZmax;

    public int Enemy1Count;

    public int Enemy2Count;

    public int Enemy3Count;

    public Vector3 powerUpSpawn;

    public float Enemy1SpawnWait;
    public float startEnemny1Wait;
    public float waveEnemy1Wait;


    public static GameController instance;

    public TextMeshProUGUI playerLivesText;
    int playerLives;

    public TextMeshProUGUI scoreText;

    int score;
    private void Start()
    {
        instance = this;
        StartCoroutine(SpawnWavesEnemy1());

        playerLives = 3;

        updateLives();

        score = 0;
        updateScore();
    }
    IEnumerator SpawnWavesEnemy1()
    {
        yield return new WaitForSeconds(startEnemny1Wait);  
        while (true)
        {
            for (int i = 0; i < Enemy1Count; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-Enemy1SpawnValues.x, Enemy1SpawnValues.x), Enemy1SpawnValues.y, Enemy1SpawnValues.z);
                Instantiate(Enemy1, spawnPosition, Enemy1.transform.rotation);

                if(i % 5 == 0)
                {
                    Vector3 spawnPosition_2 = new Vector3(-8, 0, Random.Range(Enemy2SpawnValuesZmin, Enemy2SpawnValuesZmax));
                    Instantiate(Enemy2, spawnPosition_2, Enemy2.transform.rotation);

                    Vector3 spawnPosition_3 = new Vector3(8, 0, Random.Range(Enemy2SpawnValuesZmin, Enemy2SpawnValuesZmax));
                    Instantiate(Enemy3, spawnPosition_3, Enemy3.transform.rotation);
                }

                if (i % 7 == 0)
                {
                    Vector3 spawnPosition_03 = new Vector3(Random.Range(-powerUpSpawn.x, powerUpSpawn.x), powerUpSpawn.y, powerUpSpawn.z);
                    Instantiate(powerUp, spawnPosition_03, powerUp.transform.rotation);
                }
                yield return new WaitForSeconds(Enemy1SpawnWait);
            }
            yield return new WaitForSeconds(waveEnemy1Wait);
        }
    }

    public void playerTakeDamage()
    {
        playerLives--;
        updateLives();
    }
  
    void updateLives()
    {
        playerLivesText.text = "Lives : " + playerLives.ToString();
    }

    public void playerAddScore()
    {
        score += 100;
        updateScore();
    }

    void updateScore()
    {
        scoreText.text = "Score : " + score.ToString();
    }
}
