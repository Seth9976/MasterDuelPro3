using System;
using Unity.Profiling;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	// Token: 0x020001F0 RID: 496
	[UsedByNativeCode]
	public sealed class Recorder
	{
		// Token: 0x06001387 RID: 4999 RVA: 0x000205EB File Offset: 0x0001E7EB
		internal Recorder()
		{
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00029208 File Offset: 0x00027408
		internal Recorder(ProfilerRecorderHandle handle)
		{
			bool flag = !handle.Valid;
			if (!flag)
			{
				this.m_RecorderCPU = new ProfilerRecorder(handle, 1, (ProfilerRecorderOptions)153);
				bool flag2 = (ProfilerRecorderHandle.GetDescription(handle).Flags & MarkerFlags.SampleGPU) > MarkerFlags.Default;
				if (flag2)
				{
					this.m_RecorderGPU = new ProfilerRecorder(handle, 1, (ProfilerRecorderOptions)217);
				}
			}
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0002926C File Offset: 0x0002746C
		~Recorder()
		{
			this.m_RecorderCPU.Dispose();
			this.m_RecorderGPU.Dispose();
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x000292B0 File Offset: 0x000274B0
		// (set) Token: 0x0600138B RID: 5003 RVA: 0x000292CD File Offset: 0x000274CD
		public bool enabled
		{
			get
			{
				return this.m_RecorderCPU.IsRunning;
			}
			set
			{
				this.SetEnabled(value);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x000292D8 File Offset: 0x000274D8
		public long elapsedNanoseconds
		{
			get
			{
				bool flag = !this.m_RecorderCPU.Valid;
				long num;
				if (flag)
				{
					num = 0L;
				}
				else
				{
					num = this.m_RecorderCPU.LastValue;
				}
				return num;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x0002930C File Offset: 0x0002750C
		public long gpuElapsedNanoseconds
		{
			get
			{
				bool flag = !this.m_RecorderGPU.Valid;
				long num;
				if (flag)
				{
					num = 0L;
				}
				else
				{
					num = this.m_RecorderGPU.LastValue;
				}
				return num;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00029340 File Offset: 0x00027540
		public int sampleBlockCount
		{
			get
			{
				bool flag = !this.m_RecorderCPU.Valid;
				int num;
				if (flag)
				{
					num = 0;
				}
				else
				{
					bool flag2 = this.m_RecorderCPU.Count != 1;
					if (flag2)
					{
						num = 0;
					}
					else
					{
						num = (int)this.m_RecorderCPU.GetSample(0).Count;
					}
				}
				return num;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x00029398 File Offset: 0x00027598
		public int gpuSampleBlockCount
		{
			get
			{
				bool flag = !this.m_RecorderGPU.Valid;
				int num;
				if (flag)
				{
					num = 0;
				}
				else
				{
					bool flag2 = this.m_RecorderGPU.Count != 1;
					if (flag2)
					{
						num = 0;
					}
					else
					{
						num = (int)this.m_RecorderGPU.GetSample(0).Count;
					}
				}
				return num;
			}
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x000293F0 File Offset: 0x000275F0
		private void SetEnabled(bool state)
		{
			if (state)
			{
				this.m_RecorderCPU.Start();
				bool valid = this.m_RecorderGPU.Valid;
				if (valid)
				{
					this.m_RecorderGPU.Start();
				}
			}
			else
			{
				this.m_RecorderCPU.Stop();
				bool valid2 = this.m_RecorderGPU.Valid;
				if (valid2)
				{
					this.m_RecorderGPU.Stop();
				}
			}
		}

		// Token: 0x0400071A RID: 1818
		private const ProfilerRecorderOptions s_RecorderDefaultOptions = (ProfilerRecorderOptions)153;

		// Token: 0x0400071B RID: 1819
		internal static Recorder s_InvalidRecorder = new Recorder();

		// Token: 0x0400071C RID: 1820
		private ProfilerRecorder m_RecorderCPU;

		// Token: 0x0400071D RID: 1821
		private ProfilerRecorder m_RecorderGPU;
	}
}
