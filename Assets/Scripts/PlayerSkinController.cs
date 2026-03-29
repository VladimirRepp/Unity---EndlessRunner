using UnityEngine;

public class PlayerSkinController : MonoBehaviour
{
    [SerializeField] private SkinConfig[] skinConfigs;
    [SerializeField] private Transform anchorSkin;
    
    private int _selectedIndexSkin = -1;

    private void Awake()
    {
        GetSelectedIdSkin();

        GameObject oldSkin = anchorSkin.GetChild(0).gameObject;
        if (oldSkin != null)
            Destroy(oldSkin);

        GameObject skin = Instantiate(skinConfigs[_selectedIndexSkin].prefab);
        skin.transform.position = new Vector3(0, 2, 0);
        skin.transform.parent = anchorSkin;
    }

    private void GetSelectedIdSkin()
    {
        int idSelected = DataController.Instance.GetSelectedSkin();

        for (int i = 0; i < skinConfigs.Length; i++)
        {
            if (idSelected == skinConfigs[i].id)
            {
                _selectedIndexSkin = i;
                break;
            }
        }

        if (_selectedIndexSkin == -1)
            _selectedIndexSkin = 0;
    }
}
