using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x02000197 RID: 407
	internal static class TypeExtensions
	{
		// Token: 0x0600133D RID: 4925 RVA: 0x0005E5E0 File Offset: 0x0005C7E0
		public static bool TryConvertTo(this Type targetType, object data, out object returnValue)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			returnValue = null;
			if (data == null)
			{
				return !targetType.IsValueType;
			}
			Type type = data.GetType();
			if (targetType == type || targetType.IsAssignableFrom(type))
			{
				returnValue = data;
				return true;
			}
			foreach (MethodInfo methodInfo in targetType.GetMethods(BindingFlags.Static | BindingFlags.Public))
			{
				if (methodInfo.Name == "op_Implicit" && methodInfo.ReturnType != null && targetType.IsAssignableFrom(methodInfo.ReturnType))
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (parameters != null && parameters.Length == 1 && parameters[0].ParameterType.IsAssignableFrom(type))
					{
						returnValue = methodInfo.Invoke(null, new object[] { data });
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040008ED RID: 2285
		private const string ImplicitCastOperatorName = "op_Implicit";
	}
}
