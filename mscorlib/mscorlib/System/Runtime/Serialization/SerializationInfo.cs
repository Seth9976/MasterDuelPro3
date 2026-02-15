using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;

namespace System.Runtime.Serialization
{
	/// <summary>Stores all the data needed to serialize or deserialize an object. This class cannot be inherited.</summary>
	// Token: 0x020004CD RID: 1229
	[ComVisible(true)]
	public sealed class SerializationInfo
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize. </param>
		/// <param name="converter">The <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> used during deserialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="converter" /> is null. </exception>
		// Token: 0x0600270D RID: 9997 RVA: 0x0009D5F2 File Offset: 0x0009B7F2
		[CLSCompliant(false)]
		public SerializationInfo(Type type, IFormatterConverter converter)
			: this(type, converter, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize.</param>
		/// <param name="converter">The <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> used during deserialization.</param>
		/// <param name="requireSameTokenInPartialTrust">Indicates whether the object requires same token in partial trust.</param>
		// Token: 0x0600270E RID: 9998 RVA: 0x0009D600 File Offset: 0x0009B800
		[CLSCompliant(false)]
		public SerializationInfo(Type type, IFormatterConverter converter, bool requireSameTokenInPartialTrust)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			this.objectType = type;
			this.m_fullTypeName = type.FullName;
			this.m_assemName = type.Module.Assembly.FullName;
			this.m_members = new string[4];
			this.m_data = new object[4];
			this.m_types = new Type[4];
			this.m_nameToIndex = new Dictionary<string, int>();
			this.m_converter = converter;
			this.requireSameTokenInPartialTrust = requireSameTokenInPartialTrust;
		}

		/// <summary>Gets or sets the full name of the <see cref="T:System.Type" /> to serialize.</summary>
		/// <returns>The full name of the type to serialize.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value this property is set to is null. </exception>
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x0009D695 File Offset: 0x0009B895
		public string FullTypeName
		{
			get
			{
				return this.m_fullTypeName;
			}
		}

		/// <summary>Gets or sets the assembly name of the type to serialize during serialization only.</summary>
		/// <returns>The full name of the assembly of the type to serialize.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value the property is set to is null. </exception>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06002710 RID: 10000 RVA: 0x0009D69D File Offset: 0x0009B89D
		public string AssemblyName
		{
			get
			{
				return this.m_assemName;
			}
		}

