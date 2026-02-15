using System;

namespace System.Security
{
	/// <summary>Defines methods implemented by permission types.</summary>
	// Token: 0x0200030F RID: 783
	public interface IPermission : ISecurityEncodable
	{
		/// <summary>Throws a <see cref="T:System.Security.SecurityException" /> at run time if the security requirement is not met.</summary>
		// Token: 0x06001C4B RID: 7243
		void Demand();

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not an instance of the same class as the current permission. </exception>
		// Token: 0x06001C4C RID: 7244
		IPermission Intersect(IPermission target);

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001C4D RID: 7245
		bool IsSubsetOf(IPermission target);

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001C4E RID: 7246
		IPermission Union(IPermission target);
	}
}
