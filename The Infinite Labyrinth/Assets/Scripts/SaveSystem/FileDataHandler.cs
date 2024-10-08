using UnityEngine;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";

    private string dataFileName = "data.json";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        GameData loadedData = null;

        if(File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using(FileStream stream = new FileStream(fullPath, FileMode.Open)) 
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }

                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch
            {
                Debug.LogError("Error occurred while LOADING data from file!");
            }
        }
        return loadedData;
    }

    public SettingsData LoadSettings()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        SettingsData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }

                }
                loadedData = JsonUtility.FromJson<SettingsData>(dataToLoad);
            }
            catch
            {
                Debug.LogError("Error occurred while LOADING data from file!");
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        string dataToStore = JsonUtility.ToJson(data, true);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch
        {
            Debug.LogError("Error occured while SAVING data to file!");
        }
    }

    public void SaveSettings(SettingsData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        string dataToStore = JsonUtility.ToJson(data, true);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch
        {
            Debug.LogError("Error occured while SAVING data to file!");
        }
    }

    public void Delete()
    {
        if(!DoesDataExists())
        {
            return;
        }

        try
        {
            File.Delete(Path.Combine(dataDirPath, dataFileName));
        }
        catch
        {
            Debug.LogError("Error occurred while DELETING data!");
        }
    }

    public bool DoesDataExists()
    {
        return File.Exists(Path.Combine(dataDirPath, dataFileName));
    }
}
