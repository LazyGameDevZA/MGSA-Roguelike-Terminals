using UnityEngine;

public class MoveCharacters : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        this.transform.position += new Vector3(horizontal, vertical) * (this.speed * Time.deltaTime);
    }
}
