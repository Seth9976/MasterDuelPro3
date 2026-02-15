using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Threading;
using Mono;

namespace System
{
	// Token: 0x020001AA RID: 426
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeType : global::System.Reflection.TypeInfo, ISerializable, ICloneable
	{
		// Token: 0x06000F9A RID: 3994 RVA: 0x00041748 File Offset: 0x0003F948
		internal static RuntimeType GetType(string typeName, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			return RuntimeTypeHandle.GetTypeByName(typeName, throwOnError, ignoreCase, reflectionOnly, ref stackMark, false);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00041764 File Offset: 0x0003F964
		private static void ThrowIfTypeNeverValidGenericArgument(RuntimeType type)
		{
			if (type.IsPointer || type.IsByRef || type == typeof(void))
			{
				throw new ArgumentException(Environment.GetResourceString("The type '{0}' may not be used as a type argument.", new object[] { type.ToString() }));
			}
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x000417B4 File Offset: 0x0003F9B4
		internal static void SanityCheckGenericArguments(RuntimeType[] genericArguments, RuntimeType[] genericParamters)
		{
			if (genericArguments == null)
			{
				throw new ArgumentNullException();
			}
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (genericArguments[i] == null)
				{
					throw new ArgumentNullException();
				}
				RuntimeType.ThrowIfTypeNeverValidGenericArgument(genericArguments[i]);
			}
			if (genericArguments.Length != genericParamters.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("The type or method has {1} generic parameter(s), but {0} generic argument(s) were provided. A generic argument must be provided for each generic parameter.", new object[] { genericArguments.Length, genericParamters.Length }));
			}
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00041828 File Offset: 0x0003FA28
		private static void SplitName(string fullname, out string name, out string ns)
		{
			name = null;
			ns = null;
			if (fullname == null)
			{
				return;
			}
			int num = fullname.LastIndexOf(".", StringComparison.Ordinal);
			if (num == -1)
			{
				name = fullname;
				return;
			}
			ns = fullname.Substring(0, num);
			int num2 = fullname.Length - ns.Length - 1;
			if (num2 != 0)
			{
				name = fullname.Substring(num + 1, num2);
				return;
			}
			name = "";
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00041888 File Offset: 0x0003FA88
		internal static BindingFlags FilterPreCalculate(bool isPublic, bool isInherited, bool isStatic)
		{
			BindingFlags bindingFlags = (isPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			if (isInherited)
			{
				bindingFlags |= BindingFlags.DeclaredOnly;
				if (isStatic)
				{
					bindingFlags |= BindingFlags.Static | BindingFlags.FlattenHierarchy;
				}
				else
				{
					bindingFlags |= BindingFlags.Instance;
				}
			}
			else if (isStatic)
			{
				bindingFlags |= BindingFlags.Static;
			}
			else
			{
				bindingFlags |= BindingFlags.Instance;
			}
			return bindingFlags;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x000418C4 File Offset: 0x0003FAC4
		private static void FilterHelper(BindingFlags bindingFlags, ref string name, bool allowPrefixLookup, out bool prefixLookup, out bool ignoreCase, out RuntimeType.MemberListType listType)
		{
			prefixLookup = false;
			ignoreCase = false;
			if (name != null)
			{
				if ((bindingFlags & BindingFlags.IgnoreCase) != BindingFlags.Default)
				{
					name = name.ToLower(CultureInfo.InvariantCulture);
					ignoreCase = true;
					listType = RuntimeType.MemberListType.CaseInsensitive;
				}
				else
				{
					listType = RuntimeType.MemberListType.CaseSensitive;
				}
				if (allowPrefixLookup && name.EndsWith("*", StringComparison.Ordinal))
				{
					name = name.Substring(0, name.Length - 1);
					prefixLookup = true;
					listType = RuntimeType.MemberListType.All;
					return;
				}
			}
			else
			{
				listType = RuntimeType.MemberListType.All;
			}
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00041930 File Offset: 0x0003FB30
		private static void FilterHelper(BindingFlags bindingFlags, ref string name, out bool ignoreCase, out RuntimeType.MemberListType listType)
		{
			bool flag;
			RuntimeType.FilterHelper(bindingFlags, ref name, false, out flag, out ignoreCase, out listType);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00041949 File Offset: 0x0003FB49
		private static bool FilterApplyPrefixLookup(MemberInfo memberInfo, string name, bool ignoreCase)
		{
			if (ignoreCase)
			{
				if (!memberInfo.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			else if (!memberInfo.Name.StartsWith(name, StringComparison.Ordinal))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00041974 File Offset: 0x0003FB74
		private static bool FilterApplyBase(MemberInfo memberInfo, BindingFlags bindingFlags, bool isPublic, bool isNonProtectedInternal, bool isStatic, string name, bool prefixLookup)
		{
			if (isPublic)
			{
				if ((bindingFlags & BindingFlags.Public) == BindingFlags.Default)
				{
					return false;
				}
			}
			else if ((bindingFlags & BindingFlags.NonPublic) == BindingFlags.Default)
			{
				return false;
			}
			bool flag = memberInfo.DeclaringType != memberInfo.ReflectedType;
			if ((bindingFlags & BindingFlags.DeclaredOnly) > BindingFlags.Default && flag)
			{
				return false;
			}
			if (memberInfo.MemberType != MemberTypes.TypeInfo && memberInfo.MemberType != MemberTypes.NestedType)
			{
				if (isStatic)
				{
					if ((bindingFlags & BindingFlags.FlattenHierarchy) == BindingFlags.Default && flag)
					{
						return false;
					}
					if ((bindingFlags & BindingFlags.Static) == BindingFlags.Default)
					{
						return false;
					}
				}
				else if ((bindingFlags & BindingFlags.Instance) == BindingFlags.Default)
				{
					return false;
				}
			}
			if (prefixLookup && !RuntimeType.FilterApplyPrefixLookup(memberInfo, name, (bindingFlags & BindingFlags.IgnoreCase) > BindingFlags.Default))
			{
				return false;
			}
			if ((bindingFlags & BindingFlags.DeclaredOnly) == BindingFlags.Default && flag && isNonProtectedInternal && (bindingFlags & BindingFlags.NonPublic) != BindingFlags.Default && !isStatic && (bindingFlags & BindingFlags.Instance) != BindingFlags.Default)
			{
				MethodInfo methodInfo = memberInfo as MethodInfo;
				if (methodInfo == null)
				{
					return false;
				}
				if (!methodInfo.IsVirtual && !methodInfo.IsAbstract)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00041A40 File Offset: 0x0003FC40
		private static bool FilterApplyType(Type type, BindingFlags bindingFlags, string name, bool prefixLookup, string ns)
		{
			bool flag = type.IsNestedPublic || type.IsPublic;
			bool flag2 = false;
			return RuntimeType.FilterApplyBase(type, bindingFlags, flag, type.IsNestedAssembly, flag2, name, prefixLookup) && (ns == null || !(ns != type.Namespace));
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00041A8C File Offset: 0x0003FC8C
		private static bool FilterApplyMethodInfo(RuntimeMethodInfo method, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
		{
			return RuntimeType.FilterApplyMethodBase(method, method.BindingFlags, bindingFlags, callConv, argumentTypes);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00041A9D File Offset: 0x0003FC9D
		private static bool FilterApplyConstructorInfo(RuntimeConstructorInfo constructor, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
		{
			return RuntimeType.FilterApplyMethodBase(constructor, constructor.BindingFlags, bindingFlags, callConv, argumentTypes);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00041AB0 File Offset: 0x0003FCB0
		private static bool FilterApplyMethodBase(MethodBase methodBase, BindingFlags methodFlags, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
		{
			bindingFlags ^= BindingFlags.DeclaredOnly;
			if ((callConv & CallingConventions.Any) == (CallingConventions)0)
			{
				if ((callConv & CallingConventions.VarArgs) != (CallingConventions)0 && (methodBase.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
				{
					return false;
				}
				if ((callConv & CallingConventions.Standard) != (CallingConventions)0 && (methodBase.CallingConvention & CallingConventions.Standard) == (CallingConventions)0)
				{
					return false;
				}
			}
			if (argumentTypes != null)
			{
				ParameterInfo[] parametersNoCopy = methodBase.GetParametersNoCopy();
				if (argumentTypes.Length != parametersNoCopy.Length)
				{
					if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetProperty | BindingFlags.SetProperty)) == BindingFlags.Default)
					{
						return false;
					}
					bool flag = false;
					if (argumentTypes.Length > parametersNoCopy.Length)
					{
						if ((methodBase.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
						{
							flag = true;
						}
					}
					else if ((bindingFlags & BindingFlags.OptionalParamBinding) == BindingFlags.Default)
					{
						flag = true;
					}
					else if (!parametersNoCopy[argumentTypes.Length].IsOptional)
					{
						flag = true;
					}
					if (flag)
					{
						if (parametersNoCopy.Length == 0)
						{
							return false;
						}
						if (argumentTypes.Length < parametersNoCopy.Length - 1)
						{
							return false;
						}
						ParameterInfo parameterInfo = parametersNoCopy[parametersNoCopy.Length - 1];
						if (!parameterInfo.ParameterType.IsArray)
						{
							return false;
						}
						if (!parameterInfo.IsDefined(typeof(ParamArrayAttribute), false))
						{
							return false;
						}
					}
				}
				else if ((bindingFlags & BindingFlags.ExactBinding) != BindingFlags.Default && (bindingFlags & BindingFlags.InvokeMethod) == BindingFlags.Default)
				{
					for (int i = 0; i < parametersNoCopy.Length; i++)
					{
						if (argumentTypes[i] != null && !argumentTypes[i].MatchesParameterTypeExactly(parametersNoCopy[i]))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00041BC0 File Offset: 0x0003FDC0
		internal RuntimeType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00041BD0 File Offset: 0x0003FDD0
		private RuntimeType.ListBuilder<MethodInfo> GetMethodCandidates(string name, BindingFlags bindingAttr, CallingConventions callConv, Type[] types, int genericParamCount, bool allowPrefixLookup)
		{
			bool flag;
			bool flag2;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out flag2, out memberListType);
			RuntimeMethodInfo[] methodsByName = this.GetMethodsByName(name, bindingAttr, memberListType, this);
			RuntimeType.ListBuilder<MethodInfo> listBuilder = new RuntimeType.ListBuilder<MethodInfo>(methodsByName.Length);
			int i = 0;
			while (i < methodsByName.Length)
			{
				RuntimeMethodInfo runtimeMethodInfo = methodsByName[i];
				if (genericParamCount == -1)
				{
					goto IL_005E;
				}
				bool isGenericMethod = runtimeMethodInfo.IsGenericMethod;
				if ((genericParamCount != 0 || !isGenericMethod) && (genericParamCount <= 0 || isGenericMethod) && runtimeMethodInfo.GetGenericArguments().Length == genericParamCount)
				{
					goto IL_005E;
				}
				IL_0082:
				i++;
				continue;
				IL_005E:
				if (RuntimeType.FilterApplyMethodInfo(runtimeMethodInfo, bindingAttr, callConv, types) && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeMethodInfo, name, flag2)))
				{
					listBuilder.Add(runtimeMethodInfo);
					goto IL_0082;
				}
				goto IL_0082;
			}
			return listBuilder;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00041C70 File Offset: 0x0003FE70
		private RuntimeType.ListBuilder<ConstructorInfo> GetConstructorCandidates(string name, BindingFlags bindingAttr, CallingConventions callConv, Type[] types, bool allowPrefixLookup)
		{
			bool flag;
			bool flag2;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out flag2, out memberListType);
			if ((!flag && name != null && name.Length == 0) || (!string.IsNullOrEmpty(name) && name != ConstructorInfo.ConstructorName && name != ConstructorInfo.TypeConstructorName))
			{
				return new RuntimeType.ListBuilder<ConstructorInfo>(0);
			}
			RuntimeConstructorInfo[] constructors_internal = this.GetConstructors_internal(bindingAttr, this);
			RuntimeType.ListBuilder<ConstructorInfo> listBuilder = new RuntimeType.ListBuilder<ConstructorInfo>(constructors_internal.Length);
			foreach (RuntimeConstructorInfo runtimeConstructorInfo in constructors_internal)
			{
				if (RuntimeType.FilterApplyConstructorInfo(runtimeConstructorInfo, bindingAttr, callConv, types) && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeConstructorInfo, name, flag2)))
				{
					listBuilder.Add(runtimeConstructorInfo);
				}
			}
			return listBuilder;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00041D18 File Offset: 0x0003FF18
		private RuntimeType.ListBuilder<PropertyInfo> GetPropertyCandidates(string name, BindingFlags bindingAttr, Type[] types, bool allowPrefixLookup)
		{
			bool flag;
			bool flag2;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out flag2, out memberListType);
			RuntimePropertyInfo[] propertiesByName = this.GetPropertiesByName(name, bindingAttr, memberListType, this);
			bindingAttr ^= BindingFlags.DeclaredOnly;
			RuntimeType.ListBuilder<PropertyInfo> listBuilder = new RuntimeType.ListBuilder<PropertyInfo>(propertiesByName.Length);
			foreach (RuntimePropertyInfo runtimePropertyInfo in propertiesByName)
			{
				if ((bindingAttr & runtimePropertyInfo.BindingFlags) == runtimePropertyInfo.BindingFlags && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimePropertyInfo, name, flag2)) && (types == null || runtimePropertyInfo.GetIndexParameters().Length == types.Length))
				{
					listBuilder.Add(runtimePropertyInfo);
				}
			}
			return listBuilder;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00041DA4 File Offset: 0x0003FFA4
		private RuntimeType.ListBuilder<EventInfo> GetEventCandidates(string name, BindingFlags bindingAttr, bool allowPrefixLookup)
		{
			bool flag;
			bool flag2;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out flag2, out memberListType);
			RuntimeEventInfo[] events_internal = this.GetEvents_internal(name, bindingAttr, memberListType, this);
			bindingAttr ^= BindingFlags.DeclaredOnly;
			RuntimeType.ListBuilder<EventInfo> listBuilder = new RuntimeType.ListBuilder<EventInfo>(events_internal.Length);
			foreach (RuntimeEventInfo runtimeEventInfo in events_internal)
			{
				if ((bindingAttr & runtimeEventInfo.BindingFlags) == runtimeEventInfo.BindingFlags && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeEventInfo, name, flag2)))
				{
					listBuilder.Add(runtimeEventInfo);
				}
			}
			return listBuilder;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00041E20 File Offset: 0x00040020
		private RuntimeType.ListBuilder<FieldInfo> GetFieldCandidates(string name, BindingFlags bindingAttr, bool allowPrefixLookup)
		{
			bool flag;
			bool flag2;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out flag2, out memberListType);
			RuntimeFieldInfo[] fields_internal = this.GetFields_internal(name, bindingAttr, memberListType, this);
			bindingAttr ^= BindingFlags.DeclaredOnly;
			RuntimeType.ListBuilder<FieldInfo> listBuilder = new RuntimeType.ListBuilder<FieldInfo>(fields_internal.Length);
			foreach (RuntimeFieldInfo runtimeFieldInfo in fields_internal)
			{
				if ((bindingAttr & runtimeFieldInfo.BindingFlags) == runtimeFieldInfo.BindingFlags && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeFieldInfo, name, flag2)))
				{
					listBuilder.Add(runtimeFieldInfo);
				}
			}
			return listBuilder;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00041E9C File Offset: 0x0004009C
		private RuntimeType.ListBuilder<Type> GetNestedTypeCandidates(string fullname, BindingFlags bindingAttr, bool allowPrefixLookup)
		{
			bindingAttr &= ~BindingFlags.Static;
			string text;
			string text2;
			RuntimeType.SplitName(fullname, out text, out text2);
			bool flag;
			bool flag2;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref text, allowPrefixLookup, out flag, out flag2, out memberListType);
			RuntimeType[] nestedTypes_internal = this.GetNestedTypes_internal(text, bindingAttr, memberListType);
			RuntimeType.ListBuilder<Type> listBuilder = new RuntimeType.ListBuilder<Type>(nestedTypes_internal.Length);
			foreach (RuntimeType runtimeType in nestedTypes_internal)
			{
				if (RuntimeType.FilterApplyType(runtimeType, bindingAttr, text, flag, text2))
				{
					listBuilder.Add(runtimeType);
				}
			}
			return listBuilder;
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00041F14 File Offset: 0x00040114
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.GetMethodCandidates(null, bindingAttr, CallingConventions.Any, null, -1, false).ToArray();
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00041F38 File Offset: 0x00040138
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.GetConstructorCandidates(null, bindingAttr, CallingConventions.Any, null, false).ToArray();
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00041F58 File Offset: 0x00040158
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.GetPropertyCandidates(null, bindingAttr, null, false).ToArray();
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00041F78 File Offset: 0x00040178
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.GetEventCandidates(null, bindingAttr, false).ToArray();
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00041F98 File Offset: 0x00040198
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.GetFieldCandidates(null, bindingAttr, false).ToArray();
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00041FB8 File Offset: 0x000401B8
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			RuntimeType.ListBuilder<MethodInfo> methodCandidates = this.GetMethodCandidates(null, bindingAttr, CallingConventions.Any, null, -1, false);
			RuntimeType.ListBuilder<ConstructorInfo> constructorCandidates = this.GetConstructorCandidates(null, bindingAttr, CallingConventions.Any, null, false);
			RuntimeType.ListBuilder<PropertyInfo> propertyCandidates = this.GetPropertyCandidates(null, bindingAttr, null, false);
			RuntimeType.ListBuilder<EventInfo> eventCandidates = this.GetEventCandidates(null, bindingAttr, false);
			RuntimeType.ListBuilder<FieldInfo> fieldCandidates = this.GetFieldCandidates(null, bindingAttr, false);
			RuntimeType.ListBuilder<Type> nestedTypeCandidates = this.GetNestedTypeCandidates(null, bindingAttr, false);
			MemberInfo[] array = new MemberInfo[methodCandidates.Count + constructorCandidates.Count + propertyCandidates.Count + eventCandidates.Count + fieldCandidates.Count + nestedTypeCandidates.Count];
			int num = 0;
			object[] array2 = array;
			methodCandidates.CopyTo(array2, num);
			num += methodCandidates.Count;
			array2 = array;
			constructorCandidates.CopyTo(array2, num);
			num += constructorCandidates.Count;
			array2 = array;
			propertyCandidates.CopyTo(array2, num);
			num += propertyCandidates.Count;
			array2 = array;
			eventCandidates.CopyTo(array2, num);
			num += eventCandidates.Count;
			array2 = array;
			fieldCandidates.CopyTo(array2, num);
			num += fieldCandidates.Count;
			array2 = array;
			nestedTypeCandidates.CopyTo(array2, num);
			num += nestedTypeCandidates.Count;
			return array;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000420E8 File Offset: 0x000402E8
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			RuntimeType.ListBuilder<ConstructorInfo> constructorCandidates = this.GetConstructorCandidates(null, bindingAttr, CallingConventions.Any, types, false);
			if (constructorCandidates.Count == 0)
			{
				return null;
			}
			if (types.Length == 0 && constructorCandidates.Count == 1)
			{
				ConstructorInfo constructorInfo = constructorCandidates[0];
				ParameterInfo[] parametersNoCopy = constructorInfo.GetParametersNoCopy();
				if (parametersNoCopy == null || parametersNoCopy.Length == 0)
				{
					return constructorInfo;
				}
			}
			MethodBase[] array;
			if ((bindingAttr & BindingFlags.ExactBinding) != BindingFlags.Default)
			{
				array = constructorCandidates.ToArray();
				return global::System.DefaultBinder.ExactBinding(array, types, modifiers) as ConstructorInfo;
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			Binder binder2 = binder;
			array = constructorCandidates.ToArray();
			return binder2.SelectMethod(bindingAttr, array, types, modifiers) as ConstructorInfo;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0004217C File Offset: 0x0004037C
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			RuntimeType.ListBuilder<PropertyInfo> propertyCandidates = this.GetPropertyCandidates(name, bindingAttr, types, false);
			if (propertyCandidates.Count == 0)
			{
				return null;
			}
			if (types == null || types.Length == 0)
			{
				if (propertyCandidates.Count == 1)
				{
					PropertyInfo propertyInfo = propertyCandidates[0];
					if (returnType != null && !returnType.IsEquivalentTo(propertyInfo.PropertyType))
					{
						return null;
					}
					return propertyInfo;
				}
				else if (returnType == null)
				{
					throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
				}
			}
			if ((bindingAttr & BindingFlags.ExactBinding) != BindingFlags.Default)
			{
				return global::System.DefaultBinder.ExactPropertyBinding(propertyCandidates.ToArray(), returnType, types, modifiers);
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			return binder.SelectProperty(bindingAttr, propertyCandidates.ToArray(), returnType, types, modifiers);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0004222C File Offset: 0x0004042C
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			bool flag;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, out flag, out memberListType);
			RuntimeEventInfo[] events_internal = this.GetEvents_internal(name, bindingAttr, memberListType, this);
			EventInfo eventInfo = null;
			bindingAttr ^= BindingFlags.DeclaredOnly;
			foreach (RuntimeEventInfo runtimeEventInfo in events_internal)
			{
				if ((bindingAttr & runtimeEventInfo.BindingFlags) == runtimeEventInfo.BindingFlags)
				{
					if (eventInfo != null)
					{
						throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
					}
					eventInfo = runtimeEventInfo;
				}
			}
			return eventInfo;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x000422A8 File Offset: 0x000404A8
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			bool flag;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, out flag, out memberListType);
			RuntimeFieldInfo[] fields_internal = this.GetFields_internal(name, bindingAttr, memberListType, this);
			FieldInfo fieldInfo = null;
			bindingAttr ^= BindingFlags.DeclaredOnly;
			bool flag2 = false;
			foreach (RuntimeFieldInfo runtimeFieldInfo in fields_internal)
			{
				if ((bindingAttr & runtimeFieldInfo.BindingFlags) == runtimeFieldInfo.BindingFlags)
				{
					if (fieldInfo != null)
					{
						if (runtimeFieldInfo.DeclaringType == fieldInfo.DeclaringType)
						{
							throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
						}
						if (fieldInfo.DeclaringType.IsInterface && runtimeFieldInfo.DeclaringType.IsInterface)
						{
							flag2 = true;
						}
					}
					if (fieldInfo == null || runtimeFieldInfo.DeclaringType.IsSubclassOf(fieldInfo.DeclaringType) || fieldInfo.DeclaringType.IsInterface)
					{
						fieldInfo = runtimeFieldInfo;
					}
				}
			}
			if (flag2 && fieldInfo.DeclaringType.IsInterface)
			{
				throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
			}
			return fieldInfo;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x000423A4 File Offset: 0x000405A4
		public override Type GetInterface(string fullname, bool ignoreCase)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException();
			}
			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
			bindingFlags &= ~BindingFlags.Static;
			if (ignoreCase)
			{
				bindingFlags |= BindingFlags.IgnoreCase;
			}
			string text;
			string text2;
			RuntimeType.SplitName(fullname, out text, out text2);
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingFlags, ref text, out ignoreCase, out memberListType);
			List<RuntimeType> list = null;
			StringComparison stringComparison = (ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			foreach (RuntimeType runtimeType in this.GetInterfaces())
			{
				if (string.Equals(runtimeType.Name, text, stringComparison))
				{
					if (list == null)
					{
						list = new List<RuntimeType>(2);
					}
					list.Add(runtimeType);
				}
			}
			if (list == null)
			{
				return null;
			}
			RuntimeType[] array = list.ToArray();
			RuntimeType runtimeType2 = null;
			foreach (RuntimeType runtimeType3 in array)
			{
				if (RuntimeType.FilterApplyType(runtimeType3, bindingFlags, text, false, text2))
				{
					if (runtimeType2 != null)
					{
						throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
					}
					runtimeType2 = runtimeType3;
				}
			}
			return runtimeType2;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00042494 File Offset: 0x00040694
		public override Type GetNestedType(string fullname, BindingFlags bindingAttr)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException();
			}
			bindingAttr &= ~BindingFlags.Static;
			string text;
			string text2;
			RuntimeType.SplitName(fullname, out text, out text2);
			bool flag;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref text, out flag, out memberListType);
			RuntimeType[] nestedTypes_internal = this.GetNestedTypes_internal(text, bindingAttr, memberListType);
			RuntimeType runtimeType = null;
			foreach (RuntimeType runtimeType2 in nestedTypes_internal)
			{
				if (RuntimeType.FilterApplyType(runtimeType2, bindingAttr, text, false, text2))
				{
					if (runtimeType != null)
					{
						throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
					}
					runtimeType = runtimeType2;
				}
			}
			return runtimeType;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0004251C File Offset: 0x0004071C
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			RuntimeType.ListBuilder<MethodInfo> listBuilder = default(RuntimeType.ListBuilder<MethodInfo>);
			RuntimeType.ListBuilder<ConstructorInfo> listBuilder2 = default(RuntimeType.ListBuilder<ConstructorInfo>);
			RuntimeType.ListBuilder<PropertyInfo> listBuilder3 = default(RuntimeType.ListBuilder<PropertyInfo>);
			RuntimeType.ListBuilder<EventInfo> listBuilder4 = default(RuntimeType.ListBuilder<EventInfo>);
			RuntimeType.ListBuilder<FieldInfo> listBuilder5 = default(RuntimeType.ListBuilder<FieldInfo>);
			RuntimeType.ListBuilder<Type> listBuilder6 = default(RuntimeType.ListBuilder<Type>);
			int num = 0;
			if ((type & MemberTypes.Method) != (MemberTypes)0)
			{
				listBuilder = this.GetMethodCandidates(name, bindingAttr, CallingConventions.Any, null, -1, true);
				if (type == MemberTypes.Method)
				{
					return listBuilder.ToArray();
				}
				num += listBuilder.Count;
			}
			if ((type & MemberTypes.Constructor) != (MemberTypes)0)
			{
				listBuilder2 = this.GetConstructorCandidates(name, bindingAttr, CallingConventions.Any, null, true);
				if (type == MemberTypes.Constructor)
				{
					return listBuilder2.ToArray();
				}
				num += listBuilder2.Count;
			}
			if ((type & MemberTypes.Property) != (MemberTypes)0)
			{
				listBuilder3 = this.GetPropertyCandidates(name, bindingAttr, null, true);
				if (type == MemberTypes.Property)
				{
					return listBuilder3.ToArray();
				}
				num += listBuilder3.Count;
			}
			if ((type & MemberTypes.Event) != (MemberTypes)0)
			{
				listBuilder4 = this.GetEventCandidates(name, bindingAttr, true);
				if (type == MemberTypes.Event)
				{
					return listBuilder4.ToArray();
				}
				num += listBuilder4.Count;
			}
			if ((type & MemberTypes.Field) != (MemberTypes)0)
			{
				listBuilder5 = this.GetFieldCandidates(name, bindingAttr, true);
				if (type == MemberTypes.Field)
				{
					return listBuilder5.ToArray();
				}
				num += listBuilder5.Count;
			}
			if ((type & (MemberTypes.TypeInfo | MemberTypes.NestedType)) != (MemberTypes)0)
			{
				listBuilder6 = this.GetNestedTypeCandidates(name, bindingAttr, true);
				if (type == MemberTypes.NestedType || type == MemberTypes.TypeInfo)
				{
					return listBuilder6.ToArray();
				}
				num += listBuilder6.Count;
			}
			MemberInfo[] array;
			if (type != (MemberTypes.Constructor | MemberTypes.Method))
			{
				array = new MemberInfo[num];
			}
			else
			{
				MemberInfo[] array2 = new MethodBase[num];
				array = array2;
			}
			MemberInfo[] array3 = array;
			int num2 = 0;
			object[] array4 = array3;
			listBuilder.CopyTo(array4, num2);
			num2 += listBuilder.Count;
			array4 = array3;
			listBuilder2.CopyTo(array4, num2);
			num2 += listBuilder2.Count;
			array4 = array3;
			listBuilder3.CopyTo(array4, num2);
			num2 += listBuilder3.Count;
			array4 = array3;
			listBuilder4.CopyTo(array4, num2);
			num2 += listBuilder4.Count;
			array4 = array3;
			listBuilder5.CopyTo(array4, num2);
			num2 += listBuilder5.Count;
			array4 = array3;
			listBuilder6.CopyTo(array4, num2);
			num2 += listBuilder6.Count;
			return array3;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00042740 File Offset: 0x00040940
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00042748 File Offset: 0x00040948
		internal RuntimeModule GetRuntimeModule()
		{
			return RuntimeTypeHandle.GetModule(this);
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00042750 File Offset: 0x00040950
		public override Assembly Assembly
		{
			get
			{
				return this.GetRuntimeAssembly();
			}
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00042758 File Offset: 0x00040958
		internal RuntimeAssembly GetRuntimeAssembly()
		{
			return RuntimeTypeHandle.GetAssembly(this);
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00042760 File Offset: 0x00040960
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return new RuntimeTypeHandle(this);
			}
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00042768 File Offset: 0x00040968
		public override bool IsInstanceOfType(object o)
		{
			return RuntimeTypeHandle.IsInstanceOfType(this, o);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00042774 File Offset: 0x00040974
		public override bool IsAssignableFrom(Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (c == this)
			{
				return true;
			}
			RuntimeType runtimeType = c.UnderlyingSystemType as RuntimeType;
			if (runtimeType != null)
			{
				return RuntimeTypeHandle.CanCastTo(runtimeType, this);
			}
			if (RuntimeFeature.IsDynamicCodeSupported && c is TypeBuilder)
			{
				if (c.IsSubclassOf(this))
				{
					return true;
				}
				if (base.IsInterface)
				{
					return c.ImplementInterface(this);
				}
				if (this.IsGenericParameter)
				{
					Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
					for (int i = 0; i < genericParameterConstraints.Length; i++)
					{
						if (!genericParameterConstraints[i].IsAssignableFrom(c))
						{
							return false;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00042800 File Offset: 0x00040A00
		public override bool IsEquivalentTo(Type other)
		{
			RuntimeType runtimeType = other as RuntimeType;
			return runtimeType != null && (runtimeType == this || RuntimeTypeHandle.IsEquivalentTo(this, runtimeType));
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x0004282B File Offset: 0x00040A2B
		public override Type BaseType
		{
			get
			{
				return this.GetBaseType();
			}
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00042834 File Offset: 0x00040A34
		private RuntimeType GetBaseType()
		{
			if (base.IsInterface)
			{
				return null;
			}
			if (RuntimeTypeHandle.IsGenericVariable(this))
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				RuntimeType runtimeType = RuntimeType.ObjectType;
				foreach (RuntimeType runtimeType2 in genericParameterConstraints)
				{
					if (!runtimeType2.IsInterface)
					{
						if (runtimeType2.IsGenericParameter)
						{
							GenericParameterAttributes genericParameterAttributes = runtimeType2.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask;
							if ((genericParameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) == GenericParameterAttributes.None && (genericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) == GenericParameterAttributes.None)
							{
								goto IL_0055;
							}
						}
						runtimeType = runtimeType2;
					}
					IL_0055:;
				}
				if (runtimeType == RuntimeType.ObjectType && (this.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask & GenericParameterAttributes.NotNullableValueTypeConstraint) != GenericParameterAttributes.None)
				{
					runtimeType = RuntimeType.ValueType;
				}
				return runtimeType;
			}
			return RuntimeTypeHandle.GetBaseType(this);
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00002645 File Offset: 0x00000845
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000428C8 File Offset: 0x00040AC8
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return RuntimeTypeHandle.GetAttributes(this);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x000428D0 File Offset: 0x00040AD0
		protected override bool IsContextfulImpl()
		{
			return RuntimeTypeHandle.IsContextful(this);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x000428D8 File Offset: 0x00040AD8
		protected override bool IsByRefImpl()
		{
			return RuntimeTypeHandle.IsByRef(this);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x000428E0 File Offset: 0x00040AE0
		protected override bool IsPrimitiveImpl()
		{
			return RuntimeTypeHandle.IsPrimitive(this);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x000428E8 File Offset: 0x00040AE8
		protected override bool IsPointerImpl()
		{
			return RuntimeTypeHandle.IsPointer(this);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x000428F0 File Offset: 0x00040AF0
		protected override bool IsCOMObjectImpl()
		{
			return RuntimeTypeHandle.IsComObject(this, false);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000428F9 File Offset: 0x00040AF9
		protected override bool IsValueTypeImpl()
		{
			return !(this == typeof(ValueType)) && !(this == typeof(Enum)) && this.IsSubclassOf(typeof(ValueType));
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00042931 File Offset: 0x00040B31
		public override bool IsEnum
		{
			get
			{
				return this.GetBaseType() == RuntimeType.EnumType;
			}
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00042943 File Offset: 0x00040B43
		protected override bool HasElementTypeImpl()
		{
			return RuntimeTypeHandle.HasElementType(this);
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0004294B File Offset: 0x00040B4B
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				if (!this.IsGenericParameter)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Method may only be called on a Type for which Type.IsGenericParameter is true."));
				}
				return this.GetGenericParameterAttributes();
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x0004296B File Offset: 0x00040B6B
		internal override bool IsSzArray
		{
			get
			{
				return RuntimeTypeHandle.IsSzArray(this);
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00042973 File Offset: 0x00040B73
		protected override bool IsArrayImpl()
		{
			return RuntimeTypeHandle.IsArray(this);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0004297B File Offset: 0x00040B7B
		public override int GetArrayRank()
		{
			if (!this.IsArrayImpl())
			{
				throw new ArgumentException(Environment.GetResourceString("Must be an array type."));
			}
			return RuntimeTypeHandle.GetArrayRank(this);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0004299B File Offset: 0x00040B9B
		public override Type GetElementType()
		{
			return RuntimeTypeHandle.GetElementType(this);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x000429A4 File Offset: 0x00040BA4
		public override string[] GetEnumNames()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			string[] array = Enum.InternalGetNames(this);
			string[] array2 = new string[array.Length];
			Array.Copy(array, array2, array.Length);
			return array2;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x000429EC File Offset: 0x00040BEC
		public override Array GetEnumValues()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			ulong[] array = Enum.InternalGetValues(this);
			Array array2 = Array.CreateInstance(this, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				object obj = Enum.ToObject(this, array[i]);
				array2.SetValue(obj, i);
			}
			return array2;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00042A48 File Offset: 0x00040C48
		public override Type GetEnumUnderlyingType()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			return Enum.InternalGetUnderlyingType(this);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00042A70 File Offset: 0x00040C70
		public override bool IsEnumDefined(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			RuntimeType runtimeType = (RuntimeType)value.GetType();
			if (runtimeType.IsEnum)
			{
				if (!runtimeType.IsEquivalentTo(this))
				{
					throw new ArgumentException(Environment.GetResourceString("Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.", new object[]
					{
						runtimeType.ToString(),
						this.ToString()
					}));
				}
				runtimeType = (RuntimeType)runtimeType.GetEnumUnderlyingType();
			}
			if (runtimeType == RuntimeType.StringType)
			{
				object[] array = Enum.InternalGetNames(this);
				return Array.IndexOf<object>(array, value) >= 0;
			}
			if (Type.IsIntegerType(runtimeType))
			{
				RuntimeType runtimeType2 = Enum.InternalGetUnderlyingType(this);
				if (runtimeType2 != runtimeType)
				{
					throw new ArgumentException(Environment.GetResourceString("Enum underlying type and the object must be same type or object must be a String. Type passed in was '{0}'; the enum underlying type was '{1}'.", new object[]
					{
						runtimeType.ToString(),
						runtimeType2.ToString()
					}));
				}
				ulong[] array2 = Enum.InternalGetValues(this);
				ulong num = Enum.ToUInt64(value);
				return Array.BinarySearch<ulong>(array2, num) >= 0;
			}
			else
			{
				if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
				{
					throw new ArgumentException(Environment.GetResourceString("Enum underlying type and the object must be same type or object must be a String. Type passed in was '{0}'; the enum underlying type was '{1}'.", new object[]
					{
						runtimeType.ToString(),
						this.GetEnumUnderlyingType()
					}));
				}
				throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00042B98 File Offset: 0x00040D98
		public override string GetEnumName(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (!type.IsEnum && !Type.IsIntegerType(type))
			{
				throw new ArgumentException(Environment.GetResourceString("The value passed in must be an enum base or an underlying type for an enum, such as an Int32."), "value");
			}
			ulong[] array = Enum.InternalGetValues(this);
			ulong num = Enum.ToUInt64(value);
			int num2 = Array.BinarySearch<ulong>(array, num);
			if (num2 >= 0)
			{
				return Enum.InternalGetNames(this)[num2];
			}
			return null;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00042C01 File Offset: 0x00040E01
		internal RuntimeType[] GetGenericArgumentsInternal()
		{
			return (RuntimeType[])this.GetGenericArgumentsInternal(true);
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00042C10 File Offset: 0x00040E10
		public override Type[] GetGenericArguments()
		{
			Type[] array = this.GetGenericArgumentsInternal(false);
			if (array == null)
			{
				array = Array.Empty<Type>();
			}
			return array;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00042C30 File Offset: 0x00040E30
		public override Type MakeGenericType(params Type[] instantiation)
		{
			if (instantiation == null)
			{
				throw new ArgumentNullException("instantiation");
			}
			RuntimeType[] array = new RuntimeType[instantiation.Length];
			if (!this.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException(Environment.GetResourceString("{0} is not a GenericTypeDefinition. MakeGenericType may only be called on a type for which Type.IsGenericTypeDefinition is true.", new object[] { this }));
			}
			if (this.GetGenericArguments().Length != instantiation.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("The number of generic arguments provided doesn't equal the arity of the generic type definition."), "instantiation");
			}
			int i = 0;
			while (i < instantiation.Length)
			{
				Type type = instantiation[i];
				if (type == null)
				{
					throw new ArgumentNullException();
				}
				RuntimeType runtimeType = type as RuntimeType;
				if (runtimeType == null)
				{
					if (type.IsSignatureType)
					{
						return Type.MakeGenericSignatureType(this, instantiation);
					}
					Type[] array2 = new Type[instantiation.Length];
					for (int j = 0; j < instantiation.Length; j++)
					{
						array2[j] = instantiation[j];
					}
					instantiation = array2;
					if (!RuntimeFeature.IsDynamicCodeSupported)
					{
						throw new PlatformNotSupportedException();
					}
					return RuntimeType.MakeTypeBuilderInstantiation(this, instantiation);
				}
				else
				{
					array[i] = runtimeType;
					i++;
				}
			}
			RuntimeType[] genericArgumentsInternal = this.GetGenericArgumentsInternal();
			RuntimeType.SanityCheckGenericArguments(array, genericArgumentsInternal);
			Type[] array3 = array;
			Type type2 = RuntimeType.MakeGenericType(this, array3);
			if (type2 == null)
			{
				throw new TypeLoadException();
			}
			return type2;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00042D51 File Offset: 0x00040F51
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return RuntimeTypeHandle.IsGenericTypeDefinition(this);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00042D59 File Offset: 0x00040F59
		public override bool IsGenericParameter
		{
			get
			{
				return RuntimeTypeHandle.IsGenericVariable(this);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00042D61 File Offset: 0x00040F61
		public override int GenericParameterPosition
		{
			get
			{
				if (!this.IsGenericParameter)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Method may only be called on a Type for which Type.IsGenericParameter is true."));
				}
				return this.GetGenericParameterPosition();
			}
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x00042D81 File Offset: 0x00040F81
		public override Type GetGenericTypeDefinition()
		{
			if (!this.IsGenericType)
			{
				throw new InvalidOperationException(Environment.GetResourceString("This operation is only valid on generic types."));
			}
			return RuntimeTypeHandle.GetGenericTypeDefinition(this);
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00042DA1 File Offset: 0x00040FA1
		public override bool IsGenericType
		{
			get
			{
				return RuntimeTypeHandle.HasInstantiation(this);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00042DA9 File Offset: 0x00040FA9
		public override bool IsConstructedGenericType
		{
			get
			{
				return this.IsGenericType && !this.IsGenericTypeDefinition;
			}
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00042DC0 File Offset: 0x00040FC0
		public override MemberInfo[] GetDefaultMembers()
		{
			MemberInfo[] array = null;
			string defaultMemberName = this.GetDefaultMemberName();
			if (defaultMemberName != null)
			{
				array = base.GetMember(defaultMemberName);
			}
			if (array == null)
			{
				array = Array.Empty<MemberInfo>();
			}
			return array;
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00042DEC File Offset: 0x00040FEC
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object InvokeMember(string name, BindingFlags bindingFlags, Binder binder, object target, object[] providedArgs, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParams)
		{
			if (this.IsGenericParameter)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Method must be called on a Type for which Type.IsGenericParameter is false."));
			}
			if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) == BindingFlags.Default)
			{
				throw new ArgumentException(Environment.GetResourceString("Must specify binding flags describing the invoke operation required (BindingFlags.InvokeMethod CreateInstance GetField SetField GetProperty SetProperty)."), "bindingFlags");
			}
			if ((bindingFlags & (BindingFlags)255) == BindingFlags.Default)
			{
				bindingFlags |= BindingFlags.Instance | BindingFlags.Public;
				if ((bindingFlags & BindingFlags.CreateInstance) == BindingFlags.Default)
				{
					bindingFlags |= BindingFlags.Static;
				}
			}
			if (namedParams != null)
			{
				if (providedArgs != null)
				{
					if (namedParams.Length > providedArgs.Length)
					{
						throw new ArgumentException(Environment.GetResourceString("Named parameter array cannot be bigger than argument array."), "namedParams");
					}
				}
				else if (namedParams.Length != 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Named parameter array cannot be bigger than argument array."), "namedParams");
				}
			}
			if (target != null && target.GetType().IsCOMObject)
			{
				if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) == BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Must specify property Set or Get or method call for a COM Object."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.GetProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~(BindingFlags.InvokeMethod | BindingFlags.GetProperty)) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Cannot specify both Get and Set on a property."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~(BindingFlags.InvokeMethod | BindingFlags.GetProperty)) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Cannot specify Set on a property and Invoke on a method."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.SetProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.SetProperty) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Only one of the following binding flags can be set: BindingFlags.SetProperty, BindingFlags.PutDispProperty,  BindingFlags.PutRefDispProperty."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.PutDispProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.PutDispProperty) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Only one of the following binding flags can be set: BindingFlags.SetProperty, BindingFlags.PutDispProperty,  BindingFlags.PutRefDispProperty."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.PutRefDispProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.PutRefDispProperty) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Only one of the following binding flags can be set: BindingFlags.SetProperty, BindingFlags.PutDispProperty,  BindingFlags.PutRefDispProperty."), "bindingFlags");
				}
				if (RemotingServices.IsTransparentProxy(target))
				{
					throw new NotImplementedException();
				}
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				throw new NotImplementedException();
			}
			else
			{
				if (namedParams != null && Array.IndexOf<string>(namedParams, null) != -1)
				{
					throw new ArgumentException(Environment.GetResourceString("Named parameter value must not be null."), "namedParams");
				}
				int num = ((providedArgs != null) ? providedArgs.Length : 0);
				if (binder == null)
				{
					binder = Type.DefaultBinder;
				}
				if ((bindingFlags & BindingFlags.CreateInstance) != BindingFlags.Default)
				{
					if ((bindingFlags & BindingFlags.CreateInstance) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty)) != BindingFlags.Default)
					{
						throw new ArgumentException(Environment.GetResourceString("Cannot specify both CreateInstance and another access type."), "bindingFlags");
					}
					return Activator.CreateInstance(this, bindingFlags, binder, providedArgs, culture);
				}
				else
				{
					if ((bindingFlags & (BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) != BindingFlags.Default)
					{
						bindingFlags |= BindingFlags.SetProperty;
					}
					if (name == null)
					{
						throw new ArgumentNullException("name");
					}
					if (name.Length == 0 || name.Equals("[DISPID=0]"))
					{
						name = this.GetDefaultMemberName();
						if (name == null)
						{
							name = "ToString";
						}
					}
					bool flag = (bindingFlags & BindingFlags.GetField) > BindingFlags.Default;
					bool flag2 = (bindingFlags & BindingFlags.SetField) > BindingFlags.Default;
					if (flag || flag2)
					{
						if (flag)
						{
							if (flag2)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify both Get and Set on a field."), "bindingFlags");
							}
							if ((bindingFlags & BindingFlags.SetProperty) != BindingFlags.Default)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify both GetField and SetProperty."), "bindingFlags");
							}
						}
						else
						{
							if (providedArgs == null)
							{
								throw new ArgumentNullException("providedArgs");
							}
							if ((bindingFlags & BindingFlags.GetProperty) != BindingFlags.Default)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify both SetField and GetProperty."), "bindingFlags");
							}
							if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify Set on a Field and Invoke on a method."), "bindingFlags");
							}
						}
						FieldInfo fieldInfo = null;
						FieldInfo[] array = this.GetMember(name, MemberTypes.Field, bindingFlags) as FieldInfo[];
						if (array.Length == 1)
						{
							fieldInfo = array[0];
						}
						else if (array.Length != 0)
						{
							fieldInfo = binder.BindToField(bindingFlags, array, flag ? Empty.Value : providedArgs[0], culture);
						}
						if (fieldInfo != null)
						{
							if (fieldInfo.FieldType.IsArray || fieldInfo.FieldType == typeof(Array))
							{
								int num2;
								if ((bindingFlags & BindingFlags.GetField) != BindingFlags.Default)
								{
									num2 = num;
								}
								else
								{
									num2 = num - 1;
								}
								if (num2 > 0)
								{
									int[] array2 = new int[num2];
									for (int i = 0; i < num2; i++)
									{
										try
										{
											array2[i] = ((IConvertible)providedArgs[i]).ToInt32(null);
										}
										catch (InvalidCastException)
										{
											throw new ArgumentException(Environment.GetResourceString("All indexes must be of type Int32."));
										}
									}
									Array array3 = (Array)fieldInfo.GetValue(target);
									if ((bindingFlags & BindingFlags.GetField) != BindingFlags.Default)
									{
										return array3.GetValue(array2);
									}
									array3.SetValue(providedArgs[num2], array2);
									return null;
								}
							}
							if (flag)
							{
								if (num != 0)
								{
									throw new ArgumentException(Environment.GetResourceString("No arguments can be provided to Get a field value."), "bindingFlags");
								}
								return fieldInfo.GetValue(target);
							}
							else
							{
								if (num != 1)
								{
									throw new ArgumentException(Environment.GetResourceString("Only the field value can be specified to set a field value."), "bindingFlags");
								}
								fieldInfo.SetValue(target, providedArgs[0], bindingFlags, binder, culture);
								return null;
							}
						}
						else if ((bindingFlags & (BindingFlags)16773888) == BindingFlags.Default)
						{
							throw new MissingFieldException(this.FullName, name);
						}
					}
					bool flag3 = (bindingFlags & BindingFlags.GetProperty) > BindingFlags.Default;
					bool flag4 = (bindingFlags & BindingFlags.SetProperty) > BindingFlags.Default;
					if (flag3 || flag4)
					{
						if (flag3)
						{
							if (flag4)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify both Get and Set on a property."), "bindingFlags");
							}
						}
						else if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
						{
							throw new ArgumentException(Environment.GetResourceString("Cannot specify Set on a property and Invoke on a method."), "bindingFlags");
						}
					}
					MethodInfo[] array4 = null;
					MethodInfo methodInfo = null;
					if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
					{
						MethodInfo[] array5 = this.GetMember(name, MemberTypes.Method, bindingFlags) as MethodInfo[];
						List<MethodInfo> list = null;
						foreach (MethodInfo methodInfo2 in array5)
						{
							if (RuntimeType.FilterApplyMethodInfo((RuntimeMethodInfo)methodInfo2, bindingFlags, CallingConventions.Any, new Type[num]))
							{
								if (methodInfo == null)
								{
									methodInfo = methodInfo2;
								}
								else
								{
									if (list == null)
									{
										list = new List<MethodInfo>(array5.Length);
										list.Add(methodInfo);
									}
									list.Add(methodInfo2);
								}
							}
						}
						if (list != null)
						{
							array4 = new MethodInfo[list.Count];
							list.CopyTo(array4);
						}
					}
					if ((methodInfo == null && flag3) || flag4)
					{
						PropertyInfo[] array6 = this.GetMember(name, MemberTypes.Property, bindingFlags) as PropertyInfo[];
						List<MethodInfo> list2 = null;
						for (int k = 0; k < array6.Length; k++)
						{
							MethodInfo methodInfo3;
							if (flag4)
							{
								methodInfo3 = array6[k].GetSetMethod(true);
							}
							else
							{
								methodInfo3 = array6[k].GetGetMethod(true);
							}
							if (!(methodInfo3 == null) && RuntimeType.FilterApplyMethodInfo((RuntimeMethodInfo)methodInfo3, bindingFlags, CallingConventions.Any, new Type[num]))
							{
								if (methodInfo == null)
								{
									methodInfo = methodInfo3;
								}
								else
								{
									if (list2 == null)
									{
										list2 = new List<MethodInfo>(array6.Length);
										list2.Add(methodInfo);
									}
									list2.Add(methodInfo3);
								}
							}
						}
						if (list2 != null)
						{
							array4 = new MethodInfo[list2.Count];
							list2.CopyTo(array4);
						}
					}
					if (!(methodInfo != null))
					{
						throw new MissingMethodException(this.FullName, name);
					}
					if (array4 == null && num == 0 && methodInfo.GetParametersNoCopy().Length == 0 && (bindingFlags & BindingFlags.OptionalParamBinding) == BindingFlags.Default)
					{
						return methodInfo.Invoke(target, bindingFlags, binder, providedArgs, culture);
					}
					if (array4 == null)
					{
						array4 = new MethodInfo[] { methodInfo };
					}
					if (providedArgs == null)
					{
						providedArgs = Array.Empty<object>();
					}
					object obj = null;
					MethodBase methodBase = null;
					try
					{
						Binder binder2 = binder;
						BindingFlags bindingFlags2 = bindingFlags;
						MethodBase[] array7 = array4;
						methodBase = binder2.BindToMethod(bindingFlags2, array7, ref providedArgs, modifiers, culture, namedParams, out obj);
					}
					catch (MissingMethodException)
					{
					}
					if (methodBase == null)
					{
						throw new MissingMethodException(this.FullName, name);
					}
					object obj2 = ((MethodInfo)methodBase).Invoke(target, bindingFlags, binder, providedArgs, culture);
					if (obj != null)
					{
						binder.ReorderArgumentArray(ref providedArgs, obj);
					}
					return obj2;
				}
			}
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0004352C File Offset: 0x0004172C
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00033FD7 File Offset: 0x000321D7
		public static bool operator ==(RuntimeType left, RuntimeType right)
		{
			return left == right;
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00033FDD File Offset: 0x000321DD
		public static bool operator !=(RuntimeType left, RuntimeType right)
		{
			return left != right;
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00002645 File Offset: 0x00000845
		public object Clone()
		{
			return this;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00043532 File Offset: 0x00041732
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, this);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00043549 File Offset: 0x00041749
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, RuntimeType.ObjectType, inherit);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00043558 File Offset: 0x00041758
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "attributeType");
			}
			return MonoCustomAttrs.GetCustomAttributes(this, runtimeType, inherit);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x000435A8 File Offset: 0x000417A8
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "attributeType");
			}
			return MonoCustomAttrs.IsDefined(this, runtimeType, inherit);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x000435F8 File Offset: 0x000417F8
		internal override string FormatTypeName(bool serialization)
		{
			if (serialization)
			{
				return this.GetCachedName(TypeNameKind.SerializationName);
			}
			Type rootElementType = base.GetRootElementType();
			if (rootElementType.IsNested)
			{
				return this.Name;
			}
			string text = this.ToString();
			if (rootElementType.IsPrimitive || rootElementType == typeof(void) || rootElementType == typeof(TypedReference))
			{
				text = text.Substring("System.".Length);
			}
			return text;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0004366B File Offset: 0x0004186B
		public override MemberTypes MemberType
		{
			get
			{
				if (base.IsPublic || base.IsNotPublic)
				{
					return MemberTypes.TypeInfo;
				}
				return MemberTypes.NestedType;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x00043685 File Offset: 0x00041885
		public override Type ReflectedType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x0004368D File Offset: 0x0004188D
		public override int MetadataToken
		{
			get
			{
				return RuntimeTypeHandle.GetToken(this);
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00043698 File Offset: 0x00041898
		private void CreateInstanceCheckThis()
		{
			if (this is ReflectionOnlyType)
			{
				throw new ArgumentException(Environment.GetResourceString("It is illegal to invoke a method on a Type loaded via ReflectionOnlyGetType."));
			}
			if (this.ContainsGenericParameters)
			{
				throw new ArgumentException(Environment.GetResourceString("Cannot create an instance of {0} because Type.ContainsGenericParameters is true.", new object[] { this }));
			}
			Type rootElementType = base.GetRootElementType();
			if (rootElementType == typeof(ArgIterator))
			{
				throw new NotSupportedException(Environment.GetResourceString("Cannot dynamically create an instance of ArgIterator."));
			}
			if (rootElementType == typeof(void))
			{
				throw new NotSupportedException(Environment.GetResourceString("Cannot dynamically create an instance of System.Void."));
			}
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00043720 File Offset: 0x00041920
		internal object CreateInstanceImpl(BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, ref StackCrawlMark stackMark)
		{
			this.CreateInstanceCheckThis();
			object obj = null;
			try
			{
				try
				{
					if (activationAttributes != null)
					{
						ActivationServices.PushActivationAttributes(this, activationAttributes);
					}
					if (args == null)
					{
						args = Array.Empty<object>();
					}
					int num = args.Length;
					if (binder == null)
					{
						binder = Type.DefaultBinder;
					}
					bool flag = (bindingAttr & BindingFlags.NonPublic) == BindingFlags.Default;
					bool flag2 = (bindingAttr & BindingFlags.DoNotWrapExceptions) == BindingFlags.Default;
					if (num == 0 && (bindingAttr & BindingFlags.Public) != BindingFlags.Default && (bindingAttr & BindingFlags.Instance) != BindingFlags.Default && (this.IsGenericCOMObjectImpl() || base.IsValueType))
					{
						obj = this.CreateInstanceDefaultCtor(flag, false, true, flag2, ref stackMark);
					}
					else
					{
						ConstructorInfo[] constructors = this.GetConstructors(bindingAttr);
						List<MethodBase> list = new List<MethodBase>(constructors.Length);
						Type[] array = new Type[num];
						for (int i = 0; i < num; i++)
						{
							if (args[i] != null)
							{
								array[i] = args[i].GetType();
							}
						}
						for (int j = 0; j < constructors.Length; j++)
						{
							if (RuntimeType.FilterApplyConstructorInfo((RuntimeConstructorInfo)constructors[j], bindingAttr, CallingConventions.Any, array))
							{
								list.Add(constructors[j]);
							}
						}
						MethodBase[] array2 = new MethodBase[list.Count];
						list.CopyTo(array2);
						if (array2 != null && array2.Length == 0)
						{
							array2 = null;
						}
						if (array2 == null)
						{
							if (activationAttributes != null)
							{
								ActivationServices.PopActivationAttributes(this);
								activationAttributes = null;
							}
							throw new MissingMethodException(Environment.GetResourceString("Constructor on type '{0}' not found.", new object[] { this.FullName }));
						}
						object obj2 = null;
						MethodBase methodBase;
						try
						{
							methodBase = binder.BindToMethod(bindingAttr, array2, ref args, null, culture, null, out obj2);
						}
						catch (MissingMethodException)
						{
							methodBase = null;
						}
						if (methodBase == null)
						{
							if (activationAttributes != null)
							{
								ActivationServices.PopActivationAttributes(this);
								activationAttributes = null;
							}
							throw new MissingMethodException(Environment.GetResourceString("Constructor on type '{0}' not found.", new object[] { this.FullName }));
						}
						if (methodBase.GetParametersNoCopy().Length == 0)
						{
							if (args.Length != 0)
							{
								throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Vararg calling convention not supported."), Array.Empty<object>()));
							}
							if (activationAttributes != null && activationAttributes.Length != 0)
							{
								obj = this.ActivationCreateInstance(methodBase, bindingAttr, binder, args, culture, activationAttributes);
							}
							else
							{
								obj = Activator.CreateInstance(this, true, flag2);
							}
						}
						else
						{
							if (activationAttributes != null && activationAttributes.Length != 0)
							{
								obj = this.ActivationCreateInstance(methodBase, bindingAttr, binder, args, culture, activationAttributes);
							}
							else
							{
								obj = ((ConstructorInfo)methodBase).Invoke(bindingAttr, binder, args, culture);
							}
							if (obj2 != null)
							{
								binder.ReorderArgumentArray(ref args, obj2);
							}
						}
					}
				}
				finally
				{
					if (activationAttributes != null)
					{
						ActivationServices.PopActivationAttributes(this);
						activationAttributes = null;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return obj;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x000439A8 File Offset: 0x00041BA8
		private object ActivationCreateInstance(MethodBase invokeMethod, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			object obj = ActivationServices.CreateProxyFromAttributes(this, activationAttributes);
			if (obj != null)
			{
				invokeMethod.Invoke(obj, bindingAttr, binder, args, culture);
			}
			return obj;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x000439D0 File Offset: 0x00041BD0
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal object CreateInstanceDefaultCtor(bool publicOnly, bool skipCheckThis, bool fillCache, bool wrapExceptions, ref StackCrawlMark stackMark)
		{
			if (base.GetType() == typeof(ReflectionOnlyType))
			{
				throw new InvalidOperationException(Environment.GetResourceString("The requested operation is invalid in the ReflectionOnly context."));
			}
			return this.CreateInstanceSlow(publicOnly, wrapExceptions, skipCheckThis, fillCache);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00043A04 File Offset: 0x00041C04
		internal RuntimeConstructorInfo GetDefaultConstructor()
		{
			RuntimeConstructorInfo runtimeConstructorInfo = null;
			if (this.type_info == null)
			{
				this.type_info = new MonoTypeInfo();
			}
			else
			{
				runtimeConstructorInfo = this.type_info.default_ctor;
			}
			if (runtimeConstructorInfo == null)
			{
				ConstructorInfo[] constructors = this.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				for (int i = 0; i < constructors.Length; i++)
				{
					if (constructors[i].GetParametersCount() == 0)
					{
						runtimeConstructorInfo = (this.type_info.default_ctor = (RuntimeConstructorInfo)constructors[i]);
						break;
					}
				}
			}
			return runtimeConstructorInfo;
		}

		// Token: 0x06000FF5 RID: 4085
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo GetCorrespondingInflatedMethod(MethodInfo generic);

		// Token: 0x06000FF6 RID: 4086
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ConstructorInfo GetCorrespondingInflatedConstructor(ConstructorInfo generic);

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00043A76 File Offset: 0x00041C76
		internal override MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			if (fromNoninstanciated == null)
			{
				throw new ArgumentNullException("fromNoninstanciated");
			}
			return this.GetCorrespondingInflatedMethod(fromNoninstanciated);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00043A93 File Offset: 0x00041C93
		internal override ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			if (fromNoninstanciated == null)
			{
				throw new ArgumentNullException("fromNoninstanciated");
			}
			return this.GetCorrespondingInflatedConstructor(fromNoninstanciated);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00043AB0 File Offset: 0x00041CB0
		internal override FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			BindingFlags bindingFlags = (fromNoninstanciated.IsStatic ? BindingFlags.Static : BindingFlags.Instance);
			bindingFlags |= (fromNoninstanciated.IsPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			return this.GetField(fromNoninstanciated.Name, bindingFlags);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00043AE8 File Offset: 0x00041CE8
		private string GetDefaultMemberName()
		{
			object[] customAttributes = this.GetCustomAttributes(typeof(DefaultMemberAttribute), true);
			if (customAttributes.Length == 0)
			{
				return null;
			}
			return ((DefaultMemberAttribute)customAttributes[0]).MemberName;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00043B1C File Offset: 0x00041D1C
		internal RuntimeConstructorInfo GetSerializationCtor()
		{
			if (this.m_serializationCtor == null)
			{
				Type[] array = new Type[]
				{
					typeof(SerializationInfo),
					typeof(StreamingContext)
				};
				this.m_serializationCtor = base.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, array, null) as RuntimeConstructorInfo;
			}
			return this.m_serializationCtor;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00043B75 File Offset: 0x00041D75
		internal object CreateInstanceSlow(bool publicOnly, bool wrapExceptions, bool skipCheckThis, bool fillCache)
		{
			if (!skipCheckThis)
			{
				this.CreateInstanceCheckThis();
			}
			return this.CreateInstanceMono(!publicOnly, wrapExceptions);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00043B8C File Offset: 0x00041D8C
		private object CreateInstanceMono(bool nonPublic, bool wrapExceptions)
		{
			RuntimeConstructorInfo runtimeConstructorInfo = this.GetDefaultConstructor();
			if (!nonPublic && runtimeConstructorInfo != null && !runtimeConstructorInfo.IsPublic)
			{
				runtimeConstructorInfo = null;
			}
			if (runtimeConstructorInfo == null)
			{
				Type rootElementType = base.GetRootElementType();
				if (rootElementType == typeof(TypedReference) || rootElementType == typeof(RuntimeArgumentHandle))
				{
					throw new NotSupportedException(Environment.GetResourceString("Cannot create boxed TypedReference, ArgIterator, or RuntimeArgumentHandle Objects."));
				}
				if (base.IsValueType)
				{
					return RuntimeType.CreateInstanceInternal(this);
				}
				throw new MissingMethodException("Default constructor not found for type " + this.FullName);
			}
			else
			{
				if (base.IsAbstract)
				{
					throw new MissingMethodException("Cannot create an abstract class '{0}'.", this.FullName);
				}
				return runtimeConstructorInfo.InternalInvoke(null, null, wrapExceptions);
			}
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00043C38 File Offset: 0x00041E38
		internal object CheckValue(object value, Binder binder, CultureInfo culture, BindingFlags invokeAttr)
		{
			bool flag = false;
			object obj = this.TryConvertToType(value, ref flag);
			if (!flag)
			{
				return obj;
			}
			if ((invokeAttr & BindingFlags.ExactBinding) == BindingFlags.ExactBinding)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Object of type '{0}' cannot be converted to type '{1}'."), value.GetType(), this));
			}
			if (binder != null && binder != Type.DefaultBinder)
			{
				return binder.ChangeType(value, this, culture);
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Object of type '{0}' cannot be converted to type '{1}'."), value.GetType(), this));
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00043CBC File Offset: 0x00041EBC
		private object TryConvertToType(object value, ref bool failed)
		{
			if (this.IsInstanceOfType(value))
			{
				return value;
			}
			if (base.IsByRef)
			{
				Type elementType = this.GetElementType();
				if (value == null || elementType.IsInstanceOfType(value))
				{
					return value;
				}
			}
			if (value == null)
			{
				return value;
			}
			if (this.IsEnum)
			{
				if (Enum.GetUnderlyingType(this) == value.GetType())
				{
					return value;
				}
				object obj = RuntimeType.IsConvertibleToPrimitiveType(value, this);
				if (obj != null)
				{
					return obj;
				}
			}
			else if (base.IsPrimitive)
			{
				object obj2 = RuntimeType.IsConvertibleToPrimitiveType(value, this);
				if (obj2 != null)
				{
					return obj2;
				}
			}
			else if (base.IsPointer)
			{
				Type type = value.GetType();
				if (type == typeof(IntPtr) || type == typeof(UIntPtr))
				{
					return value;
				}
			}
			failed = true;
			return null;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00043D70 File Offset: 0x00041F70
		private static object IsConvertibleToPrimitiveType(object value, Type targetType)
		{
			Type type = value.GetType();
			if (type.IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
				if (type == targetType)
				{
					return value;
				}
			}
			TypeCode typeCode = Type.GetTypeCode(type);
			switch (Type.GetTypeCode(targetType))
			{
			case TypeCode.Char:
				if (typeCode == TypeCode.Byte)
				{
					return (char)((byte)value);
				}
				if (typeCode == TypeCode.UInt16)
				{
					return value;
				}
				break;
			case TypeCode.Int16:
				if (typeCode == TypeCode.SByte)
				{
					return (short)((sbyte)value);
				}
				if (typeCode == TypeCode.Byte)
				{
					return (short)((byte)value);
				}
				break;
			case TypeCode.UInt16:
				if (typeCode == TypeCode.Char)
				{
					return value;
				}
				if (typeCode == TypeCode.Byte)
				{
					return (ushort)((byte)value);
				}
				break;
			case TypeCode.Int32:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (int)((char)value);
				case TypeCode.SByte:
					return (int)((sbyte)value);
				case TypeCode.Byte:
					return (int)((byte)value);
				case TypeCode.Int16:
					return (int)((short)value);
				case TypeCode.UInt16:
					return (int)((ushort)value);
				}
				break;
			case TypeCode.UInt32:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (uint)((char)value);
				case TypeCode.Byte:
					return (uint)((byte)value);
				case TypeCode.UInt16:
					return (uint)((ushort)value);
				}
				break;
			case TypeCode.Int64:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (long)((ulong)((char)value));
				case TypeCode.SByte:
					return (long)((sbyte)value);
				case TypeCode.Byte:
					return (long)((ulong)((byte)value));
				case TypeCode.Int16:
					return (long)((short)value);
				case TypeCode.UInt16:
					return (long)((ulong)((ushort)value));
				case TypeCode.Int32:
					return (long)((int)value);
				case TypeCode.UInt32:
					return (long)((ulong)((uint)value));
				}
				break;
			case TypeCode.UInt64:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (ulong)((char)value);
				case TypeCode.Byte:
					return (ulong)((byte)value);
				case TypeCode.UInt16:
					return (ulong)((ushort)value);
				case TypeCode.UInt32:
					return (ulong)((uint)value);
				}
				break;
			case TypeCode.Single:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (float)((char)value);
				case TypeCode.SByte:
					return (float)((sbyte)value);
				case TypeCode.Byte:
					return (float)((byte)value);
				case TypeCode.Int16:
					return (float)((short)value);
				case TypeCode.UInt16:
					return (float)((ushort)value);
				case TypeCode.Int32:
					return (float)((int)value);
				case TypeCode.UInt32:
					return (uint)value;
				case TypeCode.Int64:
					return (float)((long)value);
				case TypeCode.UInt64:
					return (ulong)value;
				}
				break;
			case TypeCode.Double:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (double)((char)value);
				case TypeCode.SByte:
					return (double)((sbyte)value);
				case TypeCode.Byte:
					return (double)((byte)value);
				case TypeCode.Int16:
					return (double)((short)value);
				case TypeCode.UInt16:
					return (double)((ushort)value);
				case TypeCode.Int32:
					return (double)((int)value);
				case TypeCode.UInt32:
					return (uint)value;
				case TypeCode.Int64:
					return (double)((long)value);
				case TypeCode.UInt64:
					return (ulong)value;
				case TypeCode.Single:
					return (double)((float)value);
				}
				break;
			}
			return null;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00044121 File Offset: 0x00042321
		private string GetCachedName(TypeNameKind kind)
		{
			if (kind == TypeNameKind.SerializationName)
			{
				return this.ToString();
			}
			throw new NotImplementedException();
		}

		// Token: 0x06001002 RID: 4098
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type make_array_type(int rank);

		// Token: 0x06001003 RID: 4099 RVA: 0x00044133 File Offset: 0x00042333
		public override Type MakeArrayType()
		{
			return this.make_array_type(0);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0004413C File Offset: 0x0004233C
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1 || rank > 255)
			{
				throw new IndexOutOfRangeException();
			}
			return this.make_array_type(rank);
		}

		// Token: 0x06001005 RID: 4101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type make_byref_type();

		// Token: 0x06001006 RID: 4102 RVA: 0x00044157 File Offset: 0x00042357
		public override Type MakeByRefType()
		{
			if (base.IsByRef)
			{
				throw new TypeLoadException("Can not call MakeByRefType on a ByRef type");
			}
			return this.make_byref_type();
		}

		// Token: 0x06001007 RID: 4103
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type MakePointerType(Type type);

		// Token: 0x06001008 RID: 4104 RVA: 0x00044172 File Offset: 0x00042372
		public override Type MakePointerType()
		{
			if (base.IsByRef)
			{
				throw new TypeLoadException(string.Format("Could not load type '{0}' from assembly '{1}", base.GetType(), this.AssemblyQualifiedName));
			}
			return RuntimeType.MakePointerType(this);
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x000441A0 File Offset: 0x000423A0
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (this.IsGenericType)
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
				return base.HasElementType && this.GetElementType().ContainsGenericParameters;
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x000441F8 File Offset: 0x000423F8
		public override Type[] GetGenericParameterConstraints()
		{
			if (!this.IsGenericParameter)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Method may only be called on a Type for which Type.IsGenericParameter is true."));
			}
			RuntimeGenericParamInfoHandle runtimeGenericParamInfoHandle = new RuntimeGenericParamInfoHandle(RuntimeTypeHandle.GetGenericParameterInfo(this));
			Type[] array = runtimeGenericParamInfoHandle.Constraints;
			if (array == null)
			{
				array = EmptyArray<Type>.Value;
			}
			return array;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0004423C File Offset: 0x0004243C
		internal static object CreateInstanceForAnotherGenericParameter(Type genericType, RuntimeType genericArgument)
		{
			return ((RuntimeType)RuntimeType.MakeGenericType(genericType, new Type[] { genericArgument })).GetDefaultConstructor().InternalInvoke(null, null, true);
		}

		// Token: 0x0600100C RID: 4108
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type MakeGenericType(Type gt, Type[] types);

		// Token: 0x0600100D RID: 4109
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetMethodsByName_native(IntPtr namePtr, BindingFlags bindingAttr, RuntimeType.MemberListType listType);

		// Token: 0x0600100E RID: 4110 RVA: 0x00044260 File Offset: 0x00042460
		internal RuntimeMethodInfo[] GetMethodsByName(string name, BindingFlags bindingAttr, RuntimeType.MemberListType listType, RuntimeType reflectedType)
		{
			RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(reflectedType);
			RuntimeMethodInfo[] array2;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(name))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetMethodsByName_native(safeStringMarshal.Value, bindingAttr, listType)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimeMethodInfo[] array = new RuntimeMethodInfo[length];
					for (int i = 0; i < length; i++)
					{
						RuntimeMethodHandle runtimeMethodHandle = new RuntimeMethodHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimeMethodInfo)RuntimeMethodInfo.GetMethodFromHandleNoGenericCheck(runtimeMethodHandle, runtimeTypeHandle);
					}
					array2 = array;
				}
			}
			return array2;
		}

		// Token: 0x0600100F RID: 4111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetPropertiesByName_native(IntPtr name, BindingFlags bindingAttr, RuntimeType.MemberListType listType);

		// Token: 0x06001010 RID: 4112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetConstructors_native(BindingFlags bindingAttr);

		// Token: 0x06001011 RID: 4113 RVA: 0x00044318 File Offset: 0x00042518
		private RuntimeConstructorInfo[] GetConstructors_internal(BindingFlags bindingAttr, RuntimeType reflectedType)
		{
			RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(reflectedType);
			RuntimeConstructorInfo[] array2;
			using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetConstructors_native(bindingAttr)))
			{
				int length = safeGPtrArrayHandle.Length;
				RuntimeConstructorInfo[] array = new RuntimeConstructorInfo[length];
				for (int i = 0; i < length; i++)
				{
					RuntimeMethodHandle runtimeMethodHandle = new RuntimeMethodHandle(safeGPtrArrayHandle[i]);
					array[i] = (RuntimeConstructorInfo)RuntimeMethodInfo.GetMethodFromHandleNoGenericCheck(runtimeMethodHandle, runtimeTypeHandle);
				}
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x000443A0 File Offset: 0x000425A0
		private RuntimePropertyInfo[] GetPropertiesByName(string name, BindingFlags bindingAttr, RuntimeType.MemberListType listType, RuntimeType reflectedType)
		{
			RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(reflectedType);
			RuntimePropertyInfo[] array2;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(name))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetPropertiesByName_native(safeStringMarshal.Value, bindingAttr, listType)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimePropertyInfo[] array = new RuntimePropertyInfo[length];
					for (int i = 0; i < length; i++)
					{
						RuntimePropertyHandle runtimePropertyHandle = new RuntimePropertyHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimePropertyInfo)RuntimePropertyInfo.GetPropertyFromHandle(runtimePropertyHandle, runtimeTypeHandle);
					}
					array2 = array;
				}
			}
			return array2;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00044458 File Offset: 0x00042658
		public override InterfaceMapping GetInterfaceMap(Type ifaceType)
		{
			if (this.IsGenericParameter)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Method must be called on a Type for which Type.IsGenericParameter is false."));
			}
			if (ifaceType == null)
			{
				throw new ArgumentNullException("ifaceType");
			}
			if (ifaceType as RuntimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a runtime Type object."), "ifaceType");
			}
			if (!ifaceType.IsInterface)
			{
				throw new ArgumentException("Argument must be an interface.", "ifaceType");
			}
			if (base.IsInterface)
			{
				throw new ArgumentException("'this' type cannot be an interface itself");
			}
			InterfaceMapping interfaceMapping;
			interfaceMapping.TargetType = this;
			interfaceMapping.InterfaceType = ifaceType;
			RuntimeType.GetInterfaceMapData(this, ifaceType, out interfaceMapping.TargetMethods, out interfaceMapping.InterfaceMethods);
			if (interfaceMapping.TargetMethods == null)
			{
				throw new ArgumentException("Interface not found", "ifaceType");
			}
			return interfaceMapping;
		}

		// Token: 0x06001014 RID: 4116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInterfaceMapData(Type t, Type iface, out MethodInfo[] targets, out MethodInfo[] methods);

		// Token: 0x06001015 RID: 4117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGUID(Type type, byte[] guid);

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x00044518 File Offset: 0x00042718
		public override Guid GUID
		{
			get
			{
				byte[] array = new byte[16];
				RuntimeType.GetGUID(this, array);
				return new Guid(array);
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0004453C File Offset: 0x0004273C
		internal static Type GetTypeFromCLSIDImpl(Guid clsid, string server, bool throwOnError)
		{
			if (RuntimeType.clsid_types == null)
			{
				Dictionary<Guid, Type> dictionary = new Dictionary<Guid, Type>();
				Interlocked.CompareExchange<Dictionary<Guid, Type>>(ref RuntimeType.clsid_types, dictionary, null);
			}
			Dictionary<Guid, Type> dictionary2 = RuntimeType.clsid_types;
			Type type2;
			lock (dictionary2)
			{
				Type type;
				if (RuntimeType.clsid_types.TryGetValue(clsid, out type))
				{
					type2 = type;
				}
				else
				{
					if (RuntimeType.clsid_assemblybuilder == null)
					{
						RuntimeType.clsid_assemblybuilder = new AssemblyBuilder(new AssemblyName
						{
							Name = "GetTypeFromCLSIDDummyAssembly"
						}, null, AssemblyBuilderAccess.Run, true);
					}
					TypeBuilder typeBuilder = RuntimeType.clsid_assemblybuilder.DefineDynamicModule(clsid.ToString()).DefineType("System.__ComObject", TypeAttributes.Public, typeof(__ComObject));
					Type[] array = new Type[] { typeof(string) };
					CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(typeof(GuidAttribute).GetConstructor(array), new object[] { clsid.ToString() });
					typeBuilder.SetCustomAttribute(customAttributeBuilder);
					customAttributeBuilder = new CustomAttributeBuilder(typeof(ComImportAttribute).GetConstructor(Type.EmptyTypes), new object[0]);
					typeBuilder.SetCustomAttribute(customAttributeBuilder);
					type = typeBuilder.CreateType();
					RuntimeType.clsid_types.Add(clsid, type);
					type2 = type;
				}
			}
			return type2;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0004469C File Offset: 0x0004289C
		protected override TypeCode GetTypeCodeImpl()
		{
			return RuntimeType.GetTypeCodeImplInternal(this);
		}

		// Token: 0x06001019 RID: 4121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TypeCode GetTypeCodeImplInternal(Type type);

		// Token: 0x0600101A RID: 4122 RVA: 0x000446A4 File Offset: 0x000428A4
		public override string ToString()
		{
			return this.getFullName(false, false);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00033991 File Offset: 0x00031B91
		private bool IsGenericCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x0600101C RID: 4124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object CreateInstanceInternal(Type type);

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600101D RID: 4125
		public override extern MethodBase DeclaringMethod
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600101E RID: 4126
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string getFullName(bool full_name, bool assembly_qualified);

		// Token: 0x0600101F RID: 4127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type[] GetGenericArgumentsInternal(bool runtimeArray);

		// Token: 0x06001020 RID: 4128 RVA: 0x000446B0 File Offset: 0x000428B0
		private GenericParameterAttributes GetGenericParameterAttributes()
		{
			return new RuntimeGenericParamInfoHandle(RuntimeTypeHandle.GetGenericParameterInfo(this)).Attributes;
		}

		// Token: 0x06001021 RID: 4129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetGenericParameterPosition();

		// Token: 0x06001022 RID: 4130
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetEvents_native(IntPtr name, RuntimeType.MemberListType listType);

		// Token: 0x06001023 RID: 4131
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetFields_native(IntPtr name, BindingFlags bindingAttr, RuntimeType.MemberListType listType);

		// Token: 0x06001024 RID: 4132 RVA: 0x000446D0 File Offset: 0x000428D0
		private RuntimeFieldInfo[] GetFields_internal(string name, BindingFlags bindingAttr, RuntimeType.MemberListType listType, RuntimeType reflectedType)
		{
			RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(reflectedType);
			RuntimeFieldInfo[] array2;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(name))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetFields_native(safeStringMarshal.Value, bindingAttr, listType)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimeFieldInfo[] array = new RuntimeFieldInfo[length];
					for (int i = 0; i < length; i++)
					{
						RuntimeFieldHandle runtimeFieldHandle = new RuntimeFieldHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimeFieldInfo)FieldInfo.GetFieldFromHandle(runtimeFieldHandle, runtimeTypeHandle);
					}
					array2 = array;
				}
			}
			return array2;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00044788 File Offset: 0x00042988
		private RuntimeEventInfo[] GetEvents_internal(string name, BindingFlags bindingAttr, RuntimeType.MemberListType listType, RuntimeType reflectedType)
		{
			RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(reflectedType);
			RuntimeEventInfo[] array2;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(name))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetEvents_native(safeStringMarshal.Value, listType)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimeEventInfo[] array = new RuntimeEventInfo[length];
					for (int i = 0; i < length; i++)
					{
						RuntimeEventHandle runtimeEventHandle = new RuntimeEventHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimeEventInfo)EventInfo.GetEventFromHandle(runtimeEventHandle, runtimeTypeHandle);
					}
					array2 = array;
				}
			}
			return array2;
		}

		// Token: 0x06001026 RID: 4134
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern Type[] GetInterfaces();

		// Token: 0x06001027 RID: 4135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetNestedTypes_native(IntPtr name, BindingFlags bindingAttr, RuntimeType.MemberListType listType);

		// Token: 0x06001028 RID: 4136 RVA: 0x00044840 File Offset: 0x00042A40
		private RuntimeType[] GetNestedTypes_internal(string displayName, BindingFlags bindingAttr, RuntimeType.MemberListType listType)
		{
			string text = null;
			if (displayName != null)
			{
				text = TypeIdentifiers.FromDisplay(displayName).InternalName;
			}
			RuntimeType[] array2;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(text))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetNestedTypes_native(safeStringMarshal.Value, bindingAttr, listType)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimeType[] array = new RuntimeType[length];
					for (int i = 0; i < length; i++)
					{
						RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimeType)Type.GetTypeFromHandle(runtimeTypeHandle);
					}
					array2 = array;
				}
			}
			return array2;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x00044900 File Offset: 0x00042B00
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.getFullName(true, true);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600102A RID: 4138
		public override extern Type DeclaringType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600102B RID: 4139
		public override extern string Name
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600102C RID: 4140
		public override extern string Namespace
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0004490C File Offset: 0x00042B0C
		public override int GetHashCode()
		{
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType != null && underlyingSystemType != this)
			{
				return underlyingSystemType.GetHashCode();
			}
			return (int)this._impl.Value;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0004494C File Offset: 0x00042B4C
		public override string FullName
		{
			get
			{
				if (this.ContainsGenericParameters && !base.GetRootElementType().IsGenericTypeDefinition)
				{
					return null;
				}
				if (this.type_info == null)
				{
					this.type_info = new MonoTypeInfo();
				}
				string text;
				if ((text = this.type_info.full_name) == null)
				{
					text = (this.type_info.full_name = this.getFullName(true, false));
				}
				return text;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x000449AA File Offset: 0x00042BAA
		public override bool IsSZArray
		{
			get
			{
				return base.IsArray && this == this.GetElementType().MakeArrayType();
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x00033991 File Offset: 0x00031B91
		internal override bool IsUserType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x000449C4 File Offset: 0x00042BC4
		[ComVisible(true)]
		public override bool IsSubclassOf(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type as RuntimeType;
			return !(runtimeType == null) && RuntimeTypeHandle.IsSubclassOf(this, runtimeType);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x000449F8 File Offset: 0x00042BF8
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConv, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethodImplCommon(name, -1, bindingAttr, binder, callConv, types, modifiers);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00044A0C File Offset: 0x00042C0C
		private MethodInfo GetMethodImplCommon(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConv, Type[] types, ParameterModifier[] modifiers)
		{
			RuntimeType.ListBuilder<MethodInfo> methodCandidates = this.GetMethodCandidates(name, genericParameterCount, bindingAttr, callConv, types, false);
			if (methodCandidates.Count == 0)
			{
				return null;
			}
			MethodBase[] array;
			if (types == null || types.Length == 0)
			{
				MethodInfo methodInfo = methodCandidates[0];
				if (methodCandidates.Count == 1)
				{
					return methodInfo;
				}
				if (types == null)
				{
					for (int i = 1; i < methodCandidates.Count; i++)
					{
						if (!global::System.DefaultBinder.CompareMethodSig(methodCandidates[i], methodInfo))
						{
							throw new AmbiguousMatchException("Ambiguous match found.");
						}
					}
					array = methodCandidates.ToArray();
					return global::System.DefaultBinder.FindMostDerivedNewSlotMeth(array, methodCandidates.Count) as MethodInfo;
				}
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			Binder binder2 = binder;
			array = methodCandidates.ToArray();
			return binder2.SelectMethod(bindingAttr, array, types, modifiers) as MethodInfo;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00044AC4 File Offset: 0x00042CC4
		private RuntimeType.ListBuilder<MethodInfo> GetMethodCandidates(string name, int genericParameterCount, BindingFlags bindingAttr, CallingConventions callConv, Type[] types, bool allowPrefixLookup)
		{
			bool flag;
			bool flag2;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out flag2, out memberListType);
			RuntimeMethodInfo[] methodsByName = this.GetMethodsByName(name, bindingAttr, memberListType, this);
			RuntimeType.ListBuilder<MethodInfo> listBuilder = new RuntimeType.ListBuilder<MethodInfo>(methodsByName.Length);
			foreach (RuntimeMethodInfo runtimeMethodInfo in methodsByName)
			{
				if ((genericParameterCount == -1 || genericParameterCount == runtimeMethodInfo.GenericParameterCount) && RuntimeType.FilterApplyMethodInfo(runtimeMethodInfo, bindingAttr, callConv, types) && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeMethodInfo, name, flag2)))
				{
					listBuilder.Add(runtimeMethodInfo);
				}
			}
			return listBuilder;
		}

		// Token: 0x04000622 RID: 1570
		internal static readonly RuntimeType ValueType = (RuntimeType)typeof(ValueType);

		// Token: 0x04000623 RID: 1571
		internal static readonly RuntimeType EnumType = (RuntimeType)typeof(Enum);

		// Token: 0x04000624 RID: 1572
		private static readonly RuntimeType ObjectType = (RuntimeType)typeof(object);

		// Token: 0x04000625 RID: 1573
		private static readonly RuntimeType StringType = (RuntimeType)typeof(string);

		// Token: 0x04000626 RID: 1574
		private static readonly RuntimeType DelegateType = (RuntimeType)typeof(Delegate);

		// Token: 0x04000627 RID: 1575
		private static Type[] s_SICtorParamTypes;

		// Token: 0x04000628 RID: 1576
		internal static Func<Type, Type[], Type> MakeTypeBuilderInstantiation;

		// Token: 0x04000629 RID: 1577
		private const BindingFlags MemberBindingMask = (BindingFlags)255;

		// Token: 0x0400062A RID: 1578
		private const BindingFlags InvocationMask = BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty;

		// Token: 0x0400062B RID: 1579
		private const BindingFlags BinderNonCreateInstance = BindingFlags.InvokeMethod | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty;

		// Token: 0x0400062C RID: 1580
		private const BindingFlags BinderGetSetProperty = BindingFlags.GetProperty | BindingFlags.SetProperty;

		// Token: 0x0400062D RID: 1581
		private const BindingFlags BinderSetInvokeProperty = BindingFlags.InvokeMethod | BindingFlags.SetProperty;

		// Token: 0x0400062E RID: 1582
		private const BindingFlags BinderGetSetField = BindingFlags.GetField | BindingFlags.SetField;

		// Token: 0x0400062F RID: 1583
		private const BindingFlags BinderSetInvokeField = BindingFlags.InvokeMethod | BindingFlags.SetField;

		// Token: 0x04000630 RID: 1584
		private const BindingFlags BinderNonFieldGetSet = (BindingFlags)16773888;

		// Token: 0x04000631 RID: 1585
		private const BindingFlags ClassicBindingMask = BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty;

		// Token: 0x04000632 RID: 1586
		private static RuntimeType s_typedRef = (RuntimeType)typeof(TypedReference);

		// Token: 0x04000633 RID: 1587
		[NonSerialized]
		private MonoTypeInfo type_info;

		// Token: 0x04000634 RID: 1588
		internal object GenericCache;

		// Token: 0x04000635 RID: 1589
		private RuntimeConstructorInfo m_serializationCtor;

		// Token: 0x04000636 RID: 1590
		private static Dictionary<Guid, Type> clsid_types;

		// Token: 0x04000637 RID: 1591
		private static AssemblyBuilder clsid_assemblybuilder;

		// Token: 0x04000638 RID: 1592
		private const int GenericParameterCountAny = -1;

		// Token: 0x020001AB RID: 427
		internal enum MemberListType
		{
			// Token: 0x0400063A RID: 1594
			All,
			// Token: 0x0400063B RID: 1595
			CaseSensitive,
			// Token: 0x0400063C RID: 1596
			CaseInsensitive,
			// Token: 0x0400063D RID: 1597
			HandleToInfo
		}

		// Token: 0x020001AC RID: 428
		private struct ListBuilder<T> where T : class
		{
			// Token: 0x06001036 RID: 4150 RVA: 0x00044BC9 File Offset: 0x00042DC9
			public ListBuilder(int capacity)
			{
				this._items = null;
				this._item = default(T);
				this._count = 0;
				this._capacity = capacity;
			}

			// Token: 0x17000170 RID: 368
			public T this[int index]
			{
				get
				{
					if (this._items == null)
					{
						return this._item;
					}
					return this._items[index];
				}
			}

			// Token: 0x06001038 RID: 4152 RVA: 0x00044C0C File Offset: 0x00042E0C
			public T[] ToArray()
			{
				if (this._count == 0)
				{
					return Array.Empty<T>();
				}
				if (this._count == 1)
				{
					return new T[] { this._item };
				}
				Array.Resize<T>(ref this._items, this._count);
				this._capacity = this._count;
				return this._items;
			}

			// Token: 0x06001039 RID: 4153 RVA: 0x00044C67 File Offset: 0x00042E67
			public void CopyTo(object[] array, int index)
			{
				if (this._count == 0)
				{
					return;
				}
				if (this._count == 1)
				{
					array[index] = this._item;
					return;
				}
				Array.Copy(this._items, 0, array, index, this._count);
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x0600103A RID: 4154 RVA: 0x00044C9E File Offset: 0x00042E9E
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x0600103B RID: 4155 RVA: 0x00044CA8 File Offset: 0x00042EA8
			public void Add(T item)
			{
				if (this._count == 0)
				{
					this._item = item;
				}
				else
				{
					if (this._count == 1)
					{
						if (this._capacity < 2)
						{
							this._capacity = 4;
						}
						this._items = new T[this._capacity];
						this._items[0] = this._item;
					}
					else if (this._capacity == this._count)
					{
						int num = 2 * this._capacity;
						Array.Resize<T>(ref this._items, num);
						this._capacity = num;
					}
					this._items[this._count] = item;
				}
				this._count++;
			}

			// Token: 0x0400063E RID: 1598
			private T[] _items;

			// Token: 0x0400063F RID: 1599
			private T _item;

			// Token: 0x04000640 RID: 1600
			private int _count;

			// Token: 0x04000641 RID: 1601
			private int _capacity;
		}
	}
}
