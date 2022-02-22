using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject FireBall;
    public int damage;
    public float health;

    public float knockback = 6;
    
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector2[] positions;

    private int index = 0;

    [Header("Item Drops")]
    public int itemOnePercentChance;
    public GameObject itemOne;
    public int itemTwoPercentChance;
    public GameObject itemTwo;
    public int itemThreePercentChance;
    public GameObject itemThree;

    // Update is called once per frame

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);

        if (Mathf.Abs(transform.position.x - positions[index].x) < .1)
        {
            if(index == positions.Length-1)
            {
    
                index = 0;
            }
            else
            {
                index++;
            }
        }

        if (health < 0) {
            Destroy(gameObject);

            int diceRoll = Random.Range(1,101);
            if (diceRoll <= itemOnePercentChance){
                Instantiate(itemOne, new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);
            } else if (diceRoll <= itemTwoPercentChance+itemTwoPercentChance){
                Instantiate(itemTwo, new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);
            } else {
                Instantiate(itemThree, new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);
            }
        }

        if (gameObject.name == "demon skull")    
        {
            GameObject shoot = Instantiate(FireBall, transform.position, Quaternion.identity);
            float shootAngle = Vector2.Angle(transform.position, PlayerController.player.transform.position);

            shoot.GetComponent<Rigidbody2D>().rotation = shootAngle;
            shoot.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(shootAngle * Mathf.Deg2Rad) * 10, Mathf.Sin(shootAngle * Mathf.Deg2Rad) * 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.health -= damage - PlayerController.armor;
            collision.attachedRigidbody.velocity += new Vector2(Mathf.Sign(collision.transform.position.x - transform.position.x) * knockback * -2, knockback / 2);
        }

    }
}



