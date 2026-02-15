using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020003BB RID: 955
	public abstract class RenderPipeline
	{
		// Token: 0x060019BB RID: 6587
		protected abstract void Render(ScriptableRenderContext context, Camera[] cameras);

		// Token: 0x060019BC RID: 6588 RVA: 0x00003D56 File Offset: 0x00001F56
		protected virtual void ProcessRenderRequests<RequestData>(ScriptableRenderContext context, Camera camera, RequestData renderRequest)
		{
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00038598 File Offset: 0x00036798
		protected internal virtual bool IsRenderRequestSupported<RequestData>(Camera camera, RequestData data)
		{
			return false;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000385AB File Offset: 0x000367AB
		protected static void BeginContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			RenderPipelineManager.BeginContextRendering(context, cameras);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000385B6 File Offset: 0x000367B6
		protected static void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			RenderPipelineManager.BeginCameraRendering(context, camera);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000385C1 File Offset: 0x000367C1
		protected static void EndContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			RenderPipelineManager.EndContextRendering(context, cameras);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x000385CC File Offset: 0x000367CC
		protected static void EndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			RenderPipelineManager.EndCameraRendering(context, camera);
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000385D7 File Offset: 0x000367D7
		protected virtual void Render(ScriptableRenderContext context, List<Camera> cameras)
		{
			this.Render(context, cameras.ToArray());
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000385E8 File Offset: 0x000367E8
		internal void InternalRender(ScriptableRenderContext context, List<Camera> cameras)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(string.Format("{0} has been disposed. Do not call Render on disposed a RenderPipeline.", this));
			}
			this.Render(context, cameras);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0003861C File Offset: 0x0003681C
		internal void InternalProcessRenderRequests<RequestData>(ScriptableRenderContext context, Camera camera, RequestData renderRequest)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(string.Format("{0} has been disposed. Do not call Render on disposed a RenderPipeline.", this));
			}
			this.ProcessRenderRequests<RequestData>(context, camera, renderRequest);
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x0003864F File Offset: 0x0003684F
		// (set) Token: 0x060019C6 RID: 6598 RVA: 0x00038657 File Offset: 0x00036857
		public bool disposed { get; private set; }

		// Token: 0x060019C7 RID: 6599 RVA: 0x00038660 File Offset: 0x00036860
		internal void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			this.disposed = true;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00003D56 File Offset: 0x00001F56
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x0003867C File Offset: 0x0003687C
		public virtual RenderPipelineGlobalSettings defaultSettings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x020003BC RID: 956
		public class StandardRequest
		{
			// Token: 0x04000C3C RID: 3132
			public RenderTexture destination;

			// Token: 0x04000C3D RID: 3133
			public int mipLevel;

			// Token: 0x04000C3E RID: 3134
			public CubemapFace face;

			// Token: 0x04000C3F RID: 3135
			public int slice;
		}
	}
}
