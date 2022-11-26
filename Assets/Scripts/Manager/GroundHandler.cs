using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundHandler : MonoSingleton<GroundHandler>
{
    public List<TreeObject> treeObjects;
    
    #if UNITY_EDITOR

    private void OnValidate()
    {
        treeObjects = GetComponentsInChildren<TreeObject>().ToList();
    }

#endif
    public int TreeLenght => treeObjects.Count;
    public void RemoveTree(TreeObject T)
    {
        if (treeObjects.Contains(T))
        {
            treeObjects.Remove(T);
        }
        
        if(TreeLenght <= 0)
            GameManager.Instance.OnEndGame();
    }
}
