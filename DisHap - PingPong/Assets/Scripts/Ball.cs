using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 4f;
    [SerializeField] private float speedMultiplier = 1.1f;
    private Rigidbody2D ballRb;
    private bool isLaunched = false;

    // Sound
    [SerializeField] private AudioSource bounceSound;

    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        ballRb.Sleep();
        Invoke("Launch", 2.0f);
    }

    void Launch()
    {
        isLaunched = true;
        ballRb.WakeUp();
        float yDirection = Random.Range(0, 2) == 0 ? 1 : -1;
        float xDirection = Random.Range(0, 2) == 0 ? 1 : -1;
        ballRb.velocity = new Vector2(xDirection, yDirection) * initialSpeed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
            ballRb.velocity *= speedMultiplier;

        if (collision.gameObject.CompareTag("Limit"))
            bounceSound.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Goal1"))
        {
            GameManager.Instance.EnemyScored();
            GameManager.Instance.Restart();
            ballRb.Sleep();
            isLaunched = false;
            Invoke("Launch", 1.5f);
        }

        if (collision.gameObject.CompareTag("Goal2"))
        {
            GameManager.Instance.PlayerScored();
            GameManager.Instance.Restart();
            ballRb.Sleep();
            isLaunched = false;
            Invoke("Launch", 1.5f);
        }
    }

}
