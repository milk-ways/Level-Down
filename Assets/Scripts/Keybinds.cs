using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings")]
public class Keybinds : ScriptableObject
{
    // Movements
    public KeyCode up;
    public KeyCode down;
    public KeyCode right;
    public KeyCode left;
    public KeyCode jump;
    public KeyCode dash;

    // Attacks
    public KeyCode fire1;       // Normal attack
    public KeyCode fire2;       // Skill attack
    public KeyCode swap;

    public KeyCode CheckKey(string key)
    {
        switch (key)
        {
            case "Up":
                return up;

            case "Down":
                return down;

            case "Right":
                return right;

            case "Left":
                return left;

            case "Jump":
                return jump;

            case "Dash":
                return dash;

            case "Fire1":
                return fire1;

            case "Fire2":
                return fire2;

            case "Swap":
                return swap;

            default:
                return KeyCode.None;
        }
    }

    public void ChangeKey(string name, KeyCode key)
    {
        switch (name)
        {
            case "Up":
                up = key;
                break;

            case "Down":
                down = key;
                break;

            case "Right":
                right = key;
                break;

            case "Left":
                left = key;
                break;

            case "Jump":
                jump = key;
                break;

            case "Dash":
                dash = key;
                break;

            case "Fire1":
                fire1 = key;
                break;

            case "Fire2":
                fire2 = key;
                break;

            case "Swap":
                swap = key;
                break;

            default:
                Debug.Log("No key found");
                break;
        }
    }
}
