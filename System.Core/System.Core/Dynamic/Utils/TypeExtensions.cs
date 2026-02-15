using System;
using System.Reflection;

namespace System.Dynamic.Utils
{
	// Token: 0x0200014E RID: 334
	internal static class TypeExtensions
	{
		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002B070 File Offset: 0x00029270
		public static MethodInfo GetAnyStaticMethodValidated(this Type type, string name, Type[] types)
		{
			MethodInfo method = type.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
			if (!method.MatchesArgumentTypes(types))
			{
				return null;
			}
			return method;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002B098 File Offset: 0x00029298
		private static bool MatchesArgumentTypes(this MethodInfo mi, Type[] argTypes)
		{
			if (mi == null)
			{
				return false;
			}
			ParameterInfo[] parametersCached = mi.GetParametersCached();
			if (parametersCached.Length != argTypes.Length)
			{
				return false;
			}
			for (int i = 0; i < parametersCached.Length; i++)
			{
				if (!TypeUtils.AreReferenceAssignable(parametersCached[i].ParameterType, argTypes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002B0E4 File Offset: 0x000292E4
		public static Type GetReturnType(this MethodBase mi)
		{
			if (!mi.IsConstructor)
			{
				return ((MethodInfo)mi).ReturnType;
			}
			return mi.DeclaringType;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002B100 File Offset: 0x00029300
		public static TypeCode GetTypeCode(this Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002B108 File Offset: 0x00029308
		internal static ParameterInfo[] GetParametersCached(this MethodBase method)
		{
			CacheDict<MethodBase, ParameterInfo[]> cacheDict = TypeExtensions.s_paramInfoCache;
			ParameterInfo[] parameters;
			if (!cacheDict.TryGetValue(method, out parameters))
			{
				parameters = method.GetParameters();
				Type declaringType = method.DeclaringType;
				if (declaringType != null && !declaringType.IsCollectible)
				{
					cacheDict[method] = parameters;
				}
			}
			return parameters;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002B14D File Offset: 0x0002934D
		internal static bool IsByRefParameter(this ParameterInfo pi)
		{
			return pi.ParameterType.IsByRef || (pi.Attributes & ParameterAttributes.Out) == ParameterAttributes.Out;
		}

		// Token: 0x04000356 RID: 854
		private static readonly CacheDict<MethodBase, ParameterInfo[]> s_paramInfoCache = new CacheDict<MethodBase, ParameterInfo[]>(75);
	}
}
