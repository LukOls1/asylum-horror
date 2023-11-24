using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Global Variables
    public static GameManager Instance;

    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerBehaviour playerAnimatorBehaviour;
    [SerializeField] private PlayerTeleport playerTeleport;
    [SerializeField] private Fade fade;

    public GameStates ActualState;
    public enum GameStates
    {
        Part1,
        Part2,
        Part3,
        Part4,
        Part5,
        Part6,
        Part7,
        GameOver,
        IdleState
    }
    #endregion

    #region Part1 Variables
    [Header("Part 1 Variables")]
    [SerializeField] private Collider partTwoTrigger;
    [SerializeField] private Door dayRoomDoorLeft;
    [SerializeField] private Door dayRoomDoorRight;
    #endregion
    #region Part2 Variables
    [Header("Part 2 Variables")]
    #endregion
    #region Part3 Variables
    [Header("Part 3 Variables")]
    [SerializeField] Painting paintingScript;
    #endregion
    #region Part4 Variables
    [Header("Part 4 Variables")]
    #endregion
    #region Part5 Variables
    [Header("Part 5 Variables")]
    [SerializeField] private Projector projector;
    #endregion
    #region Part6 Variables
    #endregion
    #region Part7 Variables
    [Header("Part 7 Variables")]
    [SerializeField] private Vector3 playerOnBedPosition;
    [SerializeField] private Quaternion playerOnBedRotation;
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

        ActualState = GameStates.Part5;
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
            case GameStates.Part3:
                PartThreeMechanics();
                break;
            case GameStates.Part4:
                PartFourMechanics();
                break;
            case GameStates.Part5:
                PartFiveMechanics();
                break;
            case GameStates.Part6:
                PartSixMechanics();
                break;
            case GameStates.Part7:
                PartSevenMechanics();
                break;
            case GameStates.GameOver:
                GameOverMechanics();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
        OnGameStateChange?.Invoke(state);
    }
    // Poczatek gry, instrukcja dostania sie do pokoju z projektorem
    private void PartOneMechanics()
    {
        //Zamkniêcie wszystkich drzwi, pozostawienie drzwi z kodem jako Locked
        dayRoomDoorLeft.doorState = Door.DoorState.Closed;
        dayRoomDoorRight.doorState = Door.DoorState.Closed;
        PlayDialogue(AudioManager.Instance.dialogueOne);
    }
    // Odnalezienie notatki z instrukcja odnalezienia kodu do drzwi z tasma
    private void PartTwoMechanics()
    {
        // ZnaleŸæ skrytke do notatki
        
        //dayRoomDoorRight.doorState = Door.DoorState.Opened;
        PlayDialogue(AudioManager.Instance.dialogueTwo);
        partTwoTrigger.gameObject.SetActive(false);
    }
    // Odnalezienie tasmy, próba obejrzenia taœmy 
    private void PartThreeMechanics()
    {
        PlayDialogue(AudioManager.Instance.dialogueThree);
        paintingScript.Interactable = true;
    }
    // Obudzenie siê w pokoju na stole operacyjnym, znalezienie notatki z zawartoœcia o eksperymencie z tasma
    // W notatce bedzie zawarta informacja o tym co zrobic w wypadku œmierci obiektu badanego 
    // Brakuj¹ce strony w raporcie o obiektach beda do odszukania po ca³ym obiekcie 
    private void PartFourMechanics()
    {
        PlayDialogue(AudioManager.Instance.dialogueFour);
    }
    private void PartFiveMechanics()
    {
        PlayDialogue(AudioManager.Instance.dialogueFive);
        projector.Interactable = true;
    }
    private void PartSixMechanics()
    {
        fade.FadeAndChangeState(GameStates.Part7);
        playerController.CameraCanMove = false;
        playerController.PlayerCanMove = false;
    }
    private void PartSevenMechanics()
    {
        playerTeleport.Teleport(playerOnBedPosition, playerOnBedRotation);
        //playerAnimatorBehaviour.
        //playerAnimator.enabled = true;
    }

    private void PlayDialogue(AudioClip dialogue)
    {
        AudioManager.Instance.dialogueSource.Stop();
        AudioManager.Instance.PlaySound(AudioManager.Instance.dialogueSource, dialogue);
    }
    private void GameOverMechanics()
    {
        playerController.enabled = false;
    }
}
