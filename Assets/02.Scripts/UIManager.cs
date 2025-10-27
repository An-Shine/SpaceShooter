using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{    
    public void OnButtonClick(RectTransform rt)    
    {
        Debug.Log($"Click Button : {rt.localScale.x}");
    }
}
/*
shopButton.onClick.AddListener(
    () => OnButtonClick(shopButton.name));

void Start()    
{
    action = () -> OnStartClick();    
}

public void OnButtonClick(string msg)
{
    Debug.Log($"Clilck Button : {msg}");
}


public void OnButtonClick()
{
    SceneManager.LoadScene("Level_01");
    SceneManager.LoadScene("Play", LoadSceneMode.Additive);
}
*/