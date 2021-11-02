using System.Collections;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Camera _me;
    private float _minX = 50, _minY = 50, _maxX, _maxY;

    public GameObject SnakeHeadToLook;

    void Start()
    {
        _me = GetComponent<Camera>();
        (this._maxX, this._maxY) = (_me.pixelWidth - 50, _me.pixelHeight - 50);
        StartCoroutine(MakeSnakeHeadStaysFocused());
    }

    void Update()
    {
    }

    IEnumerator MakeSnakeHeadStaysFocused()
    {
        while(true)
        {
            if (IsSnakeHeadAboutToGetOffScreen())
                FocusSnakeHead();

            yield return new WaitForSeconds(0.5f);
        }
    }

    private bool IsSnakeHeadAboutToGetOffScreen()
    {
        var relativeSnakeHeadPosition = _me.WorldToScreenPoint(SnakeHeadToLook.transform.position);
        return relativeSnakeHeadPosition.y < this._minY || relativeSnakeHeadPosition.y > this._maxY
            || relativeSnakeHeadPosition.x < this._minX || relativeSnakeHeadPosition.x > this._maxX;
    }

    private void FocusSnakeHead()
    {
        transform.LookAt(this.SnakeHeadToLook.transform);
        transform.Translate(Vector3.forward);
    }

    private void PrintLocation(string context, Vector3 location)
    {
        print($"[{context}] {location.x}, {location.y}, {location.z}");
    }
}
