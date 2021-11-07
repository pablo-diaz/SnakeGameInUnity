using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeHeadMovement : MonoBehaviour
{
    private float _movingSpeed = 1.5f;
    private Vector3 _movementVector = Vector3.forward;

    public GameObject NextSnakeBodyPart;

    private void Start()
    {
        NextSnakeBodyPart.SendMessage("SetFollowingSnakePart", this.gameObject);
    }

    void OnMove(InputValue movementVector)
    {
        var keyboardCoords = movementVector.Get<Vector2>();
        if (!WasMovementRequested(keyboardCoords))
            return;

        this._movementVector = new Vector3(keyboardCoords.x, 0, keyboardCoords.y);
    }

    private bool WasMovementRequested(Vector2 coords) =>
        coords != Vector2.zero;

    private void FixedUpdate()
    {
        this.transform.Translate(_movementVector * _movingSpeed * Time.deltaTime);
    }

    void IncreaseSpeed(float increase)
    {
        _movingSpeed += increase;
        NextSnakeBodyPart.SendMessage("IncreaseSpeed", increase);
    }
}
