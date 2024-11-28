using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text balanceText; // Текст для отображения баланса
    public Button[] upgradeButtons; // Кнопки улучшений
    public TMP_Text[] upgradeTexts; // Тексты на кнопках улучшений

    [Header("Upgrade Data")]
    public int[] upgradeCosts = { 20, 100, 400, 1000 }; // Цены улучшений
    public int[] upgradeValues = { 5, 20, 50, 100 }; // Значения улучшений

    private int balance; // Баланс игрока
    private int totalHP; // Итоговое HP

    public AudioClip btnClick;
    void Start()
    {
        // Загружаем баланс и итоговое HP
        balance = PlayerPrefs.GetInt("Balance", 0);
        totalHP = PlayerPrefs.GetInt("TotalHP", 100); // Стартовое HP по умолчанию — 100

        // Инициализируем UI
        UpdateUI();
    }

    public void PurchaseUpgrade(int upgradeIndex)
    {

        if (upgradeIndex < 0 || upgradeIndex >= upgradeCosts.Length) return;

        int cost = upgradeCosts[upgradeIndex];
        int value = upgradeValues[upgradeIndex];

        // Проверяем, хватает ли баланса и не куплено ли улучшение
        if (balance >= cost && PlayerPrefs.GetInt($"Upgrade_{upgradeIndex}", 0) == 0)
        {
            // Совершаем покупку
            balance -= cost;
            totalHP += value;
            AudioManager.Instance.PlaySFX(btnClick);

            // Сохраняем изменения
            PlayerPrefs.SetInt("Balance", balance);
            PlayerPrefs.SetInt("TotalHP", totalHP);
            PlayerPrefs.SetInt($"Upgrade_{upgradeIndex}", 1); // Отмечаем улучшение как купленное
            PlayerPrefs.Save();

            Debug.Log($"Purchased upgrade: +{value} HP for {cost} credits. Total HP: {totalHP}");

            // Обновляем UI
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough balance or upgrade already purchased!");
        }
    }

    private void UpdateUI()
    {
        // Обновляем текст баланса
        balanceText.text = $"Balance: {balance:0000}";

        // Обновляем состояние кнопок
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            if (upgradeButtons[i] != null)
            {
                // Проверяем, куплено ли улучшение
                bool isPurchased = PlayerPrefs.GetInt($"Upgrade_{i}", 0) == 1;

                // Проверяем, достаточно ли баланса для покупки
                bool canAfford = balance >= upgradeCosts[i];

                // Кнопка доступна только если улучшение не куплено и хватает баланса
                upgradeButtons[i].interactable = !isPurchased && canAfford;

                // Обновляем текст на кнопке
                if (upgradeTexts[i] != null)
                {
                    upgradeTexts[i].text = isPurchased ? "Purchased" : $"{upgradeCosts[i]}";
                }
            }
        }
    }

}
