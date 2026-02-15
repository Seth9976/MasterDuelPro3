using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security
{
	/// <summary>Defines a permission set that has a name and description associated with it. This class cannot be inherited.</summary>
	// Token: 0x02000321 RID: 801
	[ComVisible(true)]
	[Serializable]
	public sealed class NamedPermissionSet : PermissionSet
	{
		// Token: 0x06001C7B RID: 7291 RVA: 0x0006F0E4 File Offset: 0x0006D2E4
		internal NamedPermissionSet()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.NamedPermissionSet" /> class with the specified name in either an unrestricted or a fully restricted state.</summary>
		/// <param name="name">The name for the new named permission set. </param>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is null or is an empty string (""). </exception>
		// Token: 0x06001C7C RID: 7292 RVA: 0x0006F0EC File Offset: 0x0006D2EC
		public NamedPermissionSet(string name, PermissionState state)
			: base(state)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.NamedPermissionSet" /> class from another named permission set.</summary>
		/// <param name="permSet">The named permission set from which to create the new instance. </param>
		// Token: 0x06001C7D RID: 7293 RVA: 0x0006F0FC File Offset: 0x0006D2FC
		public NamedPermissionSet(NamedPermissionSet permSet)
			: base(permSet)
		{
			this.name = permSet.name;
			this.description = permSet.description;
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Security.NamedPermissionSet" /> class with the specified name.</summary>
		/// <param name="name">The name for the new named permission set. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is null or is an empty string (""). </exception>
		// Token: 0x06001C7E RID: 7294 RVA: 0x0006F11D File Offset: 0x0006D31D
		public NamedPermissionSet(string name)
			: this(name, PermissionState.Unrestricted)
		{
		}

		/// <summary>Gets or sets the name of the current named permission set.</summary>
		/// <returns>The name of the named permission set.</returns>
		/// <exception cref="T:System.ArgumentException">The name is null or is an empty string (""). </exception>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x0006F127 File Offset: 0x0006D327
		// (set) Token: 0x06001C80 RID: 7296 RVA: 0x0006F12F File Offset: 0x0006D32F
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					throw new ArgumentException(Locale.GetText("invalid name"));
				}
				this.name = value;
			}
		}

		/// <summary>Creates a permission set copy from a named permission set.</summary>
		/// <returns>A permission set that is a copy of the permissions in the named permission set.</returns>
		// Token: 0x06001C81 RID: 7297 RVA: 0x0006F158 File Offset: 0x0006D358
		public override PermissionSet Copy()
		{
			return new NamedPermissionSet(this);
		}

		/// <summary>Reconstructs a named permission set with a specified state from an XML encoding.</summary>
		/// <param name="et">A security element containing the XML representation of the named permission set. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid representation of a named permission set. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001C82 RID: 7298 RVA: 0x0006F160 File Offset: 0x0006D360
		public override void FromXml(SecurityElement et)
		{
			base.FromXml(et);
			this.name = et.Attribute("Name");
			this.description = et.Attribute("Description");
			if (this.description == null)
			{
				this.description = string.Empty;
			}
		}

		/// <summary>Creates an XML element description of the named permission set.</summary>
		/// <returns>The XML representation of the named permission set.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001C83 RID: 7299 RVA: 0x0006F1A0 File Offset: 0x0006D3A0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.ToXml();
			if (this.name != null)
			{
				securityElement.AddAttribute("Name", this.name);
			}
			if (this.description != null)
			{
				securityElement.AddAttribute("Description", this.description);
			}
			return securityElement;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.NamedPermissionSet" /> object is equal to the current <see cref="T:System.Security.NamedPermissionSet" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.NamedPermissionSet" /> is equal to the current <see cref="T:System.Security.NamedPermissionSet" /> object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Security.NamedPermissionSet" /> object to compare with the current <see cref="T:System.Security.NamedPermissionSet" />. </param>
		// Token: 0x06001C84 RID: 7300 RVA: 0x0006F1E8 File Offset: 0x0006D3E8
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			NamedPermissionSet namedPermissionSet = obj as NamedPermissionSet;
			return namedPermissionSet != null && this.name == namedPermissionSet.Name && base.Equals(obj);
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.NamedPermissionSet" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.NamedPermissionSet" /> object.</returns>
		// Token: 0x06001C85 RID: 7301 RVA: 0x0006F224 File Offset: 0x0006D424
		[ComVisible(false)]
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.name != null)
			{
				num ^= this.name.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000D1D RID: 3357
		private string name;

		// Token: 0x04000D1E RID: 3358
		private string description;
	}
}
