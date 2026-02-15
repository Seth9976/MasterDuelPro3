using System;

namespace System
{
	/// <summary>Defines the kinds of <see cref="T:System.Uri" />s for the <see cref="M:System.Uri.IsWellFormedUriString(System.String,System.UriKind)" /> and several <see cref="Overload:System.Uri.#ctor" /> methods.</summary>
	// Token: 0x020000F8 RID: 248
	public enum UriKind
	{
		/// <summary>The kind of the Uri is indeterminate.</summary>
		// Token: 0x04000404 RID: 1028
		RelativeOrAbsolute,
		/// <summary>The Uri is an absolute Uri.</summary>
		// Token: 0x04000405 RID: 1029
		Absolute,
		/// <summary>The Uri is a relative Uri.</summary>
		// Token: 0x04000406 RID: 1030
		Relative
	}
}
