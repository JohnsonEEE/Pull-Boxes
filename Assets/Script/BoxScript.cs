using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public PlayerScript playerScript;
    
    public int forbidLeftCount;
    
    public int forbidRightCount;
    
    public int forbidUpCount;
    
    public int forbidDownCount;

    private Color originColor;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WallLeft"))
        {
            forbidRightCount++;
        }
        if (other.gameObject.CompareTag("WallRight"))
        {
            forbidLeftCount++;
        }
        if (other.gameObject.CompareTag("WallUp"))
        {
            forbidDownCount++;
        }
        if (other.gameObject.CompareTag("WallDown"))
        {
            forbidUpCount++;
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            Renderer theRenderer = GetComponent<Renderer>();
            var material = theRenderer.material;
            originColor = material.color;
            material.color = Color.red;
            playerScript.UpdateRemainGoalCount(-1);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WallLeft"))
        {
            forbidRightCount--;
        }
        if (other.gameObject.CompareTag("WallRight"))
        {
            forbidLeftCount--;
        }
        if (other.gameObject.CompareTag("WallUp"))
        {
            forbidDownCount--;
        }
        if (other.gameObject.CompareTag("WallDown"))
        {
            forbidUpCount--;
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            Renderer theRenderer = GetComponent<Renderer>();
            theRenderer.material.color = originColor;
            playerScript.UpdateRemainGoalCount(1);
        }
    }
}
