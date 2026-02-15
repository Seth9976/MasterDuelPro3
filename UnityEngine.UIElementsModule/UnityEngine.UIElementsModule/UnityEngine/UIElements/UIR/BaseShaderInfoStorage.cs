using System;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000553 RID: 1363
	internal abstract class BaseShaderInfoStorage : IDisposable
	{
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x0600259B RID: 9627
		public abstract Texture2D texture { get; }

		// Token: 0x0600259C RID: 9628
		public abstract bool AllocateRect(int width, int height, out RectInt uvs);

		// Token: 0x0600259D RID: 9629
		public abstract void SetTexel(int x, int y, Color color);

		// Token: 0x0600259E RID: 9630
		public abstract void UpdateTexture();

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x00095617 File Offset: 0x00093817
		// (set) Token: 0x060025A0 RID: 9632 RVA: 0x0009561F File Offset: 0x0009381F
		private protected bool disposed { protected get; private set; }

		// Token: 0x060025A1 RID: 9633 RVA: 0x00095628 File Offset: 0x00093828
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x0009563C File Offset: 0x0009383C
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				bool flag = !disposing;
				if (flag)
				{
				}
				this.disposed = true;
			}
		}

		// Token: 0x040012D1 RID: 4817
		protected static int s_TextureCounter;

		// Token: 0x040012D2 RID: 4818
		internal static ProfilerMarker s_MarkerCopyTexture = new ProfilerMarker("UIR.ShaderInfoStorage.CopyTexture");

		// Token: 0x040012D3 RID: 4819
		internal static ProfilerMarker s_MarkerGetTextureData = new ProfilerMarker("UIR.ShaderInfoStorage.GetTextureData");

		// Token: 0x040012D4 RID: 4820
		internal static ProfilerMarker s_MarkerUpdateTexture = new ProfilerMarker("UIR.ShaderInfoStorage.UpdateTexture");
	}
}
