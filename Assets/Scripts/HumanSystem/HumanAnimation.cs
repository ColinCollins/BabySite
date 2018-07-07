using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimation{
    // play the Animation
    // bind in the Human body
    private HumanSystem _person = null;
    private Animator animator = null;
    public HumanAnimation(HumanSystem person) {
        _person = person;
        animator = _person.gameObject.GetComponent<Animator>();
        animator.speed = 0;
    }
    
    public void PlayAction() {

        if (_person.state == HumanState.isIdle)
        {
            int info = animator.GetCurrentAnimatorStateInfo(0).GetHashCode();
            animator.ForceStateNormalizedTime(0);
            StopAllAnimation();
            return;
        }
        else if (_person.state == HumanState.isSlowing) {
            StopAllAnimation();
        }
        else {
            animator.speed = 1;
        }

        //int clip = animator.GetCurrentAnimatorStateInfo(0).GetHashCode();
        if (_person.state == HumanState.isWalking || _person.state == HumanState.isSlowing)
        {
            switch (_person.direction)
            {
                case Direction.Left:
                    WalkLeft();
                    break;
                case Direction.Up:
                    WalkUp();
                    break;
                case Direction.Right:
                    WalkRight();
                    break;
                case Direction.Down:
                    WalkDown();
                    break;
            }
        }
    }

    private void StopAllAnimation() {
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
    }
    private void WalkLeft() {
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Walk.Left"))
        {
            animator.SetBool("Left", true);
        }
    }
    private void WalkUp() {
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Walk.Up"))
        {
            animator.SetBool("Up", true);
        } 
    }
    private void WalkRight() {
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Walk.Right"))
        {
            animator.SetBool("Right", true);
        } 
    }
    private void WalkDown() {
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Walk.Down"))
        {
            animator.SetBool("Down", true);
        }            
    }
}
