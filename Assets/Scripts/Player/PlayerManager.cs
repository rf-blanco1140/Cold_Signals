using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // Velocidad de caminado del jugador
    public float playerWalkSpeed;

    // Velocidad de correr del jugador
    public float playerRunSpeed;

    // Booleano que indica si esta corriendo o no
    public bool isRunning;

    // Indica si el jugador puede moverse o no
    public bool canMove;

    // Vector que indica la direccion en la que se va a mover el personaje
    private Vector2 movementVector;

    // Referencia la Rigibody2D del personaje
    private Rigidbody2D playerRigidbody;

    // Referencia a la barra de hambre en la escena
    public HungerBar hungerBarReference;

    // Referencia a la barra de frio en la escena
    public ColdBar coldBarReference;

    // Referencia a la radio en la escena
    public UIManager interfaz;

    // Indica si el jugador puede utilizar la radio o no
    public bool canRadio;

    //public AudioSource sonidoEntradaAntena;

    // Indica si el jugador puede escuchar las antenas de radio o no
    private bool playAttentionSound;

    // Referencia al animator del jugador
    private Animator playerAnimator;

    // referencia al Sprite renderer del jugador
    private SpriteRenderer playerSpriteRenderer;



    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    // Use this for initialization
    void Start ()
    {
        playerRigidbody = this.GetComponent<Rigidbody2D>();
        canMove = true;
        //canRadio = true;
        playAttentionSound = false;

        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        canRadio = true;
    }

    private void Update()
    {
        // Detecta el input del boton para correr
        if (Input.GetButton("Run"))
        {
            //Debug.Log("quiere correr");
            isRunning = true;
        }
        if (Input.GetButtonUp("Run"))
        {
            isRunning = false;
        }
        if(Input.GetKey("r") && canRadio)
        {
            interfaz.toggleRadio();
            StartCoroutine(waitToRadio());
            canMove = !canMove;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Mina")
        {
            GameManager.instance.gameOver();
        }

        
    }

    public void WaitForRadio()
    {
        StartCoroutine(waitToRadio());
    }

    public IEnumerator waitToRadio()
    {
        canRadio = false;
        yield return new WaitForSeconds(0.3f);
        canRadio = true;
    }

    private void FixedUpdate()
    {
        //Deteccion del input de movimiento
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        //Llamada al metodo que mueve al juagdor
        if (canMove)
            moveCharacter(horizontalMove, verticalMove);

        if (horizontalMove != 0 || verticalMove != 0)
        {
            playerAnimator.SetBool("caminando", true);
        }
        else
        {
            playerAnimator.SetBool("caminando", false);
            playerAnimator.SetFloat("vertical", 0);
            playerAnimator.SetFloat("horizontal", 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("tag radio es " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Radio" && Input.GetKey("q"))
        {
            interfaz.cambiarRadio();
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            playAttentionSound = true;
        }

        /*if (collision.gameObject.tag == "Antena" && !sonidoEntradaAntena.isPlaying && canRadio && playAttentionSound)
        {
            sonidoEntradaAntena.Play();
            StartCoroutine(timeToPlayAttentionSoundAgain());
        }*/
    }

    // Metodos relacionados con el movimiento

    /// <summary>
    /// Mueve al personaje en base al input del jugador
    /// </summary>
    /// <param name="hMove"> movimiento horizontal </param>
    /// <param name="vMove"> movimiento vertical </param>
    /// 
    
    public void moveCharacter(float hMove, float vMove)
    {
        /*if(hMove > 0 ){
            spriteRenderer.flipX = true;
        }
        else if(hMove < 0) {
            spriteRenderer.flipX = false;
        }*/

        //playerAnimator.SetFloat("VelocityX", hMove);
        //playerAnimator.SetFloat("VelocityY", vMove);

            movementVector.Set(hMove, vMove);
            float speedToUse = playerActualSpeed();// metodo que indica cual velocidad se va a usar
            movementVector = movementVector.normalized * speedToUse * Time.deltaTime;
            Vector2 actualPosition = new Vector2(this.transform.position.x, this.transform.position.y);
            playerRigidbody.MovePosition(actualPosition + movementVector);

        playerAnimator.SetFloat("vertical", vMove);
        playerAnimator.SetFloat("horizontal", hMove);


        cambiarNiveldeFrioHambre(hMove, vMove);
        
    }

    /// <summary>
    /// Metodo que indica cual velocidad tiene el personaje en base a que boton esta presionando
    /// </summary>
    /// <returns> Velocidad que el juagdor queire usar </returns>
    public float playerActualSpeed()
    {
        // SI esta prsionando boton de correr la velocidad es la de correr
        // SI no esta presionando el boton, es la de caminar
        float speedToUse = 0f;
        if(isRunning)
        {
            speedToUse = playerRunSpeed;
        }
        else
        {
            speedToUse = playerWalkSpeed;
        }
        return speedToUse;
    }


    // Metodos que manejan lo relacionado a temperatura

    /// <summary>
    /// Metodo que detiene el efecto de enfriamiento
    /// </summary>
    public void detenerEnfriamiento()
    {
        coldBarReference.seEstaEnfriando = false;
    }

    /// <summary>
    /// Metodo que re-activa el efecto de enfiramiento
    /// </summary>
    public void activarEnfriamiento()
    {
        coldBarReference.seEstaEnfriando = true;
    }

    public void cambiarNiveldeFrioHambre(float hMove, float vMove)
    {
        if(GameManager.instance.idFranjaHoraria == 4)
        {
            if (hMove != 0 || vMove != 0)
            {
                if (isRunning)
                {
                    hungerBarReference.selectHungerLevel(3);

                }
                else
                {
                    hungerBarReference.selectHungerLevel(2);
                }
            }
            else
            {
                hungerBarReference.selectHungerLevel(1);
            }

            coldBarReference.selectValorFrio(3);
        }
        else
        {
            if (hMove != 0 || vMove != 0)
            {
                if (isRunning)
                {
                    coldBarReference.selectValorFrio(1);
                    hungerBarReference.selectHungerLevel(3);

                }
                else
                {
                    coldBarReference.selectValorFrio(2);
                    hungerBarReference.selectHungerLevel(2);
                }
            }
            else
            {
                coldBarReference.selectValorFrio(3);
                hungerBarReference.selectHungerLevel(1);
            }
        }

        
    }

    // Metodos que manejan lo relacionado a el hambre

    public void alimentarPersonaje(float valorAlimenticio)
    {
        // Llama al metodo de la hunger bar que la llena en base a un valor
        hungerBarReference.alimentarEnBaseAValor();
    }

    public IEnumerator timeToPlayAttentionSoundAgain()
    {
        playAttentionSound = false;
        yield return new WaitForSeconds(5);
        playAttentionSound = true;
    }

}
