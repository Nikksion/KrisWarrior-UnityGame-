using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out AbstractPlayer player)) {
            player.TakeDamage(1);
            Debug.Log("PlayerTakeDamage");
        }
    }




}
