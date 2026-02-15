using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000640 RID: 1600
	internal struct MonoMethodInfo
	{
		// Token: 0x06002FD0 RID: 12240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_method_info(IntPtr handle, out MonoMethodInfo info);

		// Token: 0x06002FD1 RID: 12241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_method_attributes(IntPtr handle);

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000B6278 File Offset: 0x000B4478
		internal static MonoMethodInfo GetMethodInfo(IntPtr handle)
		{
			MonoMethodInfo monoMethodInfo;
			MonoMethodInfo.get_method_info(handle, out monoMethodInfo);
			return monoMethodInfo;
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000B628E File Offset: 0x000B448E
		internal static Type GetDeclaringType(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).parent;
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x000B629B File Offset: 0x000B449B
		internal static Type GetReturnType(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).ret;
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x000B62A8 File Offset: 0x000B44A8
		internal static MethodAttributes GetAttributes(IntPtr handle)
		{
			return (MethodAttributes)MonoMethodInfo.get_method_attributes(handle);
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000B62B0 File Offset: 0x000B44B0
		internal static CallingConventions GetCallingConvention(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).callconv;
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000B62BD File Offset: 0x000B44BD
		internal static MethodImplAttributes GetMethodImplementationFlags(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).iattrs;
		}

		// Token: 0x06002FD8 RID: 12248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ParameterInfo[] get_parameter_info(IntPtr handle, MemberInfo member);

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000B62CA File Offset: 0x000B44CA
		internal static ParameterInfo[] GetParametersInfo(IntPtr handle, MemberInfo member)
		{
			return MonoMethodInfo.get_parameter_info(handle, member);
		}

		// Token: 0x06002FDA RID: 12250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MarshalAsAttribute get_retval_marshal(IntPtr handle);

		// Token: 0x06002FDB RID: 12251 RVA: 0x000B62D3 File Offset: 0x000B44D3
		internal static ParameterInfo GetReturnParameterInfo(RuntimeMethodInfo method)
		{
			return RuntimeParameterInfo.New(MonoMethodInfo.GetReturnType(method.mhandle), method, MonoMethodInfo.get_retval_marshal(method.mhandle));
		}

		// Token: 0x0400186A RID: 6250
		private Type parent;

		// Token: 0x0400186B RID: 6251
		private Type ret;

		// Token: 0x0400186C RID: 6252
		internal MethodAttributes attrs;

		// Token: 0x0400186D RID: 6253
		internal MethodImplAttributes iattrs;

		// Token: 0x0400186E RID: 6254
		private CallingConventions callconv;
	}
}
