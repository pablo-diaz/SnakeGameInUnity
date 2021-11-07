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
        SetYouLostLabelVisibility(true, losingReason: "you hit the snake body");
        StopGame();
    }

    private void ProcessBoundaryHit()
    {
        SetYouLostLabelVisibility(true, losingReason: "you hit a game wall");
        StopGame();
    }

    private void SetYouLostLabelVisibility(bool visible, string losingReason = null)
    {
        YouLostLabel.enabled = visible;
        if (!string.IsNullOrEmpty(losingReason))
            YouLostLabel.text += $", because {losingReason}";
    }

    private void StopGame()
    {
        this.SendMessage("StopMovement");
    }
}
