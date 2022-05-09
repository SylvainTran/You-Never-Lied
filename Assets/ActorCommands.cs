using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Invector.vCharacterController.AI;

public class ActorCommands : MonoBehaviour
{
    [YarnCommand("shootTarget")]
    public void ShootTarget(GameObject target)
    {
        transform.LookAt(target.transform);
        target.GetComponent<vControlAIShooter>().ChangeHealth(0);
        // Play shooting clip
        // Then after a while, return to previous anim state
        // Look at player finally
    }
}
