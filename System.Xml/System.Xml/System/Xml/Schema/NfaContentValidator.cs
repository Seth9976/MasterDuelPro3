using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200022C RID: 556
	internal sealed class NfaContentValidator : ContentValidator
	{
		// Token: 0x06001AFD RID: 6909 RVA: 0x0009BA71 File Offset: 0x00099C71
		internal NfaContentValidator(BitSet firstpos, BitSet[] followpos, SymbolsDictionary symbols, Positions positions, int endMarkerPos, XmlSchemaContentType contentType, bool isOpen, bool isEmptiable)
			: base(contentType, isOpen, isEmptiable)
		{
			this.firstpos = firstpos;
			this.followpos = followpos;
			this.symbols = symbols;
			this.positions = positions;
			this.endMarkerPos = endMarkerPos;
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0009BAA4 File Offset: 0x00099CA4
		public override void InitValidation(ValidationState context)
		{
			context.CurPos[0] = this.firstpos.Clone();
			context.CurPos[1] = new BitSet(this.firstpos.Count);
			context.CurrentState.CurPosIndex = 0;
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0009BAE0 File Offset: 0x00099CE0
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			BitSet bitSet = context.CurPos[context.CurrentState.CurPosIndex];
			int num = (context.CurrentState.CurPosIndex + 1) % 2;
			BitSet bitSet2 = context.CurPos[num];
			bitSet2.Clear();
			int num2 = this.symbols[name];
			object obj = null;
			errorCode = 0;
			for (int num3 = bitSet.NextSet(-1); num3 != -1; num3 = bitSet.NextSet(num3))
			{
				if (num2 == this.positions[num3].symbol)
				{
					bitSet2.Or(this.followpos[num3]);
					obj = this.positions[num3].particle;
					break;
				}
			}
			if (!bitSet2.IsEmpty)
			{
				context.CurrentState.CurPosIndex = num;
				return obj;
			}
			if (base.IsOpen && bitSet[this.endMarkerPos])
			{
				return null;
			}
			context.NeedValidateChildren = false;
			errorCode = -1;
			return null;
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0009BBC0 File Offset: 0x00099DC0
		public override bool CompleteValidation(ValidationState context)
		{
			return context.CurPos[context.CurrentState.CurPosIndex][this.endMarkerPos];
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0009BBE4 File Offset: 0x00099DE4
		public override ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			ArrayList arrayList = null;
			BitSet bitSet = context.CurPos[context.CurrentState.CurPosIndex];
			for (int num = bitSet.NextSet(-1); num != -1; num = bitSet.NextSet(num))
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)this.positions[num].particle;
				if (xmlSchemaParticle == null)
				{
					string text = this.symbols.NameOf(this.positions[num].symbol);
					if (text.Length != 0)
					{
						arrayList.Add(text);
					}
				}
				else
				{
					string nameString = xmlSchemaParticle.NameString;
					if (!arrayList.Contains(nameString))
					{
						arrayList.Add(nameString);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0009BC90 File Offset: 0x00099E90
		public override ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
		{
			ArrayList arrayList = new ArrayList();
			BitSet bitSet = context.CurPos[context.CurrentState.CurPosIndex];
			for (int num = bitSet.NextSet(-1); num != -1; num = bitSet.NextSet(num))
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)this.positions[num].particle;
				if (xmlSchemaParticle != null)
				{
					ContentValidator.AddParticleToExpected(xmlSchemaParticle, schemaSet, arrayList);
				}
			}
			return arrayList;
		}

		// Token: 0x04000B84 RID: 2948
		private BitSet firstpos;

		// Token: 0x04000B85 RID: 2949
		private BitSet[] followpos;

		// Token: 0x04000B86 RID: 2950
		private SymbolsDictionary symbols;

		// Token: 0x04000B87 RID: 2951
		private Positions positions;

		// Token: 0x04000B88 RID: 2952
		private int endMarkerPos;
	}
}
