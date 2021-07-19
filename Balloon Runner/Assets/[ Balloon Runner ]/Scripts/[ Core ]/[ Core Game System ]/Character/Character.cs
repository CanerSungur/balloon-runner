using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    private Rigidbody rb;
    public Rigidbody Rigidbody { get { return (rb == null) ? rb = GetComponent<Rigidbody>() : rb; } }

    private Collider coll;
    public Collider Collider { get { return (coll == null) ? coll = GetComponent<Collider>() : coll; } }

    #region Events

    [HideInInspector] public UnityEvent OnCharacterHit = new UnityEvent();
    [HideInInspector] public UnityEvent OnCharacterHeal = new UnityEvent();
    [HideInInspector] public UnityEvent OnCharacterDie = new UnityEvent();
    [HideInInspector] public UnityEvent OnCharacterRevive = new UnityEvent();
    [HideInInspector] public UnityEvent OnCharacterJump = new UnityEvent();
    [HideInInspector] public UnityEvent OnCharacterSlide = new UnityEvent();
    [HideInInspector] public UnityEvent OnBubbleGumGetBigger = new UnityEvent();
    [HideInInspector] public UnityEvent OnBubbleGumGetSmaller = new UnityEvent();
    [HideInInspector] public UnityEvent OnBubbleGumPop = new UnityEvent();

    #endregion

    private bool isDead;
    public bool IsDead { get { return (isDead); } set { isDead = value; } }

    private bool isControlable;
    public bool IsControlable { get { return !GameManager.GameIsOver && GameManager.GameIsStarted && !IsDead ? true : false ; } set { isControlable = value; } }

    private int size;
    public int Size
    {
        get
        {
            if (size <= 0)
                return 0;
            else if (size >= 10)
                return 10;
            else
                return size;
        }
        set
        {
            size = value;
        }
    }

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnLevelContine.AddListener(ReviveCharacter);
        EventManager.OnLevelStart.AddListener(ReviveCharacter);
        EventManager.OnLevelFinish.AddListener(() =>
        {
            //transform.position = new Vector3(TrackManager.Instance.MiddleLane.transform.position.x, transform.position.y, transform.position.z);
            //transform.LookAt(TrackManager.Instance.MiddleLane.transform);
        });

        EventManager.OnGameStart.AddListener(() => Rigidbody.useGravity = true);

        OnBubbleGumGetBigger.AddListener(GetBigger);
        OnBubbleGumGetSmaller.AddListener(GetSmaller);
        OnBubbleGumPop.AddListener(Pop);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnLevelContine.RemoveListener(ReviveCharacter);
        EventManager.OnLevelStart.RemoveListener(ReviveCharacter);
        EventManager.OnLevelFinish.RemoveListener(() =>
        {
            //transform.position = new Vector3(TrackManager.Instance.MiddleLane.transform.position.x, transform.position.y, transform.position.z);
            //transform.LookAt(TrackManager.Instance.MiddleLane.transform);
        });

        EventManager.OnGameStart.RemoveListener(() => Rigidbody.useGravity = true);

        OnBubbleGumGetBigger.RemoveListener(GetBigger);
        OnBubbleGumGetSmaller.RemoveListener(GetSmaller);
        OnBubbleGumPop.RemoveListener(Pop);
    }

    private void Awake()
    {
        IsDead = false;
        IsControlable = true;

        Size = 1;
    }

    public void KillCharacter()
    {
        if (IsDead)
            return;

        IsDead = true;
        IsControlable = false;
        OnCharacterDie.Invoke();

        EventManager.OnLevelFail.Invoke();
    }

    public void ReviveCharacter()
    {
        if (!IsDead)
            return;

        IsDead = false;
        IsControlable = false;
        OnCharacterRevive.Invoke();
    }

    private void GetBigger() => Size++;
    private void GetSmaller()
    {
        if (Size == 1)
            OnBubbleGumPop.Invoke();
        else
            Size--;
    }

    private void Pop() => EventManager.OnGameEnd.Invoke();

    #region If there are collectables or Obstacles

    private void OnTriggerEnter(Collider other)
    {
        CollectableBase collectable = other.GetComponent<CollectableBase>();

        if (collectable != null)
            collectable.Collect();

        //IObstacle obstacle = other.GetComponent<IObstacle>();
        //if (obstacle != null)
        //    obstacle.Hit();
    }

    #endregion
}
