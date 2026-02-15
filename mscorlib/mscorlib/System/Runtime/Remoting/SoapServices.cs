using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Remoting
{
	/// <summary>Provides several methods for using and publishing remoted objects in SOAP format.</summary>
	// Token: 0x02000428 RID: 1064
	[ComVisible(true)]
	public class SoapServices
	{
		/// <summary>Gets the default XML namespace prefix that should be used for XML encoding of a common language runtime class that has an assembly, but no native namespace.</summary>
		/// <returns>The default XML namespace prefix that should be used for XML encoding of a common language runtime class that has an assembly, but no native namespace.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x00092A30 File Offset: 0x00090C30
		public static string XmlNsForClrTypeWithAssembly
		{
			get
			{
				return "http://schemas.microsoft.com/clr/assem/";
			}
		}

		/// <summary>Gets the XML namespace prefix that should be used for XML encoding of a common language runtime class that is part of the mscorlib.dll file.</summary>
		/// <returns>The XML namespace prefix that should be used for XML encoding of a common language runtime class that is part of the mscorlib.dll file.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x00092A37 File Offset: 0x00090C37
		public static string XmlNsForClrTypeWithNs
		{
			get
			{
				return "http://schemas.microsoft.com/clr/ns/";
			}
		}

		/// <summary>Gets the default XML namespace prefix that should be used for XML encoding of a common language runtime class that has both a common language runtime namespace and an assembly.</summary>
		/// <returns>The default XML namespace prefix that should be used for XML encoding of a common language runtime class that has both a common language runtime namespace and an assembly.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x00092A3E File Offset: 0x00090C3E
		public static string XmlNsForClrTypeWithNsAndAssembly
		{
			get
			{
				return "http://schemas.microsoft.com/clr/nsassem/";
			}
		}

		/// <summary>Returns the common language runtime type namespace name from the provided namespace and assembly names.</summary>
		/// <returns>The common language runtime type namespace name from the provided namespace and assembly names.</returns>
		/// <param name="typeNamespace">The namespace that is to be coded. </param>
		/// <param name="assemblyName">The name of the assembly that is to be coded. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assemblyName" /> and <paramref name="typeNamespace" /> parameters are both either null or empty. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600238D RID: 9101 RVA: 0x00092A48 File Offset: 0x00090C48
		public static string CodeXmlNamespaceForClrTypeNamespace(string typeNamespace, string assemblyName)
		{
			if (assemblyName == string.Empty)
			{
				return SoapServices.XmlNsForClrTypeWithNs + typeNamespace;
			}
			if (typeNamespace == string.Empty)
			{
				return SoapServices.EncodeNs(SoapServices.XmlNsForClrTypeWithAssembly + assemblyName);
			}
			return SoapServices.EncodeNs(SoapServices.XmlNsForClrTypeWithNsAndAssembly + typeNamespace + "/" + assemblyName);
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x00092AA2 File Offset: 0x00090CA2
		private static string GetNameKey(string name, string namspace)
		{
			if (namspace == null)
			{
				return name;
			}
			return name + " " + namspace;
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x00092AB5 File Offset: 0x00090CB5
		private static string GetAssemblyName(MethodBase mb)
		{
			if (mb.DeclaringType.Assembly == typeof(object).Assembly)
			{
				return string.Empty;
			}
			return mb.DeclaringType.Assembly.GetName().Name;
		}

		/// <summary>Returns XML element information that should be used when serializing the given type.</summary>
		/// <returns>true if the requested values have been set flagged with <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" />; otherwise, false.</returns>
		/// <param name="type">The object <see cref="T:System.Type" /> for which the XML element and namespace names were requested. </param>
		/// <param name="xmlElement">When this method returns, contains a <see cref="T:System.String" /> that holds the XML element name of the specified object type. This parameter is passed uninitialized. </param>
		/// <param name="xmlNamespace">When this method returns, contains a <see cref="T:System.String" /> that holds the XML namespace name of the specified object type. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002390 RID: 9104 RVA: 0x00092AF4 File Offset: 0x00090CF4
		public static bool GetXmlElementForInteropType(Type type, out string xmlElement, out string xmlNamespace)
		{
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (!soapTypeAttribute.IsInteropXmlElement)
			{
				xmlElement = null;
				xmlNamespace = null;
				return false;
			}
			xmlElement = soapTypeAttribute.XmlElementName;
			xmlNamespace = soapTypeAttribute.XmlNamespace;
			return true;
		}

		/// <summary>Retrieves the XML namespace used during remote calls of the method specified in the given <see cref="T:System.Reflection.MethodBase" />.</summary>
		/// <returns>The XML namespace used during remote calls of the specified method.</returns>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> of the method for which the XML namespace was requested. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002391 RID: 9105 RVA: 0x00092B2E File Offset: 0x00090D2E
		public static string GetXmlNamespaceForMethodCall(MethodBase mb)
		{
			return SoapServices.CodeXmlNamespaceForClrTypeNamespace(mb.DeclaringType.FullName, SoapServices.GetAssemblyName(mb));
		}

		/// <summary>Retrieves the XML namespace used during the generation of responses to the remote call to the method specified in the given <see cref="T:System.Reflection.MethodBase" />.</summary>
		/// <returns>The XML namespace used during the generation of responses to a remote method call.</returns>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> of the method for which the XML namespace was requested. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002392 RID: 9106 RVA: 0x00092B2E File Offset: 0x00090D2E
		public static string GetXmlNamespaceForMethodResponse(MethodBase mb)
		{
			return SoapServices.CodeXmlNamespaceForClrTypeNamespace(mb.DeclaringType.FullName, SoapServices.GetAssemblyName(mb));
		}

		/// <summary>Returns XML type information that should be used when serializing the given <see cref="T:System.Type" />.</summary>
		/// <returns>true if the requested values have been set flagged with <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" />; otherwise, false.</returns>
		/// <param name="type">The object <see cref="T:System.Type" /> for which the XML element and namespace names were requested. </param>
		/// <param name="xmlType">The XML type of the specified object <see cref="T:System.Type" />. </param>
		/// <param name="xmlTypeNamespace">The XML type namespace of the specified object <see cref="T:System.Type" />. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002393 RID: 9107 RVA: 0x00092B48 File Offset: 0x00090D48
		public static bool GetXmlTypeForInteropType(Type type, out string xmlType, out string xmlTypeNamespace)
		{
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (!soapTypeAttribute.IsInteropXmlType)
			{
				xmlType = null;
				xmlTypeNamespace = null;
				return false;
			}
			xmlType = soapTypeAttribute.XmlTypeName;
			xmlTypeNamespace = soapTypeAttribute.XmlTypeNamespace;
			return true;
		}

		/// <summary>Preloads every <see cref="T:System.Type" /> found in the specified <see cref="T:System.Reflection.Assembly" /> from the information found in the <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> associated with each type.</summary>
		/// <param name="assembly">The <see cref="T:System.Reflection.Assembly" /> for each type of which to call <see cref="M:System.Runtime.Remoting.SoapServices.PreLoad(System.Type)" />. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002394 RID: 9108 RVA: 0x00092B84 File Offset: 0x00090D84
		public static void PreLoad(Assembly assembly)
		{
			Type[] types = assembly.GetTypes();
			for (int i = 0; i < types.Length; i++)
			{
				SoapServices.PreLoad(types[i]);
			}
		}

		/// <summary>Preloads the given <see cref="T:System.Type" /> based on values set in a <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> on the type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to preload. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002395 RID: 9109 RVA: 0x00092BB0 File Offset: 0x00090DB0
		public static void PreLoad(Type type)
		{
			SoapServices.TypeInfo typeInfo = SoapServices._typeInfos[type] as SoapServices.TypeInfo;
			if (typeInfo != null)
			{
				return;
			}
			string text;
			string text2;
			if (SoapServices.GetXmlTypeForInteropType(type, out text, out text2))
			{
				SoapServices.RegisterInteropXmlType(text, text2, type);
			}
			if (SoapServices.GetXmlElementForInteropType(type, out text, out text2))
			{
				SoapServices.RegisterInteropXmlElement(text, text2, type);
			}
			object syncRoot = SoapServices._typeInfos.SyncRoot;
			lock (syncRoot)
			{
				typeInfo = new SoapServices.TypeInfo();
				foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					SoapFieldAttribute soapFieldAttribute = (SoapFieldAttribute)InternalRemotingServices.GetCachedSoapAttribute(fieldInfo);
					if (soapFieldAttribute.IsInteropXmlElement())
					{
						string nameKey = SoapServices.GetNameKey(soapFieldAttribute.XmlElementName, soapFieldAttribute.XmlNamespace);
						if (soapFieldAttribute.UseAttribute)
						{
							if (typeInfo.Attributes == null)
							{
								typeInfo.Attributes = new Hashtable();
							}
							typeInfo.Attributes[nameKey] = fieldInfo;
						}
						else
						{
							if (typeInfo.Elements == null)
							{
								typeInfo.Elements = new Hashtable();
							}
							typeInfo.Elements[nameKey] = fieldInfo;
						}
					}
				}
				SoapServices._typeInfos[type] = typeInfo;
			}
		}

		/// <summary>Associates the given XML element name and namespace with a run-time type that should be used for deserialization.</summary>
		/// <param name="xmlElement">The XML element name to use in deserialization. </param>
		/// <param name="xmlNamespace">The XML namespace to use in deserialization. </param>
		/// <param name="type">The run-time <see cref="T:System.Type" /> to use in deserialization. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002396 RID: 9110 RVA: 0x00092CE4 File Offset: 0x00090EE4
		public static void RegisterInteropXmlElement(string xmlElement, string xmlNamespace, Type type)
		{
			object syncRoot = SoapServices._xmlElements.SyncRoot;
			lock (syncRoot)
			{
				SoapServices._xmlElements[xmlElement + " " + xmlNamespace] = type;
			}
		}

		/// <summary>Associates the given XML type name and namespace with the run-time type that should be used for deserialization.</summary>
		/// <param name="xmlType">The XML type to use in deserialization. </param>
		/// <param name="xmlTypeNamespace">The XML namespace to use in deserialization. </param>
		/// <param name="type">The run-time <see cref="T:System.Type" /> to use in deserialization. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002397 RID: 9111 RVA: 0x00092D3C File Offset: 0x00090F3C
		public static void RegisterInteropXmlType(string xmlType, string xmlTypeNamespace, Type type)
		{
			object syncRoot = SoapServices._xmlTypes.SyncRoot;
			lock (syncRoot)
			{
				SoapServices._xmlTypes[xmlType + " " + xmlTypeNamespace] = type;
			}
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x00092D94 File Offset: 0x00090F94
		private static string EncodeNs(string ns)
		{
			ns = ns.Replace(",", "%2C");
			ns = ns.Replace(" ", "%20");
			return ns.Replace("=", "%3D");
		}

		// Token: 0x0400112B RID: 4395
		private static Hashtable _xmlTypes = new Hashtable();

		// Token: 0x0400112C RID: 4396
		private static Hashtable _xmlElements = new Hashtable();

		// Token: 0x0400112D RID: 4397
		private static Hashtable _soapActions = new Hashtable();

		// Token: 0x0400112E RID: 4398
		private static Hashtable _soapActionsMethods = new Hashtable();

		// Token: 0x0400112F RID: 4399
		private static Hashtable _typeInfos = new Hashtable();

		// Token: 0x02000429 RID: 1065
		private class TypeInfo
		{
			// Token: 0x04001130 RID: 4400
			public Hashtable Attributes;

			// Token: 0x04001131 RID: 4401
			public Hashtable Elements;
		}
	}
}
