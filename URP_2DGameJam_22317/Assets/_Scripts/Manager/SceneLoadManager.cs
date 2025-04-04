using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;
using System.Threading.Tasks;

/// <summary>
///注释
/// <summary>

public class SceneLoadManager :SingletonMono<SceneLoadManager>
{
    public AssetReference menu; //菜单场景
    public AssetReference map; //菜单场景

    private AssetReference currentScene; //当前场景
    private SceneInstance currentSceneInstance; // 保存加载的场景实例


    private void OnEnable()
    {
        LoadMenu();
    }


    //加载场景
    private async Task LoadSceneTask()
    {
        AsyncOperationHandle<SceneInstance> handle =
            Addressables.LoadSceneAsync(currentScene, LoadSceneMode.Additive);

        await handle.Task; // 直接等待 Task

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            currentSceneInstance = handle.Result; // 关键：保存场景实例
            SceneManager.SetActiveScene(handle.Result.Scene);
            Debug.Log($"场景加载完成: {currentScene}");
        }
    }
    //卸载场景
    private async Task UnLoadSceneTask()
    {
        await Task.Delay(450); // 450ms 延迟
        if (currentSceneInstance.Scene.IsValid())
        {
            Debug.Log($"开始卸载场景: {currentSceneInstance.Scene.name}");
            AsyncOperationHandle<SceneInstance> unloadHandle =
                Addressables.UnloadSceneAsync(currentSceneInstance);

            await unloadHandle.Task;

        }
    }

    public async void LoadMenu()
    {
        //有场景在加载
        if(currentSceneInstance.Scene.IsValid())
        {
            //卸载场景
            await UnLoadSceneTask();
        }

        currentScene = menu;
        await LoadSceneTask();

        
        Debug.Log("菜单场景加载完成");
    }
    public async void LoadMap()
    {
        //有场景在加载
        if (currentSceneInstance.Scene.IsValid())
        {
            //卸载场景
            await UnLoadSceneTask();
        }

        currentScene = map;
        await LoadSceneTask();


        Debug.Log("地图加载完成");
    }

    public async void LoadLevel(AssetReference levelScene)
    {
        //有场景在加载
        if (currentSceneInstance.Scene.IsValid())
        {
            //卸载场景
            await UnLoadSceneTask();
        }

        currentScene = levelScene;
        await LoadSceneTask();


        Debug.Log("关卡加载完成");
    }

}
