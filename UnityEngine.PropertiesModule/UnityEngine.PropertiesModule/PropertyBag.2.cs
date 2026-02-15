using System;
using Unity.Properties.Internal;

namespace Unity.Properties
{
	// Token: 0x02000041 RID: 65
	public abstract class PropertyBag<TContainer> : IPropertyBag<TContainer>, IPropertyBag, IPropertyBagRegister, IConstructor<TContainer>, IConstructor
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00004E88 File Offset: 0x00003088
		static PropertyBag()
		{
			bool flag = !TypeTraits.IsContainer(typeof(TContainer));
			if (flag)
			{
				throw new InvalidOperationException(string.Format("Failed to create a property bag for Type=[{0}]. The type is not a valid container type.", typeof(TContainer)));
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004EC7 File Offset: 0x000030C7
		void IPropertyBagRegister.Register()
		{
			PropertyBagStore.AddPropertyBag<TContainer>(this);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004ED4 File Offset: 0x000030D4
		public void Accept(ITypeVisitor visitor)
		{
			bool flag = visitor == null;
			if (flag)
			{
				throw new ArgumentNullException("visitor");
			}
			visitor.Visit<TContainer>();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004F00 File Offset: 0x00003100
		void IPropertyBag.Accept(IPropertyBagVisitor visitor, ref object container)
		{
			bool flag = container == null;
			if (flag)
			{
				throw new ArgumentNullException("container");
			}
			object obj = container;
			TContainer typedContainer;
			int num;
			if (obj is TContainer)
			{
				typedContainer = (TContainer)((object)obj);
				num = 1;
			}
			else
			{
				num = 0;
			}
			bool flag2 = num == 0;
			if (flag2)
			{
				throw new ArgumentException(string.Format("The given ContainerType=[{0}] does not match the PropertyBagType=[{1}]", container.GetType(), typeof(TContainer)));
			}
			PropertyBag.AcceptWithSpecializedVisitor<TContainer>(this, visitor, ref typedContainer);
			container = typedContainer;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004F74 File Offset: 0x00003174
		void IPropertyBag<TContainer>.Accept(IPropertyBagVisitor visitor, ref TContainer container)
		{
			visitor.Visit<TContainer>(this, ref container);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004F80 File Offset: 0x00003180
		PropertyCollection<TContainer> IPropertyBag<TContainer>.GetProperties()
		{
			return this.GetProperties();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004F98 File Offset: 0x00003198
		PropertyCollection<TContainer> IPropertyBag<TContainer>.GetProperties(ref TContainer container)
		{
			return this.GetProperties(ref container);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004FB1 File Offset: 0x000031B1
		InstantiationKind IConstructor.InstantiationKind
		{
			get
			{
				return this.InstantiationKind;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004FBC File Offset: 0x000031BC
		TContainer IConstructor<TContainer>.Instantiate()
		{
			return this.Instantiate();
		}

		// Token: 0x060000FF RID: 255
		public abstract PropertyCollection<TContainer> GetProperties();

		// Token: 0x06000100 RID: 256
		public abstract PropertyCollection<TContainer> GetProperties(ref TContainer container);

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004FD4 File Offset: 0x000031D4
		protected virtual InstantiationKind InstantiationKind { get; } = InstantiationKind.Activator;

		// Token: 0x06000102 RID: 258 RVA: 0x00004FDC File Offset: 0x000031DC
		protected virtual TContainer Instantiate()
		{
			return default(TContainer);
		}
	}
}
