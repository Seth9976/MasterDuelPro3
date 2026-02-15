using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200001B RID: 27
	[UsedByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	public struct MeshId : IEquatable<MeshId>
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002A08 File Offset: 0x00000C08
		public override string ToString()
		{
			return string.Format("{0}-{1}", this.m_SubId1.ToString("X16"), this.m_SubId2.ToString("X16"));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A44 File Offset: 0x00000C44
		public override int GetHashCode()
		{
			return this.m_SubId1.GetHashCode() ^ this.m_SubId2.GetHashCode();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A70 File Offset: 0x00000C70
		public override bool Equals(object obj)
		{
			return obj is MeshId && this.Equals((MeshId)obj);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A9C File Offset: 0x00000C9C
		public bool Equals(MeshId other)
		{
			return this.m_SubId1 == other.m_SubId1 && this.m_SubId2 == other.m_SubId2;
		}

		// Token: 0x0400007E RID: 126
		private static MeshId s_InvalidId = default(MeshId);

		// Token: 0x0400007F RID: 127
		private ulong m_SubId1;

		// Token: 0x04000080 RID: 128
		private ulong m_SubId2;
	}
}
