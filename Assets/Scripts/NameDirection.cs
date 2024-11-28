using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class NameDirection : MonoBehaviourPunCallbacks
{
    [SerializeField] private new RectTransform name;

    void Update()
    {
        if (photonView.IsMine)
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
}
