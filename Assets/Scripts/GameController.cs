using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public GameObject playerPrefab;
    public string spawnTag;
	//
	public float playerVelocity;
	public Vector2 playerPosition;
	public float boundary;
	public bool messagePopup;




    void Start()
    {
		playerPosition=gameObject.transform.position;
        DontDestroyOnLoad(gameObject);
    }

	void Update()
	{
		playerPosition.x += Input.GetAxis("Horizontal")*playerVelocity;
		playerPosition.y += Input.GetAxis("Vertical")*playerVelocity;
		 
		transform.position = playerPosition;

		if (playerPosition.x <- boundary)
		{
			transform.position = new Vector2(-boundary,playerPosition.y);
		}
		if (playerPosition.x > boundary)
		{
			transform.position = new Vector2(boundary,playerPosition.y);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		messagePopup = true;
		if (col.gameObject.tag=="Pickup")
		{

			Destroy (col.gameObject);
		}

	}

	void OnGUI()
	{
		if (messagePopup = true)
		{
			GUI.Label(new Rect(20,20,100,50),"Press A to pick up!");
		}
		messagePopup = false;
	}

    void OnLevelWasLoaded(int level)
    {
        if(level != 0)
        {
            GameObject spawn = GameObject.FindGameObjectWithTag(spawnTag);
            GameObject.Instantiate(playerPrefab, spawn.transform.position, Quaternion.identity);
        }
    }
    
    public void LoadLevel(int i)
    {
        Application.LoadLevel(i);
    }
}
