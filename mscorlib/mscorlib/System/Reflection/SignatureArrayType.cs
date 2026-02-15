using System;

namespace System.Reflection
{
	// Token: 0x0200061C RID: 1564
	internal sealed class SignatureArrayType : SignatureHasElementType
	{
		// Token: 0x06002D86 RID: 11654 RVA: 0x000B2757 File Offset: 0x000B0957
		internal SignatureArrayType(SignatureType elementType, int rank, bool isMultiDim)
			: base(elementType)
		{
			this._rank = rank;
			this._isMultiDim = isMultiDim;
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x0000C091 File Offset: 0x0000A291
		protected sealed override bool IsArrayImpl()
		{
			return true;
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x00033991 File Offset: 0x00031B91
		protected sealed override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x00033991 File Offset: 0x00031B91
		protected sealed override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x000B276E File Offset: 0x000B096E
		public sealed override bool IsSZArray
		{
			get
			{
				return !this._isMultiDim;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06002D8B RID: 11659 RVA: 0x000B2779 File Offset: 0x000B0979
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return this._isMultiDim;
			}
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000B2781 File Offset: 0x000B0981
		public sealed override int GetArrayRank()
		{
			return this._rank;
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06002D8D RID: 11661 RVA: 0x000B2789 File Offset: 0x000B0989
		protected sealed override string Suffix
		{
			get
			{
				if (!this._isMultiDim)
				{
					return "[]";
				}
				if (this._rank == 1)
				{
					return "[*]";
				}
				return "[" + new string(',', this._rank - 1) + "]";
			}
		}

		// Token: 0x0400178D RID: 6029
		private readonly int _rank;

		// Token: 0x0400178E RID: 6030
		private readonly bool _isMultiDim;
	}
}