		/// <summary>Sets the <see cref="T:System.Type" /> of the object to serialize.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null. </exception>
		// Token: 0x06002711 RID: 10001 RVA: 0x0009D6A8 File Offset: 0x0009B8A8
		public void SetType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this.requireSameTokenInPartialTrust)
			{
				SerializationInfo.DemandForUnsafeAssemblyNameAssignments(this.ObjectType.Assembly.FullName, type.Assembly.FullName);
			}
			if (this.objectType != type)
			{
				this.objectType = type;
				this.m_fullTypeName = type.FullName;
				this.m_assemName = type.Module.Assembly.FullName;
				this.isFullTypeNameSetExplicit = false;
				this.isAssemblyNameSetExplicit = false;
			}
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x0009D72C File Offset: 0x0009B92C
		private static bool Compare(byte[] a, byte[] b)
		{
			if (a == null || b == null || a.Length == 0 || b.Length == 0 || a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x0009D76A File Offset: 0x0009B96A
		internal static void DemandForUnsafeAssemblyNameAssignments(string originalAssemblyName, string newAssemblyName)
		{
			SerializationInfo.IsAssemblyNameAssignmentSafe(originalAssemblyName, newAssemblyName);
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x0009D774 File Offset: 0x0009B974
		internal static bool IsAssemblyNameAssignmentSafe(string originalAssemblyName, string newAssemblyName)
		{
			if (originalAssemblyName == newAssemblyName)
			{
				return true;
			}
			AssemblyName assemblyName = new AssemblyName(originalAssemblyName);
			AssemblyName assemblyName2 = new AssemblyName(newAssemblyName);
			return !string.Equals(assemblyName2.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) && !string.Equals(assemblyName2.Name, "mscorlib.dll", StringComparison.OrdinalIgnoreCase) && SerializationInfo.Compare(assemblyName.GetPublicKeyToken(), assemblyName2.GetPublicKeyToken());
		}

		/// <summary>Gets the number of members that have been added to the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The number of members that have been added to the current <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</returns>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x0009D7D3 File Offset: 0x0009B9D3
		public int MemberCount
		{
			get
			{
				return this.m_currMember;
			}
		}

		/// <summary>Returns the type of the object to be serialized.</summary>
		/// <returns>The type of the object being serialized.</returns>
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06002716 RID: 10006 RVA: 0x0009D7DB File Offset: 0x0009B9DB
		public Type ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		/// <summary>Gets whether the full type name has been explicitly set.</summary>
		/// <returns>True if the full type name has been explicitly set; otherwise false.</returns>
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x0009D7E3 File Offset: 0x0009B9E3
		public bool IsFullTypeNameSetExplicit
		{
			get
			{
				return this.isFullTypeNameSetExplicit;
			}
		}

		/// <summary>Gets whether the assembly name has been explicitly set.</summary>
		/// <returns>True if the assembly name has been explicitly set; otherwise false.</returns>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06002718 RID: 10008 RVA: 0x0009D7EB File Offset: 0x0009B9EB
		public bool IsAssemblyNameSetExplicit
		{
			get
			{
				return this.isAssemblyNameSetExplicit;
			}
		}

		/// <summary>Returns a <see cref="T:System.Runtime.Serialization.SerializationInfoEnumerator" /> used to iterate through the name-value pairs in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.SerializationInfoEnumerator" /> for parsing the name-value pairs contained in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</returns>
		// Token: 0x06002719 RID: 10009 RVA: 0x0009D7F3 File Offset: 0x0009B9F3
		public SerializationInfoEnumerator GetEnumerator()
		{
			return new SerializationInfoEnumerator(this.m_members, this.m_data, this.m_types, this.m_currMember);
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x0009D814 File Offset: 0x0009BA14
		private void ExpandArrays()
		{
			int num = this.m_currMember * 2;
			if (num < this.m_currMember && 2147483647 > this.m_currMember)
			{
				num = int.MaxValue;
			}
			string[] array = new string[num];
			object[] array2 = new object[num];
			Type[] array3 = new Type[num];
			Array.Copy(this.m_members, array, this.m_currMember);
			Array.Copy(this.m_data, array2, this.m_currMember);
			Array.Copy(this.m_types, array3, this.m_currMember);
			this.m_members = array;
			this.m_data = array2;
			this.m_types = array3;
		}

		/// <summary>Adds a value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store, where <paramref name="value" /> is associated with <paramref name="name" /> and is serialized as being of <see cref="T:System.Type" /><paramref name="type" />.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The value to be serialized. Any children of this object will automatically be serialized. </param>
		/// <param name="type">The <see cref="T:System.Type" /> to associate with the current object. This parameter must always be the type of the object itself or of one of its base classes. </param>
		/// <exception cref="T:System.ArgumentNullException">If <paramref name="name" /> or <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x0600271B RID: 10011 RVA: 0x0009D8A6 File Offset: 0x0009BAA6
		public void AddValue(string name, object value, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.AddValueInternal(name, value, type);
		}

		/// <summary>Adds the specified object into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store, where it is associated with a specified name.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The value to be serialized. Any children of this object will automatically be serialized. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x0600271C RID: 10012 RVA: 0x0009D8CD File Offset: 0x0009BACD
		public void AddValue(string name, object value)
		{
			if (value == null)
			{
				this.AddValue(name, value, typeof(object));
				return;
			}
			this.AddValue(name, value, value.GetType());
		}

		/// <summary>Adds a Boolean value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The Boolean value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x0600271D RID: 10013 RVA: 0x0009D8F3 File Offset: 0x0009BAF3
		public void AddValue(string name, bool value)
		{
			this.AddValue(name, value, typeof(bool));
		}

		/// <summary>Adds an 8-bit unsigned integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The byte value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x0600271E RID: 10014 RVA: 0x0009D90C File Offset: 0x0009BB0C
		public void AddValue(string name, byte value)
		{
			this.AddValue(name, value, typeof(byte));
		}

		/// <summary>Adds a 16-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The Int16 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x0600271F RID: 10015 RVA: 0x0009D925 File Offset: 0x0009BB25
		public void AddValue(string name, short value)
		{
			this.AddValue(name, value, typeof(short));
		}

		/// <summary>Adds a 32-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The Int32 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06002720 RID: 10016 RVA: 0x0009D93E File Offset: 0x0009BB3E
		public void AddValue(string name, int value)
		{
			this.AddValue(name, value, typeof(int));
		}

		/// <summary>Adds a 64-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The Int64 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06002721 RID: 10017 RVA: 0x0009D957 File Offset: 0x0009BB57
		public void AddValue(string name, long value)
		{
			this.AddValue(name, value, typeof(long));
		}

		/// <summary>Adds a 64-bit unsigned integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The UInt64 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06002722 RID: 10018 RVA: 0x0009D970 File Offset: 0x0009BB70
		[CLSCompliant(false)]
		public void AddValue(string name, ulong value)
		{
			this.AddValue(name, value, typeof(ulong));
		}

		/// <summary>Adds a single-precision floating-point value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The single value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06002723 RID: 10019 RVA: 0x0009D989 File Offset: 0x0009BB89
		public void AddValue(string name, float value)
		{
			this.AddValue(name, value, typeof(float));
		}

		/// <summary>Adds a <see cref="T:System.DateTime" /> value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06002724 RID: 10020 RVA: 0x0009D9A2 File Offset: 0x0009BBA2
		public void AddValue(string name, DateTime value)
		{
			this.AddValue(name, value, typeof(DateTime));
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x0009D9BC File Offset: 0x0009BBBC
		internal void AddValueInternal(string name, object value, Type type)
		{
			if (this.m_nameToIndex.ContainsKey(name))
			{
				throw new SerializationException(Environment.GetResourceString("Cannot add the same member twice to a SerializationInfo object."));
			}
			this.m_nameToIndex.Add(name, this.m_currMember);
			if (this.m_currMember >= this.m_members.Length)
			{
				this.ExpandArrays();
			}
			this.m_members[this.m_currMember] = name;
			this.m_data[this.m_currMember] = value;
			this.m_types[this.m_currMember] = type;
			this.m_currMember++;
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x0009DA48 File Offset: 0x0009BC48
		internal void UpdateValue(string name, object value, Type type)
		{
			int num = this.FindElement(name);
			if (num < 0)
			{
				this.AddValueInternal(name, value, type);
				return;
			}
			this.m_data[num] = value;
			this.m_types[num] = type;
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x0009DA80 File Offset: 0x0009BC80
		private int FindElement(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			int num;
			if (this.m_nameToIndex.TryGetValue(name, out num))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x0009DAB0 File Offset: 0x0009BCB0
		private object GetElement(string name, out Type foundType)
		{
			int num = this.FindElement(name);
			if (num == -1)
			{
				throw new SerializationException(Environment.GetResourceString("Member '{0}' was not found.", new object[] { name }));
			}
			foundType = this.m_types[num];
			return this.m_data[num];
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x0009DAF8 File Offset: 0x0009BCF8
		[ComVisible(true)]
		private object GetElementNoThrow(string name, out Type foundType)
		{
			int num = this.FindElement(name);
			if (num == -1)
			{
				foundType = null;
				return null;
			}
			foundType = this.m_types[num];
			return this.m_data[num];
		}

		/// <summary>Retrieves a value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The object of the specified <see cref="T:System.Type" /> associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the value to retrieve. If the stored value cannot be converted to this type, the system will throw a <see cref="T:System.InvalidCastException" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to <paramref name="type" />. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x0600272A RID: 10026 RVA: 0x0009DB28 File Offset: 0x0009BD28
		public object GetValue(string name, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a runtime Type object."));
			}
			Type type2;
			object element = this.GetElement(name, out type2);
			if (RemotingServices.IsTransparentProxy(element))
			{
				if (RemotingServices.ProxyCheckCast(RemotingServices.GetRealProxy(element), runtimeType))
				{
					return element;
				}
			}
			else if (type2 == type || type.IsAssignableFrom(type2) || element == null)
			{
				return element;
			}
			return this.m_converter.Convert(element, type);
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x0009DBA4 File Offset: 0x0009BDA4
		[ComVisible(true)]
		internal object GetValueNoThrow(string name, Type type)
		{
			Type type2;
			object elementNoThrow = this.GetElementNoThrow(name, out type2);
			if (elementNoThrow == null)
			{
				return null;
			}
			if (RemotingServices.IsTransparentProxy(elementNoThrow))
			{
				if (RemotingServices.ProxyCheckCast(RemotingServices.GetRealProxy(elementNoThrow), (RuntimeType)type))
				{
					return elementNoThrow;
				}
			}
			else if (type2 == type || type.IsAssignableFrom(type2) || elementNoThrow == null)
			{
				return elementNoThrow;
			}
			return this.m_converter.Convert(elementNoThrow, type);
		}

		/// <summary>Retrieves a Boolean value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The Boolean value associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a Boolean value. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x0600272C RID: 10028 RVA: 0x0009DBFC File Offset: 0x0009BDFC
		public bool GetBoolean(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(bool))
			{
				return (bool)element;
			}
			return this.m_converter.ToBoolean(element);
		}

		/// <summary>Retrieves a 32-bit signed integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 32-bit signed integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name of the value to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 32-bit signed integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x0600272D RID: 10029 RVA: 0x0009DC34 File Offset: 0x0009BE34
		public int GetInt32(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(int))
			{
				return (int)element;
			}
			return this.m_converter.ToInt32(element);
		}

		/// <summary>Retrieves a 64-bit signed integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 64-bit signed integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 64-bit signed integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x0600272E RID: 10030 RVA: 0x0009DC6C File Offset: 0x0009BE6C
		public long GetInt64(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(long))
			{
				return (long)element;
			}
			return this.m_converter.ToInt64(element);
		}

		/// <summary>Retrieves a single-precision floating-point value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The single-precision floating-point value associated with <paramref name="name" />.</returns>
		/// <param name="name">The name of the value to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a single-precision floating-point value. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x0600272F RID: 10031 RVA: 0x0009DCA4 File Offset: 0x0009BEA4
		public float GetSingle(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(float))
			{
				return (float)element;
			}
			return this.m_converter.ToSingle(element);
		}

		/// <summary>Retrieves a <see cref="T:System.String" /> value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The <see cref="T:System.String" /> associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a <see cref="T:System.String" />. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06002730 RID: 10032 RVA: 0x0009DCDC File Offset: 0x0009BEDC
		public string GetString(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(string) || element == null)
			{
				return (string)element;
			}
			return this.m_converter.ToString(element);
		}

		// Token: 0x04001298 RID: 4760
		internal string[] m_members;

		// Token: 0x04001299 RID: 4761
		internal object[] m_data;

		// Token: 0x0400129A RID: 4762
		internal Type[] m_types;

		// Token: 0x0400129B RID: 4763
		private Dictionary<string, int> m_nameToIndex;

		// Token: 0x0400129C RID: 4764
		internal int m_currMember;

		// Token: 0x0400129D RID: 4765
		internal IFormatterConverter m_converter;

		// Token: 0x0400129E RID: 4766
		private string m_fullTypeName;

		// Token: 0x0400129F RID: 4767
		private string m_assemName;

		// Token: 0x040012A0 RID: 4768
		private Type objectType;

		// Token: 0x040012A1 RID: 4769
		private bool isFullTypeNameSetExplicit;

		// Token: 0x040012A2 RID: 4770
		private bool isAssemblyNameSetExplicit;

		// Token: 0x040012A3 RID: 4771
		private bool requireSameTokenInPartialTrust;
	}
}
