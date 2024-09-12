using UnityEngine;
using UnityEngine.UI;

public class ItemQuantityManager : MonoBehaviour
{
    public Text[] quantityTexts;  // UI? ??? ??? Text ??
    private int[] itemQuantities;

    void Start()
    {
        // ??? ??? quantityTexts? ??? ????.
        itemQuantities = new int[quantityTexts.Length];
        InitializeQuantities();
    }

    void InitializeQuantities()
    {
        for (int i = 0; i < itemQuantities.Length; i++)
        {
            itemQuantities[i] = 0;
            UpdateQuantityDisplay(i);
        }
    }

    public void UpdateQuantityDisplay(int itemIndex)
    {
        // ??? ????? ??
        if (itemIndex >= 0 && itemIndex < quantityTexts.Length)
        {
            quantityTexts[itemIndex].text = itemQuantities[itemIndex].ToString();
        }
        else
        {
            Debug.LogError("Invalid itemIndex: " + itemIndex);
        }
    }

    public void IncreaseQuantity(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < itemQuantities.Length)
        {
            itemQuantities[itemIndex]++;
            UpdateQuantityDisplay(itemIndex);
        }
    }



    public void DecreaseQuantity(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < itemQuantities.Length && itemQuantities[itemIndex] > 0)
        {
            itemQuantities[itemIndex]--;
            UpdateQuantityDisplay(itemIndex);
        }
    }

    public int GetQuantity(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < itemQuantities.Length)
        {
            return itemQuantities[itemIndex];
        }
        else
        {
            Debug.LogError("Invalid itemIndex: " + itemIndex);
            return 0;
        }
    }

    public void ResetQuantities()
    {
        for (int i = 0; i < itemQuantities.Length; i++)
        {
            itemQuantities[i] = 0;
            UpdateQuantityDisplay(i);
        }
    }
}
