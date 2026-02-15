using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	[VisibleToOtherModules]
	internal sealed class NativeClassAttribute : Attribute
	{
		// Token: 0x17000001 RID: 1
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000207B File Offset: 0x0000027B
		private string QualifiedNativeName
		{
			[CompilerGenerated]
			set
			{
				this.<QualifiedNativeName>k__BackingField = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002084 File Offset: 0x00000284
		private string Declaration
		{
			[CompilerGenerated]
			set
			{
				this.<Declaration>k__BackingField = value;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000208D File Offset: 0x0000028D
		public NativeClassAttribute(string qualifiedCppName)
		{
			this.QualifiedNativeName = qualifiedCppName;
			this.Declaration = "class " + qualifiedCppName;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020B1 File Offset: 0x000002B1
		public NativeClassAttribute(string qualifiedCppName, string declaration)
		{
			this.QualifiedNativeName = qualifiedCppName;
			this.Declaration = declaration;
		}
	}
}
