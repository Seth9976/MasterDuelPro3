using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Binary
{
	/// <summary>Serializes and deserializes an object, or an entire graph of connected objects, in binary format.</summary>
	// Token: 0x020004F6 RID: 1270
	[ComVisible(true)]
	public sealed class BinaryFormatter : IFormatter
	{
		/// <summary>Gets or sets the behavior of the deserializer with regards to finding and loading assemblies.</summary>
		/// <returns>One of the <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> values that specifies the deserializer behavior.</returns>
		// Token: 0x1700053D RID: 1341
		// (set) Token: 0x060027B4 RID: 10164 RVA: 0x000A058F File Offset: 0x0009E78F
		public FormatterAssemblyStyle AssemblyFormat
		{
			set
			{
				this.m_assemblyFormat = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> that controls type substitution during serialization and deserialization.</summary>
		/// <returns>The surrogate selector to use with this formatter.</returns>
		// Token: 0x1700053E RID: 1342
		// (set) Token: 0x060027B5 RID: 10165 RVA: 0x000A0598 File Offset: 0x0009E798
		public ISurrogateSelector SurrogateSelector
		{
			set
			{
				this.m_surrogates = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> class with default values.</summary>
		// Token: 0x060027B6 RID: 10166 RVA: 0x000A05A1 File Offset: 0x0009E7A1
		public BinaryFormatter()
		{
			this.m_surrogates = null;
			this.m_context = new StreamingContext(StreamingContextStates.All);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> class with a given surrogate selector and streaming context.</summary>
		/// <param name="selector">The <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> to use. Can be null. </param>
		/// <param name="context">The source and destination for the serialized data. </param>
		// Token: 0x060027B7 RID: 10167 RVA: 0x000A05CE File Offset: 0x0009E7CE
		public BinaryFormatter(ISurrogateSelector selector, StreamingContext context)
		{
			this.m_surrogates = selector;
			this.m_context = context;
		}

		/// <summary>Deserializes the specified stream into an object graph.</summary>
		/// <returns>The top (root) of the object graph.</returns>
		/// <param name="serializationStream">The stream from which to deserialize the object graph. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="serializationStream" /> supports seeking, but its length is 0. -or-The target type is a <see cref="T:System.Decimal" />, but the value is out of range of the <see cref="T:System.Decimal" /> type.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060027B8 RID: 10168 RVA: 0x000A05F2 File Offset: 0x0009E7F2
		public object Deserialize(Stream serializationStream)
		{
			return this.Deserialize(serializationStream, null);
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x000A05FC File Offset: 0x0009E7FC
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck)
		{
			return this.Deserialize(serializationStream, handler, fCheck, null);
		}

		/// <summary>Deserializes the specified stream into an object graph. The provided <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> handles any headers in that stream.</summary>
		/// <returns>The deserialized object or the top object (root) of the object graph.</returns>
		/// <param name="serializationStream">The stream from which to deserialize the object graph. </param>
		/// <param name="handler">The <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> that handles any headers in the <paramref name="serializationStream" />. Can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="serializationStream" /> supports seeking, but its length is 0. -or-The target type is a <see cref="T:System.Decimal" />, but the value is out of range of the <see cref="T:System.Decimal" /> type.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060027BA RID: 10170 RVA: 0x000A0608 File Offset: 0x0009E808
		public object Deserialize(Stream serializationStream, HeaderHandler handler)
		{
			return this.Deserialize(serializationStream, handler, true);
		}

		/// <summary>Deserializes a response to a remote method call from the provided <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The deserialized response to the remote method call.</returns>
		/// <param name="serializationStream">The stream from which to deserialize the object graph. </param>
		/// <param name="handler">The <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> that handles any headers in the <paramref name="serializationStream" />. Can be null. </param>
		/// <param name="methodCallMessage">The <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> that contains details about where the call came from. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="serializationStream" /> supports seeking, but its length is 0. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060027BB RID: 10171 RVA: 0x000A0613 File Offset: 0x0009E813
		public object DeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
		{
			return this.Deserialize(serializationStream, handler, true, methodCallMessage);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x000A061F File Offset: 0x0009E81F
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck, IMethodCallMessage methodCallMessage)
		{
			return this.Deserialize(serializationStream, handler, fCheck, false, methodCallMessage);
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x000A0630 File Offset: 0x0009E830
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck, bool isCrossAppDomain, IMethodCallMessage methodCallMessage)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream", Environment.GetResourceString("Parameter '{0}' cannot be null.", new object[] { serializationStream }));
			}
			if (serializationStream.CanSeek && serializationStream.Length == 0L)
			{
				throw new SerializationException(Environment.GetResourceString("Attempting to deserialize an empty stream."));
			}
			InternalFE internalFE = new InternalFE();
			internalFE.FEtypeFormat = this.m_typeFormat;
			internalFE.FEserializerTypeEnum = InternalSerializerTypeE.Binary;
			internalFE.FEassemblyFormat = this.m_assemblyFormat;
			internalFE.FEsecurityLevel = this.m_securityLevel;
			ObjectReader objectReader = new ObjectReader(serializationStream, this.m_surrogates, this.m_context, internalFE, this.m_binder);
			objectReader.crossAppDomainArray = this.m_crossAppDomainArray;
			return objectReader.Deserialize(handler, new __BinaryParser(serializationStream, objectReader), fCheck, isCrossAppDomain, methodCallMessage);
		}

		/// <summary>Serializes the object, or graph of objects with the specified top (root), to the given stream.</summary>
		/// <param name="serializationStream">The stream to which the graph is to be serialized. </param>
		/// <param name="graph">The object at the root of the graph to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. -or-The <paramref name="graph" /> is null.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error has occurred during serialization, such as if an object in the <paramref name="graph" /> parameter is not marked as serializable. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060027BE RID: 10174 RVA: 0x000A06E9 File Offset: 0x0009E8E9
		public void Serialize(Stream serializationStream, object graph)
		{
			this.Serialize(serializationStream, graph, null);
		}

		/// <summary>Serializes the object, or graph of objects with the specified top (root), to the given stream attaching the provided headers.</summary>
		/// <param name="serializationStream">The stream to which the object is to be serialized. </param>
		/// <param name="graph">The object at the root of the graph to serialize. </param>
		/// <param name="headers">Remoting headers to include in the serialization. Can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error has occurred during serialization, such as if an object in the <paramref name="graph" /> parameter is not marked as serializable. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060027BF RID: 10175 RVA: 0x000A06F4 File Offset: 0x0009E8F4
		public void Serialize(Stream serializationStream, object graph, Header[] headers)
		{
			this.Serialize(serializationStream, graph, headers, true);
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x000A0700 File Offset: 0x0009E900
		internal void Serialize(Stream serializationStream, object graph, Header[] headers, bool fCheck)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream", Environment.GetResourceString("Parameter '{0}' cannot be null.", new object[] { serializationStream }));
			}
			InternalFE internalFE = new InternalFE();
			internalFE.FEtypeFormat = this.m_typeFormat;
			internalFE.FEserializerTypeEnum = InternalSerializerTypeE.Binary;
			internalFE.FEassemblyFormat = this.m_assemblyFormat;
			ObjectWriter objectWriter = new ObjectWriter(this.m_surrogates, this.m_context, internalFE, this.m_binder);
			__BinaryWriter _BinaryWriter = new __BinaryWriter(serializationStream, objectWriter, this.m_typeFormat);
			objectWriter.Serialize(graph, headers, _BinaryWriter, fCheck);
			this.m_crossAppDomainArray = objectWriter.crossAppDomainArray;
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x000A0794 File Offset: 0x0009E994
		internal static TypeInformation GetTypeInformation(Type type)
		{
			Dictionary<Type, TypeInformation> dictionary = BinaryFormatter.typeNameCache;
			TypeInformation typeInformation2;
			lock (dictionary)
			{
				TypeInformation typeInformation = null;
				if (!BinaryFormatter.typeNameCache.TryGetValue(type, out typeInformation))
				{
					bool flag2;
					string clrAssemblyName = FormatterServices.GetClrAssemblyName(type, out flag2);
					typeInformation = new TypeInformation(FormatterServices.GetClrTypeFullName(type), clrAssemblyName, flag2);
					BinaryFormatter.typeNameCache.Add(type, typeInformation);
				}
				typeInformation2 = typeInformation;
			}
			return typeInformation2;
		}

		// Token: 0x040013BF RID: 5055
		internal ISurrogateSelector m_surrogates;

		// Token: 0x040013C0 RID: 5056
		internal StreamingContext m_context;

		// Token: 0x040013C1 RID: 5057
		internal SerializationBinder m_binder;

		// Token: 0x040013C2 RID: 5058
		internal FormatterTypeStyle m_typeFormat = FormatterTypeStyle.TypesAlways;

		// Token: 0x040013C3 RID: 5059
		internal FormatterAssemblyStyle m_assemblyFormat;

		// Token: 0x040013C4 RID: 5060
		internal TypeFilterLevel m_securityLevel = TypeFilterLevel.Full;

		// Token: 0x040013C5 RID: 5061
		internal object[] m_crossAppDomainArray;

		// Token: 0x040013C6 RID: 5062
		private static Dictionary<Type, TypeInformation> typeNameCache = new Dictionary<Type, TypeInformation>();
	}
}
