using UnityEngine;
using Zenject;

public class IndicatorManager : MonoBehaviour
{
    [SerializeField] private GameObject _indicatorPrefab;
    [SerializeField] private Transform _container;

    private IInstantiator _instantiator;

    [Inject]
    private void Construct(IInstantiator instantiator) => _instantiator = instantiator;

    public void CreateIndicator(Transform target, Sprite icon)
    {
        var obj = _instantiator.InstantiatePrefab(_indicatorPrefab, _container);
        obj.GetComponent<IndicatorUI>().Setup(target, icon);
    }
}
