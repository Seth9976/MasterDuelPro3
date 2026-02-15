using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000577 RID: 1399
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	public sealed class AsyncMethodBuilderAttribute : Attribute
	{
		// Token: 0x06002AC8 RID: 10952 RVA: 0x000AA6BC File Offset: 0x000A88BC
		public AsyncMethodBuilderAttribute(Type builderType)
		{
			this.<BuilderType>k__BackingField = builderType;
		}
	}
}
