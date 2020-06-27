using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Vector2 move;
    public int speed;
    public Player player;

    public void Update()
    {
        GetComponent<RectTransform>().Translate(move);
    }

    public void SetMove(Vector2 input)
    {
        move = input;
        Debug.Log("Moved Cursor");
    }

    public void Select()
    {
        Debug.Log("Selected Character");
    }

    public void Cancel()
    {
        Debug.Log("Deselected Character");
        player.character = null;
    }
}
