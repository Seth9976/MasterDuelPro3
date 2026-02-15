using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;

namespace System.Security.Claims
{
	/// <summary>Represents a claims-based identity.</summary>
	// Token: 0x020003E0 RID: 992
	[ComVisible(true)]
	[Serializable]
	public class ClaimsIdentity : IIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class with an empty claims collection.</summary>
		// Token: 0x060021AF RID: 8623 RVA: 0x0008C153 File Offset: 0x0008A353
		public ClaimsIdentity()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class using an enumerated collection of <see cref="T:System.Security.Claims.Claim" /> objects.</summary>
		/// <param name="claims">The claims with which to populate the claims identity.</param>
		// Token: 0x060021B0 RID: 8624 RVA: 0x0008C15C File Offset: 0x0008A35C
		public ClaimsIdentity(IEnumerable<Claim> claims)
			: this(null, claims, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class from the specified <see cref="T:System.Security.Principal.IIdentity" /> using the specified claims, authentication type, name claim type, and role claim type.</summary>
		/// <param name="identity">The identity from which to base the new claims identity.</param>
		/// <param name="claims">The claims with which to populate the new claims identity.</param>
		/// <param name="authenticationType">The type of authentication used.</param>
		/// <param name="nameType">The claim type to use for name claims.</param>
		/// <param name="roleType">The claim type to use for role claims.</param>
		// Token: 0x060021B1 RID: 8625 RVA: 0x0008C169 File Offset: 0x0008A369
		public ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType)
			: this(identity, claims, authenticationType, nameType, roleType, true)
		{
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x0008C17C File Offset: 0x0008A37C
		internal ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType, bool checkAuthType)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			bool flag = false;
			bool flag2 = false;
			if (checkAuthType && identity != null && string.IsNullOrEmpty(authenticationType))
			{
				if (identity is WindowsIdentity)
				{
					try
					{
						this.m_authenticationType = identity.AuthenticationType;
						goto IL_0085;
					}
					catch (UnauthorizedAccessException)
					{
						this.m_authenticationType = null;
						goto IL_0085;
					}
				}
				this.m_authenticationType = identity.AuthenticationType;
			}
			else
			{
				this.m_authenticationType = authenticationType;
			}
			IL_0085:
			if (!string.IsNullOrEmpty(nameType))
			{
				this.m_nameType = nameType;
				flag = true;
			}
			if (!string.IsNullOrEmpty(roleType))
			{
				this.m_roleType = roleType;
				flag2 = true;
			}
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				this.m_label = claimsIdentity.m_label;
				if (!flag)
				{
					this.m_nameType = claimsIdentity.m_nameType;
				}
				if (!flag2)
				{
					this.m_roleType = claimsIdentity.m_roleType;
				}
				this.m_bootstrapContext = claimsIdentity.m_bootstrapContext;
				if (claimsIdentity.Actor != null)
				{
					if (this.IsCircular(claimsIdentity.Actor))
					{
						throw new InvalidOperationException(Environment.GetResourceString("Actor cannot be set so that circular directed graph will exist chaining the subjects together."));
					}
					if (!AppContextSwitches.SetActorAsReferenceWhenCopyingClaimsIdentity)
					{
						this.m_actor = claimsIdentity.Actor.Clone();
					}
					else
					{
						this.m_actor = claimsIdentity.Actor;
					}
				}
				if (claimsIdentity is WindowsIdentity && !(this is WindowsIdentity))
				{
					this.SafeAddClaims(claimsIdentity.Claims);
				}
				else
				{
					this.SafeAddClaims(claimsIdentity.m_instanceClaims);
				}
				if (claimsIdentity.m_userSerializationData != null)
				{
					this.m_userSerializationData = claimsIdentity.m_userSerializationData.Clone() as byte[];
				}
			}
			else if (identity != null && !string.IsNullOrEmpty(identity.Name))
			{
				this.SafeAddClaim(new Claim(this.m_nameType, identity.Name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", this));
			}
			if (claims != null)
			{
				this.SafeAddClaims(claims);
			}
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x0008C360 File Offset: 0x0008A560
		protected ClaimsIdentity(ClaimsIdentity other)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other.m_actor != null)
			{
				this.m_actor = other.m_actor.Clone();
			}
			this.m_authenticationType = other.m_authenticationType;
			this.m_bootstrapContext = other.m_bootstrapContext;
			this.m_label = other.m_label;
			this.m_nameType = other.m_nameType;
			this.m_roleType = other.m_roleType;
			if (other.m_userSerializationData != null)
			{
				this.m_userSerializationData = other.m_userSerializationData.Clone() as byte[];
			}
			this.SafeAddClaims(other.m_instanceClaims);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class from a serialized stream created by using <see cref="T:System.Runtime.Serialization.ISerializable" />.</summary>
		/// <param name="info">The serialized data.</param>
		/// <param name="context">The context for serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060021B4 RID: 8628 RVA: 0x0008C438 File Offset: 0x0008A638
		protected ClaimsIdentity(SerializationInfo info, StreamingContext context)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Deserialize(info, context, true);
		}

		/// <summary>Gets the authentication type.</summary>
		/// <returns>The authentication type.</returns>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x0008C499 File Offset: 0x0008A699
		public virtual string AuthenticationType
		{
			get
			{
				return this.m_authenticationType;
			}
		}

		/// <summary>Gets or sets the identity of the calling party that was granted delegation rights.</summary>
		/// <returns>The calling party that was granted delegation rights.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt to set the property to the current instance occurs.</exception>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x0008C4A1 File Offset: 0x0008A6A1
		// (set) Token: 0x060021B7 RID: 8631 RVA: 0x0008C4A9 File Offset: 0x0008A6A9
		public ClaimsIdentity Actor
		{
			get
			{
				return this.m_actor;
			}
			set
			{
				if (value != null && this.IsCircular(value))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Actor cannot be set so that circular directed graph will exist chaining the subjects together."));
				}
				this.m_actor = value;
			}
		}

		/// <summary>Gets the claims associated with this claims identity.</summary>
		/// <returns>The collection of claims associated with this claims identity.</returns>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x0008C4CE File Offset: 0x0008A6CE
		public virtual IEnumerable<Claim> Claims
		{
			get
			{
				int num;
				for (int i = 0; i < this.m_instanceClaims.Count; i = num + 1)
				{
					yield return this.m_instanceClaims[i];
					num = i;
				}
				if (this.m_externalClaims != null)
				{
					for (int i = 0; i < this.m_externalClaims.Count; i = num + 1)
					{
						if (this.m_externalClaims[i] != null)
						{
							foreach (Claim claim in this.m_externalClaims[i])
							{
								yield return claim;
							}
							IEnumerator<Claim> enumerator = null;
						}
						num = i;
					}
				}
				yield break;
				yield break;
			}
		}

		/// <summary>Gets the name of this claims identity.</summary>
		/// <returns>The name or null.</returns>
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x0008C4E0 File Offset: 0x0008A6E0
		public virtual string Name
		{
			get
			{
				Claim claim = this.FindFirst(this.m_nameType);
				if (claim != null)
				{
					return claim.Value;
				}
				return null;
			}
		}

		/// <summary>Gets the claim type that is used to determine which claims provide the value for the <see cref="P:System.Security.Claims.ClaimsIdentity.Name" /> property of this claims identity.</summary>
		/// <returns>The name claim type.</returns>
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x0008C505 File Offset: 0x0008A705
		public string NameClaimType
		{
			get
			{
				return this.m_nameType;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Security.Claims.ClaimsIdentity" /> copied from this claims identity.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x060021BB RID: 8635 RVA: 0x0008C510 File Offset: 0x0008A710
		public virtual ClaimsIdentity Clone()
		{
			ClaimsIdentity claimsIdentity = new ClaimsIdentity(this.m_instanceClaims);
			claimsIdentity.m_authenticationType = this.m_authenticationType;
			claimsIdentity.m_bootstrapContext = this.m_bootstrapContext;
			claimsIdentity.m_label = this.m_label;
			claimsIdentity.m_nameType = this.m_nameType;
			claimsIdentity.m_roleType = this.m_roleType;
			if (this.Actor != null)
			{
				if (this.IsCircular(this.Actor))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Actor cannot be set so that circular directed graph will exist chaining the subjects together."));
				}
				if (!AppContextSwitches.SetActorAsReferenceWhenCopyingClaimsIdentity)
				{
					claimsIdentity.Actor = this.Actor.Clone();
				}
				else
				{
					claimsIdentity.Actor = this.Actor;
				}
			}
			return claimsIdentity;
		}

		/// <summary>Adds a single claim to this claims identity.</summary>
		/// <param name="claim">The claim to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="claim" /> is null.</exception>
		// Token: 0x060021BC RID: 8636 RVA: 0x0008C5B4 File Offset: 0x0008A7B4
		public virtual void AddClaim(Claim claim)
		{
			if (claim == null)
			{
				throw new ArgumentNullException("claim");
			}
			if (claim.Subject == this)
			{
				this.m_instanceClaims.Add(claim);
				return;
			}
			this.m_instanceClaims.Add(claim.Clone(this));
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x0008C5EC File Offset: 0x0008A7EC
		private void SafeAddClaims(IEnumerable<Claim> claims)
		{
			foreach (Claim claim in claims)
			{
				if (claim.Subject == this)
				{
					this.m_instanceClaims.Add(claim);
				}
				else
				{
					this.m_instanceClaims.Add(claim.Clone(this));
				}
			}
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x0008C658 File Offset: 0x0008A858
		private void SafeAddClaim(Claim claim)
		{
			if (claim.Subject == this)
			{
				this.m_instanceClaims.Add(claim);
				return;
			}
			this.m_instanceClaims.Add(claim.Clone(this));
		}

		/// <summary>Retrieves the first claim with the specified claim type.</summary>
		/// <returns>The first matching claim or null if no match is found.</returns>
		/// <param name="type">The claim type to match.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x060021BF RID: 8639 RVA: 0x0008C684 File Offset: 0x0008A884
		public virtual Claim FindFirst(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			foreach (Claim claim in this.Claims)
			{
				if (claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase))
				{
					return claim;
				}
			}
			return null;
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x0008C6F4 File Offset: 0x0008A8F4
		[OnSerializing]
		private void OnSerializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_serializedClaims = this.SerializeClaims();
			this.m_serializedNameType = this.m_nameType;
			this.m_serializedRoleType = this.m_roleType;
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x0008C724 File Offset: 0x0008A924
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.m_serializedClaims))
			{
				this.DeserializeClaims(this.m_serializedClaims);
				this.m_serializedClaims = null;
			}
			this.m_nameType = (string.IsNullOrEmpty(this.m_serializedNameType) ? "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" : this.m_serializedNameType);
			this.m_roleType = (string.IsNullOrEmpty(this.m_serializedRoleType) ? "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" : this.m_serializedRoleType);
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x0008C79A File Offset: 0x0008A99A
		[OnDeserializing]
		private void OnDeserializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
		}

		/// <summary>Populates the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with data needed to serialize the current <see cref="T:System.Security.Claims.ClaimsIdentity" /> object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization. Can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060021C3 RID: 8643 RVA: 0x0008C7BC File Offset: 0x0008A9BC
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			info.AddValue("System.Security.ClaimsIdentity.version", this.m_version);
			if (!string.IsNullOrEmpty(this.m_authenticationType))
			{
				info.AddValue("System.Security.ClaimsIdentity.authenticationType", this.m_authenticationType);
			}
			info.AddValue("System.Security.ClaimsIdentity.nameClaimType", this.m_nameType);
			info.AddValue("System.Security.ClaimsIdentity.roleClaimType", this.m_roleType);
			if (!string.IsNullOrEmpty(this.m_label))
			{
				info.AddValue("System.Security.ClaimsIdentity.label", this.m_label);
			}
			if (this.m_actor != null)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					binaryFormatter.Serialize(memoryStream, this.m_actor, null, false);
					info.AddValue("System.Security.ClaimsIdentity.actor", Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
				}
			}
			info.AddValue("System.Security.ClaimsIdentity.claims", this.SerializeClaims());
			if (this.m_bootstrapContext != null)
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					binaryFormatter.Serialize(memoryStream2, this.m_bootstrapContext, null, false);
					info.AddValue("System.Security.ClaimsIdentity.bootstrapContext", Convert.ToBase64String(memoryStream2.GetBuffer(), 0, (int)memoryStream2.Length));
				}
			}
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x0008C908 File Offset: 0x0008AB08
		private void DeserializeClaims(string serializedClaims)
		{
			if (!string.IsNullOrEmpty(serializedClaims))
			{
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(serializedClaims)))
				{
					this.m_instanceClaims = (List<Claim>)new BinaryFormatter().Deserialize(memoryStream, null, false);
					for (int i = 0; i < this.m_instanceClaims.Count; i++)
					{
						this.m_instanceClaims[i].Subject = this;
					}
				}
			}
			if (this.m_instanceClaims == null)
			{
				this.m_instanceClaims = new List<Claim>();
			}
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x0008C998 File Offset: 0x0008AB98
		private string SerializeClaims()
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new BinaryFormatter().Serialize(memoryStream, this.m_instanceClaims, null, false);
				text = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			return text;
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0008C9F0 File Offset: 0x0008ABF0
		private bool IsCircular(ClaimsIdentity subject)
		{
			if (this == subject)
			{
				return true;
			}
			ClaimsIdentity claimsIdentity = subject;
			while (claimsIdentity.Actor != null)
			{
				if (this == claimsIdentity.Actor)
				{
					return true;
				}
				claimsIdentity = claimsIdentity.Actor;
			}
			return false;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0008CA24 File Offset: 0x0008AC24
		private void Deserialize(SerializationInfo info, StreamingContext context, bool useContext)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			BinaryFormatter binaryFormatter;
			if (useContext)
			{
				binaryFormatter = new BinaryFormatter(null, context);
			}
			else
			{
				binaryFormatter = new BinaryFormatter();
			}
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 959168042U)
				{
					if (num <= 623923795U)
					{
						if (num != 373632733U)
						{
							if (num == 623923795U)
							{
								if (name == "System.Security.ClaimsIdentity.roleClaimType")
								{
									this.m_roleType = info.GetString("System.Security.ClaimsIdentity.roleClaimType");
								}
							}
						}
						else if (name == "System.Security.ClaimsIdentity.label")
						{
							this.m_label = info.GetString("System.Security.ClaimsIdentity.label");
						}
					}
					else if (num != 656336169U)
					{
						if (num == 959168042U)
						{
							if (name == "System.Security.ClaimsIdentity.nameClaimType")
							{
								this.m_nameType = info.GetString("System.Security.ClaimsIdentity.nameClaimType");
							}
						}
					}
					else if (name == "System.Security.ClaimsIdentity.authenticationType")
					{
						this.m_authenticationType = info.GetString("System.Security.ClaimsIdentity.authenticationType");
					}
				}
				else if (num <= 1476368026U)
				{
					if (num != 1453716852U)
					{
						if (num != 1476368026U)
						{
							continue;
						}
						if (!(name == "System.Security.ClaimsIdentity.actor"))
						{
							continue;
						}
						using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(info.GetString("System.Security.ClaimsIdentity.actor"))))
						{
							this.m_actor = (ClaimsIdentity)binaryFormatter.Deserialize(memoryStream, null, false);
							continue;
						}
					}
					else if (!(name == "System.Security.ClaimsIdentity.claims"))
					{
						continue;
					}
					this.DeserializeClaims(info.GetString("System.Security.ClaimsIdentity.claims"));
				}
				else if (num != 2480284791U)
				{
					if (num == 3659022112U)
					{
						if (name == "System.Security.ClaimsIdentity.bootstrapContext")
						{
							using (MemoryStream memoryStream2 = new MemoryStream(Convert.FromBase64String(info.GetString("System.Security.ClaimsIdentity.bootstrapContext"))))
							{
								this.m_bootstrapContext = binaryFormatter.Deserialize(memoryStream2, null, false);
							}
						}
					}
				}
				else if (name == "System.Security.ClaimsIdentity.version")
				{
					info.GetString("System.Security.ClaimsIdentity.version");
				}
			}
		}

		// Token: 0x04001025 RID: 4133
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x04001026 RID: 4134
		[NonSerialized]
		private List<Claim> m_instanceClaims;

		// Token: 0x04001027 RID: 4135
		[NonSerialized]
		private Collection<IEnumerable<Claim>> m_externalClaims;

		// Token: 0x04001028 RID: 4136
		[NonSerialized]
		private string m_nameType;

		// Token: 0x04001029 RID: 4137
		[NonSerialized]
		private string m_roleType;

		// Token: 0x0400102A RID: 4138
		[OptionalField(VersionAdded = 2)]
		private string m_version;

		// Token: 0x0400102B RID: 4139
		[OptionalField(VersionAdded = 2)]
		private ClaimsIdentity m_actor;

		// Token: 0x0400102C RID: 4140
		[OptionalField(VersionAdded = 2)]
		private string m_authenticationType;

		// Token: 0x0400102D RID: 4141
		[OptionalField(VersionAdded = 2)]
		private object m_bootstrapContext;

		// Token: 0x0400102E RID: 4142
		[OptionalField(VersionAdded = 2)]
		private string m_label;

		// Token: 0x0400102F RID: 4143
		[OptionalField(VersionAdded = 2)]
		private string m_serializedNameType;

		// Token: 0x04001030 RID: 4144
		[OptionalField(VersionAdded = 2)]
		private string m_serializedRoleType;

		// Token: 0x04001031 RID: 4145
		[OptionalField(VersionAdded = 2)]
		private string m_serializedClaims;
	}
}
