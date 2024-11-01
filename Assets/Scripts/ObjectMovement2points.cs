using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement2points : MonoBehaviour
{
    [SerializeField] Transform[] puntos;
    [SerializeField] float velocidad;

    void Start()
    {
        StartCoroutine("MoverPlataforma");
    }

    IEnumerator MoverPlataforma()
    {
        int i = 1;
        Vector2 nuevaPosicion = new Vector2(puntos[i].position.x, puntos[i].position.y);
        while (true)
        {
            while (Vector2.Distance(transform.position, nuevaPosicion) > 0.001f)
            {
                transform.position = Vector2.MoveTowards(transform.position, nuevaPosicion, velocidad * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(1);
            if (i < 1) i++; else i = 0;
            nuevaPosicion = new Vector2(puntos[i].position.x, puntos[i].position.y);
        }
    }
}
