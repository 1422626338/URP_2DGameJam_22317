using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine;
 
 /// <summary>
 ///注释
 /// <summary>

public class BackToMenuButton : MonoBehaviour
{
    private Button backToMenuButton;

    private void Awake()
    {
        backToMenuButton = GetComponent<Button>();
        backToMenuButton.onClick.AddListener(LoadMenu);
    }

    private void LoadMenu()
    {
        SceneLoadManager.Instance.LoadMenu();
    }
}
