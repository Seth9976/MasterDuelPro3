using System;
using System.Collections;
using System.Diagnostics;

namespace System.Xml.XPath
{
	/// <summary>Provides an iterator over a selected set of nodes.</summary>
	// Token: 0x0200013F RID: 319
	[DebuggerDisplay("Position={CurrentPosition}, Current={debuggerDisplayProxy}")]
	public abstract class XPathNodeIterator : ICloneable, IEnumerable
	{
		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x06000FC3 RID: 4035 RVA: 0x0004DD8E File Offset: 0x0004BF8E
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>When overridden in a derived class, returns a clone of this <see cref="T:System.Xml.XPath.XPathNodeIterator" /> object.</summary>
		/// <returns>A new <see cref="T:System.Xml.XPath.XPathNodeIterator" /> object clone of this <see cref="T:System.Xml.XPath.XPathNodeIterator" /> object.</returns>
		// Token: 0x06000FC4 RID: 4036
		public abstract XPathNodeIterator Clone();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> object returned by the <see cref="P:System.Xml.XPath.XPathNodeIterator.Current" /> property to the next node in the selected node set.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> object moved to the next node; false if there are no more selected nodes.</returns>
		// Token: 0x06000FC5 RID: 4037
		public abstract bool MoveNext();

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.XPath.XPathNavigator" /> object for this <see cref="T:System.Xml.XPath.XPathNodeIterator" />, positioned on the current context node.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the context node from which the node set was selected. The <see cref="M:System.Xml.XPath.XPathNodeIterator.MoveNext" /> method must be called to move the <see cref="T:System.Xml.XPath.XPathNodeIterator" /> to the first node in the selected set.</returns>
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000FC6 RID: 4038
		public abstract XPathNavigator Current { get; }

		/// <summary>When overridden in a derived class, gets the index of the current position in the selected set of nodes.</summary>
		/// <returns>The index of the current position.</returns>
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000FC7 RID: 4039
		public abstract int CurrentPosition { get; }

		/// <summary>Gets the index of the last node in the selected set of nodes.</summary>
		/// <returns>The index of the last node in the selected set of nodes, or 0 if there are no selected nodes.</returns>
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x0004DD98 File Offset: 0x0004BF98
		public virtual int Count
		{
			get
			{
				if (this.count == -1)
				{
					XPathNodeIterator xpathNodeIterator = this.Clone();
					while (xpathNodeIterator.MoveNext())
					{
					}
					this.count = xpathNodeIterator.CurrentPosition;
				}
				return this.count;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> object to iterate through the selected node set.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object to iterate through the selected node set.</returns>
		// Token: 0x06000FC9 RID: 4041 RVA: 0x0004DDCF File Offset: 0x0004BFCF
		public virtual IEnumerator GetEnumerator()
		{
			return new XPathNodeIterator.Enumerator(this);
		}

		// Token: 0x040007A3 RID: 1955
		internal int count = -1;

		// Token: 0x02000140 RID: 320
		private class Enumerator : IEnumerator
		{
			// Token: 0x06000FCB RID: 4043 RVA: 0x0004DDE6 File Offset: 0x0004BFE6
			public Enumerator(XPathNodeIterator original)
			{
				this.original = original.Clone();
			}

			// Token: 0x1700039C RID: 924
			// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0004DDFC File Offset: 0x0004BFFC
			public virtual object Current
			{
				get
				{
					if (!this.iterationStarted)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[] { string.Empty }));
					}
					if (this.current == null)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[] { string.Empty }));
					}
					return this.current.Current.Clone();
				}
			}

			// Token: 0x06000FCD RID: 4045 RVA: 0x0004DE68 File Offset: 0x0004C068
			public virtual bool MoveNext()
			{
				if (!this.iterationStarted)
				{
					this.current = this.original.Clone();
					this.iterationStarted = true;
				}
				if (this.current == null || !this.current.MoveNext())
				{
					this.current = null;
					return false;
				}
				return true;
			}

			// Token: 0x06000FCE RID: 4046 RVA: 0x0004DEB4 File Offset: 0x0004C0B4
			public virtual void Reset()
			{
				this.iterationStarted = false;
			}

			// Token: 0x040007A4 RID: 1956
			private XPathNodeIterator original;

			// Token: 0x040007A5 RID: 1957
			private XPathNodeIterator current;

			// Token: 0x040007A6 RID: 1958
			private bool iterationStarted;
		}
	}
}
