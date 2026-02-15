using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x02000179 RID: 377
	internal class FieldModel
	{
		// Token: 0x060011E4 RID: 4580 RVA: 0x00055B4C File Offset: 0x00053D4C
		internal FieldModel(string name, Type fieldType, TypeDesc fieldTypeDesc, bool checkSpecified, bool checkShouldPersist)
			: this(name, fieldType, fieldTypeDesc, checkSpecified, checkShouldPersist, false)
		{
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00055B5C File Offset: 0x00053D5C
		internal FieldModel(string name, Type fieldType, TypeDesc fieldTypeDesc, bool checkSpecified, bool checkShouldPersist, bool readOnly)
		{
			this.fieldTypeDesc = fieldTypeDesc;
			this.name = name;
			this.fieldType = fieldType;
			this.checkSpecified = (checkSpecified ? SpecifiedAccessor.ReadWrite : SpecifiedAccessor.None);
			this.checkShouldPersist = checkShouldPersist;
			this.readOnly = readOnly;
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00055B98 File Offset: 0x00053D98
		internal FieldModel(MemberInfo memberInfo, Type fieldType, TypeDesc fieldTypeDesc)
		{
			this.name = memberInfo.Name;
			this.fieldType = fieldType;
			this.fieldTypeDesc = fieldTypeDesc;
			this.memberInfo = memberInfo;
			this.checkShouldPersistMethodInfo = memberInfo.DeclaringType.GetMethod("ShouldSerialize" + memberInfo.Name, new Type[0]);
			this.checkShouldPersist = this.checkShouldPersistMethodInfo != null;
			FieldInfo field = memberInfo.DeclaringType.GetField(memberInfo.Name + "Specified");
			if (field != null)
			{
				if (field.FieldType != typeof(bool))
				{
					throw new InvalidOperationException(Res.GetString("Member '{0}' of type {1} cannot be serialized.  Members with names ending on 'Specified' suffix have special meaning to the XmlSerializer: they control serialization of optional ValueType members and have to be of type {2}.", new object[]
					{
						field.Name,
						field.FieldType.FullName,
						typeof(bool).FullName
					}));
				}
				this.checkSpecified = (field.IsInitOnly ? SpecifiedAccessor.ReadOnly : SpecifiedAccessor.ReadWrite);
				this.checkSpecifiedMemberInfo = field;
			}
			else
			{
				PropertyInfo property = memberInfo.DeclaringType.GetProperty(memberInfo.Name + "Specified");
				if (property != null)
				{
					if (StructModel.CheckPropertyRead(property))
					{
						this.checkSpecified = (property.CanWrite ? SpecifiedAccessor.ReadWrite : SpecifiedAccessor.ReadOnly);
						this.checkSpecifiedMemberInfo = property;
					}
					if (this.checkSpecified != SpecifiedAccessor.None && property.PropertyType != typeof(bool))
					{
						throw new InvalidOperationException(Res.GetString("Member '{0}' of type {1} cannot be serialized.  Members with names ending on 'Specified' suffix have special meaning to the XmlSerializer: they control serialization of optional ValueType members and have to be of type {2}.", new object[]
						{
							property.Name,
							property.PropertyType.FullName,
							typeof(bool).FullName
						}));
					}
				}
			}
			if (memberInfo is PropertyInfo)
			{
				this.readOnly = !((PropertyInfo)memberInfo).CanWrite;
				this.isProperty = true;
				return;
			}
			if (memberInfo is FieldInfo)
			{
				this.readOnly = ((FieldInfo)memberInfo).IsInitOnly;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00055D7F File Offset: 0x00053F7F
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x00055D87 File Offset: 0x00053F87
		internal Type FieldType
		{
			get
			{
				return this.fieldType;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00055D8F File Offset: 0x00053F8F
		internal TypeDesc FieldTypeDesc
		{
			get
			{
				return this.fieldTypeDesc;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x00055D97 File Offset: 0x00053F97
		internal bool CheckShouldPersist
		{
			get
			{
				return this.checkShouldPersist;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00055D9F File Offset: 0x00053F9F
		internal SpecifiedAccessor CheckSpecified
		{
			get
			{
				return this.checkSpecified;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x00055DA7 File Offset: 0x00053FA7
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00055DAF File Offset: 0x00053FAF
		internal MemberInfo CheckSpecifiedMemberInfo
		{
			get
			{
				return this.checkSpecifiedMemberInfo;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00055DB7 File Offset: 0x00053FB7
		internal MethodInfo CheckShouldPersistMethodInfo
		{
			get
			{
				return this.checkShouldPersistMethodInfo;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00055DBF File Offset: 0x00053FBF
		internal bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00055DC7 File Offset: 0x00053FC7
		internal bool IsProperty
		{
			get
			{
				return this.isProperty;
			}
		}

		// Token: 0x0400088B RID: 2187
		private SpecifiedAccessor checkSpecified;

		// Token: 0x0400088C RID: 2188
		private MemberInfo memberInfo;

		// Token: 0x0400088D RID: 2189
		private MemberInfo checkSpecifiedMemberInfo;

		// Token: 0x0400088E RID: 2190
		private MethodInfo checkShouldPersistMethodInfo;

		// Token: 0x0400088F RID: 2191
		private bool checkShouldPersist;

		// Token: 0x04000890 RID: 2192
		private bool readOnly;

		// Token: 0x04000891 RID: 2193
		private bool isProperty;

		// Token: 0x04000892 RID: 2194
		private Type fieldType;

		// Token: 0x04000893 RID: 2195
		private string name;

		// Token: 0x04000894 RID: 2196
		private TypeDesc fieldTypeDesc;
	}
}
