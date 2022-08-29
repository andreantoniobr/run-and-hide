using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMaterialController : MonoBehaviour
{
    [SerializeField] private ButtonObject button;
    [SerializeField] private Material materialActive;
    [SerializeField] private Material materialInactive;

    Material material;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        button.ButtonActiveEvent += OnButtonActive;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnDestroy()
    {
        button.ButtonActiveEvent -= OnButtonActive;
    }

    private void OnButtonActive(bool isActive)
    {
        material = materialInactive;
        if (isActive)
        {
            material = materialActive;
        }
        if (meshRenderer && material)
        {
            meshRenderer.material = material;
        }
    }
}
