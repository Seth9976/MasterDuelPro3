using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000505 RID: 1285
	internal class DetachedAllocator
	{
		// Token: 0x060023D8 RID: 9176 RVA: 0x0008484D File Offset: 0x00082A4D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x00084860 File Offset: 0x00082A60
		protected void Dispose(bool disposing)
		{
			bool disposed = this.m_Disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_VertsPool.Dispose();
					this.m_IndexPool.Dispose();
				}
				this.m_Disposed = true;
			}
		}

		// Token: 0x0400106C RID: 4204
		private TempAllocator<Vertex> m_VertsPool;

		// Token: 0x0400106D RID: 4205
		private TempAllocator<ushort> m_IndexPool;

		// Token: 0x0400106E RID: 4206
		private List<MeshWriteData> m_MeshWriteDataPool;

		// Token: 0x0400106F RID: 4207
		private int m_MeshWriteDataCount;

		// Token: 0x04001070 RID: 4208
		private bool m_Disposed;
	}
}
