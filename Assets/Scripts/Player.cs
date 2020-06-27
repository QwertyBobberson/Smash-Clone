using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public InputMaster input;
    public Character character;
    public CursorController cursor;

    public static int scene;
    
    public GameObject cursorPrefab;
    public GameObject characterPrefab;

    private void Awake()
    {
        input = new InputMaster();
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnSceneChanged;
        OnSceneChanged(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
    }

    private void OnMoveCursor(InputValue input)
    {
        if(scene == 0)
        {
            cursor.SetMove(input.Get<Vector2>());
        }
    }

    private void OnSelect()
    {
        if (scene == 0)
        {
            cursor.Select();
        }
    }

    private void OnCancel()
    {
        if (scene == 0)
        {
            cursor.Cancel();
        }
    }

    private void OnStart()
    {
        if (scene == 0)
        {
            SceneManager.LoadScene("BattleScene");
        }
    }

    private void OnMovement(InputValue input)
    {
        if(scene == 1)
        {
            character.SetMove(input.Get<float>());
        }
    }

    private void OnJump()
    {
        if(scene == 1)
        {
            character.Jump();
        }
    }

    private void OnSceneChanged(Scene prev, Scene next)
    {
        if(next.name == "CharacterSelectionScene")
        {
            cursor = GameObject.Instantiate(cursorPrefab, FindObjectOfType<Canvas>().transform).GetComponent<CursorController>();
            cursor.player = this;
            scene = 0;
        }
        else if(next.name == "BattleScene")
        {
            character = GameObject.Instantiate(characterPrefab).GetComponent<Character>();
            character.player = this;
            scene = 1;
        }
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
