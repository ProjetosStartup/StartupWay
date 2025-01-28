using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;

public class MovementController : MonoBehaviour
{
    public static MovementController Instance;

    [SerializeField]
    private GameObject alert;
    [SerializeField]
    private Button diceButton;
    [SerializeField]
    private List<Transform> nodesPositions;
    private List<Tile> tiles = new();
    [SerializeField]
    private int actualNode = 0;
    private Transform player;
    [SerializeField]
    private float velocity = 0.4f;
    [SerializeField]
    private float jumpHeight = 1f;
    private int nodesToJump = 0;

    [SerializeField]
    private List<int> nodesRandonEvent ;

   [SerializeField]
    private List<int>  nodesRandonEventCopy= new List<int>();


    private int totalJumps;

    public int TotalJumps { get => totalJumps; }

    public int NodesToJump => nodesToJump;
    public Tile ActualTile => tiles[actualNode];

    private Transform shadow;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        foreach(Transform pos in nodesPositions)
        {
            // provisório, apenas para testes
            // após receber o mapa definir as regioes das tiles
            tiles.Add(new Tile(pos));
        }

        
       
    }
    private void Start()
    {
        player = PlayerController.Instance.Player.transform;
        shadow = PlayerController.Instance.Shadow.transform;
        player.transform.position = nodesPositions[0].position;
        shadow.position = player.position - new Vector3(0,0.65f);

        GetNodeValue();

    }
    private void GetNodeValue()
    {
        for (int i = 0; i < nodesRandonEvent.Count; i++)
        {
            nodesRandonEventCopy.Add(nodesRandonEvent[i]);
        }
    }
    public void MovePlayer()
    {
        Sequence playerMoveSequence = DOTween.Sequence();
        Sequence shadowMoveSequence = DOTween.Sequence();
        for (int i = 1; i <= NodesToJump; i++)
        {
            int targetNode = (actualNode + i) % nodesPositions.Count;

            playerMoveSequence.Append(
                player.DOLocalJump(nodesPositions[targetNode].position, jumpHeight, 1, velocity).SetEase(Ease.InOutCubic).OnComplete(() => { AudioController.Instance.Play("Jump"); }));
               

            shadowMoveSequence.Append(shadow.DOLocalMove(nodesPositions[targetNode].position - new Vector3(0, 0.65f), velocity).SetEase(Ease.InOutCubic));
            shadowMoveSequence.Join(shadow.DOScale(new Vector3(0.5f, 0.25f), velocity*0.5f).SetEase(Ease.InOutCubic).SetLoops(2,LoopType.Yoyo));

           
        }

        playerMoveSequence.OnStart(() => 
        {
            player.GetComponent<Animator>().SetTrigger("Walking"); 
        }).OnComplete(() => 
        { 
            if(RandonEventVerification())
            {
                GameController.Instance.UpdateGameState(GameController.GameState.RandomEvent);
               
            }
            else
            {
                GameController.Instance.UpdateGameState(GameController.GameState.Payment);
            }
            PlayerController.Instance.Player.GetComponent<Animator>().SetTrigger("Stopped");
            GameCanvasController.Instance.ResetDiceAnimation();


        });

        actualNode = (actualNode + NodesToJump) % nodesPositions.Count;

        if(StartupController.Instance.Startup.Team.Employees.Count<=2 && totalJumps>60)
        {
            StartupController.Instance.Startup.Wallet.Balance -= 10 * totalJumps;
            FeedbackController.Instance.Faturamento -= 10 * totalJumps;

            alert.SetActive(true);  
        }
      
    }

    public void CalculateNodesToJump()
    {
        nodesToJump = Random.Range(1, 7);
        totalJumps += nodesToJump;


        
    }
    private bool RandonEventVerification()
    {
        foreach(int node  in nodesRandonEventCopy )
        {
            //if(totalJumps >= node)
            //{
            //    nodesRandonEventCopy.Remove(node);
            //    if (nodesRandonEventCopy.Count == 0)
            //    {
                   
            //        if (totalJumps - nodesPositions.Count > 0)
            //        {
            //            totalJumps -= nodesPositions.Count;
                        
            //        }
            //        GetNodeValue();

            //    }
            //    return true;
            //}

            if(actualNode== node)
                return true;    
        }
       

        return false;
    }

    public void SetCanMove(bool value)
    {
        diceButton.interactable = value;
    }
    
    [System.Serializable]
    public class Tile
    {
        public Transform position;
        public Util.Region region;

        public Tile(Transform position,Util.Region region = Util.Region.CG)
        {
            this.position = position;
            this.region = region;
        }
    }

  
}
