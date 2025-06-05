using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaUI : MonoBehaviour
{
    [SerializeField] private TMP_Text txtMana;

    public void UpdateMana(int mana)
    {
        txtMana.text = mana.ToString();
    }
}
