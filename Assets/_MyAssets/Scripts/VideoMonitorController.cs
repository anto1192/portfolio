using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoMonitorController : MonoBehaviour
{
    [SerializeField]
    private List<VideoClip> clipList = new List<VideoClip>();

    private VideoPlayer player;

    private void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    public void PlayVideo(int index)
    {
        player.clip = clipList[index];
        player.Play();
    }
}
