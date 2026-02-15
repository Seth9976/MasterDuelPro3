using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000371 RID: 881
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	[NativeClass("BatchMaterialID")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct BatchMaterialID : IEquatable<BatchMaterialID>
	{
		// Token: 0x060018C1 RID: 6337 RVA: 0x00035020 File Offset: 0x00033220
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00035040 File Offset: 0x00033240
		public override bool Equals(object obj)
		{
			bool flag = obj is BatchMaterialID;
			return flag && this.Equals((BatchMaterialID)obj);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00035070 File Offset: 0x00033270
		public bool Equals(BatchMaterialID other)
		{
			return this.value == other.value;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00035090 File Offset: 0x00033290
		public static bool operator ==(BatchMaterialID a, BatchMaterialID b)
		{
			return a.Equals(b);
		}

		// Token: 0x04000A59 RID: 2649
		public static readonly BatchMaterialID Null = new BatchMaterialID
		{
			value = 0U
		};

		// Token: 0x04000A5A RID: 2650
		public uint value;
	}
}
