using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000018 RID: 24
	public class BindableElement : VisualElement, IBindable
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003252 File Offset: 0x00001452
		public IBinding binding { get; }

		// Token: 0x1700001A RID: 26
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000325A File Offset: 0x0000145A
		public string bindingPath
		{
			[CompilerGenerated]
			set
			{
				this.<bindingPath>k__BackingField = value;
			}
		}

		// Token: 0x0400002D RID: 45
		internal const string k_BindingPathTooltip = "Default method to define a path to a serialized property. Most often used for Editor extensions and inspectors.";

		// Token: 0x02000019 RID: 25
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<BindableElement, BindableElement.UxmlTraits>
		{
		}

		// Token: 0x0200001A RID: 26
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x06000079 RID: 121 RVA: 0x00003275 File Offset: 0x00001475
			public UxmlTraits()
			{
				this.m_PropertyPath = new UxmlStringAttributeDescription
				{
					name = "binding-path"
				};
			}

			// Token: 0x0600007A RID: 122 RVA: 0x00003298 File Offset: 0x00001498
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				string propPath = this.m_PropertyPath.GetValueFromBag(bag, cc);
				bool flag = !string.IsNullOrEmpty(propPath);
				if (flag)
				{
					IBindable field = ve as IBindable;
					bool flag2 = field != null;
					if (flag2)
					{
						field.bindingPath = propPath;
					}
				}
			}

			// Token: 0x04000030 RID: 48
			private UxmlStringAttributeDescription m_PropertyPath;
		}
	}
}
