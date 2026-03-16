using UnityEngine;
using UnityEngine.UI;


public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text AmountText;


    public void Setup(InventoryItem item)
    {
        if (item == null || item.data == null) return;

        if (iconImage != null)
        {
            iconImage.sprite = item.data.icon;
            iconImage.enabled = item.data.icon != null;
        }

        if (nameText != null && AmountText != null)
        {
            nameText.text = item.data.pickUpName;
            AmountText.text = " x" + item.amount;

        }
    }
}
