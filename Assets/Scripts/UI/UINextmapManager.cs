using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINextmapManager : MonoSingleton<UINextmapManager>
{
    [SerializeField] private CanvasGroup nextMap;

    public void ActiveNextMap()
    {
        StartCoroutine(IEActiveNextMap());
    }

    IEnumerator IEActiveNextMap()
    {
        while (nextMap.alpha < 0.99)
        {
            nextMap.alpha += Time.deltaTime * 2f;
            yield return null;
        }

        nextMap.alpha = 1;
        yield return new WaitForSeconds(2f);
        
        while (nextMap.alpha > 0.05f)
        {
            nextMap.alpha -= Time.deltaTime * 2f;
            yield return null;
        }

        nextMap.alpha = 0;

    }
}
