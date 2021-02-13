﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public Light spotlight;
    public float viewDistance;
    public LayerMask viewMask;
    private float viewAngle;
    public Transform player;
    Color originalSpotlightColour;

    // Start is called before the first frame update
    void Start()
    {
        originalSpotlightColour = spotlight.color;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotlight.spotAngle;

       

    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer())
        {
            spotlight.color = Color.red;
        }
        else
        {
            spotlight.color = originalSpotlightColour;
        }
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }

}