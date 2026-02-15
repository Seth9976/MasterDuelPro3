using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000642 RID: 1602
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeConstructorInfo : ConstructorInfo, ISerializable
	{
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06003012 RID: 12306 RVA: 0x000B6B48 File Offset: 0x000B4D48
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000B6B50 File Offset: 0x000B4D50
		internal RuntimeModule GetRuntimeModule()
		{
			return RuntimeTypeHandle.GetModule((RuntimeType)this.DeclaringType);
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06003014 RID: 12308 RVA: 0x00033991 File Offset: 0x00031B91
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06003015 RID: 12309 RVA: 0x000B5DFF File Offset: 0x000B3FFF
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000B6B62 File Offset: 0x000B4D62
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Constructor, null);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000B6B92 File Offset: 0x000B4D92
		internal string SerializationToString()
		{
			return this.FormatNameAndSig(true);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000B6B9B File Offset: 0x000B4D9B
		internal void SerializationInvoke(object target, SerializationInfo info, StreamingContext context)
		{
			base.Invoke(target, new object[] { info, context });
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000B6BB8 File Offset: 0x000B4DB8
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MonoMethodInfo.GetMethodImplementationFlags(this.mhandle);
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000B6BC5 File Offset: 0x000B4DC5
		public override ParameterInfo[] GetParameters()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this);
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000B6BC5 File Offset: 0x000B4DC5
		internal override ParameterInfo[] GetParametersInternal()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this);
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000B6BD4 File Offset: 0x000B4DD4
		internal override int GetParametersCount()
		{
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			if (parametersInfo != null)
			{
				return parametersInfo.Length;
			}
			return 0;
		}

		// Token: 0x0600301D RID: 12317
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object InternalInvoke(object obj, object[] parameters, out Exception exc);

		// Token: 0x0600301E RID: 12318 RVA: 0x000B6BF6 File Offset: 0x000B4DF6
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (obj == null)
			{
				if (!base.IsStatic)
				{
					throw new TargetException("Instance constructor requires a target");
				}
			}
			else if (!this.DeclaringType.IsInstanceOfType(obj))
			{
				throw new TargetException("Constructor does not match target type");
			}
			return this.DoInvoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000B6C34 File Offset: 0x000B4E34
		private object DoInvoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			RuntimeMethodInfo.ConvertValues(binder, parameters, parametersInfo, culture, invokeAttr);
			if (obj == null && this.DeclaringType.ContainsGenericParameters)
			{
				string text = "Cannot create an instance of ";
				Type declaringType = this.DeclaringType;
				throw new MemberAccessException(text + ((declaringType != null) ? declaringType.ToString() : null) + " because Type.ContainsGenericParameters is true.");
			}
			if ((invokeAttr & BindingFlags.CreateInstance) != BindingFlags.Default && this.DeclaringType.IsAbstract)
			{
				throw new MemberAccessException(string.Format("Cannot create an instance of {0} because it is an abstract class", this.DeclaringType));
			}
			return this.InternalInvoke(obj, parameters, (invokeAttr & BindingFlags.DoNotWrapExceptions) == BindingFlags.Default);
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000B6CDC File Offset: 0x000B4EDC
		public object InternalInvoke(object obj, object[] parameters, bool wrapExceptions)
		{
			object obj2 = null;
			Exception ex;
			if (wrapExceptions)
			{
				try
				{
					obj2 = this.InternalInvoke(obj, parameters, out ex);
					goto IL_0026;
				}
				catch (OverflowException)
				{
					throw;
				}
				catch (Exception ex2)
				{
					throw new TargetInvocationException(ex2);
				}
			}
			obj2 = this.InternalInvoke(obj, parameters, out ex);
			IL_0026:
			if (ex != null)
			{
				throw ex;
			}
			if (obj != null)
			{
				return null;
			}
			return obj2;
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000B6D38 File Offset: 0x000B4F38
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.DoInvoke(null, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06003022 RID: 12322 RVA: 0x000B6D46 File Offset: 0x000B4F46
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return new RuntimeMethodHandle(this.mhandle);
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x000B6D53 File Offset: 0x000B4F53
		public override MethodAttributes Attributes
		{
			get
			{
				return MonoMethodInfo.GetAttributes(this.mhandle);
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06003024 RID: 12324 RVA: 0x000B6D60 File Offset: 0x000B4F60
		public override CallingConventions CallingConvention
		{
			get
			{
				return MonoMethodInfo.GetCallingConvention(this.mhandle);
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x000B6D6D File Offset: 0x000B4F6D
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06003026 RID: 12326 RVA: 0x000B6D7A File Offset: 0x000B4F7A
		public override Type ReflectedType
		{
			get
			{
				return this.reftype;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06003027 RID: 12327 RVA: 0x000B6D82 File Offset: 0x000B4F82
		public override Type DeclaringType
		{
			get
			{
				return MonoMethodInfo.GetDeclaringType(this.mhandle);
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06003028 RID: 12328 RVA: 0x000B6D8F File Offset: 0x000B4F8F
		public override string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return RuntimeMethodInfo.get_name(this);
			}
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x0003DC2A File Offset: 0x0003BE2A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000B3EED File Offset: 0x000B20ED
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000B3EF6 File Offset: 0x000B20F6
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000B6DA6 File Offset: 0x000B4FA6
		public override string ToString()
		{
			return "Void " + this.FormatNameAndSig(false);
		}

		// Token: 0x0600302D RID: 12333
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int get_core_clr_security_level();

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x000B6DB9 File Offset: 0x000B4FB9
		public override bool IsSecurityCritical
		{
			get
			{
				return this.get_core_clr_security_level() > 0;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600302F RID: 12335 RVA: 0x000B6DC4 File Offset: 0x000B4FC4
		public override int MetadataToken
		{
			get
			{
				return RuntimeConstructorInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06003030 RID: 12336
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimeConstructorInfo method);

		// Token: 0x04001872 RID: 6258
		internal IntPtr mhandle;

		// Token: 0x04001873 RID: 6259
		private string name;

		// Token: 0x04001874 RID: 6260
		private Type reftype;
	}
}
