using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace System.Resources
{
	// Token: 0x020005C8 RID: 1480
	internal sealed class RuntimeResourceSet : ResourceSet, IEnumerable
	{
		// Token: 0x06002BC6 RID: 11206 RVA: 0x000AC99C File Offset: 0x000AAB9C
		internal RuntimeResourceSet(string fileName)
			: base(false)
		{
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this._defaultReader = new ResourceReader(stream, this._resCache);
			this.Reader = this._defaultReader;
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000AC9E8 File Offset: 0x000AABE8
		internal RuntimeResourceSet(Stream stream)
			: base(false)
		{
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._defaultReader = new ResourceReader(stream, this._resCache);
			this.Reader = this._defaultReader;
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000ACA20 File Offset: 0x000AAC20
		protected override void Dispose(bool disposing)
		{
			if (this.Reader == null)
			{
				return;
			}
			if (disposing)
			{
				IResourceReader reader = this.Reader;
				lock (reader)
				{
					this._resCache = null;
					if (this._defaultReader != null)
					{
						this._defaultReader.Close();
						this._defaultReader = null;
					}
					this._caseInsensitiveTable = null;
					base.Dispose(disposing);
					return;
				}
			}
			this._resCache = null;
			this._caseInsensitiveTable = null;
			this._defaultReader = null;
			base.Dispose(disposing);
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000ACAB4 File Offset: 0x000AACB4
		public override IDictionaryEnumerator GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000ACAB4 File Offset: 0x000AACB4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000ACABC File Offset: 0x000AACBC
		private IDictionaryEnumerator GetEnumeratorHelper()
		{
			IResourceReader reader = this.Reader;
			if (reader == null || this._resCache == null)
			{
				throw new ObjectDisposedException(null, "Cannot access a closed resource set.");
			}
			return reader.GetEnumerator();
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x000ACAE0 File Offset: 0x000AACE0
		public override string GetString(string key)
		{
			return (string)this.GetObject(key, false, true);
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000ACAF0 File Offset: 0x000AACF0
		public override string GetString(string key, bool ignoreCase)
		{
			return (string)this.GetObject(key, ignoreCase, true);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000ACB00 File Offset: 0x000AAD00
		public override object GetObject(string key)
		{
			return this.GetObject(key, false, false);
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000ACB0B File Offset: 0x000AAD0B
		public override object GetObject(string key, bool ignoreCase)
		{
			return this.GetObject(key, ignoreCase, false);
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000ACB18 File Offset: 0x000AAD18
		private object GetObject(string key, bool ignoreCase, bool isString)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.Reader == null || this._resCache == null)
			{
				throw new ObjectDisposedException(null, "Cannot access a closed resource set.");
			}
			object obj = null;
			IResourceReader reader = this.Reader;
			object obj3;
			lock (reader)
			{
				if (this.Reader == null)
				{
					throw new ObjectDisposedException(null, "Cannot access a closed resource set.");
				}
				ResourceLocator resourceLocator;
				if (this._defaultReader != null)
				{
					int num = -1;
					if (this._resCache.TryGetValue(key, out resourceLocator))
					{
						obj = resourceLocator.Value;
						num = resourceLocator.DataPosition;
					}
					if (num == -1 && obj == null)
					{
						num = this._defaultReader.FindPosForResource(key);
					}
					if (num != -1 && obj == null)
					{
						ResourceTypeCode resourceTypeCode;
						if (isString)
						{
							obj = this._defaultReader.LoadString(num);
							resourceTypeCode = ResourceTypeCode.String;
						}
						else
						{
							obj = this._defaultReader.LoadObject(num, out resourceTypeCode);
						}
						resourceLocator = new ResourceLocator(num, ResourceLocator.CanCache(resourceTypeCode) ? obj : null);
						Dictionary<string, ResourceLocator> resCache = this._resCache;
						lock (resCache)
						{
							this._resCache[key] = resourceLocator;
						}
					}
					if (obj != null || !ignoreCase)
					{
						return obj;
					}
				}
				if (!this._haveReadFromReader)
				{
					if (ignoreCase && this._caseInsensitiveTable == null)
					{
						this._caseInsensitiveTable = new Dictionary<string, ResourceLocator>(StringComparer.OrdinalIgnoreCase);
					}
					if (this._defaultReader == null)
					{
						IDictionaryEnumerator enumerator = this.Reader.GetEnumerator();
						while (enumerator.MoveNext())
						{
							DictionaryEntry entry = enumerator.Entry;
							string text = (string)entry.Key;
							ResourceLocator resourceLocator2 = new ResourceLocator(-1, entry.Value);
							this._resCache.Add(text, resourceLocator2);
							if (ignoreCase)
							{
								this._caseInsensitiveTable.Add(text, resourceLocator2);
							}
						}
						if (!ignoreCase)
						{
							this.Reader.Close();
						}
					}
					else
					{
						ResourceReader.ResourceEnumerator enumeratorInternal = this._defaultReader.GetEnumeratorInternal();
						while (enumeratorInternal.MoveNext())
						{
							string text2 = (string)enumeratorInternal.Key;
							int dataPosition = enumeratorInternal.DataPosition;
							ResourceLocator resourceLocator3 = new ResourceLocator(dataPosition, null);
							this._caseInsensitiveTable.Add(text2, resourceLocator3);
						}
					}
					this._haveReadFromReader = true;
				}
				object obj2 = null;
				bool flag3 = false;
				bool flag4 = false;
				if (this._defaultReader != null && this._resCache.TryGetValue(key, out resourceLocator))
				{
					flag3 = true;
					obj2 = this.ResolveResourceLocator(resourceLocator, key, this._resCache, flag4);
				}
				if (!flag3 && ignoreCase && this._caseInsensitiveTable.TryGetValue(key, out resourceLocator))
				{
					flag4 = true;
					obj2 = this.ResolveResourceLocator(resourceLocator, key, this._resCache, flag4);
				}
				obj3 = obj2;
			}
			return obj3;
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000ACDD4 File Offset: 0x000AAFD4
		private object ResolveResourceLocator(ResourceLocator resLocation, string key, Dictionary<string, ResourceLocator> copyOfCache, bool keyInWrongCase)
		{
			object obj = resLocation.Value;
			if (obj == null)
			{
				IResourceReader reader = this.Reader;
				ResourceTypeCode resourceTypeCode;
				lock (reader)
				{
					obj = this._defaultReader.LoadObject(resLocation.DataPosition, out resourceTypeCode);
				}
				if (!keyInWrongCase && ResourceLocator.CanCache(resourceTypeCode))
				{
					resLocation.Value = obj;
					copyOfCache[key] = resLocation;
				}
			}
			return obj;
		}

		// Token: 0x04001641 RID: 5697
		internal const int Version = 2;

		// Token: 0x04001642 RID: 5698
		private Dictionary<string, ResourceLocator> _resCache;

		// Token: 0x04001643 RID: 5699
		private ResourceReader _defaultReader;

		// Token: 0x04001644 RID: 5700
		private Dictionary<string, ResourceLocator> _caseInsensitiveTable;

		// Token: 0x04001645 RID: 5701
		private bool _haveReadFromReader;
	}
}
