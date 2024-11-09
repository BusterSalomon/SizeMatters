using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private bool buttonPressed = false;
    private List<Action<bool>> listeners =  new List<Action<bool>>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            buttonPressed = true;
            updateListeners(buttonPressed);
            Debug.Log("Btn pressed");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            buttonPressed = false;
            updateListeners(buttonPressed);
            Debug.Log("Btn not pressed");
        }
    }

    public bool isButtonPressed()
    {
        return buttonPressed;
    }

    public void addListener(Action<bool> callback)
    {
        listeners.Add(callback);
    }

    private void updateListeners (bool buttonPressed)
    {
        foreach (Action<bool> listener in listeners)
        {
            listener(buttonPressed);
        }
    }
}
