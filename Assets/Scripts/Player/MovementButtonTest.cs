using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButtonTest : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    [SerializeField] private PlayerMovementTest playerMovement;
    [SerializeField] private Vector2 moveDirection;

    public void OnPointerDown(PointerEventData eventData)
    {
        playerMovement.SetMoveDirection(moveDirection);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerMovement.StopMoving();
    }
}