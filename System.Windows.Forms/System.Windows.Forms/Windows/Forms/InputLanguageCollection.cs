using System;
using System.Collections;

namespace System.Windows.Forms
{
	/// <summary>Stores <see cref="T:System.Windows.Forms.InputLanguage" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000DD RID: 221
	public class InputLanguageCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x0002309F File Offset: 0x0002129F
		internal InputLanguageCollection(InputLanguage[] data)
		{
			base.InnerList.AddRange(data);
		}

		/// <summary>Gets the entry at the specified index of the <see cref="T:System.Windows.Forms.InputLanguageCollection" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.InputLanguage" /> at the specified index of the collection.</returns>
		/// <param name="index">The zero-based index of the entry to locate in the collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001FE RID: 510
		public InputLanguage this[int index]
		{
			get
			{
				if (index >= base.InnerList.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return base.InnerList[index] as InputLanguage;
			}
		}
	}
}
