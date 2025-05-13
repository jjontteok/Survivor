using System.Runtime.Serialization;
using UnityEngine;

public class RepositionMap : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != Define.AreaTag) return;
        if (GameManager.Instance != null && GameManager.Instance.Player != null)
        {
            Vector3 playerPos = GameManager.Instance.Player.transform.position;
            Vector3 myPos = transform.position;

            float dirX = playerPos.x - myPos.x;
            float dirY = playerPos.y - myPos.y;

            float diffX = Mathf.Abs(dirX);
            float diffY = Mathf.Abs(dirY);

            dirX = dirX > 0 ? 1 : -1;
            dirY = dirY > 0 ? 1 : -1;

            if (transform.tag == Define.GroundTag)
            {
                if (diffX > 20)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffY > 10)
                {
                    transform.Translate(Vector3.up * dirY * 20);
                }
            }
        }
    }
}
