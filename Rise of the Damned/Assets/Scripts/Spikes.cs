using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    public float knockback;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag.Equals("Player")) { //if player collides with spikes
            PlayerController.health = PlayerController.health - 10;
            //knockback
            collision.attachedRigidbody.velocity += new Vector2(Mathf.Sign(collision.transform.position.x - transform.position.x) * knockback, knockback / 2);
        }
    }

}
