using UnityEngine;
using Ebac.Core.Singleton;
using Cinemachine;
using NaughtyAttributes;

public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineVirtualCamera virtualCamera;
    public float shakeTime;

    private CinemachineBasicMultiChannelPerlin cinemaPerlin;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        cinemaPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    [Button]
    public void Shake()
    {
        CameraShake(1, 1, .3f);
    }

    public void CameraShake(float amplitude, float frequency, float time)
    {        
        cinemaPerlin.m_AmplitudeGain = amplitude;
        cinemaPerlin.m_FrequencyGain = frequency;

        shakeTime = time;
    }

    private void Update()
    {
        if(shakeTime > 0f)
        {
            shakeTime -= Time.deltaTime;
        } else
        {
            cinemaPerlin.m_AmplitudeGain = 0f;
            cinemaPerlin.m_FrequencyGain = 0f;
        }
    }
}
