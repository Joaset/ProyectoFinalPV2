using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public void IniciarMenuPrincipal()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.ResetVida();
        AudioManager.Instance.PlayAudio(AudioManager.Instance.menuMusic);
    }
}
