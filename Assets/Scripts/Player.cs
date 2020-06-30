using System;
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

    public Enums.Direction direction;

    private void Awake()
    {
        input = new InputMaster();
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnSceneChanged;

        if (SceneManager.GetActiveScene().name == "CharacterSelectionScene")
        {
            Vector3 position = new Vector3(0, 0, 0);
            Quaternion rotation = Quaternion.identity;
            cursor = GameObject.Instantiate(cursorPrefab, position, rotation).GetComponent<CursorController>();
            cursor.player = this;
            scene = 0;
        }
    }

    private void OnMoveCursor(InputValue input)
    {
        if (scene == 0)
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
        if (scene == 1)
        {
            character.SetMove(input.Get<float>());
        }
    }

    private void OnJump()
    {
        if (scene == 1)
        {
            character.Jump();
        }
    }

    private void OnDirectionalModifier(InputValue input)
    {
        if (scene == 1)
        {
            Vector2 vector = input.Get<Vector2>();

            if(vector.magnitude == 0)
            {
                direction = Enums.Direction.Neutral;
            }
            else
            {
                if(Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
                {
                    direction = Enums.Direction.Side;
                }
                else
                {
                    if(vector.y > 0)
                    {
                        direction = Enums.Direction.Up;
                    }
                    else
                    {
                        direction = Enums.Direction.Down;
                    }
                }
            }
        }
    }

    private void OnSpecialAttack()
    {
        if (scene == 1)
        {
            switch (direction)
            {
                case Enums.Direction.Down:
                    character.DownSpecial();
                    break;
                case Enums.Direction.Side:
                    character.SideSpecial();
                    break;
                case Enums.Direction.Neutral:
                    character.NeutralSpecial();
                    break;
                case Enums.Direction.Up:
                    character.UpSpecial();
                    break;
            }
        }
    }
    
    private void OnStandardAttack()
    {
        if (scene == 1)
        {
            switch (direction)
            {
                case Enums.Direction.Down:
                    character.DownStandard();
                    break;
                case Enums.Direction.Side:
                    character.SideStandard();
                    break;
                case Enums.Direction.Neutral:
                    character.NeutralStandard();
                    break;
                case Enums.Direction.Up:
                    character.UpStandard();
                    break;
            }
        }
    }

    private void OnSceneChanged(Scene prev, Scene next)
    {
        if (next.name == "CharacterSelectionScene")
        {
            GetComponent<PlayerInput>().SwitchCurrentActionMap("Menu");
            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            cursor = GameObject.Instantiate(cursorPrefab, position, rotation).GetComponent<CursorController>();
            cursor.player = this;
            scene = 0;
        }
        else if(next.name == "BattleScene")
        {
            GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
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
