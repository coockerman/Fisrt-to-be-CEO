using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int level;
    public float expRequired;

    public LevelData(int level, float expRequired)
    {
        this.level = level;
        this.expRequired = expRequired;
    }
}
[CreateAssetMenu(fileName = "DataLVPlayer", menuName = "Player/DataLVPlayer")]
public class DataLVPlayer : ScriptableObject
{
    public LevelData[] levels = new LevelData[10];
}
