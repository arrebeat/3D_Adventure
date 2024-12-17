using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI uiTextName;

    void Start()
    {
        SaveManager.instance.FileLoaded += OnLoad;
    }

    void Update()
    {
        OnLoad(SaveManager.instance.saveSetup);
    }
    
    public void OnLoad(SaveSetup setup)
    {
        uiTextName.text = "Play " + (setup.lastLevel + 1);
    }

    private void OnDestroy()
    {
        SaveManager.instance.FileLoaded -= OnLoad;
    }
}
