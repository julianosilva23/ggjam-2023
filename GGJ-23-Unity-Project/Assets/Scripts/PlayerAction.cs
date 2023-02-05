using UnityEngine;
using UnityEngine.InputSystem;
//using System.Collections;

public class PlayerAction : MonoBehaviour
{
    public float dashDuration = 0.2f;
    public float dashSpeed = 5f;
    public float energy = 100f;
    public float maxEnergy = 100f;
    public float dashCost = 60f;
    public float shieldCastDuration = 0.5f;
    public float shieldCastCost = 80f;
    public float energyRecoverySpeed = 50f;

    public ScreenShake screenShake;
    public RectTransform greenGauge;
    public RectTransform redGauge;

    public GameObject shield;

    private Rigidbody2D rb2d;
    private float dashTime;
    private bool isDashing;
    private float shieldCastTime;
    private bool isCastingShield;
    private PlayerController playerController;

    private void Start()
    {
        // remover esta parte é somente um teste
        if (screenShake != null)
        {
            screenShake.TriggerShake();
        }
    }

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
            //Debug.Log(isDashing);
            energy -= dashCost;
        }
    }

    private void OnActionShieldCast(InputValue context)
    {
        if (!isCastingShield && shieldCastCost <= energy)
        {
            //Debug.Log("Shield!!!");
            shieldCastTime = shieldCastDuration;
            isCastingShield = true;
            energy -= shieldCastCost;
            playerController.SetVulnerability(false);
            shield.SetActive(true);
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
                playerController.SetVulnerability(true);
                Debug.Log("Shield Over...");
                shield.SetActive(false);
            }
        }

        if (energy < maxEnergy)
        {
            energy += energyRecoverySpeed * Time.fixedDeltaTime;
            //Debug.Log(energy);
        }
        GaugeUpdate();
    }

    private void GaugeUpdate()
    {
        greenGauge.sizeDelta = new Vector2((energy / maxEnergy) * redGauge.sizeDelta.x, greenGauge.sizeDelta.y);
    }
}