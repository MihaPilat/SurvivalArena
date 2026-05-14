using System;

public interface ILevelable
{
    event Action<int, int> OnExpChanged;
    event Action<int> OnLevelUp;

    int CurrentLevel { get; }
    void AddExperience(int amount);

}