using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoaderService : ISceneLoader
{
    private readonly CanvasGroup _curtainCanvasGroup;
    private readonly ICoroutineRunner _coroutineRunner;
    private bool _isLoading;

    public SceneLoaderService(CanvasGroup curtainPrefab, ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;

        CanvasGroup curtainInstance = UnityEngine.Object.Instantiate(curtainPrefab);
        _curtainCanvasGroup = curtainInstance;

        UnityEngine.Object.DontDestroyOnLoad(_curtainCanvasGroup.gameObject);

        _curtainCanvasGroup.alpha = 0f;
        _curtainCanvasGroup.interactable = false;
        _curtainCanvasGroup.blocksRaycasts = false;
    }

    public void LoadScene(int sceneIndex, Action onComplete = null)
    {
        if (_isLoading) return;
        _coroutineRunner.StartCoroutine(LoadSceneRoutine(sceneIndex, string.Empty, onComplete));
    }

    public void LoadScene(string sceneName, Action onComplete = null)
    {
        if (_isLoading) return;
        _coroutineRunner.StartCoroutine(LoadSceneRoutine(-1, sceneName, onComplete));
    }

    private IEnumerator LoadSceneRoutine(int sceneIndex, string sceneName, Action onComplete)
    {
        _isLoading = true;

        _curtainCanvasGroup.blocksRaycasts = true;
        yield return _curtainCanvasGroup.DOFade(1f, 0.4f).SetEase(Ease.OutCubic).WaitForCompletion();

        AsyncOperation asyncLoad = sceneIndex >= 0
            ? SceneManager.LoadSceneAsync(sceneIndex)
            : SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        onComplete?.Invoke();

        yield return _curtainCanvasGroup.DOFade(0f, 0.4f).SetEase(Ease.InCubic).WaitForCompletion();

        _curtainCanvasGroup.blocksRaycasts = false;
        _isLoading = false;
    }
}
