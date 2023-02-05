using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public bool vulnerable = true;
    public bool canMove = true;

    public float score = 0;
    public float stunDuration = 3;
    public float stunnedTime;

    public Transform gaugeCanvas;

    public GameObject arrow;
    public GameObject flag;
    public GameObject aoe;

    private Rigidbody2D rigidBody;
    private Vector2 movementInput;

    public GameController gameController;

    private void Start()
    {
        // verificar o jogar 1 e 2
        stunnedTime = stunDuration + 1;
        if (gameController.player1 == null)
        {
            gameController.player1 = this;
            transform.position = gameController.initialPosition1;
            this.SetFlag(true);
                    
        } else {
            gameController.player2 = this;
            transform.position = gameController.initialPosition2;
            this.SetFlag(false);
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
        if (canMove && !Stunned())
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
        if (Stunned()){
            Debug.Log("Stunned!!!");
            stunnedTime += Time.deltaTime;
            if(Stunned())
            {
                SetVulnerability(true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("shield" + gameObject.GetComponent<PlayerAction>().isShieldActive());
        if(gameObject.GetComponent<PlayerAction>().isShieldActive() && 
            (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Hunter")))
        {
            other.gameObject.GetComponent<PlayerController>().stunDuration = 0;
            other.gameObject.GetComponent<PlayerController>().SetVulnerability(false);
            return;
        }else if (other.gameObject.CompareTag("Player") && vulnerable)
        {
            Debug.Log("player " + other.gameObject.GetComponent<PlayerController>().vulnerable);
            if(other.gameObject.GetComponent<PlayerController>().vulnerable)
            {
                gameController.TriggerShake();

                SetVulnerability(false);
                gameController.ChangeRole();
                SetVulnerability(true);                
            }
        }
    }

    public void SetVulnerability(bool isVulnerable)
    {
        vulnerable = isVulnerable;
    }

    public void SetFlag(bool isFlagged)
    {
        arrow.SetActive(!isFlagged);
        flag.SetActive(isFlagged);
        aoe.SetActive(isFlagged);
    }

    public void SetActiveCollider(bool isActive)
    {
        GetComponent<CapsuleCollider2D>().enabled = isActive;
    }
    
    public bool Stunned()
    {
        
        if(stunnedTime < stunDuration)
        {
            Debug.Log("STUNNED!!!");
            return true;
        }
        return false;
    }
}
