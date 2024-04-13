using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DesaturateFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class DesaturatePassSettings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
        public Material effectMaterial;
    }

    class DesaturatePass : ScriptableRenderPass
    {
        private Material mat;
        private RenderTargetIdentifier source;
        private RenderTargetHandle tempTexture;

        public DesaturatePass(Material material) : base()
        {
            mat = material;
            tempTexture.Init("_TempDesaturateTexture");
        }
        public void SetSource(RenderTargetIdentifier cameraColorTarget)
        {
            source = cameraColorTarget;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get("DesaturateFeature");

            RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
            descriptor.depthBufferBits = 0;
            cmd.GetTemporaryRT(tempTexture.id, descriptor, FilterMode.Bilinear);

            Blit(cmd, source, tempTexture.Identifier(), mat, 0);
            Blit(cmd, tempTexture.Identifier(), source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tempTexture.id);
        }

    }

    [SerializeField] private DesaturatePassSettings settings;
    private DesaturatePass saturatePass;

    public override void Create()
    {
        saturatePass = new DesaturatePass(settings.effectMaterial);

        saturatePass.renderPassEvent = settings.renderPassEvent;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
//#if UNITY_EDITOR
//        if (renderingData.cameraData.isSceneViewCamera)
//            return;
//#endif
        saturatePass.SetSource(renderer.cameraColorTarget);
        renderer.EnqueuePass(saturatePass);
    }
}
