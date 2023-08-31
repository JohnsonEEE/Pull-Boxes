using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject successCanvas;
    
    private Vector3 destination;
    
    private Vector3 boxDestination;

    private GameObject toLeftBoxBuffer;
    
    private GameObject toRightBoxBuffer;
    
    private GameObject toUpBoxBuffer;
    
    private GameObject toDownBoxBuffer;
    
    private GameObject toLeftBox;
    
    private GameObject toRightBox;
    
    private GameObject toUpBox;
    
    private GameObject toDownBox;
    
    private float xSpeed;
    
    private float zSpeed;
    
    private int forbidLeftCount;
    
    private int forbidRightCount;
    
    private int forbidUpCount;
    
    private int forbidDownCount;

    private const float movingSpeed = 3F;

    private int remainGoalCount;

    private bool isKeyBoardFrozen;
    
    void Start()
    {
        successCanvas.SetActive(false);
        remainGoalCount = GameObject.FindGameObjectsWithTag("Box").Length;
        var position = transform.position;
        destination = position;
        boxDestination = position;
    }

    void Update()
    {
        listenKeyBoard();
        
        stopMoving();

        // 移动player和box
        if (xSpeed != 0 || zSpeed != 0)
        {
            transform.Translate(new Vector3(xSpeed * Time.deltaTime, 0, zSpeed * Time.deltaTime));
            if (toLeftBox != null && xSpeed < 0)
            {
                toLeftBox.transform.Translate(new Vector3(xSpeed * Time.deltaTime, 0, zSpeed * Time.deltaTime));
            }
            if (toRightBox != null && xSpeed > 0)
            {
                toRightBox.transform.Translate(new Vector3(xSpeed * Time.deltaTime, 0, zSpeed * Time.deltaTime));
            }
            if (toUpBox != null && zSpeed > 0)
            {
                toUpBox.transform.Translate(new Vector3(xSpeed * Time.deltaTime, 0, zSpeed * Time.deltaTime));
            }
            if (toDownBox != null && zSpeed < 0)
            {
                toDownBox.transform.Translate(new Vector3(xSpeed * Time.deltaTime, 0, zSpeed * Time.deltaTime));
            }
        }
    }

    public void UpdateRemainGoalCount(int count)
    {
        remainGoalCount += count;
        if (remainGoalCount == 0)
        {
            successCanvas.SetActive(true);
            isKeyBoardFrozen = true;
        }
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
        if (other.gameObject.CompareTag("BoxLeft"))
        {
            toRightBoxBuffer = other.transform.parent.gameObject;
        }
        if (other.gameObject.CompareTag("BoxRight"))
        {
            toLeftBoxBuffer = other.transform.parent.gameObject;
        }
        if (other.gameObject.CompareTag("BoxUp"))
        {
            toDownBoxBuffer = other.transform.parent.gameObject;
        }
        if (other.gameObject.CompareTag("BoxDown"))
        {
            toUpBoxBuffer = other.transform.parent.gameObject;
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
        if (other.gameObject.CompareTag("BoxLeft"))
        {
            toRightBox = null;
        }
        if (other.gameObject.CompareTag("BoxRight"))
        {
            toLeftBox = null;
        }
        if (other.gameObject.CompareTag("BoxUp"))
        {
            toDownBox = null;
        }
        if (other.gameObject.CompareTag("BoxDown"))
        {
            toUpBox = null;
        }
    }

    private void listenKeyBoard()
    {
        if (xSpeed == 0 && zSpeed == 0 && !isKeyBoardFrozen)
        {
            if (Input.GetKeyDown(KeyCode.A) && forbidLeftCount == 0)
            {
                bool isBoxForbidLeft = false;
                if (toLeftBox != null)
                {
                    Transform child = toLeftBox.transform.Find("SelfArea");
                    if (child.CompareTag("BoxSelf"))
                    {
                        isBoxForbidLeft = child.GetComponent<BoxScript>().forbidLeftCount != 0;
                    }
                }
                if (!isBoxForbidLeft)
                {
                    xSpeed = -movingSpeed;
                    destination.x = transform.position.x - 1;
                    boxDestination = destination;
                    boxDestination.x = destination.x - 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.D) && forbidRightCount == 0)
            {
                bool isBoxForbidRight = false;
                if (toRightBox != null)
                {
                    Transform child = toRightBox.transform.Find("SelfArea");
                    if (child.CompareTag("BoxSelf"))
                    {
                        isBoxForbidRight = child.GetComponent<BoxScript>().forbidRightCount != 0;
                    }
                }
                if (!isBoxForbidRight)
                {
                    xSpeed = movingSpeed;
                    destination.x = transform.position.x + 1;
                    boxDestination = destination;
                    boxDestination.x = destination.x + 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.W) && forbidUpCount == 0)
            {
                bool isBoxForbidUp = false;
                if (toUpBox != null)
                {
                    Transform child = toUpBox.transform.Find("SelfArea");
                    if (child.CompareTag("BoxSelf"))
                    {
                        isBoxForbidUp = child.GetComponent<BoxScript>().forbidUpCount != 0;
                    }
                }
                if (!isBoxForbidUp)
                {
                    zSpeed = movingSpeed;
                    destination.z = transform.position.z + 1;
                    boxDestination = destination;
                    boxDestination.z = destination.z + 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.S) && forbidDownCount == 0)
            {
                bool isBoxForbidDown = false;
                if (toDownBox != null)
                {
                    Transform child = toDownBox.transform.Find("SelfArea");
                    if (child.CompareTag("BoxSelf"))
                    {
                        isBoxForbidDown = child.GetComponent<BoxScript>().forbidDownCount != 0;
                    }
                }
                if (!isBoxForbidDown)
                {
                    zSpeed = -movingSpeed;
                    destination.z = transform.position.z - 1;
                    boxDestination = destination;
                    boxDestination.z = destination.z - 1;
                }
            }
        }
    }

    private void stopMoving()
    {
        if (xSpeed > 0 && transform.position.x >= destination.x || xSpeed < 0 && transform.position.x <= destination.x)
        {
            xSpeed = 0;
            transform.position = destination;
            if (toLeftBox != null)
            {
                toLeftBox.transform.position = boxDestination;
            }
            if (toRightBox != null)
            {
                toRightBox.transform.position = boxDestination;
            }
            if (toUpBox != null)
            {
                toUpBox.transform.position = boxDestination;
            }
            if (toDownBox != null)
            {
                toDownBox.transform.position = boxDestination;
            }
            if (toLeftBoxBuffer != null)
            {
                toLeftBox = toLeftBoxBuffer;
                toLeftBoxBuffer = null;
            }
            if (toRightBoxBuffer != null)
            {
                toRightBox = toRightBoxBuffer;
                toRightBoxBuffer = null;
            }
            if (toUpBoxBuffer != null)
            {
                toUpBox = toUpBoxBuffer;
                toUpBoxBuffer = null;
            }
            if (toDownBoxBuffer != null)
            {
                toDownBox = toDownBoxBuffer;
                toDownBoxBuffer = null;
            }
        }
        
        if (zSpeed > 0 && transform.position.z >= destination.z || zSpeed < 0 && transform.position.z <= destination.z)
        {
            zSpeed = 0;
            transform.position = destination;
            if (toLeftBox != null)
            {
                toLeftBox.transform.position = boxDestination;
            }
            if (toRightBox != null)
            {
                toRightBox.transform.position = boxDestination;
            }
            if (toUpBox != null)
            {
                toUpBox.transform.position = boxDestination;
            }
            if (toDownBox != null)
            {
                toDownBox.transform.position = boxDestination;
            }
            if (toLeftBoxBuffer != null)
            {
                toLeftBox = toLeftBoxBuffer;
                toLeftBoxBuffer = null;
            }
            if (toRightBoxBuffer != null)
            {
                toRightBox = toRightBoxBuffer;
                toRightBoxBuffer = null;
            }
            if (toUpBoxBuffer != null)
            {
                toUpBox = toUpBoxBuffer;
                toUpBoxBuffer = null;
            }
            if (toDownBoxBuffer != null)
            {
                toDownBox = toDownBoxBuffer;
                toDownBoxBuffer = null;
            }
        }
    }
}
