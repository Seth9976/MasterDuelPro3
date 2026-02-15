using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Net
{
	/// <summary>Contains protocol headers associated with a request or response.</summary>
	// Token: 0x020003BF RID: 959
	[ComVisible(true)]
	[DefaultMember("Item")]
	[Serializable]
	public class WebHeaderCollection : NameValueCollection, ISerializable
	{
		// Token: 0x060017C4 RID: 6084 RVA: 0x000650E8 File Offset: 0x000632E8
		private void NormalizeCommonHeaders()
		{
			if (this.m_CommonHeaders == null)
			{
				return;
			}
			for (int i = 0; i < this.m_CommonHeaders.Length; i++)
			{
				if (this.m_CommonHeaders[i] != null)
				{
					this.InnerCollection.Add(WebHeaderCollection.s_CommonHeaderNames[i], this.m_CommonHeaders[i]);
				}
			}
			this.m_CommonHeaders = null;
			this.m_NumCommonHeaders = 0;
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x00065143 File Offset: 0x00063343
		private NameValueCollection InnerCollection
		{
			get
			{
				if (this.m_InnerCollection == null)
				{
					this.m_InnerCollection = new NameValueCollection(16, CaseInsensitiveAscii.StaticInstance);
				}
				return this.m_InnerCollection;
			}
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00065168 File Offset: 0x00063368
		internal static bool AllowMultiValues(string name)
		{
			HeaderInfo headerInfo = WebHeaderCollection.HInfo[name];
			return headerInfo.AllowMultiValues || headerInfo.HeaderName == "";
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x0006519B File Offset: 0x0006339B
		private bool AllowHttpRequestHeader
		{
			get
			{
				if (this.m_Type == WebHeaderCollectionType.Unknown)
				{
					this.m_Type = WebHeaderCollectionType.WebRequest;
				}
				return this.m_Type == WebHeaderCollectionType.WebRequest || this.m_Type == WebHeaderCollectionType.HttpWebRequest || this.m_Type == WebHeaderCollectionType.HttpListenerRequest;
			}
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpRequestHeader" /> instance to remove from the collection. </param>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060017C8 RID: 6088 RVA: 0x000651C9 File Offset: 0x000633C9
		public void Remove(HttpRequestHeader header)
		{
			if (!this.AllowHttpRequestHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds response headers and cannot contain the specified request header."));
			}
			this.Remove(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header));
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x000651EF File Offset: 0x000633EF
		internal void AddInternal(string name, string value)
		{
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(name, value);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0006520A File Offset: 0x0006340A
		internal void ChangeInternal(string name, string value)
		{
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00065225 File Offset: 0x00063425
		internal void RemoveInternal(string name)
		{
			this.NormalizeCommonHeaders();
			if (this.m_InnerCollection != null)
			{
				base.InvalidateCachedArrays();
				this.m_InnerCollection.Remove(name);
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00065248 File Offset: 0x00063448
		internal static string CheckBadChars(string name, bool isHeaderValue)
		{
			if (name != null && name.Length != 0)
			{
				if (isHeaderValue)
				{
					name = name.Trim(WebHeaderCollection.HttpTrimCharacters);
					int num = 0;
					for (int i = 0; i < name.Length; i++)
					{
						char c = 'ÿ' & name[i];
						switch (num)
						{
						case 0:
							if (c == '\r')
							{
								num = 1;
							}
							else if (c == '\n')
							{
								num = 2;
							}
							else if (c == '\u007f' || (c < ' ' && c != '\t'))
							{
								throw new ArgumentException(SR.GetString("Specified value has invalid Control characters."), "value");
							}
							break;
						case 1:
							if (c != '\n')
							{
								throw new ArgumentException(SR.GetString("Specified value has invalid CRLF characters."), "value");
							}
							num = 2;
							break;
						case 2:
							if (c != ' ' && c != '\t')
							{
								throw new ArgumentException(SR.GetString("Specified value has invalid CRLF characters."), "value");
							}
							num = 0;
							break;
						}
					}
					if (num != 0)
					{
						throw new ArgumentException(SR.GetString("Specified value has invalid CRLF characters."), "value");
					}
				}
				else
				{
					if (name.IndexOfAny(ValidationHelper.InvalidParamChars) != -1)
					{
						throw new ArgumentException(SR.GetString("Specified value has invalid HTTP Header characters."), "name");
					}
					if (WebHeaderCollection.ContainsNonAsciiChars(name))
					{
						throw new ArgumentException(SR.GetString("Specified value has invalid non-ASCII characters."), "name");
					}
				}
				return name;
			}
			if (!isHeaderValue)
			{
				throw (name == null) ? new ArgumentNullException("name") : new ArgumentException(SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "name" }), "name");
			}
			return string.Empty;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x000653BC File Offset: 0x000635BC
		internal static bool ContainsNonAsciiChars(string token)
		{
			for (int i = 0; i < token.Length; i++)
			{
				if (token[i] < ' ' || token[i] > '~')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x000653F4 File Offset: 0x000635F4
		internal void ThrowOnRestrictedHeader(string headerName)
		{
			if (this.m_Type == WebHeaderCollectionType.HttpWebRequest)
			{
				if (WebHeaderCollection.HInfo[headerName].IsRequestRestricted)
				{
					throw new ArgumentException(SR.GetString("The '{0}' header must be modified using the appropriate property or method.", new object[] { headerName }), "name");
				}
			}
			else if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && WebHeaderCollection.HInfo[headerName].IsResponseRestricted)
			{
				throw new ArgumentException(SR.GetString("The '{0}' header must be modified using the appropriate property or method.", new object[] { headerName }), "name");
			}
		}

		/// <summary>Inserts a header with the specified name and value into the collection.</summary>
		/// <param name="name">The header to add to the collection. </param>
		/// <param name="value">The content of the header. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null, <see cref="F:System.String.Empty" />, or contains invalid characters.-or- <paramref name="name" /> is a restricted header that must be set with a property setting.-or- <paramref name="value" /> contains invalid characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		// Token: 0x060017CF RID: 6095 RVA: 0x00065478 File Offset: 0x00063678
		public override void Add(string name, string value)
		{
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.ThrowOnRestrictedHeader(name);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(name, value);
		}

		/// <summary>Inserts the specified header into the collection.</summary>
		/// <param name="header">The header to add, with the name and value separated by a colon. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="header" /> is null or <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="header" /> does not contain a colon (:) character.The length of <paramref name="value" /> is greater than 65535.-or- The name part of <paramref name="header" /> is <see cref="F:System.String.Empty" /> or contains invalid characters.-or- <paramref name="header" /> is a restricted header that should be set with a property.-or- The value part of <paramref name="header" /> contains invalid characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length the string after the colon (:) is greater than 65535. </exception>
		// Token: 0x060017D0 RID: 6096 RVA: 0x000654FC File Offset: 0x000636FC
		public void Add(string header)
		{
			if (ValidationHelper.IsBlankString(header))
			{
				throw new ArgumentNullException("header");
			}
			int num = header.IndexOf(':');
			if (num < 0)
			{
				throw new ArgumentException(SR.GetString("Specified value does not have a ':' separator."), "header");
			}
			string text = header.Substring(0, num);
			string text2 = header.Substring(num + 1);
			text = WebHeaderCollection.CheckBadChars(text, false);
			this.ThrowOnRestrictedHeader(text);
			text2 = WebHeaderCollection.CheckBadChars(text2, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && text2 != null && text2.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", text2, SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(text, text2);
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="name">The header to set. </param>
		/// <param name="value">The content of the header to set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null or <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a restricted header.-or- <paramref name="name" /> or <paramref name="value" /> contain invalid characters. </exception>
		// Token: 0x060017D1 RID: 6097 RVA: 0x000655C4 File Offset: 0x000637C4
		public override void Set(string name, string value)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.ThrowOnRestrictedHeader(name);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00065658 File Offset: 0x00063858
		internal void SetInternal(string name, string value)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			name = WebHeaderCollection.CheckBadChars(name, false);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="name">The name of the header to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null<see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a restricted header.-or- <paramref name="name" /> contains invalid characters. </exception>
		// Token: 0x060017D3 RID: 6099 RVA: 0x000656E8 File Offset: 0x000638E8
		public override void Remove(string name)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			this.ThrowOnRestrictedHeader(name);
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.NormalizeCommonHeaders();
			if (this.m_InnerCollection != null)
			{
				base.InvalidateCachedArrays();
				this.m_InnerCollection.Remove(name);
			}
		}

		/// <summary>Gets an array of header values stored in a header.</summary>
		/// <returns>An array of header strings.</returns>
		/// <param name="header">The header to return. </param>
		// Token: 0x060017D4 RID: 6100 RVA: 0x00065738 File Offset: 0x00063938
		public override string[] GetValues(string header)
		{
			this.NormalizeCommonHeaders();
			HeaderInfo headerInfo = WebHeaderCollection.HInfo[header];
			string[] values = this.InnerCollection.GetValues(header);
			if (headerInfo == null || values == null || !headerInfo.AllowMultiValues)
			{
				return values;
			}
			ArrayList arrayList = null;
			for (int i = 0; i < values.Length; i++)
			{
				string[] array = headerInfo.Parser(values[i]);
				if (arrayList == null)
				{
					if (array.Length > 1)
					{
						arrayList = new ArrayList(values);
						arrayList.RemoveRange(i, values.Length - i);
						arrayList.AddRange(array);
					}
				}
				else
				{
					arrayList.AddRange(array);
				}
			}
			if (arrayList != null)
			{
				string[] array2 = new string[arrayList.Count];
				arrayList.CopyTo(array2);
				return array2;
			}
			return values;
		}

		/// <summary>This method is obsolete.</summary>
		/// <returns>The <see cref="T:System.String" /> representation of the collection.</returns>
		// Token: 0x060017D5 RID: 6101 RVA: 0x000657E2 File Offset: 0x000639E2
		public override string ToString()
		{
			return WebHeaderCollection.GetAsString(this, false, false);
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000657EC File Offset: 0x000639EC
		internal static string GetAsString(NameValueCollection cc, bool winInetCompat, bool forTrace)
		{
			if (winInetCompat)
			{
				throw new InvalidOperationException();
			}
			if (cc == null || cc.Count == 0)
			{
				return "\r\n";
			}
			StringBuilder stringBuilder = new StringBuilder(30 * cc.Count);
			string text = cc[string.Empty];
			if (text != null)
			{
				stringBuilder.Append(text).Append("\r\n");
			}
			for (int i = 0; i < cc.Count; i++)
			{
				string key = cc.GetKey(i);
				string text2 = cc.Get(i);
				if (!ValidationHelper.IsBlankString(key))
				{
					stringBuilder.Append(key);
					if (winInetCompat)
					{
						stringBuilder.Append(':');
					}
					else
					{
						stringBuilder.Append(": ");
					}
					stringBuilder.Append(text2).Append("\r\n");
				}
			}
			if (!forTrace)
			{
				stringBuilder.Append("\r\n");
			}
			return stringBuilder.ToString();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebHeaderCollection" /> class.</summary>
		// Token: 0x060017D7 RID: 6103 RVA: 0x000658B7 File Offset: 0x00063AB7
		public WebHeaderCollection()
			: base(DBNull.Value)
		{
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x000658C4 File Offset: 0x00063AC4
		internal WebHeaderCollection(WebHeaderCollectionType type)
			: base(DBNull.Value)
		{
			this.m_Type = type;
			if (type == WebHeaderCollectionType.HttpWebResponse)
			{
				this.m_CommonHeaders = new string[WebHeaderCollection.s_CommonHeaderNames.Length - 1];
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebHeaderCollection" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> containing the information required to serialize the <see cref="T:System.Net.WebHeaderCollection" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> containing the source of the serialized stream associated with the new <see cref="T:System.Net.WebHeaderCollection" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is a null reference or <see cref="F:System.String.Empty" />. </exception>
		// Token: 0x060017D9 RID: 6105 RVA: 0x000658F0 File Offset: 0x00063AF0
		protected WebHeaderCollection(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(DBNull.Value)
		{
			int @int = serializationInfo.GetInt32("Count");
			this.m_InnerCollection = new NameValueCollection(@int + 2, CaseInsensitiveAscii.StaticInstance);
			for (int i = 0; i < @int; i++)
			{
				string @string = serializationInfo.GetString(i.ToString(NumberFormatInfo.InvariantInfo));
				string string2 = serializationInfo.GetString((i + @int).ToString(NumberFormatInfo.InvariantInfo));
				this.InnerCollection.Add(@string, string2);
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x060017DA RID: 6106 RVA: 0x00002FA0 File Offset: 0x000011A0
		public override void OnDeserialization(object sender)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x060017DB RID: 6107 RVA: 0x0006596C File Offset: 0x00063B6C
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.NormalizeCommonHeaders();
			serializationInfo.AddValue("Count", this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				serializationInfo.AddValue(i.ToString(NumberFormatInfo.InvariantInfo), this.GetKey(i));
				serializationInfo.AddValue((i + this.Count).ToString(NumberFormatInfo.InvariantInfo), this.Get(i));
			}
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.WebHeaderCollection" /> will be serialized. </param>
		/// <param name="streamingContext">The destination of the serialization. </param>
		// Token: 0x060017DC RID: 6108 RVA: 0x000659DC File Offset: 0x00063BDC
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Get the value of a particular header in the collection, specified by the name of the header.</summary>
		/// <returns>A <see cref="T:System.String" /> holding the value of the specified header.</returns>
		/// <param name="name">The name of the Web header.</param>
		// Token: 0x060017DD RID: 6109 RVA: 0x000659E8 File Offset: 0x00063BE8
		public override string Get(string name)
		{
			if (this.m_CommonHeaders != null && name != null && name.Length > 0 && name[0] < 'Ā')
			{
				int num = (int)WebHeaderCollection.s_CommonHeaderHints[(int)(name[0] & '\u001f')];
				if (num >= 0)
				{
					for (;;)
					{
						string text = WebHeaderCollection.s_CommonHeaderNames[num++];
						if (text.Length < name.Length || CaseInsensitiveAscii.AsciiToLower[(int)name[0]] != CaseInsensitiveAscii.AsciiToLower[(int)text[0]])
						{
							goto IL_00EF;
						}
						if (text.Length <= name.Length)
						{
							int num2 = 1;
							while (num2 < text.Length && (name[num2] == text[num2] || (name[num2] <= 'ÿ' && CaseInsensitiveAscii.AsciiToLower[(int)name[num2]] == CaseInsensitiveAscii.AsciiToLower[(int)text[num2]])))
							{
								num2++;
							}
							if (num2 == text.Length)
							{
								break;
							}
						}
					}
					return this.m_CommonHeaders[num - 1];
				}
			}
			IL_00EF:
			if (this.m_InnerCollection == null)
			{
				return null;
			}
			return this.m_InnerCollection.Get(name);
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Net.WebHeaderCollection" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Net.WebHeaderCollection" />.</returns>
		// Token: 0x060017DE RID: 6110 RVA: 0x00065AFA File Offset: 0x00063CFA
		public override IEnumerator GetEnumerator()
		{
			this.NormalizeCommonHeaders();
			return new NameObjectCollectionBase.NameObjectKeysEnumerator(this.InnerCollection);
		}

		/// <summary>Gets the number of headers in the collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> indicating the number of headers in a request.</returns>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x00065B0D File Offset: 0x00063D0D
		public override int Count
		{
			get
			{
				return ((this.m_InnerCollection == null) ? 0 : this.m_InnerCollection.Count) + this.m_NumCommonHeaders;
			}
		}

		/// <summary>Gets the collection of header names (keys) in the collection.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> containing all header names in a Web request.</returns>
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x00065B2C File Offset: 0x00063D2C
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				this.NormalizeCommonHeaders();
				return this.InnerCollection.Keys;
			}
		}

		/// <summary>Get the value of a particular header in the collection, specified by an index into the collection.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value of the specified header.</returns>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is negative. -or-<paramref name="index" /> exceeds the size of the collection.</exception>
		// Token: 0x060017E1 RID: 6113 RVA: 0x00065B3F File Offset: 0x00063D3F
		public override string Get(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.Get(index);
		}

		/// <summary>Gets an array of header values stored in the <paramref name="index" /> position of the header collection.</summary>
		/// <returns>An array of header strings.</returns>
		/// <param name="index">The header index to return.</param>
		// Token: 0x060017E2 RID: 6114 RVA: 0x00065B53 File Offset: 0x00063D53
		public override string[] GetValues(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.GetValues(index);
		}

		/// <summary>Get the header name at the specified position in the collection.</summary>
		/// <returns>A <see cref="T:System.String" /> holding the header name.</returns>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is negative. -or-<paramref name="index" /> exceeds the size of the collection.</exception>
		// Token: 0x060017E3 RID: 6115 RVA: 0x00065B67 File Offset: 0x00063D67
		public override string GetKey(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.GetKey(index);
		}

		/// <summary>Gets all header names (keys) in the collection.</summary>
		/// <returns>An array of type <see cref="T:System.String" /> containing all header names in a Web request.</returns>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x00065B7B File Offset: 0x00063D7B
		public override string[] AllKeys
		{
			get
			{
				this.NormalizeCommonHeaders();
				return this.InnerCollection.AllKeys;
			}
		}

		// Token: 0x04000F0B RID: 3851
		private const int ApproxAveHeaderLineSize = 30;

		// Token: 0x04000F0C RID: 3852
		private const int ApproxHighAvgNumHeaders = 16;

		// Token: 0x04000F0D RID: 3853
		private static readonly HeaderInfoTable HInfo = new HeaderInfoTable();

		// Token: 0x04000F0E RID: 3854
		private string[] m_CommonHeaders;

		// Token: 0x04000F0F RID: 3855
		private int m_NumCommonHeaders;

		// Token: 0x04000F10 RID: 3856
		private static readonly string[] s_CommonHeaderNames = new string[]
		{
			"Accept-Ranges", "Content-Length", "Cache-Control", "Content-Type", "Date", "Expires", "ETag", "Last-Modified", "Location", "Proxy-Authenticate",
			"P3P", "Set-Cookie2", "Set-Cookie", "Server", "Via", "WWW-Authenticate", "X-AspNet-Version", "X-Powered-By", "["
		};

		// Token: 0x04000F11 RID: 3857
		private static readonly sbyte[] s_CommonHeaderHints = new sbyte[]
		{
			-1, 0, -1, 1, 4, 5, -1, -1, -1, -1,
			-1, -1, 7, -1, -1, -1, 9, -1, -1, 11,
			-1, -1, 14, 15, 16, -1, -1, -1, -1, -1,
			-1, -1
		};

		// Token: 0x04000F12 RID: 3858
		private const int c_AcceptRanges = 0;

		// Token: 0x04000F13 RID: 3859
		private const int c_ContentLength = 1;

		// Token: 0x04000F14 RID: 3860
		private const int c_CacheControl = 2;

		// Token: 0x04000F15 RID: 3861
		private const int c_ContentType = 3;

		// Token: 0x04000F16 RID: 3862
		private const int c_Date = 4;

		// Token: 0x04000F17 RID: 3863
		private const int c_Expires = 5;

		// Token: 0x04000F18 RID: 3864
		private const int c_ETag = 6;

		// Token: 0x04000F19 RID: 3865
		private const int c_LastModified = 7;

		// Token: 0x04000F1A RID: 3866
		private const int c_Location = 8;

		// Token: 0x04000F1B RID: 3867
		private const int c_ProxyAuthenticate = 9;

		// Token: 0x04000F1C RID: 3868
		private const int c_P3P = 10;

		// Token: 0x04000F1D RID: 3869
		private const int c_SetCookie2 = 11;

		// Token: 0x04000F1E RID: 3870
		private const int c_SetCookie = 12;

		// Token: 0x04000F1F RID: 3871
		private const int c_Server = 13;

		// Token: 0x04000F20 RID: 3872
		private const int c_Via = 14;

		// Token: 0x04000F21 RID: 3873
		private const int c_WwwAuthenticate = 15;

		// Token: 0x04000F22 RID: 3874
		private const int c_XAspNetVersion = 16;

		// Token: 0x04000F23 RID: 3875
		private const int c_XPoweredBy = 17;

		// Token: 0x04000F24 RID: 3876
		private NameValueCollection m_InnerCollection;

		// Token: 0x04000F25 RID: 3877
		private WebHeaderCollectionType m_Type;

		// Token: 0x04000F26 RID: 3878
		private static readonly char[] HttpTrimCharacters = new char[] { '\t', '\n', '\v', '\f', '\r', ' ' };

		// Token: 0x04000F27 RID: 3879
		private static WebHeaderCollection.RfcChar[] RfcCharMap = new WebHeaderCollection.RfcChar[]
		{
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.WS,
			WebHeaderCollection.RfcChar.LF,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.CR,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.WS,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Colon,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Ctl
		};

		// Token: 0x020003C0 RID: 960
		private enum RfcChar : byte
		{
			// Token: 0x04000F29 RID: 3881
			High,
			// Token: 0x04000F2A RID: 3882
			Reg,
			// Token: 0x04000F2B RID: 3883
			Ctl,
			// Token: 0x04000F2C RID: 3884
			CR,
			// Token: 0x04000F2D RID: 3885
			LF,
			// Token: 0x04000F2E RID: 3886
			WS,
			// Token: 0x04000F2F RID: 3887
			Colon,
			// Token: 0x04000F30 RID: 3888
			Delim
		}
	}
}
