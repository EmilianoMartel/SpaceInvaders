using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int count=0;
    public float speed = 2f;

    private Vector3 direction = Vector2.right;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += direction * this.speed * Time.deltaTime;
        count++;
    }
        
    
}
