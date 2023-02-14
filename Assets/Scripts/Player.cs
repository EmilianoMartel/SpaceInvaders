using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //float canShoot=10;
    float xPosition;
    float yPosition;
    float zPosition;
    public GameObject PlayerAttack;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        xPosition = gameObject.transform.position.x;
        yPosition = gameObject.transform.position.y;
        zPosition = gameObject.transform.position.z;
    }

    private void Move() //movimiento
    {
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            gameObject.transform.Translate(-10f * Time.deltaTime,0,0);
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            gameObject.transform.Translate(10f * Time.deltaTime, 0, 0);
        }
    }

    void Shoot() //disparos
    {
        if (Input.GetKey("up"))
        {
            Instantiate(PlayerAttack);
        }
    }
}
