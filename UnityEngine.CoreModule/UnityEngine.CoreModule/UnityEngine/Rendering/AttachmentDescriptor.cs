using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200039A RID: 922
	public struct AttachmentDescriptor : IEquatable<AttachmentDescriptor>
	{
		// Token: 0x17000396 RID: 918
		// (set) Token: 0x06001929 RID: 6441 RVA: 0x00035EE6 File Offset: 0x000340E6
		public RenderBufferLoadAction loadAction
		{
			set
			{
				this.m_LoadAction = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (set) Token: 0x0600192A RID: 6442 RVA: 0x00035EF0 File Offset: 0x000340F0
		public RenderBufferStoreAction storeAction
		{
			set
			{
				this.m_StoreAction = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x00035EFC File Offset: 0x000340FC
		public GraphicsFormat graphicsFormat
		{
			get
			{
				return this.m_Format;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x00035F14 File Offset: 0x00034114
		// (set) Token: 0x0600192D RID: 6445 RVA: 0x00035F2C File Offset: 0x0003412C
		public RenderTargetIdentifier loadStoreTarget
		{
			get
			{
				return this.m_LoadStoreTarget;
			}
			set
			{
				this.m_LoadStoreTarget = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (set) Token: 0x0600192E RID: 6446 RVA: 0x00035F36 File Offset: 0x00034136
		public RenderTargetIdentifier resolveTarget
		{
			set
			{
				this.m_ResolveTarget = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (set) Token: 0x0600192F RID: 6447 RVA: 0x00035F40 File Offset: 0x00034140
		public Color clearColor
		{
			set
			{
				this.m_ClearColor = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (set) Token: 0x06001930 RID: 6448 RVA: 0x00035F4A File Offset: 0x0003414A
		public float clearDepth
		{
			set
			{
				this.m_ClearDepth = value;
			}
		}

		// Token: 0x1700039D RID: 925
		// (set) Token: 0x06001931 RID: 6449 RVA: 0x00035F54 File Offset: 0x00034154
		public uint clearStencil
		{
			set
			{
				this.m_ClearStencil = value;
			}
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00035F60 File Offset: 0x00034160
		public void ConfigureTarget(RenderTargetIdentifier target, bool loadExistingContents, bool storeResults)
		{
			this.m_LoadStoreTarget = target;
			bool flag = loadExistingContents && this.m_LoadAction != RenderBufferLoadAction.Clear;
			if (flag)
			{
				this.m_LoadAction = RenderBufferLoadAction.Load;
			}
			if (storeResults)
			{
				bool flag2 = this.m_StoreAction == RenderBufferStoreAction.StoreAndResolve || this.m_StoreAction == RenderBufferStoreAction.Resolve;
				if (flag2)
				{
					this.m_StoreAction = RenderBufferStoreAction.StoreAndResolve;
				}
				else
				{
					this.m_StoreAction = RenderBufferStoreAction.Store;
				}
			}
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00035FC4 File Offset: 0x000341C4
		public void ConfigureResolveTarget(RenderTargetIdentifier target)
		{
			this.m_ResolveTarget = target;
			bool flag = this.m_StoreAction == RenderBufferStoreAction.StoreAndResolve || this.m_StoreAction == RenderBufferStoreAction.Store;
			if (flag)
			{
				this.m_StoreAction = RenderBufferStoreAction.StoreAndResolve;
			}
			else
			{
				this.m_StoreAction = RenderBufferStoreAction.Resolve;
			}
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00036002 File Offset: 0x00034202
		public void ConfigureClear(Color clearColor, float clearDepth = 1f, uint clearStencil = 0U)
		{
			this.m_ClearColor = clearColor;
			this.m_ClearDepth = clearDepth;
			this.m_ClearStencil = clearStencil;
			this.m_LoadAction = RenderBufferLoadAction.Clear;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00036024 File Offset: 0x00034224
		public AttachmentDescriptor(GraphicsFormat format)
		{
			this = default(AttachmentDescriptor);
			this.m_LoadAction = RenderBufferLoadAction.DontCare;
			this.m_StoreAction = RenderBufferStoreAction.DontCare;
			this.m_Format = format;
			this.m_LoadStoreTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.None);
			this.m_ResolveTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.None);
			this.m_ClearColor = new Color(0f, 0f, 0f, 0f);
			this.m_ClearDepth = 1f;
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00036090 File Offset: 0x00034290
		public bool Equals(AttachmentDescriptor other)
		{
			return this.m_LoadAction == other.m_LoadAction && this.m_StoreAction == other.m_StoreAction && this.m_Format == other.m_Format && this.m_LoadStoreTarget.Equals(other.m_LoadStoreTarget) && this.m_ResolveTarget.Equals(other.m_ResolveTarget) && this.m_ClearColor.Equals(other.m_ClearColor) && this.m_ClearDepth.Equals(other.m_ClearDepth) && this.m_ClearStencil == other.m_ClearStencil;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0003612C File Offset: 0x0003432C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is AttachmentDescriptor && this.Equals((AttachmentDescriptor)obj);
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00036164 File Offset: 0x00034364
		public override int GetHashCode()
		{
			int hashCode = (int)this.m_LoadAction;
			hashCode = (hashCode * 397) ^ (int)this.m_StoreAction;
			hashCode = (hashCode * 397) ^ (int)this.m_Format;
			hashCode = (hashCode * 397) ^ this.m_LoadStoreTarget.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_ResolveTarget.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_ClearColor.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_ClearDepth.GetHashCode();
			return (hashCode * 397) ^ (int)this.m_ClearStencil;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00036210 File Offset: 0x00034410
		public static bool operator !=(AttachmentDescriptor left, AttachmentDescriptor right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000B68 RID: 2920
		private RenderBufferLoadAction m_LoadAction;

		// Token: 0x04000B69 RID: 2921
		private RenderBufferStoreAction m_StoreAction;

		// Token: 0x04000B6A RID: 2922
		private GraphicsFormat m_Format;

		// Token: 0x04000B6B RID: 2923
		private RenderTargetIdentifier m_LoadStoreTarget;

		// Token: 0x04000B6C RID: 2924
		private RenderTargetIdentifier m_ResolveTarget;

		// Token: 0x04000B6D RID: 2925
		private Color m_ClearColor;

		// Token: 0x04000B6E RID: 2926
		private float m_ClearDepth;

		// Token: 0x04000B6F RID: 2927
		private uint m_ClearStencil;
	}
}
