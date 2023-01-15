using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusUI : MonoBehaviour
{
    [SerializeField] private GameObject _victoryText;
    [SerializeField] private GameObject _loseText;

    public void ShowLose()
    {
        _loseText.SetActive(true);
    }
    
    public void ShowVictory()
    {
        _victoryText.SetActive(true);
    }
    
}
