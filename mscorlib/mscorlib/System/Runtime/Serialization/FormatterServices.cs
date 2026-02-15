using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters;
using System.Security;
using System.Text;

namespace System.Runtime.Serialization
{
	/// <summary>Provides static methods to aid with the implementation of a <see cref="T:System.Runtime.Serialization.Formatter" /> for serialization. This class cannot be inherited.</summary>
	// Token: 0x020004B7 RID: 1207
	[ComVisible(true)]
	public static class FormatterServices
	{
		// Token: 0x0600266A RID: 9834 RVA: 0x0009ADA4 File Offset: 0x00098FA4
		private static MemberInfo[] GetSerializableMembers(RuntimeType type)
		{
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			int num = 0;
			for (int i = 0; i < fields.Length; i++)
			{
				if ((fields[i].Attributes & FieldAttributes.NotSerialized) != FieldAttributes.NotSerialized)
				{
					num++;
				}
			}
			if (num != fields.Length)
			{
				FieldInfo[] array = new FieldInfo[num];
				num = 0;
				for (int j = 0; j < fields.Length; j++)
				{
					if ((fields[j].Attributes & FieldAttributes.NotSerialized) != FieldAttributes.NotSerialized)
					{
						array[num] = fields[j];
						num++;
					}
				}
				return array;
			}
			return fields;
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x0009AE30 File Offset: 0x00099030
		private static bool CheckSerializable(RuntimeType type)
		{
			return type.IsSerializable;
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x0009AE40 File Offset: 0x00099040
		private static MemberInfo[] InternalGetSerializableMembers(RuntimeType type)
		{
			if (type.IsInterface)
			{
				return new MemberInfo[0];
			}
			if (!FormatterServices.CheckSerializable(type))
			{
				throw new SerializationException(Environment.GetResourceString("Type '{0}' in Assembly '{1}' is not marked as serializable.", new object[]
				{
					type.FullName,
					type.Module.Assembly.FullName
				}));
			}
			MemberInfo[] array = FormatterServices.GetSerializableMembers(type);
			RuntimeType runtimeType = (RuntimeType)type.BaseType;
			if (runtimeType != null && runtimeType != (RuntimeType)typeof(object))
			{
				RuntimeType[] array2 = null;
				int num = 0;
				bool parentTypes = FormatterServices.GetParentTypes(runtimeType, out array2, out num);
				if (num > 0)
				{
					List<SerializationFieldInfo> list = new List<SerializationFieldInfo>();
					for (int i = 0; i < num; i++)
					{
						runtimeType = array2[i];
						if (!FormatterServices.CheckSerializable(runtimeType))
						{
							throw new SerializationException(Environment.GetResourceString("Type '{0}' in Assembly '{1}' is not marked as serializable.", new object[]
							{
								runtimeType.FullName,
								runtimeType.Module.Assembly.FullName
							}));
						}
						FieldInfo[] fields = runtimeType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
						string text = (parentTypes ? runtimeType.Name : runtimeType.FullName);
						foreach (FieldInfo fieldInfo in fields)
						{
							if (!fieldInfo.IsNotSerialized)
							{
								list.Add(new SerializationFieldInfo((RuntimeFieldInfo)fieldInfo, text));
							}
						}
					}
					if (list != null && list.Count > 0)
					{
						MemberInfo[] array4 = new MemberInfo[list.Count + array.Length];
						Array.Copy(array, array4, array.Length);
						((ICollection)list).CopyTo(array4, array.Length);
						array = array4;
					}
				}
			}
			return array;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x0009AFD4 File Offset: 0x000991D4
		private static bool GetParentTypes(RuntimeType parentType, out RuntimeType[] parentTypes, out int parentTypeCount)
		{
			parentTypes = null;
			parentTypeCount = 0;
			bool flag = true;
			RuntimeType runtimeType = (RuntimeType)typeof(object);
			RuntimeType runtimeType2 = parentType;
			while (runtimeType2 != runtimeType)
			{
				if (runtimeType2 == null)
				{
					throw new InvalidOperationException(string.Format("Type '{0}' of type '{1}' does not derive from System.Object", parentType, (parentType != null) ? parentType.GetType() : null));
				}
				if (!runtimeType2.IsInterface)
				{
					string name = runtimeType2.Name;
					int num = 0;
					while (flag && num < parentTypeCount)
					{
						string name2 = parentTypes[num].Name;
						if (name2.Length == name.Length && name2[0] == name[0] && name == name2)
						{
							flag = false;
							break;
						}
						num++;
					}
					if (parentTypes == null || parentTypeCount == parentTypes.Length)
					{
						RuntimeType[] array = new RuntimeType[Math.Max(parentTypeCount * 2, 12)];
						if (parentTypes != null)
						{
							Array.Copy(parentTypes, 0, array, 0, parentTypeCount);
						}
						parentTypes = array;
					}
					RuntimeType[] array2 = parentTypes;
					int num2 = parentTypeCount;
					parentTypeCount = num2 + 1;
					array2[num2] = runtimeType2;
				}
				runtimeType2 = (RuntimeType)runtimeType2.BaseType;
			}
			return flag;
		}

		/// <summary>Gets all the serializable members for a class of the specified <see cref="T:System.Type" /> and in the provided <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.MemberInfo" /> of the non-transient, non-static members.</returns>
		/// <param name="type">The type being serialized or cloned. </param>
		/// <param name="context">The context where the serialization occurs. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x0600266E RID: 9838 RVA: 0x0009B0E4 File Offset: 0x000992E4
		public static MemberInfo[] GetSerializableMembers(Type type, StreamingContext context)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(type is RuntimeType))
			{
				throw new SerializationException(Environment.GetResourceString("Only system-provided types can be passed to the GetUninitializedObject method. '{0}' is not a valid instance of a type.", new object[] { type.ToString() }));
			}
			MemberHolder memberHolder = new MemberHolder(type, context);
			return FormatterServices.m_MemberInfoTable.GetOrAdd(memberHolder, (MemberHolder _) => FormatterServices.InternalGetSerializableMembers((RuntimeType)type));
		}

		/// <summary>Determines whether the specified <see cref="T:System.Type" /> can be deserialized with the <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> property set to Low.</summary>
		/// <param name="t">The <see cref="T:System.Type" /> to check for the ability to deserialize. </param>
		/// <param name="securityLevel">The <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> property value. </param>
		/// <exception cref="T:System.Security.SecurityException">The <paramref name="t" /> parameter is an advanced type and cannot be deserialized when the <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> property is set to Low. </exception>
		// Token: 0x0600266F RID: 9839 RVA: 0x0009B168 File Offset: 0x00099368
		public static void CheckTypeSecurity(Type t, TypeFilterLevel securityLevel)
		{
			if (securityLevel == TypeFilterLevel.Low)
			{
				for (int i = 0; i < FormatterServices.advancedTypes.Length; i++)
				{
					if (FormatterServices.advancedTypes[i].IsAssignableFrom(t))
					{
						throw new SecurityException(Environment.GetResourceString("Type {0} and the types derived from it (such as {1}) are not permitted to be deserialized at this security level.", new object[]
						{
							FormatterServices.advancedTypes[i].FullName,
							t.FullName
						}));
					}
				}
			}
		}

		/// <summary>Creates a new instance of the specified object type.</summary>
		/// <returns>A zeroed object of the specified type.</returns>
		/// <param name="type">The type of object to create. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06002670 RID: 9840 RVA: 0x0009B1C8 File Offset: 0x000993C8
		public static object GetUninitializedObject(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(type is RuntimeType))
			{
				throw new SerializationException(Environment.GetResourceString("Only system-provided types can be passed to the GetUninitializedObject method. '{0}' is not a valid instance of a type.", new object[] { type.ToString() }));
			}
			return FormatterServices.nativeGetUninitializedObject((RuntimeType)type);
		}

