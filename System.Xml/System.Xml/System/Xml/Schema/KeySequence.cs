using System;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000219 RID: 537
	internal class KeySequence
	{
		// Token: 0x06001A7F RID: 6783 RVA: 0x0009A0B3 File Offset: 0x000982B3
		internal KeySequence(int dim, int line, int col)
		{
			this.dim = dim;
			this.ks = new TypedObject[dim];
			this.posline = line;
			this.poscol = col;
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x0009A0E3 File Offset: 0x000982E3
		public int PosLine
		{
			get
			{
				return this.posline;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0009A0EB File Offset: 0x000982EB
		public int PosCol
		{
			get
			{
				return this.poscol;
			}
		}

		// Token: 0x170005F8 RID: 1528
		public object this[int index]
		{
			get
			{
				return this.ks[index];
			}
			set
			{
				this.ks[index] = (TypedObject)value;
			}
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x0009A110 File Offset: 0x00098310
		internal bool IsQualified()
		{
			for (int i = 0; i < this.ks.Length; i++)
			{
				if (this.ks[i] == null || this.ks[i].Value == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0009A14C File Offset: 0x0009834C
		public override int GetHashCode()
		{
			if (this.hashcode != -1)
			{
				return this.hashcode;
			}
			this.hashcode = 0;
			for (int i = 0; i < this.ks.Length; i++)
			{
				this.ks[i].SetDecimal();
				if (this.ks[i].IsDecimal)
				{
					for (int j = 0; j < this.ks[i].Dim; j++)
					{
						this.hashcode += this.ks[i].Dvalue[j].GetHashCode();
					}
				}
				else
				{
					Array array = this.ks[i].Value as Array;
					if (array != null)
					{
						XmlAtomicValue[] array2 = array as XmlAtomicValue[];
						if (array2 != null)
						{
							for (int k = 0; k < array2.Length; k++)
							{
								this.hashcode += ((XmlAtomicValue)array2.GetValue(k)).TypedValue.GetHashCode();
							}
						}
						else
						{
							for (int l = 0; l < ((Array)this.ks[i].Value).Length; l++)
							{
								this.hashcode += ((Array)this.ks[i].Value).GetValue(l).GetHashCode();
							}
						}
					}
					else
					{
						this.hashcode += this.ks[i].Value.GetHashCode();
					}
				}
			}
			return this.hashcode;
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0009A2BC File Offset: 0x000984BC
		public override bool Equals(object other)
		{
			KeySequence keySequence = (KeySequence)other;
			for (int i = 0; i < this.ks.Length; i++)
			{
				if (!this.ks[i].Equals(keySequence.ks[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0009A300 File Offset: 0x00098500
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.ks[0].ToString());
			for (int i = 1; i < this.ks.Length; i++)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(this.ks[i].ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000B57 RID: 2903
		private TypedObject[] ks;

		// Token: 0x04000B58 RID: 2904
		private int dim;

		// Token: 0x04000B59 RID: 2905
		private int hashcode = -1;

		// Token: 0x04000B5A RID: 2906
		private int posline;

		// Token: 0x04000B5B RID: 2907
		private int poscol;
	}
}
