using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000586 RID: 1414
	internal struct ComponentType
	{
		// Token: 0x060026D1 RID: 9937 RVA: 0x0009A658 File Offset: 0x00098858
		public static ComponentType Create<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
		{
			return new ComponentType
			{
				Size = UnsafeUtility.SizeOf<T>()
			};
		}

		// Token: 0x040013A7 RID: 5031
		public int Size;
	}
}
