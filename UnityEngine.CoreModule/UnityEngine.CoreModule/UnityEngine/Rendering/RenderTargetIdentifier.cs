using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000342 RID: 834
	public struct RenderTargetIdentifier : IEquatable<RenderTargetIdentifier>
	{
		// Token: 0x06001656 RID: 5718 RVA: 0x0002ED3D File Offset: 0x0002CF3D
		public RenderTargetIdentifier(BuiltinRenderTextureType type)
		{
			this.m_Type = type;
			this.m_NameID = -1;
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0002ED75 File Offset: 0x0002CF75
		public RenderTargetIdentifier(string name)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = Shader.PropertyToID(name);
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0002EDB3 File Offset: 0x0002CFB3
		public RenderTargetIdentifier(int nameID)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = nameID;
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0002EDEC File Offset: 0x0002CFEC
		public RenderTargetIdentifier(int nameID, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = nameID;
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0002EE28 File Offset: 0x0002D028
		public RenderTargetIdentifier(RenderTargetIdentifier renderTargetIdentifier, int mipLevel, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = renderTargetIdentifier.m_Type;
			this.m_NameID = renderTargetIdentifier.m_NameID;
			this.m_InstanceID = renderTargetIdentifier.m_InstanceID;
			this.m_BufferPointer = renderTargetIdentifier.m_BufferPointer;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0002EE7C File Offset: 0x0002D07C
		public RenderTargetIdentifier(Texture tex)
		{
			bool flag = tex == null;
			if (flag)
			{
				this.m_Type = BuiltinRenderTextureType.None;
			}
			else
			{
				bool flag2 = tex is RenderTexture;
				if (flag2)
				{
					this.m_Type = BuiltinRenderTextureType.RenderTexture;
				}
				else
				{
					this.m_Type = BuiltinRenderTextureType.BindableTexture;
				}
			}
			this.m_BufferPointer = IntPtr.Zero;
			this.m_NameID = -1;
			this.m_InstanceID = (tex ? tex.GetInstanceID() : 0);
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0002EF00 File Offset: 0x0002D100
		public RenderTargetIdentifier(RenderBuffer buf, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = BuiltinRenderTextureType.BufferPtr;
			this.m_NameID = -1;
			this.m_InstanceID = buf.m_RenderTextureInstanceID;
			this.m_BufferPointer = buf.m_BufferPtr;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0002EF40 File Offset: 0x0002D140
		public static implicit operator RenderTargetIdentifier(BuiltinRenderTextureType type)
		{
			return new RenderTargetIdentifier(type);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0002EF58 File Offset: 0x0002D158
		public static implicit operator RenderTargetIdentifier(string name)
		{
			return new RenderTargetIdentifier(name);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0002EF70 File Offset: 0x0002D170
		public static implicit operator RenderTargetIdentifier(int nameID)
		{
			return new RenderTargetIdentifier(nameID);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0002EF88 File Offset: 0x0002D188
		public static implicit operator RenderTargetIdentifier(Texture tex)
		{
			return new RenderTargetIdentifier(tex);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0002EFA0 File Offset: 0x0002D1A0
		public override string ToString()
		{
			return UnityString.Format("Type {0} NameID {1} InstanceID {2} BufferPointer {3} MipLevel {4} CubeFace {5} DepthSlice {6}", new object[] { this.m_Type, this.m_NameID, this.m_InstanceID, this.m_BufferPointer, this.m_MipLevel, this.m_CubeFace, this.m_DepthSlice });
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0002F024 File Offset: 0x0002D224
		public override int GetHashCode()
		{
			return (this.m_Type.GetHashCode() * 23 + this.m_NameID.GetHashCode()) * 23 + this.m_InstanceID.GetHashCode();
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0002F068 File Offset: 0x0002D268
		public bool Equals(RenderTargetIdentifier rhs)
		{
			return this.m_Type == rhs.m_Type && this.m_NameID == rhs.m_NameID && this.m_InstanceID == rhs.m_InstanceID && this.m_BufferPointer == rhs.m_BufferPointer && this.m_MipLevel == rhs.m_MipLevel && this.m_CubeFace == rhs.m_CubeFace && this.m_DepthSlice == rhs.m_DepthSlice;
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0002F0E4 File Offset: 0x0002D2E4
		public override bool Equals(object obj)
		{
			bool flag = !(obj is RenderTargetIdentifier);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				RenderTargetIdentifier rhs = (RenderTargetIdentifier)obj;
				flag2 = this.Equals(rhs);
			}
			return flag2;
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0002F118 File Offset: 0x0002D318
		public static bool operator ==(RenderTargetIdentifier lhs, RenderTargetIdentifier rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0002F134 File Offset: 0x0002D334
		public static bool operator !=(RenderTargetIdentifier lhs, RenderTargetIdentifier rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0400097C RID: 2428
		public const int AllDepthSlices = -1;

		// Token: 0x0400097D RID: 2429
		private BuiltinRenderTextureType m_Type;

		// Token: 0x0400097E RID: 2430
		private int m_NameID;

		// Token: 0x0400097F RID: 2431
		private int m_InstanceID;

		// Token: 0x04000980 RID: 2432
		private IntPtr m_BufferPointer;

		// Token: 0x04000981 RID: 2433
		private int m_MipLevel;

		// Token: 0x04000982 RID: 2434
		private CubemapFace m_CubeFace;

		// Token: 0x04000983 RID: 2435
		private int m_DepthSlice;
	}
}
