using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CE RID: 206
	internal class FrameTimeSampleHistory
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x00010220 File Offset: 0x0000E420
		public FrameTimeSampleHistory(int initialCapacity)
		{
			this.m_Samples.Capacity = initialCapacity;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001023F File Offset: 0x0000E43F
		internal void Add(FrameTimeSample sample)
		{
			this.m_Samples.Add(sample);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00010250 File Offset: 0x0000E450
		internal void ComputeAggregateValues()
		{
			FrameTimeSample average = default(FrameTimeSample);
			FrameTimeSample min = new FrameTimeSample(float.MaxValue);
			FrameTimeSample max = new FrameTimeSample(float.MinValue);
			FrameTimeSample numValidSamples = default(FrameTimeSample);
			for (int i = 0; i < this.m_Samples.Count; i++)
			{
				FrameTimeSample s = this.m_Samples[i];
				FrameTimeSampleHistory.<ComputeAggregateValues>g__ForEachSampleMember|12_0(ref min, s, FrameTimeSampleHistory.s_SampleValueMin);
				FrameTimeSampleHistory.<ComputeAggregateValues>g__ForEachSampleMember|12_0(ref max, s, FrameTimeSampleHistory.s_SampleValueMax);
				FrameTimeSampleHistory.<ComputeAggregateValues>g__ForEachSampleMember|12_0(ref average, s, FrameTimeSampleHistory.s_SampleValueAdd);
				FrameTimeSampleHistory.<ComputeAggregateValues>g__ForEachSampleMember|12_0(ref numValidSamples, s, FrameTimeSampleHistory.s_SampleValueCountValid);
			}
			FrameTimeSampleHistory.<ComputeAggregateValues>g__ForEachSampleMember|12_0(ref min, numValidSamples, FrameTimeSampleHistory.s_SampleValueEnsureValid);
			FrameTimeSampleHistory.<ComputeAggregateValues>g__ForEachSampleMember|12_0(ref max, numValidSamples, FrameTimeSampleHistory.s_SampleValueEnsureValid);
			FrameTimeSampleHistory.<ComputeAggregateValues>g__ForEachSampleMember|12_0(ref average, numValidSamples, FrameTimeSampleHistory.s_SampleValueDivide);
			this.SampleAverage = average;
			this.SampleMin = min;
			this.SampleMax = max;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00010322 File Offset: 0x0000E522
		internal void DiscardOldSamples(int sampleHistorySize)
		{
			while (this.m_Samples.Count >= sampleHistorySize)
			{
				this.m_Samples.RemoveAt(0);
			}
			this.m_Samples.Capacity = sampleHistorySize;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001034C File Offset: 0x0000E54C
		internal void Clear()
		{
			this.m_Samples.Clear();
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000103E8 File Offset: 0x0000E5E8
		[CompilerGenerated]
		internal static void <ComputeAggregateValues>g__ForEachSampleMember|12_0(ref FrameTimeSample aggregate, FrameTimeSample sample, Func<float, float, float> func)
		{
			aggregate.FramesPerSecond = func(aggregate.FramesPerSecond, sample.FramesPerSecond);
			aggregate.FullFrameTime = func(aggregate.FullFrameTime, sample.FullFrameTime);
			aggregate.MainThreadCPUFrameTime = func(aggregate.MainThreadCPUFrameTime, sample.MainThreadCPUFrameTime);
			aggregate.MainThreadCPUPresentWaitTime = func(aggregate.MainThreadCPUPresentWaitTime, sample.MainThreadCPUPresentWaitTime);
			aggregate.RenderThreadCPUFrameTime = func(aggregate.RenderThreadCPUFrameTime, sample.RenderThreadCPUFrameTime);
			aggregate.GPUFrameTime = func(aggregate.GPUFrameTime, sample.GPUFrameTime);
		}

		// Token: 0x04000287 RID: 647
		private List<FrameTimeSample> m_Samples = new List<FrameTimeSample>();

		// Token: 0x04000288 RID: 648
		internal FrameTimeSample SampleAverage;

		// Token: 0x04000289 RID: 649
		internal FrameTimeSample SampleMin;

		// Token: 0x0400028A RID: 650
		internal FrameTimeSample SampleMax;

		// Token: 0x0400028B RID: 651
		private static Func<float, float, float> s_SampleValueAdd = (float value, float other) => value + other;

		// Token: 0x0400028C RID: 652
		private static Func<float, float, float> s_SampleValueMin = delegate(float value, float other)
		{
			if (other <= 0f)
			{
				return value;
			}
			return Mathf.Min(value, other);
		};

		// Token: 0x0400028D RID: 653
		private static Func<float, float, float> s_SampleValueMax = (float value, float other) => Mathf.Max(value, other);

		// Token: 0x0400028E RID: 654
		private static Func<float, float, float> s_SampleValueCountValid = delegate(float value, float other)
		{
			if (other <= 0f)
			{
				return value;
			}
			return value + 1f;
		};

		// Token: 0x0400028F RID: 655
		private static Func<float, float, float> s_SampleValueEnsureValid = delegate(float value, float other)
		{
			if (other <= 0f)
			{
				return 0f;
			}
			return value;
		};

		// Token: 0x04000290 RID: 656
		private static Func<float, float, float> s_SampleValueDivide = delegate(float value, float other)
		{
			if (other <= 0f)
			{
				return 0f;
			}
			return value / other;
		};
	}
}
