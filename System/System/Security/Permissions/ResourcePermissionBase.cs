using System;
using System.Collections;

namespace System.Security.Permissions
{
	/// <summary>Allows control of code access security permissions.</summary>
	// Token: 0x0200019C RID: 412
	[Serializable]
	public abstract class ResourcePermissionBase : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBase" /> class.</summary>
		// Token: 0x060009D3 RID: 2515 RVA: 0x00033340 File Offset: 0x00031540
		protected ResourcePermissionBase()
		{
			this._list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBase" /> class with the specified level of access to resources at creation.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x060009D4 RID: 2516 RVA: 0x00033353 File Offset: 0x00031553
		protected ResourcePermissionBase(PermissionState state)
			: this()
		{
			PermissionHelper.CheckPermissionState(state, true);
			this._unrestricted = state == PermissionState.Unrestricted;
		}

		/// <summary>Gets or sets an enumeration value that describes the types of access that you are giving the resource.</summary>
		/// <returns>An enumeration value that is derived from <see cref="T:System.Type" /> and describes the types of access that you are giving the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is null. </exception>
		/// <exception cref="T:System.ArgumentException">The property value is not an enumeration value. </exception>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0003336D File Offset: 0x0003156D
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x00033375 File Offset: 0x00031575
		protected Type PermissionAccessType
		{
			get
			{
				return this._type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PermissionAccessType");
				}
				if (!value.IsEnum)
				{
					throw new ArgumentException("!Enum", "PermissionAccessType");
				}
				this._type = value;
			}
		}

