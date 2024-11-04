using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
   [SerializeField] string nombre;
   private void OnTriggerEnter2D(Collider2D collision) 
   {
        if (nombre == "puerta")
        {
            if (collision.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(2);
            }
        }
        if (nombre == "puertaFinal")
        {
            if (collision.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(3);
                AudioManager.Instance.StopAudio(AudioManager.Instance.backgroundMusic);
            }
        }
    }
}
