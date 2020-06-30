using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Vector2 move;
    public int speed;
    public Player player;

    public void Update()
    {
        transform.Translate(move/35);
    }

    public void SetMove(Vector2 input)
    {
        move = input;
    }

    public void Select()
    {
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(transform.position));
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.forward, out hit))
        {
            GameObject temp = hit.transform.gameObject;
            PlayerSelect button = temp.GetComponent<PlayerSelect>();
            
            if(button != null)
            {
                player.characterPrefab = button.character;
            }
            
        }
    }

    public void Cancel()
    {
        player.characterPrefab = null;
    }
}
