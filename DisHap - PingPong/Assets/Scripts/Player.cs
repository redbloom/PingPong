using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Movimiento
    [SerializeField] private float speed = 7f;
    float movement;
    [SerializeField] private float yMaxBound = 2f;
    [SerializeField] private float yMinBound = -3f;

    // Sound
    [SerializeField] private AudioSource hitSound;

    // Animacion
    Animator anim;
    public bool isPunching = false;

    // Fisicas
    private Rigidbody2D playerRb;
    private Collider2D playerColl;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        playerColl = GetComponent<Collider2D>();
        playerColl.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Dectectar si el usuario clickeo en las teclas que estan asignadas para el movimiento vertical  
        movement = Input.GetAxisRaw("Vertical");
        Mover(movement);
        // Dectectar si el usuario clickeo en la tecla que esta asignada para saltar
        if (Input.GetButtonDown("Jump") && !isPunching)
        {
            hitSound.Play();
            Punch();
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch")) // check if "Punch" is not playing...
        {
            isPunching = false;
            playerColl.isTrigger = true; // Para que la pelota no rebote con el golpe
        }
        else
        {
            playerColl.isTrigger = false;
        }
  
    }

    void Punch()
    {
        // Mandar la señal al componente Animator para que ejecute la animacion mencionada
        anim.SetTrigger("Punch");
        isPunching = true;
    }


    void Mover(float mover)
    {
        Vector2 playerPos = transform.position;
        // Para delimitar el rango de la raqueta
        playerPos.y = Mathf.Clamp(playerPos.y + mover * speed * Time.deltaTime, yMinBound, yMaxBound);
        transform.position = playerPos;
    }
}
