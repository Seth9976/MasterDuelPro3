using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001B3 RID: 435
	[Serializable]
	internal class UnitySerializationHolder : ISerializable, IObjectReference
	{
		// Token: 0x06001054 RID: 4180 RVA: 0x00045224 File Offset: 0x00043424
		internal static RuntimeType AddElementTypes(SerializationInfo info, RuntimeType type)
		{
			List<int> list = new List<int>();
			while (type.HasElementType)
			{
				if (type.IsSzArray)
				{
					list.Add(3);
				}
				else if (type.IsArray)
				{
					list.Add(type.GetArrayRank());
					list.Add(2);
				}
				else if (type.IsPointer)
				{
					list.Add(1);
				}
				else if (type.IsByRef)
				{
					list.Add(4);
				}
				type = (RuntimeType)type.GetElementType();
			}
			info.AddValue("ElementTypes", list.ToArray(), typeof(int[]));
			return type;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x000452B8 File Offset: 0x000434B8
		internal Type MakeElementTypes(Type type)
		{
			for (int i = this.m_elementTypes.Length - 1; i >= 0; i--)
			{
				if (this.m_elementTypes[i] == 3)
				{
					type = type.MakeArrayType();
				}
				else if (this.m_elementTypes[i] == 2)
				{
					type = type.MakeArrayType(this.m_elementTypes[--i]);
				}
				else if (this.m_elementTypes[i] == 1)
				{
					type = type.MakePointerType();
				}
				else if (this.m_elementTypes[i] == 4)
				{
					type = type.MakeByRefType();
				}
			}
			return type;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0004533C File Offset: 0x0004353C
		internal static void GetUnitySerializationInfo(SerializationInfo info, int unityType)
		{
			info.SetType(typeof(UnitySerializationHolder));
			info.AddValue("Data", null, typeof(string));
			info.AddValue("UnityType", unityType);
			info.AddValue("AssemblyName", string.Empty);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0004538C File Offset: 0x0004358C
		internal static void GetUnitySerializationInfo(SerializationInfo info, RuntimeType type)
		{
			if (type.GetRootElementType().IsGenericParameter)
			{
				type = UnitySerializationHolder.AddElementTypes(info, type);
				info.SetType(typeof(UnitySerializationHolder));
				info.AddValue("UnityType", 7);
				info.AddValue("GenericParameterPosition", type.GenericParameterPosition);
				info.AddValue("DeclaringMethod", type.DeclaringMethod, typeof(MethodBase));
				info.AddValue("DeclaringType", type.DeclaringType, typeof(Type));
				return;
			}
			int num = 4;
			if (!type.IsGenericTypeDefinition && type.ContainsGenericParameters)
			{
				num = 8;
				type = UnitySerializationHolder.AddElementTypes(info, type);
				info.AddValue("GenericArguments", type.GetGenericArguments(), typeof(Type[]));
				type = (RuntimeType)type.GetGenericTypeDefinition();
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, num, type.FullName, type.GetRuntimeAssembly());
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0004546C File Offset: 0x0004366C
		internal static void GetUnitySerializationInfo(SerializationInfo info, int unityType, string data, RuntimeAssembly assembly)
		{
			info.SetType(typeof(UnitySerializationHolder));
			info.AddValue("Data", data, typeof(string));
			info.AddValue("UnityType", unityType);
			string text;
			if (assembly == null)
			{
				text = string.Empty;
			}
			else
			{
				text = assembly.FullName;
			}
			info.AddValue("AssemblyName", text);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000454D0 File Offset: 0x000436D0
		internal UnitySerializationHolder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_unityType = info.GetInt32("UnityType");
			if (this.m_unityType == 3)
			{
				return;
			}
			if (this.m_unityType == 7)
			{
				this.m_declaringMethod = info.GetValue("DeclaringMethod", typeof(MethodBase)) as MethodBase;
				this.m_declaringType = info.GetValue("DeclaringType", typeof(Type)) as Type;
				this.m_genericParameterPosition = info.GetInt32("GenericParameterPosition");
				this.m_elementTypes = info.GetValue("ElementTypes", typeof(int[])) as int[];
				return;
			}
			if (this.m_unityType == 8)
			{
				this.m_instantiation = info.GetValue("GenericArguments", typeof(Type[])) as Type[];
				this.m_elementTypes = info.GetValue("ElementTypes", typeof(int[])) as int[];
			}
			this.m_data = info.GetString("Data");
			this.m_assemblyName = info.GetString("AssemblyName");
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x000455F2 File Offset: 0x000437F2
		private void ThrowInsufficientInformation(string field)
		{
			throw new SerializationException(Environment.GetResourceString("Insufficient state to deserialize the object. Missing field '{0}'. More information is needed.", new object[] { field }));
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0004560D File Offset: 0x0004380D
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("The UnitySerializationHolder object is designed to transmit information about other types and is not serializable itself."));
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00045620 File Offset: 0x00043820
		public virtual object GetRealObject(StreamingContext context)
		{
			switch (this.m_unityType)
			{
			case 1:
				return Empty.Value;
			case 2:
				return DBNull.Value;
			case 3:
				return Missing.Value;
			case 4:
				if (this.m_data == null || this.m_data.Length == 0)
				{
					this.ThrowInsufficientInformation("Data");
				}
				if (this.m_assemblyName == null)
				{
					this.ThrowInsufficientInformation("AssemblyName");
				}
				if (this.m_assemblyName.Length == 0)
				{
					return Type.GetType(this.m_data, true, false);
				}
				return Assembly.Load(this.m_assemblyName).GetType(this.m_data, true, false);
			case 5:
			{
				if (this.m_data == null || this.m_data.Length == 0)
				{
					this.ThrowInsufficientInformation("Data");
				}
				if (this.m_assemblyName == null)
				{
					this.ThrowInsufficientInformation("AssemblyName");
				}
				Module module = Assembly.Load(this.m_assemblyName).GetModule(this.m_data);
				if (module == null)
				{
					throw new SerializationException(Environment.GetResourceString("The given module {0} cannot be found within the assembly {1}.", new object[] { this.m_data, this.m_assemblyName }));
				}
				return module;
			}
			case 6:
				if (this.m_data == null || this.m_data.Length == 0)
				{
					this.ThrowInsufficientInformation("Data");
				}
				if (this.m_assemblyName == null)
				{
					this.ThrowInsufficientInformation("AssemblyName");
				}
				return Assembly.Load(this.m_assemblyName);
			case 7:
				if (this.m_declaringMethod == null && this.m_declaringType == null)
				{
					this.ThrowInsufficientInformation("DeclaringMember");
				}
				if (this.m_declaringMethod != null)
				{
					return this.m_declaringMethod.GetGenericArguments()[this.m_genericParameterPosition];
				}
				return this.MakeElementTypes(this.m_declaringType.GetGenericArguments()[this.m_genericParameterPosition]);
			case 8:
			{
				this.m_unityType = 4;
				Type type = this.GetRealObject(context) as Type;
				this.m_unityType = 8;
				if (this.m_instantiation[0] == null)
				{
					return null;
				}
				return this.MakeElementTypes(type.MakeGenericType(this.m_instantiation));
			}
			default:
				throw new ArgumentException(Environment.GetResourceString("Invalid Unity type."));
			}
		}

		// Token: 0x040006B3 RID: 1715
		private Type[] m_instantiation;

		// Token: 0x040006B4 RID: 1716
		private int[] m_elementTypes;

		// Token: 0x040006B5 RID: 1717
		private int m_genericParameterPosition;

		// Token: 0x040006B6 RID: 1718
		private Type m_declaringType;

		// Token: 0x040006B7 RID: 1719
		private MethodBase m_declaringMethod;

		// Token: 0x040006B8 RID: 1720
		private string m_data;

		// Token: 0x040006B9 RID: 1721
		private string m_assemblyName;

		// Token: 0x040006BA RID: 1722
		private int m_unityType;
	}
}
