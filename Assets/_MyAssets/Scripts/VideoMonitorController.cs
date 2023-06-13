using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoMonitorController : MonoBehaviour
{
    [Header("List of video clips and descriptions")]
    [SerializeField]
    private List<VideoClip> clipList = new List<VideoClip>();
    [SerializeField]
    private List<string> clipDescriptionList = new List<string>();

    [Header("Reference to description text")]
    [SerializeField]
    private TextMeshProUGUI descriptionText;

    private VideoPlayer player;

    private void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    public void PlayVideo(int index)
    {
        player.clip = clipList[index];
        descriptionText.text = clipDescriptionList[index].Replace("\\n", "\n");
        player.Play();
    }
}
