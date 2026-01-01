using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerStats playerStats;
    PlayerAnimator playerAnimator;

    CharacterController controller;

    private float horizontalInput;
    private float verticalInput;
    private float rotationVelocity;
    private Vector3 moveDirection;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        controller = GetComponentInChildren<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }
    private void Update()
    {
        if(playerStats.GetPlayerIsDead())
        { return; }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.V))
        {
            playerStats.TakeDamage(30);
        }
    }
    private void FixedUpdate()
    {
        if (playerStats.GetPlayerIsDead()) {
            return;
        }
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if(direction.sqrMagnitude >= 0.01f)
        {
            // Calculate the target angle in degrees
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            // Smoothly rotate towards the target angle
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            // Move in the target direction
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * playerStats.GetMoveSpeed() * Time.fixedDeltaTime);

            playerAnimator.PlayerMove(true);
        }
        else
        {
            playerAnimator.PlayerMove(false);
        }
    }
}
