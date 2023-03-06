using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //float canShoot=10;
    public PlayerAttack PlayerAttack;
    public int life;
    private bool _laserActive;    

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
            
    }

    private void Move() //movimiento
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) && gameObject.transform.position.x >= -30f)
        {
            gameObject.transform.Translate(-10f * Time.deltaTime,0,0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) && gameObject.transform.position.x <= 30f)
        {
            gameObject.transform.Translate(10f * Time.deltaTime, 0, 0);
        }
    }

    void Shoot() //disparos
    {
        if (!_laserActive)
        {
            PlayerAttack PlayerAttack = Instantiate(this.PlayerAttack, this.transform.position, Quaternion.identity);
            PlayerAttack.destroyed += LaserDestroyed;
            _laserActive = true;
        }
    }

    void LaserDestroyed()
    {
        _laserActive = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        life--;
        if (life == 0)
        {
            Destroy(gameObject);
        }
    }
}
