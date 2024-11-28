using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int levelIndex;
    public Button button;
    public TMP_Text levelText;

    public MenuManager menuManager;

    private void Start()
    {
        Setup(levelIndex, LevelManager.Instance.IsLevelUnlocked(levelIndex), menuManager);
    }

    public void Setup(int levelIndex, bool isUnlocked, MenuManager menuManager)
    {
        this.levelIndex = levelIndex;
        this.menuManager = menuManager;

        levelText.text = $"Level {levelIndex+1}";
        button.interactable = isUnlocked;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => menuManager.LoadLevel(levelIndex));
    }
}
