using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using System.Threading.Tasks;

public class SceneLoadManager : SingletonMono<SceneLoadManager>
{
    public GameObject unlockPanel;

    public AssetReference menu;
    public AssetReference president;
    public AssetReference map;
    public AssetReference level1_1;

    private AssetReference currentScene;
    private SceneInstance currentSceneInstance;
    private bool isLoading = false;

    public ObjectEventSO OnLoadLevelafterEvent;

    // 记录 boot 加载的 persistent President Scene 名字
    private string persistentSceneName = "President"; // 或用 runtimeKey 也行

    private void OnEnable()
    {
        // 不再加载 President，由 Boot 负责
        LoadMenu();
    }

    private async Task LoadSceneTask()
    {
        if (currentScene == null) return;

        isLoading = true;
        Debug.Log($"开始加载场景：{currentScene.RuntimeKey}");

        AsyncOperationHandle<SceneInstance> handle =
            Addressables.LoadSceneAsync(currentScene, LoadSceneMode.Additive);

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            currentSceneInstance = handle.Result;
            Debug.Log($"成功加载场景：{handle.Result.Scene.name}");
        }
        else
        {
            Debug.LogError($"加载场景失败：{currentScene.RuntimeKey}");
        }

        isLoading = false;
    }

    private async Task UnLoadSceneTask()
    {
        if (currentSceneInstance.Scene.IsValid())
        {
            var sceneName = currentSceneInstance.Scene.name;

            // 不允许卸载 President
            if (sceneName == persistentSceneName ||
                (currentScene != null && currentScene.RuntimeKey.ToString() == president.RuntimeKey.ToString()))
            {
                Debug.LogWarning($"阻止卸载持久场景：{sceneName}");
                return;
            }

            Debug.Log($"卸载场景：{sceneName}");
            AsyncOperationHandle<SceneInstance> unloadHandle =
                Addressables.UnloadSceneAsync(currentSceneInstance);

            await unloadHandle.Task;
        }
    }

    private async Task SafeLoadScene(AssetReference targetScene)
    {
        if (isLoading || targetScene == null) return;

        // 阻止意外传入 president
        if (targetScene.RuntimeKey.ToString() == president.RuntimeKey.ToString())
        {
            Debug.LogWarning("禁止通过 SceneLoadManager 加载 President 场景");
            return;
        }

        isLoading = true;

        if (currentSceneInstance.Scene.IsValid())
        {
            await UnLoadSceneTask();
        }

        currentScene = targetScene;
        await LoadSceneTask();

        isLoading = false;
    }

    public async void LoadMenu() => await SafeLoadScene(menu);
    public async void LoadMap() => await SafeLoadScene(map);

    public async void LoadLevel(object levelSceneObj)
    {
        if (levelSceneObj is AssetReference levelScene)
        {
            await SafeLoadScene(levelScene);
            OnLoadLevelafterEvent?.RaiseEvent(null, this);
        }
    }

    public async void OnMapLoadLevel(object levelSceneObj)
    {
        if (levelSceneObj is level levelScene)
        {
            if (levelScene.levelState != LevelState.locked)
            {
                await SafeLoadScene(levelScene.curScene);
                OnLoadLevelafterEvent?.RaiseEvent(null, this);
            }
            else
            {
                Debug.Log("该关卡未解锁");
                if (unlockPanel != null)
                    unlockPanel.SetActive(true);
            }
        }
    }

    public async void NewGame()
    {
        await SafeLoadScene(level1_1);
        OnLoadLevelafterEvent?.RaiseEvent(null, this);
    }
}
