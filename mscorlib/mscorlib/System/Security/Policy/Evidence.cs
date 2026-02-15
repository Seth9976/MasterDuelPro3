using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Defines the set of information that constitutes input to security policy decisions. This class cannot be inherited.</summary>
	// Token: 0x0200033D RID: 829
	[MonoTODO("Serialization format not compatible with .NET")]
	[ComVisible(true)]
	[Serializable]
	public sealed class Evidence : ICollection, IEnumerable
	{
		/// <summary>Initializes a new empty instance of the <see cref="T:System.Security.Policy.Evidence" /> class.</summary>
		// Token: 0x06001D50 RID: 7504 RVA: 0x00003CE1 File Offset: 0x00001EE1
		public Evidence()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Evidence" /> class from a shallow copy of an existing one.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> instance from which to create the new instance. This instance is not deep-copied. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="evidence" /> parameter is not a valid instance of <see cref="T:System.Security.Policy.Evidence" />. </exception>
		// Token: 0x06001D51 RID: 7505 RVA: 0x00073452 File Offset: 0x00071652
		public Evidence(Evidence evidence)
		{
			if (evidence != null)
			{
				this.Merge(evidence);
			}
		}

		/// <summary>Gets the number of evidence objects in the evidence set.</summary>
		/// <returns>The number of evidence objects in the evidence set.</returns>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x00073464 File Offset: 0x00071664
		[Obsolete]
		public int Count
		{
			get
			{
				int num = 0;
				if (this.hostEvidenceList != null)
				{
					num += this.hostEvidenceList.Count;
				}
				if (this.assemblyEvidenceList != null)
				{
					num += this.assemblyEvidenceList.Count;
				}
				return num;
			}
		}

		/// <summary>Gets a value indicating whether the evidence set is thread-safe.</summary>
		/// <returns>Always false because thread-safe evidence sets are not supported.</returns>
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x00033991 File Offset: 0x00031B91
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the synchronization root.</summary>
		/// <returns>Always this (Me in Visual Basic), because synchronization of evidence sets is not supported.</returns>
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x00002645 File Offset: 0x00000845
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x000734A0 File Offset: 0x000716A0
		internal ArrayList HostEvidenceList
		{
			get
			{
				if (this.hostEvidenceList == null)
				{
					this.hostEvidenceList = ArrayList.Synchronized(new ArrayList());
				}
				return this.hostEvidenceList;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x000734C0 File Offset: 0x000716C0
		internal ArrayList AssemblyEvidenceList
		{
			get
			{
				if (this.assemblyEvidenceList == null)
				{
					this.assemblyEvidenceList = ArrayList.Synchronized(new ArrayList());
				}
				return this.assemblyEvidenceList;
			}
		}

		/// <summary>Adds the specified assembly evidence to the evidence set.</summary>
		/// <param name="id">Any evidence object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="id" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="id" /> is not serializable.</exception>
		// Token: 0x06001D57 RID: 7511 RVA: 0x000734E0 File Offset: 0x000716E0
		[Obsolete]
		public void AddAssembly(object id)
		{
			this.AssemblyEvidenceList.Add(id);
		}

		/// <summary>Adds the specified evidence supplied by the host to the evidence set.</summary>
		/// <param name="id">Any evidence object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="id" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="id" /> is not serializable.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001D58 RID: 7512 RVA: 0x000734EF File Offset: 0x000716EF
		[Obsolete]
		public void AddHost(object id)
		{
			if (this._locked && SecurityManager.SecurityEnabled)
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			this.HostEvidenceList.Add(id);
		}

		/// <summary>Returns a duplicate copy of this evidence object.</summary>
		/// <returns>A duplicate copy of this evidence object.</returns>
		// Token: 0x06001D59 RID: 7513 RVA: 0x00073519 File Offset: 0x00071719
		[ComVisible(false)]
		public Evidence Clone()
		{
			return new Evidence(this);
		}

		/// <summary>Copies evidence objects to an <see cref="T:System.Array" />.</summary>
		/// <param name="array">The target array to which to copy evidence objects. </param>
		/// <param name="index">The zero-based position in the array to which to begin copying evidence objects. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index " />is outside the range of the target array<paramref name="." /></exception>
		// Token: 0x06001D5A RID: 7514 RVA: 0x00073524 File Offset: 0x00071724
		[Obsolete]
		public void CopyTo(Array array, int index)
		{
			int num = 0;
			if (this.hostEvidenceList != null)
			{
				num = this.hostEvidenceList.Count;
				if (num > 0)
				{
					this.hostEvidenceList.CopyTo(array, index);
				}
			}
			if (this.assemblyEvidenceList != null && this.assemblyEvidenceList.Count > 0)
			{
				this.assemblyEvidenceList.CopyTo(array, index + num);
			}
		}

		/// <summary>Enumerates all evidence in the set, both that provided by the host and that provided by the assembly.</summary>
		/// <returns>An enumerator for evidence added by both the <see cref="M:System.Security.Policy.Evidence.AddHost(System.Object)" /> method and the <see cref="M:System.Security.Policy.Evidence.AddAssembly(System.Object)" /> method.</returns>
		// Token: 0x06001D5B RID: 7515 RVA: 0x00073580 File Offset: 0x00071780
		[Obsolete]
		public IEnumerator GetEnumerator()
		{
			IEnumerator enumerator = null;
			if (this.hostEvidenceList != null)
			{
				enumerator = this.hostEvidenceList.GetEnumerator();
			}
			IEnumerator enumerator2 = null;
			if (this.assemblyEvidenceList != null)
			{
				enumerator2 = this.assemblyEvidenceList.GetEnumerator();
			}
			return new Evidence.EvidenceEnumerator(enumerator, enumerator2);
		}

		/// <summary>Enumerates evidence supplied by the host.</summary>
		/// <returns>An enumerator for evidence added by the <see cref="M:System.Security.Policy.Evidence.AddHost(System.Object)" /> method.</returns>
		// Token: 0x06001D5C RID: 7516 RVA: 0x000735C0 File Offset: 0x000717C0
		public IEnumerator GetHostEnumerator()
		{
			return this.HostEvidenceList.GetEnumerator();
		}

		/// <summary>Merges the specified evidence set into the current evidence set.</summary>
		/// <param name="evidence">The evidence set to be merged into the current evidence set. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="evidence" /> parameter is not a valid instance of <see cref="T:System.Security.Policy.Evidence" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="P:System.Security.Policy.Evidence.Locked" /> is true, the code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlEvidence" />, and the <paramref name="evidence" /> parameter has a host list that is not empty. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001D5D RID: 7517 RVA: 0x000735D0 File Offset: 0x000717D0
		public void Merge(Evidence evidence)
		{
			if (evidence != null && evidence.Count > 0)
			{
				if (evidence.hostEvidenceList != null)
				{
					foreach (object obj in evidence.hostEvidenceList)
					{
						this.AddHost(obj);
					}
				}
				if (evidence.assemblyEvidenceList != null)
				{
					foreach (object obj2 in evidence.assemblyEvidenceList)
					{
						this.AddAssembly(obj2);
					}
				}
			}
		}

		// Token: 0x06001D5E RID: 7518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAuthenticodePresent(Assembly a);

		// Token: 0x06001D5F RID: 7519 RVA: 0x00073688 File Offset: 0x00071888
		internal static Evidence GetDefaultHostEvidence(Assembly a)
		{
			Evidence evidence = new Evidence();
			string escapedCodeBase = a.EscapedCodeBase;
			evidence.AddHost(Zone.CreateFromUrl(escapedCodeBase));
			evidence.AddHost(new Url(escapedCodeBase));
			evidence.AddHost(new Hash(a));
			if (string.Compare("FILE://", 0, escapedCodeBase, 0, 7, true, CultureInfo.InvariantCulture) != 0)
			{
				evidence.AddHost(Site.CreateFromUrl(escapedCodeBase));
			}
			AssemblyName name = a.GetName();
			byte[] publicKey = name.GetPublicKey();
			if (publicKey != null && publicKey.Length != 0)
			{
				StrongNamePublicKeyBlob strongNamePublicKeyBlob = new StrongNamePublicKeyBlob(publicKey);
				evidence.AddHost(new StrongName(strongNamePublicKeyBlob, name.Name, name.Version));
			}
			if (Evidence.IsAuthenticodePresent(a))
			{
				try
				{
					X509Certificate x509Certificate = X509Certificate.CreateFromSignedFile(a.Location);
					evidence.AddHost(new Publisher(x509Certificate));
				}
				catch (CryptographicException)
				{
				}
			}
			if (a.GlobalAssemblyCache)
			{
				evidence.AddHost(new GacInstalled());
			}
			AppDomainManager domainManager = AppDomain.CurrentDomain.DomainManager;
			if (domainManager != null && (domainManager.HostSecurityManager.Flags & HostSecurityManagerOptions.HostAssemblyEvidence) == HostSecurityManagerOptions.HostAssemblyEvidence)
			{
				evidence = domainManager.HostSecurityManager.ProvideAssemblyEvidence(a, evidence);
			}
			return evidence;
		}

		// Token: 0x04000D98 RID: 3480
		private bool _locked;

		// Token: 0x04000D99 RID: 3481
		private ArrayList hostEvidenceList;

		// Token: 0x04000D9A RID: 3482
		private ArrayList assemblyEvidenceList;

		// Token: 0x0200033E RID: 830
		private class EvidenceEnumerator : IEnumerator
		{
			// Token: 0x06001D60 RID: 7520 RVA: 0x0007379C File Offset: 0x0007199C
			public EvidenceEnumerator(IEnumerator hostenum, IEnumerator assemblyenum)
			{
				this.hostEnum = hostenum;
				this.assemblyEnum = assemblyenum;
				this.currentEnum = this.hostEnum;
			}

			// Token: 0x06001D61 RID: 7521 RVA: 0x000737C0 File Offset: 0x000719C0
			public bool MoveNext()
			{
				if (this.currentEnum == null)
				{
					return false;
				}
				bool flag = this.currentEnum.MoveNext();
				if (!flag && this.hostEnum == this.currentEnum && this.assemblyEnum != null)
				{
					this.currentEnum = this.assemblyEnum;
					flag = this.assemblyEnum.MoveNext();
				}
				return flag;
			}

			// Token: 0x06001D62 RID: 7522 RVA: 0x00073818 File Offset: 0x00071A18
			public void Reset()
			{
				if (this.hostEnum != null)
				{
					this.hostEnum.Reset();
					this.currentEnum = this.hostEnum;
				}
				else
				{
					this.currentEnum = this.assemblyEnum;
				}
				if (this.assemblyEnum != null)
				{
					this.assemblyEnum.Reset();
				}
			}

			// Token: 0x1700033A RID: 826
			// (get) Token: 0x06001D63 RID: 7523 RVA: 0x00073865 File Offset: 0x00071A65
			public object Current
			{
				get
				{
					return this.currentEnum.Current;
				}
			}

			// Token: 0x04000D9B RID: 3483
			private IEnumerator currentEnum;

			// Token: 0x04000D9C RID: 3484
			private IEnumerator hostEnum;

			// Token: 0x04000D9D RID: 3485
			private IEnumerator assemblyEnum;
		}
	}
}
