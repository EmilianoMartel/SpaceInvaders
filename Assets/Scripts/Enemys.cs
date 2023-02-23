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

    private Vector3 _direction = Vector2.right;


    private void Update()
    {
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime; //para manejar la velocidad

        //Variables para el movimiento, esto nos da el punto de la derecha y de la izquierda
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform enemy in this.transform) //movimiento de los enemigos
        {
            if (enemy.gameObject.activeInHierarchy) //pregunta si esta activo
            {
                continue;
            }
            if (_direction == Vector3.right && enemy.position.x >= (rightEdge.x - 1.0f)) //si va a la derecha y choca con el borde esto
            {
                AdvanceRow();
            }
            else if (_direction == Vector3.left && enemy.position.x >= (leftEdge.x + 1.0f)) //si va a la izquierda y choca con el borde esto
            {
                AdvanceRow();
            }
        }
    }

    private void AdvanceRow() //funcion para que el enemigo cuando toca un borde baje y cambie de direccion
    {
        _direction *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    private void Awake() //la matriz toda rota
    {
        for(int row = 0; row < this.rows; row++)
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width/2, -height/2);
            Vector2 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);            

            for (int col =0 ; col < this.columns; col++)
            {
                // if (col/2 == 0)
                // {
                //     Enemy enemy = Instantiate(this.prefabs[0], this.transform);                 
                // }
                // else
                // {
                //     Enemy enemy = Instantiate(this.prefabs[1], this.transform);
                // }
                Enemy enemy = Instantiate(this.prefabs[row], this.transform);
                //enemy.killed += EnemyKilled();
                Vector3 position = rowPosition;
                position.x += col * 10.0f;
                enemy.transform.localPosition = position;
            }
        }
    }
    private void Start()
    {
       InvokeRepeating(nameof(Attack), this.enemyAttackRate, this.enemyAttackRate);
    }

    private void EnemyKilled()
    {
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
            if (Random.value < (1.0f / ( (float) this.totalEnemy - (float) this.amountKilled)))
            {
                Instantiate(this.EnemyAttack, enemy.position, Quaternion.identity);
                break;
            }
        }    
    }
}
