using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x0200014C RID: 332
	internal class MethodBuilderInfo
	{
		// Token: 0x06001085 RID: 4229 RVA: 0x00050BB8 File Offset: 0x0004EDB8
		public MethodBuilderInfo(MethodBuilder methodBuilder, Type[] parameterTypes)
		{
			this.MethodBuilder = methodBuilder;
			this.ParameterTypes = parameterTypes;
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0000A558 File Offset: 0x00008758
		public void Validate(Type returnType, Type[] parameterTypes, MethodAttributes attributes)
		{
		}

		// Token: 0x040007FB RID: 2043
		public readonly MethodBuilder MethodBuilder;

		// Token: 0x040007FC RID: 2044
		public readonly Type[] ParameterTypes;
	}
}
