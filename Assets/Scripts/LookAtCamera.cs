using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        void Update()
        {
            Camera cam = Camera.main;
            if (cam != null)
            {
                Vector3 direction = cam.transform.position - transform.position;
                direction.y = 0; // Esto asegura que el texto no se incline
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
