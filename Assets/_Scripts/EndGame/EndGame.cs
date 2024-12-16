using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGame : MonoBehaviour
{

    private bool _endGame = false;
    public List<GameObject> endGameObjects;
    public int currentLevel = 100;

    void OnTriggerEnter(Collider other)
    {
        PlayerController_Astronaut p = other.GetComponent<PlayerController_Astronaut>();

        if (!_endGame && p != null)
        {
            LaunchEndGame();
        }
    }

    private void LaunchEndGame()
    {
        _endGame = true;

        foreach (var obj in endGameObjects)
        {
            obj.SetActive(true);
            obj.transform.DOScale(0, .25f).SetEase(Ease.OutBack).From();
        }

        SaveManager.instance.SaveLastLevel(currentLevel);
    }
}
