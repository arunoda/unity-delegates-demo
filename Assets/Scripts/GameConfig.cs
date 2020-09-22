using System.Collections;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public delegate void FreezeChangeHandler(bool isFreezed);
    private FreezeChangeHandler freezeHandlers;

    public static GameConfig current;
    private bool freezed;

    void Start()
    {
        GameConfig.current = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnFreezeChange(FreezeChangeHandler e)
    {
        freezeHandlers += e;
    }

    public void OffFreezeChange(FreezeChangeHandler e)
    {
        if (freezeHandlers == null) return;
        freezeHandlers -= e;
    }

    public void Freeze()
    {
        if (IsFreezed())
        {
            return;
        }

        freezed = true;
        freezeHandlers?.Invoke(true);
    }

    public void UnFreeze()
    {
        if (!IsFreezed())
        {
            return;
        }

        freezed = false;
        freezeHandlers?.Invoke(false);
    }

    public bool IsFreezed()
    {
        return freezed;
    }

    public void FreezeFor1Sec()
    {
        if (IsFreezed()) return;

        _ = StartCoroutine(nameof(StartFreezing));
    }

    IEnumerator StartFreezing()
    {
        Freeze();
        yield return new WaitForSeconds(1);
        UnFreeze();
    }
}
