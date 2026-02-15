using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200022A RID: 554
	internal sealed class ParticleContentValidator : ContentValidator
	{
		// Token: 0x06001ADD RID: 6877 RVA: 0x0009AF64 File Offset: 0x00099164
		public ParticleContentValidator(XmlSchemaContentType contentType)
			: this(contentType, true)
		{
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0009AF6E File Offset: 0x0009916E
		public ParticleContentValidator(XmlSchemaContentType contentType, bool enableUpaCheck)
			: base(contentType)
		{
			this.enableUpaCheck = enableUpaCheck;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x000356D5 File Offset: 0x000338D5
		public override void InitValidation(ValidationState context)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x000356D5 File Offset: 0x000338D5
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x000356D5 File Offset: 0x000338D5
		public override bool CompleteValidation(ValidationState context)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0009AF7E File Offset: 0x0009917E
		public void Start()
		{
			this.symbols = new SymbolsDictionary();
			this.positions = new Positions();
			this.stack = new Stack();
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0009AFA1 File Offset: 0x000991A1
		public void OpenGroup()
		{
			this.stack.Push(null);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0009AFB0 File Offset: 0x000991B0
		public void CloseGroup()
		{
			SyntaxTreeNode syntaxTreeNode = (SyntaxTreeNode)this.stack.Pop();
			if (syntaxTreeNode == null)
			{
				return;
			}
			if (this.stack.Count == 0)
			{
				this.contentNode = syntaxTreeNode;
				this.isPartial = false;
				return;
			}
			InteriorNode interiorNode = (InteriorNode)this.stack.Pop();
			if (interiorNode != null)
			{
				interiorNode.RightChild = syntaxTreeNode;
				syntaxTreeNode = interiorNode;
				this.isPartial = true;
			}
			else
			{
				this.isPartial = false;
			}
			this.stack.Push(syntaxTreeNode);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0009B027 File Offset: 0x00099227
		public bool Exists(XmlQualifiedName name)
		{
			return this.symbols.Exists(name);
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0009B03A File Offset: 0x0009923A
		public void AddName(XmlQualifiedName name, object particle)
		{
			this.AddLeafNode(new LeafNode(this.positions.Add(this.symbols.AddName(name, particle), particle)));
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0009B060 File Offset: 0x00099260
		public void AddNamespaceList(NamespaceList namespaceList, object particle)
		{
			this.symbols.AddNamespaceList(namespaceList, particle, false);
			this.AddLeafNode(new NamespaceListNode(namespaceList, particle));
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0009B080 File Offset: 0x00099280
		private void AddLeafNode(SyntaxTreeNode node)
		{
			if (this.stack.Count > 0)
			{
				InteriorNode interiorNode = (InteriorNode)this.stack.Pop();
				if (interiorNode != null)
				{
					interiorNode.RightChild = node;
					node = interiorNode;
				}
			}
			this.stack.Push(node);
			this.isPartial = true;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0009B0CC File Offset: 0x000992CC
		public void AddChoice()
		{
			SyntaxTreeNode syntaxTreeNode = (SyntaxTreeNode)this.stack.Pop();
			InteriorNode interiorNode = new ChoiceNode();
			interiorNode.LeftChild = syntaxTreeNode;
			this.stack.Push(interiorNode);
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0009B104 File Offset: 0x00099304
		public void AddSequence()
		{
			SyntaxTreeNode syntaxTreeNode = (SyntaxTreeNode)this.stack.Pop();
			InteriorNode interiorNode = new SequenceNode();
			interiorNode.LeftChild = syntaxTreeNode;
			this.stack.Push(interiorNode);
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x0009B13B File Offset: 0x0009933B
		public void AddStar()
		{
			this.Closure(new StarNode());
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0009B148 File Offset: 0x00099348
		public void AddPlus()
		{
			this.Closure(new PlusNode());
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x0009B155 File Offset: 0x00099355
		public void AddQMark()
		{
			this.Closure(new QmarkNode());
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0009B164 File Offset: 0x00099364
		public void AddLeafRange(decimal min, decimal max)
		{
			LeafRangeNode leafRangeNode = new LeafRangeNode(min, max);
			int num = this.positions.Add(-2, leafRangeNode);
			leafRangeNode.Pos = num;
			this.Closure(new SequenceNode
			{
				RightChild = leafRangeNode
			});
			this.minMaxNodesCount++;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0009B1B4 File Offset: 0x000993B4
		private void Closure(InteriorNode node)
		{
			if (this.stack.Count > 0)
			{
				SyntaxTreeNode syntaxTreeNode = (SyntaxTreeNode)this.stack.Pop();
				InteriorNode interiorNode = syntaxTreeNode as InteriorNode;
				if (this.isPartial && interiorNode != null)
				{
					node.LeftChild = interiorNode.RightChild;
					interiorNode.RightChild = node;
				}
				else
				{
					node.LeftChild = syntaxTreeNode;
					syntaxTreeNode = node;
				}
				this.stack.Push(syntaxTreeNode);
				return;
			}
			if (this.contentNode != null)
			{
				node.LeftChild = this.contentNode;
				this.contentNode = node;
			}
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0009B238 File Offset: 0x00099438
		public ContentValidator Finish(bool useDFA)
		{
			if (this.contentNode == null)
			{
				if (base.ContentType != XmlSchemaContentType.Mixed)
				{
					return ContentValidator.Empty;
				}
				bool isOpen = base.IsOpen;
				if (!base.IsOpen)
				{
					return ContentValidator.TextOnly;
				}
				return ContentValidator.Any;
			}
			else
			{
				InteriorNode interiorNode = new SequenceNode();
				interiorNode.LeftChild = this.contentNode;
				LeafNode leafNode = new LeafNode(this.positions.Add(this.symbols.AddName(XmlQualifiedName.Empty, null), null));
				interiorNode.RightChild = leafNode;
				this.contentNode.ExpandTree(interiorNode, this.symbols, this.positions);
				int count = this.symbols.Count;
				int count2 = this.positions.Count;
				BitSet bitSet = new BitSet(count2);
				BitSet bitSet2 = new BitSet(count2);
				BitSet[] array = new BitSet[count2];
				for (int i = 0; i < count2; i++)
				{
					array[i] = new BitSet(count2);
				}
				interiorNode.ConstructPos(bitSet, bitSet2, array);
				if (this.minMaxNodesCount > 0)
				{
					BitSet bitSet3;
					BitSet[] array2 = this.CalculateTotalFollowposForRangeNodes(bitSet, array, out bitSet3);
					if (this.enableUpaCheck)
					{
						this.CheckCMUPAWithLeafRangeNodes(this.GetApplicableMinMaxFollowPos(bitSet, bitSet3, array2));
						for (int j = 0; j < count2; j++)
						{
							this.CheckCMUPAWithLeafRangeNodes(this.GetApplicableMinMaxFollowPos(array[j], bitSet3, array2));
						}
					}
					return new RangeContentValidator(bitSet, array, this.symbols, this.positions, leafNode.Pos, base.ContentType, interiorNode.LeftChild.IsNullable, bitSet3, this.minMaxNodesCount);
				}
				int[][] array3 = null;
				if (!this.symbols.IsUpaEnforced)
				{
					if (this.enableUpaCheck)
					{
						this.CheckUniqueParticleAttribution(bitSet, array);
					}
				}
				else if (useDFA)
				{
					array3 = this.BuildTransitionTable(bitSet, array, leafNode.Pos);
				}
				if (array3 != null)
				{
					return new DfaContentValidator(array3, this.symbols, base.ContentType, base.IsOpen, interiorNode.LeftChild.IsNullable);
				}
				return new NfaContentValidator(bitSet, array, this.symbols, this.positions, leafNode.Pos, base.ContentType, base.IsOpen, interiorNode.LeftChild.IsNullable);
			}
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0009B43C File Offset: 0x0009963C
		private BitSet[] CalculateTotalFollowposForRangeNodes(BitSet firstpos, BitSet[] followpos, out BitSet posWithRangeTerminals)
		{
			int count = this.positions.Count;
			posWithRangeTerminals = new BitSet(count);
			BitSet[] array = new BitSet[this.minMaxNodesCount];
			int num = 0;
			for (int i = count - 1; i >= 0; i--)
			{
				Position position = this.positions[i];
				if (position.symbol == -2)
				{
					LeafRangeNode leafRangeNode = position.particle as LeafRangeNode;
					BitSet bitSet = new BitSet(count);
					bitSet.Clear();
					bitSet.Or(followpos[i]);
					if (leafRangeNode.Min != leafRangeNode.Max)
					{
						bitSet.Or(leafRangeNode.NextIteration);
					}
					for (int num2 = bitSet.NextSet(-1); num2 != -1; num2 = bitSet.NextSet(num2))
					{
						if (num2 > i)
						{
							Position position2 = this.positions[num2];
							if (position2.symbol == -2)
							{
								LeafRangeNode leafRangeNode2 = position2.particle as LeafRangeNode;
								bitSet.Or(array[leafRangeNode2.Pos]);
							}
						}
					}
					array[num] = bitSet;
					leafRangeNode.Pos = num++;
					posWithRangeTerminals.Set(i);
				}
			}
			return array;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x0009B558 File Offset: 0x00099758
		private void CheckCMUPAWithLeafRangeNodes(BitSet curpos)
		{
			object[] array = new object[this.symbols.Count];
			for (int num = curpos.NextSet(-1); num != -1; num = curpos.NextSet(num))
			{
				Position position = this.positions[num];
				int symbol = position.symbol;
				if (symbol >= 0)
				{
					if (array[symbol] != null)
					{
						throw new UpaException(array[symbol], position.particle);
					}
					array[symbol] = position.particle;
				}
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0009B5C4 File Offset: 0x000997C4
		private BitSet GetApplicableMinMaxFollowPos(BitSet curpos, BitSet posWithRangeTerminals, BitSet[] minmaxFollowPos)
		{
			if (curpos.Intersects(posWithRangeTerminals))
			{
				BitSet bitSet = new BitSet(this.positions.Count);
				bitSet.Or(curpos);
				bitSet.And(posWithRangeTerminals);
				curpos = curpos.Clone();
				for (int num = bitSet.NextSet(-1); num != -1; num = bitSet.NextSet(num))
				{
					LeafRangeNode leafRangeNode = this.positions[num].particle as LeafRangeNode;
					curpos.Or(minmaxFollowPos[leafRangeNode.Pos]);
				}
			}
			return curpos;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0009B640 File Offset: 0x00099840
		private void CheckUniqueParticleAttribution(BitSet firstpos, BitSet[] followpos)
		{
			this.CheckUniqueParticleAttribution(firstpos);
			for (int i = 0; i < this.positions.Count; i++)
			{
				this.CheckUniqueParticleAttribution(followpos[i]);
			}
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0009B674 File Offset: 0x00099874
		private void CheckUniqueParticleAttribution(BitSet curpos)
		{
			object[] array = new object[this.symbols.Count];
			for (int num = curpos.NextSet(-1); num != -1; num = curpos.NextSet(num))
			{
				int symbol = this.positions[num].symbol;
				if (array[symbol] == null)
				{
					array[symbol] = this.positions[num].particle;
				}
				else if (array[symbol] != this.positions[num].particle)
				{
					throw new UpaException(array[symbol], this.positions[num].particle);
				}
			}
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0009B708 File Offset: 0x00099908
		private int[][] BuildTransitionTable(BitSet firstpos, BitSet[] followpos, int endMarkerPos)
		{
			int count = this.positions.Count;
			int num = 8192 / count;
			int count2 = this.symbols.Count;
			ArrayList arrayList = new ArrayList();
			Hashtable hashtable = new Hashtable();
			hashtable.Add(new BitSet(count), -1);
			Queue queue = new Queue();
			int num2 = 0;
			queue.Enqueue(firstpos);
			hashtable.Add(firstpos, 0);
			arrayList.Add(new int[count2 + 1]);
			while (queue.Count > 0)
			{
				BitSet bitSet = (BitSet)queue.Dequeue();
				int[] array = (int[])arrayList[num2];
				if (bitSet[endMarkerPos])
				{
					array[count2] = 1;
				}
				for (int i = 0; i < count2; i++)
				{
					BitSet bitSet2 = new BitSet(count);
					for (int num3 = bitSet.NextSet(-1); num3 != -1; num3 = bitSet.NextSet(num3))
					{
						if (i == this.positions[num3].symbol)
						{
							bitSet2.Or(followpos[num3]);
						}
					}
					object obj = hashtable[bitSet2];
					if (obj != null)
					{
						array[i] = (int)obj;
					}
					else
					{
						int num4 = hashtable.Count - 1;
						if (num4 >= num)
						{
							return null;
						}
						queue.Enqueue(bitSet2);
						hashtable.Add(bitSet2, num4);
						arrayList.Add(new int[count2 + 1]);
						array[i] = num4;
					}
				}
				num2++;
			}
			return (int[][])arrayList.ToArray(typeof(int[]));
		}

		// Token: 0x04000B7B RID: 2939
		private SymbolsDictionary symbols;

		// Token: 0x04000B7C RID: 2940
		private Positions positions;

		// Token: 0x04000B7D RID: 2941
		private Stack stack;

		// Token: 0x04000B7E RID: 2942
		private SyntaxTreeNode contentNode;

		// Token: 0x04000B7F RID: 2943
		private bool isPartial;

		// Token: 0x04000B80 RID: 2944
		private int minMaxNodesCount;

		// Token: 0x04000B81 RID: 2945
		private bool enableUpaCheck;
	}
}
