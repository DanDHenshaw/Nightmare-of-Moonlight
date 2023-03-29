using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public int floorNumber { get; private set; } = 1;
    private TMP_Text _floorText;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } 
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);

        _floorText = GetComponentInChildren<TMP_Text>();
    }

    public void IncreaseFloorNumber()
    {
        Instance.floorNumber++;
        _floorText.SetText(floorNumber.ToString());
    }
}
