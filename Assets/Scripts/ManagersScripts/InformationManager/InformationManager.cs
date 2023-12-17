using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InformationManager : MonoBehaviour
{
    public static InformationManager Instance;

    [SerializeField] private Animator informationAnimator;
    [SerializeField] private TextMeshProUGUI informationText;
    
    [SerializeField] public List<string> TipsList;
    private int tipIndex = 0;

    [TextArea(1, 10)]
    public string ControlsTip;
    [TextArea(1, 10)]
    public string FindRoomTip;
    [TextArea(1, 10)]
    public string FindCodeTip;
    [TextArea(1, 10)]
    public string FindOfficeTip;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        AudioManager.EndOfClip += ShowTip;
    }

    public void ShowTip(int tipsListIndex)
    {
        informationText.text = TipsList[tipsListIndex];
        informationAnimator.SetTrigger("fadeActive");
    }
    public void ShowNextTip()
    {
        informationText.text = TipsList[tipIndex];
        informationAnimator.SetTrigger("fadeActive");
        tipIndex++;
    }
}
