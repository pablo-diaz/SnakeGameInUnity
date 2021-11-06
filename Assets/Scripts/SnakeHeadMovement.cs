using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeHeadMovement : MonoBehaviour
{
    private float _movingSpeed = 1.5f;
    private Vector3 _movementVector = Vector3.forward;

    void OnMove(InputValue movementVector)
    {
        var keyboardCoords = movementVector.Get<Vector2>();
        if (keyboardCoords.x == 0 && keyboardCoords.y == 0)
            return;

        this._movementVector = new Vector3(keyboardCoords.x, 0, keyboardCoords.y);
    }

    private void FixedUpdate()
    {
        transform.Translate(_movementVector * _movingSpeed * Time.deltaTime);
    }

    void IncreaseSpeed(float increase)
    {
        _movingSpeed += increase;
    }
}
