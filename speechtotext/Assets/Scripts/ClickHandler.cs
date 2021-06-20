using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour
{

    public UnityEvent upEvent;
    public UnityEvent downEvent;

    private void OnMouseDown()
    {
        print("Pressed Down");
        downEvent?.Invoke();
    }

    private void OnMouseUp()
    {
        print("Pressed Up");
        upEvent?.Invoke();
    }
}
