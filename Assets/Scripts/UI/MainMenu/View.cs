using TMPro;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    [SerializeField] protected TMP_Text Label;

    public abstract void OnButtonClick();

}
