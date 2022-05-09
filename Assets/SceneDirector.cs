using Invector.vCamera;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SceneDirector : MonoBehaviour
{
    public Camera m_PrimaryCamera;
    public Camera m_LastCamera;
    public Camera m_LastAnimatedCamera;
    public vThirdPersonCamera m_vThirdPersonCamera; // InVector's ThirdPersonCamera on the player game object

    public GameObject HUD;
    public CanvasGroup EquipmentDisplayWindow;
    public GameObject AmmoDisplay;

    private DialogueRunner dialogueRunner; // utility object that serves lines of dialogue
    private FadeLayer fadeLayer; // black overlay used to fade in/out of scenes

    // when this scene conductor object is created
    // (in our example, this happens when the scene is created)
    private void Awake()
    {
        // get handles of utility objects in the scene that we need
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        fadeLayer = FindObjectOfType<FadeLayer>();

        // <<camera NAME_OF_LOCATION>>
        dialogueRunner.AddCommandHandler<Location, int>("camera", MoveCamera);
        dialogueRunner.AddCommandHandler<Location, int, float>("animCamera", TriggerAnimatedCamera);
        dialogueRunner.AddCommandHandler<Location, int>("disableAnimCamera", DisableAnimatedCamera);

        // <<fadeIn DURATION>> and <<fadeOut DURATION>>
        // Handlers for <<fadeIn DURATION>> and <<fadeOut DURATION>>
        dialogueRunner.AddCommandHandler<float>("fadeIn", FadeIn);
        dialogueRunner.AddCommandHandler<float>("fadeOut", FadeOut);

        Debug.Log("SceneConductor created.");
    }

    // Remember Disable InVector Components that would re-calculate the camera's position and rotation automatically
    public void DisableCameraScripts()
    {
        m_PrimaryCamera.enabled = false;
        m_vThirdPersonCamera.enabled = false;
    }

    public void EnableCameraScripts()
    {
        m_PrimaryCamera.enabled = true;
        m_vThirdPersonCamera.enabled = true;
    }

    // Event called after node is completed and camera needs to move to another location
    public void DisableCurrentlyActiveCamera()
    {
        m_LastCamera.enabled = false;
    }

    public void HideHUD()
    {
        HUD.SetActive(false);
        EquipmentDisplayWindow.alpha = 0;
        AmmoDisplay.SetActive(false);
    }

    public void ShowHUD()
    {
        //HUD.SetActive(true);
        //EquipmentDisplayWindow.alpha = 1;
        AmmoDisplay.SetActive(true);
    }

    // moves camera to camera location {location}>Camera in the scene
    private void MoveCamera(Location location, int camNumber)
    {
        string name = "Camera" + camNumber;

        Transform destination = location.GetMarkerWithName(name);
        if (destination != null)
        {
            m_LastCamera = destination.GetComponent<Camera>();
            m_LastCamera.enabled = true;

            //m_PrimaryCamera.transform.position = destination.position;
            //m_PrimaryCamera.transform.rotation = destination.rotation;

            Debug.Log($"Main Camera moved to {location.name}>Camera.");
        }
    }

    private Coroutine TriggerAnimatedCamera(Location location, int camNumber, float time = 10f)
    {        
        return StartCoroutine(AnimateCameraOverTime(location, camNumber, time));
    }

    private IEnumerator AnimateCameraOverTime(Location location, int camNumber, float time)
    {
        string name = "AnimatedCamera" + camNumber;

        Transform destination = location.GetMarkerWithName(name);
        if (destination != null)
        {
            m_LastCamera.enabled = false;
            m_LastAnimatedCamera = destination.GetComponent<Camera>();
            m_LastAnimatedCamera.enabled = true;

            Animation anim = m_LastAnimatedCamera.GetComponent<Animation>();
            if (!anim.isPlaying)
            {
                anim.clip.frameRate = 30;
                anim.Play();
            }
        }
        yield return new WaitForSeconds(time);
    }

    private void DisableAnimatedCamera(Location location, int camNumber)
    {
        string name = "AnimatedCamera" + camNumber;

        Transform destination = location.GetMarkerWithName(name);
        if (destination != null)
        {
            m_LastAnimatedCamera = destination.GetComponent<Camera>();
            m_LastAnimatedCamera.enabled = false;

            Animation anim = m_LastAnimatedCamera.GetComponent<Animation>();
            if (anim.isPlaying)
            {
                anim.Stop();
            }
        }
    }

    // fades in from a black screen over {time} seconds
    private Coroutine FadeIn(float time = 1f)
    {
        Debug.Log($"Fading in from black over {time} seconds.");
        return StartCoroutine(fadeLayer.ChangeAlphaOverTime(0, time));
    }

    // fades out to a black screen over {time} seconds
    private Coroutine FadeOut(float time = 1f)
    {
        Debug.Log($"Fading out to black over {time} seconds.");
        return StartCoroutine(fadeLayer.ChangeAlphaOverTime(1, time));
    }
}
