using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    //public UserData UserData;
    public TextMeshProUGUI text;

    [SerializeField]
    private Transform playerTransform;
}
