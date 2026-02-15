using System;
using System.Text;

namespace System.Reflection
{
	// Token: 0x0200061E RID: 1566
	internal sealed class SignatureConstructedGenericType : SignatureType
	{
		// Token: 0x06002D96 RID: 11670 RVA: 0x000B27E4 File Offset: 0x000B09E4
		internal SignatureConstructedGenericType(Type genericTypeDefinition, Type[] typeArguments)
		{
			if (genericTypeDefinition == null)
			{
				throw new ArgumentNullException("genericTypeDefinition");
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			typeArguments = (Type[])typeArguments.Clone();
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i] == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			this._genericTypeDefinition = genericTypeDefinition;
			this._genericTypeArguments = typeArguments;
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06002D97 RID: 11671 RVA: 0x00033991 File Offset: 0x00031B91
		public sealed override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x00033991 File Offset: 0x00031B91
		protected sealed override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x00033991 File Offset: 0x00031B91
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x00033991 File Offset: 0x00031B91
		protected sealed override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x00033991 File Offset: 0x00031B91
		protected sealed override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06002D9C RID: 11676 RVA: 0x00033991 File Offset: 0x00031B91
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06002D9D RID: 11677 RVA: 0x00033991 File Offset: 0x00031B91
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06002D9E RID: 11678 RVA: 0x0000C091 File Offset: 0x0000A291
		public sealed override bool IsConstructedGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06002D9F RID: 11679 RVA: 0x00033991 File Offset: 0x00031B91
		public sealed override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x00033991 File Offset: 0x00031B91
		public sealed override bool IsGenericMethodParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x000B2858 File Offset: 0x000B0A58
		public sealed override bool ContainsGenericParameters
		{
			get
			{
				for (int i = 0; i < this._genericTypeArguments.Length; i++)
				{
					if (this._genericTypeArguments[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06002DA2 RID: 11682 RVA: 0x000082D2 File Offset: 0x000064D2
		internal sealed override SignatureType ElementType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000B27CF File Offset: 0x000B09CF
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException("Must be an array type.");
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000B288A File Offset: 0x000B0A8A
		public sealed override Type GetGenericTypeDefinition()
		{
			return this._genericTypeDefinition;
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000B2892 File Offset: 0x000B0A92
		public sealed override Type[] GetGenericArguments()
		{
			return this.GenericTypeArguments;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x000B289A File Offset: 0x000B0A9A
		public sealed override Type[] GenericTypeArguments
		{
			get
			{
				return (Type[])this._genericTypeArguments.Clone();
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x000339F3 File Offset: 0x00031BF3
		public sealed override int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException("Method may only be called on a Type for which Type.IsGenericParameter is true.");
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x000B28AC File Offset: 0x000B0AAC
		public sealed override string Name
		{
			get
			{
				return this._genericTypeDefinition.Name;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x000B28B9 File Offset: 0x000B0AB9
		public sealed override string Namespace
		{
			get
			{
				return this._genericTypeDefinition.Namespace;
			}
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000B28C8 File Offset: 0x000B0AC8
		public sealed override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this._genericTypeDefinition.ToString());
			stringBuilder.Append('[');
			for (int i = 0; i < this._genericTypeArguments.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(this._genericTypeArguments[i].ToString());
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x0400178F RID: 6031
		private readonly Type _genericTypeDefinition;

		// Token: 0x04001790 RID: 6032
		private readonly Type[] _genericTypeArguments;
	}
}
