using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    private int scoreno = 0;

    [SerializeField] private Text ScoreText;

    [SerializeField] private AudioSource CollectSound;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Score")) {
            CollectSound.Play();
            Destroy(collision.gameObject);
            scoreno++;
            ScoreText.text = "Score: " + scoreno;
        }
    }
}
