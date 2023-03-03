using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerScoreText;
    [SerializeField] private TMP_Text enemyScoreText;

    [SerializeField] private Transform Player;
    [SerializeField] private Transform Enemy;
    [SerializeField] private Transform Ball;

    private int playerScore;
    private int enemyScore;

    [SerializeField] public int maxPoints = 6;

    private static GameManager instance;

    public void CheckVictory()
    {
        if (playerScore >= maxPoints)
            SceneManager.LoadScene("PlayerVictory");
        if (enemyScore >= maxPoints)
            SceneManager.LoadScene("EnemyVictory");

    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public void PlayerScored()
    {
        playerScore++;
        playerScoreText.text = playerScore.ToString();
        CheckVictory();
    }

    public void EnemyScored()
    {
        enemyScore++;
        enemyScoreText.text = enemyScore.ToString();
        CheckVictory();
    }

    public void Restart()
    {
        Player.position = new Vector2(Player.position.x, -0.5f);
        Enemy.position = new Vector2(Enemy.position.x, -0.5f);
        Ball.position = new Vector2(0.6f, -0.5f);
    }


}
