using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using VStateMachines;

public enum LookDirection
{
    RIGHT,
    LEFT,
    UP,
    DOWN
}

public abstract class VAnimController<T> : VStateMachine<T>
{
    RenderTexture renderTexture;
    GameObject quad;
    Material mat;

    public int videoWidth;
    public int videoHeight;

    public VideoPlayer videoPlayer { get; protected set; }
    public LookDirection lookDirection { get; set; }

    void FixedUpdate()
    {
        if (currentState != null)
            currentState.UpdateState();
    }

    protected void Initialize(bool sendFrameReadyEvents = true)
    {
        lookDirection = LookDirection.RIGHT;
        videoPlayer = gameObject.GetComponentInChildren<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        quad = GameObject.Find(gameObject.name + "/Quad");
        
        renderTexture = new RenderTexture(videoWidth, videoHeight, 16, RenderTextureFormat.Default);
        renderTexture.Create();
        videoPlayer.targetTexture = renderTexture;
        videoPlayer.sendFrameReadyEvents = sendFrameReadyEvents;

        videoPlayer.frameReady += FrameUpdated;
        videoPlayer.loopPointReached += LoopReached;

        mat = new Material(Shader.Find("Standard"));

        mat.SetFloat("_Mode", 2f);
        mat.SetOverrideTag("RenderType", "Transparent");
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");
        mat.SetFloat("_SpecularHighlights", 0f);
        mat.SetFloat("_Metallic", 0.5f);
        mat.renderQueue = 3000;

        mat.mainTexture = renderTexture;

        quad.GetComponent<MeshRenderer>().material = mat;
        quad.GetComponent<Renderer>().material = mat;
        videoPlayer.playOnAwake = true;
    }

    public void SetLookDirection(float lookX, float lookZ)
    {
        if (Mathf.Abs(lookX) >= Mathf.Abs(lookZ))
        {
            if (lookX > 0)
                lookDirection = LookDirection.RIGHT;
            else
                lookDirection = LookDirection.LEFT;
        }
        else
        {
            if (lookZ > 0)
                lookDirection = LookDirection.UP;
            else
                lookDirection = LookDirection.DOWN;
        }
    }

    protected void FrameUpdated(VideoPlayer source, long frameIdx)
    {

        currentState.FrameUpdated(source, frameIdx);
    }

    protected void LoopReached(VideoPlayer source)
    {
        currentState.LoopReached(source);
    }
}
