using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private FirstPersonController playerController;

    public GameStates ActualState;
    public enum GameStates 
    { 
        Part1,
        Part2,
        GameOver
    }

    #region Part1 Variables
    [SerializeField] Collider partTwoTrigger;
    private bool partTwoTriggerd = false;
    #endregion
    #region Part2 Variables
    #endregion
    #region Part3 Variables
    #endregion
    #region Part4 Variables
    #endregion
    #region Part5 Variables
    #endregion

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

        ActualState = GameStates.Part1;
    }
    private void Start()
    {
        UpdateGameState(ActualState);
    }

    public static event Action<GameStates> OnGameStateChange;

    public void UpdateGameState(GameStates state)
    {
        ActualState = state;
        switch (state)
        {
            case GameStates.Part1:
                PartOneMechanics();
                break;
            case GameStates.Part2:
                PartTwoMechanics();
                break;
            case GameStates.GameOver:
                GameOverMechanics();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
        OnGameStateChange?.Invoke(state);
    }

    private void PartOneMechanics()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.dialogueSource, AudioManager.Instance.dialogueOne);
    }
    private void PartTwoMechanics()
    {
        AudioManager.Instance.dialogueSource.Stop();
        AudioManager.Instance.PlaySound(AudioManager.Instance.dialogueSource, AudioManager.Instance.dialogueTwo);
    }
    private void GameOverMechanics()
    {
        playerController.enabled = false;
    }
}
