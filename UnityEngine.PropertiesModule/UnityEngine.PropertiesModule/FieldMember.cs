using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.Properties.Internal;

namespace Unity.Properties
{
	// Token: 0x0200001E RID: 30
	internal readonly struct FieldMember : IMemberInfo
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003F4C File Offset: 0x0000214C
		public FieldMember(FieldInfo fieldInfo)
		{
			this.m_FieldInfo = fieldInfo;
			this.Name = ReflectionUtilities.SanitizeMemberName(this.m_FieldInfo);
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003F67 File Offset: 0x00002167
		public string Name { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003F6F File Offset: 0x0000216F
		public bool IsReadOnly
		{
			get
			{
				return this.m_FieldInfo.IsInitOnly;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003F7C File Offset: 0x0000217C
		public Type ValueType
		{
			get
			{
				return this.m_FieldInfo.FieldType;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003F89 File Offset: 0x00002189
		public object GetValue(object obj)
		{
			return this.m_FieldInfo.GetValue(obj);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003F97 File Offset: 0x00002197
		public void SetValue(object obj, object value)
		{
			this.m_FieldInfo.SetValue(obj, value);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003FA7 File Offset: 0x000021A7
		public IEnumerable<Attribute> GetCustomAttributes()
		{
			return this.m_FieldInfo.GetCustomAttributes();
		}

		// Token: 0x04000037 RID: 55
		internal readonly FieldInfo m_FieldInfo;
	}
}
