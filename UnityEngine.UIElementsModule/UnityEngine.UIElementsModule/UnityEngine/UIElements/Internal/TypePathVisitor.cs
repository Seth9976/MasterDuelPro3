using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Properties;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x020005E5 RID: 1509
	internal class TypePathVisitor : ITypeVisitor, IPropertyBagVisitor, IPropertyVisitor
	{
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060028E2 RID: 10466 RVA: 0x000A817F File Offset: 0x000A637F
		// (set) Token: 0x060028E3 RID: 10467 RVA: 0x000A8187 File Offset: 0x000A6387
		public PropertyPath Path { get; set; }

		// Token: 0x17000AA2 RID: 2722
		// (set) Token: 0x060028E4 RID: 10468 RVA: 0x000A8190 File Offset: 0x000A6390
		private Type resolvedType
		{
			[CompilerGenerated]
			set
			{
				this.<resolvedType>k__BackingField = value;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x000A8199 File Offset: 0x000A6399
		// (set) Token: 0x060028E6 RID: 10470 RVA: 0x000A81A1 File Offset: 0x000A63A1
		public VisitReturnCode ReturnCode { get; internal set; }

		// Token: 0x060028E7 RID: 10471 RVA: 0x000A81AC File Offset: 0x000A63AC
		public void Reset()
		{
			this.resolvedType = null;
			this.m_LastType = null;
			this.Path = default(PropertyPath);
			this.ReturnCode = VisitReturnCode.Ok;
			this.m_PathIndex = 0;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000A81E8 File Offset: 0x000A63E8
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
					foreach (IProperty<TContainer> p in properties.GetProperties())
					{
						bool flag2 = p.Name == part.Name;
						if (flag2)
						{
							Type propertyType = (this.m_LastType = p.DeclaredValueType());
							IPropertyBag untypedBag = PropertyBag.GetPropertyBag(propertyType);
							if (untypedBag != null)
							{
								untypedBag.Accept(this);
							}
							return;
						}
					}
					this.ReturnCode = VisitReturnCode.InvalidPath;
				}
				return;
			}
			case PropertyPathPartKind.Index:
			{
				IIndexedProperties<TContainer> indexable = properties as IIndexedProperties<TContainer>;
				IProperty<TContainer> property;
				bool flag3 = indexable != null && indexable.TryGetProperty(ref container, part.Index, out property);
				if (flag3)
				{
					property.Accept(this, ref container);
				}
				else
				{
					Type elementType = TypePathVisitor.GetElementType(typeof(TContainer));
					bool flag4 = elementType != null;
					if (flag4)
					{
						IPropertyBag untypedBag2 = PropertyBag.GetPropertyBag(elementType);
						if (untypedBag2 != null)
						{
							untypedBag2.Accept(this);
						}
					}
					else
					{
						this.ReturnCode = VisitReturnCode.InvalidPath;
					}
				}
				return;
			}
			}
			this.ReturnCode = VisitReturnCode.InvalidPath;
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000A83A0 File Offset: 0x000A65A0
		void IPropertyVisitor.Visit<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container)
		{
			TValue value = property.GetValue(ref container);
			bool flag = this.m_PathIndex >= this.Path.Length;
			if (flag)
			{
				this.resolvedType = property.DeclaredValueType();
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
						IPropertyBag untypedBag = PropertyBag.GetPropertyBag(property.DeclaredValueType());
						if (untypedBag != null)
						{
							untypedBag.Accept(this);
						}
					}
					else
					{
						PropertyContainer.Accept<TValue>(this, ref value, default(VisitParameters));
					}
				}
				else
				{
					this.ReturnCode = VisitReturnCode.InvalidPath;
				}
			}
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000A8458 File Offset: 0x000A6658
		void ITypeVisitor.Visit<TContainer>()
		{
			bool flag = this.IsLastPartReached();
			if (!flag)
			{
				PropertyPath propertyPath = this.Path;
				int num = this.m_PathIndex;
				this.m_PathIndex = num + 1;
				PropertyPathPart part = propertyPath[num];
				this.m_LastType = null;
				PropertyPathPartKind kind = part.Kind;
				PropertyPathPartKind propertyPathPartKind = kind;
				if (propertyPathPartKind != PropertyPathPartKind.Name)
				{
					if (propertyPathPartKind == PropertyPathPartKind.Index)
					{
						Type type = typeof(TContainer);
						Type elementType = TypePathVisitor.GetElementType(type);
						bool flag2 = elementType != null;
						if (flag2)
						{
							this.m_LastType = elementType;
							IPropertyBag untypedBag = PropertyBag.GetPropertyBag(elementType);
							bool flag3 = untypedBag != null;
							if (flag3)
							{
								untypedBag.Accept(this);
								return;
							}
						}
					}
				}
				else
				{
					IPropertyBag<TContainer> propertyBag = PropertyBag.GetPropertyBag<TContainer>();
					bool flag4 = propertyBag == null;
					if (flag4)
					{
						return;
					}
					foreach (IProperty<TContainer> prop in propertyBag.GetProperties())
					{
						bool flag5 = prop.Name != part.Name;
						if (!flag5)
						{
							Type type2 = (this.m_LastType = prop.DeclaredValueType());
							IPropertyBag bag = PropertyBag.GetPropertyBag(type2);
							bool flag6 = bag != null;
							if (flag6)
							{
								bag.Accept(this);
								return;
							}
							Type elementType2 = TypePathVisitor.GetElementType(type2);
							bool flag7 = elementType2 != null;
							if (flag7)
							{
								bool flag8 = this.IsLastPartReached();
								if (flag8)
								{
									return;
								}
								propertyPath = this.Path;
								num = this.m_PathIndex;
								this.m_PathIndex = num + 1;
								bool isIndex = propertyPath[num].IsIndex;
								if (isIndex)
								{
									this.m_LastType = elementType2;
									IPropertyBag untypedBag2 = PropertyBag.GetPropertyBag(elementType2);
									if (untypedBag2 != null)
									{
										untypedBag2.Accept(this);
									}
									return;
								}
							}
							break;
						}
					}
				}
				bool flag9 = this.IsLastPartReached();
				if (!flag9)
				{
					bool flag10 = this.ReturnCode == VisitReturnCode.Ok;
					if (flag10)
					{
						this.ReturnCode = VisitReturnCode.InvalidPath;
					}
				}
			}
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x000A8674 File Offset: 0x000A6874
		private bool IsLastPartReached()
		{
			bool flag = this.m_PathIndex < this.Path.Length;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.m_LastType == null;
				if (flag3)
				{
					this.ReturnCode = VisitReturnCode.InvalidPath;
				}
				this.resolvedType = this.m_LastType;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x000A86D0 File Offset: 0x000A68D0
		private static Type GetElementType(Type type)
		{
			Type elementType = null;
			bool flag = type.IsArray && type.GetArrayRank() == 1;
			if (flag)
			{
				elementType = type.GetElementType();
			}
			else
			{
				bool flag2 = type.IsGenericType && (type.GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)) || type.GetGenericTypeDefinition().IsAssignableFrom(typeof(IList<>)));
				if (flag2)
				{
					elementType = type.GenericTypeArguments[0];
				}
			}
			return elementType;
		}

		// Token: 0x04001592 RID: 5522
		private Type m_LastType;

		// Token: 0x04001593 RID: 5523
		private int m_PathIndex;
	}
}
