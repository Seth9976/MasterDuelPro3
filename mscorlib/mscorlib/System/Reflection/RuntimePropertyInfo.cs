using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using Mono;

namespace System.Reflection
{
	// Token: 0x02000648 RID: 1608
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimePropertyInfo : PropertyInfo, ISerializable
	{
		// Token: 0x06003071 RID: 12401
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void get_property_info(RuntimePropertyInfo prop, ref MonoPropertyInfo info, PInfo req_info);

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06003072 RID: 12402 RVA: 0x00033991 File Offset: 0x00031B91
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06003073 RID: 12403 RVA: 0x000B7771 File Offset: 0x000B5971
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000B5DF2 File Offset: 0x000B3FF2
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return (RuntimeType)this.DeclaringType;
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06003075 RID: 12405 RVA: 0x000B5DFF File Offset: 0x000B3FFF
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000B7779 File Offset: 0x000B5979
		internal RuntimeModule GetRuntimeModule()
		{
			return this.GetDeclaringTypeInternal().GetRuntimeModule();
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000B7786 File Offset: 0x000B5986
		public override string ToString()
		{
			return this.FormatNameAndSig(false);
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000B7790 File Offset: 0x000B5990
		private string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.PropertyType.FormatTypeName(serialization));
			stringBuilder.Append(" ");
			stringBuilder.Append(this.Name);
			ParameterInfo[] indexParameters = this.GetIndexParameters();
			if (indexParameters.Length != 0)
			{
				stringBuilder.Append(" [");
				RuntimeParameterInfo.FormatParameters(stringBuilder, indexParameters, (CallingConventions)0, serialization);
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000B77FA File Offset: 0x000B59FA
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Property, null);
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000B782B File Offset: 0x000B5A2B
		internal string SerializationToString()
		{
			return this.FormatNameAndSig(true);
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000B7834 File Offset: 0x000B5A34
		private void CachePropertyInfo(PInfo flags)
		{
			if ((this.cached & flags) != flags)
			{
				RuntimePropertyInfo.get_property_info(this, ref this.info, flags);
				this.cached |= flags;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x000B785C File Offset: 0x000B5A5C
		public override bool CanRead
		{
			get
			{
				this.CachePropertyInfo(PInfo.GetMethod);
				return this.info.get_method != null;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600307D RID: 12413 RVA: 0x000B7876 File Offset: 0x000B5A76
		public override bool CanWrite
		{
			get
			{
				this.CachePropertyInfo(PInfo.SetMethod);
				return this.info.set_method != null;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x000B7890 File Offset: 0x000B5A90
		public override Type PropertyType
		{
			get
			{
				this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
				if (this.info.get_method != null)
				{
					return this.info.get_method.ReturnType;
				}
				ParameterInfo[] parametersInternal = this.info.set_method.GetParametersInternal();
				return parametersInternal[parametersInternal.Length - 1].ParameterType;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x0600307F RID: 12415 RVA: 0x000B78E3 File Offset: 0x000B5AE3
		public override Type ReflectedType
		{
			get
			{
				this.CachePropertyInfo(PInfo.ReflectedType);
				return this.info.parent;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x000B78F7 File Offset: 0x000B5AF7
		public override Type DeclaringType
		{
			get
			{
				this.CachePropertyInfo(PInfo.DeclaringType);
				return this.info.declaring_type;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06003081 RID: 12417 RVA: 0x000B790C File Offset: 0x000B5B0C
		public override string Name
		{
			get
			{
				this.CachePropertyInfo(PInfo.Name);
				return this.info.name;
			}
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000B7921 File Offset: 0x000B5B21
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			this.CachePropertyInfo(PInfo.GetMethod);
			if (this.info.get_method != null && (nonPublic || this.info.get_method.IsPublic))
			{
				return this.info.get_method;
			}
			return null;
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000B7960 File Offset: 0x000B5B60
		public override ParameterInfo[] GetIndexParameters()
		{
			this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
			ParameterInfo[] array;
			int num;
			if (this.info.get_method != null)
			{
				array = this.info.get_method.GetParametersInternal();
				num = array.Length;
			}
			else
			{
				if (!(this.info.set_method != null))
				{
					return EmptyArray<ParameterInfo>.Value;
				}
				array = this.info.set_method.GetParametersInternal();
				num = array.Length - 1;
			}
			ParameterInfo[] array2 = new ParameterInfo[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = RuntimeParameterInfo.New(array[i], this);
			}
			return array2;
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x000B79F0 File Offset: 0x000B5BF0
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			this.CachePropertyInfo(PInfo.SetMethod);
			if (this.info.set_method != null && (nonPublic || this.info.set_method.IsPublic))
			{
				return this.info.set_method;
			}
			return null;
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000B7A2E File Offset: 0x000B5C2E
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, false);
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x000B754C File Offset: 0x000B574C
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, false);
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000B7555 File Offset: 0x000B5755
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, false);
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000B7A38 File Offset: 0x000B5C38
		private static object GetterAdapterFrame<T, R>(RuntimePropertyInfo.Getter<T, R> getter, object obj)
		{
			return getter((T)((object)obj));
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000B7A4B File Offset: 0x000B5C4B
		private static object StaticGetterAdapterFrame<R>(RuntimePropertyInfo.StaticGetter<R> getter, object obj)
		{
			return getter();
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000B7A58 File Offset: 0x000B5C58
		private static RuntimePropertyInfo.GetterAdapter CreateGetterDelegate(MethodInfo method)
		{
			Type[] array;
			Type type;
			string text;
			if (method.IsStatic)
			{
				array = new Type[] { method.ReturnType };
				type = typeof(RuntimePropertyInfo.StaticGetter<>);
				text = "StaticGetterAdapterFrame";
			}
			else
			{
				array = new Type[] { method.DeclaringType, method.ReturnType };
				type = typeof(RuntimePropertyInfo.Getter<, >);
				text = "GetterAdapterFrame";
			}
			object obj = Delegate.CreateDelegate(type.MakeGenericType(array), method);
			MethodInfo methodInfo = typeof(RuntimePropertyInfo).GetMethod(text, BindingFlags.Static | BindingFlags.NonPublic);
			methodInfo = methodInfo.MakeGenericMethod(array);
			return (RuntimePropertyInfo.GetterAdapter)Delegate.CreateDelegate(typeof(RuntimePropertyInfo.GetterAdapter), obj, methodInfo, true);
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000B7B00 File Offset: 0x000B5D00
		public override object GetValue(object obj, object[] index)
		{
			if (index == null || index.Length == 0)
			{
				if (this.cached_getter == null)
				{
					MethodInfo getMethod = this.GetGetMethod(true);
					if (getMethod == null)
					{
						throw new ArgumentException("Get Method not found for '" + this.Name + "'");
					}
					if (this.DeclaringType.IsValueType || this.PropertyType.IsByRef || getMethod.ContainsGenericParameters)
					{
						goto IL_0097;
					}
					this.cached_getter = RuntimePropertyInfo.CreateGetterDelegate(getMethod);
					try
					{
						return this.cached_getter(obj);
					}
					catch (Exception ex)
					{
						throw new TargetInvocationException(ex);
					}
				}
				try
				{
					return this.cached_getter(obj);
				}
				catch (Exception ex2)
				{
					throw new TargetInvocationException(ex2);
				}
			}
			IL_0097:
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000B7BD0 File Offset: 0x000B5DD0
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			object obj2 = null;
			MethodInfo getMethod = this.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException("Get Method not found for '" + this.Name + "'");
			}
			try
			{
				if (index == null || index.Length == 0)
				{
					obj2 = getMethod.Invoke(obj, invokeAttr, binder, null, culture);
				}
				else
				{
					obj2 = getMethod.Invoke(obj, invokeAttr, binder, index, culture);
				}
			}
			catch (SecurityException ex)
			{
				throw new TargetInvocationException(ex);
			}
			return obj2;
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000B7C4C File Offset: 0x000B5E4C
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			MethodInfo setMethod = this.GetSetMethod(true);
			if (setMethod == null)
			{
				throw new ArgumentException("Set Method not found for '" + this.Name + "'");
			}
			object[] array;
			if (index == null || index.Length == 0)
			{
				array = new object[] { value };
			}
			else
			{
				int num = index.Length;
				array = new object[num + 1];
				index.CopyTo(array, 0);
				array[num] = value;
			}
			setMethod.Invoke(obj, invokeAttr, binder, array, culture);
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600308E RID: 12430 RVA: 0x000B7CC4 File Offset: 0x000B5EC4
		public override int MetadataToken
		{
			get
			{
				return RuntimePropertyInfo.get_metadata_token(this);
			}
		}

		// Token: 0x0600308F RID: 12431
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimePropertyInfo monoProperty);

		// Token: 0x06003090 RID: 12432
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PropertyInfo internal_from_handle_type(IntPtr event_handle, IntPtr type_handle);

		// Token: 0x06003091 RID: 12433 RVA: 0x000B7CCC File Offset: 0x000B5ECC
		internal static PropertyInfo GetPropertyFromHandle(RuntimePropertyHandle handle, RuntimeTypeHandle reflectedType)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			PropertyInfo propertyInfo = RuntimePropertyInfo.internal_from_handle_type(handle.Value, reflectedType.Value);
			if (propertyInfo == null)
			{
				throw new ArgumentException("The property handle and the type handle are incompatible.");
			}
			return propertyInfo;
		}

		// Token: 0x0400188E RID: 6286
		internal IntPtr klass;

		// Token: 0x0400188F RID: 6287
		internal IntPtr prop;

		// Token: 0x04001890 RID: 6288
		private MonoPropertyInfo info;

		// Token: 0x04001891 RID: 6289
		private PInfo cached;

		// Token: 0x04001892 RID: 6290
		private RuntimePropertyInfo.GetterAdapter cached_getter;

		// Token: 0x02000649 RID: 1609
		// (Invoke) Token: 0x06003094 RID: 12436
		private delegate object GetterAdapter(object _this);

		// Token: 0x0200064A RID: 1610
		// (Invoke) Token: 0x06003096 RID: 12438
		private delegate R Getter<T, R>(T _this);

		// Token: 0x0200064B RID: 1611
		// (Invoke) Token: 0x06003098 RID: 12440
		private delegate R StaticGetter<R>();
	}
}
