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

    private AssetReference currentScene; //当前场景
    private SceneInstance currentSceneInstance; // 保存加载的场景实例


    private void Awake()
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
            SceneManager.SetActiveScene(handle.Result.Scene);
        }
    }
    //卸载场景
    private async Task UnLoadSceneTask()
    {
        await Task.Delay(450); // 450ms 延迟
        if (currentSceneInstance.Scene.IsValid()) // 检查场景是否有效
        {
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

}
