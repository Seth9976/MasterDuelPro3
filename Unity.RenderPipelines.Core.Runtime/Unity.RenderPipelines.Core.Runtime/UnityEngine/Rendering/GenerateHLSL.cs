using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000180 RID: 384
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
	public class GenerateHLSL : Attribute
	{
		// Token: 0x06000AD6 RID: 2774 RVA: 0x00027354 File Offset: 0x00025554
		public GenerateHLSL(PackingRules rules = PackingRules.Exact, bool needAccessors = true, bool needSetters = false, bool needParamDebug = false, int paramDefinesStart = 1, bool omitStructDeclaration = false, bool containsPackedFields = false, bool generateCBuffer = false, int constantRegister = -1, [CallerFilePath] string sourcePath = null)
		{
			this.sourcePath = sourcePath;
			this.packingRules = rules;
			this.needAccessors = needAccessors;
			this.needSetters = needSetters;
			this.needParamDebug = needParamDebug;
			this.paramDefinesStart = paramDefinesStart;
			this.omitStructDeclaration = omitStructDeclaration;
			this.containsPackedFields = containsPackedFields;
			this.generateCBuffer = generateCBuffer;
			this.constantRegister = constantRegister;
		}

		// Token: 0x04000771 RID: 1905
		public PackingRules packingRules;

		// Token: 0x04000772 RID: 1906
		public bool containsPackedFields;

		// Token: 0x04000773 RID: 1907
		public bool needAccessors;

		// Token: 0x04000774 RID: 1908
		public bool needSetters;

		// Token: 0x04000775 RID: 1909
		public bool needParamDebug;

		// Token: 0x04000776 RID: 1910
		public int paramDefinesStart;

		// Token: 0x04000777 RID: 1911
		public bool omitStructDeclaration;

		// Token: 0x04000778 RID: 1912
		public bool generateCBuffer;

		// Token: 0x04000779 RID: 1913
		public int constantRegister;

		// Token: 0x0400077A RID: 1914
		public string sourcePath;
	}
}
