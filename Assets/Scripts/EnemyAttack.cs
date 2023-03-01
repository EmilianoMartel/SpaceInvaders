using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EnemyAttackEvent(EnemyAttack EnemyAttack);
public class EnemyAttack : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public EnemyAttackEvent destroyed;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(0, -10f * Time.deltaTime, 0);
        if(gameObject.transform.position.y <= -40)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        destroyed?.Invoke(this);
        Destroy(this.gameObject);
        Debug.Log("choque");
    }
}
