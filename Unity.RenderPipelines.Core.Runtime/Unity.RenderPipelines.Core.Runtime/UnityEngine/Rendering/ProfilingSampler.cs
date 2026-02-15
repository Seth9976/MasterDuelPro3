using System;
using Unity.Profiling;
using UnityEngine.Profiling;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D9 RID: 217
	[IgnoredByDeepProfiler]
	public class ProfilingSampler
	{
		// Token: 0x06000708 RID: 1800 RVA: 0x000104E9 File Offset: 0x0000E6E9
		public static ProfilingSampler Get<TEnum>(TEnum marker) where TEnum : Enum
		{
			return null;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000105B0 File Offset: 0x0000E7B0
		public ProfilingSampler(string name)
		{
			this.sampler = CustomSampler.Create(name, true);
			this.inlineSampler = CustomSampler.Create("Inl_" + name, false);
			this.name = name;
			this.m_Recorder = this.sampler.GetRecorder();
			this.m_Recorder.enabled = false;
			this.m_InlineRecorder = this.inlineSampler.GetRecorder();
			this.m_InlineRecorder.enabled = false;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00010628 File Offset: 0x0000E828
		public void Begin(CommandBuffer cmd)
		{
			if (cmd != null)
			{
				if (this.sampler != null && this.sampler.isValid)
				{
					cmd.BeginSample(this.sampler);
					return;
				}
				cmd.BeginSample(this.name);
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001065B File Offset: 0x0000E85B
		public void End(CommandBuffer cmd)
		{
			if (cmd != null)
			{
				if (this.sampler != null && this.sampler.isValid)
				{
					cmd.EndSample(this.sampler);
					return;
				}
				cmd.EndSample(this.name);
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001068E File Offset: 0x0000E88E
		internal bool IsValid()
		{
			return this.sampler != null && this.inlineSampler != null;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x000106A3 File Offset: 0x0000E8A3
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x000106AB File Offset: 0x0000E8AB
		internal CustomSampler sampler { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x000106B4 File Offset: 0x0000E8B4
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x000106BC File Offset: 0x0000E8BC
		internal CustomSampler inlineSampler { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x000106C5 File Offset: 0x0000E8C5
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x000106CD File Offset: 0x0000E8CD
		public string name { get; private set; }

		// Token: 0x170000AC RID: 172
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x000106D6 File Offset: 0x0000E8D6
		public bool enableRecording
		{
			set
			{
				this.m_Recorder.enabled = value;
				this.m_InlineRecorder.enabled = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x000106F0 File Offset: 0x0000E8F0
		public float gpuElapsedTime
		{
			get
			{
				if (!this.m_Recorder.enabled)
				{
					return 0f;
				}
				return (float)this.m_Recorder.gpuElapsedNanoseconds / 1000000f;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00010717 File Offset: 0x0000E917
		public int gpuSampleCount
		{
			get
			{
				if (!this.m_Recorder.enabled)
				{
					return 0;
				}
				return this.m_Recorder.gpuSampleBlockCount;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00010733 File Offset: 0x0000E933
		public float cpuElapsedTime
		{
			get
			{
				if (!this.m_Recorder.enabled)
				{
					return 0f;
				}
				return (float)this.m_Recorder.elapsedNanoseconds / 1000000f;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001075A File Offset: 0x0000E95A
		public int cpuSampleCount
		{
			get
			{
				if (!this.m_Recorder.enabled)
				{
					return 0;
				}
				return this.m_Recorder.sampleBlockCount;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00010776 File Offset: 0x0000E976
		public float inlineCpuElapsedTime
		{
			get
			{
				if (!this.m_InlineRecorder.enabled)
				{
					return 0f;
				}
				return (float)this.m_InlineRecorder.elapsedNanoseconds / 1000000f;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001079D File Offset: 0x0000E99D
		public int inlineCpuSampleCount
		{
			get
			{
				if (!this.m_InlineRecorder.enabled)
				{
					return 0;
				}
				return this.m_InlineRecorder.sampleBlockCount;
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000022CB File Offset: 0x000004CB
		private ProfilingSampler()
		{
		}

		// Token: 0x04000297 RID: 663
		private Recorder m_Recorder;

		// Token: 0x04000298 RID: 664
		private Recorder m_InlineRecorder;
	}
}
