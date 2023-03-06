using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Sprite[] lifeSprites;
    private int _lifeWall;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _lifeWall = lifeSprites.Length - 1;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Life();
    }

    void Life() 
    {
        _spriteRenderer.sprite = lifeSprites[_lifeWall];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyAttack"))
        {
            _lifeWall--;
            Debug.Log("en la pared");
            if (_lifeWall < 0)
            {
                Destroy(this.gameObject);
                return;
            }
            Life();
        }        
    }

}
