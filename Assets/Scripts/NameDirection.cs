using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameDirection : MonoBehaviour
{
    [SerializeField] private new RectTransform name;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            name.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            name.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
