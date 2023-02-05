using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public bool vulnerable = true;
    public bool canMove = true;
    public ScreenShake screenShake;

    public float score = 0;

    public Transform gaugeCanvas;

    //public GameObject arrow;
    //public GameObject flag;
    public GameObject aoe;

    private Rigidbody2D rigidBody;
    private Vector2 movementInput;

    private GameController gameController;

    private void Start()
    {
        // verificar o jogar 1 e 2
        if (gameController.player1 == null)
        {
            gameController.player1 = this;
            transform.position = gameController.initialPosition1;
                    
        } else {
            gameController.player2 = this;
            transform.position = gameController.initialPosition2;
            this.SetFlag(true);
            gameController.buildPlayers();
        }
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
    }

    private void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rigidBody.velocity = movementInput * speed;

            if (movementInput.sqrMagnitude > 0)
            {
                float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
                rigidBody.rotation = angle;
            }
        }
    }

    private void Update()
    {
        gaugeCanvas.position =  transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("hunter " + vulnerable);
        if (other.gameObject.CompareTag("Player") && vulnerable)
        {
            Debug.Log("player " + other.gameObject.GetComponent<PlayerController>().vulnerable);
            if(other.gameObject.GetComponent<PlayerController>().vulnerable)
            {
                SetVulnerability(false);
                gameController.ChangeRole();
                SetVulnerability(true);

                if (screenShake != null)
                {
                    screenShake.TriggerShake();
                }
            }
        }
    }

    public void SetVulnerability(bool isVulnerable)
    {
        vulnerable = isVulnerable;
    }

    public void SetFlag(bool isFlagged)
    {
        //arrow.SetActive(!isFlagged);
        //flag.SetActive(isFlagged);
        aoe.SetActive(isFlagged);
    }
}
