using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for files originating in the global assembly cache. This class cannot be inherited.</summary>
	// Token: 0x0200035E RID: 862
	[ComVisible(true)]
	[Serializable]
	public sealed class GacIdentityPermission : CodeAccessPermission
	{
		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06001E25 RID: 7717 RVA: 0x00073BC7 File Offset: 0x00071DC7
		public override IPermission Copy()
		{
			return new GacIdentityPermission();
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. The new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001E26 RID: 7718 RVA: 0x000770EC File Offset: 0x000752EC
		public override IPermission Intersect(IPermission target)
		{
			if (this.Cast(target) == null)
			{
				return null;
			}
			return this.Copy();
		}

		/// <summary>Indicates whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission object to test for the subset relationship. The permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001E27 RID: 7719 RVA: 0x000770FF File Offset: 0x000752FF
		public override bool IsSubsetOf(IPermission target)
		{
			return this.Cast(target) != null;
		}

		/// <summary>Creates and returns a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001E28 RID: 7720 RVA: 0x0007710B File Offset: 0x0007530B
		public override IPermission Union(IPermission target)
		{
			this.Cast(target);
			return this.Copy();
		}

		/// <summary>Creates a permission from an XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" />  that contains the XML encoding to use to create the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" />is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a valid permission element. -or- The version number of <paramref name="securityElement" /> is not valid. </exception>
		// Token: 0x06001E29 RID: 7721 RVA: 0x0007711B File Offset: 0x0007531B
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.CheckSecurityElement(securityElement, "securityElement", 1, 1);
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that represents the XML encoding of the permission, including any state information.</returns>
		// Token: 0x06001E2A RID: 7722 RVA: 0x0007712B File Offset: 0x0007532B
		public override SecurityElement ToXml()
		{
			return base.Element(1);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00077134 File Offset: 0x00075334
		private GacIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			GacIdentityPermission gacIdentityPermission = target as GacIdentityPermission;
			if (gacIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(GacIdentityPermission));
			}
			return gacIdentityPermission;
		}
	}
}
