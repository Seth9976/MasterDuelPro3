using System;

namespace System.Reflection
{
	// Token: 0x02000620 RID: 1568
	internal sealed class SignaturePointerType : SignatureHasElementType
	{
		// Token: 0x06002DC1 RID: 11713 RVA: 0x000B27C6 File Offset: 0x000B09C6
		internal SignaturePointerType(SignatureType elementType)
			: base(elementType)
		{
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x00033991 File Offset: 0x00031B91
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x00033991 File Offset: 0x00031B91
		protected sealed override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x0000C091 File Offset: 0x0000A291
		protected sealed override bool IsPointerImpl()
		{
			return true;
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06002DC5 RID: 11717 RVA: 0x00033991 File Offset: 0x00031B91
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x00033991 File Offset: 0x00031B91
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000B27CF File Offset: 0x000B09CF
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException("Must be an array type.");
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x000B29AC File Offset: 0x000B0BAC
		protected sealed override string Suffix
		{
			get
			{
				return "*";
			}
		}
	}
}
