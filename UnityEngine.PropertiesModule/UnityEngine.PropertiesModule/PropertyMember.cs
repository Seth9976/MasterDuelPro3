using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.Properties.Internal;

namespace Unity.Properties
{
	// Token: 0x0200001F RID: 31
	internal readonly struct PropertyMember : IMemberInfo
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003FB4 File Offset: 0x000021B4
		public string Name { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003FBC File Offset: 0x000021BC
		public bool IsReadOnly
		{
			get
			{
				return !this.m_PropertyInfo.CanWrite;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003FCC File Offset: 0x000021CC
		public Type ValueType
		{
			get
			{
				return this.m_PropertyInfo.PropertyType;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003FD9 File Offset: 0x000021D9
		public PropertyMember(PropertyInfo propertyInfo)
		{
			this.m_PropertyInfo = propertyInfo;
			this.Name = ReflectionUtilities.SanitizeMemberName(this.m_PropertyInfo);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003FF4 File Offset: 0x000021F4
		public object GetValue(object obj)
		{
			return this.m_PropertyInfo.GetValue(obj);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004002 File Offset: 0x00002202
		public void SetValue(object obj, object value)
		{
			this.m_PropertyInfo.SetValue(obj, value);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004012 File Offset: 0x00002212
		public IEnumerable<Attribute> GetCustomAttributes()
		{
			return this.m_PropertyInfo.GetCustomAttributes();
		}

		// Token: 0x04000039 RID: 57
		internal readonly PropertyInfo m_PropertyInfo;
	}
}
