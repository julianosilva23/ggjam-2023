using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public bool vulnerable = true;
    public bool canMove = true;

    public float score = 0;

    private Rigidbody2D rigidBody;
    private Vector2 movementInput;

    private GameController gameController;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && vulnerable)
        {
            gameController.ChangeRole();
        }
    }

    public void SetVulnerability(bool isVulnerable)
    {
        vulnerable = isVulnerable;
    }
}
