using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PlayerCamera : MonoBehaviourPunCallbacks
{
    public CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        if (photonView.IsMine)
        {
            virtualCamera.gameObject.SetActive(true);
        }
        else
        {
            virtualCamera.gameObject.SetActive(false);
        }
    }
}
