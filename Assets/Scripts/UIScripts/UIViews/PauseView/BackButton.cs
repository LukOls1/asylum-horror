using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    [SerializeField] private UIView previousView;

    public UIView SetPreviousAsCurrent()
    {
        return previousView;
    }
}
