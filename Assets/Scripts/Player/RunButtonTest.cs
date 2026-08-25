using UnityEngine;
using UnityEngine.EventSystems;

public class RunButtonTest : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    [SerializeField] private PlayerMovementTest playerMovement;

    public void OnPointerDown(PointerEventData eventData)
    {
        playerMovement.StartRunning();
        Debug.Log("Run Start");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerMovement.StopRunning();
        Debug.Log("Run Stop");
    }
}