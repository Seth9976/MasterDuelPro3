using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x020004CC RID: 1228
	internal sealed class SerializationFieldInfo : FieldInfo
	{
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x0009D490 File Offset: 0x0009B690
		public override Module Module
		{
			get
			{
				return this.m_field.Module;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x0009D49D File Offset: 0x0009B69D
		public override int MetadataToken
		{
			get
			{
				return this.m_field.MetadataToken;
			}
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x0009D4AA File Offset: 0x0009B6AA
		internal SerializationFieldInfo(RuntimeFieldInfo field, string namePrefix)
		{
			this.m_field = field;
			this.m_serializationName = namePrefix + "+" + this.m_field.Name;
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x0009D4D5 File Offset: 0x0009B6D5
		public override string Name
		{
			get
			{
				return this.m_serializationName;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x0009D4DD File Offset: 0x0009B6DD
		public override Type DeclaringType
		{
			get
			{
				return this.m_field.DeclaringType;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x0009D4EA File Offset: 0x0009B6EA
		public override Type ReflectedType
		{
			get
			{
				return this.m_field.ReflectedType;
			}
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x0009D4F7 File Offset: 0x0009B6F7
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_field.GetCustomAttributes(inherit);
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x0009D505 File Offset: 0x0009B705
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_field.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x0009D514 File Offset: 0x0009B714
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_field.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06002705 RID: 9989 RVA: 0x0009D523 File Offset: 0x0009B723
		public override Type FieldType
		{
			get
			{
				return this.m_field.FieldType;
			}
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x0009D530 File Offset: 0x0009B730
		public override object GetValue(object obj)
		{
			return this.m_field.GetValue(obj);
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x0009D540 File Offset: 0x0009B740
		internal object InternalGetValue(object obj)
		{
			RtFieldInfo field = this.m_field;
			if (field != null)
			{
				field.CheckConsistency(obj);
				return field.UnsafeGetValue(obj);
			}
			return this.m_field.GetValue(obj);
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x0009D578 File Offset: 0x0009B778
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			this.m_field.SetValue(obj, value, invokeAttr, binder, culture);
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x0009D58C File Offset: 0x0009B78C
		internal void InternalSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			RtFieldInfo field = this.m_field;
			if (field != null)
			{
				field.CheckConsistency(obj);
				field.UnsafeSetValue(obj, value, invokeAttr, binder, culture);
				return;
			}
			this.m_field.SetValue(obj, value, invokeAttr, binder, culture);
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x0009D5D0 File Offset: 0x0009B7D0
		internal RuntimeFieldInfo FieldInfo
		{
			get
			{
				return this.m_field;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x0009D5D8 File Offset: 0x0009B7D8
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				return this.m_field.FieldHandle;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x0009D5E5 File Offset: 0x0009B7E5
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_field.Attributes;
			}
		}

		// Token: 0x04001296 RID: 4758
		private RuntimeFieldInfo m_field;

		// Token: 0x04001297 RID: 4759
		private string m_serializationName;
	}
}
