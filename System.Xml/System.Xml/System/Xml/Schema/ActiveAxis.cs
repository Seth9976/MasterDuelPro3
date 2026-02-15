using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000208 RID: 520
	internal class ActiveAxis
	{
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x000982AE File Offset: 0x000964AE
		public int CurrentDepth
		{
			get
			{
				return this._currentDepth;
			}
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x000982B6 File Offset: 0x000964B6
		internal void Reactivate()
		{
			this._isActive = true;
			this._currentDepth = -1;
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x000982C8 File Offset: 0x000964C8
		internal ActiveAxis(Asttree axisTree)
		{
			this._axisTree = axisTree;
			this._currentDepth = -1;
			this._axisStack = new ArrayList(axisTree.SubtreeArray.Count);
			for (int i = 0; i < axisTree.SubtreeArray.Count; i++)
			{
				AxisStack axisStack = new AxisStack((ForwardAxis)axisTree.SubtreeArray[i], this);
				this._axisStack.Add(axisStack);
			}
			this._isActive = true;
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00098344 File Offset: 0x00096544
		public bool MoveToStartElement(string localname, string URN)
		{
			if (!this._isActive)
			{
				return false;
			}
			this._currentDepth++;
			bool flag = false;
			for (int i = 0; i < this._axisStack.Count; i++)
			{
				AxisStack axisStack = (AxisStack)this._axisStack[i];
				if (axisStack.Subtree.IsSelfAxis)
				{
					if (axisStack.Subtree.IsDss || this.CurrentDepth == 0)
					{
						flag = true;
					}
				}
				else if (this.CurrentDepth != 0 && axisStack.MoveToChild(localname, URN, this._currentDepth))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x000983D4 File Offset: 0x000965D4
		public virtual bool EndElement(string localname, string URN)
		{
			if (this._currentDepth == 0)
			{
				this._isActive = false;
				this._currentDepth--;
			}
			if (!this._isActive)
			{
				return false;
			}
			for (int i = 0; i < this._axisStack.Count; i++)
			{
				((AxisStack)this._axisStack[i]).MoveToParent(localname, URN, this._currentDepth);
			}
			this._currentDepth--;
			return false;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0009844C File Offset: 0x0009664C
		public bool MoveToAttribute(string localname, string URN)
		{
			if (!this._isActive)
			{
				return false;
			}
			bool flag = false;
			for (int i = 0; i < this._axisStack.Count; i++)
			{
				if (((AxisStack)this._axisStack[i]).MoveToAttribute(localname, URN, this._currentDepth + 1))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x04000B0D RID: 2829
		private int _currentDepth;

		// Token: 0x04000B0E RID: 2830
		private bool _isActive;

		// Token: 0x04000B0F RID: 2831
		private Asttree _axisTree;

		// Token: 0x04000B10 RID: 2832
		private ArrayList _axisStack;
	}
}
