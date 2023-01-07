using System;
using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private CharacterComponent _player;

    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameObject _button;
    private bool _dcorIsRunning;
    private Button _jumpButton;



    private void Start()
    {
         _player.EntryAnimatiom = _player.PlayerAnimation.AnimationState.SetAnimation(0, _player.IdleAnim, true);
         _jumpButton = _button.GetComponent<Button>();
    }


    private void FixedUpdate()
    {
        var playerRotationRight = new Quaternion(0f, 0f, 0f, 0f);
        var playerRotationLeft = new Quaternion(0, 180, 0, 0);
        var xVelocity = _joystick.Horizontal;

        if (_player.IsDead)
        {
            if (!_dcorIsRunning)
            {
                _player.PlayerAnimation.AnimationName = _player.DeathAnim;
                _player.enabled = false;
                _player.RB.simulated = false;
                StartCoroutine(DeathDelay(_player.DeathDealayF));
                return;
            }
            return;
        }
        
        if (_player.IsInFalling)
        {
            _player.PlayerAnimation.AnimationName = _player.HoverAnim;
            _joystick.gameObject.SetActive(false);
            _jumpButton.gameObject.SetActive(false);
            xVelocity = 0;
        }
        if (!_player.IsInFalling)
        {
            _joystick.enabled = true;
            _joystick.gameObject.SetActive(true);
            _jumpButton.gameObject.SetActive(true);
            _player.CanJump = true;
            xVelocity = _joystick.Horizontal;

        }
        if (xVelocity != 0 && !_player.IsInFalling && !_player.IsDead && _player.CanJump)
        {
            _player.PlayerAnimation.AnimationName = _player.RunAnim;
        }
        if (xVelocity == 0 && !_player.IsInFalling && !_player.IsDead && _player.CanJump)
        {
            _player.PlayerAnimation.AnimationName = _player.IdleAnim;
        }
        _player.transform.rotation = _joystick.Horizontal switch
        {
            > 0.1f => playerRotationRight,
            0 => _player.transform.rotation,
            _ => playerRotationLeft
        };
        _player.RB.velocity = new Vector2(xVelocity * _player.MovementSpeed, -_player.MovementSpeed);
    }

    public void Jump()
    {
        if (!_player.CanJump) return;
            _player.RB.AddForce(_player.JumpForce, ForceMode2D.Impulse);
            var track = _player.PlayerAnimation.state.SetAnimation(2, _player.JumpAnim, false);
            StartCoroutine(AnimationCor(track));
            _player.CanJump = false;
    }

    private IEnumerator DeathDelay(float time)
    {
        _dcorIsRunning = true;
        Debug.Log(2);
        yield return new WaitForSeconds(time);
        Debug.Log(3);
        _player.enabled = true;
        _player.IsDead = false;
        _player.PlayerAnimation.AnimationName = _player.IdleAnim;
        _player.RB.simulated = true;
        _dcorIsRunning = false;
        
    }

    private IEnumerator AnimationCor(TrackEntry track)
    {
        yield return new WaitForSpineAnimationComplete(track);
        _player.PlayerAnimation.ClearState();
        _player.PlayerAnimation.name = _player.IdleAnim;
        _player.CanJump = true;
    }
    
    
}
