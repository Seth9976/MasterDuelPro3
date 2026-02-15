using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000141 RID: 321
	internal sealed class RegexNode
	{
		// Token: 0x0600071B RID: 1819 RVA: 0x00026A20 File Offset: 0x00024C20
		public RegexNode(int type, RegexOptions options)
		{
			this.NType = type;
			this.Options = options;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00026A36 File Offset: 0x00024C36
		public RegexNode(int type, RegexOptions options, char ch)
		{
			this.NType = type;
			this.Options = options;
			this.Ch = ch;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00026A53 File Offset: 0x00024C53
		public RegexNode(int type, RegexOptions options, string str)
		{
			this.NType = type;
			this.Options = options;
			this.Str = str;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00026A70 File Offset: 0x00024C70
		public RegexNode(int type, RegexOptions options, int m)
		{
			this.NType = type;
			this.Options = options;
			this.M = m;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00026A8D File Offset: 0x00024C8D
		public RegexNode(int type, RegexOptions options, int m, int n)
		{
			this.NType = type;
			this.Options = options;
			this.M = m;
			this.N = n;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00026AB2 File Offset: 0x00024CB2
		public bool UseOptionR()
		{
			return (this.Options & RegexOptions.RightToLeft) > RegexOptions.None;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00026AC0 File Offset: 0x00024CC0
		public RegexNode ReverseLeft()
		{
			if (this.UseOptionR() && this.NType == 25 && this.Children != null)
			{
				this.Children.Reverse(0, this.Children.Count);
			}
			return this;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00026AF4 File Offset: 0x00024CF4
		private void MakeRep(int type, int min, int max)
		{
			this.NType += type - 9;
			this.M = min;
			this.N = max;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00026B18 File Offset: 0x00024D18
		private RegexNode Reduce()
		{
			int num = this.Type();
			RegexNode regexNode;
			if (num != 5 && num != 11)
			{
				switch (num)
				{
				case 24:
					return this.ReduceAlternation();
				case 25:
					return this.ReduceConcatenation();
				case 26:
				case 27:
					return this.ReduceRep();
				case 29:
					return this.ReduceGroup();
				}
				regexNode = this;
			}
			else
			{
				regexNode = this.ReduceSet();
			}
			return regexNode;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00026B88 File Offset: 0x00024D88
		private RegexNode StripEnation(int emptyType)
		{
			int num = this.ChildCount();
			if (num == 0)
			{
				return new RegexNode(emptyType, this.Options);
			}
			if (num != 1)
			{
				return this;
			}
			return this.Child(0);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00026BBC File Offset: 0x00024DBC
		private RegexNode ReduceGroup()
		{
			RegexNode regexNode = this;
			while (regexNode.Type() == 29)
			{
				regexNode = regexNode.Child(0);
			}
			return regexNode;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00026BE0 File Offset: 0x00024DE0
		private RegexNode ReduceRep()
		{
			RegexNode regexNode = this;
			int num = this.Type();
			int num2 = this.M;
			int num3 = this.N;
			while (regexNode.ChildCount() != 0)
			{
				RegexNode regexNode2 = regexNode.Child(0);
				if (regexNode2.Type() != num)
				{
					int num4 = regexNode2.Type();
					if ((num4 < 3 || num4 > 5 || num != 26) && (num4 < 6 || num4 > 8 || num != 27))
					{
						break;
					}
				}
				if ((regexNode.M == 0 && regexNode2.M > 1) || regexNode2.N < regexNode2.M * 2)
				{
					break;
				}
				regexNode = regexNode2;
				if (regexNode.M > 0)
				{
					num2 = (regexNode.M = ((2147483646 / regexNode.M < num2) ? int.MaxValue : (regexNode.M * num2)));
				}
				if (regexNode.N > 0)
				{
					num3 = (regexNode.N = ((2147483646 / regexNode.N < num3) ? int.MaxValue : (regexNode.N * num3)));
				}
			}
			if (num2 != 2147483647)
			{
				return regexNode;
			}
			return new RegexNode(22, this.Options);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00026CF4 File Offset: 0x00024EF4
		private RegexNode ReduceSet()
		{
			if (RegexCharClass.IsEmpty(this.Str))
			{
				this.NType = 22;
				this.Str = null;
			}
			else if (RegexCharClass.IsSingleton(this.Str))
			{
				this.Ch = RegexCharClass.SingletonChar(this.Str);
				this.Str = null;
				this.NType += -2;
			}
			else if (RegexCharClass.IsSingletonInverse(this.Str))
			{
				this.Ch = RegexCharClass.SingletonChar(this.Str);
				this.Str = null;
				this.NType += -1;
			}
			return this;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00026D8C File Offset: 0x00024F8C
		private RegexNode ReduceAlternation()
		{
			if (this.Children == null)
			{
				return new RegexNode(22, this.Options);
			}
			bool flag = false;
			bool flag2 = false;
			RegexOptions regexOptions = RegexOptions.None;
			int i = 0;
			int num = 0;
			while (i < this.Children.Count)
			{
				RegexNode regexNode = this.Children[i];
				if (num < i)
				{
					this.Children[num] = regexNode;
				}
				if (regexNode.NType == 24)
				{
					for (int j = 0; j < regexNode.Children.Count; j++)
					{
						regexNode.Children[j].Next = this;
					}
					this.Children.InsertRange(i + 1, regexNode.Children);
					num--;
				}
				else if (regexNode.NType == 11 || regexNode.NType == 9)
				{
					RegexOptions regexOptions2 = regexNode.Options & (RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
					if (regexNode.NType == 11)
					{
						if (!flag || regexOptions != regexOptions2 || flag2 || !RegexCharClass.IsMergeable(regexNode.Str))
						{
							flag = true;
							flag2 = !RegexCharClass.IsMergeable(regexNode.Str);
							regexOptions = regexOptions2;
							goto IL_01D0;
						}
					}
					else if (!flag || regexOptions != regexOptions2 || flag2)
					{
						flag = true;
						flag2 = false;
						regexOptions = regexOptions2;
						goto IL_01D0;
					}
					num--;
					RegexNode regexNode2 = this.Children[num];
					RegexCharClass regexCharClass;
					if (regexNode2.NType == 9)
					{
						regexCharClass = new RegexCharClass();
						regexCharClass.AddChar(regexNode2.Ch);
					}
					else
					{
						regexCharClass = RegexCharClass.Parse(regexNode2.Str);
					}
					if (regexNode.NType == 9)
					{
						regexCharClass.AddChar(regexNode.Ch);
					}
					else
					{
						RegexCharClass regexCharClass2 = RegexCharClass.Parse(regexNode.Str);
						regexCharClass.AddCharClass(regexCharClass2);
					}
					regexNode2.NType = 11;
					regexNode2.Str = regexCharClass.ToStringClass();
				}
				else if (regexNode.NType == 22)
				{
					num--;
				}
				else
				{
					flag = false;
					flag2 = false;
				}
				IL_01D0:
				i++;
				num++;
			}
			if (num < i)
			{
				this.Children.RemoveRange(num, i - num);
			}
			return this.StripEnation(22);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00026FA8 File Offset: 0x000251A8
		private RegexNode ReduceConcatenation()
		{
			if (this.Children == null)
			{
				return new RegexNode(23, this.Options);
			}
			bool flag = false;
			RegexOptions regexOptions = RegexOptions.None;
			int i = 0;
			int num = 0;
			while (i < this.Children.Count)
			{
				RegexNode regexNode = this.Children[i];
				if (num < i)
				{
					this.Children[num] = regexNode;
				}
				if (regexNode.NType == 25 && (regexNode.Options & RegexOptions.RightToLeft) == (this.Options & RegexOptions.RightToLeft))
				{
					for (int j = 0; j < regexNode.Children.Count; j++)
					{
						regexNode.Children[j].Next = this;
					}
					this.Children.InsertRange(i + 1, regexNode.Children);
					num--;
				}
				else if (regexNode.NType == 12 || regexNode.NType == 9)
				{
					RegexOptions regexOptions2 = regexNode.Options & (RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
					if (!flag || regexOptions != regexOptions2)
					{
						flag = true;
						regexOptions = regexOptions2;
					}
					else
					{
						RegexNode regexNode2 = this.Children[--num];
						if (regexNode2.NType == 9)
						{
							regexNode2.NType = 12;
							regexNode2.Str = Convert.ToString(regexNode2.Ch, CultureInfo.InvariantCulture);
						}
						if ((regexOptions2 & RegexOptions.RightToLeft) == RegexOptions.None)
						{
							if (regexNode.NType == 9)
							{
								RegexNode regexNode3 = regexNode2;
								regexNode3.Str += regexNode.Ch.ToString();
							}
							else
							{
								RegexNode regexNode4 = regexNode2;
								regexNode4.Str += regexNode.Str;
							}
						}
						else if (regexNode.NType == 9)
						{
							regexNode2.Str = regexNode.Ch.ToString() + regexNode2.Str;
						}
						else
						{
							regexNode2.Str = regexNode.Str + regexNode2.Str;
						}
					}
				}
				else if (regexNode.NType == 23)
				{
					num--;
				}
				else
				{
					flag = false;
				}
				i++;
				num++;
			}
			if (num < i)
			{
				this.Children.RemoveRange(num, i - num);
			}
			return this.StripEnation(23);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000271C0 File Offset: 0x000253C0
		public RegexNode MakeQuantifier(bool lazy, int min, int max)
		{
			if (min == 0 && max == 0)
			{
				return new RegexNode(23, this.Options);
			}
			if (min == 1 && max == 1)
			{
				return this;
			}
			int ntype = this.NType;
			if (ntype - 9 <= 2)
			{
				this.MakeRep(lazy ? 6 : 3, min, max);
				return this;
			}
			RegexNode regexNode = new RegexNode(lazy ? 27 : 26, this.Options, min, max);
			regexNode.AddChild(this);
			return regexNode;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00027228 File Offset: 0x00025428
		public void AddChild(RegexNode newChild)
		{
			if (this.Children == null)
			{
				this.Children = new List<RegexNode>(4);
			}
			RegexNode regexNode = newChild.Reduce();
			this.Children.Add(regexNode);
			regexNode.Next = this;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00027263 File Offset: 0x00025463
		public RegexNode Child(int i)
		{
			return this.Children[i];
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00027271 File Offset: 0x00025471
		public int ChildCount()
		{
			if (this.Children != null)
			{
				return this.Children.Count;
			}
			return 0;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00027288 File Offset: 0x00025488
		public int Type()
		{
			return this.NType;
		}

		// Token: 0x040005B5 RID: 1461
		public int NType;

		// Token: 0x040005B6 RID: 1462
		public List<RegexNode> Children;

		// Token: 0x040005B7 RID: 1463
		public string Str;

		// Token: 0x040005B8 RID: 1464
		public char Ch;

		// Token: 0x040005B9 RID: 1465
		public int M;

		// Token: 0x040005BA RID: 1466
		public int N;

		// Token: 0x040005BB RID: 1467
		public readonly RegexOptions Options;

		// Token: 0x040005BC RID: 1468
		public RegexNode Next;
	}
}
