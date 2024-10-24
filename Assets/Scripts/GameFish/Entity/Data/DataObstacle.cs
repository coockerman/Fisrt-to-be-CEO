using UnityEngine;

[CreateAssetMenu(fileName = "DataObstacle", menuName = "Entity/dataObstacle")]
public class DataObstacle : DataEntity
{
    public EObstacle TypeObstacle = EObstacle.Waste;
    public float TimeEffect = 2f;
}

