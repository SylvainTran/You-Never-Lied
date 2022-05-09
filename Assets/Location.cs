using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Location : MonoBehaviour
{
    [YarnCommand("disableLocation")]
    public void DisableLocation()
    {
        gameObject.SetActive(false);
    }

    [YarnCommand("disableLocationCamera")]
    public void DisableLocationCamera()
    {
        GetComponentInChildren<Camera>().enabled = false;
    }
    public Transform GetMarkerWithName(string markerName)
    {
        Transform marker = transform.Find(markerName);
        if (marker == null)
        {
            Debug.LogError($"Location {name} has no marker named {markerName}.");
            return null;
        }
        return marker;
    }
}
