using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum State 
    {
        Pace,
        Follow,
    }

    private State currentState = State.Pace;

    [SerializeField]
    GameObject[] route;

    int routeIndex = 0;
    GameObject target;
    public float speed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState) 
        {
            case State.Pace:
                Pacing();
                break;
            case State.Follow:
                Following();
                break;
        }
    }

    void Pacing() 
    {
        //What do we do when we're pacing?
        target = route[routeIndex];
        MoveTo(target);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.001f) 
        {
            routeIndex += 1;
            if (routeIndex >= route.Length)
            {
                routeIndex = 0;
            }
        }

        //On what condition do we switch states?
        GameObject obstacle = CheckForward();
        if (obstacle != null) 
        {
            target = obstacle;
            currentState = State.Follow;
        }
    }

    void Following() 
    {
        MoveTo(target);

        //On what condition do we switch states?
        GameObject obstacle = CheckForward();
        if (obstacle == null) 
        {
            currentState = State.Pace;
        }
    }

    void MoveTo(GameObject t) 
    {
        transform.LookAt(t.transform, Vector3.up);
        transform.position = Vector3.MoveTowards(transform.position, t.transform.position, speed * Time.deltaTime);
    }

    GameObject CheckForward() 
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
        if(Physics.Raycast(transform.position, transform.forward, out hit, 10)) 
        {
            PlayerStateManager player = hit.transform.gameObject.GetComponent<PlayerStateManager>();
            if (player != null) 
            {
                //if (player.currentState != player.sneakState) 
                //{
                    print(hit.transform.gameObject.name);
                    return hit.transform.gameObject;
                //}
            }
        }
        return null;
    }
}