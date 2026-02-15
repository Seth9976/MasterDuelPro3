using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Resources
{
	/// <summary>Stores all the resources localized for one particular culture, ignoring all other cultures, including any fallback rules.</summary>
	// Token: 0x020005D6 RID: 1494
	[ComVisible(true)]
	[Serializable]
	public class ResourceSet : IDisposable, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceSet" /> class with default properties.</summary>
		// Token: 0x06002C46 RID: 11334 RVA: 0x000AFF71 File Offset: 0x000AE171
		protected ResourceSet()
		{
			this.CommonInit();
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal ResourceSet(bool junk)
		{
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000AFF7F File Offset: 0x000AE17F
		private void CommonInit()
		{
			this.Table = new Hashtable();
		}

		/// <summary>Releases resources (other than memory) associated with the current instance, closing internal managed objects if requested.</summary>
		/// <param name="disposing">Indicates whether the objects contained in the current instance should be explicitly closed. </param>
		// Token: 0x06002C49 RID: 11337 RVA: 0x000AFF8C File Offset: 0x000AE18C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				IResourceReader reader = this.Reader;
				this.Reader = null;
				if (reader != null)
				{
					reader.Close();
				}
			}
			this.Reader = null;
			this._caseInsensitiveTable = null;
			this.Table = null;
		}

		/// <summary>Disposes of the resources (other than memory) used by the current instance of <see cref="T:System.Resources.ResourceSet" />.</summary>
		// Token: 0x06002C4A RID: 11338 RVA: 0x000AFFC8 File Offset: 0x000AE1C8
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that can iterate through the <see cref="T:System.Resources.ResourceSet" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for this <see cref="T:System.Resources.ResourceSet" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The resource set has been closed or disposed. </exception>
		// Token: 0x06002C4B RID: 11339 RVA: 0x000AFFD1 File Offset: 0x000AE1D1
		[ComVisible(false)]
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> object to avoid a race condition with Dispose. This member is not intended to be used directly from your code.</summary>
		/// <returns>An enumerator for the current <see cref="T:System.Resources.ResourceSet" /> object.</returns>
		// Token: 0x06002C4C RID: 11340 RVA: 0x000AFFD1 File Offset: 0x000AE1D1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000AFFD9 File Offset: 0x000AE1D9
		private IDictionaryEnumerator GetEnumeratorHelper()
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed resource set."));
			}
			return table.GetEnumerator();
		}

		/// <summary>Searches for a <see cref="T:System.String" /> resource with the specified name.</summary>
		/// <returns>The value of a resource, if the value is a <see cref="T:System.String" />.</returns>
		/// <param name="name">Name of the resource to search for. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The resource specified by <paramref name="name" /> is not a <see cref="T:System.String" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x06002C4E RID: 11342 RVA: 0x000AFFFC File Offset: 0x000AE1FC
		public virtual string GetString(string name)
		{
			object objectInternal = this.GetObjectInternal(name);
			string text;
			try
			{
				text = (string)objectInternal;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Resource '{0}' was not a String - call GetObject instead.", new object[] { name }));
			}
			return text;
		}

		/// <summary>Searches for a <see cref="T:System.String" /> resource with the specified name in a case-insensitive manner, if requested.</summary>
		/// <returns>The value of a resource, if the value is a <see cref="T:System.String" />.</returns>
		/// <param name="name">Name of the resource to search for. </param>
		/// <param name="ignoreCase">Indicates whether the case of the case of the specified name should be ignored. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The resource specified by <paramref name="name" /> is not a <see cref="T:System.String" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x06002C4F RID: 11343 RVA: 0x000B0048 File Offset: 0x000AE248
		public virtual string GetString(string name, bool ignoreCase)
		{
			object obj = this.GetObjectInternal(name);
			string text;
			try
			{
				text = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Resource '{0}' was not a String - call GetObject instead.", new object[] { name }));
			}
			if (text != null || !ignoreCase)
			{
				return text;
			}
			obj = this.GetCaseInsensitiveObjectInternal(name);
			string text2;
			try
			{
				text2 = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Resource '{0}' was not a String - call GetObject instead.", new object[] { name }));
			}
			return text2;
		}

		/// <summary>Searches for a resource object with the specified name.</summary>
		/// <returns>The requested resource.</returns>
		/// <param name="name">Case-sensitive name of the resource to search for. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x06002C50 RID: 11344 RVA: 0x000B00D4 File Offset: 0x000AE2D4
		public virtual object GetObject(string name)
		{
			return this.GetObjectInternal(name);
		}

		/// <summary>Searches for a resource object with the specified name in a case-insensitive manner, if requested.</summary>
		/// <returns>The requested resource.</returns>
		/// <param name="name">Name of the resource to search for. </param>
		/// <param name="ignoreCase">Indicates whether the case of the specified name should be ignored. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x06002C51 RID: 11345 RVA: 0x000B00E0 File Offset: 0x000AE2E0
		public virtual object GetObject(string name, bool ignoreCase)
		{
			object objectInternal = this.GetObjectInternal(name);
			if (objectInternal != null || !ignoreCase)
			{
				return objectInternal;
			}
			return this.GetCaseInsensitiveObjectInternal(name);
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000B0104 File Offset: 0x000AE304
		private object GetObjectInternal(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed resource set."));
			}
			return table[name];
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000B0134 File Offset: 0x000AE334
		private object GetCaseInsensitiveObjectInternal(string name)
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed resource set."));
			}
			Hashtable hashtable = this._caseInsensitiveTable;
			if (hashtable == null)
			{
				hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
				IDictionaryEnumerator enumerator = table.GetEnumerator();
				while (enumerator.MoveNext())
				{
					hashtable.Add(enumerator.Key, enumerator.Value);
				}
				this._caseInsensitiveTable = hashtable;
			}
			return hashtable[name];
		}

		/// <summary>Indicates the <see cref="T:System.Resources.IResourceReader" /> used to read the resources.</summary>
		// Token: 0x0400167D RID: 5757
		[NonSerialized]
		protected IResourceReader Reader;

		/// <summary>The <see cref="T:System.Collections.Hashtable" /> in which the resources are stored.</summary>
		// Token: 0x0400167E RID: 5758
		protected Hashtable Table;

		// Token: 0x0400167F RID: 5759
		private Hashtable _caseInsensitiveTable;
	}
}
