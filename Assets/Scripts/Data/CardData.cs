using System.Collections;
using System.Collections.Generic;
using SerializeReferenceEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Card")]
public class CardData : ScriptableObject 
{
   [field: SerializeField] public string Desctiption { get; private set; }
   [field: SerializeField] public int Mana { get; private set; }
   [field: SerializeField] public Sprite Icon { get; private set; }
   [field: SerializeReference, SR] public List<Effect> Effects { get; private set; }
}
