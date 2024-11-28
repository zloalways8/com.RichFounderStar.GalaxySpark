using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text balanceText; // ����� ��� ����������� �������
    public Button[] upgradeButtons; // ������ ���������
    public TMP_Text[] upgradeTexts; // ������ �� ������� ���������

    [Header("Upgrade Data")]
    public int[] upgradeCosts = { 20, 100, 400, 1000 }; // ���� ���������
    public int[] upgradeValues = { 5, 20, 50, 100 }; // �������� ���������

    private int balance; // ������ ������
    private int totalHP; // �������� HP

    public AudioClip btnClick;
    void Start()
    {
        // ��������� ������ � �������� HP
        balance = PlayerPrefs.GetInt("Balance", 0);
        totalHP = PlayerPrefs.GetInt("TotalHP", 100); // ��������� HP �� ��������� � 100

        // �������������� UI
        UpdateUI();
    }

    public void PurchaseUpgrade(int upgradeIndex)
    {

        if (upgradeIndex < 0 || upgradeIndex >= upgradeCosts.Length) return;

        int cost = upgradeCosts[upgradeIndex];
        int value = upgradeValues[upgradeIndex];

        // ���������, ������� �� ������� � �� ������� �� ���������
        if (balance >= cost && PlayerPrefs.GetInt($"Upgrade_{upgradeIndex}", 0) == 0)
        {
            // ��������� �������
            balance -= cost;
            totalHP += value;
            AudioManager.Instance.PlaySFX(btnClick);

            // ��������� ���������
            PlayerPrefs.SetInt("Balance", balance);
            PlayerPrefs.SetInt("TotalHP", totalHP);
            PlayerPrefs.SetInt($"Upgrade_{upgradeIndex}", 1); // �������� ��������� ��� ���������
            PlayerPrefs.Save();

            Debug.Log($"Purchased upgrade: +{value} HP for {cost} credits. Total HP: {totalHP}");

            // ��������� UI
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough balance or upgrade already purchased!");
        }
    }

    private void UpdateUI()
    {
        // ��������� ����� �������
        balanceText.text = $"Balance: {balance:0000}";

        // ��������� ��������� ������
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            if (upgradeButtons[i] != null)
            {
                // ���������, ������� �� ���������
                bool isPurchased = PlayerPrefs.GetInt($"Upgrade_{i}", 0) == 1;

                // ���������, ���������� �� ������� ��� �������
                bool canAfford = balance >= upgradeCosts[i];

                // ������ �������� ������ ���� ��������� �� ������� � ������� �������
                upgradeButtons[i].interactable = !isPurchased && canAfford;

                // ��������� ����� �� ������
                if (upgradeTexts[i] != null)
                {
                    upgradeTexts[i].text = isPurchased ? "Purchased" : $"{upgradeCosts[i]}";
                }
            }
        }
    }

}
