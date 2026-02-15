using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200014D RID: 333
	internal class CodeGeneratorConversionException : Exception
	{
		// Token: 0x06001087 RID: 4231 RVA: 0x00050BCE File Offset: 0x0004EDCE
		public CodeGeneratorConversionException(Type sourceType, Type targetType, bool isAddress, string reason)
		{
			this.sourceType = sourceType;
			this.targetType = targetType;
			this.isAddress = isAddress;
			this.reason = reason;
		}

		// Token: 0x040007FD RID: 2045
		private Type sourceType;

		// Token: 0x040007FE RID: 2046
		private Type targetType;

		// Token: 0x040007FF RID: 2047
		private bool isAddress;

		// Token: 0x04000800 RID: 2048
		private string reason;
	}
}
