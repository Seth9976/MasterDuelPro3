using System;
using System.Collections.Generic;
using Unity.Properties.Internal;
using UnityEngine.Pool;

namespace Unity.Properties
{
	// Token: 0x02000004 RID: 4
	public static class PropertyContainer
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public static void Accept<TContainer>(IPropertyBagVisitor visitor, ref TContainer container, VisitParameters parameters = default(VisitParameters))
		{
			VisitReturnCode returnCode = VisitReturnCode.Ok;
			try
			{
				bool flag = PropertyContainer.TryAccept<TContainer>(visitor, ref container, out returnCode, parameters);
				if (flag)
				{
					return;
				}
			}
			catch (Exception)
			{
				bool flag2 = (parameters.IgnoreExceptions & VisitExceptionKind.Visitor) == VisitExceptionKind.None;
				if (flag2)
				{
					throw;
				}
			}
			bool flag3 = (parameters.IgnoreExceptions & VisitExceptionKind.Internal) > VisitExceptionKind.None;
			if (!flag3)
			{
				switch (returnCode)
				{
				case VisitReturnCode.Ok:
				case VisitReturnCode.InvalidContainerType:
					break;
				case VisitReturnCode.NullContainer:
					throw new ArgumentException("The given container was null. Visitation only works for valid non-null containers.");
				case VisitReturnCode.MissingPropertyBag:
					throw new MissingPropertyBagException(container.GetType());
				default:
					throw new Exception(string.Format("Unexpected {0}=[{1}]", "VisitReturnCode", returnCode));
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002114 File Offset: 0x00000314
		public static bool TryAccept<TContainer>(IPropertyBagVisitor visitor, ref TContainer container, VisitParameters parameters = default(VisitParameters))
		{
			VisitReturnCode visitReturnCode;
			return PropertyContainer.TryAccept<TContainer>(visitor, ref container, out visitReturnCode, parameters);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002130 File Offset: 0x00000330
		public static bool TryAccept<TContainer>(IPropertyBagVisitor visitor, ref TContainer container, out VisitReturnCode returnCode, VisitParameters parameters = default(VisitParameters))
		{
			bool flag = !TypeTraits<TContainer>.IsContainer;
			bool flag2;
			if (flag)
			{
				returnCode = VisitReturnCode.InvalidContainerType;
				flag2 = false;
			}
			else
			{
				bool canBeNull = TypeTraits<TContainer>.CanBeNull;
				if (canBeNull)
				{
					bool flag3 = EqualityComparer<TContainer>.Default.Equals(container, default(TContainer));
					if (flag3)
					{
						returnCode = VisitReturnCode.NullContainer;
						return false;
					}
				}
				bool flag4 = !TypeTraits<TContainer>.IsValueType && typeof(TContainer) != container.GetType();
				if (flag4)
				{
					bool flag5 = !TypeTraits.IsContainer(container.GetType());
					if (flag5)
					{
						returnCode = VisitReturnCode.InvalidContainerType;
						return false;
					}
					IPropertyBag properties = PropertyBagStore.GetPropertyBag(container.GetType());
					bool flag6 = properties == null;
					if (flag6)
					{
						returnCode = VisitReturnCode.MissingPropertyBag;
						return false;
					}
					object boxed = container;
					properties.Accept(visitor, ref boxed);
					container = (TContainer)((object)boxed);
				}
				else
				{
					IPropertyBag<TContainer> properties2 = PropertyBagStore.GetPropertyBag<TContainer>();
					bool flag7 = properties2 == null;
					if (flag7)
					{
						returnCode = VisitReturnCode.MissingPropertyBag;
						return false;
					}
					PropertyBag.AcceptWithSpecializedVisitor<TContainer>(properties2, visitor, ref container);
				}
				returnCode = VisitReturnCode.Ok;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002254 File Offset: 0x00000454
		public static bool TryGetProperty<TContainer>(ref TContainer container, in PropertyPath path, out IProperty property)
		{
			VisitReturnCode visitReturnCode;
			return PropertyContainer.TryGetProperty<TContainer>(ref container, in path, out property, out visitReturnCode);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000226C File Offset: 0x0000046C
		public static bool TryGetProperty<TContainer>(ref TContainer container, in PropertyPath path, out IProperty property, out VisitReturnCode returnCode)
		{
			PropertyContainer.GetPropertyVisitor getPropertyVisitor = PropertyContainer.GetPropertyVisitor.Pool.Get();
			bool flag2;
			try
			{
				getPropertyVisitor.Path = path;
				bool flag = !PropertyContainer.TryAccept<TContainer>(getPropertyVisitor, ref container, out returnCode, default(VisitParameters));
				if (flag)
				{
					property = null;
					flag2 = false;
				}
				else
				{
					returnCode = getPropertyVisitor.ReturnCode;
					property = getPropertyVisitor.Property;
					flag2 = returnCode == VisitReturnCode.Ok;
				}
			}
			finally
			{
				PropertyContainer.GetPropertyVisitor.Pool.Release(getPropertyVisitor);
			}
			return flag2;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022EC File Offset: 0x000004EC
		public static bool TryGetValue<TContainer, TValue>(ref TContainer container, string name, out TValue value)
		{
			PropertyPath path = new PropertyPath(name);
			VisitReturnCode visitReturnCode;
			return PropertyContainer.TryGetValue<TContainer, TValue>(ref container, in path, out value, out visitReturnCode);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002314 File Offset: 0x00000514
		public static bool TryGetValue<TContainer, TValue>(ref TContainer container, in PropertyPath path, out TValue value, out VisitReturnCode returnCode)
		{
			bool isEmpty = path.IsEmpty;
			bool flag;
			if (isEmpty)
			{
				returnCode = VisitReturnCode.InvalidPath;
				value = default(TValue);
				flag = false;
			}
			else
			{
				PropertyContainer.GetValueVisitor<TValue> visitor = PropertyContainer.GetValueVisitor<TValue>.Pool.Get();
				visitor.Path = path;
				visitor.ReadonlyVisit = true;
				try
				{
					bool flag2 = !PropertyContainer.TryAccept<TContainer>(visitor, ref container, out returnCode, default(VisitParameters));
					if (flag2)
					{
						value = default(TValue);
						return false;
					}
					value = visitor.Value;
					returnCode = visitor.ReturnCode;
				}
				finally
				{
					PropertyContainer.GetValueVisitor<TValue>.Pool.Release(visitor);
				}
				flag = returnCode == VisitReturnCode.Ok;
			}
			return flag;
		}

		// Token: 0x02000005 RID: 5
		private class GetPropertyVisitor : PathVisitor
		{
			// Token: 0x06000009 RID: 9 RVA: 0x000023C0 File Offset: 0x000005C0
			public override void Reset()
			{
				base.Reset();
				this.Property = null;
				base.ReadonlyVisit = true;
			}

			// Token: 0x0600000A RID: 10 RVA: 0x000023D9 File Offset: 0x000005D9
			protected override void VisitPath<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
			{
				this.Property = property;
			}

			// Token: 0x04000007 RID: 7
			public static readonly ObjectPool<PropertyContainer.GetPropertyVisitor> Pool = new ObjectPool<PropertyContainer.GetPropertyVisitor>(() => new PropertyContainer.GetPropertyVisitor(), null, delegate(PropertyContainer.GetPropertyVisitor v)
			{
				v.Reset();
			}, null, true, 10, 10000);

			// Token: 0x04000008 RID: 8
			public IProperty Property;
		}

		// Token: 0x02000007 RID: 7
		private class GetValueVisitor<TSrcValue> : PathVisitor
		{
			// Token: 0x06000011 RID: 17 RVA: 0x00002447 File Offset: 0x00000647
			public override void Reset()
			{
				base.Reset();
				this.Value = default(TSrcValue);
				base.ReadonlyVisit = true;
			}

			// Token: 0x06000012 RID: 18 RVA: 0x00002468 File Offset: 0x00000668
			protected override void VisitPath<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
			{
				bool flag = !TypeConversion.TryConvert<TValue, TSrcValue>(ref value, out this.Value);
				if (flag)
				{
					base.ReturnCode = VisitReturnCode.InvalidCast;
				}
			}

			// Token: 0x0400000A RID: 10
			public static readonly ObjectPool<PropertyContainer.GetValueVisitor<TSrcValue>> Pool = new ObjectPool<PropertyContainer.GetValueVisitor<TSrcValue>>(() => new PropertyContainer.GetValueVisitor<TSrcValue>(), null, delegate(PropertyContainer.GetValueVisitor<TSrcValue> v)
			{
				v.Reset();
			}, null, true, 10, 10000);

			// Token: 0x0400000B RID: 11
			public TSrcValue Value;
		}
	}
}
