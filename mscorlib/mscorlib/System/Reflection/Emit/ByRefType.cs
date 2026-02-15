using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200065C RID: 1628
	[StructLayout(LayoutKind.Sequential)]
	internal class ByRefType : SymbolType
	{
		// Token: 0x0600317A RID: 12666 RVA: 0x000BA87D File Offset: 0x000B8A7D
		internal ByRefType(Type elementType)
			: base(elementType)
		{
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000BA886 File Offset: 0x000B8A86
		internal override Type InternalResolve()
		{
			return this.m_baseType.InternalResolve().MakeByRefType();
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x0000C091 File Offset: 0x0000A291
		protected override bool IsByRefImpl()
		{
			return true;
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x000BA898 File Offset: 0x000B8A98
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			return elementName + "&";
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000BA8AA File Offset: 0x000B8AAA
		public override Type MakeArrayType()
		{
			throw new ArgumentException("Cannot create an array type of a byref type");
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x000BA8AA File Offset: 0x000B8AAA
		public override Type MakeArrayType(int rank)
		{
			throw new ArgumentException("Cannot create an array type of a byref type");
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x000BA8B6 File Offset: 0x000B8AB6
		public override Type MakeByRefType()
		{
			throw new ArgumentException("Cannot create a byref type of an already byref type");
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x000BA8C2 File Offset: 0x000B8AC2
		public override Type MakePointerType()
		{
			throw new ArgumentException("Cannot create a pointer type of a byref type");
		}
	}
}
