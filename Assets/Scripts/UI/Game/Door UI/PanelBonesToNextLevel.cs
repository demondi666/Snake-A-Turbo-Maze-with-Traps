using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelBonesToNextLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text _bonesText;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        
    }
    
    private void OnDisable()
    {
        
    }

    private void LateUpdate()
    {
        transform.LookAt(_camera.transform);
    }
    
    public void Renderer(int bones)
    {
        _bonesText.text = "X " + bones.ToString();
    }
}
