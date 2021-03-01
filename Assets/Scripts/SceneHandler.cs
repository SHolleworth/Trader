using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] Computer computer = null;

    public void LoadScene(string sceneName)
    {
        PlayerPrefs.SetFloat(StatHandler.moneyName, computer.GetMoney());
        PlayerPrefs.SetFloat(StatHandler.fuelName, computer.GetFuel());
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneNumber)
    {
        PlayerPrefs.SetFloat(StatHandler.moneyName, computer.GetMoney());
        PlayerPrefs.SetFloat(StatHandler.fuelName, computer.GetFuel());
        SceneManager.LoadScene(sceneNumber);
    }
}
