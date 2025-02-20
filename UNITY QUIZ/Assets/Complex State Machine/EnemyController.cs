using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private enum State
    {
        Pace,
        Follow,
    }

    [SerializeField]
    GameObject[] route;
    GameObject target;

    [SerializeField]
    
    int routeIndex = 0;

    [SerializeField]
    int health = 10;

    [SerializeField]
    float speed = 1.0f;
    private State currentState = State.Pace;
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
                OnPace();
                break;
            case State.Follow:
                OnFollow();
                break;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnPace()
    {
        //What do we do when we're pacing?
        print("I'm pacing!");
        target = route[routeIndex];

        MoveTo(target);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1)
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
            print("switching");
            currentState = State.Follow;
        }
   
    }

    void OnFollow()
    {
        //What do we do when we're following?
        print("I'm following!");
        MoveTo(target);

        //On what condition do we switch states?
        GameObject obstacle = CheckForward();
        
        if (obstacle == null)
        {
            currentState = State.Pace;
        }

    }

      void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<bullet>() != null) 
        {
            health -= 1;
            Destroy(other.gameObject);
        }
    }







    
    void MoveTo(GameObject t)
    {
     transform.position = Vector3.MoveTowards(transform.position, t.transform.position, speed * Time.deltaTime);
     transform.LookAt(t.transform, Vector3.up);
    }

    GameObject CheckForward()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 10, Color.magenta);

        if(Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            PlayerStateManager player = hit.transform.gameObject.GetComponent<PlayerStateManager>();

            if (player!= null)
            {
                if (player.currentState != player.sneakState)
                {
                    print(hit.transform.name);
                    return hit.transform.gameObject;
                }
            }
        }

        return null;
    }
}


