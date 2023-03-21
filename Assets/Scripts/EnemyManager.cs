using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void EnemysEvent(EnemyManager enemyManager);
public class EnemyManager : MonoBehaviour
{
    //variables para la cantidad de enemigos
    public Enemy[] prefabs;
    public int rows = 3;
    public int columns = 3;
    public bool EnemyTouch = false;
    //variable para la velocidad de los enemigos
    public int amountKilled { get; private set; }
    public int totalEnemy => this.rows * this.columns;
    public float percentKilled => (float)this.amountKilled / (float)this.totalEnemy;
    //variable para el ataque
    public EnemyAttack EnemyAttack;
    public float enemyAttackRate = 1.0f;
    public EnemyAttackEvent destroyed;
    //variable UI PA LA PROXIMA en su scrip propio
    public TMPro.TMP_Text resultText;
    public TMPro.TMP_Text lifeText;
    public UnityEngine.UI.Button retryButton;
    public UnityEngine.UI.Button exitButton;
    public UnityEngine.UI.Button menuButton;
    public UnityEngine.UI.Button addButton;
    public Player Player;

    public static Action OnWallTouched;
    public static Action AdvanceSpeed;
    private List<Enemy> spawnedEnemies = new List<Enemy>();

    private void Awake() //matriz de enamigos
    {
        lifeText.text = "Life: " + Player.life;
        Time.timeScale = 2;
        retryButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        addButton.gameObject.SetActive(false);
        OnWallTouched += SwapDirection; //le agregamos la accion de swapear
        AdvanceSpeed += HighSpeed; //le ageragmos la accion de aumentar la velocidad
        for (int row = 0; row < this.rows; row++)
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector2 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);

            for (int col = 0; col < this.columns; col++)
            {
                Enemy enemy = Instantiate(this.prefabs[row], this.transform);
                enemy.OnKilled += EnemyKilled;
                Vector3 position = rowPosition;
                position.x += col * 10.0f;
                enemy.transform.localPosition = position;
                spawnedEnemies.Add(enemy);
            }
        }
    }

    private void SwapDirection()
    {
        foreach (Enemy enemy in spawnedEnemies)
        {
            enemy.SwapDirection();
        }
    }

    private void OnDestroy()
    {
        OnWallTouched -= SwapDirection;
        AdvanceSpeed -= HighSpeed;
    }

    private void HighSpeed()
    {
        foreach (Enemy enemy in spawnedEnemies)
        {
            enemy.HighSpeed(percentKilled);
        }
    }       

    private void Start()
    {
        InvokeRepeating(nameof(Attack), this.enemyAttackRate, this.enemyAttackRate);
    }

    private void EnemyKilled(Enemy enemy)
    {
        spawnedEnemies.Remove(enemy);
        enemy.OnKilled -= EnemyKilled;
        this.amountKilled++;
        HighSpeed();
        if (amountKilled == totalEnemy)
        {
            EndGame();
        }
    }

    private void Destroyed(EnemyAttack enemyAttack)
    {
        EnemyAttack.destroyed -= Destroyed;

    }

    private void Attack() //el ataque
    {
        foreach (Transform enemy in this.transform)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (UnityEngine.Random.value < (1.0f / ((float)this.totalEnemy - (float)this.amountKilled)))
            {
                EnemyAttack EnemyAttack = Instantiate(this.EnemyAttack, enemy.position, Quaternion.identity);
                EnemyAttack.destroyed += Destroyed;
                break;
            }
        }
    }

    public void EndGame()
    {
        if (Player.life == 0 || EnemyTouch == true)
        {
            resultText.text = "You Lose";
            retryButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
            resultText.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
            addButton.gameObject.SetActive(true);
            return;
        }
        if (amountKilled == totalEnemy)
        {
            resultText.text = "You Win";
            retryButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
            resultText.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
            addButton.gameObject.SetActive(true);
            return;
        }
        return;
    }

    public void Close()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void ChangeScene(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}