using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class VolumeActivation : MonoBehaviour
{
    public UnityEvent volumeActivationEvent;
    public UnityEvent volumeDeactivationEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Constant.PLAYER_TAG)
        {
            volumeActivationEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Constant.PLAYER_TAG)
        {
            volumeDeactivationEvent.Invoke();
        }
    }
}
