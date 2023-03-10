using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(0, 10f * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.destroyed.Invoke();
        Destroy(this.gameObject);        
    }
}
