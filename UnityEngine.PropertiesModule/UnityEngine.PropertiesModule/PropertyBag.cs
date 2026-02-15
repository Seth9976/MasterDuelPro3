using System;
using System.Collections.Generic;
using Unity.Properties.Internal;

namespace Unity.Properties
{
	// Token: 0x02000040 RID: 64
	public static class PropertyBag
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00004D34 File Offset: 0x00002F34
		public static void AcceptWithSpecializedVisitor<TContainer>(IPropertyBag<TContainer> properties, IPropertyBagVisitor visitor, ref TContainer container)
		{
			bool flag = properties == null;
			if (flag)
			{
				throw new ArgumentNullException("properties");
			}
			IDictionaryPropertyBagAccept<TContainer> accept = properties as IDictionaryPropertyBagAccept<TContainer>;
			if (accept != null)
			{
				IDictionaryPropertyBagVisitor typedVisitor = visitor as IDictionaryPropertyBagVisitor;
				if (typedVisitor != null)
				{
					accept.Accept(typedVisitor, ref container);
					return;
				}
			}
			IListPropertyBagAccept<TContainer> accept2 = properties as IListPropertyBagAccept<TContainer>;
			if (accept2 != null)
			{
				IListPropertyBagVisitor typedVisitor2 = visitor as IListPropertyBagVisitor;
				if (typedVisitor2 != null)
				{
					accept2.Accept(typedVisitor2, ref container);
					return;
				}
			}
			ISetPropertyBagAccept<TContainer> accept3 = properties as ISetPropertyBagAccept<TContainer>;
			if (accept3 != null)
			{
				ISetPropertyBagVisitor typedVisitor3 = visitor as ISetPropertyBagVisitor;
				if (typedVisitor3 != null)
				{
					accept3.Accept(typedVisitor3, ref container);
					return;
				}
			}
			ICollectionPropertyBagAccept<TContainer> accept4 = properties as ICollectionPropertyBagAccept<TContainer>;
			if (accept4 != null)
			{
				ICollectionPropertyBagVisitor typedVisitor4 = visitor as ICollectionPropertyBagVisitor;
				if (typedVisitor4 != null)
				{
					accept4.Accept(typedVisitor4, ref container);
					return;
				}
			}
			properties.Accept(visitor, ref container);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004E00 File Offset: 0x00003000
		public static void Register<TContainer>(PropertyBag<TContainer> propertyBag)
		{
			PropertyBagStore.AddPropertyBag<TContainer>(propertyBag);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004E0C File Offset: 0x0000300C
		public static void RegisterList<TElement>()
		{
			bool flag = PropertyBagStore.TypedStore<IPropertyBag<TElement[]>>.PropertyBag == null;
			if (flag)
			{
				PropertyBagStore.AddPropertyBag<List<TElement>>(new ListPropertyBag<TElement>());
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004E33 File Offset: 0x00003033
		public static void RegisterList<TContainer, TElement>()
		{
			PropertyBag.RegisterList<TElement>();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004E3C File Offset: 0x0000303C
		public static IPropertyBag GetPropertyBag(Type type)
		{
			return PropertyBagStore.GetPropertyBag(type);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004E54 File Offset: 0x00003054
		public static IPropertyBag<TContainer> GetPropertyBag<TContainer>()
		{
			return PropertyBagStore.GetPropertyBag<TContainer>();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004E6C File Offset: 0x0000306C
		public static bool TryGetPropertyBagForValue<TValue>(ref TValue value, out IPropertyBag propertyBag)
		{
			return PropertyBagStore.TryGetPropertyBagForValue<TValue>(ref value, out propertyBag);
		}
	}
}
