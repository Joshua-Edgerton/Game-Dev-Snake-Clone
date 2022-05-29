using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [Header("Player Movement and Aim")]
    private Vector2 _direction = Vector2.right;
    public Camera mainCamera;
    public Vector3 currentMousePosition;
    public GameObject aim;
    public GameObject crosshair;
    public GameObject crosshairWhenChoosingSpawn;
    [Space(1)]
    [Header("Script Links")]
    public Abilities abilitiesScript;
    public StatsManager statsManagerScript;
    [Space(1)]
    [Header("Game Objects and Transforms")]
    public Transform segmentPrefab;
    [Space(1)]
    [Header("Snake Stats")]
    public int initialSize = 4;
    public string segmentDisplay;
    public int segmentTotal;
    [Space(1)]
    [Header("Spawn Variables")]
    public bool choseSpawnLocation = true;
    public bool invulnerableSpawn = false;
    [Space(1)]
    [Header("Lists")]
    private List<Transform> _segments = new List<Transform>();

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
        //Makes the "aim" game object look towards the mouse position if the game is not paused
        if (statsManagerScript.gamePaused == false)
        {
            aim.transform.right = mouseWorldPosition - transform.position;
            currentMousePosition = mouseWorldPosition;
        }

        Debug.Log("spawn: " + invulnerableSpawn);

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
        if (Input.GetMouseButtonDown(0) && segmentTotal > 1 && statsManagerScript.gamePaused == false)
        {
            abilitiesScript.PlayAbility();
        }
        if (Input.GetMouseButtonDown(0) && choseSpawnLocation == false && this.gameObject.transform.localScale.x != 1 && statsManagerScript.gamePaused == false)
        {
            ChooseSpawn();
        }
    }

    private void FixedUpdate()
    {
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
        statsManagerScript.LostLife();
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
    public void DieThenChooseSpawn()
    {
        choseSpawnLocation = false;
        statsManagerScript.pauseTimer = true;
        //crosshair.SetActive(false);
        statsManagerScript.LostLife();
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
        invulnerableSpawn = true;
        crosshair.gameObject.SetActive(false);
        crosshairWhenChoosingSpawn.SetActive(true);
    }
    public void ChooseSpawn()
    {
        choseSpawnLocation = true;
        invulnerableSpawn = true;
        statsManagerScript.pauseTimer = false;
        Debug.Log("Choose spawn - Called");

        this.transform.position = currentMousePosition;
        _segments.Add(this.transform);
        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        Uninvulnerable();
        crosshair.gameObject.SetActive(true);
        crosshairWhenChoosingSpawn.SetActive(false);
    }

    private void Grow(int superAmount)
    {
        statsManagerScript.SegmentScore(superAmount);
        for (int i = superAmount; i > 0; i--)
        {
            Transform segment = Instantiate(this.segmentPrefab);
            segment.position = _segments[_segments.Count - 1].position;
            _segments.Add(segment);
        }
    }
    private void Uninvulnerable()
    {
        invulnerableSpawn = false;
    }
    private void Invulnerable()
    {
        invulnerableSpawn = true;
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
        else if (other.tag == "Obstacle" || other.tag == "Enemy" || other.tag == "Enemy Projectile")
        {
            DieThenChooseSpawn();
            Debug.Log("Snake hit obstacle: " + other.tag);

        }
        else if (other.tag == "SnakeBody" && invulnerableSpawn == false)
        {
            DieThenChooseSpawn();
            Debug.Log("Snake hit obstacle: " + other.tag);
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
