using System;
using System.Reflection;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000268 RID: 616
	internal static class TypeHelpers
	{
		// Token: 0x06001667 RID: 5735 RVA: 0x00064FC8 File Offset: 0x000631C8
		public static TObject As<TObject>(this object obj)
		{
			if (obj == null)
			{
				return default(TObject);
			}
			return (TObject)((object)obj);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00064FE8 File Offset: 0x000631E8
		public static bool IsInt(this TypeCode type)
		{
			switch (type)
			{
			case TypeCode.SByte:
				return true;
			case TypeCode.Byte:
				return true;
			case TypeCode.Int16:
				return true;
			case TypeCode.UInt16:
				return true;
			case TypeCode.Int32:
				return true;
			case TypeCode.UInt32:
				return true;
			case TypeCode.Int64:
				return true;
			case TypeCode.UInt64:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00065028 File Offset: 0x00063228
		public static Type GetValueType(MemberInfo member)
		{
			FieldInfo field = member as FieldInfo;
			if (field != null)
			{
				return field.FieldType;
			}
			PropertyInfo property = member as PropertyInfo;
			if (property != null)
			{
				return property.PropertyType;
			}
			MethodInfo method = member as MethodInfo;
			if (method != null)
			{
				return method.ReturnType;
			}
			return null;
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0006507C File Offset: 0x0006327C
		public static string GetNiceTypeName(this Type type)
		{
			if (type.IsPrimitive)
			{
				if (type == typeof(int))
				{
					return "int";
				}
				if (type == typeof(float))
				{
					return "float";
				}
				if (type == typeof(char))
				{
					return "char";
				}
				if (type == typeof(byte))
				{
					return "byte";
				}
				if (type == typeof(short))
				{
					return "short";
				}
				if (type == typeof(long))
				{
					return "long";
				}
				if (type == typeof(double))
				{
					return "double";
				}
				if (type == typeof(uint))
				{
					return "uint";
				}
				if (type == typeof(sbyte))
				{
					return "sbyte";
				}
				if (type == typeof(ushort))
				{
					return "ushort";
				}
				if (type == typeof(ulong))
				{
					return "ulong";
				}
			}
			return type.Name;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x000651A4 File Offset: 0x000633A4
		public static Type GetGenericTypeArgumentFromHierarchy(Type type, Type genericTypeDefinition, int argumentIndex)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (genericTypeDefinition == null)
			{
				throw new ArgumentNullException("genericTypeDefinition");
			}
			if (argumentIndex < 0)
			{
				throw new ArgumentOutOfRangeException("argumentIndex");
			}
			if (genericTypeDefinition.IsInterface)
			{
				Type typeArgument;
				for (;;)
				{
					Type[] interfaces = type.GetInterfaces();
					bool haveFoundInterface = false;
					foreach (Type element in interfaces)
					{
						if (element.IsConstructedGenericType && element.GetGenericTypeDefinition() == genericTypeDefinition)
						{
							type = element;
							haveFoundInterface = true;
							break;
						}
						typeArgument = TypeHelpers.GetGenericTypeArgumentFromHierarchy(element, genericTypeDefinition, argumentIndex);
						if (typeArgument != null)
						{
							return typeArgument;
						}
					}
					if (haveFoundInterface)
					{
						goto IL_00EB;
					}
					type = type.BaseType;
					if (type == null || type == typeof(object))
					{
						goto IL_00B7;
					}
				}
				return typeArgument;
				IL_00B7:
				return null;
			}
			while (!type.IsConstructedGenericType || type.GetGenericTypeDefinition() != genericTypeDefinition)
			{
				type = type.BaseType;
				if (type == typeof(object))
				{
					return null;
				}
			}
			IL_00EB:
			return type.GenericTypeArguments[argumentIndex];
		}
	}
}
