using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Movement
    [SerializeField] private GameObject ball;
    [SerializeField] private float initialSpeed = 7f;
    [SerializeField] private float accuracy = 0.8f;
    [SerializeField] private float yMaxBound = 2f;
    [SerializeField] private float yMinBound = -3f;
    private Vector2 ballPos;

    // Sound
    [SerializeField] private AudioSource hitSound;

    // Animacion
    private Animator anim;
    private bool canPunch;
    public bool isPunching = false;
    private Collider2D enemyColl;

    // Start is called before the first frame update
    void Start()
    {
        canPunch = true;
        anim = GetComponent<Animator>();
        enemyColl = GetComponent<Collider2D>();
        enemyColl.isTrigger = false;

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch")) // check if "Punch" is not playing...
        {
            hitSound.Play();
            Punch();
        }
           
        

        if (!isPunching)
            enemyColl.isTrigger = true; // Para que la pelota no rebote con el golpe
        else
            enemyColl.isTrigger = false;
    }

    private void Move()
    {
        if (ball.GetComponent<Rigidbody2D>().IsSleeping())
            return;

        accuracy = Random.Range(0.8f, 1.2f);

        ballPos = ball.transform.position;
        if(ballPos.y >= yMinBound && ballPos.y <= yMaxBound) {
            if (transform.position.y > ballPos.y)
                transform.position += new Vector3(0, (- initialSpeed + accuracy) * Time.deltaTime);
            else
                transform.position += new Vector3(0, (initialSpeed - accuracy) * Time.deltaTime);

        }
    }

    private void Punch()
    {
        if (transform.position.x - ballPos.x >= 1.8 || transform.position.x < ballPos.x)
        {
            if (!canPunch)
                canPunch = true;
            isPunching = false;
            return;
        }

        // Si puede golpear
        if(canPunch)
            PunchValidation();

        // Si no puede
        if (!canPunch)
        {
            isPunching = false;
            return;
        }

        // Mandar la señal al componente Animator para que ejecute la animacion mencionada
        anim.SetTrigger("Punch");
        isPunching = true;
      
    }

    private void PunchValidation()
    {
        float Rand = Random.Range(0, 20);
        if (Rand >= 7 && Rand <= 9)
            canPunch = false;
        else
            canPunch = true;
    }

}
