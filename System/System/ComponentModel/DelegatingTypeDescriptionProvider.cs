using System;
using System.Collections;

namespace System.ComponentModel
{
	// Token: 0x0200026D RID: 621
	internal sealed class DelegatingTypeDescriptionProvider : TypeDescriptionProvider
	{
		// Token: 0x06000EB1 RID: 3761 RVA: 0x0004121F File Offset: 0x0003F41F
		internal DelegatingTypeDescriptionProvider(Type type)
		{
			this._type = type;
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0004122E File Offset: 0x0003F42E
		internal TypeDescriptionProvider Provider
		{
			get
			{
				return TypeDescriptor.GetProviderRecursive(this._type);
			}
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0004123B File Offset: 0x0003F43B
		public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			return this.Provider.CreateInstance(provider, objectType, argTypes, args);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0004124D File Offset: 0x0003F44D
		public override IDictionary GetCache(object instance)
		{
			return this.Provider.GetCache(instance);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0004125B File Offset: 0x0003F45B
		public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			return this.Provider.GetExtendedTypeDescriptor(instance);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00041269 File Offset: 0x0003F469
		protected internal override IExtenderProvider[] GetExtenderProviders(object instance)
		{
			return this.Provider.GetExtenderProviders(instance);
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00041277 File Offset: 0x0003F477
		public override Type GetReflectionType(Type objectType, object instance)
		{
			return this.Provider.GetReflectionType(objectType, instance);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00041286 File Offset: 0x0003F486
		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			return this.Provider.GetTypeDescriptor(objectType, instance);
		}

		// Token: 0x040009DD RID: 2525
		private readonly Type _type;
	}
}
