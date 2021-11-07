using UnityEngine;

using TMPro;

public class EatCookie : MonoBehaviour
{
    private static System.Random _randomizer = new System.Random();

    private int _cookieCounter = 0;
    private int CookieCounter {
        get => _cookieCounter;
        set {
            _cookieCounter = value;
            CookieCountLabel.text = $"Cookies: {_cookieCounter}";
        }
    }

    public GameObject SnakeTail;
    public TextMeshProUGUI CookieCountLabel;

    private void Start()
    {
        CookieCounter = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (DidItHitACookie(collision.gameObject))
            ProcessCookieHit(collision.gameObject);
    }

    private bool DidItHitACookie(GameObject gameObjectInCollision) =>
        gameObjectInCollision.tag == "Cookie";

    private void ProcessCookieHit(GameObject cookie)
    {
        AddNewCookie(cookie);
        Eat(cookie);
        IncreaseSnakeSpeed();
        EnlargeSnakeBody();
    }

    private void Eat(GameObject cookieToEat)
    {
        Destroy(cookieToEat);
        CookieCounter++;
    }

    private void AddNewCookie(GameObject basedOnCookieTemplate)
    {
        var newCookie = Instantiate(basedOnCookieTemplate);
        newCookie.SendMessage("SetPosition", GetRandomPositionForNewCookie());
    }

    private Vector2 GetRandomPositionForNewCookie()
    {
        var x = _randomizer.Next(-10, 10);
        var z = _randomizer.Next(-10, 10);
        return new Vector2(x, z);
    }

    private void IncreaseSnakeSpeed()
    {
        this.SendMessage("IncreaseSpeed", 0.5f);
    }

    private void EnlargeSnakeBody()
    {
        var newSnakeBodyPart = CreateNewSnakeBodyPart();
        SetNewTail(newSnakeBodyPart);
    }

    private GameObject CreateNewSnakeBodyPart()
    {
        var newSnakeBodyPart = Instantiate(SnakeTail);
        newSnakeBodyPart.transform.position = GetNextTailPosition();
        newSnakeBodyPart.SendMessage("SetFollowingSnakePart", SnakeTail);
        return newSnakeBodyPart;
    }

    private void SetNewTail(GameObject newSnakeBodyPart)
    {
        SnakeTail = newSnakeBodyPart;
    }

    private Vector3 GetNextTailPosition()
    {
        var direction = (this.transform.position - SnakeTail.transform.position).normalized;
        return SnakeTail.transform.position - (direction * 1.5f);
    }
}
