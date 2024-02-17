using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CmbTestComponent : MonoBehaviour
{
    [SerializeField] Pass customPass = new Pass();



    private void Awake()
    {
        //customPass.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques;
        customPass.renderPassEvent = RenderPassEvent.BeforeRenderingTransparents;
        customPass.Init();

        RenderPipelineManager.beginCameraRendering += Draw;
    }

    private void Draw(ScriptableRenderContext arg1, Camera arg2)
    {
        var renderer = Camera.main.GetComponent<UniversalAdditionalCameraData>().scriptableRenderer;
        if (renderer != null)
        {
            renderer.EnqueuePass(customPass);
        }
    }

    private void OnDestroy()
    {
        RenderPipelineManager.beginCameraRendering -= Draw;

        if (customPass != null)
            customPass.Dispose();
    }
}

[System.Serializable]
public class Pass : ScriptableRenderPass, IDisposable
{
    [SerializeField] Mesh drawMesh;
    [SerializeField] Material drawMaterial;
    [Space]
    [SerializeField] Vector3 drawPos;
    [SerializeField] Vector3 drawRotate;
    [SerializeField] Vector3 drawScale;


    private CommandBuffer commandBuffer;

    public void Init()
    {
        commandBuffer = new CommandBuffer();
    }

    private Matrix4x4 GetDrawMatrix()
    {
        return Matrix4x4.TRS(
            drawPos,
            Quaternion.Euler(drawRotate),
            drawScale);
    }


    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        commandBuffer.Clear();
        commandBuffer.DrawMesh(drawMesh,GetDrawMatrix(),drawMaterial);

        context.ExecuteCommandBuffer(commandBuffer);
        context.Submit();
    }

    public void Dispose()
    {
        if (commandBuffer != null)
            commandBuffer.Dispose();
    }
}