		/// <summary>Creates a new instance of the specified object type.</summary>
		/// <returns>A zeroed object of the specified type.</returns>
		/// <param name="type">The type of object to create. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="type" /> parameter is not a valid common language runtime type. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06002671 RID: 9841 RVA: 0x0009B218 File Offset: 0x00099418
		public static object GetSafeUninitializedObject(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(type is RuntimeType))
			{
				throw new SerializationException(Environment.GetResourceString("Only system-provided types can be passed to the GetUninitializedObject method. '{0}' is not a valid instance of a type.", new object[] { type.ToString() }));
			}
			if (type == typeof(ConstructionCall) || type == typeof(LogicalCallContext) || type == typeof(SynchronizationAttribute))
			{
				return FormatterServices.nativeGetUninitializedObject((RuntimeType)type);
			}
			object obj;
			try
			{
				obj = FormatterServices.nativeGetSafeUninitializedObject((RuntimeType)type);
			}
			catch (SecurityException ex)
			{
				throw new SerializationException(Environment.GetResourceString("Because of security restrictions, the type {0} cannot be accessed.", new object[] { type.FullName }), ex);
			}
			return obj;
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x0009B2D0 File Offset: 0x000994D0
		private static object nativeGetUninitializedObject(RuntimeType type)
		{
			return ActivationServices.AllocateUninitializedClassInstance(type);
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x0009B2D0 File Offset: 0x000994D0
		private static object nativeGetSafeUninitializedObject(RuntimeType type)
		{
			return ActivationServices.AllocateUninitializedClassInstance(type);
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x00033991 File Offset: 0x00031B91
		private static bool GetEnableUnsafeTypeForwarders()
		{
			return false;
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x0009B2D8 File Offset: 0x000994D8
		internal static bool UnsafeTypeForwardersIsEnabled()
		{
			if (!FormatterServices.unsafeTypeForwardersIsEnabledInitialized)
			{
				FormatterServices.unsafeTypeForwardersIsEnabled = FormatterServices.GetEnableUnsafeTypeForwarders();
				FormatterServices.unsafeTypeForwardersIsEnabledInitialized = true;
			}
			return FormatterServices.unsafeTypeForwardersIsEnabled;
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x0009B2FC File Offset: 0x000994FC
		internal static void SerializationSetValue(MemberInfo fi, object target, object value)
		{
			RtFieldInfo rtFieldInfo = fi as RtFieldInfo;
			if (rtFieldInfo != null)
			{
				rtFieldInfo.CheckConsistency(target);
				rtFieldInfo.UnsafeSetValue(target, value, BindingFlags.Default, FormatterServices.s_binder, null);
				return;
			}
			SerializationFieldInfo serializationFieldInfo = fi as SerializationFieldInfo;
			if (serializationFieldInfo != null)
			{
				serializationFieldInfo.InternalSetValue(target, value, BindingFlags.Default, FormatterServices.s_binder, null);
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("The FieldInfo object is not valid."));
		}

		/// <summary>Populates the specified object with values for each field drawn from the data array of objects.</summary>
		/// <returns>The newly populated object.</returns>
		/// <param name="obj">The object to populate. </param>
		/// <param name="members">An array of <see cref="T:System.Reflection.MemberInfo" /> that describes which fields and properties to populate. </param>
		/// <param name="data">An array of <see cref="T:System.Object" /> that specifies the values for each field and property to populate. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" />, <paramref name="members" />, or <paramref name="data" /> parameter is null.An element of <paramref name="members" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="members" /> does not match the length of <paramref name="data" />. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element of <paramref name="members" /> is not an instance of <see cref="T:System.Reflection.FieldInfo" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06002677 RID: 9847 RVA: 0x0009B360 File Offset: 0x00099560
		public static object PopulateObjectMembers(object obj, MemberInfo[] members, object[] data)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (members == null)
			{
				throw new ArgumentNullException("members");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (members.Length != data.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Parameters 'members' and 'data' must have the same length."));
			}
			for (int i = 0; i < members.Length; i++)
			{
				MemberInfo memberInfo = members[i];
				if (memberInfo == null)
				{
					throw new ArgumentNullException("members", Environment.GetResourceString("Member at position {0} was null.", new object[] { i }));
				}
				if (data[i] != null)
				{
					if (memberInfo.MemberType != MemberTypes.Field)
					{
						throw new SerializationException(Environment.GetResourceString("Only FieldInfo, PropertyInfo, and SerializationMemberInfo are recognized."));
					}
					FormatterServices.SerializationSetValue(memberInfo, obj, data[i]);
				}
			}
			return obj;
		}

		/// <summary>Extracts the data from the specified object and returns it as an array of objects.</summary>
		/// <returns>An array of <see cref="T:System.Object" /> that contains data stored in <paramref name="members" /> and associated with <paramref name="obj" />.</returns>
		/// <param name="obj">The object to write to the formatter. </param>
		/// <param name="members">The members to extract from the object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> or <paramref name="members" /> parameter is null.An element of <paramref name="members" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element of <paramref name="members" /> does not represent a field. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06002678 RID: 9848 RVA: 0x0009B41C File Offset: 0x0009961C
		public static object[] GetObjectData(object obj, MemberInfo[] members)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (members == null)
			{
				throw new ArgumentNullException("members");
			}
			int num = members.Length;
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				MemberInfo memberInfo = members[i];
				if (memberInfo == null)
				{
					throw new ArgumentNullException("members", Environment.GetResourceString("Member at position {0} was null.", new object[] { i }));
				}
				if (memberInfo.MemberType != MemberTypes.Field)
				{
					throw new SerializationException(Environment.GetResourceString("Only FieldInfo, PropertyInfo, and SerializationMemberInfo are recognized."));
				}
				RtFieldInfo rtFieldInfo = memberInfo as RtFieldInfo;
				if (rtFieldInfo != null)
				{
					rtFieldInfo.CheckConsistency(obj);
					array[i] = rtFieldInfo.UnsafeGetValue(obj);
				}
				else
				{
					array[i] = ((SerializationFieldInfo)memberInfo).InternalGetValue(obj);
				}
			}
			return array;
		}