		/// <summary>Gets or sets an array of strings that identify the resource you are protecting.</summary>
		/// <returns>An array of strings that identify the resource you are trying to protect.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of the array is 0. </exception>
		// Token: 0x170001B1 RID: 433
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x000333AA File Offset: 0x000315AA
		protected string[] TagNames
		{
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("TagNames");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("Length==0", "TagNames");
				}
				this._tags = value;
			}
		}

		/// <summary>Adds a permission entry to the permission.</summary>
		/// <param name="entry">The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccessPath" /> property is not equal to the number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBase.TagNames" /> property.-or- The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is already included in the permission. </exception>
		// Token: 0x060009D8 RID: 2520 RVA: 0x000333D5 File Offset: 0x000315D5
		protected void AddPermissionAccess(ResourcePermissionBaseEntry entry)
		{
			this.CheckEntry(entry);
			if (this.Exists(entry))
			{
				throw new InvalidOperationException(global::Locale.GetText("Entry already exists."));
			}
			this._list.Add(entry);
		}

		/// <summary>Creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060009D9 RID: 2521 RVA: 0x00033404 File Offset: 0x00031604
		public override IPermission Copy()
		{
			ResourcePermissionBase resourcePermissionBase = ResourcePermissionBase.CreateFromType(base.GetType(), this._unrestricted);
			if (this._tags != null)
			{
				resourcePermissionBase._tags = (string[])this._tags.Clone();
			}
			resourcePermissionBase._type = this._type;
			resourcePermissionBase._list.AddRange(this._list);
			return resourcePermissionBase;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="securityElement" /> parameter is not a valid permission element.-or- The version number of the <paramref name="securityElement" /> parameter is not supported.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="securityElement" /> parameter is null.</exception>
		// Token: 0x060009DA RID: 2522 RVA: 0x00033460 File Offset: 0x00031660
		[MonoTODO("incomplete - need more test")]
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			CodeAccessPermission.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			this._list.Clear();
			this._unrestricted = PermissionHelper.IsUnrestricted(securityElement);
			if (securityElement.Children == null || securityElement.Children.Count < 1)
			{
				return;
			}
			string[] array = new string[1];
			foreach (object obj in securityElement.Children)
			{
				SecurityElement securityElement2 = (SecurityElement)obj;
				array[0] = securityElement2.Attribute("name");
				ResourcePermissionBaseEntry resourcePermissionBaseEntry = new ResourcePermissionBaseEntry((int)Enum.Parse(this.PermissionAccessType, securityElement2.Attribute("access")), array);
				this.AddPermissionAccess(resourcePermissionBaseEntry);
			}
		}

		/// <summary>Creates and returns a permission object that is the intersection of the current permission object and a target permission object.</summary>
		/// <returns>A new permission object that represents the intersection of the current object and the specified target. This object is null if the intersection is empty.</returns>
		/// <param name="target">A permission object of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The target permission object is not of the same type as the current permission object. </exception>
		// Token: 0x060009DB RID: 2523 RVA: 0x00033540 File Offset: 0x00031740
		public override IPermission Intersect(IPermission target)
		{
			ResourcePermissionBase resourcePermissionBase = this.Cast(target);
			if (resourcePermissionBase == null)
			{
				return null;
			}
			bool flag = this.IsUnrestricted();
			bool flag2 = resourcePermissionBase.IsUnrestricted();
			if (this.IsEmpty() && !flag2)
			{
				return null;
			}
			if (resourcePermissionBase.IsEmpty() && !flag)
			{
				return null;
			}
			ResourcePermissionBase resourcePermissionBase2 = ResourcePermissionBase.CreateFromType(base.GetType(), flag && flag2);
			foreach (object obj in this._list)
			{
				ResourcePermissionBaseEntry resourcePermissionBaseEntry = (ResourcePermissionBaseEntry)obj;
				if (flag2 || resourcePermissionBase.Exists(resourcePermissionBaseEntry))
				{
					resourcePermissionBase2.AddPermissionAccess(resourcePermissionBaseEntry);
				}
			}
			foreach (object obj2 in resourcePermissionBase._list)
			{
				ResourcePermissionBaseEntry resourcePermissionBaseEntry2 = (ResourcePermissionBaseEntry)obj2;
				if ((flag || this.Exists(resourcePermissionBaseEntry2)) && !resourcePermissionBase2.Exists(resourcePermissionBaseEntry2))
				{
					resourcePermissionBase2.AddPermissionAccess(resourcePermissionBaseEntry2);
				}
			}
			return resourcePermissionBase2;
		}

		/// <summary>Determines whether the current permission object is a subset of the specified permission.</summary>
		/// <returns>true if the current permission object is a subset of the specified permission object; otherwise, false.</returns>
		/// <param name="target">A permission object that is to be tested for the subset relationship. </param>
		// Token: 0x060009DC RID: 2524 RVA: 0x0003365C File Offset: 0x0003185C
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return true;
			}
			ResourcePermissionBase resourcePermissionBase = target as ResourcePermissionBase;
			if (resourcePermissionBase == null)
			{
				return false;
			}
			if (resourcePermissionBase.IsUnrestricted())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return resourcePermissionBase.IsUnrestricted();
			}
			foreach (object obj in this._list)
			{
				ResourcePermissionBaseEntry resourcePermissionBaseEntry = (ResourcePermissionBaseEntry)obj;
				if (!resourcePermissionBase.Exists(resourcePermissionBaseEntry))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a value indicating whether the permission is unrestricted.</summary>
		/// <returns>true if permission is unrestricted; otherwise, false.</returns>
		// Token: 0x060009DD RID: 2525 RVA: 0x000336EC File Offset: 0x000318EC
		public bool IsUnrestricted()
		{
			return this._unrestricted;
		}

		/// <summary>Creates and returns an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x060009DE RID: 2526 RVA: 0x000336F4 File Offset: 0x000318F4
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(base.GetType(), 1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				foreach (object obj in this._list)
				{
					ResourcePermissionBaseEntry resourcePermissionBaseEntry = (ResourcePermissionBaseEntry)obj;
					SecurityElement securityElement2 = securityElement;
					string text = null;
					if (this.PermissionAccessType != null)
					{
						text = Enum.Format(this.PermissionAccessType, resourcePermissionBaseEntry.PermissionAccess, "g");
					}
					for (int i = 0; i < this._tags.Length; i++)
					{
						SecurityElement securityElement3 = new SecurityElement(this._tags[i]);
						securityElement3.AddAttribute("name", resourcePermissionBaseEntry.PermissionAccessPath[i]);
						if (text != null)
						{
							securityElement3.AddAttribute("access", text);
						}
						securityElement2.AddChild(securityElement3);
					}
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission object that combines the current permission object and the target permission object.</summary>
		/// <returns>A new permission object that represents the union of the current permission object and the specified permission object.</returns>
		/// <param name="target">A permission object to combine with the current permission object. It must be of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> permission object is not of the same type as the current permission object. </exception>
		// Token: 0x060009DF RID: 2527 RVA: 0x00033804 File Offset: 0x00031A04
		public override IPermission Union(IPermission target)
		{
			ResourcePermissionBase resourcePermissionBase = this.Cast(target);
			if (resourcePermissionBase == null)
			{
				return this.Copy();
			}
			if (this.IsEmpty() && resourcePermissionBase.IsEmpty())
			{
				return null;
			}
			if (resourcePermissionBase.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return resourcePermissionBase.Copy();
			}
			bool flag = this.IsUnrestricted() || resourcePermissionBase.IsUnrestricted();
			ResourcePermissionBase resourcePermissionBase2 = ResourcePermissionBase.CreateFromType(base.GetType(), flag);
			if (!flag)
			{
				foreach (object obj in this._list)
				{
					ResourcePermissionBaseEntry resourcePermissionBaseEntry = (ResourcePermissionBaseEntry)obj;
					resourcePermissionBase2.AddPermissionAccess(resourcePermissionBaseEntry);
				}
				foreach (object obj2 in resourcePermissionBase._list)
				{
					ResourcePermissionBaseEntry resourcePermissionBaseEntry2 = (ResourcePermissionBaseEntry)obj2;
					if (!resourcePermissionBase2.Exists(resourcePermissionBaseEntry2))
					{
						resourcePermissionBase2.AddPermissionAccess(resourcePermissionBaseEntry2);
					}
				}
			}
			return resourcePermissionBase2;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00033924 File Offset: 0x00031B24
		private bool IsEmpty()
		{
			return !this._unrestricted && this._list.Count == 0;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0003393E File Offset: 0x00031B3E
		private ResourcePermissionBase Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ResourcePermissionBase resourcePermissionBase = target as ResourcePermissionBase;
			if (resourcePermissionBase == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(ResourcePermissionBase));
			}
			return resourcePermissionBase;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0003395E File Offset: 0x00031B5E
		internal void CheckEntry(ResourcePermissionBaseEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (entry.PermissionAccessPath == null || entry.PermissionAccessPath.Length != this._tags.Length)
			{
				throw new InvalidOperationException(global::Locale.GetText("Entry doesn't match TagNames"));
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00033998 File Offset: 0x00031B98
		internal bool Equals(ResourcePermissionBaseEntry entry1, ResourcePermissionBaseEntry entry2)
		{
			if (entry1.PermissionAccess != entry2.PermissionAccess)
			{
				return false;
			}
			if (entry1.PermissionAccessPath.Length != entry2.PermissionAccessPath.Length)
			{
				return false;
			}
			for (int i = 0; i < entry1.PermissionAccessPath.Length; i++)
			{
				if (entry1.PermissionAccessPath[i] != entry2.PermissionAccessPath[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x000339F8 File Offset: 0x00031BF8
		internal bool Exists(ResourcePermissionBaseEntry entry)
		{
			if (this._list.Count == 0)
			{
				return false;
			}
			foreach (object obj in this._list)
			{
				ResourcePermissionBaseEntry resourcePermissionBaseEntry = (ResourcePermissionBaseEntry)obj;
				if (this.Equals(resourcePermissionBaseEntry, entry))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00033A6C File Offset: 0x00031C6C
		internal static ResourcePermissionBase CreateFromType(Type type, bool unrestricted)
		{
			return (ResourcePermissionBase)Activator.CreateInstance(type, new object[] { unrestricted ? PermissionState.Unrestricted : PermissionState.None });
		}

		// Token: 0x04000753 RID: 1875
		private ArrayList _list;

		// Token: 0x04000754 RID: 1876
		private bool _unrestricted;

		// Token: 0x04000755 RID: 1877
		private Type _type;

		// Token: 0x04000756 RID: 1878
		private string[] _tags;

		// Token: 0x04000757 RID: 1879
		private static char[] invalidChars = new char[] { '\t', '\n', '\v', '\f', '\r', ' ', '\\', 'Š' };
	}
}
