using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollText : MonoBehaviour
{

    public Image renderer;
    private float offset = 0;
    public float scrollSpeed;

    // Update is called once per frame
    void Update()
    {
        offset+= (Time.deltaTime*scrollSpeed)/10f;
        renderer.material.SetTextureOffset ("_MainTex", new Vector2(offset,offset));

    }
}
