using UnityEngine;

public class BlendShapeController : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    public float seed = 0f;

    private 

    void Start()
    {
        // SkinnedMeshRenderer ��������
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        // "Nyam"�̶�� �̸��� BlendShape �ε����� ã��
        int blendShapeIndex = GetBlendShapeIndexByName("Nyam");
        

        if (blendShapeIndex != -1)
        {
            // "Nyam" BlendShape�� ���� 50���� ����
            SetBlendShape(blendShapeIndex, 50f);  // 50%�� ����
            Debug.Log("50");
        }

    }

    // BlendShape �̸����� �ε����� ã�� �޼���
    private int GetBlendShapeIndexByName(string blendShapeName)
    {
        for (int i = 0; i < skinnedMeshRenderer.sharedMesh.blendShapeCount; i++)
        {
            if (skinnedMeshRenderer.sharedMesh.GetBlendShapeName(i) == blendShapeName)
            {
                Debug.Log("ã��");
                return i;  // BlendShape�� �ε����� ��ȯ
            }
        }
        return -1;  // BlendShape�� ã�� �� ������ -1 ��ȯ
    }

    // BlendShape�� ���� �����ϴ� �޼���
    public void SetBlendShape(int index, float weight)
    {
        if (skinnedMeshRenderer != null)
        {
            // weight ���� 0~100 ���̷� ����
            skinnedMeshRenderer.SetBlendShapeWeight(index, Mathf.Clamp(weight, 0f, 100f));
            Debug.Log(index);
        }
    }
}

