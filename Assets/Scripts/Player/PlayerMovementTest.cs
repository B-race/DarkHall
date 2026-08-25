using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float runMultiplier = 1.5f;

    private Vector2 moveDirection = Vector2.zero;
    private bool isRunning = false;

    private void Update()
    {
        float currentSpeed = moveSpeed;

        if (isRunning)
        {
            currentSpeed *= runMultiplier;
        }

        Vector3 movement = new Vector3(
            moveDirection.x,
            moveDirection.y,
            0f
        );

        transform.Translate(
            movement * currentSpeed * Time.deltaTime,
            Space.World
        );
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void StopMoving()
    {
        moveDirection = Vector2.zero;
    }

    public void StartRunning()
    {
        isRunning = true;
    }

    public void StopRunning()
    {
        isRunning = false;
    }
}