using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtils
{
    // ��Ȱ��ȭ�� ������Ʈ���� ã�� �Լ�
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

    // ��Ȱ��ȭ�� ������Ʈ���� ã�� �Լ�
    public static GameObject[] FindInActiveObjectByTag(string tag)
    {
        List<GameObject> matchingObjects = new List<GameObject>();
        GameObject[] objs = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in objs)
        {
            // ��Ȱ��ȭ�� ��ü�� �±׷� ���� �� �ֽ��ϴ�.
            if (obj.CompareTag(tag))
                matchingObjects.Add(obj);
        }
        return matchingObjects.ToArray();
    }

}

