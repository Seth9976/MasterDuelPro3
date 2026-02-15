using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using Mono.Security.Cryptography;

namespace System.Security.Policy
{
	/// <summary>Encapsulates security decisions about an application. This class cannot be inherited.</summary>
	// Token: 0x02000338 RID: 824
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationTrust : EvidenceBase, ISecurityEncodable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationTrust" /> class.</summary>
		// Token: 0x06001D22 RID: 7458 RVA: 0x0007274C File Offset: 0x0007094C
		public ApplicationTrust()
		{
			this.fullTrustAssemblies = new List<StrongName>(0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationTrust" /> class using the provided grant set and collection of full-trust assemblies.</summary>
		/// <param name="defaultGrantSet">A default permission set that is granted to all assemblies that do not have specific grants.</param>
		/// <param name="fullTrustAssemblies">An array of strong names that represent assemblies that should be considered fully trusted in an application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="defaultGrantSet" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fullTrustAssemblies" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fullTrustAssemblies" /> contains an assembly that does not have a <see cref="T:System.Security.Policy.StrongName" />.</exception>
		// Token: 0x06001D23 RID: 7459 RVA: 0x00072760 File Offset: 0x00070960
		public ApplicationTrust(PermissionSet defaultGrantSet, IEnumerable<StrongName> fullTrustAssemblies)
		{
			if (defaultGrantSet == null)
			{
				throw new ArgumentNullException("defaultGrantSet");
			}
			this._defaultPolicy = new PolicyStatement(defaultGrantSet);
			if (fullTrustAssemblies == null)
			{
				throw new ArgumentNullException("fullTrustAssemblies");
			}
			this.fullTrustAssemblies = new List<StrongName>();
			foreach (StrongName strongName in fullTrustAssemblies)
			{
				if (strongName == null)
				{
					throw new ArgumentException("fullTrustAssemblies contains an assembly that does not have a StrongName");
				}
				this.fullTrustAssemblies.Add((StrongName)strongName.Copy());
			}
		}

		/// <summary>Gets or sets the policy statement defining the default grant set.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.PolicyStatement" /> describing the default grants.</returns>
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x00072800 File Offset: 0x00070A00
		public PolicyStatement DefaultGrantSet
		{
			get
			{
				if (this._defaultPolicy == null)
				{
					this._defaultPolicy = this.GetDefaultGrantSet();
				}
				return this._defaultPolicy;
			}
		}

		/// <summary>Reconstructs an <see cref="T:System.Security.Policy.ApplicationTrust" /> object with a given state from an XML encoding.</summary>
		/// <param name="element">The XML encoding to use to reconstruct the <see cref="T:System.Security.Policy.ApplicationTrust" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The XML encoding used for <paramref name="element" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001D25 RID: 7461 RVA: 0x0007281C File Offset: 0x00070A1C
		public void FromXml(SecurityElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (element.Tag != "ApplicationTrust")
			{
				throw new ArgumentException("element");
			}
			string text = element.Attribute("FullName");
			if (text != null)
			{
				this._appid = new ApplicationIdentity(text);
			}
			else
			{
				this._appid = null;
			}
			this._defaultPolicy = null;
			SecurityElement securityElement = element.SearchForChildByTag("DefaultGrant");
			if (securityElement != null)
			{
				for (int i = 0; i < securityElement.Children.Count; i++)
				{
					SecurityElement securityElement2 = securityElement.Children[i] as SecurityElement;
					if (securityElement2.Tag == "PolicyStatement")
					{
						this.DefaultGrantSet.FromXml(securityElement2, null);
						break;
					}
				}
			}
			if (!bool.TryParse(element.Attribute("TrustedToRun"), out this._trustrun))
			{
				this._trustrun = false;
			}
			if (!bool.TryParse(element.Attribute("Persist"), out this._persist))
			{
				this._persist = false;
			}
			this._xtranfo = null;
			SecurityElement securityElement3 = element.SearchForChildByTag("ExtraInfo");
			if (securityElement3 != null)
			{
				text = securityElement3.Attribute("Data");
				if (text != null)
				{
					using (MemoryStream memoryStream = new MemoryStream(CryptoConvert.FromHex(text)))
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						this._xtranfo = binaryFormatter.Deserialize(memoryStream);
					}
				}
			}
		}

		/// <summary>Creates an XML encoding of the <see cref="T:System.Security.Policy.ApplicationTrust" /> object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001D26 RID: 7462 RVA: 0x00072980 File Offset: 0x00070B80
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("ApplicationTrust");
			securityElement.AddAttribute("version", "1");
			if (this._appid != null)
			{
				securityElement.AddAttribute("FullName", this._appid.FullName);
			}
			if (this._trustrun)
			{
				securityElement.AddAttribute("TrustedToRun", "true");
			}
			if (this._persist)
			{
				securityElement.AddAttribute("Persist", "true");
			}
			SecurityElement securityElement2 = new SecurityElement("DefaultGrant");
			securityElement2.AddChild(this.DefaultGrantSet.ToXml());
			securityElement.AddChild(securityElement2);
			if (this._xtranfo != null)
			{
				byte[] array = null;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					new BinaryFormatter().Serialize(memoryStream, this._xtranfo);
					array = memoryStream.ToArray();
				}
				SecurityElement securityElement3 = new SecurityElement("ExtraInfo");
				securityElement3.AddAttribute("Data", CryptoConvert.ToHex(array));
				securityElement.AddChild(securityElement3);
			}
			return securityElement;
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x00072A88 File Offset: 0x00070C88
		private PolicyStatement GetDefaultGrantSet()
		{
			return new PolicyStatement(new PermissionSet(PermissionState.None));
		}

		// Token: 0x04000D7A RID: 3450
		private ApplicationIdentity _appid;

		// Token: 0x04000D7B RID: 3451
		private PolicyStatement _defaultPolicy;

		// Token: 0x04000D7C RID: 3452
		private object _xtranfo;

		// Token: 0x04000D7D RID: 3453
		private bool _trustrun;

		// Token: 0x04000D7E RID: 3454
		private bool _persist;

		// Token: 0x04000D7F RID: 3455
		private IList<StrongName> fullTrustAssemblies;
	}
}
