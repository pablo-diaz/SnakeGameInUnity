using UnityEngine;

using TMPro;

public class OtherCollisions : MonoBehaviour
{
    public TextMeshProUGUI YouLostLabel;

    private void Start()
    {
        SetYouLostLabelVisibility(false);
    }

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
        SetYouLostLabelVisibility(true);
        StopGame();
    }

    private void ProcessBoundaryHit()
    {
        SetYouLostLabelVisibility(true);
        StopGame();
    }

    private void SetYouLostLabelVisibility(bool visible)
    {
        YouLostLabel.enabled = visible;
    }

    private void StopGame()
    {

    }
}
