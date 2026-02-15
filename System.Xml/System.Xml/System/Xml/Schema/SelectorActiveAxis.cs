using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000215 RID: 533
	internal class SelectorActiveAxis : ActiveAxis
	{
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00099BA3 File Offset: 0x00097DA3
		public int lastDepth
		{
			get
			{
				if (this.KSpointer != 0)
				{
					return ((KSStruct)this.KSs[this.KSpointer - 1]).depth;
				}
				return -1;
			}
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00099BCC File Offset: 0x00097DCC
		public SelectorActiveAxis(Asttree axisTree, ConstraintStruct cs)
			: base(axisTree)
		{
			this.KSs = new ArrayList();
			this.cs = cs;
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00099BE7 File Offset: 0x00097DE7
		public override bool EndElement(string localname, string URN)
		{
			base.EndElement(localname, URN);
			return this.KSpointer > 0 && base.CurrentDepth == this.lastDepth;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00099C0C File Offset: 0x00097E0C
		public int PushKS(int errline, int errcol)
		{
			KeySequence keySequence = new KeySequence(this.cs.TableDim, errline, errcol);
			KSStruct ksstruct;
			if (this.KSpointer < this.KSs.Count)
			{
				ksstruct = (KSStruct)this.KSs[this.KSpointer];
				ksstruct.ks = keySequence;
				for (int i = 0; i < this.cs.TableDim; i++)
				{
					ksstruct.fields[i].Reactivate(keySequence);
				}
			}
			else
			{
				ksstruct = new KSStruct(keySequence, this.cs.TableDim);
				for (int j = 0; j < this.cs.TableDim; j++)
				{
					ksstruct.fields[j] = new LocatedActiveAxis(this.cs.constraint.Fields[j], keySequence, j);
					this.cs.axisFields.Add(ksstruct.fields[j]);
				}
				this.KSs.Add(ksstruct);
			}
			ksstruct.depth = base.CurrentDepth - 1;
			int kspointer = this.KSpointer;
			this.KSpointer = kspointer + 1;
			return kspointer;
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00099D14 File Offset: 0x00097F14
		public KeySequence PopKS()
		{
			ArrayList kss = this.KSs;
			int num = this.KSpointer - 1;
			this.KSpointer = num;
			return ((KSStruct)kss[num]).ks;
		}

		// Token: 0x04000B49 RID: 2889
		private ConstraintStruct cs;

		// Token: 0x04000B4A RID: 2890
		private ArrayList KSs;

		// Token: 0x04000B4B RID: 2891
		private int KSpointer;
	}
}
