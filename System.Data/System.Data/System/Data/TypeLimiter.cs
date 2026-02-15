using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace System.Data
{
	// Token: 0x02000020 RID: 32
	internal sealed class TypeLimiter
	{
		// Token: 0x0600030F RID: 783 RVA: 0x0001190F File Offset: 0x0000FB0F
		private TypeLimiter(TypeLimiter.Scope scope)
		{
			this.m_instanceScope = scope;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0001191E File Offset: 0x0000FB1E
		private static bool IsTypeLimitingDisabled
		{
			get
			{
				return global::System.LocalAppContextSwitches.AllowArbitraryTypeInstantiation;
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00011928 File Offset: 0x0000FB28
		[NullableContext(2)]
		public static TypeLimiter Capture()
		{
			TypeLimiter.Scope scope = TypeLimiter.s_activeScope;
			if (scope == null)
			{
				return null;
			}
			return new TypeLimiter(scope);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00011948 File Offset: 0x0000FB48
		[NullableContext(2)]
		public static void EnsureTypeIsAllowed(Type type, TypeLimiter capturedLimiter = null)
		{
			if (type == null)
			{
				return;
			}
			TypeLimiter.Scope scope = ((capturedLimiter != null) ? capturedLimiter.m_instanceScope : null) ?? TypeLimiter.s_activeScope;
			if (scope == null)
			{
				return;
			}
			if (scope.IsAllowedType(type))
			{
				return;
			}
			throw ExceptionBuilder.TypeNotAllowed(type);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00011983 File Offset: 0x0000FB83
		[return: Nullable(2)]
		public static IDisposable EnterRestrictedScope(DataSet dataSet)
		{
			if (TypeLimiter.IsTypeLimitingDisabled)
			{
				return null;
			}
			return TypeLimiter.s_activeScope = new TypeLimiter.Scope(TypeLimiter.s_activeScope, TypeLimiter.GetPreviouslyDeclaredDataTypes(dataSet));
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000119A4 File Offset: 0x0000FBA4
		[return: Nullable(2)]
		public static IDisposable EnterRestrictedScope(DataTable dataTable)
		{
			if (TypeLimiter.IsTypeLimitingDisabled)
			{
				return null;
			}
			return TypeLimiter.s_activeScope = new TypeLimiter.Scope(TypeLimiter.s_activeScope, TypeLimiter.GetPreviouslyDeclaredDataTypes(dataTable));
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000119C5 File Offset: 0x0000FBC5
		private static IEnumerable<Type> GetPreviouslyDeclaredDataTypes(DataTable dataTable)
		{
			if (dataTable == null)
			{
				return Enumerable.Empty<Type>();
			}
			return from DataColumn column in dataTable.Columns
				select column.DataType;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000119FF File Offset: 0x0000FBFF
		private static IEnumerable<Type> GetPreviouslyDeclaredDataTypes(DataSet dataSet)
		{
			if (dataSet == null)
			{
				return Enumerable.Empty<Type>();
			}
			return dataSet.Tables.Cast<DataTable>().SelectMany((DataTable table) => TypeLimiter.GetPreviouslyDeclaredDataTypes(table));
		}

		// Token: 0x040000D3 RID: 211
		[Nullable(2)]
		[ThreadStatic]
		private static TypeLimiter.Scope s_activeScope;

		// Token: 0x040000D4 RID: 212
		private TypeLimiter.Scope m_instanceScope;

		// Token: 0x02000021 RID: 33
		private sealed class Scope : IDisposable
		{
			// Token: 0x06000317 RID: 791 RVA: 0x00011A39 File Offset: 0x0000FC39
			internal Scope([Nullable(2)] TypeLimiter.Scope previousScope, IEnumerable<Type> allowedTypes)
			{
				this.m_previousScope = previousScope;
				this.m_allowedTypes = new HashSet<Type>(allowedTypes.Where((Type type) => type != null));
			}

			// Token: 0x06000318 RID: 792 RVA: 0x00011A78 File Offset: 0x0000FC78
			public void Dispose()
			{
				if (this != TypeLimiter.s_activeScope)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				TypeLimiter.s_activeScope = this.m_previousScope;
			}

			// Token: 0x06000319 RID: 793 RVA: 0x00011AA0 File Offset: 0x0000FCA0
			public bool IsAllowedType(Type type)
			{
				if (TypeLimiter.Scope.IsTypeUnconditionallyAllowed(type))
				{
					return true;
				}
				for (TypeLimiter.Scope scope = this; scope != null; scope = scope.m_previousScope)
				{
					if (scope.m_allowedTypes.Contains(type))
					{
						return true;
					}
				}
				Type[] array = (Type[])AppDomain.CurrentDomain.GetData("System.Data.DataSetDefaultAllowedTypes");
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (type == array[i])
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x0600031A RID: 794 RVA: 0x00011B0C File Offset: 0x0000FD0C
			private static bool IsTypeUnconditionallyAllowed(Type type)
			{
				while (!TypeLimiter.Scope.s_allowedTypes.Contains(type))
				{
					if (type.IsEnum)
					{
						return true;
					}
					if (type.IsSZArray)
					{
						type = type.GetElementType();
					}
					else
					{
						if (!type.IsGenericType || type.IsGenericTypeDefinition || !(type.GetGenericTypeDefinition() == typeof(List<>)))
						{
							return false;
						}
						type = type.GetGenericArguments()[0];
					}
				}
				return true;
			}

			// Token: 0x040000D5 RID: 213
			private static readonly HashSet<Type> s_allowedTypes = new HashSet<Type>
			{
				typeof(bool),
				typeof(char),
				typeof(sbyte),
				typeof(byte),
				typeof(short),
				typeof(ushort),
				typeof(int),
				typeof(uint),
				typeof(long),
				typeof(ulong),
				typeof(float),
				typeof(double),
				typeof(decimal),
				typeof(DateTime),
				typeof(DateTimeOffset),
				typeof(TimeSpan),
				typeof(string),
				typeof(Guid),
				typeof(SqlBinary),
				typeof(SqlBoolean),
				typeof(SqlByte),
				typeof(SqlBytes),
				typeof(SqlChars),
				typeof(SqlDateTime),
				typeof(SqlDecimal),
				typeof(SqlDouble),
				typeof(SqlGuid),
				typeof(SqlInt16),
				typeof(SqlInt32),
				typeof(SqlInt64),
				typeof(SqlMoney),
				typeof(SqlSingle),
				typeof(SqlString),
				typeof(object),
				typeof(Type),
				typeof(BigInteger),
				typeof(Uri),
				typeof(Color),
				typeof(Point),
				typeof(PointF),
				typeof(Rectangle),
				typeof(RectangleF),
				typeof(Size),
				typeof(SizeF)
			};

			// Token: 0x040000D6 RID: 214
			private HashSet<Type> m_allowedTypes;

			// Token: 0x040000D7 RID: 215
			[Nullable(2)]
			private readonly TypeLimiter.Scope m_previousScope;
		}
	}
}
