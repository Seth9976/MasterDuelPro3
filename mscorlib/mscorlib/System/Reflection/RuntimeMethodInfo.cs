using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x02000641 RID: 1601
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeMethodInfo : MethodInfo, ISerializable
	{
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x00033991 File Offset: 0x00031B91
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06002FDD RID: 12253 RVA: 0x000B62F1 File Offset: 0x000B44F1
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x000B5DFF File Offset: 0x000B3FFF
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000B62FC File Offset: 0x000B44FC
		internal override string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Name);
			TypeNameFormatFlags typeNameFormatFlags = (serialization ? TypeNameFormatFlags.FormatSerialization : TypeNameFormatFlags.FormatBasic);
			if (this.IsGenericMethod)
			{
				stringBuilder.Append(RuntimeMethodHandle.ConstructInstantiation(this, typeNameFormatFlags));
			}
			stringBuilder.Append("(");
			RuntimeParameterInfo.FormatParameters(stringBuilder, this.GetParametersNoCopy(), this.CallingConvention, serialization);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000B6368 File Offset: 0x000B4568
		public override Delegate CreateDelegate(Type delegateType)
		{
			return Delegate.CreateDelegate(delegateType, this);
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000B6371 File Offset: 0x000B4571
		public override Delegate CreateDelegate(Type delegateType, object target)
		{
			return Delegate.CreateDelegate(delegateType, target, this);
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000B637B File Offset: 0x000B457B
		public override string ToString()
		{
			return this.ReturnType.FormatTypeName() + " " + this.FormatNameAndSig(false);
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000B6399 File Offset: 0x000B4599
		internal RuntimeModule GetRuntimeModule()
		{
			return ((RuntimeType)this.DeclaringType).GetRuntimeModule();
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000B63AC File Offset: 0x000B45AC
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Method, (this.IsGenericMethod & !this.IsGenericMethodDefinition) ? this.GetGenericArguments() : null);
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000B6401 File Offset: 0x000B4601
		internal string SerializationToString()
		{
			return this.ReturnType.FormatTypeName(true) + " " + this.FormatNameAndSig(true);
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000B6420 File Offset: 0x000B4620
		internal static MethodBase GetMethodFromHandleNoGenericCheck(RuntimeMethodHandle handle)
		{
			return RuntimeMethodInfo.GetMethodFromHandleInternalType_native(handle.Value, IntPtr.Zero, false);
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000B6434 File Offset: 0x000B4634
		internal static MethodBase GetMethodFromHandleNoGenericCheck(RuntimeMethodHandle handle, RuntimeTypeHandle reflectedType)
		{
			return RuntimeMethodInfo.GetMethodFromHandleInternalType_native(handle.Value, reflectedType.Value, false);
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000B644A File Offset: 0x000B464A
		internal static MethodBase GetMethodFromHandleInternalType(IntPtr method_handle, IntPtr type_handle)
		{
			return RuntimeMethodInfo.GetMethodFromHandleInternalType_native(method_handle, type_handle, true);
		}

		// Token: 0x06002FE9 RID: 12265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MethodBase GetMethodFromHandleInternalType_native(IntPtr method_handle, IntPtr type_handle, bool genericCheck);

		// Token: 0x06002FEA RID: 12266 RVA: 0x000B6454 File Offset: 0x000B4654
		internal RuntimeMethodInfo()
		{
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000B645C File Offset: 0x000B465C
		internal RuntimeMethodInfo(RuntimeMethodHandle mhandle)
		{
			this.mhandle = mhandle.Value;
		}

		// Token: 0x06002FEC RID: 12268
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string get_name(MethodBase method);

		// Token: 0x06002FED RID: 12269
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodInfo get_base_method(RuntimeMethodInfo method, bool definition);

		// Token: 0x06002FEE RID: 12270
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimeMethodInfo method);

		// Token: 0x06002FEF RID: 12271 RVA: 0x000B6471 File Offset: 0x000B4671
		public override MethodInfo GetBaseDefinition()
		{
			return RuntimeMethodInfo.get_base_method(this, true);
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000B647A File Offset: 0x000B467A
		internal MethodInfo GetBaseMethod()
		{
			return RuntimeMethodInfo.get_base_method(this, false);
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06002FF1 RID: 12273 RVA: 0x000B6483 File Offset: 0x000B4683
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return MonoMethodInfo.GetReturnParameterInfo(this);
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06002FF2 RID: 12274 RVA: 0x000B648B File Offset: 0x000B468B
		public override Type ReturnType
		{
			get
			{
				return MonoMethodInfo.GetReturnType(this.mhandle);
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06002FF3 RID: 12275 RVA: 0x000B6498 File Offset: 0x000B4698
		public override int MetadataToken
		{
			get
			{
				return RuntimeMethodInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000B64A0 File Offset: 0x000B46A0
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MonoMethodInfo.GetMethodImplementationFlags(this.mhandle);
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000B64B0 File Offset: 0x000B46B0
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			if (parametersInfo.Length == 0)
			{
				return parametersInfo;
			}
			ParameterInfo[] array = new ParameterInfo[parametersInfo.Length];
			Array.FastCopy(parametersInfo, 0, array, 0, parametersInfo.Length);
			return array;
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000B64E7 File Offset: 0x000B46E7
		internal override ParameterInfo[] GetParametersInternal()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000B64F5 File Offset: 0x000B46F5
		internal override int GetParametersCount()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this).Length;
		}

		// Token: 0x06002FF8 RID: 12280
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object InternalInvoke(object obj, object[] parameters, out Exception exc);

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000B6508 File Offset: 0x000B4708
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (!base.IsStatic && !this.DeclaringType.IsInstanceOfType(obj))
			{
				if (obj == null)
				{
					throw new TargetException("Non-static method requires a target.");
				}
				throw new TargetException("Object does not match target type.");
			}
			else
			{
				if (binder == null)
				{
					binder = Type.DefaultBinder;
				}
				ParameterInfo[] parametersInternal = this.GetParametersInternal();
				RuntimeMethodInfo.ConvertValues(binder, parameters, parametersInternal, culture, invokeAttr);
				if (this.ContainsGenericParameters)
				{
					throw new InvalidOperationException("Late bound operations cannot be performed on types or methods for which ContainsGenericParameters is true.");
				}
				object obj2 = null;
				Exception ex;
				if ((invokeAttr & BindingFlags.DoNotWrapExceptions) == BindingFlags.Default)
				{
					try
					{
						obj2 = this.InternalInvoke(obj, parameters, out ex);
						goto IL_0090;
					}
					catch (ThreadAbortException)
					{
						throw;
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
				IL_0090:
				if (ex != null)
				{
					throw ex;
				}
				return obj2;
			}
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000B65D4 File Offset: 0x000B47D4
		internal static void ConvertValues(Binder binder, object[] args, ParameterInfo[] pinfo, CultureInfo culture, BindingFlags invokeAttr)
		{
			if (args == null)
			{
				if (pinfo.Length == 0)
				{
					return;
				}
				throw new TargetParameterCountException();
			}
			else
			{
				if (pinfo.Length != args.Length)
				{
					throw new TargetParameterCountException();
				}
				for (int i = 0; i < args.Length; i++)
				{
					object obj = args[i];
					ParameterInfo parameterInfo = pinfo[i];
					if (obj == Type.Missing)
					{
						if (parameterInfo.DefaultValue == DBNull.Value)
						{
							throw new ArgumentException(Environment.GetResourceString("Missing parameter does not have a default value."), "parameters");
						}
						args[i] = parameterInfo.DefaultValue;
					}
					else
					{
						RuntimeType runtimeType = (RuntimeType)parameterInfo.ParameterType;
						args[i] = runtimeType.CheckValue(obj, binder, culture, invokeAttr);
					}
				}
				return;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06002FFB RID: 12283 RVA: 0x000B6662 File Offset: 0x000B4862
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return new RuntimeMethodHandle(this.mhandle);
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002FFC RID: 12284 RVA: 0x000B666F File Offset: 0x000B486F
		public override MethodAttributes Attributes
		{
			get
			{
				return MonoMethodInfo.GetAttributes(this.mhandle);
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002FFD RID: 12285 RVA: 0x000B667C File Offset: 0x000B487C
		public override CallingConventions CallingConvention
		{
			get
			{
				return MonoMethodInfo.GetCallingConvention(this.mhandle);
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06002FFE RID: 12286 RVA: 0x000B6689 File Offset: 0x000B4889
		public override Type ReflectedType
		{
			get
			{
				return this.reftype;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06002FFF RID: 12287 RVA: 0x000B6691 File Offset: 0x000B4891
		public override Type DeclaringType
		{
			get
			{
				return MonoMethodInfo.GetDeclaringType(this.mhandle);
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06003000 RID: 12288 RVA: 0x000B669E File Offset: 0x000B489E
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

		// Token: 0x06003001 RID: 12289 RVA: 0x0003DC2A File Offset: 0x0003BE2A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000B3EED File Offset: 0x000B20ED
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000B3EF6 File Offset: 0x000B20F6
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06003004 RID: 12292
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetPInvoke(out PInvokeAttributes flags, out string entryPoint, out string dllName);

		// Token: 0x06003005 RID: 12293 RVA: 0x000B66B8 File Offset: 0x000B48B8
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			MonoMethodInfo methodInfo = MonoMethodInfo.GetMethodInfo(this.mhandle);
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				num++;
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				array[num++] = new PreserveSigAttribute();
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				array[num++] = DllImportAttribute.GetCustomAttribute(this);
			}
			return array;
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x000B673C File Offset: 0x000B493C
		internal CustomAttributeData[] GetPseudoCustomAttributesData()
		{
			int num = 0;
			MonoMethodInfo methodInfo = MonoMethodInfo.GetMethodInfo(this.mhandle);
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				num++;
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			CustomAttributeData[] array = new CustomAttributeData[num];
			num = 0;
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				array[num++] = new CustomAttributeData(typeof(PreserveSigAttribute).GetConstructor(Type.EmptyTypes));
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				array[num++] = this.GetDllImportAttributeData();
			}
			return array;
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x000B67D4 File Offset: 0x000B49D4
		private CustomAttributeData GetDllImportAttributeData()
		{
			if ((this.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
			{
				return null;
			}
			string text = null;
			PInvokeAttributes pinvokeAttributes = PInvokeAttributes.CharSetNotSpec;
			string text2;
			this.GetPInvoke(out pinvokeAttributes, out text2, out text);
			CharSet charSet;
			switch (pinvokeAttributes & PInvokeAttributes.CharSetMask)
			{
			case PInvokeAttributes.CharSetNotSpec:
				charSet = CharSet.None;
				goto IL_005C;
			case PInvokeAttributes.CharSetAnsi:
				charSet = CharSet.Ansi;
				goto IL_005C;
			case PInvokeAttributes.CharSetUnicode:
				charSet = CharSet.Unicode;
				goto IL_005C;
			case PInvokeAttributes.CharSetMask:
				charSet = CharSet.Auto;
				goto IL_005C;
			}
			charSet = CharSet.None;
			IL_005C:
			PInvokeAttributes pinvokeAttributes2 = pinvokeAttributes & PInvokeAttributes.CallConvMask;
			CallingConvention callingConvention;
			if (pinvokeAttributes2 <= PInvokeAttributes.CallConvCdecl)
			{
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvWinapi)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.Winapi;
					goto IL_00BB;
				}
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvCdecl)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl;
					goto IL_00BB;
				}
			}
			else
			{
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvStdcall)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.StdCall;
					goto IL_00BB;
				}
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvThiscall)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.ThisCall;
					goto IL_00BB;
				}
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvFastcall)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.FastCall;
					goto IL_00BB;
				}
			}
			callingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl;
			IL_00BB:
			bool flag = (pinvokeAttributes & PInvokeAttributes.NoMangle) > PInvokeAttributes.CharSetNotSpec;
			bool flag2 = (pinvokeAttributes & PInvokeAttributes.SupportsLastError) > PInvokeAttributes.CharSetNotSpec;
			bool flag3 = (pinvokeAttributes & PInvokeAttributes.BestFitMask) == PInvokeAttributes.BestFitEnabled;
			bool flag4 = (pinvokeAttributes & PInvokeAttributes.ThrowOnUnmappableCharMask) == PInvokeAttributes.ThrowOnUnmappableCharEnabled;
			bool flag5 = (this.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
			CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(typeof(string), text)
			};
			Type typeFromHandle = typeof(DllImportAttribute);
			CustomAttributeNamedArgument[] array2 = new CustomAttributeNamedArgument[]
			{
				new CustomAttributeNamedArgument(typeFromHandle.GetField("EntryPoint"), text2),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CharSet"), charSet),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ExactSpelling"), flag),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("SetLastError"), flag2),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("PreserveSig"), flag5),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CallingConvention"), callingConvention),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("BestFitMapping"), flag3),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ThrowOnUnmappableChar"), flag4)
			};
			return new CustomAttributeData(typeFromHandle.GetConstructor(new Type[] { typeof(string) }), array, array2);
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x000B6A20 File Offset: 0x000B4C20
		public override MethodInfo MakeGenericMethod(params Type[] methodInstantiation)
		{
			if (methodInstantiation == null)
			{
				throw new ArgumentNullException("methodInstantiation");
			}
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException("not a generic method definition");
			}
			if (this.GetGenericArguments().Length != methodInstantiation.Length)
			{
				throw new ArgumentException("Incorrect length");
			}
			bool flag = false;
			foreach (Type type in methodInstantiation)
			{
				if (type == null)
				{
					throw new ArgumentNullException();
				}
				if (!(type is RuntimeType))
				{
					flag = true;
				}
			}
			if (flag)
			{
				if (RuntimeFeature.IsDynamicCodeSupported)
				{
					return new MethodOnTypeBuilderInst(this, methodInstantiation);
				}
				throw new NotSupportedException("User types are not supported under full aot");
			}
			else
			{
				MethodInfo methodInfo = this.MakeGenericMethod_impl(methodInstantiation);
				if (methodInfo == null)
				{
					throw new ArgumentException(string.Format("The method has {0} generic parameter(s) but {1} generic argument(s) were provided.", this.GetGenericArguments().Length, methodInstantiation.Length));
				}
				return methodInfo;
			}
		}

		// Token: 0x06003009 RID: 12297
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo MakeGenericMethod_impl(Type[] types);

		// Token: 0x0600300A RID: 12298
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern Type[] GetGenericArguments();

		// Token: 0x0600300B RID: 12299
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo GetGenericMethodDefinition_impl();

		// Token: 0x0600300C RID: 12300 RVA: 0x000B6AE5 File Offset: 0x000B4CE5
		public override MethodInfo GetGenericMethodDefinition()
		{
			MethodInfo genericMethodDefinition_impl = this.GetGenericMethodDefinition_impl();
			if (genericMethodDefinition_impl == null)
			{
				throw new InvalidOperationException();
			}
			return genericMethodDefinition_impl;
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600300D RID: 12301
		public override extern bool IsGenericMethodDefinition
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600300E RID: 12302
		public override extern bool IsGenericMethod
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600300F RID: 12303 RVA: 0x000B6AFC File Offset: 0x000B4CFC
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.IsGenericMethod)
				{
					Type[] genericArguments = this.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						if (genericArguments[i].ContainsGenericParameters)
						{
							return true;
						}
					}
				}
				return this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x06003010 RID: 12304
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int get_core_clr_security_level();

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06003011 RID: 12305 RVA: 0x000B6B3D File Offset: 0x000B4D3D
		public override bool IsSecurityCritical
		{
			get
			{
				return this.get_core_clr_security_level() > 0;
			}
		}

		// Token: 0x0400186F RID: 6255
		internal IntPtr mhandle;

		// Token: 0x04001870 RID: 6256
		private string name;

		// Token: 0x04001871 RID: 6257
		private Type reftype;
	}
}
