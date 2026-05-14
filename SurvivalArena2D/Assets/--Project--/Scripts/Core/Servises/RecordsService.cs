using UnityEngine;

public class RecordsService
{
    private const string BestTimeKey = "BestTime";

    public void TrySaveRecord(float newTime)
    {
        float currentTime = PlayerPrefs.GetFloat(BestTimeKey, 0);

        if (newTime > currentTime)
        {
            PlayerPrefs.SetFloat(BestTimeKey, newTime);
            PlayerPrefs.Save();
            Debug.Log("New record");
        }
    }

    public float GetRecord() => PlayerPrefs.GetFloat(BestTimeKey, 0);
}