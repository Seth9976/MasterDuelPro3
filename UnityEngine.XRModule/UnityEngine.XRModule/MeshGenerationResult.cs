using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200001E RID: 30
	[RequiredByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	public struct MeshGenerationResult : IEquatable<MeshGenerationResult>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002B6B File Offset: 0x00000D6B
		public readonly MeshId MeshId { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002B73 File Offset: 0x00000D73
		public readonly Mesh Mesh { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002B7B File Offset: 0x00000D7B
		public readonly MeshCollider MeshCollider { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002B83 File Offset: 0x00000D83
		public readonly MeshGenerationStatus Status { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002B8B File Offset: 0x00000D8B
		public readonly MeshVertexAttributes Attributes { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002B93 File Offset: 0x00000D93
		public readonly Vector3 Position { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002B9B File Offset: 0x00000D9B
		public readonly Quaternion Rotation { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002BA3 File Offset: 0x00000DA3
		public readonly Vector3 Scale { get; }

		// Token: 0x06000066 RID: 102 RVA: 0x00002BAC File Offset: 0x00000DAC
		public override bool Equals(object obj)
		{
			bool flag = !(obj is MeshGenerationResult);
			return !flag && this.Equals((MeshGenerationResult)obj);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public bool Equals(MeshGenerationResult other)
		{
			return this.MeshId.Equals(other.MeshId) && this.Mesh.Equals(other.Mesh) && this.MeshCollider.Equals(other.MeshCollider) && this.Status == other.Status && this.Attributes == other.Attributes && this.Position.Equals(other.Position) && this.Rotation.Equals(other.Rotation) && this.Scale.Equals(other.Scale);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002C98 File Offset: 0x00000E98
		public override int GetHashCode()
		{
			return HashCodeHelper.Combine(this.MeshId.GetHashCode(), this.Mesh.GetHashCode(), this.MeshCollider.GetHashCode(), ((int)this.Status).GetHashCode(), ((int)this.Attributes).GetHashCode(), this.Position.GetHashCode(), this.Rotation.GetHashCode(), this.Scale.GetHashCode());
		}
	}
}
