using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security
{
	/// <summary>Defines the underlying structure of all code access permissions.</summary>
	// Token: 0x0200031E RID: 798
	[MonoTODO("CAS support is experimental (and unsupported).")]
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeAccessPermission : IPermission, ISecurityEncodable, IStackWalk
	{
		/// <summary>Declares that the calling code can access the resource protected by a permission demand through the code that calls this method, even if callers higher in the stack have not been granted permission to access the resource. Using <see cref="M:System.Security.CodeAccessPermission.Assert" /> can create security issues.</summary>
		/// <exception cref="T:System.Security.SecurityException">The calling code does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Assertion" />.-or- There is already an active <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame. </exception>
		// Token: 0x06001C67 RID: 7271 RVA: 0x0006EEB1 File Offset: 0x0006D0B1
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public void Assert()
		{
			new PermissionSet(this).Assert();
		}

		/// <summary>When implemented by a derived class, creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x06001C68 RID: 7272
		public abstract IPermission Copy();

		/// <summary>Forces a <see cref="T:System.Security.SecurityException" /> at run time if all callers higher in the call stack have not been granted the permission specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have the permission specified by the current instance.-or- A caller higher in the call stack has called <see cref="M:System.Security.CodeAccessPermission.Deny" /> on the current permission object. </exception>
		// Token: 0x06001C69 RID: 7273 RVA: 0x0006EEBE File Offset: 0x0006D0BE
		public void Demand()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			new PermissionSet(this).CasOnlyDemand(3);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.CodeAccessPermission" /> object is equal to the current <see cref="T:System.Security.CodeAccessPermission" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.CodeAccessPermission" /> object is equal to the current <see cref="T:System.Security.CodeAccessPermission" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Security.CodeAccessPermission" /> object to compare with the current <see cref="T:System.Security.CodeAccessPermission" />. </param>
		// Token: 0x06001C6A RID: 7274 RVA: 0x0006EED4 File Offset: 0x0006D0D4
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj.GetType() != base.GetType())
			{
				return false;
			}
			CodeAccessPermission codeAccessPermission = obj as CodeAccessPermission;
			return this.IsSubsetOf(codeAccessPermission) && codeAccessPermission.IsSubsetOf(this);
		}

		/// <summary>When overridden in a derived class, reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="elem">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elem" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="elem" /> parameter does not contain the XML encoding for an instance of the same type as the current instance.-or- The version number of the <paramref name="elem" /> parameter is not supported. </exception>
		// Token: 0x06001C6B RID: 7275
		public abstract void FromXml(SecurityElement elem);

		/// <summary>Gets a hash code for the <see cref="T:System.Security.CodeAccessPermission" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.CodeAccessPermission" /> object.</returns>
		// Token: 0x06001C6C RID: 7276 RVA: 0x0006EF14 File Offset: 0x0006D114
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>When implemented by a derived class, creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not an instance of the same class as the current permission. </exception>
		// Token: 0x06001C6D RID: 7277
		public abstract IPermission Intersect(IPermission target);

		/// <summary>When implemented by a derived class, determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001C6E RID: 7278
		public abstract bool IsSubsetOf(IPermission target);

		/// <summary>Creates and returns a string representation of the current permission object.</summary>
		/// <returns>A string representation of the current permission object.</returns>
		// Token: 0x06001C6F RID: 7279 RVA: 0x0006EF1C File Offset: 0x0006D11C
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		/// <summary>When overridden in a derived class, creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06001C70 RID: 7280
		public abstract SecurityElement ToXml();

		/// <summary>When overridden in a derived class, creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="other" /> parameter is not null. This method is only supported at this level when passed null. </exception>
		// Token: 0x06001C71 RID: 7281 RVA: 0x0006EF29 File Offset: 0x0006D129
		public virtual IPermission Union(IPermission other)
		{
			if (other != null)
			{
				throw new NotSupportedException();
			}
			return null;
		}

		/// <summary>Causes any previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame to be removed and no longer in effect.</summary>
		/// <exception cref="T:System.InvalidOperationException">There is no previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame. </exception>
		// Token: 0x06001C72 RID: 7282 RVA: 0x0006EF35 File Offset: 0x0006D135
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public static void RevertAssert()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x0006EF44 File Offset: 0x0006D144
		internal SecurityElement Element(int version)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			Type type = base.GetType();
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x0006EFA3 File Offset: 0x0006D1A3
		internal static PermissionState CheckPermissionState(PermissionState state, bool allowUnrestricted)
		{
			if (state != PermissionState.None && state != PermissionState.Unrestricted)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), state), "state");
			}
			return state;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x0006EFD0 File Offset: 0x0006D1D0
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != "IPermission")
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid tag {0}"), se.Tag), parameterName);
			}
			int num = minimumVersion;
			string text = se.Attribute("version");
			if (text != null)
			{
				try
				{
					num = int.Parse(text);
				}
				catch (Exception ex)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Couldn't parse version from '{0}'."), text), parameterName, ex);
				}
			}
			if (num < minimumVersion || num > maximumVersion)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Unknown version '{0}', expected versions between ['{1}','{2}']."), num, minimumVersion, maximumVersion), parameterName);
			}
			return num;
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0006F08C File Offset: 0x0006D28C
		internal static bool IsUnrestricted(SecurityElement se)
		{
			string text = se.Attribute("Unrestricted");
			return text != null && string.Compare(text, bool.TrueString, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x0006F0BE File Offset: 0x0006D2BE
		internal static void ThrowInvalidPermission(IPermission target, Type expected)
		{
			throw new ArgumentException(string.Format(Locale.GetText("Invalid permission type '{0}', expected type '{1}'."), target.GetType(), expected), "target");
		}
	}
}
