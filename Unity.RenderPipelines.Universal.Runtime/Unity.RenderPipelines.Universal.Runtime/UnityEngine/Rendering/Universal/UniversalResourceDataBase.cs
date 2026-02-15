using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C7 RID: 199
	public abstract class UniversalResourceDataBase : ContextItem
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00012F35 File Offset: 0x00011135
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00012F3D File Offset: 0x0001113D
		internal bool isAccessible { get; set; }

		// Token: 0x060004F0 RID: 1264 RVA: 0x00012F46 File Offset: 0x00011146
		internal void InitFrame()
		{
			this.isAccessible = true;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00012F4F File Offset: 0x0001114F
		internal void EndFrame()
		{
			this.isAccessible = false;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00012F58 File Offset: 0x00011158
		protected void CheckAndSetTextureHandle(ref TextureHandle handle, TextureHandle newHandle)
		{
			if (!this.CheckAndWarnAboutAccessibility())
			{
				return;
			}
			handle = newHandle;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00012F6A File Offset: 0x0001116A
		protected TextureHandle CheckAndGetTextureHandle(ref TextureHandle handle)
		{
			if (!this.CheckAndWarnAboutAccessibility())
			{
				return TextureHandle.nullHandle;
			}
			return handle;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00012F80 File Offset: 0x00011180
		protected void CheckAndSetTextureHandle(ref TextureHandle[] handle, TextureHandle[] newHandle)
		{
			if (!this.CheckAndWarnAboutAccessibility())
			{
				return;
			}
			if (handle == null || handle.Length != newHandle.Length)
			{
				handle = new TextureHandle[newHandle.Length];
			}
			for (int i = 0; i < newHandle.Length; i++)
			{
				handle[i] = newHandle[i];
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00012FCA File Offset: 0x000111CA
		protected TextureHandle[] CheckAndGetTextureHandle(ref TextureHandle[] handle)
		{
			if (!this.CheckAndWarnAboutAccessibility())
			{
				return new TextureHandle[] { TextureHandle.nullHandle };
			}
			return handle;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00012FE9 File Offset: 0x000111E9
		protected bool CheckAndWarnAboutAccessibility()
		{
			if (!this.isAccessible)
			{
				Debug.LogError("Trying to access Universal Resources outside of the current frame setup.");
			}
			return this.isAccessible;
		}

		// Token: 0x020000C8 RID: 200
		internal enum ActiveID
		{
			// Token: 0x0400045B RID: 1115
			Camera,
			// Token: 0x0400045C RID: 1116
			BackBuffer
		}
	}
}
