using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;

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
        rigidBody.velocity = movementInput * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameController.ChangeRole();
        }
    }
}
/*In this script, the player's movement is determined by the movementInput vector, which is updated in the OnMove method in response to player input. The InputValue object passed to OnMove represents the current state of the input, and the Get<Vector2> method is used to extract a 2D vector from the input. The rigidBody.velocity property is set to the movementInput vector multiplied by the speed value, causing the player character to move in the desired direction.

Note: In this script, movement input is handled in the FixedUpdate method rather than the Update method, as is done in the previous script. This is because the new input system uses fixed-update-based inputs by default, so it's recommended to handle player movement in FixedUpdate as well to ensure smooth and consistent movement.*/