using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x0200000A RID: 10
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property)]
	internal class NativeConditionalAttribute : Attribute
	{
		// Token: 0x17000003 RID: 3
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020EF File Offset: 0x000002EF
		public string Condition
		{
			[CompilerGenerated]
			set
			{
				this.<Condition>k__BackingField = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020F8 File Offset: 0x000002F8
		public string StubReturnStatement
		{
			[CompilerGenerated]
			set
			{
				this.<StubReturnStatement>k__BackingField = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002101 File Offset: 0x00000301
		public bool Enabled
		{
			[CompilerGenerated]
			set
			{
				this.<Enabled>k__BackingField = value;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000210A File Offset: 0x0000030A
		public NativeConditionalAttribute(string condition)
		{
			this.Condition = condition;
			this.Enabled = true;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002124 File Offset: 0x00000324
		public NativeConditionalAttribute(string condition, string stubReturnStatement)
			: this(condition)
		{
			this.StubReturnStatement = stubReturnStatement;
		}
	}
}
