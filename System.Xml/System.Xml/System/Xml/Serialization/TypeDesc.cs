using System;
using System.Xml.Schema;
using System.Xml.Serialization.Advanced;

namespace System.Xml.Serialization
{
	// Token: 0x0200019A RID: 410
	internal class TypeDesc
	{
		// Token: 0x0600133E RID: 4926 RVA: 0x0005E6B4 File Offset: 0x0005C8B4
		internal TypeDesc(string name, string fullName, XmlSchemaType dataType, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags, string formatterName)
		{
			this.name = name.Replace('+', '.');
			this.fullName = fullName.Replace('+', '.');
			this.kind = kind;
			this.baseTypeDesc = baseTypeDesc;
			this.flags = flags;
			this.isXsdType = kind == TypeKind.Primitive;
			if (this.isXsdType)
			{
				this.weight = 1;
			}
			else if (kind == TypeKind.Enum)
			{
				this.weight = 2;
			}
			else if (this.kind == TypeKind.Root)
			{
				this.weight = -1;
			}
			else
			{
				this.weight = ((baseTypeDesc == null) ? 0 : (baseTypeDesc.Weight + 1));
			}
			this.dataType = dataType;
			this.formatterName = formatterName;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0005E75F File Offset: 0x0005C95F
		internal TypeDesc(string name, string fullName, XmlSchemaType dataType, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags)
			: this(name, fullName, dataType, kind, baseTypeDesc, flags, null)
		{
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0005E771 File Offset: 0x0005C971
		internal TypeDesc(string name, string fullName, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags)
			: this(name, fullName, null, kind, baseTypeDesc, flags, null)
		{
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0005E782 File Offset: 0x0005C982
		internal TypeDesc(Type type, bool isXsdType, XmlSchemaType dataType, string formatterName, TypeFlags flags)
			: this(type.Name, type.FullName, dataType, TypeKind.Primitive, null, flags, formatterName)
		{
			this.isXsdType = isXsdType;
			this.type = type;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0005E7AB File Offset: 0x0005C9AB
		internal TypeDesc(Type type, string name, string fullName, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags, TypeDesc arrayElementTypeDesc)
			: this(name, fullName, null, kind, baseTypeDesc, flags, null)
		{
			this.arrayElementTypeDesc = arrayElementTypeDesc;
			this.type = type;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0005E7CC File Offset: 0x0005C9CC
		public override string ToString()
		{
			return this.fullName;
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x0005E7D4 File Offset: 0x0005C9D4
		internal TypeFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0005E7DC File Offset: 0x0005C9DC
		internal bool IsXsdType
		{
			get
			{
				return this.isXsdType;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x0005E7E4 File Offset: 0x0005C9E4
		internal bool IsMappedType
		{
			get
			{
				return this.extendedType != null;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x0005E7EF File Offset: 0x0005C9EF
		internal MappedTypeDesc ExtendedType
		{
			get
			{
				return this.extendedType;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x0005E7F7 File Offset: 0x0005C9F7
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x0005E7CC File Offset: 0x0005C9CC
		internal string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0005E7FF File Offset: 0x0005C9FF
		internal string CSharpName
		{
			get
			{
				if (this.cSharpName == null)
				{
					this.cSharpName = ((this.type == null) ? CodeIdentifier.GetCSharpName(this.fullName) : CodeIdentifier.GetCSharpName(this.type));
				}
				return this.cSharpName;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x0005E83B File Offset: 0x0005CA3B
		internal XmlSchemaType DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x0005E843 File Offset: 0x0005CA43
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x0005E84B File Offset: 0x0005CA4B
		internal string FormatterName
		{
			get
			{
				return this.formatterName;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x0005E853 File Offset: 0x0005CA53
		internal TypeKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x0005E85B File Offset: 0x0005CA5B
		internal bool IsValueType
		{
			get
			{
				return (this.flags & TypeFlags.Reference) == TypeFlags.None;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x0005E868 File Offset: 0x0005CA68
		internal bool CanBeAttributeValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeAttributeValue) > TypeFlags.None;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x0005E875 File Offset: 0x0005CA75
		internal bool XmlEncodingNotRequired
		{
			get
			{
				return (this.flags & TypeFlags.XmlEncodingNotRequired) > TypeFlags.None;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x0005E886 File Offset: 0x0005CA86
		internal bool CanBeElementValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeElementValue) > TypeFlags.None;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x0005E894 File Offset: 0x0005CA94
		internal bool CanBeTextValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeTextValue) > TypeFlags.None;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0005E8A2 File Offset: 0x0005CAA2
		// (set) Token: 0x06001355 RID: 4949 RVA: 0x0005E8B4 File Offset: 0x0005CAB4
		internal bool IsMixed
		{
			get
			{
				return this.isMixed || this.CanBeTextValue;
			}
			set
			{
				this.isMixed = value;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x0005E8BD File Offset: 0x0005CABD
		internal bool IsSpecial
		{
			get
			{
				return (this.flags & TypeFlags.Special) > TypeFlags.None;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x0005E8CA File Offset: 0x0005CACA
		internal bool IsAmbiguousDataType
		{
			get
			{
				return (this.flags & TypeFlags.AmbiguousDataType) > TypeFlags.None;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x0005E8DB File Offset: 0x0005CADB
		internal bool HasCustomFormatter
		{
			get
			{
				return (this.flags & TypeFlags.HasCustomFormatter) > TypeFlags.None;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x0005E8E9 File Offset: 0x0005CAE9
		internal bool HasDefaultSupport
		{
			get
			{
				return (this.flags & TypeFlags.IgnoreDefault) == TypeFlags.None;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x0005E8FA File Offset: 0x0005CAFA
		internal bool HasIsEmpty
		{
			get
			{
				return (this.flags & TypeFlags.HasIsEmpty) > TypeFlags.None;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x0005E90B File Offset: 0x0005CB0B
		internal bool CollapseWhitespace
		{
			get
			{
				return (this.flags & TypeFlags.CollapseWhitespace) > TypeFlags.None;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x0005E91C File Offset: 0x0005CB1C
		internal bool HasDefaultConstructor
		{
			get
			{
				return (this.flags & TypeFlags.HasDefaultConstructor) > TypeFlags.None;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x0005E92D File Offset: 0x0005CB2D
		internal bool IsUnsupported
		{
			get
			{
				return (this.flags & TypeFlags.Unsupported) > TypeFlags.None;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0005E93E File Offset: 0x0005CB3E
		internal bool IsGenericInterface
		{
			get
			{
				return (this.flags & TypeFlags.GenericInterface) > TypeFlags.None;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0005E94F File Offset: 0x0005CB4F
		internal bool IsPrivateImplementation
		{
			get
			{
				return (this.flags & TypeFlags.UsePrivateImplementation) > TypeFlags.None;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0005E960 File Offset: 0x0005CB60
		internal bool CannotNew
		{
			get
			{
				return !this.HasDefaultConstructor || this.ConstructorInaccessible;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x0005E972 File Offset: 0x0005CB72
		internal bool IsAbstract
		{
			get
			{
				return (this.flags & TypeFlags.Abstract) > TypeFlags.None;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x0005E97F File Offset: 0x0005CB7F
		internal bool IsOptionalValue
		{
			get
			{
				return (this.flags & TypeFlags.OptionalValue) > TypeFlags.None;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0005E990 File Offset: 0x0005CB90
		internal bool UseReflection
		{
			get
			{
				return (this.flags & TypeFlags.UseReflection) > TypeFlags.None;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x0005E9A1 File Offset: 0x0005CBA1
		internal bool IsVoid
		{
			get
			{
				return this.kind == TypeKind.Void;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x0005E9AC File Offset: 0x0005CBAC
		internal bool IsClass
		{
			get
			{
				return this.kind == TypeKind.Class;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0005E9B7 File Offset: 0x0005CBB7
		internal bool IsStructLike
		{
			get
			{
				return this.kind == TypeKind.Struct || this.kind == TypeKind.Class;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x0005E9CD File Offset: 0x0005CBCD
		internal bool IsArrayLike
		{
			get
			{
				return this.kind == TypeKind.Array || this.kind == TypeKind.Collection || this.kind == TypeKind.Enumerable;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x0005E9EC File Offset: 0x0005CBEC
		internal bool IsCollection
		{
			get
			{
				return this.kind == TypeKind.Collection;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0005E9F7 File Offset: 0x0005CBF7
		internal bool IsEnumerable
		{
			get
			{
				return this.kind == TypeKind.Enumerable;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0005EA02 File Offset: 0x0005CC02
		internal bool IsArray
		{
			get
			{
				return this.kind == TypeKind.Array;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0005EA0D File Offset: 0x0005CC0D
		internal bool IsPrimitive
		{
			get
			{
				return this.kind == TypeKind.Primitive;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x0005EA18 File Offset: 0x0005CC18
		internal bool IsEnum
		{
			get
			{
				return this.kind == TypeKind.Enum;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0005EA23 File Offset: 0x0005CC23
		internal bool IsNullable
		{
			get
			{
				return !this.IsValueType;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x0005EA2E File Offset: 0x0005CC2E
		internal bool IsRoot
		{
			get
			{
				return this.kind == TypeKind.Root;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0005EA39 File Offset: 0x0005CC39
		internal bool ConstructorInaccessible
		{
			get
			{
				return (this.flags & TypeFlags.CtorInaccessible) > TypeFlags.None;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x0005EA4A File Offset: 0x0005CC4A
		// (set) Token: 0x06001371 RID: 4977 RVA: 0x0005EA52 File Offset: 0x0005CC52
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0005EA5C File Offset: 0x0005CC5C
		internal TypeDesc GetNullableTypeDesc(Type type)
		{
			if (this.IsOptionalValue)
			{
				return this;
			}
			if (this.nullableTypeDesc == null)
			{
				this.nullableTypeDesc = new TypeDesc("NullableOf" + this.name, "System.Nullable`1[" + this.fullName + "]", null, TypeKind.Struct, this, this.flags | TypeFlags.OptionalValue, this.formatterName);
				this.nullableTypeDesc.type = type;
			}
			return this.nullableTypeDesc;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0005EAD4 File Offset: 0x0005CCD4
		internal void CheckSupported()
		{
			if (!this.IsUnsupported)
			{
				if (this.baseTypeDesc != null)
				{
					this.baseTypeDesc.CheckSupported();
				}
				if (this.arrayElementTypeDesc != null)
				{
					this.arrayElementTypeDesc.CheckSupported();
				}
				return;
			}
			if (this.Exception != null)
			{
				throw this.Exception;
			}
			throw new NotSupportedException(Res.GetString("{0} is an unsupported type. Please use [XmlIgnore] attribute to exclude members of this type from serialization graph.", new object[] { this.FullName }));
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0005EB40 File Offset: 0x0005CD40
		internal void CheckNeedConstructor()
		{
			if (!this.IsValueType && !this.IsAbstract && !this.HasDefaultConstructor)
			{
				this.flags |= TypeFlags.Unsupported;
				this.exception = new InvalidOperationException(Res.GetString("{0} cannot be serialized because it does not have a parameterless constructor.", new object[] { this.FullName }));
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x0005EB9B File Offset: 0x0005CD9B
		internal string ArrayLengthName
		{
			get
			{
				if (this.kind != TypeKind.Array)
				{
					return "Count";
				}
				return "Length";
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x0005EBB1 File Offset: 0x0005CDB1
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x0005EBB9 File Offset: 0x0005CDB9
		internal TypeDesc ArrayElementTypeDesc
		{
			get
			{
				return this.arrayElementTypeDesc;
			}
			set
			{
				this.arrayElementTypeDesc = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x0005EBC2 File Offset: 0x0005CDC2
		internal int Weight
		{
			get
			{
				return this.weight;
			}
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0005EBCC File Offset: 0x0005CDCC
		internal TypeDesc CreateArrayTypeDesc()
		{
			if (this.arrayTypeDesc == null)
			{
				this.arrayTypeDesc = new TypeDesc(null, this.name + "[]", this.fullName + "[]", TypeKind.Array, null, TypeFlags.Reference | (this.flags & TypeFlags.UseReflection), this);
			}
			return this.arrayTypeDesc;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0005EC24 File Offset: 0x0005CE24
		internal TypeDesc CreateMappedTypeDesc(MappedTypeDesc extension)
		{
			return new TypeDesc(extension.Name, extension.Name, null, this.kind, this.baseTypeDesc, this.flags, null)
			{
				isXsdType = this.isXsdType,
				isMixed = this.isMixed,
				extendedType = extension,
				dataType = this.dataType
			};
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0005EC81 File Offset: 0x0005CE81
		// (set) Token: 0x0600137C RID: 4988 RVA: 0x0005EC89 File Offset: 0x0005CE89
		internal TypeDesc BaseTypeDesc
		{
			get
			{
				return this.baseTypeDesc;
			}
			set
			{
				this.baseTypeDesc = value;
				this.weight = ((this.baseTypeDesc == null) ? 0 : (this.baseTypeDesc.Weight + 1));
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0005ECB0 File Offset: 0x0005CEB0
		internal bool IsDerivedFrom(TypeDesc baseTypeDesc)
		{
			for (TypeDesc typeDesc = this; typeDesc != null; typeDesc = typeDesc.BaseTypeDesc)
			{
				if (typeDesc == baseTypeDesc)
				{
					return true;
				}
			}
			return baseTypeDesc.IsRoot;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0005ECD8 File Offset: 0x0005CED8
		internal static TypeDesc FindCommonBaseTypeDesc(TypeDesc[] typeDescs)
		{
			if (typeDescs.Length == 0)
			{
				return null;
			}
			TypeDesc typeDesc = null;
			int num = int.MaxValue;
			for (int i = 0; i < typeDescs.Length; i++)
			{
				int num2 = typeDescs[i].Weight;
				if (num2 < num)
				{
					num = num2;
					typeDesc = typeDescs[i];
				}
			}
			while (typeDesc != null)
			{
				int num3 = 0;
				while (num3 < typeDescs.Length && typeDescs[num3].IsDerivedFrom(typeDesc))
				{
					num3++;
				}
				if (num3 == typeDescs.Length)
				{
					break;
				}
				typeDesc = typeDesc.BaseTypeDesc;
			}
			return typeDesc;
		}

		// Token: 0x04000910 RID: 2320
		private string name;

		// Token: 0x04000911 RID: 2321
		private string fullName;

		// Token: 0x04000912 RID: 2322
		private string cSharpName;

		// Token: 0x04000913 RID: 2323
		private TypeDesc arrayElementTypeDesc;

		// Token: 0x04000914 RID: 2324
		private TypeDesc arrayTypeDesc;

		// Token: 0x04000915 RID: 2325
		private TypeDesc nullableTypeDesc;

		// Token: 0x04000916 RID: 2326
		private TypeKind kind;

		// Token: 0x04000917 RID: 2327
		private XmlSchemaType dataType;

		// Token: 0x04000918 RID: 2328
		private Type type;

		// Token: 0x04000919 RID: 2329
		private TypeDesc baseTypeDesc;

		// Token: 0x0400091A RID: 2330
		private TypeFlags flags;

		// Token: 0x0400091B RID: 2331
		private string formatterName;

		// Token: 0x0400091C RID: 2332
		private bool isXsdType;

		// Token: 0x0400091D RID: 2333
		private bool isMixed;

		// Token: 0x0400091E RID: 2334
		private MappedTypeDesc extendedType;

		// Token: 0x0400091F RID: 2335
		private int weight;

		// Token: 0x04000920 RID: 2336
		private Exception exception;
	}
}
