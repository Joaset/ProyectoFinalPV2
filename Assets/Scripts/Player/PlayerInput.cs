using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float ApretarBotonMover()
    {
        return Input.GetAxis("Horizontal");
    }

    public bool ApretarBottonSalto()
    {
        return Input.GetButton("Jump");
    }

    public bool ApretarBotonDisparo()
    {
       return Input.GetButtonDown("Fire1");
    }
}
