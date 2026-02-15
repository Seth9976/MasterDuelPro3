using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Pool;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C0 RID: 960
	public static class RenderPipelineManager
	{
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x00038A1B File Offset: 0x00036C1B
		// (set) Token: 0x060019F9 RID: 6649 RVA: 0x00038A22 File Offset: 0x00036C22
		public static RenderPipeline currentPipeline
		{
			get
			{
				return RenderPipelineManager.s_CurrentPipeline;
			}
			private set
			{
				RenderPipelineManager.s_CurrentPipelineType = ((value != null) ? value.GetType().ToString() : "Built-in Pipeline");
				RenderPipelineManager.s_CurrentPipeline = value;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060019FA RID: 6650 RVA: 0x00038A48 File Offset: 0x00036C48
		// (remove) Token: 0x060019FB RID: 6651 RVA: 0x00038A7C File Offset: 0x00036C7C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<ScriptableRenderContext, List<Camera>> beginContextRendering;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060019FC RID: 6652 RVA: 0x00038AB0 File Offset: 0x00036CB0
		// (remove) Token: 0x060019FD RID: 6653 RVA: 0x00038AE4 File Offset: 0x00036CE4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<ScriptableRenderContext, List<Camera>> endContextRendering;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060019FE RID: 6654 RVA: 0x00038B18 File Offset: 0x00036D18
		// (remove) Token: 0x060019FF RID: 6655 RVA: 0x00038B4C File Offset: 0x00036D4C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<ScriptableRenderContext, Camera> beginCameraRendering;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06001A00 RID: 6656 RVA: 0x00038B80 File Offset: 0x00036D80
		// (remove) Token: 0x06001A01 RID: 6657 RVA: 0x00038BB4 File Offset: 0x00036DB4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<ScriptableRenderContext, Camera> endCameraRendering;

		// Token: 0x06001A02 RID: 6658 RVA: 0x00038BE7 File Offset: 0x00036DE7
		internal static void BeginContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			Action<ScriptableRenderContext, List<Camera>> action = RenderPipelineManager.beginContextRendering;
			if (action != null)
			{
				action(context, cameras);
			}
			Action<ScriptableRenderContext, Camera[]> action2 = RenderPipelineManager.beginFrameRendering;
			if (action2 != null)
			{
				action2(context, cameras.ToArray());
			}
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00038C15 File Offset: 0x00036E15
		internal static void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			Action<ScriptableRenderContext, Camera> action = RenderPipelineManager.beginCameraRendering;
			if (action != null)
			{
				action(context, camera);
			}
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00038C2B File Offset: 0x00036E2B
		internal static void EndContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			Action<ScriptableRenderContext, Camera[]> action = RenderPipelineManager.endFrameRendering;
			if (action != null)
			{
				action(context, cameras.ToArray());
			}
			Action<ScriptableRenderContext, List<Camera>> action2 = RenderPipelineManager.endContextRendering;
			if (action2 != null)
			{
				action2(context, cameras);
			}
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00038C59 File Offset: 0x00036E59
		internal static void EndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			Action<ScriptableRenderContext, Camera> action = RenderPipelineManager.endCameraRendering;
			if (action != null)
			{
				action(context, camera);
			}
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00038C6F File Offset: 0x00036E6F
		[RequiredByNativeCode]
		internal static void OnActiveRenderPipelineTypeChanged()
		{
			Action action = RenderPipelineManager.activeRenderPipelineTypeChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00038C83 File Offset: 0x00036E83
		[RequiredByNativeCode]
		internal static void OnActiveRenderPipelineAssetChanged(ScriptableObject from, ScriptableObject to)
		{
			Action<RenderPipelineAsset, RenderPipelineAsset> action = RenderPipelineManager.activeRenderPipelineAssetChanged;
			if (action != null)
			{
				action(from as RenderPipelineAsset, to as RenderPipelineAsset);
			}
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00038CA4 File Offset: 0x00036EA4
		[RequiredByNativeCode]
		internal static void HandleRenderPipelineChange(RenderPipelineAsset pipelineAsset)
		{
			bool hasRPAssetChanged = RenderPipelineManager.s_CurrentPipelineAsset != pipelineAsset;
			bool flag = RenderPipelineManager.s_CleanUpPipeline || hasRPAssetChanged;
			if (flag)
			{
				RenderPipelineManager.CleanupRenderPipeline();
				RenderPipelineManager.s_CurrentPipelineAsset = pipelineAsset;
			}
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00038CD8 File Offset: 0x00036ED8
		[RequiredByNativeCode]
		internal static void RecreateCurrentPipeline(RenderPipelineAsset pipelineAsset)
		{
			bool flag = RenderPipelineManager.s_CurrentPipelineAsset == pipelineAsset;
			if (flag)
			{
				RenderPipelineManager.s_CleanUpPipeline = true;
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00038D00 File Offset: 0x00036F00
		[RequiredByNativeCode]
		internal static void CleanupRenderPipeline()
		{
			bool flag = !RenderPipelineManager.isCurrentPipelineValid;
			if (!flag)
			{
				bool flag2 = GraphicsSettings.currentRenderPipeline == null;
				if (flag2)
				{
					Shader.globalRenderPipeline = string.Empty;
				}
				Action action = RenderPipelineManager.activeRenderPipelineDisposed;
				if (action != null)
				{
					action();
				}
				RenderPipelineManager.currentPipeline.Dispose();
				RenderPipelineManager.currentPipeline = null;
				RenderPipelineManager.s_CleanUpPipeline = false;
				RenderPipelineManager.s_CurrentPipelineAsset = null;
				SupportedRenderingFeatures.active = null;
			}
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00038D6C File Offset: 0x00036F6C
		[RequiredByNativeCode]
		private static string GetCurrentPipelineAssetType()
		{
			return RenderPipelineManager.s_CurrentPipelineType;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00038D84 File Offset: 0x00036F84
		[RequiredByNativeCode]
		private static void DoRenderLoop_Internal(RenderPipelineAsset pipelineAsset, IntPtr loopPtr, Object renderRequest)
		{
			bool flag = !RenderPipelineManager.TryPrepareRenderPipeline(pipelineAsset);
			if (!flag)
			{
				ScriptableRenderContext loop = new ScriptableRenderContext(loopPtr);
				List<Camera> cameras;
				using (CollectionPool<List<Camera>, Camera>.Get(out cameras))
				{
					loop.GetCameras(cameras);
					bool flag2 = renderRequest == null;
					if (flag2)
					{
						RenderPipelineManager.currentPipeline.InternalRender(loop, cameras);
					}
					else
					{
						RenderPipelineManager.currentPipeline.InternalProcessRenderRequests<Object>(loop, cameras[0], renderRequest);
					}
				}
			}
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00038E10 File Offset: 0x00037010
		internal static bool TryPrepareRenderPipeline(RenderPipelineAsset pipelineAsset)
		{
			RenderPipelineManager.HandleRenderPipelineChange(pipelineAsset);
			bool flag = !RenderPipelineManager.IsPipelineRequireCreation();
			bool flag2;
			if (flag)
			{
				flag2 = RenderPipelineManager.currentPipeline != null;
			}
			else
			{
				RenderPipelineManager.currentPipeline = RenderPipelineManager.s_CurrentPipelineAsset.InternalCreatePipeline();
				Shader.globalRenderPipeline = RenderPipelineManager.s_CurrentPipelineAsset.renderPipelineShaderTag;
				Action action = RenderPipelineManager.activeRenderPipelineCreated;
				if (action != null)
				{
					action();
				}
				flag2 = RenderPipelineManager.currentPipeline != null;
			}
			return flag2;
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x00038E7C File Offset: 0x0003707C
		private static bool isCurrentPipelineValid
		{
			get
			{
				RenderPipeline currentPipeline = RenderPipelineManager.currentPipeline;
				return currentPipeline != null && !currentPipeline.disposed;
			}
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00038E9E File Offset: 0x0003709E
		private static bool IsPipelineRequireCreation()
		{
			return RenderPipelineManager.s_CurrentPipelineAsset != null && (RenderPipelineManager.currentPipeline == null || RenderPipelineManager.currentPipeline.disposed);
		}

		// Token: 0x04000C41 RID: 3137
		private static bool s_CleanUpPipeline = false;

		// Token: 0x04000C42 RID: 3138
		private static string s_CurrentPipelineType = "Built-in Pipeline";

		// Token: 0x04000C43 RID: 3139
		private static RenderPipelineAsset s_CurrentPipelineAsset;

		// Token: 0x04000C44 RID: 3140
		private static RenderPipeline s_CurrentPipeline = null;

		// Token: 0x04000C49 RID: 3145
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action activeRenderPipelineTypeChanged;

		// Token: 0x04000C4A RID: 3146
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<RenderPipelineAsset, RenderPipelineAsset> activeRenderPipelineAssetChanged;

		// Token: 0x04000C4B RID: 3147
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action activeRenderPipelineCreated;

		// Token: 0x04000C4C RID: 3148
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action activeRenderPipelineDisposed;

		// Token: 0x04000C4D RID: 3149
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<ScriptableRenderContext, Camera[]> beginFrameRendering;

		// Token: 0x04000C4E RID: 3150
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<ScriptableRenderContext, Camera[]> endFrameRendering;
	}
}
