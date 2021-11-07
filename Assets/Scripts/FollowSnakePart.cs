using System;
using System.Collections.Concurrent;

using UnityEngine;

public class FollowSnakePart : MonoBehaviour
{
    private const float _minimumDistanceToFollowedPosition = 1f;

    private float _movingSpeed = 1.5f;

    private float _previousRemainingDistance = float.MaxValue;
    private Vector3? _followingPoint = null;
    private ConcurrentStack<Vector3> _pointsToFollow = new ConcurrentStack<Vector3>();
    private bool _hasFirstFollowingPointBeenSet = false;

    private GameObject _followingSnakePart;

    private void RequestNextFollowingPoint()
    {
        if (_pointsToFollow.IsEmpty || !_pointsToFollow.TryPop(out var nextPoint))
            return;

        _followingPoint = nextPoint;
    }

    private Vector3 GetCurrentFollowingPoint() =>
        /*_followingPoint ?? */_followingSnakePart.transform.position;

    private void SetNextPositionToFollow()
    {
        (var remainingDistance, var isItHeadingTowardsFollowedPoint) = SetRemainingDistance();
        if (!isItHeadingTowardsFollowedPoint)
        {
            _followingPoint = null;
            RequestNextFollowingPoint();
            return;
        }

        var isItNearFollowedPoint = remainingDistance <= _minimumDistanceToFollowedPosition;
        if (!isItNearFollowedPoint)
            return;

        RequestNextFollowingPoint();
    }

    private void FixedUpdate()
    {
        var targetDir = (GetCurrentFollowingPoint() - this.transform.position).normalized;
        this.transform.Translate(targetDir * _movingSpeed * Time.deltaTime);

        SetNextPositionToFollow();
    }

    private (float remainingDistance, bool isItHeadingTowards) SetRemainingDistance()
    {
        var distance = Vector3.Distance(this.transform.position, GetCurrentFollowingPoint());
        var isItHeadingTowards = _previousRemainingDistance >= distance;
        _previousRemainingDistance = distance;

        return (distance, isItHeadingTowards);
    }

    void AddPositionToFollow(Vector3 newFollowingPoint)
    {
        _pointsToFollow.Push(newFollowingPoint);

        if(!_hasFirstFollowingPointBeenSet)
        {
            RequestNextFollowingPoint();
            _hasFirstFollowingPointBeenSet = true;
        }
    }

    void IncreaseSpeed(float increase)
    {
        _movingSpeed += increase;
    }

    void SetFollowingSnakePart(GameObject snakePartToFollow)
    {
        _followingSnakePart = snakePartToFollow;
    }
}
