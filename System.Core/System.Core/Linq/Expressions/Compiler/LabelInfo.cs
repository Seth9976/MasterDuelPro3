using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection.Emit;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000F1 RID: 241
	internal sealed class LabelInfo
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0001ACC9 File Offset: 0x00018EC9
		internal Label Label
		{
			get
			{
				this.EnsureLabelAndValue();
				return this._label;
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001ACD7 File Offset: 0x00018ED7
		internal LabelInfo(ILGenerator il, LabelTarget node, bool canReturn)
		{
			this._ilg = il;
			this._node = node;
			this._canReturn = canReturn;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0001AD15 File Offset: 0x00018F15
		internal bool CanReturn
		{
			get
			{
				return this._canReturn;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0001AD1D File Offset: 0x00018F1D
		internal bool CanBranch
		{
			get
			{
				return this._opCode != OpCodes.Leave;
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001AD2F File Offset: 0x00018F2F
		internal void Reference(LabelScopeInfo block)
		{
			this._references.Add(block);
			if (this._definitions.Count > 0)
			{
				this.ValidateJump(block);
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001AD54 File Offset: 0x00018F54
		internal void Define(LabelScopeInfo block)
		{
			for (LabelScopeInfo labelScopeInfo = block; labelScopeInfo != null; labelScopeInfo = labelScopeInfo.Parent)
			{
				if (labelScopeInfo.ContainsTarget(this._node))
				{
					throw Error.LabelTargetAlreadyDefined(this._node.Name);
				}
			}
			this._definitions.Add(block);
			block.AddLabelInfo(this._node, this);
			if (this._definitions.Count == 1)
			{
				using (List<LabelScopeInfo>.Enumerator enumerator = this._references.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						LabelScopeInfo labelScopeInfo2 = enumerator.Current;
						this.ValidateJump(labelScopeInfo2);
					}
					return;
				}
			}
			if (this._acrossBlockJump)
			{
				throw Error.AmbiguousJump(this._node.Name);
			}
			this._labelDefined = false;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001AE1C File Offset: 0x0001901C
		private void ValidateJump(LabelScopeInfo reference)
		{
			this._opCode = (this._canReturn ? OpCodes.Ret : OpCodes.Br);
			for (LabelScopeInfo labelScopeInfo = reference; labelScopeInfo != null; labelScopeInfo = labelScopeInfo.Parent)
			{
				if (this._definitions.Contains(labelScopeInfo))
				{
					return;
				}
				if (labelScopeInfo.Kind == LabelScopeKind.Finally || labelScopeInfo.Kind == LabelScopeKind.Filter)
				{
					break;
				}
				if (labelScopeInfo.Kind == LabelScopeKind.Try || labelScopeInfo.Kind == LabelScopeKind.Catch)
				{
					this._opCode = OpCodes.Leave;
				}
			}
			this._acrossBlockJump = true;
			if (this._node != null && this._node.Type != typeof(void))
			{
				throw Error.NonLocalJumpWithValue(this._node.Name);
			}
			if (this._definitions.Count > 1)
			{
				throw Error.AmbiguousJump(this._node.Name);
			}
			LabelScopeInfo labelScopeInfo2 = this._definitions.First<LabelScopeInfo>();
			LabelScopeInfo labelScopeInfo3 = Helpers.CommonNode<LabelScopeInfo>(labelScopeInfo2, reference, (LabelScopeInfo b) => b.Parent);
			this._opCode = (this._canReturn ? OpCodes.Ret : OpCodes.Br);
			for (LabelScopeInfo labelScopeInfo4 = reference; labelScopeInfo4 != labelScopeInfo3; labelScopeInfo4 = labelScopeInfo4.Parent)
			{
				if (labelScopeInfo4.Kind == LabelScopeKind.Finally)
				{
					throw Error.ControlCannotLeaveFinally();
				}
				if (labelScopeInfo4.Kind == LabelScopeKind.Filter)
				{
					throw Error.ControlCannotLeaveFilterTest();
				}
				if (labelScopeInfo4.Kind == LabelScopeKind.Try || labelScopeInfo4.Kind == LabelScopeKind.Catch)
				{
					this._opCode = OpCodes.Leave;
				}
			}
			LabelScopeInfo labelScopeInfo5 = labelScopeInfo2;
			while (labelScopeInfo5 != labelScopeInfo3)
			{
				if (!labelScopeInfo5.CanJumpInto)
				{
					if (labelScopeInfo5.Kind == LabelScopeKind.Expression)
					{
						throw Error.ControlCannotEnterExpression();
					}
					throw Error.ControlCannotEnterTry();
				}
				else
				{
					labelScopeInfo5 = labelScopeInfo5.Parent;
				}
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001AFB3 File Offset: 0x000191B3
		internal void ValidateFinish()
		{
			if (this._references.Count > 0 && this._definitions.Count == 0)
			{
				throw Error.LabelTargetUndefined(this._node.Name);
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001AFE4 File Offset: 0x000191E4
		internal void EmitJump()
		{
			if (this._opCode == OpCodes.Ret)
			{
				this._ilg.Emit(OpCodes.Ret);
				return;
			}
			this.StoreValue();
			this._ilg.Emit(this._opCode, this.Label);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001B031 File Offset: 0x00019231
		private void StoreValue()
		{
			this.EnsureLabelAndValue();
			if (this._value != null)
			{
				this._ilg.Emit(OpCodes.Stloc, this._value);
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001B057 File Offset: 0x00019257
		internal void Mark()
		{
			if (this._canReturn)
			{
				if (!this._labelDefined)
				{
					return;
				}
				this._ilg.Emit(OpCodes.Ret);
			}
			else
			{
				this.StoreValue();
			}
			this.MarkWithEmptyStack();
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001B088 File Offset: 0x00019288
		internal void MarkWithEmptyStack()
		{
			this._ilg.MarkLabel(this.Label);
			if (this._value != null)
			{
				this._ilg.Emit(OpCodes.Ldloc, this._value);
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001B0BC File Offset: 0x000192BC
		private void EnsureLabelAndValue()
		{
			if (!this._labelDefined)
			{
				this._labelDefined = true;
				this._label = this._ilg.DefineLabel();
				if (this._node != null && this._node.Type != typeof(void))
				{
					this._value = this._ilg.DeclareLocal(this._node.Type);
				}
			}
		}

		// Token: 0x04000268 RID: 616
		private readonly LabelTarget _node;

		// Token: 0x04000269 RID: 617
		private Label _label;

		// Token: 0x0400026A RID: 618
		private bool _labelDefined;

		// Token: 0x0400026B RID: 619
		private LocalBuilder _value;

		// Token: 0x0400026C RID: 620
		private readonly HashSet<LabelScopeInfo> _definitions = new HashSet<LabelScopeInfo>();

		// Token: 0x0400026D RID: 621
		private readonly List<LabelScopeInfo> _references = new List<LabelScopeInfo>();

		// Token: 0x0400026E RID: 622
		private readonly bool _canReturn;

		// Token: 0x0400026F RID: 623
		private bool _acrossBlockJump;

		// Token: 0x04000270 RID: 624
		private OpCode _opCode = OpCodes.Leave;

		// Token: 0x04000271 RID: 625
		private readonly ILGenerator _ilg;
	}
}
