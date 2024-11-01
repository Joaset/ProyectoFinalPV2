using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiempoDeExplosion : MonoBehaviour
{
    [SerializeField] private float tiempo;

    void Start()
    {
        Destroy(gameObject, tiempo);
    }

}
