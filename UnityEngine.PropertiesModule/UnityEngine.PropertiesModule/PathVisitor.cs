using System;
using System.Collections.Generic;
using Unity.Properties.Internal;

namespace Unity.Properties
{
	// Token: 0x02000057 RID: 87
	public abstract class PathVisitor : IPropertyBagVisitor, IPropertyVisitor
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000055C9 File Offset: 0x000037C9
		// (set) Token: 0x0600013D RID: 317 RVA: 0x000055D1 File Offset: 0x000037D1
		public PropertyPath Path { get; set; }

		// Token: 0x0600013E RID: 318 RVA: 0x000055DC File Offset: 0x000037DC
		public virtual void Reset()
		{
			this.m_PathIndex = 0;
			this.Path = default(PropertyPath);
			this.ReturnCode = VisitReturnCode.Ok;
			this.ReadonlyVisit = false;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005611 File Offset: 0x00003811
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00005619 File Offset: 0x00003819
		private IProperty Property { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005622 File Offset: 0x00003822
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000562A File Offset: 0x0000382A
		public bool ReadonlyVisit { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005633 File Offset: 0x00003833
		// (set) Token: 0x06000144 RID: 324 RVA: 0x0000563B File Offset: 0x0000383B
		public VisitReturnCode ReturnCode { get; protected set; }

		// Token: 0x06000145 RID: 325 RVA: 0x00005644 File Offset: 0x00003844
		void IPropertyBagVisitor.Visit<TContainer>(IPropertyBag<TContainer> properties, ref TContainer container)
		{
			PropertyPath path = this.Path;
			int pathIndex = this.m_PathIndex;
			this.m_PathIndex = pathIndex + 1;
			PropertyPathPart part = path[pathIndex];
			switch (part.Kind)
			{
			case PropertyPathPartKind.Name:
			{
				INamedProperties<TContainer> named = properties as INamedProperties<TContainer>;
				IProperty<TContainer> property;
				bool flag = named != null && named.TryGetProperty(ref container, part.Name, out property);
				if (flag)
				{
					property.Accept(this, ref container);
				}
				else
				{
					this.ReturnCode = VisitReturnCode.InvalidPath;
				}
				break;
			}
			case PropertyPathPartKind.Index:
			{
				IIndexedProperties<TContainer> indexable = properties as IIndexedProperties<TContainer>;
				IProperty<TContainer> property;
				bool flag2 = indexable != null && indexable.TryGetProperty(ref container, part.Index, out property);
				if (flag2)
				{
					using ((property as IAttributes).CreateAttributesScope(this.Property as IAttributes))
					{
						property.Accept(this, ref container);
					}
				}
				else
				{
					this.ReturnCode = VisitReturnCode.InvalidPath;
				}
				break;
			}
			case PropertyPathPartKind.Key:
			{
				IKeyedProperties<TContainer, object> keyable = properties as IKeyedProperties<TContainer, object>;
				IProperty<TContainer> property;
				bool flag3 = keyable != null && keyable.TryGetProperty(ref container, part.Key, out property);
				if (flag3)
				{
					using ((property as IAttributes).CreateAttributesScope(this.Property as IAttributes))
					{
						property.Accept(this, ref container);
					}
				}
				else
				{
					this.ReturnCode = VisitReturnCode.InvalidPath;
				}
				break;
			}
			default:
				this.ReturnCode = VisitReturnCode.InvalidPath;
				break;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000057DC File Offset: 0x000039DC
		void IPropertyVisitor.Visit<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container)
		{
			TValue value = property.GetValue(ref container);
			bool flag = this.m_PathIndex >= this.Path.Length;
			if (flag)
			{
				this.VisitPath<TContainer, TValue>(property, ref container, ref value);
			}
			else
			{
				IPropertyBag propertyBag;
				bool flag2 = PropertyBag.TryGetPropertyBagForValue<TValue>(ref value, out propertyBag);
				if (flag2)
				{
					bool flag3 = TypeTraits<TValue>.CanBeNull && EqualityComparer<TValue>.Default.Equals(value, default(TValue));
					if (flag3)
					{
						this.ReturnCode = VisitReturnCode.InvalidPath;
					}
					else
					{
						using (new PathVisitor.PropertyScope(this, property))
						{
							PropertyContainer.Accept<TValue>(this, ref value, default(VisitParameters));
						}
						bool flag4 = !property.IsReadOnly && !this.ReadonlyVisit;
						if (flag4)
						{
							property.SetValue(ref container, value);
						}
					}
				}
				else
				{
					this.ReturnCode = VisitReturnCode.InvalidPath;
				}
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000467B File Offset: 0x0000287B
		protected virtual void VisitPath<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
		{
		}

		// Token: 0x04000079 RID: 121
		private int m_PathIndex;

		// Token: 0x02000058 RID: 88
		private readonly struct PropertyScope : IDisposable
		{
			// Token: 0x06000149 RID: 329 RVA: 0x000058D4 File Offset: 0x00003AD4
			public PropertyScope(PathVisitor visitor, IProperty property)
			{
				this.m_Visitor = visitor;
				this.m_Property = this.m_Visitor.Property;
				this.m_Visitor.Property = property;
			}

			// Token: 0x0600014A RID: 330 RVA: 0x000058FC File Offset: 0x00003AFC
			public void Dispose()
			{
				this.m_Visitor.Property = this.m_Property;
			}

			// Token: 0x0400007E RID: 126
			private readonly PathVisitor m_Visitor;

			// Token: 0x0400007F RID: 127
			private readonly IProperty m_Property;
		}
	}
}
