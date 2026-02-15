using System;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.PlayerLoop
{
	// Token: 0x02000263 RID: 611
	[RequiredByNativeCode]
	[MovedFrom("UnityEngine.Experimental.PlayerLoop")]
	public struct EarlyUpdate
	{
		// Token: 0x02000264 RID: 612
		[RequiredByNativeCode]
		public struct PollPlayerConnection
		{
		}

		// Token: 0x02000265 RID: 613
		[RequiredByNativeCode]
		public struct PollHtcsPlayerConnection
		{
		}

		// Token: 0x02000266 RID: 614
		[RequiredByNativeCode]
		public struct GpuTimestamp
		{
		}

		// Token: 0x02000267 RID: 615
		[RequiredByNativeCode]
		public struct AnalyticsCoreStatsUpdate
		{
		}

		// Token: 0x02000268 RID: 616
		[RequiredByNativeCode]
		public struct UnityWebRequestUpdate
		{
		}

		// Token: 0x02000269 RID: 617
		[RequiredByNativeCode]
		public struct UpdateStreamingManager
		{
		}

		// Token: 0x0200026A RID: 618
		[RequiredByNativeCode]
		public struct ExecuteMainThreadJobs
		{
		}

		// Token: 0x0200026B RID: 619
		[RequiredByNativeCode]
		public struct ProcessMouseInWindow
		{
		}

		// Token: 0x0200026C RID: 620
		[RequiredByNativeCode]
		public struct ClearIntermediateRenderers
		{
		}

		// Token: 0x0200026D RID: 621
		[RequiredByNativeCode]
		public struct ClearLines
		{
		}

		// Token: 0x0200026E RID: 622
		[RequiredByNativeCode]
		public struct PresentBeforeUpdate
		{
		}

		// Token: 0x0200026F RID: 623
		[RequiredByNativeCode]
		public struct ResetFrameStatsAfterPresent
		{
		}

		// Token: 0x02000270 RID: 624
		[RequiredByNativeCode]
		public struct UpdateAsyncReadbackManager
		{
		}

		// Token: 0x02000271 RID: 625
		[RequiredByNativeCode]
		public struct UpdateTextureStreamingManager
		{
		}

		// Token: 0x02000272 RID: 626
		[RequiredByNativeCode]
		public struct UpdatePreloading
		{
		}

		// Token: 0x02000273 RID: 627
		[RequiredByNativeCode]
		public struct UpdateContentLoading
		{
		}

		// Token: 0x02000274 RID: 628
		[RequiredByNativeCode]
		public struct UpdateAsyncInstantiate
		{
		}

		// Token: 0x02000275 RID: 629
		[RequiredByNativeCode]
		public struct RendererNotifyInvisible
		{
		}

		// Token: 0x02000276 RID: 630
		[RequiredByNativeCode]
		public struct PlayerCleanupCachedData
		{
		}

		// Token: 0x02000277 RID: 631
		[RequiredByNativeCode]
		public struct UpdateMainGameViewRect
		{
		}

		// Token: 0x02000278 RID: 632
		[RequiredByNativeCode]
		public struct UpdateCanvasRectTransform
		{
		}

		// Token: 0x02000279 RID: 633
		[RequiredByNativeCode]
		public struct UpdateInputManager
		{
		}

		// Token: 0x0200027A RID: 634
		[RequiredByNativeCode]
		public struct ProcessRemoteInput
		{
		}

		// Token: 0x0200027B RID: 635
		[RequiredByNativeCode]
		public struct XRUpdate
		{
		}

		// Token: 0x0200027C RID: 636
		[RequiredByNativeCode]
		public struct ScriptRunDelayedStartupFrame
		{
		}

		// Token: 0x0200027D RID: 637
		[RequiredByNativeCode]
		public struct UpdateKinect
		{
		}

		// Token: 0x0200027E RID: 638
		[RequiredByNativeCode]
		public struct DeliverIosPlatformEvents
		{
		}

		// Token: 0x0200027F RID: 639
		[RequiredByNativeCode]
		public struct DispatchEventQueueEvents
		{
		}

		// Token: 0x02000280 RID: 640
		[RequiredByNativeCode]
		public struct Physics2DEarlyUpdate
		{
		}

		// Token: 0x02000281 RID: 641
		[RequiredByNativeCode]
		public struct PhysicsResetInterpolatedTransformPosition
		{
		}

		// Token: 0x02000282 RID: 642
		[RequiredByNativeCode]
		public struct SpriteAtlasManagerUpdate
		{
		}

		// Token: 0x02000283 RID: 643
		[Obsolete("TangoUpdate has been deprecated. Use ARCoreUpdate instead (UnityUpgradable) -> UnityEngine.PlayerLoop.EarlyUpdate/ARCoreUpdate", false)]
		[RequiredByNativeCode]
		public struct TangoUpdate
		{
		}

		// Token: 0x02000284 RID: 644
		[RequiredByNativeCode]
		public struct ARCoreUpdate
		{
		}

		// Token: 0x02000285 RID: 645
		[RequiredByNativeCode]
		public struct PerformanceAnalyticsUpdate
		{
		}
	}
}
