using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    public float MovementSpeed = 10f;
    public Rigidbody2D RB;
    public Collider2D collider2D;
    public SkeletonAnimation PlayerAnimation;
    public float DeathDealayF = 0.3f;
    public Vector2 JumpForce;
    [HideInInspector] public string IdleAnim = "idle";
    [HideInInspector] public string RunAnim = "run";
    [HideInInspector] public string JumpAnim = "jump";
    [HideInInspector] public string DeathAnim = "death";
    [HideInInspector] public string HoverAnim = "hoverboard";
    [HideInInspector] public Spine.TrackEntry EntryAnimatiom;
    public bool IsInFalling { private set; get; }
    [HideInInspector] public bool IsDead;
    [HideInInspector] public bool CanJump;
    
    
// collisions must be moved to a separate system 
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsInFalling = other.gameObject.transform.eulerAngles.z != 0;
            CanJump = other.gameObject.transform.eulerAngles.z == 0;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            IsDead = true;
        }
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            CanJump = false;
        }
    }
}
