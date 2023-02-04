using UnityEngine;
using UnityEngine.InputSystem;
//using System.Collections;

public class PlayerAction : MonoBehaviour
{
    public float dashDuration = 100000f;
    public float dashSpeed = 1f;
    public float energy = 100f;
    public float maxEnergy = 100f;
    public float dashCost = 20f;
    public float shieldCastDuration = 0.5f;
    public float shieldCastCost = 50f;
    public float energyRecoverySpeed = 1f;

    private Rigidbody2D rb2d;
    private float dashTime;
    private bool isDashing;
    private float shieldCastTime;
    private bool isCastingShield;
    private PlayerController playerController;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnActionDash(InputValue context)
    {
        if (!isDashing && energy >= dashCost)
        {
            playerController.canMove = false;
            dashTime = dashDuration;
            isDashing = true;
            Debug.Log(isDashing);
            energy -= dashCost;
        }
    }

    private void OnActionShieldCast(InputAction.CallbackContext context)
    {
        Debug.Log("Shield!!!");
        if (context.performed && !isCastingShield && shieldCastCost <= energy)
        {
            shieldCastTime = shieldCastDuration;
            isCastingShield = true;
            energy -= shieldCastCost;
            playerController.SetVulnerability(true);
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            dashTime -= Time.fixedDeltaTime;
            rb2d.velocity = (transform.right * dashSpeed)* 10;
            Debug.Log(rb2d.velocity);

            if (dashTime <= 0)
            {
                isDashing = false;
                rb2d.velocity = Vector2.zero;
                playerController.canMove = true;
            }
        }

        if (isCastingShield)
        {
            shieldCastTime -= Time.fixedDeltaTime;

            if (shieldCastTime <= 0)
            {
                isCastingShield = false;
                playerController.SetVulnerability(false);
            }
        }

        if (energy < maxEnergy)
        {
            energy += energyRecoverySpeed * Time.fixedDeltaTime;
            //Debug.Log(energy);
        }
    }
}