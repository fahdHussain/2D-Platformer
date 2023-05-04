using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeAnimator : MonoBehaviour
{
    public Animator animator;
    public GnomeController controller;
    private gAnim currentState;

    public enum gAnim
    {
        IDLE,
        RUN,
        ATTACK
    }
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<GnomeController>();
    }
    public void changeAnimationState(gAnim newState)
    {
        if(currentState == newState) return;

        animator.Play(getAnimation(newState));

        currentState = newState;
    }

    public gAnim getCurrentState()
    {
        return currentState;
    }

    string getAnimation(gAnim state)
    {
        switch(state)
        {
            case gAnim.IDLE:
                switch(controller.GetGnomeType())
                {
                    case GnomeController.GnomeType.MUSHROOM:
                        return "gnome_idle";
                    case GnomeController.GnomeType.ARCHER:
                        return "gnome_archer_idle";
                    case GnomeController.GnomeType.KNIGHT:
                        return "gnome_knight_idle";
                    default:
                        return null;
                }
            case gAnim.RUN:
                switch(controller.GetGnomeType())
                {
                    case GnomeController.GnomeType.MUSHROOM:
                        return "gnome_run";
                    case GnomeController.GnomeType.ARCHER:
                        return "gnome_archer_run";
                    case GnomeController.GnomeType.KNIGHT:
                        return "gnome_knight_run";
                    default:
                        return null;
                }
            case gAnim.ATTACK:
                switch(controller.GetGnomeType())
                {
                    case GnomeController.GnomeType.MUSHROOM:
                        return "gnome_attack";
                    case GnomeController.GnomeType.ARCHER:
                        return "gnome_archer_attack";
                    case GnomeController.GnomeType.KNIGHT:
                        return "gnome_knight_attack";
                    default:
                        return null;
                }
            default:
                return null;
        }
    }

    
}
