using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamara : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float xPos;
    [SerializeField] float yPos;
    [SerializeField] float zPos;

    void Start()
    {
        transform.position = new Vector3(player.position.x + xPos, player.position.y + yPos, zPos);
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x + xPos, player.position.y + yPos, zPos);

        if (player.GetComponent<Rigidbody2D>().velocity.x < -1)
        {
            xPos = (float)-0.05;
            transform.rotation = new Quaternion(0, -178, 0, 0);
            zPos = 10;
            transform.position = new Vector3(player.position.x - xPos, player.position.y + yPos, zPos);
        }
        if (player.GetComponent<Rigidbody2D>().velocity.x > 1)
        {
            xPos = (float)0.05;
            transform.rotation = new Quaternion(0, 0, 0, 0);
            zPos = -10;
            transform.position = new Vector3(player.position.x + xPos, player.position.y + yPos, zPos);
        }

        //if (player == null)
        //{
        //    Destroy(this);
        //}
    }
}
