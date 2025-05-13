using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class LevelStateWrapper
{
    public List<LevelState> levels;
}

public class SaveDataManager : SingletonMono<SaveDataManager>
{
    public List<LevelState> list = new List<LevelState>();

    private void Start()
    {
        InitData();
        loadData();
    }
    public void InitData()
    {
        Debug.Log("list = " + list == null); 
        Debug.Log("levellist = " + LevelManager.Instance.levelList.Count);
        LevelManager.Instance.levelList[0].levelState = LevelState.unlock;
        for (int i = 0; i < 4; i++)
        {
            LevelManager.Instance.levelList[i + 1].levelState = LevelState.locked;
        }

        if (list.Count == 0)
        {
            list.Add(LevelState.unlock);
            for (int i = 0; i < 4; i++)
            {
                list.Add(LevelState.locked);
            }
        }
        else
        {
            list[0] = LevelState.unlock;
            for (int i = 0; i < 4; i++)
            {
                LevelManager.Instance.levelList[i + 1].levelState = LevelState.locked;
                list[i] = LevelState.locked;
            }
        }       
    }

    public void SaveData()
    {

        Debug.Log("instance = " + LevelManager.Instance == null);
        Debug.Log("levellist = " + LevelManager.Instance.levelList.Count);
        Debug.Log("list = " + list.Count);
        for (int i = 0; i < 5; i++)
        {
            list[i] = LevelManager.Instance.levelList[i].levelState;
        }

        LevelStateWrapper wrapper = new LevelStateWrapper();
        wrapper.levels = list;

        string json = JsonUtility.ToJson(wrapper , true);
        string filepath = Application.streamingAssetsPath + "/savedData.json";

        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        using (StreamWriter sw = new StreamWriter(filepath)) 
        {
            sw.WriteLine(json);
            sw.Close();
            sw.Dispose();
            Debug.Log("存储完成: " + json);
        }
    }

    public void loadData()
    {
        string filepath = Path.Combine(Application.streamingAssetsPath, "savedData.json");
        if (File.Exists(filepath))
        {
            string json = File.ReadAllText(filepath);
            LevelStateWrapper wrapper = JsonUtility.FromJson<LevelStateWrapper>(json);
            list = wrapper.levels;
        }
        Debug.Log("instance = " + LevelManager.Instance == null);
        Debug.Log("levellist = " + LevelManager.Instance.levelList.Count);
        Debug.Log("list = " + list.Count);
        for (int i = 0; i < 5;i++) 
        {
            LevelManager.Instance.levelList[i].levelState = list[i];
        }
    }

    public void OnquitEvent()
    {
        SaveData();
        Application.Quit();
    }
   
}
