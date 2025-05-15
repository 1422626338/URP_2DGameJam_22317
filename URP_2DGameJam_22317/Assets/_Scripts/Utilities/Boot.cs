using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    public AssetReference president;

    private async void Awake()
    {
       
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(president, LoadSceneMode.Additive);
        await handle.Task;

       
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            SceneManager.SetActiveScene(handle.Result.Scene);
        }
    }
}
