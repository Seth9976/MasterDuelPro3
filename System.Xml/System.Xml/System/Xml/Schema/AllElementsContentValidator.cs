using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200022F RID: 559
	internal sealed class AllElementsContentValidator : ContentValidator
	{
		// Token: 0x06001B09 RID: 6921 RVA: 0x0009C3AA File Offset: 0x0009A5AA
		public AllElementsContentValidator(XmlSchemaContentType contentType, int size, bool isEmptiable)
			: base(contentType, false, isEmptiable)
		{
			this.elements = new Hashtable(size);
			this.particles = new object[size];
			this.isRequired = new BitSet(size);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0009C3DC File Offset: 0x0009A5DC
		public bool AddElement(XmlQualifiedName name, object particle, bool isEmptiable)
		{
			if (this.elements[name] != null)
			{
				return false;
			}
			int count = this.elements.Count;
			this.elements.Add(name, count);
			this.particles[count] = particle;
			if (!isEmptiable)
			{
				this.isRequired.Set(count);
				this.countRequired++;
			}
			return true;
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x0009C43E File Offset: 0x0009A63E
		public override bool IsEmptiable
		{
			get
			{
				return base.IsEmptiable || this.countRequired == 0;
			}
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0009C453 File Offset: 0x0009A653
		public override void InitValidation(ValidationState context)
		{
			context.AllElementsSet = new BitSet(this.elements.Count);
			context.CurrentState.AllElementsRequired = -1;
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0009C478 File Offset: 0x0009A678
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			object obj = this.elements[name];
			errorCode = 0;
			if (obj == null)
			{
				context.NeedValidateChildren = false;
				return null;
			}
			int num = (int)obj;
			if (context.AllElementsSet[num])
			{
				errorCode = -2;
				return null;
			}
			if (context.CurrentState.AllElementsRequired == -1)
			{
				context.CurrentState.AllElementsRequired = 0;
			}
			context.AllElementsSet.Set(num);
			if (this.isRequired[num])
			{
				context.CurrentState.AllElementsRequired = context.CurrentState.AllElementsRequired + 1;
			}
			return this.particles[num];
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0009C508 File Offset: 0x0009A708
		public override bool CompleteValidation(ValidationState context)
		{
			return context.CurrentState.AllElementsRequired == this.countRequired || (this.IsEmptiable && context.CurrentState.AllElementsRequired == -1);
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0009C538 File Offset: 0x0009A738
		public override ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			ArrayList arrayList = null;
			foreach (object obj in this.elements)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!context.AllElementsSet[(int)dictionaryEntry.Value] && (!isRequiredOnly || this.isRequired[(int)dictionaryEntry.Value]))
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(dictionaryEntry.Key);
				}
			}
			return arrayList;
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0009C5DC File Offset: 0x0009A7DC
		public override ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.elements)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!context.AllElementsSet[(int)dictionaryEntry.Value] && (!isRequiredOnly || this.isRequired[(int)dictionaryEntry.Value]))
				{
					ContentValidator.AddParticleToExpected(this.particles[(int)dictionaryEntry.Value] as XmlSchemaParticle, schemaSet, arrayList);
				}
			}
			return arrayList;
		}

		// Token: 0x04000B92 RID: 2962
		private Hashtable elements;

		// Token: 0x04000B93 RID: 2963
		private object[] particles;

		// Token: 0x04000B94 RID: 2964
		private BitSet isRequired;

		// Token: 0x04000B95 RID: 2965
		private int countRequired;
	}
}
