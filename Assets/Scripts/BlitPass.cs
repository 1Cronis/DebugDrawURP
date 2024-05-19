using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


// Копирует заданный цветовой буфер в целевой цветовой буфер.
//
// Вы можете использовать этот проход для копирования цветового буфера в целевой,
// чтобы использовать его позже при рендеринге. Например, вы можете скопировать
// непрозрачную текстуру для использования ее в эффектах искажения.

// "Проход" (pass) - это то, что фактически выполняет работу по рендерингу, в то время как "фича" (feature) - это то, как это взаимодействует со всем остальным в scriptable pipeline.
public class BlitPass : ScriptableRenderPass
{

    // Используется для метки этого прохода в инструменте Unity Frame Debug
    string m_ProfilerTag;

    public Material blitMaterial = null;
    public int blitShaderPassIndex = 0;


    private RenderTargetIdentifier source { get; set; }
    private RenderTargetHandle destination { get; set; }
    RenderTargetHandle m_TemporaryColorTexture;
    //public FilterMode filterMode { get; set; }
    public FilterMode filterMode = FilterMode.Trilinear;

    /// Создает проход CopyColorPass

    public BlitPass(RenderPassEvent renderPassEvent, Material blitMaterial, int blitShaderPassIndex, string tag)
    {
        this.renderPassEvent = renderPassEvent;
        this.blitMaterial = blitMaterial;
        this.blitShaderPassIndex = blitShaderPassIndex;
        m_ProfilerTag = tag;
        m_TemporaryColorTexture.Init("_TemporaryColorTexture");
    }

    // Это не является частью класса ScriptableRenderPass и является нашим собственным дополнением.
    // Для этого пользовательского прохода нам нужна цветовая цель камеры, поэтому она передается сюда.
    /// <summary>
    /// Настроить проход с источником и целью для выполнения.
    /// </summary>
    /// <param name="source">Исходная цветовая цель</param>
    /// <param name="destination">Целевая цветовая цель</param>
    public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)
    {
        this.source = source;
        this.destination = destination;
    }

    // вызывается каждый кадр перед Execute, используйте его для настройки вещей, которые проходу понадобятся
    public override void Configure(UnityEngine.Rendering.CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
    {
        // Нельзя читать и записывать в одну и ту же целевую цветовую цель, создайте временную целевую цветовую цель для копирования.
        if (destination == RenderTargetHandle.CameraTarget)
        {
            // Создать временный render texture, соответствующий камере
            //cmd.GetTemporaryRT(m_TemporaryColorTexture.id, cameraTextureDescriptor, filterMode);
            cmd.GetTemporaryRT(m_TemporaryColorTexture.id, cameraTextureDescriptor);
        }
    }

    // Execute вызывается для каждой подходящей камеры каждый кадр. В это время
    // сам рендеринг еще не происходит, поэтому здесь необходимо устанавливать только инструкции.
    // RenderingData предоставляет информацию о сцене и рендеринге.
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {

        // получаем буфер команд для использования
        UnityEngine.Rendering.CommandBuffer cmd = CommandBufferPool.Get(m_ProfilerTag);
        cmd.Clear();

        RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
        opaqueDesc.depthBufferBits = 0;

        // Нельзя читать и записывать в одну и ту же целевую цветовую цель, создайте временную целевую цветовую цель для копирования.
        if (destination == RenderTargetHandle.CameraTarget)
        {
            // Создать временный render texture, соответствующий камере
            //cmd.GetTemporaryRT(m_TemporaryColorTexture.id, opaqueDesc, filterMode);
            // фактическое содержимое нашего пользовательского прохода рендеринга
            // мы применяем наш материал при копировании во временную текстуру
            Blit(cmd, source, m_TemporaryColorTexture.Identifier(), blitMaterial, blitShaderPassIndex);
            // ...затем копируем обратно
            Blit(cmd, m_TemporaryColorTexture.Identifier(), source);
            //cmd.Blit(source, m_TemporaryColorTexture.Identifier(), blitMaterial, blitShaderPassIndex);
        }
        else
        {
            Blit(cmd, source, destination.Identifier(), blitMaterial, blitShaderPassIndex);
        }

        // не забывайте сообщить ScriptableRenderContext фактически выполнить команды
        context.ExecuteCommandBuffer(cmd);

        // убираем за собой
        CommandBufferPool.Release(cmd);
    }

    // вызывается после Execute, используйте его для очистки всего, что было выделено в Configure и Execute
    public override void FrameCleanup(UnityEngine.Rendering.CommandBuffer cmd)
    {
        if (destination == RenderTargetHandle.CameraTarget)
            cmd.ReleaseTemporaryRT(m_TemporaryColorTexture.id);
    }
}

