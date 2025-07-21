using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI : MonoBehaviour
{
    [SerializeField] protected CanvasGroup CanvasGroup;
    public abstract void Open();
    public abstract void Close();
}
