using UnityEngine;
using UnityEngine.InputSystem;
//using System.Collections;

public class PlayerAction : MonoBehaviour
{
    public float dashDuration = 0.2f;
    public float jumpDuration = 1f;
    public float dashSpeed = 5f;
    public float energy = 100f;
    public float maxEnergy = 100f;

    public float dashCost = 60f;
    public float jumpCost = 60f;

    public float shieldCastDuration = 0.5f;
    public float shieldCastCost = 80f;
    public float energyRecoverySpeed = 50f;

    public ScreenShake screenShake;
    public RectTransform greenGauge;
    public RectTransform redGauge;

    public GameObject shield;

    private Rigidbody2D rb2d;
    private float dashTime;
    private float jumpTime;

    private bool isDashing;
    private bool isJumping;
    private float shieldCastTime;
    private bool isCastingShield;
    private PlayerController playerController;

    private Animator animator;

    private void Start()
    {
        // remover esta parte é somente um teste
        if (screenShake != null)
        {
            screenShake.TriggerShake();
        }

        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnActionDash(InputValue context)
    {
        if (!playerController.Stunned() && !isDashing && energy >= dashCost)
        {
            playerController.canMove = false;
            dashTime = dashDuration;
            isDashing = true;
            energy -= dashCost;
        }
    }

    private void OnActionShieldCast(InputValue context)
    {
        if (!playerController.Stunned() && !isCastingShield && shieldCastCost <= energy)
        {
            //Debug.Log("Shield!!!");
            shieldCastTime = shieldCastDuration;
            isCastingShield = true;
            energy -= shieldCastCost;
            playerController.SetVulnerability(false);
            shield.SetActive(true);
        }
    }

    private void OnActionJump(InputValue context)
    {
        Debug.Log("pulando");
        // if (!playerController.Stunned() && !isDashing && !isJumping & energy >= jumpCost)
        // {
        //     jumpTime = jumpDuration;
        //     isJumping = true;
        //     energy -= jumpCost;
        //     animator.enabled = true;
        //     animator.Play("Jump");
        // }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            dashTime -= Time.fixedDeltaTime;
            rb2d.velocity = (transform.right * dashSpeed)* 10;

            if (dashTime <= 0)
            {
                isDashing = false;
                rb2d.velocity = Vector2.zero;
                playerController.canMove = true;
            }
        }


        // // pulo
        // if (isJumping && !isDashing)
        // {
        //     Debug.Log("pulando");
        //     jumpTime -= Time.fixedDeltaTime;
        //     playerController.SetVulnerability(true);
        //     playerController.SetActiveCollider(false);

        //     if (jumpTime <= 0)
        //     {
        //         animator.enabled = false;
        //         playerController.SetVulnerability(false);
        //         playerController.SetActiveCollider(true);
        //         Debug.Log("parou de pular");
        //         isJumping = false;
        //     }
        // }        

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
        }
        GaugeUpdate();
    }

    public bool isShieldActive(){
        return isCastingShield;
    }

    private void GaugeUpdate()
    {
        greenGauge.sizeDelta = new Vector2((energy / maxEnergy) * redGauge.sizeDelta.x, greenGauge.sizeDelta.y);
    }

    public void OnActionRestart(InputValue context)
    {
        playerController.gameController.Retry();
    }    
}