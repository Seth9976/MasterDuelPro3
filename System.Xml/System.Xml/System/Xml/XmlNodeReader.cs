using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml
{
	/// <summary>Represents a reader that provides fast, non-cached forward only access to XML data in an <see cref="T:System.Xml.XmlNode" />.</summary>
	// Token: 0x020000F7 RID: 247
	public class XmlNodeReader : XmlReader, IXmlNamespaceResolver
	{
		/// <summary>Creates an instance of the XmlNodeReader class using the specified <see cref="T:System.Xml.XmlNode" />.</summary>
		/// <param name="node">The XmlNode you want to read. </param>
		// Token: 0x06000CF5 RID: 3317 RVA: 0x00042210 File Offset: 0x00040410
		public XmlNodeReader(XmlNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.readerNav = new XmlNodeReaderNavigator(node);
			this.curDepth = 0;
			this.readState = ReadState.Initial;
			this.fEOF = false;
			this.nodeType = XmlNodeType.None;
			this.bResolveEntity = false;
			this.bStartFromDocument = false;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00042267 File Offset: 0x00040467
		internal bool IsInReadingStates()
		{
			return this.readState == ReadState.Interactive;
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlNodeType" /> values representing the type of the current node.</returns>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00042272 File Offset: 0x00040472
		public override XmlNodeType NodeType
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return XmlNodeType.None;
				}
				return this.nodeType;
			}
		}

		/// <summary>Gets the qualified name of the current node.</summary>
		/// <returns>The qualified name of the current node. For example, Name is bk:book for the element &lt;bk:book&gt;.The name returned is dependent on the <see cref="P:System.Xml.XmlNodeReader.NodeType" /> of the node. The following node types return the listed values. All other node types return an empty string.Node Type Name AttributeThe name of the attribute. DocumentTypeThe document type name. ElementThe tag name. EntityReferenceThe name of the entity referenced. ProcessingInstructionThe target of the processing instruction. XmlDeclarationThe literal string xml. </returns>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00042284 File Offset: 0x00040484
		public override string Name
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.Name;
			}
		}

		/// <summary>Gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.For node types that do not have a name (like Text, Comment, and so on), this property returns String.Empty.</returns>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0004229F File Offset: 0x0004049F
		public override string LocalName
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.LocalName;
			}
		}

		/// <summary>Gets the namespace URI (as defined in the W3C Namespace specification) of the node on which the reader is positioned.</summary>
		/// <returns>The namespace URI of the current node; otherwise an empty string.</returns>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x000422BA File Offset: 0x000404BA
		public override string NamespaceURI
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.NamespaceURI;
			}
		}

		/// <summary>Gets the namespace prefix associated with the current node.</summary>
		/// <returns>The namespace prefix associated with the current node.</returns>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x000422D5 File Offset: 0x000404D5
		public override string Prefix
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.Prefix;
			}
		}

		/// <summary>Gets a value indicating whether the current node can have a <see cref="P:System.Xml.XmlNodeReader.Value" />.</summary>
		/// <returns>true if the node on which the reader is currently positioned can have a Value; otherwise, false.</returns>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x000422F0 File Offset: 0x000404F0
		public override bool HasValue
		{
			get
			{
				return this.IsInReadingStates() && this.readerNav.HasValue;
			}
		}

		/// <summary>Gets the text value of the current node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlNodeReader.NodeType" /> of the node. The following table lists node types that have a value to return. All other node types return String.Empty.Node Type Value AttributeThe value of the attribute. CDATAThe content of the CDATA section. CommentThe content of the comment. DocumentTypeThe internal subset. ProcessingInstructionThe entire content, excluding the target. SignificantWhitespaceThe white space between markup in a mixed content model. TextThe content of the text node. WhitespaceThe white space between markup. XmlDeclarationThe content of the declaration. </returns>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00042307 File Offset: 0x00040507
		public override string Value
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.Value;
			}
		}

		/// <summary>Gets the depth of the current node in the XML document.</summary>
		/// <returns>The depth of the current node in the XML document.</returns>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00042322 File Offset: 0x00040522
		public override int Depth
		{
			get
			{
				return this.curDepth;
			}
		}

		/// <summary>Gets the base URI of the current node.</summary>
		/// <returns>The base URI of the current node.</returns>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0004232A File Offset: 0x0004052A
		public override string BaseURI
		{
			get
			{
				return this.readerNav.BaseURI;
			}
		}

		/// <summary>Gets a value indicating whether this reader can parse and resolve entities.</summary>
		/// <returns>true if the reader can parse and resolve entities; otherwise, false. XmlNodeReader always returns true.</returns>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an empty element (for example, &lt;MyElement/&gt;).</summary>
		/// <returns>true if the current node is an element (<see cref="P:System.Xml.XmlNodeReader.NodeType" /> equals XmlNodeType.Element) and it ends with /&gt;; otherwise, false.</returns>
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00042337 File Offset: 0x00040537
		public override bool IsEmptyElement
		{
			get
			{
				return this.IsInReadingStates() && this.readerNav.IsEmptyElement;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an attribute that was generated from the default value defined in the document type definition (DTD) or schema.</summary>
		/// <returns>true if the current node is an attribute whose value was generated from the default value defined in the DTD or schema; false if the attribute value was explicitly set.</returns>
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0004234E File Offset: 0x0004054E
		public override bool IsDefault
		{
			get
			{
				return this.IsInReadingStates() && this.readerNav.IsDefault;
			}
		}

		/// <summary>Gets the current xml:space scope.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlSpace" /> values. If no xml:space scope exists, this property defaults to XmlSpace.None.</returns>
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00042365 File Offset: 0x00040565
		public override XmlSpace XmlSpace
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return XmlSpace.None;
				}
				return this.readerNav.XmlSpace;
			}
		}

		/// <summary>Gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope.</returns>
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0004237C File Offset: 0x0004057C
		public override string XmlLang
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.XmlLang;
			}
		}

		/// <summary>Gets the schema information that has been assigned to the current node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object containing the schema information for the current node.</returns>
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00042397 File Offset: 0x00040597
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return null;
				}
				return this.readerNav.SchemaInfo;
			}
		}

		/// <summary>Gets the number of attributes on the current node.</summary>
		/// <returns>The number of attributes on the current node. This number includes default attributes.</returns>
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x000423AE File Offset: 0x000405AE
		public override int AttributeCount
		{
			get
			{
				if (!this.IsInReadingStates() || this.nodeType == XmlNodeType.EndElement)
				{
					return 0;
				}
				return this.readerNav.AttributeCount;
			}
		}

		/// <summary>Gets the value of the attribute with the specified name.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x06000D07 RID: 3335 RVA: 0x000423CF File Offset: 0x000405CF
		public override string GetAttribute(string name)
		{
			if (!this.IsInReadingStates())
			{
				return null;
			}
			return this.readerNav.GetAttribute(name);
		}

		/// <summary>Gets the value of the attribute with the specified local name and namespace URI.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned.</returns>
		/// <param name="name">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000D08 RID: 3336 RVA: 0x000423E8 File Offset: 0x000405E8
		public override string GetAttribute(string name, string namespaceURI)
		{
			if (!this.IsInReadingStates())
			{
				return null;
			}
			string text = ((namespaceURI == null) ? string.Empty : namespaceURI);
			return this.readerNav.GetAttribute(name, text);
		}

		/// <summary>Gets the value of the attribute with the specified index.</summary>
		/// <returns>The value of the specified attribute.</returns>
		/// <param name="attributeIndex">The index of the attribute. The index is zero-based. (The first attribute has index 0.) </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlNodeReader.AttributeCount" />. </exception>
		// Token: 0x06000D09 RID: 3337 RVA: 0x00042418 File Offset: 0x00040618
		public override string GetAttribute(int attributeIndex)
		{
			if (!this.IsInReadingStates())
			{
				throw new ArgumentOutOfRangeException("attributeIndex");
			}
			return this.readerNav.GetAttribute(attributeIndex);
		}

		/// <summary>Moves to the attribute with the specified name.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the reader's position does not change.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x06000D0A RID: 3338 RVA: 0x0004243C File Offset: 0x0004063C
		public override bool MoveToAttribute(string name)
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
			if (this.readerNav.MoveToAttribute(name))
			{
				this.curDepth++;
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		/// <summary>Moves to the attribute with the specified local name and namespace URI.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the reader's position does not change.</returns>
		/// <param name="name">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000D0B RID: 3339 RVA: 0x000424BC File Offset: 0x000406BC
		public override bool MoveToAttribute(string name, string namespaceURI)
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
			string text = ((namespaceURI == null) ? string.Empty : namespaceURI);
			if (this.readerNav.MoveToAttribute(name, text))
			{
				this.curDepth++;
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		/// <summary>Moves to the attribute with the specified index.</summary>
		/// <param name="attributeIndex">The index of the attribute. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlReader.AttributeCount" />. </exception>
		// Token: 0x06000D0C RID: 3340 RVA: 0x00042548 File Offset: 0x00040748
		public override void MoveToAttribute(int attributeIndex)
		{
			if (!this.IsInReadingStates())
			{
				throw new ArgumentOutOfRangeException("attributeIndex");
			}
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
			try
			{
				if (this.AttributeCount <= 0)
				{
					throw new ArgumentOutOfRangeException("attributeIndex");
				}
				this.readerNav.MoveToAttribute(attributeIndex);
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
			}
			catch
			{
				this.readerNav.RollBackMove(ref this.curDepth);
				throw;
			}
			this.curDepth++;
			this.nodeType = this.readerNav.NodeType;
		}

		/// <summary>Moves to the first attribute.</summary>
		/// <returns>true if an attribute exists (the reader moves to the first attribute); otherwise, false (the position of the reader does not change).</returns>
		// Token: 0x06000D0D RID: 3341 RVA: 0x000425F4 File Offset: 0x000407F4
		public override bool MoveToFirstAttribute()
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
			if (this.AttributeCount > 0)
			{
				this.readerNav.MoveToAttribute(0);
				this.curDepth++;
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		/// <summary>Moves to the next attribute.</summary>
		/// <returns>true if there is a next attribute; false if there are no more attributes.</returns>
		// Token: 0x06000D0E RID: 3342 RVA: 0x00042678 File Offset: 0x00040878
		public override bool MoveToNextAttribute()
		{
			if (!this.IsInReadingStates() || this.nodeType == XmlNodeType.EndElement)
			{
				return false;
			}
			this.readerNav.LogMove(this.curDepth);
			this.readerNav.ResetToAttribute(ref this.curDepth);
			if (this.readerNav.MoveToNextAttribute(ref this.curDepth))
			{
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		/// <summary>Moves to the element that contains the current attribute node.</summary>
		/// <returns>true if the reader is positioned on an attribute (the reader moves to the element that owns the attribute); false if the reader is not positioned on an attribute (the position of the reader does not change).</returns>
		// Token: 0x06000D0F RID: 3343 RVA: 0x00042704 File Offset: 0x00040904
		public override bool MoveToElement()
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			this.readerNav.LogMove(this.curDepth);
			this.readerNav.ResetToAttribute(ref this.curDepth);
			if (this.readerNav.MoveToElement())
			{
				this.curDepth--;
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		/// <summary>Reads the next node from the stream.</summary>
		/// <returns>true if the next node was read successfully; false if there are no more nodes to read.</returns>
		// Token: 0x06000D10 RID: 3344 RVA: 0x0004278B File Offset: 0x0004098B
		public override bool Read()
		{
			return this.Read(false);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00042794 File Offset: 0x00040994
		private bool Read(bool fSkipChildren)
		{
			if (this.fEOF)
			{
				return false;
			}
			if (this.readState == ReadState.Initial)
			{
				if (this.readerNav.NodeType == XmlNodeType.Document || this.readerNav.NodeType == XmlNodeType.DocumentFragment)
				{
					this.bStartFromDocument = true;
					if (!this.ReadNextNode(fSkipChildren))
					{
						this.readState = ReadState.Error;
						return false;
					}
				}
				this.ReSetReadingMarks();
				this.readState = ReadState.Interactive;
				this.nodeType = this.readerNav.NodeType;
				this.curDepth = 0;
				return true;
			}
			if (this.bInReadBinary)
			{
				this.FinishReadBinary();
			}
			if (this.readerNav.CreatedOnAttribute)
			{
				return false;
			}
			this.ReSetReadingMarks();
			if (this.ReadNextNode(fSkipChildren))
			{
				return true;
			}
			if (this.readState == ReadState.Initial || this.readState == ReadState.Interactive)
			{
				this.readState = ReadState.Error;
			}
			if (this.readState == ReadState.EndOfFile)
			{
				this.nodeType = XmlNodeType.None;
			}
			return false;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00042868 File Offset: 0x00040A68
		private bool ReadNextNode(bool fSkipChildren)
		{
			if (this.readState != ReadState.Interactive && this.readState != ReadState.Initial)
			{
				this.nodeType = XmlNodeType.None;
				return false;
			}
			bool flag = !fSkipChildren;
			XmlNodeType xmlNodeType = this.readerNav.NodeType;
			if (flag && this.nodeType != XmlNodeType.EndElement && this.nodeType != XmlNodeType.EndEntity && (xmlNodeType == XmlNodeType.Element || (xmlNodeType == XmlNodeType.EntityReference && this.bResolveEntity) || ((this.readerNav.NodeType == XmlNodeType.Document || this.readerNav.NodeType == XmlNodeType.DocumentFragment) && this.readState == ReadState.Initial)))
			{
				if (this.readerNav.MoveToFirstChild())
				{
					this.nodeType = this.readerNav.NodeType;
					this.curDepth++;
					if (this.bResolveEntity)
					{
						this.bResolveEntity = false;
					}
					return true;
				}
				if (this.readerNav.NodeType == XmlNodeType.Element && !this.readerNav.IsEmptyElement)
				{
					this.nodeType = XmlNodeType.EndElement;
					return true;
				}
				if (this.readerNav.NodeType == XmlNodeType.EntityReference && this.bResolveEntity)
				{
					this.bResolveEntity = false;
					this.nodeType = XmlNodeType.EndEntity;
					return true;
				}
				return this.ReadForward(fSkipChildren);
			}
			else
			{
				if (this.readerNav.NodeType == XmlNodeType.EntityReference && this.bResolveEntity)
				{
					if (this.readerNav.MoveToFirstChild())
					{
						this.nodeType = this.readerNav.NodeType;
						this.curDepth++;
					}
					else
					{
						this.nodeType = XmlNodeType.EndEntity;
					}
					this.bResolveEntity = false;
					return true;
				}
				return this.ReadForward(fSkipChildren);
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x000429E7 File Offset: 0x00040BE7
		private void SetEndOfFile()
		{
			this.fEOF = true;
			this.readState = ReadState.EndOfFile;
			this.nodeType = XmlNodeType.None;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x000429FE File Offset: 0x00040BFE
		private bool ReadAtZeroLevel(bool fSkipChildren)
		{
			if (!fSkipChildren && this.nodeType != XmlNodeType.EndElement && this.readerNav.NodeType == XmlNodeType.Element && !this.readerNav.IsEmptyElement)
			{
				this.nodeType = XmlNodeType.EndElement;
				return true;
			}
			this.SetEndOfFile();
			return false;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00042A3C File Offset: 0x00040C3C
		private bool ReadForward(bool fSkipChildren)
		{
			if (this.readState == ReadState.Error)
			{
				return false;
			}
			if (!this.bStartFromDocument && this.curDepth == 0)
			{
				return this.ReadAtZeroLevel(fSkipChildren);
			}
			if (this.readerNav.MoveToNext())
			{
				this.nodeType = this.readerNav.NodeType;
				return true;
			}
			if (this.curDepth == 0)
			{
				return this.ReadAtZeroLevel(fSkipChildren);
			}
			if (!this.readerNav.MoveToParent())
			{
				return false;
			}
			if (this.readerNav.NodeType == XmlNodeType.Element)
			{
				this.curDepth--;
				this.nodeType = XmlNodeType.EndElement;
				return true;
			}
			if (this.readerNav.NodeType == XmlNodeType.EntityReference)
			{
				this.curDepth--;
				this.nodeType = XmlNodeType.EndEntity;
				return true;
			}
			return true;
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00042AF8 File Offset: 0x00040CF8
		private void ReSetReadingMarks()
		{
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
		}

		/// <summary>Gets a value indicating whether the reader is positioned at the end of the stream.</summary>
		/// <returns>true if the reader is positioned at the end of the stream; otherwise, false.</returns>
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00042B11 File Offset: 0x00040D11
		public override bool EOF
		{
			get
			{
				return this.readState != ReadState.Closed && this.fEOF;
			}
		}

		/// <summary>Changes the <see cref="P:System.Xml.XmlNodeReader.ReadState" /> to Closed.</summary>
		// Token: 0x06000D18 RID: 3352 RVA: 0x00042B24 File Offset: 0x00040D24
		public override void Close()
		{
			this.readState = ReadState.Closed;
		}

		/// <summary>Gets the state of the reader.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ReadState" /> values.</returns>
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00042B2D File Offset: 0x00040D2D
		public override ReadState ReadState
		{
			get
			{
				return this.readState;
			}
		}

		/// <summary>Skips the children of the current node.</summary>
		// Token: 0x06000D1A RID: 3354 RVA: 0x00042B35 File Offset: 0x00040D35
		public override void Skip()
		{
			this.Read(true);
		}

		/// <summary>Reads the contents of an element or text node as a string.</summary>
		/// <returns>The contents of the element or text-like node (This can include CDATA, Text nodes, and so on). This can be an empty string if the reader is positioned on something other than an element or text node, or if there is no more text content to return in the current context.Note: The text node can be either an element or an attribute text node.</returns>
		// Token: 0x06000D1B RID: 3355 RVA: 0x00042B3F File Offset: 0x00040D3F
		public override string ReadString()
		{
			if (this.NodeType == XmlNodeType.EntityReference && this.bResolveEntity && !this.Read())
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
			return base.ReadString();
		}

		/// <summary>Gets a value indicating whether the current node has any attributes.</summary>
		/// <returns>true if the current node has attributes; otherwise, false.</returns>
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00016653 File Offset: 0x00014853
		public override bool HasAttributes
		{
			get
			{
				return this.AttributeCount > 0;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this implementation.</summary>
		/// <returns>The XmlNameTable enabling you to get the atomized version of a string within the node.</returns>
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x00042B70 File Offset: 0x00040D70
		public override XmlNameTable NameTable
		{
			get
			{
				return this.readerNav.NameTable;
			}
		}

		/// <summary>Resolves a namespace prefix in the current element's scope.</summary>
		/// <returns>The namespace URI to which the prefix maps or null if no matching prefix is found.</returns>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass an empty string. This string does not have to be atomized. </param>
		// Token: 0x06000D1E RID: 3358 RVA: 0x00042B80 File Offset: 0x00040D80
		public override string LookupNamespace(string prefix)
		{
			if (!this.IsInReadingStates())
			{
				return null;
			}
			string text = this.readerNav.LookupNamespace(prefix);
			if (text != null && text.Length == 0)
			{
				return null;
			}
			return text;
		}

		/// <summary>Resolves the entity reference for EntityReference nodes.</summary>
		/// <exception cref="T:System.InvalidOperationException">The reader is not positioned on an EntityReference node. </exception>
		// Token: 0x06000D1F RID: 3359 RVA: 0x00042BB2 File Offset: 0x00040DB2
		public override void ResolveEntity()
		{
			if (!this.IsInReadingStates() || this.nodeType != XmlNodeType.EntityReference)
			{
				throw new InvalidOperationException(Res.GetString("The node is not an expandable 'EntityReference' node."));
			}
			this.bResolveEntity = true;
		}

		/// <summary>Parses the attribute value into one or more Text, EntityReference, or EndEntity nodes.</summary>
		/// <returns>true if there are nodes to return.false if the reader is not positioned on an attribute node when the initial call is made or if all the attribute values have been read.An empty attribute, such as, misc="", returns true with a single node with a value of String.Empty.</returns>
		// Token: 0x06000D20 RID: 3360 RVA: 0x00042BDC File Offset: 0x00040DDC
		public override bool ReadAttributeValue()
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			if (this.readerNav.ReadAttributeValue(ref this.curDepth, ref this.bResolveEntity, ref this.nodeType))
			{
				this.bInReadBinary = false;
				return true;
			}
			return false;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlNodeReader" /> implements the binary content read methods.</summary>
		/// <returns>true if the binary content read methods are implemented; otherwise false. The <see cref="T:System.Xml.XmlNodeReader" /> class always returns true.</returns>
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		/// <summary>Reads the content and returns the Base64 decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Xml.XmlNodeReader.ReadContentAsBase64(System.Byte[],System.Int32,System.Int32)" /> is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		// Token: 0x06000D22 RID: 3362 RVA: 0x00042C14 File Offset: 0x00040E14
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.readState != ReadState.Interactive)
			{
				return 0;
			}
			if (!this.bInReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			this.bInReadBinary = false;
			int num = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.bInReadBinary = true;
			return num;
		}

		/// <summary>Reads the content and returns the BinHex decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Xml.XmlNodeReader.ReadContentAsBinHex(System.Byte[],System.Int32,System.Int32)" />  is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		// Token: 0x06000D23 RID: 3363 RVA: 0x00042C64 File Offset: 0x00040E64
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.readState != ReadState.Interactive)
			{
				return 0;
			}
			if (!this.bInReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			this.bInReadBinary = false;
			int num = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.bInReadBinary = true;
			return num;
		}

		/// <summary>Reads the element and decodes the Base64 content.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06000D24 RID: 3364 RVA: 0x00042CB4 File Offset: 0x00040EB4
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.readState != ReadState.Interactive)
			{
				return 0;
			}
			if (!this.bInReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			this.bInReadBinary = false;
			int num = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.bInReadBinary = true;
			return num;
		}

		/// <summary>Reads the element and decodes the BinHex content.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06000D25 RID: 3365 RVA: 0x00042D04 File Offset: 0x00040F04
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.readState != ReadState.Interactive)
			{
				return 0;
			}
			if (!this.bInReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			this.bInReadBinary = false;
			int num = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.bInReadBinary = true;
			return num;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00042D52 File Offset: 0x00040F52
		private void FinishReadBinary()
		{
			this.bInReadBinary = false;
			this.readBinaryHelper.Finish();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.GetNamespacesInScope(System.Xml.XmlNamespaceScope)" />.</summary>
		/// <returns>
		///   <see cref="T:System.Collections.IDictionary" /> object that contains the namespaces that are in scope.</returns>
		/// <param name="scope">
		///   <see cref="T:System.Xml.XmlNamespaceScope" /> object.</param>
		// Token: 0x06000D27 RID: 3367 RVA: 0x00042D66 File Offset: 0x00040F66
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerNav.GetNamespacesInScope(scope);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupPrefix(System.String)" />.</summary>
		/// <returns>
		///   <see cref="T:System.String" /> object that contains the namespace prefix.</returns>
		/// <param name="namespaceName">
		///   <see cref="T:System.String" /> object that identifies the namespace.</param>
		// Token: 0x06000D28 RID: 3368 RVA: 0x00042D74 File Offset: 0x00040F74
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerNav.LookupPrefix(namespaceName);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupNamespace(System.String)" />.</summary>
		/// <returns>
		///   <see cref="T:System.String" /> that contains the namespace name.</returns>
		/// <param name="prefix">
		///   <see cref="T:System.String" /> that contains the namespace prefix.</param>
		// Token: 0x06000D29 RID: 3369 RVA: 0x00042D84 File Offset: 0x00040F84
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			if (!this.IsInReadingStates())
			{
				return this.readerNav.DefaultLookupNamespace(prefix);
			}
			string text = this.readerNav.LookupNamespace(prefix);
			if (text != null)
			{
				text = this.readerNav.NameTable.Add(text);
			}
			return text;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00042DC9 File Offset: 0x00040FC9
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.readerNav.Document.DtdSchemaInfo;
			}
		}

		// Token: 0x04000676 RID: 1654
		private XmlNodeReaderNavigator readerNav;

		// Token: 0x04000677 RID: 1655
		private XmlNodeType nodeType;

		// Token: 0x04000678 RID: 1656
		private int curDepth;

		// Token: 0x04000679 RID: 1657
		private ReadState readState;

		// Token: 0x0400067A RID: 1658
		private bool fEOF;

		// Token: 0x0400067B RID: 1659
		private bool bResolveEntity;

		// Token: 0x0400067C RID: 1660
		private bool bStartFromDocument;

		// Token: 0x0400067D RID: 1661
		private bool bInReadBinary;

		// Token: 0x0400067E RID: 1662
		private ReadContentAsBinaryHelper readBinaryHelper;
	}
}
