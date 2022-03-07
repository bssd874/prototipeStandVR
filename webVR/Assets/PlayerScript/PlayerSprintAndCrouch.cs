using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public float SprintSpeed = 10f;
    public float MoveSpeed = 5f;
    public float CrouchSpeed = 2f;

    private Transform look_Root;
    private float Stand_Height = 1.6f;
    private float Crouch_Height = 1f;

    private bool IsCrouching;

    private PlayerFootStep player_Footstep;

    private float SprintVolume = 1f;
    private float CrouchVolume = 0.1f;
    private float WalkVolumeMin = 0.2f, WalkVolumeMax = 0.6f;

    private float WalkStepDistance = 0.4f;
    private float SprintStepDistance = 0.25f;
    private float CrouchStepDistance = 0.5f;


    private float sprint_Value = 100f;
    public float sprint_Treshold = 10f;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        look_Root = transform.GetChild(0);

        player_Footstep = GetComponentInChildren<PlayerFootStep>();

    }

    void Start()
    {
        player_Footstep.VolumeMin = WalkVolumeMin;
        player_Footstep.VolumeMax = WalkVolumeMax;
        player_Footstep.StepDistance = WalkStepDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    //untuk lari
    void Sprint()
    {
        // if we have stamina we can sprint
        if (sprint_Value > 0f)
        {

            if (Input.GetKeyDown(KeyCode.LeftShift) && !IsCrouching)
            {

                playerMovement.speed = SprintSpeed;

                player_Footstep.StepDistance = SprintStepDistance;
                player_Footstep.VolumeMin = SprintVolume;
                player_Footstep.VolumeMax = SprintVolume;

            }

        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !IsCrouching)
        {
            playerMovement.speed = SprintSpeed;

            player_Footstep.StepDistance = SprintStepDistance;
            player_Footstep.VolumeMin = SprintVolume;
            player_Footstep.VolumeMax = SprintVolume;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && !IsCrouching)
        {
            playerMovement.speed = MoveSpeed;

            player_Footstep.StepDistance = WalkStepDistance;
            player_Footstep.VolumeMin = WalkVolumeMin;
            player_Footstep.VolumeMax = WalkVolumeMax;

            
        }

        if (Input.GetKey(KeyCode.LeftShift) && !IsCrouching)
        {

            sprint_Value -= sprint_Treshold * Time.deltaTime;

            if (sprint_Value <= 0f)
            {

                sprint_Value = 0f;

                // reset the speed and sound
                playerMovement.speed = MoveSpeed;
                player_Footstep.StepDistance = WalkStepDistance;
                player_Footstep.VolumeMin = WalkVolumeMin;
                player_Footstep.VolumeMax = WalkVolumeMax;

                
            }

        }
        else
        {

            if (sprint_Value != 100f)
            {

                sprint_Value += (sprint_Treshold / 2f) * Time.deltaTime;

                if (sprint_Value > 100f)
                {
                    sprint_Value = 100f;
                }

            }

        }
    }

    void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if (IsCrouching)
            {
                look_Root.localPosition = new Vector3(0f, Stand_Height, 0f);
                playerMovement.speed = MoveSpeed;

                player_Footstep.StepDistance = WalkStepDistance;
                player_Footstep.VolumeMin = WalkVolumeMin;
                player_Footstep.VolumeMax = WalkVolumeMax;
                
                 IsCrouching = false;
            }
            else
            {
                look_Root.localPosition = new Vector3(0f, Crouch_Height, 0f);
                playerMovement.speed =CrouchSpeed;

                player_Footstep.StepDistance = CrouchStepDistance;
                player_Footstep.VolumeMin = CrouchVolume;
                player_Footstep.VolumeMax = CrouchVolume;

                IsCrouching = true;
            }
        }
    }
}
