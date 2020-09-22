using UnityEngine;

public class Drop : MonoBehaviour
{
    void Start()
    {
        GameConfig.current.OnFreezeChange(WithFreezeChange);
    }

    private void OnDestroy()
    {
        GameConfig.current.OffFreezeChange(WithFreezeChange);
    }

    void Update()
    {
        transform.position += Vector3.down * (5 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    private void WithFreezeChange(bool isFreezed)
    {
        gameObject.SetActive(!isFreezed);
    }
}
