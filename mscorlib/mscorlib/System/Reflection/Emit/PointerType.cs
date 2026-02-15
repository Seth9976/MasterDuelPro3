using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200065D RID: 1629
	[StructLayout(LayoutKind.Sequential)]
	internal class PointerType : SymbolType
	{
		// Token: 0x06003182 RID: 12674 RVA: 0x000BA87D File Offset: 0x000B8A7D
		internal PointerType(Type elementType)
			: base(elementType)
		{
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000BA8CE File Offset: 0x000B8ACE
		internal override Type InternalResolve()
		{
			return this.m_baseType.InternalResolve().MakePointerType();
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x0000C091 File Offset: 0x0000A291
		protected override bool IsPointerImpl()
		{
			return true;
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000BA8E0 File Offset: 0x000B8AE0
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			return elementName + "*";
		}
	}
}
