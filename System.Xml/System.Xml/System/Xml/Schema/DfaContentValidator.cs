using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200022B RID: 555
	internal sealed class DfaContentValidator : ContentValidator
	{
		// Token: 0x06001AF7 RID: 6903 RVA: 0x0009B896 File Offset: 0x00099A96
		internal DfaContentValidator(int[][] transitionTable, SymbolsDictionary symbols, XmlSchemaContentType contentType, bool isOpen, bool isEmptiable)
			: base(contentType, isOpen, isEmptiable)
		{
			this.transitionTable = transitionTable;
			this.symbols = symbols;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0009B8B1 File Offset: 0x00099AB1
		public override void InitValidation(ValidationState context)
		{
			context.CurrentState.State = 0;
			context.HasMatched = this.transitionTable[0][this.symbols.Count] > 0;
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0009B8DC File Offset: 0x00099ADC
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			int num = this.symbols[name];
			int num2 = this.transitionTable[context.CurrentState.State][num];
			errorCode = 0;
			if (num2 != -1)
			{
				context.CurrentState.State = num2;
				context.HasMatched = this.transitionTable[context.CurrentState.State][this.symbols.Count] > 0;
				return this.symbols.GetParticle(num);
			}
			if (base.IsOpen && context.HasMatched)
			{
				return null;
			}
			context.NeedValidateChildren = false;
			errorCode = -1;
			return null;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0009B96F File Offset: 0x00099B6F
		public override bool CompleteValidation(ValidationState context)
		{
			return context.HasMatched;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0009B97C File Offset: 0x00099B7C
		public override ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			ArrayList arrayList = null;
			int[] array = this.transitionTable[context.CurrentState.State];
			if (array != null)
			{
				for (int i = 0; i < array.Length - 1; i++)
				{
					if (array[i] != -1)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)this.symbols.GetParticle(i);
						if (xmlSchemaParticle == null)
						{
							string text = this.symbols.NameOf(i);
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
				}
			}
			return arrayList;
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x0009BA14 File Offset: 0x00099C14
		public override ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
		{
			ArrayList arrayList = new ArrayList();
			int[] array = this.transitionTable[context.CurrentState.State];
			if (array != null)
			{
				for (int i = 0; i < array.Length - 1; i++)
				{
					if (array[i] != -1)
					{
						XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)this.symbols.GetParticle(i);
						if (xmlSchemaParticle != null)
						{
							ContentValidator.AddParticleToExpected(xmlSchemaParticle, schemaSet, arrayList);
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x04000B82 RID: 2946
		private int[][] transitionTable;

		// Token: 0x04000B83 RID: 2947
		private SymbolsDictionary symbols;
	}
}
