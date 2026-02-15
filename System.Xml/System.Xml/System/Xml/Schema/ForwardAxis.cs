using System;

namespace System.Xml.Schema
{
	// Token: 0x0200020A RID: 522
	internal class ForwardAxis
	{
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x00098526 File Offset: 0x00096726
		internal DoubleLinkAxis RootNode
		{
			get
			{
				return this._rootNode;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x0009852E File Offset: 0x0009672E
		internal DoubleLinkAxis TopNode
		{
			get
			{
				return this._topNode;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x00098536 File Offset: 0x00096736
		internal bool IsAttribute
		{
			get
			{
				return this._isAttribute;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x0009853E File Offset: 0x0009673E
		internal bool IsDss
		{
			get
			{
				return this._isDss;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x00098546 File Offset: 0x00096746
		internal bool IsSelfAxis
		{
			get
			{
				return this._isSelfAxis;
			}
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00098550 File Offset: 0x00096750
		public ForwardAxis(DoubleLinkAxis axis, bool isdesorself)
		{
			this._isDss = isdesorself;
			this._isAttribute = Asttree.IsAttribute(axis);
			this._topNode = axis;
			this._rootNode = axis;
			while (this._rootNode.Input != null)
			{
				this._rootNode = (DoubleLinkAxis)this._rootNode.Input;
			}
			this._isSelfAxis = Asttree.IsSelf(this._topNode);
		}

		// Token: 0x04000B12 RID: 2834
		private DoubleLinkAxis _topNode;

		// Token: 0x04000B13 RID: 2835
		private DoubleLinkAxis _rootNode;

		// Token: 0x04000B14 RID: 2836
		private bool _isAttribute;

		// Token: 0x04000B15 RID: 2837
		private bool _isDss;

		// Token: 0x04000B16 RID: 2838
		private bool _isSelfAxis;
	}
}
