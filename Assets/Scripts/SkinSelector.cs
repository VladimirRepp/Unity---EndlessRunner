using System;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    [Header("Camera move Settings")]
    [SerializeField] private GameObject camera;
    [SerializeField] private float moveSpeed;

    [Header("Controllers Settings")]
    [SerializeField] private SkinDecorator decorator;
    [SerializeField] private StoreController storeController;

    private int _selectedIndex = 0;

    public Action<int, string, float> OnChangeHeaderSkin;

    public int SelectedIndex => _selectedIndex;
    public SkinConfig[] Configs => decorator.SkinConfigs;
    public StoreController StoreController => storeController;

    private void Awake()
    {
        if(camera == null)
            camera = Camera.main.gameObject;

        OnChangeHeaderSkin?.Invoke(decorator.SkinConfigs[_selectedIndex].id, decorator.SkinConfigs[_selectedIndex].name, decorator.SkinConfigs[_selectedIndex].cost);
    }

    private void CameraMove()
    {
        Vector3 newCameraPOsition = new Vector3(decorator.StartPoint.position.x + (decorator.StepBySkins * _selectedIndex), 0, -10);
        camera.transform.position = newCameraPOsition;
    }

    public void ToNext()
    {
        _selectedIndex = _selectedIndex < decorator.SkinConfigs.Length - 1 ? 
            _selectedIndex + 1 : 0;

        CameraMove();

        OnChangeHeaderSkin?.Invoke(decorator.SkinConfigs[_selectedIndex].id, decorator.SkinConfigs[_selectedIndex].name, decorator.SkinConfigs[_selectedIndex].cost);
    }

    public void ToPrev()
    {
        _selectedIndex = _selectedIndex > 0 ?
           _selectedIndex - 1 : decorator.SkinConfigs.Length - 1;

        CameraMove();

        OnChangeHeaderSkin?.Invoke(decorator.SkinConfigs[_selectedIndex].id, decorator.SkinConfigs[_selectedIndex].name, decorator.SkinConfigs[_selectedIndex].cost);
    }
}
