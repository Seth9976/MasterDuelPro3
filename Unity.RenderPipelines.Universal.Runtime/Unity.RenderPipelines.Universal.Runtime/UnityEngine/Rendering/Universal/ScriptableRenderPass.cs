using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200009E RID: 158
	public abstract class ScriptableRenderPass : IRenderGraphRecorder
	{
		// Token: 0x06000377 RID: 887 RVA: 0x0000D451 File Offset: 0x0000B651
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void FrameCleanup(CommandBuffer cmd)
		{
			this.OnCameraCleanup(cmd);
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000D45A File Offset: 0x0000B65A
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000D462 File Offset: 0x0000B662
		public RenderPassEvent renderPassEvent { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000D46B File Offset: 0x0000B66B
		[Obsolete("Use colorAttachmentHandles", true)]
		public RenderTargetIdentifier[] colorAttachments
		{
			get
			{
				throw new NotSupportedException("colorAttachments has been deprecated. Use colorAttachmentHandles instead.");
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000D477 File Offset: 0x0000B677
		[Obsolete("Use colorAttachmentHandle", true)]
		public RenderTargetIdentifier[] colorAttachment
		{
			get
			{
				throw new NotSupportedException("colorAttachment has been deprecated. Use colorAttachmentHandle instead.");
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000D484 File Offset: 0x0000B684
		[Obsolete("Use depthAttachmentHandle", true)]
		public RenderTargetIdentifier depthAttachment
		{
			get
			{
				throw new NotSupportedException("depthAttachment has been deprecated. Use depthAttachmentHandle instead.");
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000D49B File Offset: 0x0000B69B
		public RTHandle[] colorAttachmentHandles
		{
			get
			{
				return this.m_ColorAttachments;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000D4A3 File Offset: 0x0000B6A3
		public RTHandle colorAttachmentHandle
		{
			get
			{
				return this.m_ColorAttachments[0];
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000D4AD File Offset: 0x0000B6AD
		public RTHandle depthAttachmentHandle
		{
			get
			{
				return this.m_DepthAttachment;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000D4B5 File Offset: 0x0000B6B5
		public RenderBufferStoreAction[] colorStoreActions
		{
			get
			{
				return this.m_ColorStoreActions;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000D4BD File Offset: 0x0000B6BD
		public RenderBufferStoreAction depthStoreAction
		{
			get
			{
				return this.m_DepthStoreAction;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000D4C5 File Offset: 0x0000B6C5
		internal bool[] overriddenColorStoreActions
		{
			get
			{
				return this.m_OverriddenColorStoreActions;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000D4CD File Offset: 0x0000B6CD
		internal bool overriddenDepthStoreAction
		{
			get
			{
				return this.m_OverriddenDepthStoreAction;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000D4D5 File Offset: 0x0000B6D5
		public ScriptableRenderPassInput input
		{
			get
			{
				return this.m_Input;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000D4DD File Offset: 0x0000B6DD
		public ClearFlag clearFlag
		{
			get
			{
				return this.m_ClearFlag;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000D4E5 File Offset: 0x0000B6E5
		public Color clearColor
		{
			get
			{
				return this.m_ClearColor;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000D4ED File Offset: 0x0000B6ED
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000D4F5 File Offset: 0x0000B6F5
		public bool requiresIntermediateTexture { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000D4FE File Offset: 0x0000B6FE
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000D528 File Offset: 0x0000B728
		protected internal ProfilingSampler profilingSampler
		{
			get
			{
				if (this.m_RenderGraphSettings == null)
				{
					this.m_RenderGraphSettings = GraphicsSettings.GetRenderPipelineSettings<RenderGraphSettings>();
				}
				if (!this.m_RenderGraphSettings.enableRenderCompatibilityMode)
				{
					return null;
				}
				return this.m_ProfingSampler;
			}
			set
			{
				this.m_ProfingSampler = value;
				this.m_PassName = ((value != null) ? value.name : base.GetType().Name);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000D54D File Offset: 0x0000B74D
		protected internal string passName
		{
			get
			{
				return this.m_PassName;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000D555 File Offset: 0x0000B755
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0000D55D File Offset: 0x0000B75D
		internal bool overrideCameraTarget { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000D566 File Offset: 0x0000B766
		// (set) Token: 0x0600038F RID: 911 RVA: 0x0000D56E File Offset: 0x0000B76E
		internal bool isBlitRenderPass { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000D577 File Offset: 0x0000B777
		// (set) Token: 0x06000391 RID: 913 RVA: 0x0000D57F File Offset: 0x0000B77F
		internal bool useNativeRenderPass { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000D588 File Offset: 0x0000B788
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0000D590 File Offset: 0x0000B790
		internal bool breakGBufferAndDeferredRenderPass { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000D599 File Offset: 0x0000B799
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000D5A1 File Offset: 0x0000B7A1
		internal int renderPassQueueIndex { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000D5AA File Offset: 0x0000B7AA
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000D5B2 File Offset: 0x0000B7B2
		internal GraphicsFormat[] renderTargetFormat { get; set; }

		// Token: 0x06000398 RID: 920 RVA: 0x0000D5BC File Offset: 0x0000B7BC
		internal static DebugHandler GetActiveDebugHandler(UniversalCameraData cameraData)
		{
			DebugHandler debugHandler = cameraData.renderer.DebugHandler;
			if (debugHandler != null && debugHandler.IsActiveForCamera(cameraData.isPreviewCamera))
			{
				return debugHandler;
			}
			return null;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000D5EC File Offset: 0x0000B7EC
		public ScriptableRenderPass()
		{
			this.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
			RTHandle[] array = new RTHandle[8];
			array[0] = ScriptableRenderPass.k_CameraTarget;
			this.m_ColorAttachments = array;
			this.m_DepthAttachment = ScriptableRenderPass.k_CameraTarget;
			this.m_InputAttachments = new RTHandle[8];
			this.m_InputAttachmentIsTransient = new bool[8];
			this.m_ColorStoreActions = new RenderBufferStoreAction[8];
			this.m_DepthStoreAction = RenderBufferStoreAction.Store;
			this.m_OverriddenColorStoreActions = new bool[8];
			this.m_OverriddenDepthStoreAction = false;
			this.m_ClearFlag = ClearFlag.None;
			this.m_ClearColor = Color.black;
			this.overrideCameraTarget = false;
			this.isBlitRenderPass = false;
			this.useNativeRenderPass = true;
			this.breakGBufferAndDeferredRenderPass = true;
			this.renderPassQueueIndex = -1;
			this.renderTargetFormat = new GraphicsFormat[8];
			this.profilingSampler = new ProfilingSampler(base.GetType().Name);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000D6F9 File Offset: 0x0000B8F9
		public void ConfigureInput(ScriptableRenderPassInput passInput)
		{
			this.m_Input = passInput;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000D702 File Offset: 0x0000B902
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public void ConfigureColorStoreAction(RenderBufferStoreAction storeAction, uint attachmentIndex = 0U)
		{
			this.m_ColorStoreActions[(int)attachmentIndex] = storeAction;
			this.m_OverriddenColorStoreActions[(int)attachmentIndex] = true;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000D718 File Offset: 0x0000B918
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public void ConfigureColorStoreActions(RenderBufferStoreAction[] storeActions)
		{
			int count = Math.Min(storeActions.Length, this.m_ColorStoreActions.Length);
			uint i = 0U;
			while ((ulong)i < (ulong)((long)count))
			{
				this.m_ColorStoreActions[(int)i] = storeActions[(int)i];
				this.m_OverriddenColorStoreActions[(int)i] = true;
				i += 1U;
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000D758 File Offset: 0x0000B958
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public void ConfigureDepthStoreAction(RenderBufferStoreAction storeAction)
		{
			this.m_DepthStoreAction = storeAction;
			this.m_OverriddenDepthStoreAction = true;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000D768 File Offset: 0x0000B968
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal void ConfigureInputAttachments(RTHandle input, bool isTransient = false)
		{
			this.m_InputAttachments[0] = input;
			this.m_InputAttachmentIsTransient[0] = isTransient;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000D77C File Offset: 0x0000B97C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal void ConfigureInputAttachments(RTHandle[] inputs)
		{
			this.m_InputAttachments = inputs;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000D785 File Offset: 0x0000B985
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal void ConfigureInputAttachments(RTHandle[] inputs, bool[] isTransient)
		{
			this.ConfigureInputAttachments(inputs);
			this.m_InputAttachmentIsTransient = isTransient;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000D795 File Offset: 0x0000B995
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal void SetInputAttachmentTransient(int idx, bool isTransient)
		{
			this.m_InputAttachmentIsTransient[idx] = isTransient;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal bool IsInputAttachmentTransient(int idx)
		{
			return this.m_InputAttachmentIsTransient[idx];
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public void ResetTarget()
		{
			this.overrideCameraTarget = false;
			this.m_DepthAttachment = null;
			this.m_ColorAttachments[0] = null;
			for (int i = 1; i < this.m_ColorAttachments.Length; i++)
			{
				this.m_ColorAttachments[i] = null;
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		[Obsolete("Use RTHandles for colorAttachment and depthAttachment", true)]
		public void ConfigureTarget(RenderTargetIdentifier colorAttachment, RenderTargetIdentifier depthAttachment)
		{
			throw new NotSupportedException("ConfigureTarget with RenderTargetIdentifier has been deprecated. Use RTHandles instead");
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public void ConfigureTarget(RTHandle colorAttachment, RTHandle depthAttachment)
		{
			this.overrideCameraTarget = true;
			this.m_DepthAttachment = depthAttachment;
			this.m_ColorAttachments[0] = colorAttachment;
			for (int i = 1; i < this.m_ColorAttachments.Length; i++)
			{
				this.m_ColorAttachments[i] = null;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000D838 File Offset: 0x0000BA38
		[Obsolete("Use RTHandles for colorAttachments and depthAttachment", true)]
		public void ConfigureTarget(RenderTargetIdentifier[] colorAttachments, RenderTargetIdentifier depthAttachment)
		{
			throw new NotSupportedException("ConfigureTarget with RenderTargetIdentifier has been deprecated. Use it with RTHandles instead");
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000D844 File Offset: 0x0000BA44
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public void ConfigureTarget(RTHandle[] colorAttachments, RTHandle depthAttachment)
		{
			this.overrideCameraTarget = true;
			uint nonNullColorBuffers = RenderingUtils.GetValidColorBufferCount(colorAttachments);
			if ((ulong)nonNullColorBuffers > (ulong)((long)SystemInfo.supportedRenderTargetCount))
			{
				Debug.LogError("Trying to set " + nonNullColorBuffers.ToString() + " renderTargets, which is more than the maximum supported:" + SystemInfo.supportedRenderTargetCount.ToString());
			}
			if (colorAttachments.Length > this.m_ColorAttachments.Length)
			{
				Debug.LogError("Trying to set " + colorAttachments.Length.ToString() + " color attachments, which is more than the maximum supported:" + this.m_ColorAttachments.Length.ToString());
			}
			for (int i = 0; i < colorAttachments.Length; i++)
			{
				this.m_ColorAttachments[i] = colorAttachments[i];
			}
			for (int j = colorAttachments.Length; j < this.m_ColorAttachments.Length; j++)
			{
				this.m_ColorAttachments[j] = null;
			}
			this.m_DepthAttachment = depthAttachment;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000D90C File Offset: 0x0000BB0C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal void ConfigureTarget(RTHandle[] colorAttachments, RTHandle depthAttachment, GraphicsFormat[] formats)
		{
			this.ConfigureTarget(colorAttachments, depthAttachment);
			for (int i = 0; i < formats.Length; i++)
			{
				this.renderTargetFormat[i] = formats[i];
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000D838 File Offset: 0x0000BA38
		[Obsolete("Use RTHandle for colorAttachment", true)]
		public void ConfigureTarget(RenderTargetIdentifier colorAttachment)
		{
			throw new NotSupportedException("ConfigureTarget with RenderTargetIdentifier has been deprecated. Use it with RTHandles instead");
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000D93A File Offset: 0x0000BB3A
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public void ConfigureTarget(RTHandle colorAttachment)
		{
			this.ConfigureTarget(colorAttachment, ScriptableRenderPass.k_CameraTarget);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000D838 File Offset: 0x0000BA38
		[Obsolete("Use RTHandles for colorAttachments", true)]
		public void ConfigureTarget(RenderTargetIdentifier[] colorAttachments)
		{
			throw new NotSupportedException("ConfigureTarget with RenderTargetIdentifier has been deprecated. Use it with RTHandles instead");
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000D948 File Offset: 0x0000BB48
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public void ConfigureTarget(RTHandle[] colorAttachments)
		{
			this.ConfigureTarget(colorAttachments, ScriptableRenderPass.k_CameraTarget);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000D956 File Offset: 0x0000BB56
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public void ConfigureClear(ClearFlag clearFlag, Color clearColor)
		{
			this.m_ClearFlag = clearFlag;
			this.m_ClearColor = clearColor;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000217F File Offset: 0x0000037F
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public virtual void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000217F File Offset: 0x0000037F
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public virtual void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000217F File Offset: 0x0000037F
		public virtual void OnCameraCleanup(CommandBuffer cmd)
		{
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000217F File Offset: 0x0000037F
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public virtual void OnFinishCameraStackRendering(CommandBuffer cmd)
		{
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000D966 File Offset: 0x0000BB66
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public virtual void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			Debug.LogWarning("Execute is not implemented, the pass " + this.ToString() + " won't be executed in the current render loop.");
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000D982 File Offset: 0x0000BB82
		public virtual void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
			Debug.LogWarning("The render pass " + this.ToString() + " does not have an implementation of the RecordRenderGraph method. Please implement this method, or consider turning on Compatibility Mode (RenderGraph disabled) in the menu Edit > Project Settings > Graphics > URP. Otherwise the render pass will have no effect. For more information, refer to https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest/index.html?subfolder=/manual/customizing-urp.html.");
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000D99E File Offset: 0x0000BB9E
		[Obsolete("Use RTHandles for source and destination", true)]
		public void Blit(CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material = null, int passIndex = 0)
		{
			throw new NotSupportedException("Blit with RenderTargetIdentifier has been deprecated. Use RTHandles instead");
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000D9AA File Offset: 0x0000BBAA
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public void Blit(CommandBuffer cmd, RTHandle source, RTHandle destination, Material material = null, int passIndex = 0)
		{
			if (material == null)
			{
				Blitter.BlitCameraTexture(cmd, source, destination, 0f, source.rt.filterMode == FilterMode.Bilinear);
				return;
			}
			Blitter.BlitCameraTexture(cmd, source, destination, material, passIndex);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe void Blit(CommandBuffer cmd, ref RenderingData data, Material material, int passIndex = 0)
		{
			ScriptableRenderer renderer = *data.cameraData.renderer;
			this.Blit(cmd, renderer.cameraColorTargetHandle, renderer.GetCameraColorFrontBuffer(cmd), material, passIndex);
			renderer.SwapColorBuffer(cmd);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000DA18 File Offset: 0x0000BC18
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe void Blit(CommandBuffer cmd, ref RenderingData data, RTHandle source, Material material, int passIndex = 0)
		{
			ScriptableRenderer renderer = *data.cameraData.renderer;
			this.Blit(cmd, source, renderer.cameraColorTargetHandle, material, passIndex);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000DA44 File Offset: 0x0000BC44
		public DrawingSettings CreateDrawingSettings(ShaderTagId shaderTagId, ref RenderingData renderingData, SortingCriteria sortingCriteria)
		{
			ContextContainer frameData = renderingData.frameData;
			UniversalRenderingData universalRenderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			return RenderingUtils.CreateDrawingSettings(shaderTagId, universalRenderingData, cameraData, lightData, sortingCriteria);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000DA75 File Offset: 0x0000BC75
		public DrawingSettings CreateDrawingSettings(ShaderTagId shaderTagId, UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData, SortingCriteria sortingCriteria)
		{
			return RenderingUtils.CreateDrawingSettings(shaderTagId, renderingData, cameraData, lightData, sortingCriteria);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000DA84 File Offset: 0x0000BC84
		public DrawingSettings CreateDrawingSettings(List<ShaderTagId> shaderTagIdList, ref RenderingData renderingData, SortingCriteria sortingCriteria)
		{
			ContextContainer frameData = renderingData.frameData;
			UniversalRenderingData universalRenderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			return RenderingUtils.CreateDrawingSettings(shaderTagIdList, universalRenderingData, cameraData, lightData, sortingCriteria);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000DAB5 File Offset: 0x0000BCB5
		public DrawingSettings CreateDrawingSettings(List<ShaderTagId> shaderTagIdList, UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData, SortingCriteria sortingCriteria)
		{
			return RenderingUtils.CreateDrawingSettings(shaderTagIdList, renderingData, cameraData, lightData, sortingCriteria);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000DAC3 File Offset: 0x0000BCC3
		public static bool operator <(ScriptableRenderPass lhs, ScriptableRenderPass rhs)
		{
			return lhs.renderPassEvent < rhs.renderPassEvent;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000DAD3 File Offset: 0x0000BCD3
		public static bool operator >(ScriptableRenderPass lhs, ScriptableRenderPass rhs)
		{
			return lhs.renderPassEvent > rhs.renderPassEvent;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		internal static int GetRenderPassEventRange(RenderPassEvent renderPassEvent)
		{
			int numEvents = RenderPassEventsEnumValues.values.Length;
			int currentIndex = 0;
			int i = 0;
			while (i < numEvents && RenderPassEventsEnumValues.values[currentIndex] != (int)renderPassEvent)
			{
				currentIndex++;
				i++;
			}
			if (currentIndex >= numEvents)
			{
				Debug.LogError("GetRenderPassEventRange: invalid renderPassEvent value cannot be found in the RenderPassEvent enumeration");
				return 0;
			}
			if (currentIndex + 1 >= numEvents)
			{
				return 50;
			}
			return RenderPassEventsEnumValues.values[currentIndex + 1] - (int)renderPassEvent;
		}

		// Token: 0x040002F9 RID: 761
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public static RTHandle k_CameraTarget = RTHandles.Alloc(BuiltinRenderTextureType.CameraTarget);

		// Token: 0x040002FB RID: 763
		private RenderBufferStoreAction[] m_ColorStoreActions = new RenderBufferStoreAction[1];

		// Token: 0x040002FC RID: 764
		private RenderBufferStoreAction m_DepthStoreAction;

		// Token: 0x040002FE RID: 766
		private bool[] m_OverriddenColorStoreActions = new bool[1];

		// Token: 0x040002FF RID: 767
		private bool m_OverriddenDepthStoreAction;

		// Token: 0x04000300 RID: 768
		private ProfilingSampler m_ProfingSampler;

		// Token: 0x04000301 RID: 769
		private string m_PassName;

		// Token: 0x04000302 RID: 770
		private RenderGraphSettings m_RenderGraphSettings;

		// Token: 0x04000308 RID: 776
		internal NativeArray<int> m_ColorAttachmentIndices;

		// Token: 0x04000309 RID: 777
		internal NativeArray<int> m_InputAttachmentIndices;

		// Token: 0x0400030B RID: 779
		private RTHandle[] m_ColorAttachments;

		// Token: 0x0400030C RID: 780
		internal RTHandle[] m_InputAttachments = new RTHandle[8];

		// Token: 0x0400030D RID: 781
		internal bool[] m_InputAttachmentIsTransient = new bool[8];

		// Token: 0x0400030E RID: 782
		private RTHandle m_DepthAttachment;

		// Token: 0x0400030F RID: 783
		private ScriptableRenderPassInput m_Input;

		// Token: 0x04000310 RID: 784
		private ClearFlag m_ClearFlag;

		// Token: 0x04000311 RID: 785
		private Color m_ClearColor = Color.black;
	}
}
