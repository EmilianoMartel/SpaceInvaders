using System;
using UnityEngine;

public class EnemyWall : MonoBehaviour
{
    public EnemyManager enemyManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyManager.EnemyTouch = true;
            enemyManager.EndGame();
        }
    }
}
