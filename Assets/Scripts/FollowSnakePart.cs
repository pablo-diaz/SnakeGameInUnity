using System;
using System.Collections.Concurrent;

using UnityEngine;

public class FollowSnakePart : MonoBehaviour
{
    private const float _minimumDistanceToFollowedPosition = 1f;

    private float _movingSpeed = 1.5f;

    private GameObject _followingSnakePart;

    private void FixedUpdate()
    {
        var targetDir = (_followingSnakePart.transform.position - this.transform.position).normalized;
        this.transform.Translate(targetDir * _movingSpeed * Time.deltaTime);
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
