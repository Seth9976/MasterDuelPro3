using System;

namespace System.Reflection
{
	// Token: 0x0200061D RID: 1565
	internal sealed class SignatureByRefType : SignatureHasElementType
	{
		// Token: 0x06002D8E RID: 11662 RVA: 0x000B27C6 File Offset: 0x000B09C6
		internal SignatureByRefType(SignatureType elementType)
			: base(elementType)
		{
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x00033991 File Offset: 0x00031B91
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x0000C091 File Offset: 0x0000A291
		protected sealed override bool IsByRefImpl()
		{
			return true;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x00033991 File Offset: 0x00031B91
		protected sealed override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x00033991 File Offset: 0x00031B91
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06002D93 RID: 11667 RVA: 0x00033991 File Offset: 0x00031B91
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000B27CF File Offset: 0x000B09CF
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException("Must be an array type.");
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06002D95 RID: 11669 RVA: 0x000B27DB File Offset: 0x000B09DB
		protected sealed override string Suffix
		{
			get
			{
				return "&";
			}
		}
	}
}
