using UnityEngine;
using Zenject;

public class LevelInitializer : IInitializable
{
    private readonly AudioService _audioService;
    private readonly SoundData _musicData;

    public LevelInitializer(
        AudioService audioService,
        [Inject(Id = "LevelMusic")] SoundData musicData)
    {
        _audioService = audioService;
        _musicData = musicData;
    }

    public void Initialize()
    {
        _audioService.PlayMusic(_musicData);
    }
}
