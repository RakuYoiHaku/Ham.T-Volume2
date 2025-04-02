using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtils
{
    // 비활성화된 오브젝트까지 찾는 함수
    public static GameObject FindInActiveObjectByName(string name)
    {
        GameObject[] objs = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in objs)
        {
            if (obj.name == name)
                return obj;
        }
        return null;
    }

    // 비활성화된 오브젝트까지 찾는 함수
    public static GameObject[] FindInActiveObjectByTag(string tag)
    {
        List<GameObject> matchingObjects = new List<GameObject>();
        GameObject[] objs = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in objs)
        {
            // 비활성화된 객체도 태그로 비교할 수 있습니다.
            if (obj.CompareTag(tag))
                matchingObjects.Add(obj);
        }
        return matchingObjects.ToArray();
    }

}

