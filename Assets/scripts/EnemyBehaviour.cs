using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyComponent _enemy;
    [SerializeField] private CharacterComponent _player;
    
    void FixedUpdate()
    {
        _enemy.RB.velocity = new Vector2(_enemy.MovementVelocity, -_enemy.MovementVelocity);
        _enemy.transform.Rotate(0, 0, _enemy.RotationVelocity * Time.deltaTime);
        // по хорошему нужен coreManager или LevelConfig, где будет хранится инстанс плеера
        if (_player.IsInFalling)
        {
            var enemySprite = _enemy.GetComponent<SpriteRenderer>();
            enemySprite.color = Color.green;
        }
    }
}
