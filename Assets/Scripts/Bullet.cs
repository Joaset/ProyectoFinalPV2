using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed;
    private float daño;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 10f;
        daño = 1f;    
    }

    
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable.RecibirDaño(daño);
            Destroy(gameObject);
        }
    }
}
