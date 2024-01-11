using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    #region Global Variables
    public static GameManager Instance;

    [SerializeField] private bool objectivesDone = false;
    public bool ObjectivesDone
    {
        get         { return objectivesDone; }
        private set { objectivesDone = value; }
    }

    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private Crosshair crosshair;
    [SerializeField] private GameObject ghost;
    [SerializeField] private PickingItems playerPickingItems;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerBehaviour playerAnimatorBehaviour;
    [SerializeField] private Teleport playerTeleport;
    [SerializeField] private Fade fade;
    [SerializeField] private EscapeDoor escapeDoor;
    [SerializeField] private int bodiesToBurn = 3;
    private int bodiesBurned;
    

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
        Catched,
        Revive,
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
    [Header("Part6 Variables")]
    [SerializeField] private Vector3 endCutscenePosition;
    [SerializeField] private Quaternion endCutsceneRotation;
    [SerializeField] private Door surgeryDoor;
    #endregion
    #region Part7 Variables
    [Header("Part 7 Variables")]
    [SerializeField] private PlayableDirector cutscene;

    #endregion
    #region Catched Variables
    [Header("Catched Variables")]
    [SerializeField] private float checkPointCutsceneTime = 10.5f;
    [SerializeField] private GameObject ghostCamera;
    #endregion
    #region Revive Variables
    [Header("Revive Variables")]
    [SerializeField] Teleport ghostTeleport;
    [SerializeField] GhostStateMachine ghostStateMachine;
    [SerializeField] RandomizeSpawnPoints randomizeSpawnPoints;
    #endregion
    #region GameOver Variables
    [Header("GameOver Variables")]
    [SerializeField] private PlayableDirector cutscene2;
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

        //ActualState = GameStates.Part5;
    }
    private void Start()
    {
        UpdateGameState(ActualState);
        bodiesBurned = 0;
        MourgeDoor.BodyBurned += CheckObjectives;
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
            case GameStates.Catched:
                CatchedMechanics();
                break;
            case GameStates.Revive:
                ReviveMechanics();
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
        escapeDoor.enabled = false;
        dayRoomDoorLeft.doorState = Door.DoorState.Closed;
        dayRoomDoorRight.doorState = Door.DoorState.Closed;
        AudioManager.Instance.PlayDialogue(AudioManager.Instance.dialogueOne, 2);
        InformationManager.Instance.ShowTip(1);
    }
    private void PartTwoMechanics()
    {
        AudioManager.Instance.PlayDialogue(AudioManager.Instance.dialogueTwo, 0);
        partTwoTrigger.gameObject.SetActive(false);
    }
    private void PartThreeMechanics()
    {
        AudioManager.Instance.PlayDialogue(AudioManager.Instance.dialogueThree, 3);
        paintingScript.Interactable = true;
    }
    private void PartFourMechanics()
    {
        AudioManager.Instance.PlayDialogue(AudioManager.Instance.dialogueFour, 4);
    }
    private void PartFiveMechanics()
    {
        AudioManager.Instance.PlayDialogue(AudioManager.Instance.dialogueFive, 5);
        projector.Interactable = true;
    }
    private void PartSixMechanics()
    {
        playerController.CameraCanMove = false;
        playerController.PlayerCanMove = false;
        playerController.Crosshair = false;
        playerTeleport.teleportTo = endCutscenePosition;
        playerTeleport.rotateTo = endCutsceneRotation;
        surgeryDoor.doorState = Door.DoorState.Locked;
        cutscene.Play();
    }
    private void PartSevenMechanics()
    {
        surgeryDoor.doorState = Door.DoorState.Closed;
        ghost.SetActive(true);
        AudioManager.Instance.PlayDialogue(AudioManager.Instance.dialogueAndGhost, 6);
    }


    public void CheckPointMechanics()
    {
        playerController.CameraCanMove = true;
        playerController.PlayerCanMove = true;
        playerController.Crosshair = true;
    }

    private void CatchedMechanics()
    {
        playerController.CameraCanMove = false;
        playerController.PlayerCanMove = false;
        playerController.Crosshair = false;
        crosshair.enabled = false;

        if(!playerController.IsHidden && playerPickingItems.activeItem != null)
        {
            playerPickingItems.LeaveItem();
        }
        if(playerController.IsHidden && playerPickingItems.activeItem != null)
        {
            GameObject inHandItem = playerPickingItems.GetItemPrefab();
            GameObject spawnedItem = Instantiate(inHandItem, playerController.lastPlayerPosition, inHandItem.transform.rotation);
        }
        fade.FadeAndChangeState(GameStates.Revive);
    }
    private void ReviveMechanics()
    {
        ActualState = GameStates.IdleState;
        crosshair.enabled = true;
        if (playerController.IsHidden)
        {
            playerController.OriginalPlayerAtributes();
            playerController.ResetSpeedIfCrouched();
            playerController.IsHidden = false;
        }
        playerTeleport.teleportTo = endCutscenePosition;
        playerTeleport.rotateTo = endCutsceneRotation;
        playerTeleport.TeleportCharacter();
        cutscene.time = checkPointCutsceneTime;
        ghostCamera.SetActive(false);
        cutscene.Play();

        ghost.SetActive(false);
        ghostTeleport.teleportTo = randomizeSpawnPoints.ReturnRandomPoint().position;
        ghostTeleport.TeleportCharacter();
        ghostStateMachine.ChangeState(ghostStateMachine.IdleState);
        ghost.SetActive(true);
    }
    private void GameOverMechanics()
    {
        playerController.CameraCanMove = false;
        playerController.PlayerCanMove = false;
        playerController.Crosshair = false;
        crosshair.enabled = false;
        cutscene2.Play();
    }
    private void CheckObjectives()
    {
        bodiesBurned++;
        if(bodiesBurned == bodiesToBurn)
        {
            objectivesDone = true;
            InformationManager.Instance.ShowTip(13);
        }
    }
}
