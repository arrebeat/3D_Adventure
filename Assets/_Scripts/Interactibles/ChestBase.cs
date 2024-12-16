using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using System;
using Items;

public class ChestBase : MonoBehaviour
{
    public ItemManager itemManager;
    public Animator animator { get; private set; }
    public Collider coll { get; private set; }
    public string triggerOpen = "open";
    public string triggerClose = "close";
    public bool opened = false;
    public float coinBurstDelay = .5f;
    public int coinAmount = 10;
    
    [Header("Icon")]
    public SpriteRenderer icon;
    public float iconHoverHeight = 3f;
    public float iconHoverFrequency = .5f;
    public Ease iconHoverEase;

    private ParticleSystem _coinBurst;
    private Coroutine _coroutine;

    void OnValidate()
    {
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        animator = GetComponentInChildren<Animator>();
        coll = GetComponent<Collider>();
        icon = GetComponentInChildren<SpriteRenderer>();
        _coinBurst = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        if (icon.enabled) icon.enabled = false;    
        icon.transform.DOMoveY(transform.position.y + iconHoverHeight, iconHoverFrequency).SetEase(iconHoverEase).SetLoops(-1, LoopType.Yoyo).From();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        var p = other.GetComponent<PlayerController_Astronaut>();
        if (p != null && !opened)
        {
            icon.enabled = true;
            p.interactableChest = this;
        }
    }

    void OnTriggerExit(Collider other)
    {
        var p = other.GetComponent<PlayerController_Astronaut>();
        if (p != null)
        {
            icon.enabled = false;
            p.interactableChest = null;
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (icon.enabled == true)
        {
            OpenChest();
        }
    }

    [NaughtyAttributes.Button]
    public void OpenChest()
    {
        if (!opened)
        {
            animator.SetTrigger(triggerOpen);

            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(OpenChestCoroutine());
        }
    }
    private IEnumerator OpenChestCoroutine()
    {
        opened = true;
        icon.enabled = false;

        yield return new WaitForSeconds(coinBurstDelay);
        
        _coinBurst.Emit(coinAmount);

        float time = 0;
        for (int i = 0; i < coinAmount; i++)
        {
            time += Time.deltaTime;
            itemManager.AddItemByType(ItemType.COIN);
            yield return new WaitForSeconds(Mathf.Lerp(.025f, .1f, time));
        }

        yield return null;
    }

    [NaughtyAttributes.Button]
    public void CloseChest()
    {
        if (opened)
        {
            animator.SetTrigger(triggerClose);
            opened = false;

            if (_coroutine != null) StopCoroutine(_coroutine);
        } 
            
    }
}
