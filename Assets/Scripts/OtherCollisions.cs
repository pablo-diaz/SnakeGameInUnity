using UnityEngine;

public class OtherCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (DidItHitSnakeBody(collision.gameObject))
            ProcessSnakeBodyHit();
        else if (DidItHitBoundary(collision.gameObject))
            ProcessBoundaryHit();
    }

    private bool DidItHitSnakeBody(GameObject gameObjectInCollision) =>
        gameObjectInCollision.tag == "SnakeBody";

    private bool DidItHitBoundary(GameObject gameObjectInCollision) =>
        gameObjectInCollision.tag == "Boundary";

    private void ProcessSnakeBodyHit()
    {
        print("Se ha chocado contra el cuerpo");
    }

    private void ProcessBoundaryHit()
    {
        print("Se ha chocado contra el límite");
    }
}
