﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;


    bool isFocus = false;
    Transform player;
    public NavMeshAgent playerAgent;
    bool hasInteracted = false;

    public virtual void MoveToInteraction(NavMeshAgent playerAgent)
    {
        this.playerAgent = playerAgent;
        playerAgent.stoppingDistance = 2f;
        playerAgent.destination = this.transform.position;

        Interact();
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);

    }
    void Update()
    {
        if(isFocus && !hasInteracted)
        {
            //float distance = Vector3.Distance(player.position, transform.position);
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <=radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
        //Gizmos.DrawWireSphere(transform.position, radius);

    }
}
