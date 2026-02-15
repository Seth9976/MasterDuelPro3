using System;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000483 RID: 1155
	internal class ConstructionCallDictionary : MessageDictionary
	{
		// Token: 0x06002544 RID: 9540 RVA: 0x00098081 File Offset: 0x00096281
		public ConstructionCallDictionary(IConstructionCallMessage message)
			: base(message)
		{
			base.MethodKeys = ConstructionCallDictionary.InternalKeys;
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x00098098 File Offset: 0x00096298
		protected override object GetMethodProperty(string key)
		{
			if (key == "__Activator")
			{
				return ((IConstructionCallMessage)this._message).Activator;
			}
			if (key == "__CallSiteActivationAttributes")
			{
				return ((IConstructionCallMessage)this._message).CallSiteActivationAttributes;
			}
			if (key == "__ActivationType")
			{
				return ((IConstructionCallMessage)this._message).ActivationType;
			}
			if (key == "__ContextProperties")
			{
				return ((IConstructionCallMessage)this._message).ContextProperties;
			}
			if (!(key == "__ActivationTypeName"))
			{
				return base.GetMethodProperty(key);
			}
			return ((IConstructionCallMessage)this._message).ActivationTypeName;
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x00098144 File Offset: 0x00096344
		protected override void SetMethodProperty(string key, object value)
		{
			if (key == "__Activator")
			{
				((IConstructionCallMessage)this._message).Activator = (IActivator)value;
				return;
			}
			if (!(key == "__CallSiteActivationAttributes") && !(key == "__ActivationType") && !(key == "__ContextProperties") && !(key == "__ActivationTypeName"))
			{
				base.SetMethodProperty(key, value);
				return;
			}
			throw new ArgumentException("key was invalid");
		}

		// Token: 0x040011F2 RID: 4594
		public static string[] InternalKeys = new string[]
		{
			"__Uri", "__MethodName", "__TypeName", "__MethodSignature", "__Args", "__CallContext", "__CallSiteActivationAttributes", "__ActivationType", "__ContextProperties", "__Activator",
			"__ActivationTypeName"
		};
	}
}
