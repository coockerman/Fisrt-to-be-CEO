using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int level;
    public float expRequired;
    public GameObject bodyPlayer;

    public LevelData(LevelData levelData)
    {
        this.level = levelData.level;
        this.expRequired = levelData.expRequired;
        this.bodyPlayer = levelData.bodyPlayer;
    }
}
[CreateAssetMenu(fileName = "DataLVPlayer", menuName = "Player/DataLVPlayer")]
public class DataLVPlayer : ScriptableObject
{
    public LevelData[] levels = new LevelData[10];
}
