using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public InputActionAsset inputActions;

    [SerializeField]
    private string playerOneHorizontalAxis = "Player1Horizontal";
    [SerializeField]
    private string playerOneVerticalAxis = "Player1Vertical";
    /*[SerializeField]
    private string playerTwoHorizontalAxis = "Player2Horizontal";
    [SerializeField]
    private string playerTwoVerticalAxis = "Player2Vertical";*/

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        float playerOneHorizontal = inputActions.FindAction(playerOneHorizontalAxis).ReadValue<float>();
        float playerOneVertical = inputActions.FindAction(playerOneVerticalAxis).ReadValue<float>();
        //float playerTwoHorizontal = inputActions.FindAction(playerTwoHorizontalAxis).ReadValue<float>();
        //float playerTwoVertical = inputActions.FindAction(playerTwoVerticalAxis).ReadValue<float>();

        Debug.Log("Player 1 Horizontal: " + playerOneHorizontal + " Vertical: " + playerOneVertical);
        //Debug.Log("Player 2 Horizontal: " + playerTwoHorizontal + " Vertical: " + playerTwoVertical);
    }
}