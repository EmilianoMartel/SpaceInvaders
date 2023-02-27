using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Enemy : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    public System.Action<Enemy> killed;
    public static float direction = 1.0f;
    public static bool change;

    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;
    private bool _right;
    private bool _left;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime); //cuando arranca le da la animacion
    }

    // Update is called once per frame
    void Update()
    {        
        Move();
        AdvanceRow();
        if (gameObject.transform.position.x >= 30) //si va a la derecha y choca con el borde esto
        {
            Debug.Log("derecha");
            Enemy.change = true;
            _right = true;            
            AllEnemy();

        }
        else if (gameObject.transform.position.x <= -30f) //si va a la izquierda y choca con el borde esto
        {
            Debug.Log("izquierda");
            Enemy.change = true;            
            _left = true;
            AllEnemy();
        }
    }
    private void Move()
    {
        gameObject.transform.Translate(direction * Time.deltaTime, 0, 0);
    }

    private void AdvanceRow()
    {
        if (change)
        {
            Vector3 position = gameObject.transform.position;
            position.y -= 1;
            gameObject.transform.position = position;
            change = false;
            Debug.Log("abajo");
        }        
    }

    private void AllEnemy()
    {
        if (_right)
        {
            Enemy.direction = -1.0f;
            _right= false;
        }
        if (_left)
        {
            Enemy.direction = 1.0f;
            _left= false;
        }        
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
            //this.killed.Invoke();
            this.gameObject.SetActive(false);
           // Destroy(this.gameObject);
        }        
    }

    
}
