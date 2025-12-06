using UnityEngine;

public class LiftButton : MonoBehaviour
{
    public LiftController lift; // drag lift di Inspector

    private bool isPressed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPressed && collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            isPressed = true;
            lift.ActivateButton();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPressed && collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            isPressed = false;
            lift.DeactivateButton();
        }
    }
}
