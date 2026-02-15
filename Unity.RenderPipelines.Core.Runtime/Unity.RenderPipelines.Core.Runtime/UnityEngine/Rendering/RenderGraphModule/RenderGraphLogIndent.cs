using System;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200024A RID: 586
	internal struct RenderGraphLogIndent : IDisposable
	{
		// Token: 0x06000FE9 RID: 4073 RVA: 0x0003A18C File Offset: 0x0003838C
		public RenderGraphLogIndent(RenderGraphLogger logger, int indentation = 1)
		{
			this.m_Disposed = false;
			this.m_Indentation = indentation;
			this.m_Logger = logger;
			this.m_Logger.IncrementIndentation(this.m_Indentation);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0003A1B4 File Offset: 0x000383B4
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0003A1BD File Offset: 0x000383BD
		private void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			if (disposing && this.m_Logger != null)
			{
				this.m_Logger.DecrementIndentation(this.m_Indentation);
			}
			this.m_Disposed = true;
		}

		// Token: 0x04000A40 RID: 2624
		private int m_Indentation;

		// Token: 0x04000A41 RID: 2625
		private RenderGraphLogger m_Logger;

		// Token: 0x04000A42 RID: 2626
		private bool m_Disposed;
	}
}
