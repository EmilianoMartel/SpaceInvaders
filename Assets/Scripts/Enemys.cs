using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemys : MonoBehaviour
{
    public Enemy[] prefabs;
    public int rows = 3;
    public int columns = 3;
    public AnimationCurve speed;    
    //variable para la velocidad de los enemigos
    public int amountKilled { get; private set; }
    public int totalEnemy => this.rows * this.columns;
    public float percentKilled => (float) this.amountKilled / (float) this.totalEnemy;
    //variable para el ataque
    public EnemyAttack EnemyAttack;
    public float enemyAttackRate = 1.0f;

    private float _direction = 1.0f;

    public static Action OnWallTouched;
    private List<Enemy> spawnedEnemies = new List<Enemy>(); 

    private void Update()
    {
        
    }

    private void Awake() //la matriz toda rota
    {
        OnWallTouched += SwapDirection;
        for(int row = 0; row < this.rows; row++)
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width/2, -height/2);
            Vector2 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);            

            for (int col =0 ; col < this.columns; col++)
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
    }

    private void Attack() //el ataque
    {
        foreach(Transform enemy in this.transform)
        {
            if(!enemy.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (UnityEngine.Random.value < (1.0f / ( (float) this.totalEnemy - (float) this.amountKilled)))
            {
                Instantiate(this.EnemyAttack, enemy.position, Quaternion.identity);
                break;
            }
        }    
    }
}
