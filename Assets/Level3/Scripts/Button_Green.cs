using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button_Green : MonoBehaviour
{
    private bool buttonPressed = false;
    private List<Action<bool>> listeners = new List<Action<bool>>();
    public UnityEvent ButtonPressed;
    public UnityEvent ButtonReleased;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            buttonPressed = true;
            UpdateListeners(buttonPressed);
            ButtonPressed.Invoke();
            Debug.Log(gameObject.name + " Button pressed.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            buttonPressed = false;
            UpdateListeners(buttonPressed);
            ButtonReleased.Invoke();
            Debug.Log(gameObject.name + " Button released.");
        }
    }

    public bool IsButtonPressed()
    {
        return buttonPressed;
    }

    public void AddListener(Action<bool> callback)
    {
        listeners.Add(callback);
    }

    private void UpdateListeners(bool buttonPressed)
    {
        foreach (Action<bool> listener in listeners)
        {
            listener(buttonPressed);
        }
    }
}
