using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Enemy : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    public System.Action<Enemy> killed;

    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;


    private Vector3 _direction = Vector2.right;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime); //cuando arranca le da la animacion
    }

    // Update is called once per frame
    void Update()
    {
        
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
