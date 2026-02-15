using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000441 RID: 1089
	[UxmlElement("Instance")]
	[HideInInspector]
	public class TemplateContainer : BindableElement
	{
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x00072322 File Offset: 0x00070522
		// (set) Token: 0x06001F5B RID: 8027 RVA: 0x0007232A File Offset: 0x0007052A
		[CreateProperty(ReadOnly = true)]
		public string templateId { get; private set; }

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001F5C RID: 8028 RVA: 0x00072333 File Offset: 0x00070533
		// (set) Token: 0x06001F5D RID: 8029 RVA: 0x0007233B File Offset: 0x0007053B
		[CreateProperty(ReadOnly = true)]
		public VisualTreeAsset templateSource
		{
			get
			{
				return this.m_TemplateSource;
			}
			internal set
			{
				this.m_TemplateSource = value;
			}
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00072344 File Offset: 0x00070544
		public TemplateContainer()
			: this(null)
		{
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0007234F File Offset: 0x0007054F
		public TemplateContainer(string templateId)
			: this(templateId, null)
		{
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x0007235B File Offset: 0x0007055B
		internal TemplateContainer(string templateId, VisualTreeAsset templateSource)
		{
			this.templateId = templateId;
			this.templateSource = templateSource;
			this.m_ContentContainer = this;
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x0007237C File Offset: 0x0007057C
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x00072394 File Offset: 0x00070594
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void SetContentContainer(VisualElement content)
		{
			this.m_ContentContainer = content;
		}

		// Token: 0x04000DE9 RID: 3561
		internal static readonly BindingId templateIdProperty = "templateId";

		// Token: 0x04000DEA RID: 3562
		internal static readonly BindingId templateSourceProperty = "templateSource";

		// Token: 0x04000DEC RID: 3564
		private VisualElement m_ContentContainer;

		// Token: 0x04000DED RID: 3565
		private VisualTreeAsset m_TemplateSource;

		// Token: 0x02000442 RID: 1090
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<TemplateContainer, TemplateContainer.UxmlTraits>
		{
			// Token: 0x17000877 RID: 2167
			// (get) Token: 0x06001F64 RID: 8036 RVA: 0x000723BE File Offset: 0x000705BE
			public override string uxmlName
			{
				get
				{
					return "Instance";
				}
			}

			// Token: 0x17000878 RID: 2168
			// (get) Token: 0x06001F65 RID: 8037 RVA: 0x000723C5 File Offset: 0x000705C5
			public override string uxmlQualifiedName
			{
				get
				{
					return this.uxmlNamespace + "." + this.uxmlName;
				}
			}
		}

		// Token: 0x02000443 RID: 1091
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x06001F67 RID: 8039 RVA: 0x000723E8 File Offset: 0x000705E8
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TemplateContainer templateContainer = (TemplateContainer)ve;
				templateContainer.templateId = this.m_Template.GetValueFromBag(bag, cc);
				VisualTreeAsset visualTreeAsset = cc.visualTreeAsset;
				VisualTreeAsset vta = ((visualTreeAsset != null) ? visualTreeAsset.ResolveTemplate(templateContainer.templateId) : null);
				bool flag = vta == null;
				if (flag)
				{
					templateContainer.Add(new Label(string.Format("Unknown Template: '{0}'", templateContainer.templateId)));
				}
				else
				{
					TemplateAsset templateAsset = bag as TemplateAsset;
					List<TemplateAsset.AttributeOverride> bagOverrides = ((templateAsset != null) ? templateAsset.attributeOverrides : null);
					List<CreationContext.AttributeOverrideRange> contextOverrides = cc.attributeOverrides;
					bool flag2 = bagOverrides != null;
					if (flag2)
					{
						bool flag3 = contextOverrides == null;
						if (flag3)
						{
							contextOverrides = new List<CreationContext.AttributeOverrideRange>();
						}
						contextOverrides.Add(new CreationContext.AttributeOverrideRange(cc.visualTreeAsset, bagOverrides));
					}
					templateContainer.templateSource = vta;
					vta.CloneTree(ve, new CreationContext(cc.slotInsertionPoints, contextOverrides));
				}
				bool flag4 = vta == null;
				if (flag4)
				{
					Debug.LogErrorFormat("Could not resolve template with name '{0}'", new object[] { templateContainer.templateId });
				}
			}

			// Token: 0x04000DEE RID: 3566
			private UxmlStringAttributeDescription m_Template = new UxmlStringAttributeDescription
			{
				name = "template",
				use = UxmlAttributeDescription.Use.Required
			};
		}
	}
}
