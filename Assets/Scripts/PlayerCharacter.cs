using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : NetworkBehaviour {

    [SerializeField] float offsetSize = 3f;
    private Camera playerCamera;

    public override void Spawned()  {  // Use instead of Start/Awake for NetworkObjects
        /* Move to a random location around the current location */
        float offset_x = Random.Range(-offsetSize, offsetSize);
        float offset_z = Random.Range(-offsetSize, offsetSize);
        transform.position += new Vector3(offset_x, 0, offset_z);

          if (HasStateAuthority)
    {
        playerCamera = Camera.main;
        playerCamera.GetComponent<FirstPersonCamera>().Target = GetComponent<NetworkTransform>().InterpolationTarget;
    }
    }
}
