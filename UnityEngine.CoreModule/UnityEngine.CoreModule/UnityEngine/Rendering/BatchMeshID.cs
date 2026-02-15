using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000372 RID: 882
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	[NativeClass("BatchMeshID")]
	public struct BatchMeshID : IEquatable<BatchMeshID>
	{
		// Token: 0x060018C6 RID: 6342 RVA: 0x000350D0 File Offset: 0x000332D0
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000350F0 File Offset: 0x000332F0
		public override bool Equals(object obj)
		{
			bool flag = obj is BatchMeshID;
			return flag && this.Equals((BatchMeshID)obj);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00035120 File Offset: 0x00033320
		public bool Equals(BatchMeshID other)
		{
			return this.value == other.value;
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x00035140 File Offset: 0x00033340
		public static bool operator ==(BatchMeshID a, BatchMeshID b)
		{
			return a.Equals(b);
		}

		// Token: 0x04000A5B RID: 2651
		public static readonly BatchMeshID Null = new BatchMeshID
		{
			value = 0U
		};

		// Token: 0x04000A5C RID: 2652
		public uint value;
	}
}
