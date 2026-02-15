using System;
using MS.Internal.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x02000209 RID: 521
	internal class DoubleLinkAxis : Axis
	{
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x000984A0 File Offset: 0x000966A0
		// (set) Token: 0x060019FF RID: 6655 RVA: 0x000984A8 File Offset: 0x000966A8
		internal Axis Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x000984B4 File Offset: 0x000966B4
		internal DoubleLinkAxis(Axis axis, DoubleLinkAxis inputaxis)
			: base(axis.TypeOfAxis, inputaxis, axis.Prefix, axis.Name, axis.NodeType)
		{
			this.next = null;
			base.Urn = axis.Urn;
			this.abbrAxis = axis.AbbrAxis;
			if (inputaxis != null)
			{
				inputaxis.Next = this;
			}
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00098509 File Offset: 0x00096709
		internal static DoubleLinkAxis ConvertTree(Axis axis)
		{
			if (axis == null)
			{
				return null;
			}
			return new DoubleLinkAxis(axis, DoubleLinkAxis.ConvertTree((Axis)axis.Input));
		}

		// Token: 0x04000B11 RID: 2833
		internal Axis next;
	}
}
