using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000207 RID: 519
	internal class AxisStack
	{
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x0009801D File Offset: 0x0009621D
		internal ForwardAxis Subtree
		{
			get
			{
				return this._subtree;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x00098025 File Offset: 0x00096225
		internal int Length
		{
			get
			{
				return this._stack.Count;
			}
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00098032 File Offset: 0x00096232
		public AxisStack(ForwardAxis faxis, ActiveAxis parent)
		{
			this._subtree = faxis;
			this._stack = new ArrayList();
			this._parent = parent;
			if (!faxis.IsDss)
			{
				this.Push(1);
			}
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00098064 File Offset: 0x00096264
		internal void Push(int depth)
		{
			AxisElement axisElement = new AxisElement(this._subtree.RootNode, depth);
			this._stack.Add(axisElement);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00098090 File Offset: 0x00096290
		internal void Pop()
		{
			this._stack.RemoveAt(this.Length - 1);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000980A5 File Offset: 0x000962A5
		internal static bool Equal(string thisname, string thisURN, string name, string URN)
		{
			if (thisURN == null)
			{
				if (URN != null && URN.Length != 0)
				{
					return false;
				}
			}
			else if (thisURN.Length != 0 && thisURN != URN)
			{
				return false;
			}
			return thisname.Length == 0 || !(thisname != name);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000980E0 File Offset: 0x000962E0
		internal void MoveToParent(string name, string URN, int depth)
		{
			if (this._subtree.IsSelfAxis)
			{
				return;
			}
			for (int i = 0; i < this._stack.Count; i++)
			{
				((AxisElement)this._stack[i]).MoveToParent(depth, this._subtree);
			}
			if (this._subtree.IsDss && AxisStack.Equal(this._subtree.RootNode.Name, this._subtree.RootNode.Urn, name, URN))
			{
				this.Pop();
			}
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0009816C File Offset: 0x0009636C
		internal bool MoveToChild(string name, string URN, int depth)
		{
			bool flag = false;
			if (this._subtree.IsDss && AxisStack.Equal(this._subtree.RootNode.Name, this._subtree.RootNode.Urn, name, URN))
			{
				this.Push(-1);
			}
			for (int i = 0; i < this._stack.Count; i++)
			{
				if (((AxisElement)this._stack[i]).MoveToChild(name, URN, depth, this._subtree))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x000981F4 File Offset: 0x000963F4
		internal bool MoveToAttribute(string name, string URN, int depth)
		{
			if (!this._subtree.IsAttribute)
			{
				return false;
			}
			if (!AxisStack.Equal(this._subtree.TopNode.Name, this._subtree.TopNode.Urn, name, URN))
			{
				return false;
			}
			bool flag = false;
			if (this._subtree.TopNode.Input == null)
			{
				return this._subtree.IsDss || depth == 1;
			}
			for (int i = 0; i < this._stack.Count; i++)
			{
				AxisElement axisElement = (AxisElement)this._stack[i];
				if (axisElement.isMatch && axisElement.CurNode == this._subtree.TopNode.Input)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x04000B0A RID: 2826
		private ArrayList _stack;

		// Token: 0x04000B0B RID: 2827
		private ForwardAxis _subtree;

		// Token: 0x04000B0C RID: 2828
		private ActiveAxis _parent;
	}
}
