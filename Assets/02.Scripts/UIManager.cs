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
