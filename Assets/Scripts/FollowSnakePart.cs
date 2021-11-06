using System.Collections.Concurrent;

using UnityEngine;

public class FollowSnakePart : MonoBehaviour
{
    private const float _minimumDistanceToFollowedPosition = 0.1f;

    private float _movingSpeed = 1.5f;

    private Vector3? _nextDirection = null;
    private Vector3 _currentDirectionVector = Vector3.forward;
    private Vector3? _turningPoint = null;
    private ConcurrentStack<(Vector3 direction, Vector3 turningPoint)> _positionsToFollow =
        new ConcurrentStack<(Vector3 direction, Vector3 turningPoint)>();

    private float? previousRemainingDistance = null;

    private void RequestNextTurningPoint()
    {
        if (_positionsToFollow.IsEmpty || !_positionsToFollow.TryPop(out var nextPosition))
            return;

        _nextDirection = nextPosition.direction;
        _turningPoint = nextPosition.turningPoint;
    }

    private void SetNextPositionToFollow()
    {
        if(!_turningPoint.HasValue && !_positionsToFollow.IsEmpty)
            RequestNextTurningPoint();

        var remainingDistance = GetRemainingDistance();

        var isItHeading = !previousRemainingDistance.HasValue
            ? true
            : previousRemainingDistance.Value > remainingDistance;

        if(!isItHeading && !_positionsToFollow.IsEmpty)
            RequestNextTurningPoint();

        if (remainingDistance <= _minimumDistanceToFollowedPosition)
        {
            _turningPoint = null;
            _currentDirectionVector = _nextDirection.Value;
            RequestNextTurningPoint();
        }

        previousRemainingDistance = remainingDistance;
    }

    private void FixedUpdate()
    {
        SetNextPositionToFollow();
        transform.Translate(_currentDirectionVector * _movingSpeed * Time.deltaTime);
    }

    private float GetRemainingDistance() =>
        _turningPoint.HasValue
        ? Vector3.Distance(transform.position, _turningPoint.Value)
        : float.MaxValue;

    void AddPositionToFollow((Vector3 direction, Vector3 turningPoint) position)
    {
        _positionsToFollow.Push(position);
    }

    void IncreaseSpeed(float increase)
    {
        _movingSpeed += increase;
    }
}
