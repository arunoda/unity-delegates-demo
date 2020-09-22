using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject drop;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(SpawnDrops));
        GameConfig.current.OnFreezeChange(WithFreezeChange);
    }

    void OnDestroy()
    {
        GameConfig.current.OffFreezeChange(WithFreezeChange);
    }

    Vector3 GetScreenSize()
    {
        var pixels = new Vector3(Screen.width, Screen.height, 0);
        return Camera.main.ScreenToWorldPoint(pixels);
    }

    IEnumerator SpawnDrops()
    {
        while(true)
        {
            if (drop)
            {
                var screenSize = GetScreenSize();
                var xPos = Random.Range(-screenSize.x, screenSize.x);
                _ = Instantiate(drop, new Vector3(xPos, screenSize.y, 0), Quaternion.identity);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    void WithFreezeChange(bool isFreezed)
    {
        if (isFreezed)
        {
            StopCoroutine(nameof(SpawnDrops));
        } else
        {
            _ = StartCoroutine(nameof(SpawnDrops));
        }
    }
}
