using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DA RID: 218
	public struct ProfilingScope : IDisposable
	{
		// Token: 0x0600071B RID: 1819 RVA: 0x00005704 File Offset: 0x00003904
		public ProfilingScope(ProfilingSampler sampler)
		{
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00005704 File Offset: 0x00003904
		public ProfilingScope(CommandBuffer cmd, ProfilingSampler sampler)
		{
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00005704 File Offset: 0x00003904
		public ProfilingScope(BaseCommandBuffer cmd, ProfilingSampler sampler)
		{
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00005704 File Offset: 0x00003904
		public void Dispose()
		{
		}
	}
}
