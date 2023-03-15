using System;
using UnityEngine;

public class EnemyWall : MonoBehaviour
{
    public Enemys enemys;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemys.EnemyTouch = true;
            enemys.EndGame();
        }
    }
}
