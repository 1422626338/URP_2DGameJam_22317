using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
/// <summary>
///注释
/// <summary>

public class GoToLevelButton : MonoBehaviour
{
    public AssetReference levelScene; //关卡场景

    private Button goToLevelButton;

    private void OnEnable()
    {
        goToLevelButton = GetComponent<Button>();
        goToLevelButton.onClick.AddListener(loadLevelScene);
    }

    //加载关卡
    private void loadLevelScene()
    {
        SceneLoadManager.Instance.LoadLevel(levelScene);
    }

}
