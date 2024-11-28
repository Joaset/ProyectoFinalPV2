using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable, IMovable
{
    [SerializeField] private float velocidad;
    [SerializeField] private Transform sueloEnemigo;
    [SerializeField] private Transform detectarPared;
    [SerializeField] private Transform detectarSuelo;
    private Rigidbody2D rigidEnemigo;
    [SerializeField] private bool enemigoQuieto;
    private Animator anim;
    [SerializeField] private bool enemigoCaminando;
    private bool caminaDerecha;
    [SerializeField] private bool paredDetectada;
    [SerializeField] private bool sueloDetectado;
    [SerializeField] private bool estaSuelo;
    [SerializeField] private float radioDeteccion;
    [SerializeField] private LayerMask tipoSuelo;
    void Start()
    {
        healthPoints = 2;
        rigidEnemigo = GetComponent<Rigidbody2D>();
        velocidad = 120f;
        anim = GetComponent<Animator>();
        dañoCausado = 10f;
    }

    void Update()
    {
        sueloDetectado = !Physics2D.OverlapCircle(sueloEnemigo.position, radioDeteccion, tipoSuelo);
        paredDetectada = Physics2D.OverlapCircle(detectarPared.position, radioDeteccion, tipoSuelo);
        estaSuelo = Physics2D.OverlapCircle(detectarSuelo.position, radioDeteccion, tipoSuelo);

        if (sueloDetectado || paredDetectada && estaSuelo)
        {
            Girar();
        }
    }

    private void FixedUpdate()
    {
        if (enemigoQuieto)
        {
            anim.SetBool("idle", true);
            rigidEnemigo.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (enemigoCaminando)
        {

            Mover();
        }
    }

    private void Girar()
    {
        caminaDerecha = !caminaDerecha;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    public void Mover()
    {
        rigidEnemigo.constraints = RigidbodyConstraints2D.FreezeRotation;
        anim.SetBool("idle", false);
        if (!caminaDerecha)
        {

            rigidEnemigo.velocity = new Vector2(velocidad * Time.deltaTime, rigidEnemigo.velocity.y);
        }
        else
        {
            rigidEnemigo.velocity = new Vector2(-velocidad * Time.deltaTime, rigidEnemigo.velocity.y);
        }
    }

    public void RecibirDaño(float daño)
    {
        healthPoints -= daño;

        if (healthPoints <= 0)
        {
            Morir();
        }
    }
}
