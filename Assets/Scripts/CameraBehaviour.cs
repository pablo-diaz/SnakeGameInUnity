using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject SnakeHeadToLook;
    private Vector3 _offset;

    void Start()
    {
        this._offset = transform.position - SnakeHeadToLook.transform.position;
    }

    void LateUpdate()
    {
        transform.position = SnakeHeadToLook.transform.position + _offset;
    }
}
