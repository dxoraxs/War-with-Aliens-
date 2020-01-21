using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text numberWaveText;
    [SerializeField] private TMPro.TMP_Text healthCastleText;
    [SerializeField] private TMPro.TMP_Text coinPlayerText;
    [SerializeField] private GameObject restartPanel;

    public void OnClickButtonRestart() => SceneManager.LoadScene("SampleScene");
    public void ShowRestartPanel() => restartPanel.SetActive(true); 

    public void SetTextNumberWave(int numberWave, int maxNumberWave)
    {
        numberWaveText.text = "Волна: " + numberWave + "/"+maxNumberWave;
    }
    
    public void SetTextNumberWaveTimer(string text)
    {
        numberWaveText.text = text;
    }

    public void SetTextHealth(int health)
    {
        healthCastleText.text = "Здоровье: " + health;
    }
    
    public void SetTextCoin(int coin)
    {
        coinPlayerText.text = "Монеты: " + coin;
    }
}
