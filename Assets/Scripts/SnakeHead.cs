using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    private readonly float _speedIncrement = 0.2f;
    private readonly float _spaceBetweenSnakeBodyParts = 1.5f;
    
    private float _movingSpeed;
    private Direction _direction;

    public GameObject Tail;
    
    void Start()
    {
        _movingSpeed = 1.5f;
        _direction = Direction.Create();
    }

    void Update()
    {
        ListenToUserInput();
    }

    private void ListenToUserInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _direction.TurnLeft();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            _direction.TurnRight();
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            _movingSpeed += 1;
        else if (Input.GetKeyDown(KeyCode.Space))
            _movingSpeed = 0.5f;
        else if (Input.GetKeyDown(KeyCode.P))
            print("Nada");
        else if (Input.GetKeyDown(KeyCode.E))
            EnlargeSnakeBody();
    }

    private void FixedUpdate()
    {
        this.Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * _movingSpeed * Time.deltaTime);
        transform.Rotate(_direction.RotationVector * _movingSpeed * Time.deltaTime);
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
        EatCookie(cookie);
        IncreaseSpeed();
        EnlargeSnakeBody();
    }

    private void EatCookie(GameObject cookieToEat)
    {
        Destroy(cookieToEat);
    }

    private void IncreaseSpeed()
    {
        _movingSpeed += _speedIncrement;
    }

    private void EnlargeSnakeBody()
    {
        var newSnakeBodyPart = CreateNewSnakeBodyPart();
        JoinNewSnakeBodyPartToTail(newSnakeBodyPart);
        SetNewTail(newSnakeBodyPart);
    }

    private GameObject CreateNewSnakeBodyPart()
    {
        var newSnakeBodyPart = Instantiate(Tail);
        newSnakeBodyPart.SendMessage("SetPosition", GetNextTailPosition());
        return newSnakeBodyPart;
    }

    private void JoinNewSnakeBodyPartToTail(GameObject newSnakeBodyPart)
    {
        Tail.SendMessage("JoinToSnakeBodyPart", newSnakeBodyPart.GetComponent<Rigidbody>());
    }

    private void SetNewTail(GameObject newSnakeBodyPart)
    {
        Tail = newSnakeBodyPart;
    }

    private Vector3 GetNextTailPosition()
    {
        var direction = (this.transform.position - Tail.transform.position).normalized;
        return Tail.transform.position - (direction * _spaceBetweenSnakeBodyParts);
    }

    private void PrintLocation(string context, Vector3 location)
    {
        print($"[{context}] {location.x}, {location.y}, {location.z}");
    }
}

public class Direction
{
    private enum Orientation
    {
        STRAIGHT,
        LEFT,
        RIGHT
    }

    private static Vector3 _turnLeft = new Vector3(0, -15, 0);
    private static Vector3 _turnRight = new Vector3(0, 15, 0);
    private static Vector3 _goStraight = new Vector3(0, 0, 0);

    public Vector3 RotationVector { get; private set; }
    private Orientation _towards;

    private Direction(Vector3 directionVector, Orientation towards)
    {
        this.RotationVector = directionVector;
        this._towards = towards;
    }

    public static Direction Create() =>
        new Direction(_goStraight, Orientation.STRAIGHT);

    public void TurnLeft()
    {
        if (_towards == Orientation.LEFT)
            RotationVector += _turnLeft;
        else if(_towards == Orientation.STRAIGHT)
        {
            RotationVector = _turnLeft;
            _towards = Orientation.LEFT;
        }
        else if (_towards == Orientation.RIGHT)
        {
            RotationVector = _goStraight;
            _towards = Orientation.STRAIGHT;
        }
    }

    public void TurnRight()
    {
        if (_towards == Orientation.RIGHT)
            RotationVector += _turnRight;
        else if (_towards == Orientation.STRAIGHT)
        {
            RotationVector = _turnRight;
            _towards = Orientation.RIGHT;
        }
        else if (_towards == Orientation.LEFT)
        {
            RotationVector = _goStraight;
            _towards = Orientation.STRAIGHT;
        }
    }

    public void GoStraight()
    {
        this.RotationVector = _goStraight;
        this._towards = Orientation.STRAIGHT;
    }
}
