using UnityEngine;

public class MagneticItem : MonoBehaviour
{
    public GameObject magneticFieldPrefab;

    GameObject magnetField;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            gameObject.SetActive(false);
            magnetField = Instantiate(magneticFieldPrefab);
            magnetField.SetActive(true);
        }
    }
}
