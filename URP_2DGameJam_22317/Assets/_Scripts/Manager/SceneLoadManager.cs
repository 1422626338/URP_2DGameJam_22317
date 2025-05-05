using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;
using System.Threading.Tasks;

/// <summary>
///注释
/// <summary>

public class SceneLoadManager : SingletonMono<SceneLoadManager>
{
    public AssetReference menu; //菜单场景
    public AssetReference map; //地图场景
    public AssetReference level1_1;//第一关场景

    private AssetReference currentScene; //当前场景
    private SceneInstance currentSceneInstance;

    private bool isLoading = false; // 防止多次并发加载

    private void OnEnable()
    {
        //LoadMenu();
    }

    private async Task LoadSceneTask()
    {
        if (currentScene == null) return;

        isLoading = true;

        AsyncOperationHandle<SceneInstance> handle =
            Addressables.LoadSceneAsync(currentScene, LoadSceneMode.Additive);

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            currentSceneInstance = handle.Result;
            SceneManager.SetActiveScene(handle.Result.Scene);
        }

        isLoading = false;
    }

    private async Task UnLoadSceneTask()
    {
        if (currentSceneInstance.Scene.IsValid())
        {
            AsyncOperationHandle<SceneInstance> unloadHandle =
                Addressables.UnloadSceneAsync(currentSceneInstance);

            await unloadHandle.Task;
        }
    }

    private async Task SafeLoadScene(AssetReference targetScene)
    {
        if (isLoading) return;

        isLoading = true;

        if (currentSceneInstance.Scene.IsValid())
        {
            await UnLoadSceneTask();
        }

        currentScene = targetScene;
        await LoadSceneTask();

        isLoading = false;
    }

    public async void LoadMenu()
    {
        await SafeLoadScene(menu);
    }

    public async void LoadMap()
    {
        await SafeLoadScene(map);
    }

    public async void LoadLevel(object levelSceneObj)
    {
        if (levelSceneObj is AssetReference levelScene)
        {
            await SafeLoadScene(levelScene);
        }
    }

    public async void  NewGame()
    {
        await SafeLoadScene(level1_1);
    }
}
