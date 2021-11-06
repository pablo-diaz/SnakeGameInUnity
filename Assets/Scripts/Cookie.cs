using UnityEngine;

public class Cookie : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    void SetPosition(Vector2 position)
    {
        this.transform.position = new Vector3(position.x, this.transform.position.y, position.y);
    }
}
