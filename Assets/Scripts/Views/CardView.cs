using System;
using UnityEngine;
using TMPro;

/// <summary>
/// 卡牌视图类
/// </summary>
public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text txtMana;
    [SerializeField] private TMP_Text txtTitle;
    [SerializeField] private TMP_Text txtDesc;
    [SerializeField] private SpriteRenderer imgIcon;
    [SerializeField] private GameObject wrapper;
    [SerializeField] private LayerMask dropLayer;
    public Card Card { get; private set; }
    private Vector3 dragBeginPos;
    private Quaternion dragBeginRot;

    public void SetUp(Card card)
    {
        Card = card;
        txtTitle.text = card.Title;
        txtDesc.text = card.Description;
        txtMana.text = card.Mana.ToString();
        imgIcon.sprite = card.Icon;
    }

    private void OnMouseEnter()
    {
        if(!Interactions.Instance.PlayerCanHover()) return;
        wrapper.SetActive(false);
        CardViewHoverSystem.Instance.Show(Card, new Vector3(transform.position.x,-2f,0f));
    }
    
    private void OnMouseExit()
    {
        if(!Interactions.Instance.PlayerCanHover()) return;
        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }
    
    private void OnMouseDown()
    {
        if(!Interactions.Instance.PlayerCanInteract()) return;
        Interactions.Instance.PlayerIsDragging = true;
        wrapper.SetActive(true);
        CardViewHoverSystem.Instance.Hide();
        dragBeginPos = transform.position;
        dragBeginRot = transform.rotation;
        transform.rotation = Quaternion.Euler(0,0,0);
        transform.position = MouseUtil.GetMouseWorldPosition(-1);
    }

    private void OnMouseDrag()
    {
        if(!Interactions.Instance.PlayerCanInteract()) return;
        transform.position = MouseUtil.GetMouseWorldPosition(-1);
    }

    private void OnMouseUp()
    {
        if(!Interactions.Instance.PlayerCanInteract()) return;
        if(Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, 10f, dropLayer))
        {
            // play card
            PlayCardGA playCardGA = new PlayCardGA(Card);
            ActionSystem.Instance.Perform(playCardGA);
        }
        else
        {
            var transform1 = transform;
            transform1.position = dragBeginPos;
            transform1.rotation = dragBeginRot;
        }
        Interactions.Instance.PlayerIsDragging = false;
    }
}
