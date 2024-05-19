using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace CustomDebugDraw
{
    public class CommandBufferPass : ScriptableRenderPass
    {
        public CommandBuffer UpdateCommandBuffer;
        public CommandBuffer FixedUpdateCommandBuffer;

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            context.ExecuteCommandBuffer(UpdateCommandBuffer);
            context.ExecuteCommandBuffer(FixedUpdateCommandBuffer);
        }
    }
}

