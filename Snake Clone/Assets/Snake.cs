using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Snake : MonoBehaviour
{
    //Snake Movement
    private Vector2 _direction = Vector2.right;
    //Script connection
    public Abilities abilitiesScript;
    //List creation
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    //Initial snake segments at start
    public int initialSize = 4;
    //Segment counters
    public string segmentDisplay;
    public int segmentTotal;
    //Mouse position tools and "aim" transform for projectiles
    public Camera mainCamera;
    public GameObject aim;
    public StatsManager statsManagerScript;

    private void Start()
    {
        ResetState();
        // Makes cursor invisible
        Cursor.visible = false;
        // Sets UI to show segment count
        segmentDisplay = (initialSize - 1).ToString();
        statsManagerScript.segmentCounter.text = segmentDisplay;
        //Finds the script "Abilities" attached to the gameobject called "Snake"
        abilitiesScript = GameObject.Find("Snake").GetComponent<Abilities>();
        statsManagerScript = GameObject.Find("Level Manager").GetComponent<StatsManager>();
    }

    private void Update()
    {
        //Segment info
        segmentDisplay = (_segments.Count - 1).ToString();
        statsManagerScript.segmentCounter.text = segmentDisplay;
        segmentTotal = (_segments.Count - 1);
        //Mouse position info
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        //Makes the "aim" game object look towards the mouse position
        aim.transform.right = mouseWorldPosition - transform.position;

        if (Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down)
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up)
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right)
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left)
        {
            _direction = Vector2.right;
        }
        if (Input.GetMouseButtonDown(0) && segmentTotal > 1)
        {
            abilitiesScript.PlayAbility();
            //Debug.Log("Clicked");
        }

    }

    private void FixedUpdate()
    {
        Debug.Log(segmentTotal);
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        //Also snake movement, rounded so that it moves 1 block at a time, thanks to fixedupdate also
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
            );
    }

    public void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }
        this.transform.position = Vector3.zero;
    }

    private void Grow(int superAmount)
    {
        statsManagerScript.SegmentExp(superAmount);
        for (int i = superAmount; i > 0; i--)
        {
            Transform segment = Instantiate(this.segmentPrefab);
            segment.position = _segments[_segments.Count - 1].position;
            _segments.Add(segment);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow(1);
        }
        else if (other.tag == "SuperFood")
        {
            Grow(3);
        }
        else if (other.tag == "Obstacle" || other.tag == "Enemy")
        {
            ResetState();
        }
    }
    public void DestroySegments(int segDestroyAmount)
    {
        for (int i = segDestroyAmount; i > 0; i--)
        {
            Destroy(_segments[_segments.Count - 1].gameObject);
            _segments.RemoveAt(_segments.Count - 1);
        }
    }

}
