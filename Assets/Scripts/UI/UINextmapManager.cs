using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UINextmapManager : MonoSingleton<UINextmapManager>
{
    [SerializeField] private CanvasGroup nextMap;

    public void GoToSceneLV1()
    {
        SoundManager.Instance.Play("BG",AudioType.Soundtrack);
        SceneManager.LoadScene(1);
        MapCaching.Instance.currentMap = 1;
    }

    public void ActiveNextMap()
    {
        MapCaching.Instance.currentMap++;
        if (MapCaching.Instance.currentMap > 3)
            MapCaching.Instance.currentMap = 1;
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

        SceneManager.LoadSceneAsync(MapCaching.Instance.currentMap);
        yield return new WaitForSeconds(2f);

        while (nextMap.alpha > 0.05f)
        {
            nextMap.alpha -= Time.deltaTime * 2f;
            yield return null;
        }

        nextMap.alpha = 0;
    }
}