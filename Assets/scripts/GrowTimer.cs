using UnityEngine;

public class GrowTimer : MonoBehaviour
{
    //from https://discussions.unity.com/t/how-do-i-synchronise-a-function-called-in-multiple-objects/220269/2
    public UnityEngine.Events.UnityEvent OnGrow;

    [SerializeField]
    private float interval;
    private float timer;
    public float Interval
    {
        get { return interval ;}
        set { interval = Mathf.Max( value, 0 ); }
    }

    private void Start()
    {
        timer = Interval;
    }

    private void Update()
    {
        timer -= Time.deltaTime ;
        if( timer < 0 )
        {
            timer += interval ;
            if( OnGrow != null ){
                OnGrow.Invoke();
            }
        }
    }
}
