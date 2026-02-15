using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020000DD RID: 221
	internal sealed class XmlChildEnumerator : IEnumerator
	{
		// Token: 0x06000B12 RID: 2834 RVA: 0x0003B249 File Offset: 0x00039449
		internal XmlChildEnumerator(XmlNode container)
		{
			this.container = container;
			this.child = container.FirstChild;
			this.isFirst = true;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0003B26B File Offset: 0x0003946B
		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0003B274 File Offset: 0x00039474
		internal bool MoveNext()
		{
			if (this.isFirst)
			{
				this.child = this.container.FirstChild;
				this.isFirst = false;
			}
			else if (this.child != null)
			{
				this.child = this.child.NextSibling;
			}
			return this.child != null;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0003B2C5 File Offset: 0x000394C5
		void IEnumerator.Reset()
		{
			this.isFirst = true;
			this.child = this.container.FirstChild;
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0003B2DF File Offset: 0x000394DF
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0003B2E7 File Offset: 0x000394E7
		internal XmlNode Current
		{
			get
			{
				if (this.isFirst || this.child == null)
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
				return this.child;
			}
		}

		// Token: 0x040005F9 RID: 1529
		internal XmlNode container;

		// Token: 0x040005FA RID: 1530
		internal XmlNode child;

		// Token: 0x040005FB RID: 1531
		internal bool isFirst;
	}
}
