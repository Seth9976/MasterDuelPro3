using System;
using System.Collections;
using System.Reflection;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeTypeMember" /> objects.</summary>
	// Token: 0x02000220 RID: 544
	[DefaultMember("Item")]
	[Serializable]
	public class CodeTypeMemberCollection : CollectionBase
	{
		/// <summary>Adds a <see cref="T:System.CodeDom.CodeTypeMember" /> with the specified value to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeMember" /> to add. </param>
		// Token: 0x06000CC3 RID: 3267 RVA: 0x0003A510 File Offset: 0x00038710
		public int Add(CodeTypeMember value)
		{
			return base.List.Add(value);
		}

		/// <summary>Removes a specific <see cref="T:System.CodeDom.CodeTypeMember" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeMember" /> to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection. </exception>
		// Token: 0x06000CC4 RID: 3268 RVA: 0x0003B9FB File Offset: 0x00039BFB
		public void Remove(CodeTypeMember value)
		{
			base.List.Remove(value);
		}
	}
}
