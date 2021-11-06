using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeHeadMovement : MonoBehaviour
{
    private float _movingSpeed = 1.5f;
    private Vector3 _movementVector = Vector3.forward;

    public GameObject NextSnakeBodyPart;

    private void CommunicateTurningPosition(Vector3 direction, Vector3 turningPoint)
    {
        NextSnakeBodyPart.SendMessage("AddPositionToFollow", (direction, turningPoint));
    }

    void OnMove(InputValue movementVector)
    {
        var keyboardCoords = movementVector.Get<Vector2>();
        if (!WasMovementRequested(keyboardCoords))
            return;

        this._movementVector = new Vector3(keyboardCoords.x, 0, keyboardCoords.y);

        CommunicateTurningPosition(this._movementVector, this.transform.position);
    }

    private bool WasMovementRequested(Vector2 coords) =>
        coords != Vector2.zero;

    private void FixedUpdate()
    {
        transform.Translate(_movementVector * _movingSpeed * Time.deltaTime);
    }

    void IncreaseSpeed(float increase)
    {
        _movingSpeed += increase;
        NextSnakeBodyPart.SendMessage("IncreaseSpeed", increase);
    }
}
