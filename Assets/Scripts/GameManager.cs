using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject FirstKey;
    [SerializeField] private GameObject SecondKey;
    [SerializeField] private GameObject ThirdKey;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LoseScreen;
    [SerializeField] private Text TimeCount;
    private float startTime;
    private int roundTimeDuration = 20;
    private bool isEndGame = false;
   
    private int[] keysPosition = {0,3,5 };
    private int transformMoveStep = 25;
    private int transformStartPosition = 100;
    [SerializeField] private int[] firstButtChangePos = { 1, -1,0 };
    [SerializeField] private int[] secondButtChangePos = { -1, 2, -1 };
    [SerializeField] private int[] thirdButtChangePos = { -1, 1, 1 };
    [SerializeField] private int upperBorder = 9;
    [SerializeField] private int bottomBorder = 0;
    const int pinCount = 3;

    private void Start()
    {
        UpdateKeysPosition();
        startTime = Time.time;
    }

    private void Update()
    {
        if (isEndGame) return;
        CheckTime();
    }

    public void ReloadLevel1()
    {
        isEndGame = false;
        startTime = Time.time;
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        UpdateKeysPosition();
    }
    public void HandleNewKeysPosition(int buttNumber)
    {
        switch (buttNumber)
        {
            case 1:
                {
                    SetNewKeysPosition(firstButtChangePos);
                    break;
                }
            case 2:
                {
                    SetNewKeysPosition(secondButtChangePos);
                    break;
                }
            case 3:
                {
                    SetNewKeysPosition(thirdButtChangePos);
                    break;
                }
            default:
                {
                    Debug.Log(message: "Unhandled button number");
                    break;
                }

        }
    }
    private void CheckTime()
    {
        if (startTime + roundTimeDuration < Time.time)
        {
            isEndGame = true;
            LoseScreen.SetActive(true);
        }

        TimeCount.text = ((int)(roundTimeDuration - (Time.time - startTime))).ToString();
        Debug.Log(TimeCount);
    }

    private void UpdateKeysPosition()
    {
        CheckKeysPosition();
        SetTransformY(FirstKey, yPosition:transformStartPosition - transformMoveStep * keysPosition[0]);
        SetTransformY(SecondKey, yPosition: transformStartPosition - transformMoveStep * keysPosition[1]);
        SetTransformY(ThirdKey, yPosition: transformStartPosition - transformMoveStep * keysPosition[2]);
    }

    private void CheckKeysPosition()
    {
        const int open = 5;
        for(int i = 0; i< pinCount; i++)
        {
            if (keysPosition[i] != open)
            {
                return;
            }
        }
        isEndGame = true;
        WinScreen.SetActive(true);
    }


    void SetTransformY(GameObject key, int yPosition)
    {
        var keyPosition = key.transform.localPosition;
        key.transform.localPosition = new Vector3(keyPosition.x, yPosition);
    }

    private void SetNewKeysPosition(int[] newPositions)
    {
        for (int i = 0; i< pinCount; i++)
        {
            keysPosition[i] += newPositions[i];
            if (keysPosition[i] < bottomBorder) keysPosition[i] = 0;
            if (keysPosition[i] > upperBorder) keysPosition[i] = 9;
        }
        UpdateKeysPosition();

    }
      
  
}
