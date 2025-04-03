using UnityEngine;

public class BlendShapeController : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    public float seed = 0f;

    private 

    void Start()
    {
        // SkinnedMeshRenderer 가져오기
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        // "Nyam"이라는 이름의 BlendShape 인덱스를 찾기
        int blendShapeIndex = GetBlendShapeIndexByName("Nyam");
        

        if (blendShapeIndex != -1)
        {
            // "Nyam" BlendShape의 값을 50으로 설정
            SetBlendShape(blendShapeIndex, 50f);  // 50%로 설정
            Debug.Log("50");
        }

    }

    // BlendShape 이름으로 인덱스를 찾는 메서드
    private int GetBlendShapeIndexByName(string blendShapeName)
    {
        for (int i = 0; i < skinnedMeshRenderer.sharedMesh.blendShapeCount; i++)
        {
            if (skinnedMeshRenderer.sharedMesh.GetBlendShapeName(i) == blendShapeName)
            {
                Debug.Log("찾음");
                return i;  // BlendShape의 인덱스를 반환
            }
        }
        return -1;  // BlendShape를 찾을 수 없으면 -1 반환
    }

    // BlendShape의 값을 설정하는 메서드
    public void SetBlendShape(int index, float weight)
    {
        if (skinnedMeshRenderer != null)
        {
            // weight 값이 0~100 사이로 설정
            skinnedMeshRenderer.SetBlendShapeWeight(index, Mathf.Clamp(weight, 0f, 100f));
            Debug.Log(index);
        }
    }
}

