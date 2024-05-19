using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


// �������� �������� �������� ����� � ������� �������� �����.
//
// �� ������ ������������ ���� ������ ��� ����������� ��������� ������ � �������,
// ����� ������������ ��� ����� ��� ����������. ��������, �� ������ �����������
// ������������ �������� ��� ������������� �� � �������� ���������.

// "������" (pass) - ��� ��, ��� ���������� ��������� ������ �� ����������, � �� ����� ��� "����" (feature) - ��� ��, ��� ��� ��������������� �� ���� ��������� � scriptable pipeline.
public class BlitPass : ScriptableRenderPass
{

    // ������������ ��� ����� ����� ������� � ����������� Unity Frame Debug
    string m_ProfilerTag;

    public Material blitMaterial = null;
    public int blitShaderPassIndex = 0;


    private RenderTargetIdentifier source { get; set; }
    private RenderTargetHandle destination { get; set; }
    RenderTargetHandle m_TemporaryColorTexture;
    //public FilterMode filterMode { get; set; }
    public FilterMode filterMode = FilterMode.Trilinear;

    /// ������� ������ CopyColorPass

    public BlitPass(RenderPassEvent renderPassEvent, Material blitMaterial, int blitShaderPassIndex, string tag)
    {
        this.renderPassEvent = renderPassEvent;
        this.blitMaterial = blitMaterial;
        this.blitShaderPassIndex = blitShaderPassIndex;
        m_ProfilerTag = tag;
        m_TemporaryColorTexture.Init("_TemporaryColorTexture");
    }

    // ��� �� �������� ������ ������ ScriptableRenderPass � �������� ����� ����������� �����������.
    // ��� ����� ����������������� ������� ��� ����� �������� ���� ������, ������� ��� ���������� ����.
    /// <summary>
    /// ��������� ������ � ���������� � ����� ��� ����������.
    /// </summary>
    /// <param name="source">�������� �������� ����</param>
    /// <param name="destination">������� �������� ����</param>
    public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)
    {
        this.source = source;
        this.destination = destination;
    }

    // ���������� ������ ���� ����� Execute, ����������� ��� ��� ��������� �����, ������� ������� �����������
    public override void Configure(UnityEngine.Rendering.CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
    {
        // ������ ������ � ���������� � ���� � �� �� ������� �������� ����, �������� ��������� ������� �������� ���� ��� �����������.
        if (destination == RenderTargetHandle.CameraTarget)
        {
            // ������� ��������� render texture, ��������������� ������
            //cmd.GetTemporaryRT(m_TemporaryColorTexture.id, cameraTextureDescriptor, filterMode);
            cmd.GetTemporaryRT(m_TemporaryColorTexture.id, cameraTextureDescriptor);
        }
    }

    // Execute ���������� ��� ������ ���������� ������ ������ ����. � ��� �����
    // ��� ��������� ��� �� ����������, ������� ����� ���������� ������������� ������ ����������.
    // RenderingData ������������� ���������� � ����� � ����������.
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {

        // �������� ����� ������ ��� �������������
        UnityEngine.Rendering.CommandBuffer cmd = CommandBufferPool.Get(m_ProfilerTag);
        cmd.Clear();

        RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
        opaqueDesc.depthBufferBits = 0;

        // ������ ������ � ���������� � ���� � �� �� ������� �������� ����, �������� ��������� ������� �������� ���� ��� �����������.
        if (destination == RenderTargetHandle.CameraTarget)
        {
            // ������� ��������� render texture, ��������������� ������
            //cmd.GetTemporaryRT(m_TemporaryColorTexture.id, opaqueDesc, filterMode);
            // ����������� ���������� ������ ����������������� ������� ����������
            // �� ��������� ��� �������� ��� ����������� �� ��������� ��������
            Blit(cmd, source, m_TemporaryColorTexture.Identifier(), blitMaterial, blitShaderPassIndex);
            // ...����� �������� �������
            Blit(cmd, m_TemporaryColorTexture.Identifier(), source);
            //cmd.Blit(source, m_TemporaryColorTexture.Identifier(), blitMaterial, blitShaderPassIndex);
        }
        else
        {
            Blit(cmd, source, destination.Identifier(), blitMaterial, blitShaderPassIndex);
        }

        // �� ��������� �������� ScriptableRenderContext ���������� ��������� �������
        context.ExecuteCommandBuffer(cmd);

        // ������� �� �����
        CommandBufferPool.Release(cmd);
    }

    // ���������� ����� Execute, ����������� ��� ��� ������� �����, ��� ���� �������� � Configure � Execute
    public override void FrameCleanup(UnityEngine.Rendering.CommandBuffer cmd)
    {
        if (destination == RenderTargetHandle.CameraTarget)
            cmd.ReleaseTemporaryRT(m_TemporaryColorTexture.id);
    }
}

