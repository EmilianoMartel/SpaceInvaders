using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float xPosition;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        xPosition = Player.transform.position.x;
        gameObject.transform.Translate(xPosition, 0 , 0);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(0, 10f * Time.deltaTime, 0);
    }

}
