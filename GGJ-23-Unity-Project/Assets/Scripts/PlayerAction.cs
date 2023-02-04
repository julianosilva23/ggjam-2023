using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    public float dashDuration = 0.5f;
    public float dashSpeed = 10f;
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

    private void OnActionDash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing && energy >= dashCost)
        {
            dashTime = dashDuration;
            isDashing = true;
            energy -= dashCost;
        }
    }

    private void OnActionShieldCast(InputAction.CallbackContext context)
    {
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
            rb2d.velocity = transform.forward * dashSpeed;

            if (dashTime <= 0)
            {
                isDashing = false;
                rb2d.velocity = Vector2.zero;
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
        }
    }
}