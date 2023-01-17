using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Sprites from: https://toppng.com/free-image/ikachu-db-sprites-pikachu-sprite-sheet-PNG-free-PNG-Images_216266
// Tutorial and Sprites from: https://www.youtube.com/watch?v=gB1F9G0JXOo&t=15730s
public class Camera : MonoBehaviour
{
    private Transform player;

    private Vector3 tempPos; 

    [SerializeField]
    private float minX, maxX;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tempPos = transform.position;
        tempPos.x = player.position.x;

        if (tempPos.x < minX)
        {
            tempPos.x = minX;
        }

        if (tempPos.x > maxX)
        {
            tempPos.x = maxX;
        }

        transform.position = tempPos;
    }
}
