using System;
using System.Configuration;
using System.Reflection;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for authentication module configuration elements. This class cannot be inherited.</summary>
	// Token: 0x02000485 RID: 1157
	[ConfigurationCollection(typeof(AuthenticationModuleElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	[DefaultMember("Item")]
	public sealed class AuthenticationModuleElementCollection : ConfigurationElementCollection
	{
		// Token: 0x06001C7A RID: 7290 RVA: 0x0007C8B1 File Offset: 0x0007AAB1
		protected override ConfigurationElement CreateNewElement()
		{
			return new AuthenticationModuleElement();
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0007C8B8 File Offset: 0x0007AAB8
		[MonoTODO("argument exception?")]
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is AuthenticationModuleElement))
			{
				throw new ArgumentException("element");
			}
			return ((AuthenticationModuleElement)element).Type;
		}
	}
}
