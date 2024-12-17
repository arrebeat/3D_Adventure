using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class EndGame : MonoBehaviour
{
    private bool _endGame = false;
    public List<GameObject> endGameObjects;
    public int currentLevel = 100;
    public UnityEvent OnEndGame;
    
    void OnTriggerEnter(Collider other)
    {
        PlayerController_Astronaut p = other.GetComponent<PlayerController_Astronaut>();

        if (!_endGame && p != null)
        {
            LaunchEndGame(p);
        }
    }

    private void LaunchEndGame(PlayerController_Astronaut p)
    {
        _endGame = true;

        foreach (var obj in endGameObjects)
        {
            obj.SetActive(true);
            obj.transform.DOScale(0, .25f).SetEase(Ease.OutBack).From();
        }

        SaveManager.instance.SaveLastLevel(currentLevel);
        SaveManager.instance.SaveCurrentHp(p.healthBase.CurrentHp());

        OnEndGame?.Invoke();
        
    }
}
