using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000182 RID: 386
	[AttributeUsage(AttributeTargets.Field)]
	public class HLSLArray : Attribute
	{
		// Token: 0x06000AD9 RID: 2777 RVA: 0x00027437 File Offset: 0x00025637
		public HLSLArray(int arraySize, Type elementType)
		{
			this.arraySize = arraySize;
			this.elementType = elementType;
		}

		// Token: 0x04000781 RID: 1921
		public int arraySize;

		// Token: 0x04000782 RID: 1922
		public Type elementType;
	}
}
