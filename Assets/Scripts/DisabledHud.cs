using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisabledHud : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private GameObject player;
    
    void Update()
    {
        if (player == null)
        {
            canvas.enabled = false;
        }
        else if (player != null)
        {
            canvas.enabled = true;
        }
    }

    public void SetPlayer( GameObject playerPrefab )
    {
        player = playerPrefab;
    }
}
