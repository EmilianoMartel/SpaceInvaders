using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Sprite[] animationSprites;

    public float animationTime = 1.0f;

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
        this.transform.position += _direction * this.speed * Time.deltaTime; //para manejar la velocidad

        //Variables para el movimiento, esto nos da el punto de la derecha y de la izquierda
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform enemy in this.transform) //movimiento de los enemigos
        {
            if(enemy.gameObject.activeInHierarchy) //pregunta si esta activo
            {
                continue;
            }
            if(_direction == Vector3.right && enemy.position.x >= (rightEdge.x - 1.0f)) //si va a la derecha y choca con el borde esto
            {
                AdvanceRow();
            }else if (_direction == Vector3.left && enemy.position.x >= (leftEdge.x + 1.0f)) //si va a la izquierda y choca con el borde esto
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
}
