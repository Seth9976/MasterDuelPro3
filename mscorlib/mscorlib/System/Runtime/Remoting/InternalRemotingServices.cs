using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Remoting
{
	/// <summary>Defines utility methods for use by the .NET Framework remoting infrastructure.</summary>
	// Token: 0x02000418 RID: 1048
	[ComVisible(true)]
	public class InternalRemotingServices
	{
		/// <summary>Gets an appropriate SOAP-related attribute for the specified class member or method parameter. </summary>
		/// <returns>The SOAP-related attribute for the specified class member or method parameter.</returns>
		/// <param name="reflectionObject">A class member or method parameter.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060022E1 RID: 8929 RVA: 0x0008F704 File Offset: 0x0008D904
		public static SoapAttribute GetCachedSoapAttribute(object reflectionObject)
		{
			object syncRoot = InternalRemotingServices._soapAttributes.SyncRoot;
			SoapAttribute soapAttribute2;
			lock (syncRoot)
			{
				SoapAttribute soapAttribute = InternalRemotingServices._soapAttributes[reflectionObject] as SoapAttribute;
				if (soapAttribute != null)
				{
					soapAttribute2 = soapAttribute;
				}
				else
				{
					object[] customAttributes = ((ICustomAttributeProvider)reflectionObject).GetCustomAttributes(typeof(SoapAttribute), true);
					if (customAttributes.Length != 0)
					{
						soapAttribute = (SoapAttribute)customAttributes[0];
					}
					else if (reflectionObject is Type)
					{
						soapAttribute = new SoapTypeAttribute();
					}
					else if (reflectionObject is FieldInfo)
					{
						soapAttribute = new SoapFieldAttribute();
					}
					else if (reflectionObject is MethodBase)
					{
						soapAttribute = new SoapMethodAttribute();
					}
					else if (reflectionObject is ParameterInfo)
					{
						soapAttribute = new SoapParameterAttribute();
					}
					soapAttribute.SetReflectionObject(reflectionObject);
					InternalRemotingServices._soapAttributes[reflectionObject] = soapAttribute;
					soapAttribute2 = soapAttribute;
				}
			}
			return soapAttribute2;
		}

		// Token: 0x040010ED RID: 4333
		private static Hashtable _soapAttributes = new Hashtable();
	}
}
