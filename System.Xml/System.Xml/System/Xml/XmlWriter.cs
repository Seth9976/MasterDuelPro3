using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a writer that provides a fast, non-cached, forward-only means of generating streams or files containing XML data.</summary>
	// Token: 0x020000C1 RID: 193
	public abstract class XmlWriter : IDisposable
	{
		/// <summary>Gets the <see cref="T:System.Xml.XmlWriterSettings" /> object used to create this <see cref="T:System.Xml.XmlWriter" /> instance.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlWriterSettings" /> object used to create this writer instance. If this writer was not created using the <see cref="Overload:System.Xml.XmlWriter.Create" /> method, this property returns null.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00014C6C File Offset: 0x00012E6C
		public virtual XmlWriterSettings Settings
		{
			get
			{
				return null;
			}
		}

		/// <summary>When overridden in a derived class, writes the XML declaration with the version "1.0".</summary>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000921 RID: 2337
		public abstract void WriteStartDocument();

		/// <summary>When overridden in a derived class, writes the XML declaration with the version "1.0" and the standalone attribute.</summary>
		/// <param name="standalone">If true, it writes "standalone=yes"; if false, it writes "standalone=no".</param>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000922 RID: 2338
		public abstract void WriteStartDocument(bool standalone);

		/// <summary>When overridden in a derived class, closes any open elements or attributes and puts the writer back in the Start state.</summary>
		/// <exception cref="T:System.ArgumentException">The XML document is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000923 RID: 2339
		public abstract void WriteEndDocument();

		/// <summary>When overridden in a derived class, writes the DOCTYPE declaration with the specified name and optional attributes.</summary>
		/// <param name="name">The name of the DOCTYPE. This must be non-empty.</param>
		/// <param name="pubid">If non-null it also writes PUBLIC "pubid" "sysid" where <paramref name="pubid" /> and <paramref name="sysid" /> are replaced with the value of the given arguments.</param>
		/// <param name="sysid">If <paramref name="pubid" /> is null and <paramref name="sysid" /> is non-null it writes SYSTEM "sysid" where <paramref name="sysid" /> is replaced with the value of this argument.</param>
		/// <param name="subset">If non-null it writes [subset] where subset is replaced with the value of this argument.</param>
		/// <exception cref="T:System.InvalidOperationException">This method was called outside the prolog (after the root element). </exception>
		/// <exception cref="T:System.ArgumentException">The value for <paramref name="name" /> would result in invalid XML.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000924 RID: 2340
		public abstract void WriteDocType(string name, string pubid, string sysid, string subset);

		/// <summary>When overridden in a derived class, writes the specified start tag and associates it with the given namespace.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI to associate with the element. If this namespace is already in scope and has an associated prefix, the writer automatically writes that prefix also.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000925 RID: 2341 RVA: 0x000342C7 File Offset: 0x000324C7
		public void WriteStartElement(string localName, string ns)
		{
			this.WriteStartElement(null, localName, ns);
		}

		/// <summary>When overridden in a derived class, writes the specified start tag and associates it with the given namespace and prefix.</summary>
		/// <param name="prefix">The namespace prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI to associate with the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000926 RID: 2342
		public abstract void WriteStartElement(string prefix, string localName, string ns);

		/// <summary>When overridden in a derived class, writes out a start tag with the specified local name.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000927 RID: 2343 RVA: 0x000342D2 File Offset: 0x000324D2
		public void WriteStartElement(string localName)
		{
			this.WriteStartElement(null, localName, null);
		}

		/// <summary>When overridden in a derived class, closes one element and pops the corresponding namespace scope.</summary>
		/// <exception cref="T:System.InvalidOperationException">This results in an invalid XML document.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000928 RID: 2344
		public abstract void WriteEndElement();

		/// <summary>When overridden in a derived class, closes one element and pops the corresponding namespace scope.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000929 RID: 2345
		public abstract void WriteFullEndElement();

		/// <summary>When overridden in a derived class, writes an attribute with the specified local name, namespace URI, and value.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI to associate with the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The state of writer is not WriteState.Element or writer is closed. </exception>
		/// <exception cref="T:System.ArgumentException">The xml:space or xml:lang attribute value is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600092A RID: 2346 RVA: 0x000342DD File Offset: 0x000324DD
		public void WriteAttributeString(string localName, string ns, string value)
		{
			this.WriteStartAttribute(null, localName, ns);
			this.WriteString(value);
			this.WriteEndAttribute();
		}

		/// <summary>When overridden in a derived class, writes out the attribute with the specified local name and value.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The state of writer is not WriteState.Element or writer is closed. </exception>
		/// <exception cref="T:System.ArgumentException">The xml:space or xml:lang attribute value is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600092B RID: 2347 RVA: 0x000342F5 File Offset: 0x000324F5
		public void WriteAttributeString(string localName, string value)
		{
			this.WriteStartAttribute(null, localName, null);
			this.WriteString(value);
			this.WriteEndAttribute();
		}

		/// <summary>When overridden in a derived class, writes out the attribute with the specified prefix, local name, namespace URI, and value.</summary>
		/// <param name="prefix">The namespace prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The state of writer is not WriteState.Element or writer is closed. </exception>
		/// <exception cref="T:System.ArgumentException">The xml:space or xml:lang attribute value is invalid. </exception>
		/// <exception cref="T:System.Xml.XmlException">The <paramref name="localName" /> or <paramref name="ns" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600092C RID: 2348 RVA: 0x0003430D File Offset: 0x0003250D
		public void WriteAttributeString(string prefix, string localName, string ns, string value)
		{
			this.WriteStartAttribute(prefix, localName, ns);
			this.WriteString(value);
			this.WriteEndAttribute();
		}

		/// <summary>Writes the start of an attribute with the specified local name and namespace URI.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI of the attribute.</param>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600092D RID: 2349 RVA: 0x00034326 File Offset: 0x00032526
		public void WriteStartAttribute(string localName, string ns)
		{
			this.WriteStartAttribute(null, localName, ns);
		}

		/// <summary>When overridden in a derived class, writes the start of an attribute with the specified prefix, local name, and namespace URI.</summary>
		/// <param name="prefix">The namespace prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI for the attribute.</param>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600092E RID: 2350
		public abstract void WriteStartAttribute(string prefix, string localName, string ns);

		/// <summary>Writes the start of an attribute with the specified local name.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600092F RID: 2351 RVA: 0x00034331 File Offset: 0x00032531
		public void WriteStartAttribute(string localName)
		{
			this.WriteStartAttribute(null, localName, null);
		}

		/// <summary>When overridden in a derived class, closes the previous <see cref="M:System.Xml.XmlWriter.WriteStartAttribute(System.String,System.String)" /> call.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000930 RID: 2352
		public abstract void WriteEndAttribute();

		/// <summary>When overridden in a derived class, writes out a &lt;![CDATA[...]]&gt; block containing the specified text.</summary>
		/// <param name="text">The text to place inside the CDATA block.</param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000931 RID: 2353
		public abstract void WriteCData(string text);

		/// <summary>When overridden in a derived class, writes out a comment &lt;!--...--&gt; containing the specified text.</summary>
		/// <param name="text">Text to place inside the comment.</param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well-formed XML document.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000932 RID: 2354
		public abstract void WriteComment(string text);

		/// <summary>When overridden in a derived class, writes out a processing instruction with a space between the name and text as follows: &lt;?name text?&gt;.</summary>
		/// <param name="name">The name of the processing instruction.</param>
		/// <param name="text">The text to include in the processing instruction.</param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document.<paramref name="name" /> is either null or String.Empty.This method is being used to create an XML declaration after <see cref="M:System.Xml.XmlWriter.WriteStartDocument" /> has already been called. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000933 RID: 2355
		public abstract void WriteProcessingInstruction(string name, string text);

		/// <summary>When overridden in a derived class, writes out an entity reference as &amp;name;.</summary>
		/// <param name="name">The name of the entity reference.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is either null or String.Empty.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000934 RID: 2356
		public abstract void WriteEntityRef(string name);

		/// <summary>When overridden in a derived class, forces the generation of a character entity for the specified Unicode character value.</summary>
		/// <param name="ch">The Unicode character for which to generate a character entity.</param>
		/// <exception cref="T:System.ArgumentException">The character is in the surrogate pair character range, 0xd800 - 0xdfff.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000935 RID: 2357
		public abstract void WriteCharEntity(char ch);

		/// <summary>When overridden in a derived class, writes out the given white space.</summary>
		/// <param name="ws">The string of white space characters.</param>
		/// <exception cref="T:System.ArgumentException">The string contains non-white space characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000936 RID: 2358
		public abstract void WriteWhitespace(string ws);

		/// <summary>When overridden in a derived class, writes the given text content.</summary>
		/// <param name="text">The text to write.</param>
		/// <exception cref="T:System.ArgumentException">The text string contains an invalid surrogate pair.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000937 RID: 2359
		public abstract void WriteString(string text);

		/// <summary>When overridden in a derived class, generates and writes the surrogate character entity for the surrogate character pair.</summary>
		/// <param name="lowChar">The low surrogate. This must be a value between 0xDC00 and 0xDFFF.</param>
		/// <param name="highChar">The high surrogate. This must be a value between 0xD800 and 0xDBFF.</param>
		/// <exception cref="T:System.ArgumentException">An invalid surrogate character pair was passed.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000938 RID: 2360
		public abstract void WriteSurrogateCharEntity(char lowChar, char highChar);

		/// <summary>When overridden in a derived class, writes text one buffer at a time.</summary>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position in the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />; the call results in surrogate pair characters being split or an invalid surrogate pair being written.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="buffer" /> parameter value is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000939 RID: 2361
		public abstract void WriteChars(char[] buffer, int index, int count);

		/// <summary>When overridden in a derived class, writes raw markup manually from a character buffer.</summary>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position within the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600093A RID: 2362
		public abstract void WriteRaw(char[] buffer, int index, int count);

		/// <summary>When overridden in a derived class, writes raw markup manually from a string.</summary>
		/// <param name="data">String containing the text to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="data" /> is either null or String.Empty.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600093B RID: 2363
		public abstract void WriteRaw(string data);

		/// <summary>When overridden in a derived class, encodes the specified binary bytes as Base64 and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode.</param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600093C RID: 2364
		public abstract void WriteBase64(byte[] buffer, int index, int count);

		/// <summary>When overridden in a derived class, encodes the specified binary bytes as BinHex and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode.</param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed or in error state.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600093D RID: 2365 RVA: 0x0003433C File Offset: 0x0003253C
		public virtual void WriteBinHex(byte[] buffer, int index, int count)
		{
			BinHexEncoder.Encode(buffer, index, count, this);
		}

		/// <summary>When overridden in a derived class, gets the state of the writer.</summary>
		/// <returns>One of the <see cref="T:System.Xml.WriteState" /> values.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600093E RID: 2366
		public abstract WriteState WriteState { get; }

		/// <summary>When overridden in a derived class, closes this stream and the underlying stream.</summary>
		/// <exception cref="T:System.InvalidOperationException">A call is made to write more output after Close has been called or the result of this call is an invalid XML document.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600093F RID: 2367 RVA: 0x0000A558 File Offset: 0x00008758
		public virtual void Close()
		{
		}

		/// <summary>When overridden in a derived class, flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000940 RID: 2368
		public abstract void Flush();

		/// <summary>When overridden in a derived class, returns the closest prefix defined in the current namespace scope for the namespace URI.</summary>
		/// <returns>The matching prefix or null if no matching namespace URI is found in the current scope.</returns>
		/// <param name="ns">The namespace URI whose prefix you want to find.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ns" /> is either null or String.Empty.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000941 RID: 2369
		public abstract string LookupPrefix(string ns);

		/// <summary>When overridden in a derived class, gets an <see cref="T:System.Xml.XmlSpace" /> representing the current xml:space scope.</summary>
		/// <returns>An XmlSpace representing the current xml:space scope.Value Meaning NoneThis is the default if no xml:space scope exists.DefaultThe current scope is xml:space="default".PreserveThe current scope is xml:space="preserve".</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public virtual XmlSpace XmlSpace
		{
			get
			{
				return XmlSpace.Default;
			}
		}

		/// <summary>When overridden in a derived class, gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00015451 File Offset: 0x00013651
		public virtual string XmlLang
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>When overridden in a derived class, writes out the specified name, ensuring it is a valid NmToken according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">The name to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid NmToken; or <paramref name="name" /> is either null or String.Empty.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000944 RID: 2372 RVA: 0x00034347 File Offset: 0x00032547
		public virtual void WriteNmToken(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
			}
			this.WriteString(XmlConvert.VerifyNMTOKEN(name, ExceptionType.ArgumentException));
		}

		/// <summary>When overridden in a derived class, writes out the specified name, ensuring it is a valid name according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">The name to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid XML name; or <paramref name="name" /> is either null or String.Empty.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000945 RID: 2373 RVA: 0x00034371 File Offset: 0x00032571
		public virtual void WriteName(string name)
		{
			this.WriteString(XmlConvert.VerifyQName(name, ExceptionType.ArgumentException));
		}

		/// <summary>When overridden in a derived class, writes out the namespace-qualified name. This method looks up the prefix that is in scope for the given namespace.</summary>
		/// <param name="localName">The local name to write.</param>
		/// <param name="ns">The namespace URI for the name.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="localName" /> is either null or String.Empty.<paramref name="localName" /> is not a valid name. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000946 RID: 2374 RVA: 0x00034380 File Offset: 0x00032580
		public virtual void WriteQualifiedName(string localName, string ns)
		{
			if (ns != null && ns.Length > 0)
			{
				string text = this.LookupPrefix(ns);
				if (text == null)
				{
					throw new ArgumentException(Res.GetString("The '{0}' namespace is not defined.", new object[] { ns }));
				}
				this.WriteString(text);
				this.WriteString(":");
			}
			this.WriteString(localName);
		}

		/// <summary>Writes the object value.</summary>
		/// <param name="value">The object value to write.Note   With the release of the .NET Framework 3.5, this method accepts <see cref="T:System.DateTimeOffset" /> as a parameter.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed or in error state.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000947 RID: 2375 RVA: 0x000343D7 File Offset: 0x000325D7
		public virtual void WriteValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.WriteString(XmlUntypedConverter.Untyped.ToString(value, null));
		}

		/// <summary>Writes a <see cref="T:System.String" /> value.</summary>
		/// <param name="value">The <see cref="T:System.String" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000948 RID: 2376 RVA: 0x000343F9 File Offset: 0x000325F9
		public virtual void WriteValue(string value)
		{
			if (value == null)
			{
				return;
			}
			this.WriteString(value);
		}

		/// <summary>Writes a <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Boolean" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000949 RID: 2377 RVA: 0x00034406 File Offset: 0x00032606
		public virtual void WriteValue(bool value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600094A RID: 2378 RVA: 0x00034414 File Offset: 0x00032614
		public virtual void WriteValue(DateTime value)
		{
			this.WriteString(XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind));
		}

		/// <summary>Writes a <see cref="T:System.Double" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Double" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600094B RID: 2379 RVA: 0x00034423 File Offset: 0x00032623
		public virtual void WriteValue(double value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a single-precision floating-point number.</summary>
		/// <param name="value">The single-precision floating-point number to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600094C RID: 2380 RVA: 0x00034431 File Offset: 0x00032631
		public virtual void WriteValue(float value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Decimal" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600094D RID: 2381 RVA: 0x0003443F File Offset: 0x0003263F
		public virtual void WriteValue(decimal value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a <see cref="T:System.Int32" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Int32" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600094E RID: 2382 RVA: 0x0003444D File Offset: 0x0003264D
		public virtual void WriteValue(int value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a <see cref="T:System.Int64" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Int64" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600094F RID: 2383 RVA: 0x0003445B File Offset: 0x0003265B
		public virtual void WriteValue(long value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>When overridden in a derived class, writes out all the attributes found at the current position in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The XmlReader from which to copy the attributes.</param>
		/// <param name="defattr">true to copy the default attributes from the XmlReader; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is null. </exception>
		/// <exception cref="T:System.Xml.XmlException">The reader is not positioned on an element, attribute or XmlDeclaration node. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000950 RID: 2384 RVA: 0x0003446C File Offset: 0x0003266C
		public virtual void WriteAttributes(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.XmlDeclaration)
			{
				if (reader.MoveToFirstAttribute())
				{
					this.WriteAttributes(reader, defattr);
					reader.MoveToElement();
					return;
				}
			}
			else
			{
				if (reader.NodeType != XmlNodeType.Attribute)
				{
					throw new XmlException("The current position on the Reader is neither an element nor an attribute.", string.Empty);
				}
				do
				{
					if (defattr || !reader.IsDefaultInternal)
					{
						this.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
						while (reader.ReadAttributeValue())
						{
							if (reader.NodeType == XmlNodeType.EntityReference)
							{
								this.WriteEntityRef(reader.Name);
							}
							else
							{
								this.WriteString(reader.Value);
							}
						}
						this.WriteEndAttribute();
					}
				}
				while (reader.MoveToNextAttribute());
			}
		}

		/// <summary>When overridden in a derived class, copies everything from the reader to the writer and moves the reader to the start of the next sibling.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> to read from.</param>
		/// <param name="defattr">true to copy the default attributes from the XmlReader; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="reader" /> contains invalid characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000951 RID: 2385 RVA: 0x0003452C File Offset: 0x0003272C
		public virtual void WriteNode(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			bool canReadValueChunk = reader.CanReadValueChunk;
			int num = ((reader.NodeType == XmlNodeType.None) ? (-1) : reader.Depth);
			do
			{
				switch (reader.NodeType)
				{
				case XmlNodeType.Element:
					this.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
					this.WriteAttributes(reader, defattr);
					if (reader.IsEmptyElement)
					{
						this.WriteEndElement();
					}
					break;
				case XmlNodeType.Text:
					if (canReadValueChunk)
					{
						if (this.writeNodeBuffer == null)
						{
							this.writeNodeBuffer = new char[1024];
						}
						int num2;
						while ((num2 = reader.ReadValueChunk(this.writeNodeBuffer, 0, 1024)) > 0)
						{
							this.WriteChars(this.writeNodeBuffer, 0, num2);
						}
					}
					else
					{
						this.WriteString(reader.Value);
					}
					break;
				case XmlNodeType.CDATA:
					this.WriteCData(reader.Value);
					break;
				case XmlNodeType.EntityReference:
					this.WriteEntityRef(reader.Name);
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.XmlDeclaration:
					this.WriteProcessingInstruction(reader.Name, reader.Value);
					break;
				case XmlNodeType.Comment:
					this.WriteComment(reader.Value);
					break;
				case XmlNodeType.DocumentType:
					this.WriteDocType(reader.Name, reader.GetAttribute("PUBLIC"), reader.GetAttribute("SYSTEM"), reader.Value);
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					this.WriteWhitespace(reader.Value);
					break;
				case XmlNodeType.EndElement:
					this.WriteFullEndElement();
					break;
				}
			}
			while (reader.Read() && (num < reader.Depth || (num == reader.Depth && reader.NodeType == XmlNodeType.EndElement)));
		}

		/// <summary>Copies everything from the <see cref="T:System.Xml.XPath.XPathNavigator" /> object to the writer. The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> remains unchanged.</summary>
		/// <param name="navigator">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to copy from.</param>
		/// <param name="defattr">true to copy the default attributes; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="navigator" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000952 RID: 2386 RVA: 0x000346EC File Offset: 0x000328EC
		public virtual void WriteNode(XPathNavigator navigator, bool defattr)
		{
			if (navigator == null)
			{
				throw new ArgumentNullException("navigator");
			}
			int num = 0;
			navigator = navigator.Clone();
			for (;;)
			{
				IL_0018:
				bool flag = false;
				switch (navigator.NodeType)
				{
				case XPathNodeType.Root:
					flag = true;
					break;
				case XPathNodeType.Element:
					this.WriteStartElement(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
					if (navigator.MoveToFirstAttribute())
					{
						do
						{
							IXmlSchemaInfo schemaInfo = navigator.SchemaInfo;
							if (defattr || schemaInfo == null || !schemaInfo.IsDefault)
							{
								this.WriteStartAttribute(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
								this.WriteString(navigator.Value);
								this.WriteEndAttribute();
							}
						}
						while (navigator.MoveToNextAttribute());
						navigator.MoveToParent();
					}
					if (navigator.MoveToFirstNamespace(XPathNamespaceScope.Local))
					{
						this.WriteLocalNamespaces(navigator);
						navigator.MoveToParent();
					}
					flag = true;
					break;
				case XPathNodeType.Text:
					this.WriteString(navigator.Value);
					break;
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
					this.WriteWhitespace(navigator.Value);
					break;
				case XPathNodeType.ProcessingInstruction:
					this.WriteProcessingInstruction(navigator.LocalName, navigator.Value);
					break;
				case XPathNodeType.Comment:
					this.WriteComment(navigator.Value);
					break;
				}
				if (flag)
				{
					if (navigator.MoveToFirstChild())
					{
						num++;
						continue;
					}
					if (navigator.NodeType == XPathNodeType.Element)
					{
						if (navigator.IsEmptyElement)
						{
							this.WriteEndElement();
						}
						else
						{
							this.WriteFullEndElement();
						}
					}
				}
				while (num != 0)
				{
					if (navigator.MoveToNext())
					{
						goto IL_0018;
					}
					num--;
					navigator.MoveToParent();
					if (navigator.NodeType == XPathNodeType.Element)
					{
						this.WriteFullEndElement();
					}
				}
				break;
			}
		}

		/// <summary>Writes an element with the specified local name and value.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="value">The value of the element.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="localName" /> value is null or an empty string.-or-The parameter values are not valid.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000953 RID: 2387 RVA: 0x0003486F File Offset: 0x00032A6F
		public void WriteElementString(string localName, string value)
		{
			this.WriteElementString(localName, null, value);
		}

		/// <summary>Writes an element with the specified local name, namespace URI, and value.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI to associate with the element.</param>
		/// <param name="value">The value of the element.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="localName" /> value is null or an empty string.-or-The parameter values are not valid.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000954 RID: 2388 RVA: 0x0003487A File Offset: 0x00032A7A
		public void WriteElementString(string localName, string ns, string value)
		{
			this.WriteStartElement(localName, ns);
			if (value != null && value.Length != 0)
			{
				this.WriteString(value);
			}
			this.WriteEndElement();
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Xml.XmlWriter" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000955 RID: 2389 RVA: 0x0003489C File Offset: 0x00032A9C
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Xml.XmlWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000956 RID: 2390 RVA: 0x000348A5 File Offset: 0x00032AA5
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.WriteState != WriteState.Closed)
			{
				this.Close();
			}
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000348BC File Offset: 0x00032ABC
		private void WriteLocalNamespaces(XPathNavigator nsNav)
		{
			string localName = nsNav.LocalName;
			string value = nsNav.Value;
			if (nsNav.MoveToNextNamespace(XPathNamespaceScope.Local))
			{
				this.WriteLocalNamespaces(nsNav);
			}
			if (localName.Length == 0)
			{
				this.WriteAttributeString(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/", value);
				return;
			}
			this.WriteAttributeString("xmlns", localName, "http://www.w3.org/2000/xmlns/", value);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the stream and <see cref="T:System.Xml.XmlWriterSettings" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The stream to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> writes XML 1.0 text syntax and appends it to the specified stream.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> value is null.</exception>
		// Token: 0x06000958 RID: 2392 RVA: 0x00034918 File Offset: 0x00032B18
		public static XmlWriter Create(Stream output, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return settings.CreateWriter(output);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the <see cref="T:System.IO.TextWriter" /> and <see cref="T:System.Xml.XmlWriterSettings" /> objects.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> writes XML 1.0 text syntax and appends it to the specified <see cref="T:System.IO.TextWriter" />.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="text" /> value is null.</exception>
		// Token: 0x06000959 RID: 2393 RVA: 0x0003492B File Offset: 0x00032B2B
		public static XmlWriter Create(TextWriter output, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return settings.CreateWriter(output);
		}

		/// <summary>Asynchronously writes the XML declaration with the version "1.0".</summary>
		/// <returns>The task that represents the asynchronous WriteStartDocument operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600095A RID: 2394 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public virtual Task WriteStartDocumentAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes the start of an attribute with the specified prefix, local name, and namespace URI.</summary>
		/// <returns>The task that represents the asynchronous WriteStartAttribute operation.</returns>
		/// <param name="prefix">The namespace prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI for the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600095B RID: 2395 RVA: 0x00002CE0 File Offset: 0x00000EE0
		protected internal virtual Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously closes the previous <see cref="M:System.Xml.XmlWriter.WriteStartAttribute(System.String,System.String)" /> call.</summary>
		/// <returns>The task that represents the asynchronous WriteEndAttribute operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600095C RID: 2396 RVA: 0x00002CE0 File Offset: 0x00000EE0
		protected internal virtual Task WriteEndAttributeAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out an entity reference as &amp;name;.</summary>
		/// <returns>The task that represents the asynchronous WriteEntityRef operation.</returns>
		/// <param name="name">The name of the entity reference.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600095D RID: 2397 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public virtual Task WriteEntityRefAsync(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously forces the generation of a character entity for the specified Unicode character value.</summary>
		/// <returns>The task that represents the asynchronous WriteCharEntity operation.</returns>
		/// <param name="ch">The Unicode character for which to generate a character entity.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600095E RID: 2398 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public virtual Task WriteCharEntityAsync(char ch)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out the given white space.</summary>
		/// <returns>The task that represents the asynchronous WriteWhitespace operation.</returns>
		/// <param name="ws">The string of white space characters.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600095F RID: 2399 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public virtual Task WriteWhitespaceAsync(string ws)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes the given text content.</summary>
		/// <returns>The task that represents the asynchronous WriteString operation.</returns>
		/// <param name="text">The text to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000960 RID: 2400 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public virtual Task WriteStringAsync(string text)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously generates and writes the surrogate character entity for the surrogate character pair.</summary>
		/// <returns>The task that represents the asynchronous WriteSurrogateCharEntity operation.</returns>
		/// <param name="lowChar">The low surrogate. This must be a value between 0xDC00 and 0xDFFF.</param>
		/// <param name="highChar">The high surrogate. This must be a value between 0xD800 and 0xDBFF.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000961 RID: 2401 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public virtual Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes text one buffer at a time.</summary>
		/// <returns>The task that represents the asynchronous WriteChars operation.</returns>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position in the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000962 RID: 2402 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public virtual Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes raw markup manually from a character buffer.</summary>
		/// <returns>The task that represents the asynchronous WriteRaw operation.</returns>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position within the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000963 RID: 2403 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public virtual Task WriteRawAsync(char[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes raw markup manually from a string.</summary>
		/// <returns>The task that represents the asynchronous WriteRaw operation.</returns>
		/// <param name="data">String containing the text to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000964 RID: 2404 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public virtual Task WriteRawAsync(string data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously encodes the specified binary bytes as Base64 and writes out the resulting text.</summary>
		/// <returns>The task that represents the asynchronous WriteBase64 operation.</returns>
		/// <param name="buffer">Byte array to encode.</param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to true. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000965 RID: 2405 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public virtual Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000568 RID: 1384
		private char[] writeNodeBuffer;
	}
}
