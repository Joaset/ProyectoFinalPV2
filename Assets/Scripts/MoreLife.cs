using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreLife : MonoBehaviour
{
    [SerializeField] public int aumentoVida;

    private void Start()
    {
        aumentoVida = 10;
    }


    public void Morir()
    {
        Destroy(gameObject);
    }
}
