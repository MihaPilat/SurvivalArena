using System;

public interface ISceneLoader
{
    void LoadScene(int sceneIndex, Action onComplete = null);
    void LoadScene(string sceneName, Action onComplete = null);
}
