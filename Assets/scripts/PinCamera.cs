using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PinCamera : MonoBehaviour
{
    [SerializeField] private CharacterComponent _player;

    [SerializeField] private Camera _camera;
    [SerializeField] private float _yCameraOffset = 5;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var playerPos = _player.transform.position;
        var cameraPos = _camera.transform.position;
        _camera.transform.position = new Vector3(playerPos.x, playerPos.y + _yCameraOffset, cameraPos.z);
    }
}
