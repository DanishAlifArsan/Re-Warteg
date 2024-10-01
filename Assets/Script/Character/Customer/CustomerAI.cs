using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CustomerAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform cashierPoint;
    public Transform homePoint;
    public float waitDuration;
    private float waitTimer;
    public bool isWalking;
    public bool isBuying;
    public bool isPaying;
    public float health;
    public int maxNumberOfGoods;
    public int buyAmountPerGoods;
    public Sprite battleSprite;
    public bool isEvil;
    private StateManager stateManager;
    public Animator anim;
    [SerializeField] private DialogueBubble dialogueBubble;
    [SerializeField] private RectTransform boxHolder;
    [SerializeField] private Image patienceBar;
    public CharacterSpeak speak;
    private List<DialogueBubble> dialogueBubbles = new List<DialogueBubble>();
    public Dictionary<Goods, int> goodsToBuy = new Dictionary<Goods, int>();
    public GameObject dialogueBubbleUI;
    private bool setupFlag;

    private void OnEnable() {
        waitTimer = waitDuration;
        isWalking = false;
        isBuying = false;
        isPaying = false;
        setupFlag = true;
        CustomerManager.instance.isSpawned = true;
    }

    private void OnDisable() {
        stateManager = null;
        CustomerManager.instance.isSpawned = false;
    }

    private void Update() {
        if (setupFlag)
        {
            if (ItemManager.instance.listGoodsOnSale.Count > 0)
            {
                Setup();
            } else {
                return;
            }
        }

        stateManager.currentState.UpdateState(this, stateManager);

        if (isWalking)
        {
            return;
        }

        waitTimer -= Time.deltaTime;
        patienceBar.fillAmount = waitTimer/waitDuration;
        anim.SetFloat("patience", patienceBar.fillAmount);
        if (waitTimer <= 0)
        {
            patienceBar.fillAmount = 1;
            waitTimer = waitDuration;
            isWalking = true;
        }
    }

    private void Setup() {
        stateManager = new StateManager();
        stateManager.StartState(this);
        setupFlag = false;
    }

    public void SetGoodsToBuy() {
        goodsToBuy = CustomerManager.instance.SetGoodsToBuy(maxNumberOfGoods, buyAmountPerGoods);

        int numberOfGoods = goodsToBuy.Count;
        for (int i = 0; i < numberOfGoods; i++)
        {
            dialogueBubbles.Add(Instantiate(dialogueBubble, boxHolder));
            dialogueBubbles[i].Setup(goodsToBuy.ElementAt(i));
        }
        boxHolder.anchoredPosition = new Vector3(98, (114 * (numberOfGoods - 1)) -14, 0);
    }

    public void ClearGoodsToBuy() {
        foreach (var item in dialogueBubbles)
        {
            Destroy(item.gameObject);
        }
        dialogueBubbles.Clear();
    }

    public int CountTotalPrice() {
        int totalPrice = 0;
        for (int i = 0; i < goodsToBuy.Count; i++)
        {
            int price = goodsToBuy.ElementAt(i).Key.setPrice;
            int amount = goodsToBuy[goodsToBuy.ElementAt(i).Key];
            totalPrice += price * amount;
        }
    
        return totalPrice;
    }
    
    public override void OnInteract(ItemInteract broadcaster)
    {
        if (stateManager.currentState == stateManager.buy)
        {            
            //logic pembelian
            Item item = broadcaster.itemInHand?.GetComponent<Item>();
            Broom broom = broadcaster.itemInHand?.GetComponent<Broom>();
            if (item!= null && SaleManager.instance.IsGridNull())
            {
                speak.Happy();
                broadcaster.itemInHand = null;
                SaleManager.instance.PlaceItem(goodsToBuy, item);
                patienceBar.fillAmount = 1;
                waitTimer = waitDuration;
                if (SaleManager.instance.CompareItem())
                {
                    isPaying = true;
                }
            } else if (broom != null)
            {
                Battle();
                broom.animator.SetTrigger("swing");
                BattleManager.instance.battledCustomer = this;
                BattleManager.instance.StartBattle(false);
            }
        }

        ToggleHighlight(broadcaster.centerIndicator, false, "");
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        Interactable item = broadcaster.itemInHand;

        if (item != null)
        {
            if (stateManager.currentState == stateManager.buy && item.itemType.Equals(ItemType.Goods))
            {
                ToggleHighlight(broadcaster.centerIndicator, status, "Interact Place");
            } else if(stateManager.currentState == stateManager.buy && item.itemType.Equals(ItemType.Broom)) {
                ToggleHighlight(broadcaster.centerIndicator, status, "Interact Fight");
            }
        }
    }

    public void Battle() {
        stateManager.SwitchAnyState(this, stateManager.attack, () => true);
    }

    public IState CurrentState() {
        return stateManager.currentState;
    }
}
