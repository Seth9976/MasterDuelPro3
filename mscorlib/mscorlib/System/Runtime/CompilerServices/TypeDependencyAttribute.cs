using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005BA RID: 1466
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class TypeDependencyAttribute : Attribute
	{
		// Token: 0x06002B80 RID: 11136 RVA: 0x000ABED2 File Offset: 0x000AA0D2
		public TypeDependencyAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.typeName = typeName;
		}

		// Token: 0x04001617 RID: 5655
		private string typeName;
	}
}
