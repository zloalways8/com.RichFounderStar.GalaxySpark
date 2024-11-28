using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/LevelData", order = 1)]
public class LevelDataSO : ScriptableObject
{
    public int levelIndex;
    public int scoreGoal;
    public float timeLimit;
    public bool isUnlocked; // По умолчанию false для всех, кроме первого уровня
}