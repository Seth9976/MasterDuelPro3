using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace System.Security
{
	/// <summary>Represents a collection that can contain many different types of permissions.</summary>
	// Token: 0x02000323 RID: 803
	[ComVisible(true)]
	[MonoTODO("CAS support is experimental (and unsupported).")]
	[Serializable]
	public class PermissionSet : ISecurityEncodable, ICollection, IEnumerable, IStackWalk, IDeserializationCallback
	{
		// Token: 0x06001C8C RID: 7308 RVA: 0x0006F374 File Offset: 0x0006D574
		internal PermissionSet()
		{
			this.list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.PermissionSet" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the enumeration values that specifies the permission set's access to resources. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06001C8D RID: 7309 RVA: 0x0006F387 File Offset: 0x0006D587
		public PermissionSet(PermissionState state)
			: this()
		{
			this.state = CodeAccessPermission.CheckPermissionState(state, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.PermissionSet" /> class with initial values taken from the <paramref name="permSet" /> parameter.</summary>
		/// <param name="permSet">The set from which to take the value of the new <see cref="T:System.Security.PermissionSet" />, or null to create an empty <see cref="T:System.Security.PermissionSet" />. </param>
		// Token: 0x06001C8E RID: 7310 RVA: 0x0006F39C File Offset: 0x0006D59C
		public PermissionSet(PermissionSet permSet)
			: this()
		{
			if (permSet != null)
			{
				this.state = permSet.state;
				foreach (object obj in permSet.list)
				{
					IPermission permission = (IPermission)obj;
					this.list.Add(permission);
				}
			}
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0006F410 File Offset: 0x0006D610
		internal PermissionSet(string xml)
			: this()
		{
			this.state = PermissionState.None;
			if (xml != null)
			{
				SecurityElement securityElement = SecurityElement.FromString(xml);
				this.FromXml(securityElement);
			}
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0006F43B File Offset: 0x0006D63B
		internal PermissionSet(IPermission perm)
			: this()
		{
			if (perm != null)
			{
				this.list.Add(perm);
			}
		}

		/// <summary>Adds a specified permission to the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>The union of the permission added and any permission of the same type that already exists in the <see cref="T:System.Security.PermissionSet" />.</returns>
		/// <param name="perm">The permission to add. </param>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001C91 RID: 7313 RVA: 0x0006F454 File Offset: 0x0006D654
		public IPermission AddPermission(IPermission perm)
		{
			if (perm == null || this._readOnly)
			{
				return perm;
			}
			if (this.state == PermissionState.Unrestricted)
			{
				return (IPermission)Activator.CreateInstance(perm.GetType(), PermissionSet.psUnrestricted);
			}
			IPermission permission = this.RemovePermission(perm.GetType());
			if (permission != null)
			{
				perm = perm.Union(permission);
			}
			this.list.Add(perm);
			return perm;
		}

		/// <summary>Declares that the calling code can access the resource protected by a permission demand through the code that calls this method, even if callers higher in the stack have not been granted permission to access the resource. Using <see cref="M:System.Security.PermissionSet.Assert" /> can create security vulnerabilities.</summary>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Security.PermissionSet" /> instance asserted has not been granted to the asserting code.-or- There is already an active <see cref="M:System.Security.PermissionSet.Assert" /> for the current frame. </exception>
		// Token: 0x06001C92 RID: 7314 RVA: 0x0006F4B4 File Offset: 0x0006D6B4
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public void Assert()
		{
			int num = this.Count;
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				if (permission is IStackWalk)
				{
					if (!SecurityManager.IsGranted(permission))
					{
						return;
					}
				}
				else
				{
					num--;
				}
			}
			if (SecurityManager.SecurityEnabled && num > 0)
			{
				throw new NotSupportedException("Currently only declarative Assert are supported.");
			}
		}

		/// <summary>Creates a copy of the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Security.PermissionSet" />.</returns>
		// Token: 0x06001C93 RID: 7315 RVA: 0x0006F53C File Offset: 0x0006D73C
		public virtual PermissionSet Copy()
		{
			return new PermissionSet(this);
		}

		/// <summary>Copies the permission objects of the set to the indicated location in an <see cref="T:System.Array" />.</summary>
		/// <param name="array">The target array to which to copy. </param>
		/// <param name="index">The starting position in the array to begin copying (zero based). </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="array" /> parameter has more than one dimension. </exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is out of the range of the <paramref name="array" /> parameter. </exception>
		// Token: 0x06001C94 RID: 7316 RVA: 0x0006F544 File Offset: 0x0006D744
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (this.list.Count > 0)
			{
				if (array.Rank > 1)
				{
					throw new ArgumentException(Locale.GetText("Array has more than one dimension"));
				}
				if (index < 0 || index >= array.Length)
				{
					throw new IndexOutOfRangeException("index");
				}
				this.list.CopyTo(array, index);
			}
		}

		/// <summary>Forces a <see cref="T:System.Security.SecurityException" /> at run time if all callers higher in the call stack have not been granted the permissions specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have the permission demanded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06001C95 RID: 7317 RVA: 0x0006F5AC File Offset: 0x0006D7AC
		public void Demand()
		{
			if (this.IsEmpty())
			{
				return;
			}
			int count = this.list.Count;
			if (this._ignored == null || this._ignored.Length != count)
			{
				this._ignored = new bool[count];
			}
			bool flag = this.IsUnrestricted();
			for (int i = 0; i < count; i++)
			{
				IPermission permission = (IPermission)this.list[i];
				if (permission.GetType().IsSubclassOf(typeof(CodeAccessPermission)))
				{
					this._ignored[i] = false;
					flag = true;
				}
				else
				{
					this._ignored[i] = true;
					permission.Demand();
				}
			}
			if (flag && SecurityManager.SecurityEnabled)
			{
				this.CasOnlyDemand(this._declsec ? 5 : 3);
			}
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x0006F661 File Offset: 0x0006D861
		internal void CasOnlyDemand(int skip)
		{
			if (this._ignored == null)
			{
				this._ignored = new bool[this.list.Count];
			}
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid permission element.-or- The <paramref name="et" /> parameter's version number is not supported. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001C97 RID: 7319 RVA: 0x0006F684 File Offset: 0x0006D884
		public virtual void FromXml(SecurityElement et)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			if (et.Tag != "PermissionSet")
			{
				throw new ArgumentException(string.Format("Invalid tag {0} expected {1}", et.Tag, "PermissionSet"), "et");
			}
			this.list.Clear();
			if (CodeAccessPermission.IsUnrestricted(et))
			{
				this.state = PermissionState.Unrestricted;
				return;
			}
			this.state = PermissionState.None;
			if (et.Children != null)
			{
				foreach (object obj in et.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					string text = securityElement.Attribute("class");
					if (text == null)
					{
						throw new ArgumentException(Locale.GetText("No permission class is specified."));
					}
					if (this.Resolver != null)
					{
						text = this.Resolver.ResolveClassName(text);
					}
					this.list.Add(PermissionBuilder.Create(text, securityElement));
				}
			}
		}

		/// <summary>Returns an enumerator for the permissions of the set.</summary>
		/// <returns>An enumerator object for the permissions of the set.</returns>
		// Token: 0x06001C98 RID: 7320 RVA: 0x0006F788 File Offset: 0x0006D988
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Determines whether the current <see cref="T:System.Security.PermissionSet" /> is a subset of the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>true if the current <see cref="T:System.Security.PermissionSet" /> is a subset of the <paramref name="target" /> parameter; otherwise, false.</returns>
		/// <param name="target">The permission set to test for the subset relationship. This must be either a <see cref="T:System.Security.PermissionSet" /> or a <see cref="T:System.Security.NamedPermissionSet" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001C99 RID: 7321 RVA: 0x0006F798 File Offset: 0x0006D998
		public bool IsSubsetOf(PermissionSet target)
		{
			if (target == null || target.IsEmpty())
			{
				return this.IsEmpty();
			}
			if (target.IsUnrestricted())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return false;
			}
			if (this.IsUnrestricted() && (target == null || !target.IsUnrestricted()))
			{
				return false;
			}
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				Type type = permission.GetType();
				IPermission permission2;
				if (target.IsUnrestricted() && permission is CodeAccessPermission && permission is IUnrestrictedPermission)
				{
					permission2 = (IPermission)Activator.CreateInstance(type, PermissionSet.psUnrestricted);
				}
				else
				{
					permission2 = target.GetPermission(type);
				}
				if (!permission.IsSubsetOf(permission2))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a permission object of the specified type, if it exists in the set.</summary>
		/// <returns>A copy of the permission object of the type specified by the <paramref name="permClass" /> parameter contained in the <see cref="T:System.Security.PermissionSet" />, or null if none exists.</returns>
		/// <param name="permClass">The type of the desired permission object. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001C9A RID: 7322 RVA: 0x0006F878 File Offset: 0x0006DA78
		public IPermission GetPermission(Type permClass)
		{
			if (permClass == null || this.list.Count == 0)
			{
				return null;
			}
			foreach (object obj in this.list)
			{
				if (obj != null && obj.GetType().Equals(permClass))
				{
					return (IPermission)obj;
				}
			}
			return null;
		}

		/// <summary>Creates and returns a permission set that is the intersection of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A new permission set that represents the intersection of the current <see cref="T:System.Security.PermissionSet" /> and the specified target. This object is null if the intersection is empty.</returns>
		/// <param name="other">A permission set to intersect with the current <see cref="T:System.Security.PermissionSet" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001C9B RID: 7323 RVA: 0x0006F8FC File Offset: 0x0006DAFC
		public PermissionSet Intersect(PermissionSet other)
		{
			if (other == null || other.IsEmpty() || this.IsEmpty())
			{
				return null;
			}
			PermissionState permissionState = PermissionState.None;
			if (this.IsUnrestricted() && other.IsUnrestricted())
			{
				permissionState = PermissionState.Unrestricted;
			}
			PermissionSet permissionSet;
			if (permissionState == PermissionState.Unrestricted)
			{
				permissionSet = new PermissionSet(permissionState);
			}
			else if (this.IsUnrestricted())
			{
				permissionSet = other.Copy();
			}
			else if (other.IsUnrestricted())
			{
				permissionSet = this.Copy();
			}
			else
			{
				permissionSet = new PermissionSet(permissionState);
				this.InternalIntersect(permissionSet, this, other, false);
			}
			return permissionSet;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x0006F978 File Offset: 0x0006DB78
		internal void InternalIntersect(PermissionSet intersect, PermissionSet a, PermissionSet b, bool unrestricted)
		{
			foreach (object obj in b.list)
			{
				IPermission permission = (IPermission)obj;
				IPermission permission2 = a.GetPermission(permission.GetType());
				if (permission2 != null)
				{
					intersect.AddPermission(permission.Intersect(permission2));
				}
				else if (unrestricted)
				{
					intersect.AddPermission(permission);
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Security.PermissionSet" /> is empty.</summary>
		/// <returns>true if the <see cref="T:System.Security.PermissionSet" /> is empty; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001C9D RID: 7325 RVA: 0x0006F9F8 File Offset: 0x0006DBF8
		public bool IsEmpty()
		{
			if (this.state == PermissionState.Unrestricted)
			{
				return false;
			}
			if (this.list == null || this.list.Count == 0)
			{
				return true;
			}
			using (IEnumerator enumerator = this.list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!((IPermission)enumerator.Current).IsSubsetOf(null))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Determines whether the <see cref="T:System.Security.PermissionSet" /> is Unrestricted.</summary>
		/// <returns>true if the <see cref="T:System.Security.PermissionSet" /> is Unrestricted; otherwise, false.</returns>
		// Token: 0x06001C9E RID: 7326 RVA: 0x0006FA7C File Offset: 0x0006DC7C
		public bool IsUnrestricted()
		{
			return this.state == PermissionState.Unrestricted;
		}

		/// <summary>Removes a permission of a certain type from the set.</summary>
		/// <returns>The permission removed from the set.</returns>
		/// <param name="permClass">The type of permission to delete. </param>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x06001C9F RID: 7327 RVA: 0x0006FA88 File Offset: 0x0006DC88
		public IPermission RemovePermission(Type permClass)
		{
			if (permClass == null || this._readOnly)
			{
				return null;
			}
			foreach (object obj in this.list)
			{
				if (obj.GetType().Equals(permClass))
				{
					this.list.Remove(obj);
					return (IPermission)obj;
				}
			}
			return null;
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A representation of the <see cref="T:System.Security.PermissionSet" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001CA0 RID: 7328 RVA: 0x0006FB10 File Offset: 0x0006DD10
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001CA1 RID: 7329 RVA: 0x0006FB20 File Offset: 0x0006DD20
		public virtual SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("PermissionSet");
			securityElement.AddAttribute("class", base.GetType().FullName);
			securityElement.AddAttribute("version", 1.ToString());
			if (this.state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				securityElement.AddChild(permission.ToXml());
			}
			return securityElement;
		}

		/// <summary>Creates a <see cref="T:System.Security.PermissionSet" /> that is the union of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A new permission set that represents the union of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</returns>
		/// <param name="other">The permission set to form the union with the current <see cref="T:System.Security.PermissionSet" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001CA2 RID: 7330 RVA: 0x0006FBD0 File Offset: 0x0006DDD0
		public PermissionSet Union(PermissionSet other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			PermissionSet permissionSet = null;
			if (this.IsUnrestricted() || other.IsUnrestricted())
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
			permissionSet = this.Copy();
			foreach (object obj in other.list)
			{
				IPermission permission = (IPermission)obj;
				permissionSet.AddPermission(permission);
			}
			return permissionSet;
		}

		/// <summary>Gets the number of permission objects contained in the permission set.</summary>
		/// <returns>The number of permission objects contained in the <see cref="T:System.Security.PermissionSet" />.</returns>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x0006FC58 File Offset: 0x0006DE58
		public virtual int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is guaranteed to be thread safe.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x0006FC65 File Offset: 0x0006DE65
		public virtual bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets the root object of the current collection.</summary>
		/// <returns>The root object of the current collection.</returns>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x00002645 File Offset: 0x00000845
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		// Token: 0x06001CA6 RID: 7334 RVA: 0x00002C89 File Offset: 0x00000E89
		[MonoTODO("may not be required")]
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.PermissionSet" /> or <see cref="T:System.Security.NamedPermissionSet" /> object is equal to the current <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>true if the specified object is equal to the current <see cref="T:System.Security.PermissionSet" /> object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current <see cref="T:System.Security.PermissionSet" />. </param>
		// Token: 0x06001CA7 RID: 7335 RVA: 0x0006FC74 File Offset: 0x0006DE74
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			PermissionSet permissionSet = obj as PermissionSet;
			if (permissionSet == null)
			{
				return false;
			}
			if (this.state != permissionSet.state)
			{
				return false;
			}
			if (this.list.Count != permissionSet.Count)
			{
				return false;
			}
			for (int i = 0; i < this.list.Count; i++)
			{
				bool flag = false;
				int num = 0;
				while (i < permissionSet.list.Count)
				{
					if (this.list[i].Equals(permissionSet.list[num]))
					{
						flag = true;
						break;
					}
					num++;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.PermissionSet" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.PermissionSet" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001CA8 RID: 7336 RVA: 0x0006FD0E File Offset: 0x0006DF0E
		[ComVisible(false)]
		public override int GetHashCode()
		{
			if (this.list.Count != 0)
			{
				return base.GetHashCode();
			}
			return (int)this.state;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x0006FD2A File Offset: 0x0006DF2A
		// (set) Token: 0x06001CAA RID: 7338 RVA: 0x0006FD32 File Offset: 0x0006DF32
		internal PolicyLevel Resolver
		{
			get
			{
				return this._policyLevel;
			}
			set
			{
				this._policyLevel = value;
			}
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0006FD3B File Offset: 0x0006DF3B
		internal void SetReadOnly(bool value)
		{
			this._readOnly = value;
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0006FD44 File Offset: 0x0006DF44
		internal static PermissionSet CreateFromBinaryFormat(byte[] data)
		{
			if (data == null || data[0] != 46 || data.Length < 2)
			{
				throw new SecurityException(Locale.GetText("Invalid data in 2.0 metadata format."));
			}
			int num = 1;
			int num2 = PermissionSet.ReadEncodedInt(data, ref num);
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			for (int i = 0; i < num2; i++)
			{
				IPermission permission = PermissionSet.ProcessAttribute(data, ref num);
				if (permission == null)
				{
					throw new SecurityException(Locale.GetText("Unsupported data found in 2.0 metadata format."));
				}
				permissionSet.AddPermission(permission);
			}
			return permissionSet;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0006FDB8 File Offset: 0x0006DFB8
		internal static int ReadEncodedInt(byte[] data, ref int position)
		{
			int num;
			if ((data[position] & 128) == 0)
			{
				num = (int)data[position];
				position++;
			}
			else if ((data[position] & 64) == 0)
			{
				num = ((int)(data[position] & 63) << 8) | (int)data[position + 1];
				position += 2;
			}
			else
			{
				num = ((int)(data[position] & 31) << 24) | ((int)data[position + 1] << 16) | ((int)data[position + 2] << 8) | (int)data[position + 3];
				position += 4;
			}
			return num;
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0006FE30 File Offset: 0x0006E030
		internal static IPermission ProcessAttribute(byte[] data, ref int position)
		{
			int num = PermissionSet.ReadEncodedInt(data, ref position);
			string @string = Encoding.UTF8.GetString(data, position, num);
			position += num;
			Type type = Type.GetType(@string);
			SecurityAttribute securityAttribute = Activator.CreateInstance(type, PermissionSet.action) as SecurityAttribute;
			if (securityAttribute == null)
			{
				return null;
			}
			PermissionSet.ReadEncodedInt(data, ref position);
			int num2 = PermissionSet.ReadEncodedInt(data, ref position);
			for (int i = 0; i < num2; i++)
			{
				int num3 = position;
				position = num3 + 1;
				byte b = data[num3];
				bool flag;
				if (b != 83)
				{
					if (b != 84)
					{
						return null;
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = false;
				num3 = position;
				position = num3 + 1;
				byte b2 = data[num3];
				if (b2 == 29)
				{
					flag2 = true;
					num3 = position;
					position = num3 + 1;
					b2 = data[num3];
				}
				int num4 = PermissionSet.ReadEncodedInt(data, ref position);
				string string2 = Encoding.UTF8.GetString(data, position, num4);
				position += num4;
				int num5 = 1;
				if (flag2)
				{
					num5 = BitConverter.ToInt32(data, position);
					position += 4;
				}
				object[] array = null;
				for (int j = 0; j < num5; j++)
				{
					object obj;
					switch (b2)
					{
					case 2:
						num3 = position;
						position = num3 + 1;
						obj = Convert.ToBoolean(data[num3]);
						break;
					case 3:
						obj = Convert.ToChar(data[position]);
						position += 2;
						break;
					case 4:
						num3 = position;
						position = num3 + 1;
						obj = Convert.ToSByte(data[num3]);
						break;
					case 5:
						num3 = position;
						position = num3 + 1;
						obj = Convert.ToByte(data[num3]);
						break;
					case 6:
						obj = Convert.ToInt16(data[position]);
						position += 2;
						break;
					case 7:
						obj = Convert.ToUInt16(data[position]);
						position += 2;
						break;
					case 8:
						obj = Convert.ToInt32(data[position]);
						position += 4;
						break;
					case 9:
						obj = Convert.ToUInt32(data[position]);
						position += 4;
						break;
					case 10:
						obj = Convert.ToInt64(data[position]);
						position += 8;
						break;
					case 11:
						obj = Convert.ToUInt64(data[position]);
						position += 8;
						break;
					case 12:
						obj = Convert.ToSingle(data[position]);
						position += 4;
						break;
					case 13:
						obj = Convert.ToDouble(data[position]);
						position += 8;
						break;
					case 14:
					{
						string text = null;
						if (data[position] != 255)
						{
							int num6 = PermissionSet.ReadEncodedInt(data, ref position);
							text = Encoding.UTF8.GetString(data, position, num6);
							position += num6;
						}
						else
						{
							position++;
						}
						obj = text;
						break;
					}
					default:
					{
						if (b2 != 80)
						{
							return null;
						}
						int num7 = PermissionSet.ReadEncodedInt(data, ref position);
						obj = Type.GetType(Encoding.UTF8.GetString(data, position, num7));
						position += num7;
						break;
					}
					}
					if (flag)
					{
						type.GetProperty(string2).SetValue(securityAttribute, obj, array);
					}
					else
					{
						type.GetField(string2).SetValue(securityAttribute, obj);
					}
				}
			}
			return securityAttribute.CreatePermission();
		}

		// Token: 0x04000D20 RID: 3360
		private static object[] psUnrestricted = new object[] { PermissionState.Unrestricted };

		// Token: 0x04000D21 RID: 3361
		private PermissionState state;

		// Token: 0x04000D22 RID: 3362
		private ArrayList list;

		// Token: 0x04000D23 RID: 3363
		private PolicyLevel _policyLevel;

		// Token: 0x04000D24 RID: 3364
		private bool _declsec;

		// Token: 0x04000D25 RID: 3365
		private bool _readOnly;

		// Token: 0x04000D26 RID: 3366
		private bool[] _ignored;

		// Token: 0x04000D27 RID: 3367
		private static object[] action = new object[] { (SecurityAction)0 };
	}
}
