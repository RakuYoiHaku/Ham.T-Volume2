using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinItem : MonoBehaviour
{
    public int skinId;

    [SerializeField] private float rotationSpeed = 450f;
    [SerializeField] GameObject skinItem;

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

            //CostumeManager.Instance.AddCostume(costumeId);

            CostumeUI.Instance.SkinButton(skinId);
            Debug.Log("UI����");

            Destroy(this.gameObject); // ������ ����
        }
    }
}
