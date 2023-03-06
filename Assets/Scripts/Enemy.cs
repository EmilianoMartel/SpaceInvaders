using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public delegate void EnemyEvent(Enemy enemy);

public class Enemy : MonoBehaviour
{
    private float direction = 1.0f;
    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;
    private const float _initSpeed = 1.0f; //le ponemos prefijo const para que no se toque

    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    public EnemyEvent OnKilled;
    public static Action Killed;
    public float speed=1.0f;

    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime); //cuando arranca le da la animacion
    }

    void Update()
    {
        Move();        
        if (gameObject.transform.position.x >= 30) //si va a la derecha y choca con el borde esto
        {
            Enemys.OnWallTouched?.Invoke();

        }
        else if (gameObject.transform.position.x <= -30f) //si va a la izquierda y choca con el borde esto
        {
            Enemys.OnWallTouched?.Invoke();
        }
    }

    private void Move()
    {            
        gameObject.transform.Translate(direction * speed * Time.deltaTime, 0, 0);
    }    

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void AnimateSprite() //funcion de la animacion
    {
        _animationFrame++;
        if(_animationFrame>= animationSprites.Length) //si es = de grande que la cantidad de frames lo vuelve a 0
        {
            _animationFrame = 0;
        }

        _spriteRenderer.sprite = this.animationSprites[_animationFrame];
    }
       
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            OnKilled?.Invoke(this);
            Destroy(this.gameObject);
        }        
    }

    internal void SwapDirection()
    {
        direction *= -1.0f;
        transform.position += Vector3.down * 0.2f;
    }

    internal void HighSpeed(float PercentKill)
    {
        if (speed >= 5)
        {
            speed = 5;
        }
        speed += _initSpeed * PercentKill;
    }
}
