using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000045 RID: 69
	public class SetPropertyBagBase<TSet, TElement> : PropertyBag<TSet>, ISetPropertyBag<TSet, TElement>, ICollectionPropertyBag<TSet, TElement>, IPropertyBag<TSet>, IPropertyBag, ICollectionPropertyBagAccept<TSet>, ISetPropertyBagAccept<TSet>, IKeyedProperties<TSet, object> where TSet : ISet<TElement>
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00005310 File Offset: 0x00003510
		public override PropertyCollection<TSet> GetProperties()
		{
			return PropertyCollection<TSet>.Empty;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005328 File Offset: 0x00003528
		public override PropertyCollection<TSet> GetProperties(ref TSet container)
		{
			return new PropertyCollection<TSet>(this.GetPropertiesEnumerable(container));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000534B File Offset: 0x0000354B
		private IEnumerable<IProperty<TSet>> GetPropertiesEnumerable(TSet container)
		{
			foreach (TElement element in container)
			{
				this.m_Property.m_Value = element;
				yield return this.m_Property;
				element = default(TElement);
			}
			IEnumerator<TElement> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004702 File Offset: 0x00002902
		void ICollectionPropertyBagAccept<TSet>.Accept(ICollectionPropertyBagVisitor visitor, ref TSet container)
		{
			visitor.Visit<TSet, TElement>(this, ref container);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005362 File Offset: 0x00003562
		void ISetPropertyBagAccept<TSet>.Accept(ISetPropertyBagVisitor visitor, ref TSet container)
		{
			visitor.Visit<TSet, TElement>(this, ref container);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005370 File Offset: 0x00003570
		public bool TryGetProperty(ref TSet container, object key, out IProperty<TSet> property)
		{
			bool flag = container.Contains((TElement)((object)key));
			bool flag2;
			if (flag)
			{
				property = new SetPropertyBagBase<TSet, TElement>.SetElementProperty
				{
					m_Value = (TElement)((object)key)
				};
				flag2 = true;
			}
			else
			{
				property = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0400006F RID: 111
		private readonly SetPropertyBagBase<TSet, TElement>.SetElementProperty m_Property = new SetPropertyBagBase<TSet, TElement>.SetElementProperty();

		// Token: 0x02000046 RID: 70
		private class SetElementProperty : Property<TSet, TElement>, ISetElementProperty
		{
			// Token: 0x17000036 RID: 54
			// (get) Token: 0x0600011C RID: 284 RVA: 0x000053C8 File Offset: 0x000035C8
			public override string Name
			{
				get
				{
					return this.m_Value.ToString();
				}
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x0600011D RID: 285 RVA: 0x000044A9 File Offset: 0x000026A9
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600011E RID: 286 RVA: 0x000053DB File Offset: 0x000035DB
			public override TElement GetValue(ref TSet container)
			{
				return this.m_Value;
			}

			// Token: 0x0600011F RID: 287 RVA: 0x000053E3 File Offset: 0x000035E3
			public override void SetValue(ref TSet container, TElement value)
			{
				throw new InvalidOperationException("Property is ReadOnly.");
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x06000120 RID: 288 RVA: 0x000053EF File Offset: 0x000035EF
			public object ObjectKey
			{
				get
				{
					return this.m_Value;
				}
			}

			// Token: 0x04000070 RID: 112
			internal TElement m_Value;
		}
	}
}
