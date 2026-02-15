using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000012 RID: 18
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
	internal class NativeTypeAttribute : Attribute
	{
		// Token: 0x1700000E RID: 14
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000022D2 File Offset: 0x000004D2
		public string Header
		{
			[CompilerGenerated]
			set
			{
				this.<Header>k__BackingField = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000022DB File Offset: 0x000004DB
		public string IntermediateScriptingStructName
		{
			[CompilerGenerated]
			set
			{
				this.<IntermediateScriptingStructName>k__BackingField = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000022E4 File Offset: 0x000004E4
		public CodegenOptions CodegenOptions
		{
			[CompilerGenerated]
			set
			{
				this.<CodegenOptions>k__BackingField = value;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000022ED File Offset: 0x000004ED
		public NativeTypeAttribute()
		{
			this.CodegenOptions = CodegenOptions.Auto;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000022FF File Offset: 0x000004FF
		public NativeTypeAttribute(CodegenOptions codegenOptions)
		{
			this.CodegenOptions = codegenOptions;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002314 File Offset: 0x00000514
		public NativeTypeAttribute(string header)
		{
			bool flag = header == null;
			if (flag)
			{
				throw new ArgumentNullException("header");
			}
			bool flag2 = header == "";
			if (flag2)
			{
				throw new ArgumentException("header cannot be empty", "header");
			}
			this.CodegenOptions = CodegenOptions.Auto;
			this.Header = header;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000236B File Offset: 0x0000056B
		public NativeTypeAttribute(CodegenOptions codegenOptions, string intermediateStructName)
			: this(codegenOptions)
		{
			this.IntermediateScriptingStructName = intermediateStructName;
		}
	}
}
