using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000096 RID: 150
	public class Button : TextElement
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001B314 File Offset: 0x00019514
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x0001B32C File Offset: 0x0001952C
		public Clickable clickable
		{
			get
			{
				return this.m_Clickable;
			}
			set
			{
				bool flag = this.m_Clickable != null && this.m_Clickable.target == this;
				if (flag)
				{
					this.RemoveManipulator(this.m_Clickable);
				}
				this.m_Clickable = value;
				bool flag2 = this.m_Clickable != null;
				if (flag2)
				{
					this.AddManipulator(this.m_Clickable);
				}
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0001B389 File Offset: 0x00019589
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x0001B394 File Offset: 0x00019594
		[CreateProperty]
		public Background iconImage
		{
			get
			{
				return this.m_IconImage;
			}
			set
			{
				bool flag = (value.IsEmpty() && this.m_ImageElement == null) || value == this.m_IconImage;
				if (!flag)
				{
					bool flag2 = value.IsEmpty();
					if (flag2)
					{
						this.m_IconImage = value;
						this.ResetButtonHierarchy();
						base.NotifyPropertyChanged(in Button.iconImageProperty);
					}
					else
					{
						bool flag3 = this.m_ImageElement == null;
						if (flag3)
						{
							this.UpdateButtonHierarchy();
						}
						bool flag4 = value.texture;
						if (flag4)
						{
							this.m_ImageElement.image = value.texture;
						}
						else
						{
							bool flag5 = value.sprite;
							if (flag5)
							{
								this.m_ImageElement.sprite = value.sprite;
							}
							else
							{
								bool flag6 = value.renderTexture;
								if (flag6)
								{
									this.m_ImageElement.image = value.renderTexture;
								}
								else
								{
									this.m_ImageElement.vectorImage = value.vectorImage;
								}
							}
						}
						this.m_IconImage = value;
						base.EnableInClassList(Button.iconOnlyUssClassName, string.IsNullOrEmpty(this.text));
						base.NotifyPropertyChanged(in Button.iconImageProperty);
					}
				}
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0001B4BA File Offset: 0x000196BA
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x0001B4CC File Offset: 0x000196CC
		public override string text
		{
			get
			{
				return this.m_Text ?? string.Empty;
			}
			set
			{
				this.m_Text = value;
				base.EnableInClassList(Button.iconOnlyUssClassName, !this.m_IconImage.IsEmpty() && string.IsNullOrEmpty(this.text));
				bool flag = this.m_TextElement != null;
				if (flag)
				{
					base.text = string.Empty;
					bool flag2 = this.m_TextElement.text == this.m_Text;
					if (!flag2)
					{
						this.m_TextElement.text = this.m_Text;
					}
				}
				else
				{
					bool flag3 = base.text == this.m_Text;
					if (!flag3)
					{
						base.text = this.m_Text;
					}
				}
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001B578 File Offset: 0x00019778
		public Button()
			: this(default(Background), null)
		{
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001B597 File Offset: 0x00019797
		public Button(Background iconImage, Action clickEvent = null)
			: this(clickEvent)
		{
			this.iconImage = iconImage;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001B5AC File Offset: 0x000197AC
		public Button(Action clickEvent)
		{
			base.AddToClassList(Button.ussClassName);
			this.clickable = new Clickable(clickEvent);
			this.focusable = true;
			base.tabIndex = 0;
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001B609 File Offset: 0x00019809
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			Clickable clickable = this.clickable;
			if (clickable != null)
			{
				clickable.SimulateSingleClick(evt, 100);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001B628 File Offset: 0x00019828
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			string textToMeasure = this.text;
			bool flag = string.IsNullOrEmpty(textToMeasure);
			if (flag)
			{
				textToMeasure = Button.NonEmptyString;
			}
			return base.MeasureTextSize(textToMeasure, desiredWidth, widthMode, desiredHeight, heightMode);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001B660 File Offset: 0x00019860
		private void UpdateButtonHierarchy()
		{
			bool flag = this.m_ImageElement == null;
			if (flag)
			{
				this.m_ImageElement = new Image
				{
					classList = { Button.imageUSSClassName }
				};
				base.Add(this.m_ImageElement);
				base.AddToClassList(Button.iconUssClassName);
			}
			bool flag2 = this.m_TextElement == null;
			if (flag2)
			{
				this.m_TextElement = new TextElement
				{
					text = this.text
				};
				this.m_Text = this.text;
				base.text = string.Empty;
				base.Add(this.m_TextElement);
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001B700 File Offset: 0x00019900
		private void ResetButtonHierarchy()
		{
			bool flag = this.m_ImageElement != null;
			if (flag)
			{
				this.m_ImageElement.RemoveFromHierarchy();
				this.m_ImageElement = null;
				base.RemoveFromClassList(Button.iconUssClassName);
				base.RemoveFromClassList(Button.iconOnlyUssClassName);
			}
			bool flag2 = this.m_TextElement != null;
			if (flag2)
			{
				string restoredText = this.m_TextElement.text;
				this.m_TextElement.RemoveFromHierarchy();
				this.m_TextElement = null;
				this.text = restoredText;
			}
		}

		// Token: 0x04000347 RID: 839
		internal static readonly BindingId iconImageProperty = "iconImage";

		// Token: 0x04000348 RID: 840
		public new static readonly string ussClassName = "unity-button";

		// Token: 0x04000349 RID: 841
		public static readonly string iconUssClassName = Button.ussClassName + "--with-icon";

		// Token: 0x0400034A RID: 842
		public static readonly string iconOnlyUssClassName = Button.ussClassName + "--with-icon-only";

		// Token: 0x0400034B RID: 843
		public static readonly string imageUSSClassName = Button.ussClassName + "__image";

		// Token: 0x0400034C RID: 844
		private Clickable m_Clickable;

		// Token: 0x0400034D RID: 845
		private TextElement m_TextElement;

		// Token: 0x0400034E RID: 846
		private Image m_ImageElement;

		// Token: 0x0400034F RID: 847
		private Background m_IconImage;

		// Token: 0x04000350 RID: 848
		private string m_Text = string.Empty;

		// Token: 0x04000351 RID: 849
		private static readonly string NonEmptyString = " ";

		// Token: 0x02000097 RID: 151
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Button, Button.UxmlTraits>
		{
		}

		// Token: 0x02000098 RID: 152
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextElement.UxmlTraits
		{
			// Token: 0x06000589 RID: 1417 RVA: 0x0001B7F5 File Offset: 0x000199F5
			public UxmlTraits()
			{
				base.focusable.defaultValue = true;
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x0001B824 File Offset: 0x00019A24
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Button button = (Button)ve;
				button.iconImage = this.m_IconImage.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000352 RID: 850
			private readonly UxmlImageAttributeDescription m_IconImage = new UxmlImageAttributeDescription
			{
				name = "icon-image"
			};
		}
	}
}
