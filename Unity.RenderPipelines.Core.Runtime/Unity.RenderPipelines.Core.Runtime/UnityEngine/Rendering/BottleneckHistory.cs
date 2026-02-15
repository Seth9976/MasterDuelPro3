using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CC RID: 204
	internal class BottleneckHistory
	{
		// Token: 0x060006CD RID: 1741 RVA: 0x0000FFDA File Offset: 0x0000E1DA
		public BottleneckHistory(int initialCapacity)
		{
			this.m_Bottlenecks.Capacity = initialCapacity;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0000FFF9 File Offset: 0x0000E1F9
		internal void DiscardOldSamples(int historySize)
		{
			while (this.m_Bottlenecks.Count >= historySize)
			{
				this.m_Bottlenecks.RemoveAt(0);
			}
			this.m_Bottlenecks.Capacity = historySize;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00010024 File Offset: 0x0000E224
		internal void AddBottleneckFromAveragedSample(FrameTimeSample frameHistorySampleAverage)
		{
			PerformanceBottleneck bottleneck = BottleneckHistory.DetermineBottleneck(frameHistorySampleAverage);
			this.m_Bottlenecks.Add(bottleneck);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00010044 File Offset: 0x0000E244
		internal void ComputeHistogram()
		{
			BottleneckHistogram stats = default(BottleneckHistogram);
			for (int i = 0; i < this.m_Bottlenecks.Count; i++)
			{
				switch (this.m_Bottlenecks[i])
				{
				case PerformanceBottleneck.PresentLimited:
					stats.PresentLimited += 1f;
					break;
				case PerformanceBottleneck.CPU:
					stats.CPU += 1f;
					break;
				case PerformanceBottleneck.GPU:
					stats.GPU += 1f;
					break;
				case PerformanceBottleneck.Balanced:
					stats.Balanced += 1f;
					break;
				}
			}
			stats.Balanced /= (float)this.m_Bottlenecks.Count;
			stats.CPU /= (float)this.m_Bottlenecks.Count;
			stats.GPU /= (float)this.m_Bottlenecks.Count;
			stats.PresentLimited /= (float)this.m_Bottlenecks.Count;
			this.Histogram = stats;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00010140 File Offset: 0x0000E340
		private static PerformanceBottleneck DetermineBottleneck(FrameTimeSample s)
		{
			if (s.GPUFrameTime == 0f || s.MainThreadCPUFrameTime == 0f)
			{
				return PerformanceBottleneck.Indeterminate;
			}
			float fullFrameTimeWithMargin = 0.8f * s.FullFrameTime;
			if (s.GPUFrameTime > fullFrameTimeWithMargin && s.MainThreadCPUFrameTime < fullFrameTimeWithMargin && s.RenderThreadCPUFrameTime < fullFrameTimeWithMargin)
			{
				return PerformanceBottleneck.GPU;
			}
			if (s.GPUFrameTime < fullFrameTimeWithMargin && (s.MainThreadCPUFrameTime > fullFrameTimeWithMargin || s.RenderThreadCPUFrameTime > fullFrameTimeWithMargin))
			{
				return PerformanceBottleneck.CPU;
			}
			if (s.MainThreadCPUPresentWaitTime > 0.5f && s.GPUFrameTime < fullFrameTimeWithMargin && s.MainThreadCPUFrameTime < fullFrameTimeWithMargin && s.RenderThreadCPUFrameTime < fullFrameTimeWithMargin)
			{
				return PerformanceBottleneck.PresentLimited;
			}
			return PerformanceBottleneck.Balanced;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000101DB File Offset: 0x0000E3DB
		internal void Clear()
		{
			this.m_Bottlenecks.Clear();
			this.Histogram = default(BottleneckHistogram);
		}

		// Token: 0x0400027F RID: 639
		private List<PerformanceBottleneck> m_Bottlenecks = new List<PerformanceBottleneck>();

		// Token: 0x04000280 RID: 640
		internal BottleneckHistogram Histogram;
	}
}
