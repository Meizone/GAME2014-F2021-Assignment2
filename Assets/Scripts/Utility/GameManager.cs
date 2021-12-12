/*
Nathan Nguyen
101268067

12/12/2021

Game Manager


*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("Score Values")]
    [SerializeField] private int Health = 3;
    [SerializeField] private int Gold = 0;
    [SerializeField] private int KilledEnemies = 0;
    [SerializeField] Text GoldCount;


    [Header("Health")]
	[SerializeField] List<GameObject> HeartList;


    [Header("GameOver")]
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] Text Text_GoldScore;
    [SerializeField] Text Text_KilledEnemies;
    [SerializeField] Text Text_HealthRemaining;
    [SerializeField] Text Text_TotalScore;



    
    

    // Start is called before the first frame update
    void Start()
    {
        SetGold(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamageTaken()
    {
        HeartList[Health-1].SetActive(false);
        Health-= 1;

        if(Health == 0)
            GameOver();
    }

    public void SetGold(int GoldAdd)
    {
        Gold += GoldAdd;
        GoldCount.text = "" + Gold;
    }

    public void EnemyKilled()
    {
        KilledEnemies += 1;

        if(KilledEnemies == 4)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        GameOverScreen.SetActive(true);
        Text_GoldScore.text = Gold + "";
        Text_KilledEnemies.text = "200 x " + KilledEnemies;
        Text_HealthRemaining.text = "1000 x " + Health;
        Text_TotalScore.text = "" + (Gold + (200 * KilledEnemies) + (1000 * Health));

    }
}
