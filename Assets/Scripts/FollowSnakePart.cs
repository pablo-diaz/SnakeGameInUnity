using System;
using System.Collections.Concurrent;

using UnityEngine;

public class FollowSnakePart : MonoBehaviour
{
    private const float _minimumDistanceToFollowedSnakePart = 1.5f;

    private GameObject _followingSnakePart;

    private void FixedUpdate()
    {
        if (GetDistanceToFollowedSnakePart() <= _minimumDistanceToFollowedSnakePart)
            return;

        this.Move();
    }

    private void Move()
    {
        this.transform.LookAt(_followingSnakePart.transform);
        var lookAt = Quaternion.LookRotation(_followingSnakePart.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookAt, SnakeHeadMovement.SnakeMovingSpeed * Time.deltaTime);
        this.transform.Translate(0, 0, SnakeHeadMovement.SnakeMovingSpeed * Time.deltaTime);
    }

    private float GetDistanceToFollowedSnakePart() =>
        Vector2.Distance(
            new Vector2(this.transform.position.x, this.transform.position.z),
            new Vector2(_followingSnakePart.transform.position.x, _followingSnakePart.transform.position.z));

    void SetFollowingSnakePart(GameObject snakePartToFollow)
    {
        _followingSnakePart = snakePartToFollow;
    }
}
