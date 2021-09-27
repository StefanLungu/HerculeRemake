using UnityEngine;

public class Flame : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeHit();
            gameObject.GetComponentInParent<Boss2>().playerHit = true;
        }

    }
}
