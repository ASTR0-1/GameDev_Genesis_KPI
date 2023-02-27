using System.Collections;
using UnityEngine;
using Cinemachine;
using Player;

public class LevelIntroCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _introCamera;
    [SerializeField] private float _introTime = 7f;
    [SerializeField] private float _returnTime = 7f;
    [SerializeField] private PlayerEntity _player;

    [SerializeField] private bool _controlsEnabled = true;
    private bool _introActive = false;

    void Start()
    {
        StartCoroutine(ActivateIntroCamera(_introTime));
    }

    IEnumerator ActivateIntroCamera(float introDuration)
    {
        _player.IsDisabled = true;

        _introCamera.enabled = true;

        yield return new WaitForSeconds(introDuration + 0.5f);

        _introCamera.enabled = false;

        yield return new WaitForSeconds(_returnTime + 0.5f);

        _player.IsDisabled = false;
    }
}
