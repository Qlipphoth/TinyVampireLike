using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemLaser : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] GameObject hitPlayerVFX;
    [SerializeField] GameObject hitEnemyVFX;
    
    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<Player>(out Player player)) {
            player.TakeDamage(damage);
            var contactPoint = other.GetContact(0);
            PoolManager.Release(hitPlayerVFX, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
        }
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy)) {
            enemy.Die();
            var contactPoint = other.GetContact(0);
            PoolManager.Release(hitEnemyVFX, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
        }
    }
}
