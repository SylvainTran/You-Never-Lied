using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Invector.vItemManager;
using Invector.PlayerController;
using Invector.vMelee;
using Invector.vCharacterController;
using Invector.vCharacterController.AI;
using UnityEngine.AI;

public class InputYarn : MonoBehaviour
{
    // Player
    public vThirdPersonController m_ThirdPersonController;
    public vThirdPersonInput m_ThirdPersonInput;

    // Lyra
    public vSimpleMeleeAI_Companion m_LyraController;
    public vMeleeManager m_LyraMeleeManager;
    public NavMeshAgent m_LyraNavMeshAgent;

    public vInventory m_PlayerInventory;
    public DialogueRunner m_DialogueRunner;
    public LineView m_LineView;

    void Update()
    {
        ContinueDialogueOnKeyDown();        
    }

    private void ContinueDialogueOnKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            m_LineView.OnContinueClicked();
        }
    }

    // Need to call this for Companion AIs as well
    public void DisableAllInputs()
    {
        m_ThirdPersonController.enabled = false;
        m_ThirdPersonInput.enabled = false;
        m_PlayerInventory.enabled = false;

        m_LyraController.enabled = false;
        m_LyraMeleeManager.enabled = false;
        m_LyraNavMeshAgent.enabled = false;
    }

    public void EnableAllInputs()
    {
        m_ThirdPersonController.enabled = true;
        m_ThirdPersonInput.enabled = true;
        m_PlayerInventory.enabled = true;

        m_LyraController.enabled = true;
        m_LyraMeleeManager.enabled = true;
        m_LyraNavMeshAgent.enabled = true;
    }
}
