using System;

namespace System.Reflection
{
	// Token: 0x0200061F RID: 1567
	internal abstract class SignatureHasElementType : SignatureType
	{
		// Token: 0x06002DAB RID: 11691 RVA: 0x000B2938 File Offset: 0x000B0B38
		protected SignatureHasElementType(SignatureType elementType)
		{
			this._elementType = elementType;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06002DAC RID: 11692 RVA: 0x00033991 File Offset: 0x00031B91
		public sealed override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x0000C091 File Offset: 0x0000A291
		protected sealed override bool HasElementTypeImpl()
		{
			return true;
		}

		// Token: 0x06002DAE RID: 11694
		protected abstract override bool IsArrayImpl();

		// Token: 0x06002DAF RID: 11695
		protected abstract override bool IsByRefImpl();

		// Token: 0x06002DB0 RID: 11696
		protected abstract override bool IsPointerImpl();

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06002DB1 RID: 11697
		public abstract override bool IsSZArray { get; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06002DB2 RID: 11698
		public abstract override bool IsVariableBoundArray { get; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x00033991 File Offset: 0x00031B91
		public sealed override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x00033991 File Offset: 0x00031B91
		public sealed override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x00033991 File Offset: 0x00031B91
		public sealed override bool IsGenericMethodParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x000B2947 File Offset: 0x000B0B47
		public sealed override bool ContainsGenericParameters
		{
			get
			{
				return this._elementType.ContainsGenericParameters;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x000B2954 File Offset: 0x000B0B54
		internal sealed override SignatureType ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x06002DB8 RID: 11704
		public abstract override int GetArrayRank();

		// Token: 0x06002DB9 RID: 11705 RVA: 0x000B295C File Offset: 0x000B0B5C
		public sealed override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException("This operation is only valid on generic types.");
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x000B2968 File Offset: 0x000B0B68
		public sealed override Type[] GetGenericArguments()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x000B2968 File Offset: 0x000B0B68
		public sealed override Type[] GenericTypeArguments
		{
			get
			{
				return Array.Empty<Type>();
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x000339F3 File Offset: 0x00031BF3
		public sealed override int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException("Method may only be called on a Type for which Type.IsGenericParameter is true.");
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06002DBD RID: 11709 RVA: 0x000B296F File Offset: 0x000B0B6F
		public sealed override string Name
		{
			get
			{
				return this._elementType.Name + this.Suffix;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x000B2987 File Offset: 0x000B0B87
		public sealed override string Namespace
		{
			get
			{
				return this._elementType.Namespace;
			}
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000B2994 File Offset: 0x000B0B94
		public sealed override string ToString()
		{
			return this._elementType.ToString() + this.Suffix;
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06002DC0 RID: 11712
		protected abstract string Suffix { get; }

		// Token: 0x04001791 RID: 6033
		private readonly SignatureType _elementType;
	}
}
