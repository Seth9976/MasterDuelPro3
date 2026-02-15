using System;
using Unity.Profiling;
using UnityEngine.Profiling;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DB RID: 219
	[Obsolete("Please use ProfilingScope")]
	[IgnoredByDeepProfiler]
	public struct ProfilingSample : IDisposable
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x000107B9 File Offset: 0x0000E9B9
		public ProfilingSample(CommandBuffer cmd, string name, CustomSampler sampler = null)
		{
			this.m_Cmd = cmd;
			this.m_Name = name;
			this.m_Disposed = false;
			if (cmd != null && name != "")
			{
				cmd.BeginSample(name);
			}
			this.m_Sampler = sampler;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000107EE File Offset: 0x0000E9EE
		public ProfilingSample(CommandBuffer cmd, string format, object arg)
		{
			this = new ProfilingSample(cmd, string.Format(format, arg), null);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000107FF File Offset: 0x0000E9FF
		public ProfilingSample(CommandBuffer cmd, string format, params object[] args)
		{
			this = new ProfilingSample(cmd, string.Format(format, args), null);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00010810 File Offset: 0x0000EA10
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00010819 File Offset: 0x0000EA19
		private void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			if (disposing && this.m_Cmd != null && this.m_Name != "")
			{
				this.m_Cmd.EndSample(this.m_Name);
			}
			this.m_Disposed = true;
		}

		// Token: 0x04000299 RID: 665
		private readonly CommandBuffer m_Cmd;

		// Token: 0x0400029A RID: 666
		private readonly string m_Name;

		// Token: 0x0400029B RID: 667
		private bool m_Disposed;

		// Token: 0x0400029C RID: 668
		private CustomSampler m_Sampler;
	}
}
