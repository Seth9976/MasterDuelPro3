using System;
using System.Reflection;
using System.Security;

namespace System
{
	// Token: 0x020000EE RID: 238
	internal static class SecurityUtils
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x00002FA0 File Offset: 0x000011A0
		private static void DemandReflectionAccess(Type type)
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00002FA0 File Offset: 0x000011A0
		private static void DemandGrantSet(Assembly assembly)
		{
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00011CA4 File Offset: 0x0000FEA4
		private static bool HasReflectionPermission(Type type)
		{
			try
			{
				SecurityUtils.DemandReflectionAccess(type);
				return true;
			}
			catch (SecurityException)
			{
			}
			return false;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00011CD4 File Offset: 0x0000FED4
		internal static object SecureCreateInstance(Type type)
		{
			return SecurityUtils.SecureCreateInstance(type, null, false);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00011CE0 File Offset: 0x0000FEE0
		internal static object SecureCreateInstance(Type type, object[] args, bool allowNonPublic)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
			if (!type.IsVisible)
			{
				SecurityUtils.DemandReflectionAccess(type);
			}
			else if (allowNonPublic && !SecurityUtils.HasReflectionPermission(type))
			{
				allowNonPublic = false;
			}
			if (allowNonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			return Activator.CreateInstance(type, bindingFlags, null, args, null);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00011D37 File Offset: 0x0000FF37
		internal static object SecureCreateInstance(Type type, object[] args)
		{
			return SecurityUtils.SecureCreateInstance(type, args, false);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00011D41 File Offset: 0x0000FF41
		internal static object SecureConstructorInvoke(Type type, Type[] argTypes, object[] args, bool allowNonPublic)
		{
			return SecurityUtils.SecureConstructorInvoke(type, argTypes, args, allowNonPublic, BindingFlags.Default);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00011D50 File Offset: 0x0000FF50
		internal static object SecureConstructorInvoke(Type type, Type[] argTypes, object[] args, bool allowNonPublic, BindingFlags extraFlags)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsVisible)
			{
				SecurityUtils.DemandReflectionAccess(type);
			}
			else if (allowNonPublic && !SecurityUtils.HasReflectionPermission(type))
			{
				allowNonPublic = false;
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | extraFlags;
			if (!allowNonPublic)
			{
				bindingFlags &= ~BindingFlags.NonPublic;
			}
			ConstructorInfo constructor = type.GetConstructor(bindingFlags, null, argTypes, null);
			if (constructor != null)
			{
				return constructor.Invoke(args);
			}
			return null;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00011DBC File Offset: 0x0000FFBC
		private static bool GenericArgumentsAreVisible(MethodInfo method)
		{
			if (method.IsGenericMethod)
			{
				Type[] genericArguments = method.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (!genericArguments[i].IsVisible)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00011DF4 File Offset: 0x0000FFF4
		internal static object MethodInfoInvoke(MethodInfo method, object target, object[] args)
		{
			Type declaringType = method.DeclaringType;
			if (declaringType == null)
			{
				if (!method.IsPublic || !SecurityUtils.GenericArgumentsAreVisible(method))
				{
					SecurityUtils.DemandGrantSet(method.Module.Assembly);
				}
			}
			else if (!declaringType.IsVisible || !method.IsPublic || !SecurityUtils.GenericArgumentsAreVisible(method))
			{
				SecurityUtils.DemandReflectionAccess(declaringType);
			}
			return method.Invoke(target, args);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00011E5C File Offset: 0x0001005C
		internal static object ConstructorInfoInvoke(ConstructorInfo ctor, object[] args)
		{
			Type declaringType = ctor.DeclaringType;
			if (declaringType != null && (!declaringType.IsVisible || !ctor.IsPublic))
			{
				SecurityUtils.DemandReflectionAccess(declaringType);
			}
			return ctor.Invoke(args);
		}
	}
}
