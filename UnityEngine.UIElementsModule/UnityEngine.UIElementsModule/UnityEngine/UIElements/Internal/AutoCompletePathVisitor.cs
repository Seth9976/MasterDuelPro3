using System;
using System.Collections.Generic;
using Unity.Properties;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x020005E1 RID: 1505
	internal class AutoCompletePathVisitor : ITypeVisitor, IPropertyVisitor, IPropertyBagVisitor, IListPropertyVisitor
	{
		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x060028CC RID: 10444 RVA: 0x000A7AC4 File Offset: 0x000A5CC4
		public int maxDepth { get; }

		// Token: 0x060028CD RID: 10445 RVA: 0x000A7ACC File Offset: 0x000A5CCC
		private bool HasReachedEnd(Type containerType)
		{
			return this.m_VisitContext.currentDepth >= this.maxDepth || this.m_VisitContext.types.Contains(containerType);
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x000A7AF8 File Offset: 0x000A5CF8
		public void Reset()
		{
			this.m_VisitContext.current = default(PropertyPath);
			this.m_VisitContext.propertyPathInfos = null;
			this.m_VisitContext.types.Clear();
			this.m_VisitContext.currentDepth = 0;
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000A7B48 File Offset: 0x000A5D48
		void ITypeVisitor.Visit<TContainer>()
		{
			bool flag = this.HasReachedEnd(typeof(TContainer));
			if (!flag)
			{
				using (new AutoCompletePathVisitor.InspectedTypeScope<TContainer>(this.m_VisitContext))
				{
					IPropertyBag<TContainer> bag = PropertyBag.GetPropertyBag<TContainer>();
					bool flag2 = bag == null;
					if (!flag2)
					{
						foreach (IProperty<TContainer> property in bag.GetProperties())
						{
							using (new AutoCompletePathVisitor.VisitedPropertyScope(this.m_VisitContext, property))
							{
								this.VisitPropertyType(property.DeclaredValueType());
							}
						}
					}
				}
			}
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000A7C2C File Offset: 0x000A5E2C
		void IPropertyBagVisitor.Visit<TContainer>(IPropertyBag<TContainer> properties, ref TContainer container)
		{
			bool flag = this.HasReachedEnd(typeof(TContainer));
			if (!flag)
			{
				using (new AutoCompletePathVisitor.InspectedTypeScope<TContainer>(this.m_VisitContext))
				{
					IIndexedProperties<TContainer> indexedProperties = properties as IIndexedProperties<TContainer>;
					if (indexedProperties == null)
					{
						if (!(properties is IKeyedProperties<TContainer, object>))
						{
							foreach (IProperty<TContainer> property in properties.GetProperties(ref container))
							{
								using (new AutoCompletePathVisitor.VisitedPropertyScope(this.m_VisitContext, property))
								{
									property.Accept(this, ref container);
								}
							}
						}
					}
					else
					{
						IProperty<TContainer> indexProperty;
						bool flag2 = indexedProperties.TryGetProperty(ref container, 0, out indexProperty);
						if (flag2)
						{
							using (new AutoCompletePathVisitor.VisitedPropertyScope(this.m_VisitContext, 0, indexProperty.DeclaredValueType()))
							{
								indexProperty.Accept(this, ref container);
							}
						}
						else
						{
							this.VisitPropertyType(typeof(TContainer));
						}
					}
				}
			}
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000A7D84 File Offset: 0x000A5F84
		void IPropertyVisitor.Visit<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container)
		{
			bool flag = !TypeTraits.IsContainer(typeof(TValue));
			if (!flag)
			{
				TValue value = property.GetValue(ref container);
				IPropertyBag valuePropertyBag;
				bool flag2 = (!TypeTraits<TValue>.CanBeNull || !EqualityComparer<TValue>.Default.Equals(value, default(TValue))) && PropertyBag.TryGetPropertyBagForValue<TValue>(ref value, out valuePropertyBag);
				if (flag2)
				{
					IPropertyBag propertyBag = valuePropertyBag;
					IPropertyBag propertyBag2 = propertyBag;
					IListPropertyAccept<TValue> accept = propertyBag2 as IListPropertyAccept<TValue>;
					if (accept == null)
					{
						PropertyContainer.TryAccept<TValue>(this, ref value, default(VisitParameters));
					}
					else
					{
						accept.Accept<TContainer>(this, property, ref container, ref value);
					}
				}
				else
				{
					this.VisitPropertyType(property.DeclaredValueType());
				}
			}
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000A7E3C File Offset: 0x000A603C
		void IListPropertyVisitor.Visit<TContainer, TList, TElement>(Property<TContainer, TList> property, ref TContainer container, ref TList list)
		{
			PropertyContainer.TryAccept<TList>(this, ref list, default(VisitParameters));
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000A7E5C File Offset: 0x000A605C
		private void VisitPropertyType(Type type)
		{
			bool flag = this.HasReachedEnd(type);
			if (!flag)
			{
				bool isArray = type.IsArray;
				if (isArray)
				{
					bool flag2 = type.GetArrayRank() != 1;
					if (!flag2)
					{
						Type elementType = type.GetElementType();
						IPropertyBag untypedBag = PropertyBag.GetPropertyBag(elementType);
						using (new AutoCompletePathVisitor.VisitedPropertyScope(this.m_VisitContext, 0, elementType))
						{
							if (untypedBag != null)
							{
								untypedBag.Accept(this);
							}
						}
					}
				}
				else
				{
					bool isGenericType = type.IsGenericType;
					if (isGenericType)
					{
						bool flag3 = type.GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)) || type.GetGenericTypeDefinition().IsAssignableFrom(typeof(IList<>));
						if (flag3)
						{
							Type elementType2 = type.GenericTypeArguments[0];
							IPropertyBag untypedBag2 = PropertyBag.GetPropertyBag(elementType2);
							using (new AutoCompletePathVisitor.VisitedPropertyScope(this.m_VisitContext, 0, elementType2))
							{
								if (untypedBag2 != null)
								{
									untypedBag2.Accept(this);
								}
							}
						}
					}
					else
					{
						IPropertyBag bag = PropertyBag.GetPropertyBag(type);
						if (bag != null)
						{
							bag.Accept(this);
						}
					}
				}
			}
		}

		// Token: 0x04001587 RID: 5511
		private AutoCompletePathVisitor.VisitContext m_VisitContext = new AutoCompletePathVisitor.VisitContext();

		// Token: 0x020005E2 RID: 1506
		private class VisitContext
		{
			// Token: 0x17000A9D RID: 2717
			// (get) Token: 0x060028D5 RID: 10453 RVA: 0x000A7FB0 File Offset: 0x000A61B0
			// (set) Token: 0x060028D6 RID: 10454 RVA: 0x000A7FB8 File Offset: 0x000A61B8
			public List<PropertyPathInfo> propertyPathInfos { get; set; }

			// Token: 0x17000A9E RID: 2718
			// (get) Token: 0x060028D7 RID: 10455 RVA: 0x000A7FC1 File Offset: 0x000A61C1
			public HashSet<Type> types { get; } = new HashSet<Type>();

			// Token: 0x17000A9F RID: 2719
			// (get) Token: 0x060028D8 RID: 10456 RVA: 0x000A7FC9 File Offset: 0x000A61C9
			// (set) Token: 0x060028D9 RID: 10457 RVA: 0x000A7FD1 File Offset: 0x000A61D1
			public PropertyPath current { get; set; }

			// Token: 0x17000AA0 RID: 2720
			// (get) Token: 0x060028DA RID: 10458 RVA: 0x000A7FDA File Offset: 0x000A61DA
			// (set) Token: 0x060028DB RID: 10459 RVA: 0x000A7FE2 File Offset: 0x000A61E2
			public int currentDepth { get; set; }
		}

		// Token: 0x020005E3 RID: 1507
		private struct InspectedTypeScope<TContainer> : IDisposable
		{
			// Token: 0x060028DD RID: 10461 RVA: 0x000A7FFF File Offset: 0x000A61FF
			public InspectedTypeScope(AutoCompletePathVisitor.VisitContext context)
			{
				this.m_VisitContext = context;
				this.m_VisitContext.types.Add(typeof(TContainer));
			}

			// Token: 0x060028DE RID: 10462 RVA: 0x000A8024 File Offset: 0x000A6224
			public void Dispose()
			{
				this.m_VisitContext.types.Remove(typeof(TContainer));
			}

			// Token: 0x0400158D RID: 5517
			private AutoCompletePathVisitor.VisitContext m_VisitContext;
		}

		// Token: 0x020005E4 RID: 1508
		private struct VisitedPropertyScope : IDisposable
		{
			// Token: 0x060028DF RID: 10463 RVA: 0x000A8044 File Offset: 0x000A6244
			public VisitedPropertyScope(AutoCompletePathVisitor.VisitContext context, IProperty property)
			{
				this.m_VisitContext = context;
				AutoCompletePathVisitor.VisitContext visitContext = this.m_VisitContext;
				PropertyPath propertyPath = this.m_VisitContext.current;
				visitContext.current = PropertyPath.AppendProperty(in propertyPath, property);
				propertyPath = this.m_VisitContext.current;
				PropertyPathInfo propertyPathInfo = new PropertyPathInfo(in propertyPath, property.DeclaredValueType());
				List<PropertyPathInfo> propertyPathInfos = this.m_VisitContext.propertyPathInfos;
				if (propertyPathInfos != null)
				{
					propertyPathInfos.Add(propertyPathInfo);
				}
				AutoCompletePathVisitor.VisitContext visitContext2 = this.m_VisitContext;
				int currentDepth = visitContext2.currentDepth;
				visitContext2.currentDepth = currentDepth + 1;
			}

			// Token: 0x060028E0 RID: 10464 RVA: 0x000A80C4 File Offset: 0x000A62C4
			public VisitedPropertyScope(AutoCompletePathVisitor.VisitContext context, int index, Type type)
			{
				this.m_VisitContext = context;
				AutoCompletePathVisitor.VisitContext visitContext = this.m_VisitContext;
				PropertyPath propertyPath = this.m_VisitContext.current;
				visitContext.current = PropertyPath.AppendIndex(in propertyPath, index);
				propertyPath = this.m_VisitContext.current;
				PropertyPathInfo propertyPathInfo = new PropertyPathInfo(in propertyPath, type);
				List<PropertyPathInfo> propertyPathInfos = this.m_VisitContext.propertyPathInfos;
				if (propertyPathInfos != null)
				{
					propertyPathInfos.Add(propertyPathInfo);
				}
				AutoCompletePathVisitor.VisitContext visitContext2 = this.m_VisitContext;
				int currentDepth = visitContext2.currentDepth;
				visitContext2.currentDepth = currentDepth + 1;
			}

			// Token: 0x060028E1 RID: 10465 RVA: 0x000A813C File Offset: 0x000A633C
			public void Dispose()
			{
				AutoCompletePathVisitor.VisitContext visitContext = this.m_VisitContext;
				PropertyPath current = this.m_VisitContext.current;
				visitContext.current = PropertyPath.Pop(in current);
				AutoCompletePathVisitor.VisitContext visitContext2 = this.m_VisitContext;
				int currentDepth = visitContext2.currentDepth;
				visitContext2.currentDepth = currentDepth - 1;
			}

			// Token: 0x0400158E RID: 5518
			private AutoCompletePathVisitor.VisitContext m_VisitContext;
		}
	}
}
