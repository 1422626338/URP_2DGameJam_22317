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

        }
    }
    //卸载场景
    private async Task UnLoadSceneTask()
    {

        if (currentSceneInstance.Scene.IsValid())
        {

            AsyncOperationHandle<SceneInstance> unloadHandle =
                Addressables.UnloadSceneAsync(currentSceneInstance);

            await unloadHandle.Task;

        }
    }

    public async void LoadMenu()
    {
        //有场景在加载
        if (currentSceneInstance.Scene.IsValid())
        {
            //卸载场景
            await UnLoadSceneTask();
        }

        currentScene = menu;
        await LoadSceneTask();

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

    }

    public async void LoadLevel(object levelSceneObj)
    {
        var levelScene = levelSceneObj as AssetReference;
        Debug.Log("test2");

        //有场景在加载
        if (currentSceneInstance.Scene.IsValid())
        {
            //卸载场景
            await UnLoadSceneTask();
            Debug.Log("test3");

        }

        currentScene = levelScene;
        await LoadSceneTask();

    }

    public async void NewGame()
    {
        //有场景在加载
        if (currentSceneInstance.Scene.IsValid())
        {
            //卸载场景
            await UnLoadSceneTask();
        }

        currentScene = level1_1;
        await LoadSceneTask();
    }



}