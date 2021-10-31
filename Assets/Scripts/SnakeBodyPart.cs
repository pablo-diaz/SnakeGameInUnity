using UnityEngine;

public class SnakeBodyPart : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    void JoinToSnakeBodyPart(Rigidbody snakeBodyPart)
    {
        var joint = gameObject.AddComponent<CharacterJoint>();
        joint.connectedBody = snakeBodyPart;
        joint.anchor = new Vector3(0, 0, -0.5f);
        joint.connectedAnchor = new Vector3(0, 0, 1);
        joint.lowTwistLimit = new SoftJointLimit() { limit = -15 };
        joint.highTwistLimit = new SoftJointLimit() { limit = 15 };
        joint.swing1Limit = new SoftJointLimit() { limit = 5 };
        joint.swing2Limit = new SoftJointLimit() { limit = 5 };
    }
}
