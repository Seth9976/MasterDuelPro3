using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000119 RID: 281
	internal class ProbeVolumeDebug : IDebugData
	{
		// Token: 0x0600097F RID: 2431 RVA: 0x0001E0F0 File Offset: 0x0001C2F0
		public ProbeVolumeDebug()
		{
			this.Init();
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001E168 File Offset: 0x0001C368
		private void Init()
		{
			this.drawProbes = false;
			this.drawBricks = false;
			this.drawCells = false;
			this.realtimeSubdivision = false;
			this.subdivisionCellUpdatePerFrame = 4;
			this.subdivisionDelayInSeconds = 1f;
			this.probeShading = DebugProbeShadingMode.SH;
			this.probeSize = 0.3f;
			this.subdivisionViewCullingDistance = 500f;
			this.probeCullingDistance = 200f;
			this.maxSubdivToVisualize = 7;
			this.minSubdivToVisualize = 0;
			this.exposureCompensation = 0f;
			this.drawProbeSamplingDebug = false;
			this.probeSamplingDebugSize = 0.3f;
			this.drawVirtualOffsetPush = false;
			this.offsetSize = 0.025f;
			this.freezeStreaming = false;
			this.displayCellStreamingScore = false;
			this.displayIndexFragmentation = false;
			this.otherStateIndex = 0;
			this.autoDrawProbes = true;
			this.isolationProbeDebug = true;
			this.visibleLayers = byte.MaxValue;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0001E23D File Offset: 0x0001C43D
		public Action GetReset()
		{
			return delegate
			{
				this.Init();
			};
		}

		// Token: 0x040004D6 RID: 1238
		public bool drawProbes;

		// Token: 0x040004D7 RID: 1239
		public bool drawBricks;

		// Token: 0x040004D8 RID: 1240
		public bool drawCells;

		// Token: 0x040004D9 RID: 1241
		public bool realtimeSubdivision;

		// Token: 0x040004DA RID: 1242
		public int subdivisionCellUpdatePerFrame = 4;

		// Token: 0x040004DB RID: 1243
		public float subdivisionDelayInSeconds = 1f;

		// Token: 0x040004DC RID: 1244
		public DebugProbeShadingMode probeShading;

		// Token: 0x040004DD RID: 1245
		public float probeSize = 0.3f;

		// Token: 0x040004DE RID: 1246
		public float subdivisionViewCullingDistance = 500f;

		// Token: 0x040004DF RID: 1247
		public float probeCullingDistance = 200f;

		// Token: 0x040004E0 RID: 1248
		public int maxSubdivToVisualize = 7;

		// Token: 0x040004E1 RID: 1249
		public int minSubdivToVisualize;

		// Token: 0x040004E2 RID: 1250
		public float exposureCompensation;

		// Token: 0x040004E3 RID: 1251
		public bool drawProbeSamplingDebug;

		// Token: 0x040004E4 RID: 1252
		public float probeSamplingDebugSize = 0.3f;

		// Token: 0x040004E5 RID: 1253
		public bool debugWithSamplingNoise;

		// Token: 0x040004E6 RID: 1254
		public uint samplingRenderingLayer;

		// Token: 0x040004E7 RID: 1255
		public bool drawVirtualOffsetPush;

		// Token: 0x040004E8 RID: 1256
		public float offsetSize = 0.025f;

		// Token: 0x040004E9 RID: 1257
		public bool freezeStreaming;

		// Token: 0x040004EA RID: 1258
		public bool displayCellStreamingScore;

		// Token: 0x040004EB RID: 1259
		public bool displayIndexFragmentation;

		// Token: 0x040004EC RID: 1260
		public int otherStateIndex;

		// Token: 0x040004ED RID: 1261
		public bool verboseStreamingLog;

		// Token: 0x040004EE RID: 1262
		public bool debugStreaming;

		// Token: 0x040004EF RID: 1263
		public bool autoDrawProbes = true;

		// Token: 0x040004F0 RID: 1264
		public bool isolationProbeDebug = true;

		// Token: 0x040004F1 RID: 1265
		public byte visibleLayers;

		// Token: 0x040004F2 RID: 1266
		public static Vector3 currentOffset;

		// Token: 0x040004F3 RID: 1267
		internal static int s_ActiveAdjustmentVolumes;
	}
}
