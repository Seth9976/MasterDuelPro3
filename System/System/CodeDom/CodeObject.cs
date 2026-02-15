using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.CodeDom
{
	/// <summary>Provides a common base class for most Code Document Object Model (CodeDOM) objects.</summary>
	// Token: 0x020001D5 RID: 469
	[Serializable]
	public class CodeObject
	{
		/// <summary>Gets the user-definable data for the current object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing user data for the current object.</returns>
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x00039EF8 File Offset: 0x000380F8
		public IDictionary UserData
		{
			get
			{
				IDictionary dictionary;
				if ((dictionary = this._userData) == null)
				{
					dictionary = (this._userData = new ListDictionary());
				}
				return dictionary;
			}
		}

		// Token: 0x04000867 RID: 2151
		private IDictionary _userData;
	}
}
