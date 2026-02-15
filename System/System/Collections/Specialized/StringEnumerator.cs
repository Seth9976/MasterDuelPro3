using System;

namespace System.Collections.Specialized
{
	/// <summary>Supports a simple iteration over a <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
	// Token: 0x02000306 RID: 774
	public class StringEnumerator
	{
		// Token: 0x060012F3 RID: 4851 RVA: 0x00054673 File Offset: 0x00052873
		internal StringEnumerator(StringCollection mappings)
		{
			this._temp = mappings;
			this._baseEnumerator = this._temp.GetEnumerator();
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x00054693 File Offset: 0x00052893
		public string Current
		{
			get
			{
				return (string)this._baseEnumerator.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x060012F5 RID: 4853 RVA: 0x000546A5 File Offset: 0x000528A5
		public bool MoveNext()
		{
			return this._baseEnumerator.MoveNext();
		}

		// Token: 0x04000B74 RID: 2932
		private IEnumerator _baseEnumerator;

		// Token: 0x04000B75 RID: 2933
		private IEnumerable _temp;
	}
}
