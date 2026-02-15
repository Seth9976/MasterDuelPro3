using System;
using System.Collections;
using System.Reflection;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeNamespace" /> objects.</summary>
	// Token: 0x02000207 RID: 519
	[DefaultMember("Item")]
	[Serializable]
	public class CodeNamespaceCollection : CollectionBase
	{
		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeNamespace" /> object to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespace" /> to add. </param>
		// Token: 0x06000C4F RID: 3151 RVA: 0x0003A510 File Offset: 0x00038710
		public int Add(CodeNamespace value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to begin inserting. </param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.-or- The number of elements in the <see cref="T:System.CodeDom.CodeNamespaceCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index. </exception>
		// Token: 0x06000C50 RID: 3152 RVA: 0x0003B1D9 File Offset: 0x000393D9
		public void CopyTo(CodeNamespace[] array, int index)
		{
			base.List.CopyTo(array, index);
		}
	}
}
