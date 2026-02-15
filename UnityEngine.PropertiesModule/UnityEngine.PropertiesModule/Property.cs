using System;
using System.Collections.Generic;
using Unity.Properties.Internal;

namespace Unity.Properties
{
	// Token: 0x02000018 RID: 24
	public abstract class Property<TContainer, TValue> : IProperty<TContainer>, IProperty, IPropertyAccept<TContainer>, IAttributes
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000026DA File Offset: 0x000008DA
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000026E2 File Offset: 0x000008E2
		List<Attribute> IAttributes.Attributes
		{
			get
			{
				return this.m_Attributes;
			}
			set
			{
				this.m_Attributes = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51
		public abstract string Name { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000034 RID: 52
		public abstract bool IsReadOnly { get; }

		// Token: 0x06000035 RID: 53 RVA: 0x000026EB File Offset: 0x000008EB
		public Type DeclaredValueType()
		{
			return typeof(TValue);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026F7 File Offset: 0x000008F7
		public void Accept(IPropertyVisitor visitor, ref TContainer container)
		{
			visitor.Visit<TContainer, TValue>(this, ref container);
		}

		// Token: 0x06000037 RID: 55
		public abstract TValue GetValue(ref TContainer container);

		// Token: 0x06000038 RID: 56
		public abstract void SetValue(ref TContainer container, TValue value);

		// Token: 0x06000039 RID: 57 RVA: 0x00002702 File Offset: 0x00000902
		protected void AddAttribute(Attribute attribute)
		{
			((IAttributes)this).AddAttribute(attribute);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000270C File Offset: 0x0000090C
		protected void AddAttributes(IEnumerable<Attribute> attributes)
		{
			((IAttributes)this).AddAttributes(attributes);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002718 File Offset: 0x00000918
		void IAttributes.AddAttribute(Attribute attribute)
		{
			bool flag = attribute == null || attribute.GetType() == typeof(CreatePropertyAttribute);
			if (!flag)
			{
				bool flag2 = this.m_Attributes == null;
				if (flag2)
				{
					this.m_Attributes = new List<Attribute>();
				}
				this.m_Attributes.Add(attribute);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000276C File Offset: 0x0000096C
		void IAttributes.AddAttributes(IEnumerable<Attribute> attributes)
		{
			bool flag = this.m_Attributes == null;
			if (flag)
			{
				this.m_Attributes = new List<Attribute>();
			}
			foreach (Attribute attribute in attributes)
			{
				bool flag2 = attribute == null;
				if (!flag2)
				{
					this.m_Attributes.Add(attribute);
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027E4 File Offset: 0x000009E4
		public bool HasAttribute<TAttribute>() where TAttribute : Attribute
		{
			int i = 0;
			for (;;)
			{
				int num = i;
				List<Attribute> attributes = this.m_Attributes;
				int? num2 = ((attributes != null) ? new int?(attributes.Count) : null);
				if (!((num < num2.GetValueOrDefault()) & (num2 != null)))
				{
					goto Block_3;
				}
				bool flag = this.m_Attributes[i] is TAttribute;
				if (flag)
				{
					break;
				}
				i++;
			}
			return true;
			Block_3:
			return false;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002858 File Offset: 0x00000A58
		public TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute
		{
			int i = 0;
			TAttribute typed;
			for (;;)
			{
				int num = i;
				List<Attribute> attributes = this.m_Attributes;
				int? num2 = ((attributes != null) ? new int?(attributes.Count) : null);
				if (!((num < num2.GetValueOrDefault()) & (num2 != null)))
				{
					goto Block_3;
				}
				typed = this.m_Attributes[i] as TAttribute;
				bool flag = typed != null;
				if (flag)
				{
					break;
				}
				i++;
			}
			return typed;
			Block_3:
			return default(TAttribute);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000028E1 File Offset: 0x00000AE1
		AttributesScope IAttributes.CreateAttributesScope(IAttributes attributes)
		{
			return new AttributesScope(this, (attributes != null) ? attributes.Attributes : null);
		}

		// Token: 0x04000023 RID: 35
		private List<Attribute> m_Attributes;
	}
}
