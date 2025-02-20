using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cusor : MonoBehaviour
{
    // Start is called before the first frame update

    public void cursor()
    {
        // 마우스를 화면 중앙에 고정하고 보이지 않게 설정
        Cursor.lockState = CursorLockMode.Locked;  // 마우스를 화면 중앙에 고정
        Cursor.visible = false;                    // 마우스를 화면에서 보이지 않게 설정
    }

}
