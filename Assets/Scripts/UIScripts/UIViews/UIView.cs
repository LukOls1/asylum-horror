using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView: MonoBehaviour
{
    public bool isActive = false;
    public BackButton backButton;
    public void ControlView(UIView currentView)
    {
        if(currentView == this || currentView == null)
        {
            this.isActive = !isActive;
            ActiveView(isActive);
        }
    }
    public void ActiveView(bool active)
    {
        this.gameObject.SetActive(active);
    }
}
