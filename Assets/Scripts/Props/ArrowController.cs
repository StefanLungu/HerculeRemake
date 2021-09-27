using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !collision.isTrigger)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeHit();
            gameObject.SetActive(false);
        }

    }
}
