using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class CostumeItem : MonoBehaviour
{
    public int costumeId;
    public string itemName;

    [SerializeField] private float rotationSpeed = 450f;
    [SerializeField] GameObject costumitem;

    Vector3 _origin;

    public bool _isEating = false;

    private void Awake()
    {
        _origin = transform.eulerAngles;
    }

    private void Update()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y += rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, _origin.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ư�� �±��� �����۰� �浹 ����
        {
            _isEating = true;

            PlayerPrefs.SetInt($"HasCostume_{costumeId}", 1);
            PlayerPrefs.Save();
            
            CostumeUI.Instance.UpdateBtn(costumeId);
            Debug.Log("UI����");
            UIManager.Instance.ShowNoticeByAnimator($"Get {itemName}!", 2f);

            Destroy(this.gameObject); // ������ ����
            
        }
    }
}