		/// <summary>Looks up the <see cref="T:System.Type" /> of the specified object in the provided <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the named object.</returns>
		/// <param name="assem">The assembly where you want to look up the object. </param>
		/// <param name="name">The name of the object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assem" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06002679 RID: 9849 RVA: 0x0009B4E5 File Offset: 0x000996E5
		public static Type GetTypeFromAssembly(Assembly assem, string name)
		{
			if (assem == null)
			{
				throw new ArgumentNullException("assem");
			}
			return assem.GetType(name, false, false);
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x0009B504 File Offset: 0x00099704
		internal static Assembly LoadAssemblyFromString(string assemblyName)
		{
			return Assembly.Load(assemblyName);
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x0009B50C File Offset: 0x0009970C
		internal static Assembly LoadAssemblyFromStringNoThrow(string assemblyName)
		{
			try
			{
				return FormatterServices.LoadAssemblyFromString(assemblyName);
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x0009B538 File Offset: 0x00099738
		internal static string GetClrAssemblyName(Type type, out bool hasTypeForwardedFrom)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(TypeForwardedFromAttribute), false);
			if (customAttributes != null && customAttributes.Length != 0)
			{
				hasTypeForwardedFrom = true;
				return ((TypeForwardedFromAttribute)customAttributes[0]).AssemblyFullName;
			}
			hasTypeForwardedFrom = false;
			return type.Assembly.FullName;
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x0009B58B File Offset: 0x0009978B
		internal static string GetClrTypeFullName(Type type)
		{
			if (type.IsArray)
			{
				return FormatterServices.GetClrTypeFullNameForArray(type);
			}
			return FormatterServices.GetClrTypeFullNameForNonArrayTypes(type);
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x0009B5A4 File Offset: 0x000997A4
		private static string GetClrTypeFullNameForArray(Type type)
		{
			int arrayRank = type.GetArrayRank();
			if (arrayRank == 1)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}{1}", FormatterServices.GetClrTypeFullName(type.GetElementType()), "[]");
			}
			StringBuilder stringBuilder = new StringBuilder(FormatterServices.GetClrTypeFullName(type.GetElementType())).Append("[");
			for (int i = 1; i < arrayRank; i++)
			{
				stringBuilder.Append(",");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x0009B624 File Offset: 0x00099824
		private static string GetClrTypeFullNameForNonArrayTypes(Type type)
		{
			if (!type.IsGenericType)
			{
				return type.FullName;
			}
			Type[] genericArguments = type.GetGenericArguments();
			StringBuilder stringBuilder = new StringBuilder(type.GetGenericTypeDefinition().FullName).Append("[");
			foreach (Type type2 in genericArguments)
			{
				stringBuilder.Append("[").Append(FormatterServices.GetClrTypeFullName(type2)).Append(", ");
				bool flag;
				stringBuilder.Append(FormatterServices.GetClrAssemblyName(type2, out flag)).Append("],");
			}
			return stringBuilder.Remove(stringBuilder.Length - 1, 1).Append("]").ToString();
		}

		// Token: 0x0400125C RID: 4700
		internal static ConcurrentDictionary<MemberHolder, MemberInfo[]> m_MemberInfoTable = new ConcurrentDictionary<MemberHolder, MemberInfo[]>();

		// Token: 0x0400125D RID: 4701
		private static bool unsafeTypeForwardersIsEnabled = false;

		// Token: 0x0400125E RID: 4702
		private static volatile bool unsafeTypeForwardersIsEnabledInitialized = false;

		// Token: 0x0400125F RID: 4703
		private static readonly Type[] advancedTypes = new Type[]
		{
			typeof(DelegateSerializationHolder),
			typeof(ObjRef),
			typeof(IEnvoyInfo),
			typeof(ISponsor)
		};

		// Token: 0x04001260 RID: 4704
		private static Binder s_binder = Type.DefaultBinder;
	}
}
