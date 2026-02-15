using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x02000643 RID: 1603
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Module))]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeModule : Module
	{
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06003032 RID: 12338 RVA: 0x000B6DD4 File Offset: 0x000B4FD4
		public override Assembly Assembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06003033 RID: 12339 RVA: 0x000B6DDC File Offset: 0x000B4FDC
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06003034 RID: 12340 RVA: 0x000B6DE4 File Offset: 0x000B4FE4
		public override string ScopeName
		{
			get
			{
				return this.scopename;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x000B6DEC File Offset: 0x000B4FEC
		public override Guid ModuleVersionId
		{
			get
			{
				return this.GetModuleVersionId();
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06003036 RID: 12342 RVA: 0x000B6DF4 File Offset: 0x000B4FF4
		public override string FullyQualifiedName
		{
			get
			{
				if (SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fqname).Demand();
				}
				return this.fqname;
			}
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x000B6E14 File Offset: 0x000B5014
		public override bool IsResource()
		{
			return this.is_resource;
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x000B3EED File Offset: 0x000B20ED
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x000B3EF6 File Offset: 0x000B20F6
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x000B6E1C File Offset: 0x000B501C
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.IsResource())
			{
				return null;
			}
			Type globalType = RuntimeModule.GetGlobalType(this._impl);
			if (!(globalType != null))
			{
				return null;
			}
			return globalType.GetField(name, bindingAttr);
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000B6E60 File Offset: 0x000B5060
		public override FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			if (this.IsResource())
			{
				return new FieldInfo[0];
			}
			Type globalType = RuntimeModule.GetGlobalType(this._impl);
			if (!(globalType != null))
			{
				return new FieldInfo[0];
			}
			return globalType.GetFields(bindingFlags);
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x0600303C RID: 12348 RVA: 0x000B6E9F File Offset: 0x000B509F
		public override int MetadataToken
		{
			get
			{
				return RuntimeModule.get_MetadataToken(this);
			}
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000B6EA8 File Offset: 0x000B50A8
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (this.IsResource())
			{
				return null;
			}
			Type globalType = RuntimeModule.GetGlobalType(this._impl);
			if (globalType == null)
			{
				return null;
			}
			if (types == null)
			{
				return globalType.GetMethod(name);
			}
			return globalType.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000B6EF4 File Offset: 0x000B50F4
		public override MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			if (this.IsResource())
			{
				return new MethodInfo[0];
			}
			Type globalType = RuntimeModule.GetGlobalType(this._impl);
			if (!(globalType != null))
			{
				return new MethodInfo[0];
			}
			return globalType.GetMethods(bindingFlags);
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000B6F33 File Offset: 0x000B5133
		public override Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			if (className == string.Empty)
			{
				throw new ArgumentException("Type name can't be empty");
			}
			return this.assembly.InternalGetType(this, className, throwOnError, ignoreCase);
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x0003DC2A File Offset: 0x0003BE2A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000B6F6A File Offset: 0x000B516A
		public override FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return RuntimeModule.ResolveField(this, this._impl, metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000B6F7C File Offset: 0x000B517C
		internal static FieldInfo ResolveField(Module module, IntPtr monoModule, int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = RuntimeModule.ResolveFieldToken(monoModule, metadataToken, RuntimeModule.ptrs_from_types(genericTypeArguments), RuntimeModule.ptrs_from_types(genericMethodArguments), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw RuntimeModule.resolve_token_exception(module.Name, metadataToken, resolveTokenError, "Field");
			}
			return FieldInfo.GetFieldFromHandle(new RuntimeFieldHandle(intPtr));
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000B6FCB File Offset: 0x000B51CB
		public override MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return RuntimeModule.ResolveMember(this, this._impl, metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000B6FDC File Offset: 0x000B51DC
		internal static MemberInfo ResolveMember(Module module, IntPtr monoModule, int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError resolveTokenError;
			MemberInfo memberInfo = RuntimeModule.ResolveMemberToken(monoModule, metadataToken, RuntimeModule.ptrs_from_types(genericTypeArguments), RuntimeModule.ptrs_from_types(genericMethodArguments), out resolveTokenError);
			if (memberInfo == null)
			{
				throw RuntimeModule.resolve_token_exception(module.Name, metadataToken, resolveTokenError, "MemberInfo");
			}
			return memberInfo;
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000B701D File Offset: 0x000B521D
		public override MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return RuntimeModule.ResolveMethod(this, this._impl, metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000B7030 File Offset: 0x000B5230
		internal static MethodBase ResolveMethod(Module module, IntPtr monoModule, int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = RuntimeModule.ResolveMethodToken(monoModule, metadataToken, RuntimeModule.ptrs_from_types(genericTypeArguments), RuntimeModule.ptrs_from_types(genericMethodArguments), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw RuntimeModule.resolve_token_exception(module.Name, metadataToken, resolveTokenError, "MethodBase");
			}
			return RuntimeMethodInfo.GetMethodFromHandleNoGenericCheck(new RuntimeMethodHandle(intPtr));
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000B707F File Offset: 0x000B527F
		public override string ResolveString(int metadataToken)
		{
			return RuntimeModule.ResolveString(this, this._impl, metadataToken);
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000B7090 File Offset: 0x000B5290
		internal static string ResolveString(Module module, IntPtr monoModule, int metadataToken)
		{
			ResolveTokenError resolveTokenError;
			string text = RuntimeModule.ResolveStringToken(monoModule, metadataToken, out resolveTokenError);
			if (text == null)
			{
				throw RuntimeModule.resolve_token_exception(module.Name, metadataToken, resolveTokenError, "string");
			}
			return text;
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000B70BE File Offset: 0x000B52BE
		public override Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return RuntimeModule.ResolveType(this, this._impl, metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000B70D0 File Offset: 0x000B52D0
		internal static Type ResolveType(Module module, IntPtr monoModule, int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = RuntimeModule.ResolveTypeToken(monoModule, metadataToken, RuntimeModule.ptrs_from_types(genericTypeArguments), RuntimeModule.ptrs_from_types(genericMethodArguments), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw RuntimeModule.resolve_token_exception(module.Name, metadataToken, resolveTokenError, "Type");
			}
			return Type.GetTypeFromHandle(new RuntimeTypeHandle(intPtr));
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000B711F File Offset: 0x000B531F
		public override byte[] ResolveSignature(int metadataToken)
		{
			return RuntimeModule.ResolveSignature(this, this._impl, metadataToken);
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000B7130 File Offset: 0x000B5330
		internal static byte[] ResolveSignature(Module module, IntPtr monoModule, int metadataToken)
		{
			ResolveTokenError resolveTokenError;
			byte[] array = RuntimeModule.ResolveSignature(monoModule, metadataToken, out resolveTokenError);
			if (array == null)
			{
				throw RuntimeModule.resolve_token_exception(module.Name, metadataToken, resolveTokenError, "signature");
			}
			return array;
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000B715E File Offset: 0x000B535E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 5, this.ScopeName, this.GetRuntimeAssembly());
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000B7181 File Offset: 0x000B5381
		public override Type[] GetTypes()
		{
			return RuntimeModule.InternalGetTypes(this._impl);
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x000B718E File Offset: 0x000B538E
		internal RuntimeAssembly GetRuntimeAssembly()
		{
			return (RuntimeAssembly)this.assembly;
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x000B719B File Offset: 0x000B539B
		internal IntPtr MonoModule
		{
			get
			{
				return this._impl;
			}
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000B71A4 File Offset: 0x000B53A4
		internal override Guid GetModuleVersionId()
		{
			byte[] array = new byte[16];
			RuntimeModule.GetGuidInternal(this._impl, array);
			return new Guid(array);
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000B71CB File Offset: 0x000B53CB
		internal static Exception resolve_token_exception(string name, int metadataToken, ResolveTokenError error, string tokenType)
		{
			if (error == ResolveTokenError.OutOfRange)
			{
				return new ArgumentOutOfRangeException("metadataToken", string.Format("Token 0x{0:x} is not valid in the scope of module {1}", metadataToken, name));
			}
			return new ArgumentException(string.Format("Token 0x{0:x} is not a valid {1} token in the scope of module {2}", metadataToken, tokenType, name), "metadataToken");
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000B7208 File Offset: 0x000B5408
		internal static IntPtr[] ptrs_from_types(Type[] types)
		{
			if (types == null)
			{
				return null;
			}
			IntPtr[] array = new IntPtr[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentException();
				}
				array[i] = types[i].TypeHandle.Value;
			}
			return array;
		}

		// Token: 0x06003054 RID: 12372
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_MetadataToken(Module module);

		// Token: 0x06003055 RID: 12373
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type[] InternalGetTypes(IntPtr module);

		// Token: 0x06003056 RID: 12374
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetHINSTANCE(IntPtr module);

		// Token: 0x06003057 RID: 12375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGuidInternal(IntPtr module, byte[] guid);

		// Token: 0x06003058 RID: 12376
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type GetGlobalType(IntPtr module);

		// Token: 0x06003059 RID: 12377
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ResolveTypeToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x0600305A RID: 12378
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ResolveMethodToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x0600305B RID: 12379
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ResolveFieldToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x0600305C RID: 12380
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string ResolveStringToken(IntPtr module, int token, out ResolveTokenError error);

		// Token: 0x0600305D RID: 12381
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MemberInfo ResolveMemberToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x0600305E RID: 12382
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern byte[] ResolveSignature(IntPtr module, int metadataToken, out ResolveTokenError error);

		// Token: 0x04001875 RID: 6261
		internal IntPtr _impl;

		// Token: 0x04001876 RID: 6262
		internal Assembly assembly;

		// Token: 0x04001877 RID: 6263
		internal string fqname;

		// Token: 0x04001878 RID: 6264
		internal string name;

		// Token: 0x04001879 RID: 6265
		internal string scopename;

		// Token: 0x0400187A RID: 6266
		internal bool is_resource;

		// Token: 0x0400187B RID: 6267
		internal int token;
	}
}
