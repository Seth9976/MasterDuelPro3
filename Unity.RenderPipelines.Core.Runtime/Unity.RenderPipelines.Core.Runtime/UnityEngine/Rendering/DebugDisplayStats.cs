using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000087 RID: 135
	public abstract class DebugDisplayStats<TProfileId> where TProfileId : Enum
	{
		// Token: 0x0600056B RID: 1387
		public abstract void EnableProfilingRecorders();

		// Token: 0x0600056C RID: 1388
		public abstract void DisableProfilingRecorders();

		// Token: 0x0600056D RID: 1389
		public abstract void RegisterDebugUI(List<DebugUI.Widget> list);

		// Token: 0x0600056E RID: 1390
		public abstract void Update();

		// Token: 0x0600056F RID: 1391 RVA: 0x0000B57C File Offset: 0x0000977C
		protected List<TProfileId> GetProfilerIdsToDisplay()
		{
			List<TProfileId> ids = new List<TProfileId>();
			Type type = typeof(TProfileId);
			Func<MemberInfo, bool> <>9__0;
			foreach (object enumValue in Enum.GetValues(type))
			{
				IEnumerable<MemberInfo> member = type.GetMember(enumValue.ToString());
				Func<MemberInfo, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (MemberInfo m) => m.DeclaringType == type);
				}
				if (Attribute.GetCustomAttribute(member.First(func), typeof(HideInDebugUIAttribute)) == null)
				{
					ids.Add((TProfileId)((object)enumValue));
				}
			}
			return ids;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000B644 File Offset: 0x00009844
		protected void UpdateDetailedStats(List<TProfileId> samplers)
		{
			this.m_HiddenProfileIds.Clear();
			this.m_TimeSinceLastAvgValue += Time.unscaledDeltaTime;
			this.m_AccumulatedFrames++;
			bool needUpdatingAverages = this.m_TimeSinceLastAvgValue >= 1f;
			this.UpdateListOfAveragedProfilerTimings(needUpdatingAverages, samplers);
			if (needUpdatingAverages)
			{
				this.m_TimeSinceLastAvgValue = 0f;
				this.m_AccumulatedFrames = 0;
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000B6AA File Offset: 0x000098AA
		protected DebugUI.Widget BuildDetailedStatsList(string title, List<TProfileId> samplers)
		{
			return new DebugUI.Foldout(title, this.BuildProfilingSamplerWidgetList(samplers), DebugDisplayStats<TProfileId>.k_DetailedStatsColumnLabels, null)
			{
				opened = true
			};
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000B6C8 File Offset: 0x000098C8
		private void UpdateListOfAveragedProfilerTimings(bool needUpdatingAverages, List<TProfileId> samplers)
		{
			foreach (TProfileId samplerId in samplers)
			{
				ProfilingSampler sampler = ProfilingSampler.Get<TProfileId>(samplerId);
				bool allValuesZero = true;
				DebugDisplayStats<TProfileId>.AccumulatedTiming accCPUTiming;
				if (this.m_AccumulatedTiming[0].TryGetValue(samplerId, out accCPUTiming))
				{
					accCPUTiming.accumulatedValue += sampler.cpuElapsedTime;
					allValuesZero &= accCPUTiming.accumulatedValue == 0f;
				}
				DebugDisplayStats<TProfileId>.AccumulatedTiming accInlineCPUTiming;
				if (this.m_AccumulatedTiming[1].TryGetValue(samplerId, out accInlineCPUTiming))
				{
					accInlineCPUTiming.accumulatedValue += sampler.inlineCpuElapsedTime;
					allValuesZero &= accInlineCPUTiming.accumulatedValue == 0f;
				}
				DebugDisplayStats<TProfileId>.AccumulatedTiming accGPUTiming;
				if (this.m_AccumulatedTiming[2].TryGetValue(samplerId, out accGPUTiming))
				{
					accGPUTiming.accumulatedValue += sampler.gpuElapsedTime;
					allValuesZero &= accGPUTiming.accumulatedValue == 0f;
				}
				if (needUpdatingAverages)
				{
					if (accCPUTiming != null)
					{
						accCPUTiming.UpdateLastAverage(this.m_AccumulatedFrames);
					}
					if (accInlineCPUTiming != null)
					{
						accInlineCPUTiming.UpdateLastAverage(this.m_AccumulatedFrames);
					}
					if (accGPUTiming != null)
					{
						accGPUTiming.UpdateLastAverage(this.m_AccumulatedFrames);
					}
				}
				if (allValuesZero)
				{
					this.m_HiddenProfileIds.Add(samplerId);
				}
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000B818 File Offset: 0x00009A18
		private float GetSamplerTiming(TProfileId samplerId, ProfilingSampler sampler, DebugDisplayStats<TProfileId>.DebugProfilingType type)
		{
			DebugDisplayStats<TProfileId>.AccumulatedTiming accTiming;
			if (this.averageProfilerTimingsOverASecond && this.m_AccumulatedTiming[(int)type].TryGetValue(samplerId, out accTiming))
			{
				return accTiming.lastAverage;
			}
			if (type == DebugDisplayStats<TProfileId>.DebugProfilingType.CPU)
			{
				return sampler.cpuElapsedTime;
			}
			if (type != DebugDisplayStats<TProfileId>.DebugProfilingType.GPU)
			{
				return sampler.inlineCpuElapsedTime;
			}
			return sampler.gpuElapsedTime;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000B864 File Offset: 0x00009A64
		private ObservableList<DebugUI.Widget> BuildProfilingSamplerWidgetList(IEnumerable<TProfileId> samplers)
		{
			ObservableList<DebugUI.Widget> result = new ObservableList<DebugUI.Widget>();
			using (IEnumerator<TProfileId> enumerator = samplers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DebugDisplayStats<TProfileId>.<>c__DisplayClass19_1 CS$<>8__locals1 = new DebugDisplayStats<TProfileId>.<>c__DisplayClass19_1();
					CS$<>8__locals1.<>4__this = this;
					CS$<>8__locals1.samplerId = enumerator.Current;
					ProfilingSampler sampler = ProfilingSampler.Get<TProfileId>(CS$<>8__locals1.samplerId);
					if (sampler != null)
					{
						sampler.enableRecording = true;
						result.Add(new DebugUI.ValueTuple
						{
							displayName = sampler.name,
							isHiddenCallback = () => CS$<>8__locals1.<>4__this.hideEmptyScopes && CS$<>8__locals1.<>4__this.m_HiddenProfileIds.Contains(CS$<>8__locals1.samplerId),
							values = (from DebugDisplayStats<TProfileId>.DebugProfilingType e in Enum.GetValues(typeof(DebugDisplayStats<TProfileId>.DebugProfilingType))
								select CS$<>8__locals1.<>4__this.<BuildProfilingSamplerWidgetList>g__CreateWidgetForSampler|19_0(CS$<>8__locals1.samplerId, sampler, e)).ToArray<DebugUI.Value>()
						});
					}
				}
			}
			return result;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000B9C4 File Offset: 0x00009BC4
		[CompilerGenerated]
		private DebugUI.Value <BuildProfilingSamplerWidgetList>g__CreateWidgetForSampler|19_0(TProfileId samplerId, ProfilingSampler sampler, DebugDisplayStats<TProfileId>.DebugProfilingType type)
		{
			Dictionary<TProfileId, DebugDisplayStats<TProfileId>.AccumulatedTiming> accumulatedDictionary = this.m_AccumulatedTiming[(int)type];
			if (!accumulatedDictionary.ContainsKey(samplerId))
			{
				accumulatedDictionary.Add(samplerId, new DebugDisplayStats<TProfileId>.AccumulatedTiming());
			}
			return new DebugUI.Value
			{
				formatString = "{0:F2}ms",
				refreshRate = 0.2f,
				getter = () => this.GetSamplerTiming(samplerId, sampler, type)
			};
		}

		// Token: 0x0400019B RID: 411
		private static readonly string[] k_DetailedStatsColumnLabels = new string[] { "CPU", "CPUInline", "GPU" };

		// Token: 0x0400019C RID: 412
		private Dictionary<TProfileId, DebugDisplayStats<TProfileId>.AccumulatedTiming>[] m_AccumulatedTiming = new Dictionary<TProfileId, DebugDisplayStats<TProfileId>.AccumulatedTiming>[]
		{
			new Dictionary<TProfileId, DebugDisplayStats<TProfileId>.AccumulatedTiming>(),
			new Dictionary<TProfileId, DebugDisplayStats<TProfileId>.AccumulatedTiming>(),
			new Dictionary<TProfileId, DebugDisplayStats<TProfileId>.AccumulatedTiming>()
		};

		// Token: 0x0400019D RID: 413
		private float m_TimeSinceLastAvgValue;

		// Token: 0x0400019E RID: 414
		private int m_AccumulatedFrames;

		// Token: 0x0400019F RID: 415
		private HashSet<TProfileId> m_HiddenProfileIds = new HashSet<TProfileId>();

		// Token: 0x040001A0 RID: 416
		private const float k_AccumulationTimeInSeconds = 1f;

		// Token: 0x040001A1 RID: 417
		protected bool averageProfilerTimingsOverASecond;

		// Token: 0x040001A2 RID: 418
		protected bool hideEmptyScopes = true;

		// Token: 0x02000088 RID: 136
		private class AccumulatedTiming
		{
			// Token: 0x06000578 RID: 1400 RVA: 0x0000BA4D File Offset: 0x00009C4D
			internal void UpdateLastAverage(int frameCount)
			{
				this.lastAverage = this.accumulatedValue / (float)frameCount;
				this.accumulatedValue = 0f;
			}

			// Token: 0x040001A3 RID: 419
			public float accumulatedValue;

			// Token: 0x040001A4 RID: 420
			public float lastAverage;
		}

		// Token: 0x02000089 RID: 137
		private enum DebugProfilingType
		{
			// Token: 0x040001A6 RID: 422
			CPU,
			// Token: 0x040001A7 RID: 423
			InlineCPU,
			// Token: 0x040001A8 RID: 424
			GPU
		}
	}
}
