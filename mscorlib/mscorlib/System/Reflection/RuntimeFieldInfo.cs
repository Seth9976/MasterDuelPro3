using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x0200063F RID: 1599
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeFieldInfo : RtFieldInfo, ISerializable
	{
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x00033991 File Offset: 0x00031B91
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x000B5FCD File Offset: 0x000B41CD
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000B5DF2 File Offset: 0x000B3FF2
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return (RuntimeType)this.DeclaringType;
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x000B5DFF File Offset: 0x000B3FFF
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000B5FD5 File Offset: 0x000B41D5
		internal RuntimeModule GetRuntimeModule()
		{
			return this.GetDeclaringTypeInternal().GetRuntimeModule();
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000B5FE2 File Offset: 0x000B41E2
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), MemberTypes.Field);
		}

		// Token: 0x06002FB6 RID: 12214
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal override extern object UnsafeGetValue(object obj);

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000B600C File Offset: 0x000B420C
		internal override void CheckConsistency(object target)
		{
			if ((this.Attributes & FieldAttributes.Static) == FieldAttributes.Static || this.DeclaringType.IsInstanceOfType(target))
			{
				return;
			}
			if (target == null)
			{
				throw new TargetException(Environment.GetResourceString("Non-static field requires a target."));
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Field '{0}' defined on type '{1}' is not a field on the target object which is of type '{2}'."), this.Name, this.DeclaringType, target.GetType()));
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000B6074 File Offset: 0x000B4274
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal override void UnsafeSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			bool flag = false;
			RuntimeFieldHandle.SetValue(this, obj, value, null, this.Attributes, null, ref flag);
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000B6095 File Offset: 0x000B4295
		[DebuggerHidden]
		[DebuggerStepThrough]
		public unsafe override void SetValueDirect(TypedReference obj, object value)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(Environment.GetResourceString("The TypedReference must be initialized."));
			}
			RuntimeFieldHandle.SetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), value, (RuntimeType)this.DeclaringType);
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x000B60D0 File Offset: 0x000B42D0
		public override FieldAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x000B60D8 File Offset: 0x000B42D8
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				return this.fhandle;
			}
		}

		// Token: 0x06002FBC RID: 12220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type ResolveType();

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x000B60E0 File Offset: 0x000B42E0
		public override Type FieldType
		{
			get
			{
				if (this.type == null)
				{
					this.type = this.ResolveType();
				}
				return this.type;
			}
		}

		// Token: 0x06002FBE RID: 12222
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type GetParentType(bool declaring);

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06002FBF RID: 12223 RVA: 0x000B6102 File Offset: 0x000B4302
		public override Type ReflectedType
		{
			get
			{
				return this.GetParentType(false);
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x000B610B File Offset: 0x000B430B
		public override Type DeclaringType
		{
			get
			{
				return this.GetParentType(true);
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x000B6114 File Offset: 0x000B4314
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x0003DC2A File Offset: 0x0003BE2A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000B3EED File Offset: 0x000B20ED
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x000B3EF6 File Offset: 0x000B20F6
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06002FC5 RID: 12229
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal override extern int GetFieldOffset();

		// Token: 0x06002FC6 RID: 12230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetValueInternal(object obj);

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000B611C File Offset: 0x000B431C
		public override object GetValue(object obj)
		{
			if (!base.IsStatic)
			{
				if (obj == null)
				{
					throw new TargetException("Non-static field requires a target");
				}
				if (!this.DeclaringType.IsAssignableFrom(obj.GetType()))
				{
					throw new ArgumentException(string.Format("Field {0} defined on type {1} is not a field on the target object which is of type {2}.", this.Name, this.DeclaringType, obj.GetType()), "obj");
				}
			}
			if (!base.IsLiteral)
			{
				this.CheckGeneric();
			}
			return this.GetValueInternal(obj);
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000B618E File Offset: 0x000B438E
		public override string ToString()
		{
			return string.Format("{0} {1}", this.FieldType, this.name);
		}

		// Token: 0x06002FC9 RID: 12233
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetValueInternal(FieldInfo fi, object obj, object value);

		// Token: 0x06002FCA RID: 12234 RVA: 0x000B61A8 File Offset: 0x000B43A8
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			if (!base.IsStatic)
			{
				if (obj == null)
				{
					throw new TargetException("Non-static field requires a target");
				}
				if (!this.DeclaringType.IsAssignableFrom(obj.GetType()))
				{
					throw new ArgumentException(string.Format("Field {0} defined on type {1} is not a field on the target object which is of type {2}.", this.Name, this.DeclaringType, obj.GetType()), "obj");
				}
			}
			if (base.IsLiteral)
			{
				throw new FieldAccessException("Cannot set a constant field");
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			this.CheckGeneric();
			if (val != null)
			{
				val = ((RuntimeType)this.FieldType).CheckValue(val, binder, culture, invokeAttr);
			}
			RuntimeFieldInfo.SetValueInternal(this, obj, val);
		}

		// Token: 0x06002FCB RID: 12235
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern object GetRawConstantValue();

		// Token: 0x06002FCC RID: 12236 RVA: 0x000B624C File Offset: 0x000B444C
		private void CheckGeneric()
		{
			if (this.DeclaringType.ContainsGenericParameters)
			{
				throw new InvalidOperationException("Late bound operations cannot be performed on fields with types for which Type.ContainsGenericParameters is true.");
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06002FCD RID: 12237 RVA: 0x000B6266 File Offset: 0x000B4466
		public override int MetadataToken
		{
			get
			{
				return RuntimeFieldInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06002FCE RID: 12238
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimeFieldInfo monoField);

		// Token: 0x04001865 RID: 6245
		internal IntPtr klass;

		// Token: 0x04001866 RID: 6246
		internal RuntimeFieldHandle fhandle;

		// Token: 0x04001867 RID: 6247
		private string name;

		// Token: 0x04001868 RID: 6248
		private Type type;

		// Token: 0x04001869 RID: 6249
		private FieldAttributes attrs;
	}
}
