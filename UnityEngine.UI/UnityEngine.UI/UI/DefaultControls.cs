using System;

namespace UnityEngine.UI
{
	// Token: 0x02000011 RID: 17
	public static class DefaultControls
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002CD6 File Offset: 0x00000ED6
		public static DefaultControls.IFactoryControls factory
		{
			get
			{
				return DefaultControls.m_CurrentFactory;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002CDD File Offset: 0x00000EDD
		private static GameObject CreateUIElementRoot(string name, Vector2 size, params Type[] components)
		{
			GameObject gameObject = DefaultControls.factory.CreateGameObject(name, components);
			gameObject.GetComponent<RectTransform>().sizeDelta = size;
			return gameObject;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002CF7 File Offset: 0x00000EF7
		private static GameObject CreateUIObject(string name, GameObject parent, params Type[] components)
		{
			GameObject gameObject = DefaultControls.factory.CreateGameObject(name, components);
			DefaultControls.SetParentAndAlign(gameObject, parent);
			return gameObject;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002D0C File Offset: 0x00000F0C
		private static void SetDefaultTextValues(Text lbl)
		{
			lbl.color = DefaultControls.s_TextColor;
			if (lbl.font == null)
			{
				lbl.AssignDefaultFont();
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002D30 File Offset: 0x00000F30
		private static void SetDefaultColorTransitionValues(Selectable slider)
		{
			ColorBlock colors = slider.colors;
			colors.highlightedColor = new Color(0.882f, 0.882f, 0.882f);
			colors.pressedColor = new Color(0.698f, 0.698f, 0.698f);
			colors.disabledColor = new Color(0.521f, 0.521f, 0.521f);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D95 File Offset: 0x00000F95
		private static void SetParentAndAlign(GameObject child, GameObject parent)
		{
			if (parent == null)
			{
				return;
			}
			child.transform.SetParent(parent.transform, false);
			DefaultControls.SetLayerRecursively(child, parent.layer);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002DC0 File Offset: 0x00000FC0
		private static void SetLayerRecursively(GameObject go, int layer)
		{
			go.layer = layer;
			Transform t = go.transform;
			for (int i = 0; i < t.childCount; i++)
			{
				DefaultControls.SetLayerRecursively(t.GetChild(i).gameObject, layer);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E00 File Offset: 0x00001000
		public static GameObject CreatePanel(DefaultControls.Resources resources)
		{
			GameObject gameObject = DefaultControls.CreateUIElementRoot("Panel", DefaultControls.s_ThickElementSize, new Type[] { typeof(Image) });
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.anchorMin = Vector2.zero;
			component.anchorMax = Vector2.one;
			component.anchoredPosition = Vector2.zero;
			component.sizeDelta = Vector2.zero;
			Image component2 = gameObject.GetComponent<Image>();
			component2.sprite = resources.background;
			component2.type = Image.Type.Sliced;
			component2.color = DefaultControls.s_PanelColor;
			return gameObject;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E84 File Offset: 0x00001084
		public static GameObject CreateButton(DefaultControls.Resources resources)
		{
			GameObject buttonRoot = DefaultControls.CreateUIElementRoot("Button (Legacy)", DefaultControls.s_ThickElementSize, new Type[]
			{
				typeof(Image),
				typeof(Button)
			});
			GameObject gameObject = DefaultControls.CreateUIObject("Text (Legacy)", buttonRoot, new Type[] { typeof(Text) });
			Image component = buttonRoot.GetComponent<Image>();
			component.sprite = resources.standard;
			component.type = Image.Type.Sliced;
			component.color = DefaultControls.s_DefaultSelectableColor;
			DefaultControls.SetDefaultColorTransitionValues(buttonRoot.GetComponent<Button>());
			Text component2 = gameObject.GetComponent<Text>();
			component2.text = "Button";
			component2.alignment = TextAnchor.MiddleCenter;
			DefaultControls.SetDefaultTextValues(component2);
			RectTransform component3 = gameObject.GetComponent<RectTransform>();
			component3.anchorMin = Vector2.zero;
			component3.anchorMax = Vector2.one;
			component3.sizeDelta = Vector2.zero;
			return buttonRoot;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002F50 File Offset: 0x00001150
		public static GameObject CreateText(DefaultControls.Resources resources)
		{
			GameObject gameObject = DefaultControls.CreateUIElementRoot("Text (Legacy)", DefaultControls.s_ThickElementSize, new Type[] { typeof(Text) });
			Text component = gameObject.GetComponent<Text>();
			component.text = "New Text";
			DefaultControls.SetDefaultTextValues(component);
			return gameObject;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002F8A File Offset: 0x0000118A
		public static GameObject CreateImage(DefaultControls.Resources resources)
		{
			return DefaultControls.CreateUIElementRoot("Image", DefaultControls.s_ImageElementSize, new Type[] { typeof(Image) });
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002FAE File Offset: 0x000011AE
		public static GameObject CreateRawImage(DefaultControls.Resources resources)
		{
			return DefaultControls.CreateUIElementRoot("RawImage", DefaultControls.s_ImageElementSize, new Type[] { typeof(RawImage) });
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002FD4 File Offset: 0x000011D4
		public static GameObject CreateSlider(DefaultControls.Resources resources)
		{
			GameObject root = DefaultControls.CreateUIElementRoot("Slider", DefaultControls.s_ThinElementSize, new Type[] { typeof(Slider) });
			GameObject background = DefaultControls.CreateUIObject("Background", root, new Type[] { typeof(Image) });
			GameObject fillArea = DefaultControls.CreateUIObject("Fill Area", root, new Type[] { typeof(RectTransform) });
			GameObject fill = DefaultControls.CreateUIObject("Fill", fillArea, new Type[] { typeof(Image) });
			GameObject handleArea = DefaultControls.CreateUIObject("Handle Slide Area", root, new Type[] { typeof(RectTransform) });
			GameObject handle = DefaultControls.CreateUIObject("Handle", handleArea, new Type[] { typeof(Image) });
			Image component = background.GetComponent<Image>();
			component.sprite = resources.background;
			component.type = Image.Type.Sliced;
			component.color = DefaultControls.s_DefaultSelectableColor;
			RectTransform component2 = background.GetComponent<RectTransform>();
			component2.anchorMin = new Vector2(0f, 0.25f);
			component2.anchorMax = new Vector2(1f, 0.75f);
			component2.sizeDelta = new Vector2(0f, 0f);
			RectTransform component3 = fillArea.GetComponent<RectTransform>();
			component3.anchorMin = new Vector2(0f, 0.25f);
			component3.anchorMax = new Vector2(1f, 0.75f);
			component3.anchoredPosition = new Vector2(-5f, 0f);
			component3.sizeDelta = new Vector2(-20f, 0f);
			Image component4 = fill.GetComponent<Image>();
			component4.sprite = resources.standard;
			component4.type = Image.Type.Sliced;
			component4.color = DefaultControls.s_DefaultSelectableColor;
			fill.GetComponent<RectTransform>().sizeDelta = new Vector2(10f, 0f);
			RectTransform component5 = handleArea.GetComponent<RectTransform>();
			component5.sizeDelta = new Vector2(-20f, 0f);
			component5.anchorMin = new Vector2(0f, 0f);
			component5.anchorMax = new Vector2(1f, 1f);
			Image handleImage = handle.GetComponent<Image>();
			handleImage.sprite = resources.knob;
			handleImage.color = DefaultControls.s_DefaultSelectableColor;
			handle.GetComponent<RectTransform>().sizeDelta = new Vector2(20f, 0f);
			Slider component6 = root.GetComponent<Slider>();
			component6.fillRect = fill.GetComponent<RectTransform>();
			component6.handleRect = handle.GetComponent<RectTransform>();
			component6.targetGraphic = handleImage;
			component6.direction = Slider.Direction.LeftToRight;
			DefaultControls.SetDefaultColorTransitionValues(component6);
			return root;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003258 File Offset: 0x00001458
		public static GameObject CreateScrollbar(DefaultControls.Resources resources)
		{
			GameObject scrollbarRoot = DefaultControls.CreateUIElementRoot("Scrollbar", DefaultControls.s_ThinElementSize, new Type[]
			{
				typeof(Image),
				typeof(Scrollbar)
			});
			GameObject sliderArea = DefaultControls.CreateUIObject("Sliding Area", scrollbarRoot, new Type[] { typeof(RectTransform) });
			GameObject gameObject = DefaultControls.CreateUIObject("Handle", sliderArea, new Type[] { typeof(Image) });
			Image component = scrollbarRoot.GetComponent<Image>();
			component.sprite = resources.background;
			component.type = Image.Type.Sliced;
			component.color = DefaultControls.s_DefaultSelectableColor;
			Image handleImage = gameObject.GetComponent<Image>();
			handleImage.sprite = resources.standard;
			handleImage.type = Image.Type.Sliced;
			handleImage.color = DefaultControls.s_DefaultSelectableColor;
			RectTransform component2 = sliderArea.GetComponent<RectTransform>();
			component2.sizeDelta = new Vector2(-20f, -20f);
			component2.anchorMin = Vector2.zero;
			component2.anchorMax = Vector2.one;
			RectTransform handleRect = gameObject.GetComponent<RectTransform>();
			handleRect.sizeDelta = new Vector2(20f, 20f);
			Scrollbar component3 = scrollbarRoot.GetComponent<Scrollbar>();
			component3.handleRect = handleRect;
			component3.targetGraphic = handleImage;
			DefaultControls.SetDefaultColorTransitionValues(component3);
			return scrollbarRoot;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003380 File Offset: 0x00001580
		public static GameObject CreateToggle(DefaultControls.Resources resources)
		{
			GameObject toggleRoot = DefaultControls.CreateUIElementRoot("Toggle", DefaultControls.s_ThinElementSize, new Type[] { typeof(Toggle) });
			GameObject background = DefaultControls.CreateUIObject("Background", toggleRoot, new Type[] { typeof(Image) });
			GameObject checkmark = DefaultControls.CreateUIObject("Checkmark", background, new Type[] { typeof(Image) });
			GameObject gameObject = DefaultControls.CreateUIObject("Label", toggleRoot, new Type[] { typeof(Text) });
			Toggle toggle = toggleRoot.GetComponent<Toggle>();
			toggle.isOn = true;
			Image bgImage = background.GetComponent<Image>();
			bgImage.sprite = resources.standard;
			bgImage.type = Image.Type.Sliced;
			bgImage.color = DefaultControls.s_DefaultSelectableColor;
			Image checkmarkImage = checkmark.GetComponent<Image>();
			checkmarkImage.sprite = resources.checkmark;
			Text component = gameObject.GetComponent<Text>();
			component.text = "Toggle";
			DefaultControls.SetDefaultTextValues(component);
			toggle.graphic = checkmarkImage;
			toggle.targetGraphic = bgImage;
			DefaultControls.SetDefaultColorTransitionValues(toggle);
			RectTransform component2 = background.GetComponent<RectTransform>();
			component2.anchorMin = new Vector2(0f, 1f);
			component2.anchorMax = new Vector2(0f, 1f);
			component2.anchoredPosition = new Vector2(10f, -10f);
			component2.sizeDelta = new Vector2(20f, 20f);
			RectTransform component3 = checkmark.GetComponent<RectTransform>();
			component3.anchorMin = new Vector2(0.5f, 0.5f);
			component3.anchorMax = new Vector2(0.5f, 0.5f);
			component3.anchoredPosition = Vector2.zero;
			component3.sizeDelta = new Vector2(20f, 20f);
			RectTransform component4 = gameObject.GetComponent<RectTransform>();
			component4.anchorMin = new Vector2(0f, 0f);
			component4.anchorMax = new Vector2(1f, 1f);
			component4.offsetMin = new Vector2(23f, 1f);
			component4.offsetMax = new Vector2(-5f, -2f);
			return toggleRoot;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003588 File Offset: 0x00001788
		public static GameObject CreateInputField(DefaultControls.Resources resources)
		{
			GameObject root = DefaultControls.CreateUIElementRoot("InputField (Legacy)", DefaultControls.s_ThickElementSize, new Type[]
			{
				typeof(Image),
				typeof(InputField)
			});
			GameObject childPlaceholder = DefaultControls.CreateUIObject("Placeholder", root, new Type[] { typeof(Text) });
			GameObject gameObject = DefaultControls.CreateUIObject("Text (Legacy)", root, new Type[] { typeof(Text) });
			Image component = root.GetComponent<Image>();
			component.sprite = resources.inputField;
			component.type = Image.Type.Sliced;
			component.color = DefaultControls.s_DefaultSelectableColor;
			InputField inputField = root.GetComponent<InputField>();
			DefaultControls.SetDefaultColorTransitionValues(inputField);
			Text text = gameObject.GetComponent<Text>();
			text.text = "";
			text.supportRichText = false;
			DefaultControls.SetDefaultTextValues(text);
			Text placeholder = childPlaceholder.GetComponent<Text>();
			placeholder.text = "Enter text...";
			placeholder.fontStyle = FontStyle.Italic;
			Color placeholderColor = text.color;
			placeholderColor.a *= 0.5f;
			placeholder.color = placeholderColor;
			RectTransform component2 = gameObject.GetComponent<RectTransform>();
			component2.anchorMin = Vector2.zero;
			component2.anchorMax = Vector2.one;
			component2.sizeDelta = Vector2.zero;
			component2.offsetMin = new Vector2(10f, 6f);
			component2.offsetMax = new Vector2(-10f, -7f);
			RectTransform component3 = childPlaceholder.GetComponent<RectTransform>();
			component3.anchorMin = Vector2.zero;
			component3.anchorMax = Vector2.one;
			component3.sizeDelta = Vector2.zero;
			component3.offsetMin = new Vector2(10f, 6f);
			component3.offsetMax = new Vector2(-10f, -7f);
			inputField.textComponent = text;
			inputField.placeholder = placeholder;
			return root;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003740 File Offset: 0x00001940
		public static GameObject CreateDropdown(DefaultControls.Resources resources)
		{
			GameObject root = DefaultControls.CreateUIElementRoot("Dropdown (Legacy)", DefaultControls.s_ThickElementSize, new Type[]
			{
				typeof(Image),
				typeof(Dropdown)
			});
			GameObject label = DefaultControls.CreateUIObject("Label", root, new Type[] { typeof(Text) });
			GameObject arrow = DefaultControls.CreateUIObject("Arrow", root, new Type[] { typeof(Image) });
			GameObject template = DefaultControls.CreateUIObject("Template", root, new Type[]
			{
				typeof(Image),
				typeof(ScrollRect)
			});
			GameObject viewport = DefaultControls.CreateUIObject("Viewport", template, new Type[]
			{
				typeof(Image),
				typeof(Mask)
			});
			GameObject content = DefaultControls.CreateUIObject("Content", viewport, new Type[] { typeof(RectTransform) });
			GameObject item = DefaultControls.CreateUIObject("Item", content, new Type[] { typeof(Toggle) });
			GameObject itemBackground = DefaultControls.CreateUIObject("Item Background", item, new Type[] { typeof(Image) });
			GameObject itemCheckmark = DefaultControls.CreateUIObject("Item Checkmark", item, new Type[] { typeof(Image) });
			GameObject gameObject = DefaultControls.CreateUIObject("Item Label", item, new Type[] { typeof(Text) });
			GameObject gameObject2 = DefaultControls.CreateScrollbar(resources);
			gameObject2.name = "Scrollbar";
			DefaultControls.SetParentAndAlign(gameObject2, template);
			Scrollbar scrollbarScrollbar = gameObject2.GetComponent<Scrollbar>();
			scrollbarScrollbar.SetDirection(Scrollbar.Direction.BottomToTop, true);
			RectTransform component = gameObject2.GetComponent<RectTransform>();
			component.anchorMin = Vector2.right;
			component.anchorMax = Vector2.one;
			component.pivot = Vector2.one;
			component.sizeDelta = new Vector2(component.sizeDelta.x, 0f);
			Text itemLabelText = gameObject.GetComponent<Text>();
			DefaultControls.SetDefaultTextValues(itemLabelText);
			itemLabelText.alignment = TextAnchor.MiddleLeft;
			Image itemBackgroundImage = itemBackground.GetComponent<Image>();
			itemBackgroundImage.color = new Color32(245, 245, 245, byte.MaxValue);
			Image itemCheckmarkImage = itemCheckmark.GetComponent<Image>();
			itemCheckmarkImage.sprite = resources.checkmark;
			Toggle component2 = item.GetComponent<Toggle>();
			component2.targetGraphic = itemBackgroundImage;
			component2.graphic = itemCheckmarkImage;
			component2.isOn = true;
			Image component3 = template.GetComponent<Image>();
			component3.sprite = resources.standard;
			component3.type = Image.Type.Sliced;
			ScrollRect component4 = template.GetComponent<ScrollRect>();
			component4.content = content.GetComponent<RectTransform>();
			component4.viewport = viewport.GetComponent<RectTransform>();
			component4.horizontal = false;
			component4.movementType = ScrollRect.MovementType.Clamped;
			component4.verticalScrollbar = scrollbarScrollbar;
			component4.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			component4.verticalScrollbarSpacing = -3f;
			viewport.GetComponent<Mask>().showMaskGraphic = false;
			Image component5 = viewport.GetComponent<Image>();
			component5.sprite = resources.mask;
			component5.type = Image.Type.Sliced;
			Text labelText = label.GetComponent<Text>();
			DefaultControls.SetDefaultTextValues(labelText);
			labelText.alignment = TextAnchor.MiddleLeft;
			arrow.GetComponent<Image>().sprite = resources.dropdown;
			Image backgroundImage = root.GetComponent<Image>();
			backgroundImage.sprite = resources.standard;
			backgroundImage.color = DefaultControls.s_DefaultSelectableColor;
			backgroundImage.type = Image.Type.Sliced;
			Dropdown component6 = root.GetComponent<Dropdown>();
			component6.targetGraphic = backgroundImage;
			DefaultControls.SetDefaultColorTransitionValues(component6);
			component6.template = template.GetComponent<RectTransform>();
			component6.captionText = labelText;
			component6.itemText = itemLabelText;
			itemLabelText.text = "Option A";
			component6.options.Add(new Dropdown.OptionData
			{
				text = "Option A"
			});
			component6.options.Add(new Dropdown.OptionData
			{
				text = "Option B"
			});
			component6.options.Add(new Dropdown.OptionData
			{
				text = "Option C"
			});
			component6.RefreshShownValue();
			RectTransform component7 = label.GetComponent<RectTransform>();
			component7.anchorMin = Vector2.zero;
			component7.anchorMax = Vector2.one;
			component7.offsetMin = new Vector2(10f, 6f);
			component7.offsetMax = new Vector2(-25f, -7f);
			RectTransform component8 = arrow.GetComponent<RectTransform>();
			component8.anchorMin = new Vector2(1f, 0.5f);
			component8.anchorMax = new Vector2(1f, 0.5f);
			component8.sizeDelta = new Vector2(20f, 20f);
			component8.anchoredPosition = new Vector2(-15f, 0f);
			RectTransform component9 = template.GetComponent<RectTransform>();
			component9.anchorMin = new Vector2(0f, 0f);
			component9.anchorMax = new Vector2(1f, 0f);
			component9.pivot = new Vector2(0.5f, 1f);
			component9.anchoredPosition = new Vector2(0f, 2f);
			component9.sizeDelta = new Vector2(0f, 150f);
			RectTransform component10 = viewport.GetComponent<RectTransform>();
			component10.anchorMin = new Vector2(0f, 0f);
			component10.anchorMax = new Vector2(1f, 1f);
			component10.sizeDelta = new Vector2(-18f, 0f);
			component10.pivot = new Vector2(0f, 1f);
			RectTransform component11 = content.GetComponent<RectTransform>();
			component11.anchorMin = new Vector2(0f, 1f);
			component11.anchorMax = new Vector2(1f, 1f);
			component11.pivot = new Vector2(0.5f, 1f);
			component11.anchoredPosition = new Vector2(0f, 0f);
			component11.sizeDelta = new Vector2(0f, 28f);
			RectTransform component12 = item.GetComponent<RectTransform>();
			component12.anchorMin = new Vector2(0f, 0.5f);
			component12.anchorMax = new Vector2(1f, 0.5f);
			component12.sizeDelta = new Vector2(0f, 20f);
			RectTransform component13 = itemBackground.GetComponent<RectTransform>();
			component13.anchorMin = Vector2.zero;
			component13.anchorMax = Vector2.one;
			component13.sizeDelta = Vector2.zero;
			RectTransform component14 = itemCheckmark.GetComponent<RectTransform>();
			component14.anchorMin = new Vector2(0f, 0.5f);
			component14.anchorMax = new Vector2(0f, 0.5f);
			component14.sizeDelta = new Vector2(20f, 20f);
			component14.anchoredPosition = new Vector2(10f, 0f);
			RectTransform component15 = gameObject.GetComponent<RectTransform>();
			component15.anchorMin = Vector2.zero;
			component15.anchorMax = Vector2.one;
			component15.offsetMin = new Vector2(20f, 1f);
			component15.offsetMax = new Vector2(-10f, -2f);
			template.SetActive(false);
			return root;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003DE8 File Offset: 0x00001FE8
		public static GameObject CreateScrollView(DefaultControls.Resources resources)
		{
			GameObject root = DefaultControls.CreateUIElementRoot("Scroll View", new Vector2(200f, 200f), new Type[]
			{
				typeof(Image),
				typeof(ScrollRect)
			});
			GameObject viewport = DefaultControls.CreateUIObject("Viewport", root, new Type[]
			{
				typeof(Image),
				typeof(Mask)
			});
			GameObject gameObject = DefaultControls.CreateUIObject("Content", viewport, new Type[] { typeof(RectTransform) });
			GameObject hScrollbar = DefaultControls.CreateScrollbar(resources);
			hScrollbar.name = "Scrollbar Horizontal";
			DefaultControls.SetParentAndAlign(hScrollbar, root);
			RectTransform hScrollbarRT = hScrollbar.GetComponent<RectTransform>();
			hScrollbarRT.anchorMin = Vector2.zero;
			hScrollbarRT.anchorMax = Vector2.right;
			hScrollbarRT.pivot = Vector2.zero;
			hScrollbarRT.sizeDelta = new Vector2(0f, hScrollbarRT.sizeDelta.y);
			GameObject vScrollbar = DefaultControls.CreateScrollbar(resources);
			vScrollbar.name = "Scrollbar Vertical";
			DefaultControls.SetParentAndAlign(vScrollbar, root);
			vScrollbar.GetComponent<Scrollbar>().SetDirection(Scrollbar.Direction.BottomToTop, true);
			RectTransform component = vScrollbar.GetComponent<RectTransform>();
			component.anchorMin = Vector2.right;
			component.anchorMax = Vector2.one;
			component.pivot = Vector2.one;
			component.sizeDelta = new Vector2(component.sizeDelta.x, 0f);
			RectTransform viewportRT = viewport.GetComponent<RectTransform>();
			viewportRT.anchorMin = Vector2.zero;
			viewportRT.anchorMax = Vector2.one;
			viewportRT.sizeDelta = Vector2.zero;
			viewportRT.pivot = Vector2.up;
			RectTransform contentRT = gameObject.GetComponent<RectTransform>();
			contentRT.anchorMin = Vector2.up;
			contentRT.anchorMax = Vector2.one;
			contentRT.sizeDelta = new Vector2(0f, 300f);
			contentRT.pivot = Vector2.up;
			ScrollRect component2 = root.GetComponent<ScrollRect>();
			component2.content = contentRT;
			component2.viewport = viewportRT;
			component2.horizontalScrollbar = hScrollbar.GetComponent<Scrollbar>();
			component2.verticalScrollbar = vScrollbar.GetComponent<Scrollbar>();
			component2.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			component2.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			component2.horizontalScrollbarSpacing = -3f;
			component2.verticalScrollbarSpacing = -3f;
			Image component3 = root.GetComponent<Image>();
			component3.sprite = resources.background;
			component3.type = Image.Type.Sliced;
			component3.color = DefaultControls.s_PanelColor;
			viewport.GetComponent<Mask>().showMaskGraphic = false;
			Image component4 = viewport.GetComponent<Image>();
			component4.sprite = resources.mask;
			component4.type = Image.Type.Sliced;
			return root;
		}

		// Token: 0x04000031 RID: 49
		private static DefaultControls.IFactoryControls m_CurrentFactory = DefaultControls.DefaultRuntimeFactory.Default;

		// Token: 0x04000032 RID: 50
		private const float kWidth = 160f;

		// Token: 0x04000033 RID: 51
		private const float kThickHeight = 30f;

		// Token: 0x04000034 RID: 52
		private const float kThinHeight = 20f;

		// Token: 0x04000035 RID: 53
		private static Vector2 s_ThickElementSize = new Vector2(160f, 30f);

		// Token: 0x04000036 RID: 54
		private static Vector2 s_ThinElementSize = new Vector2(160f, 20f);

		// Token: 0x04000037 RID: 55
		private static Vector2 s_ImageElementSize = new Vector2(100f, 100f);

		// Token: 0x04000038 RID: 56
		private static Color s_DefaultSelectableColor = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04000039 RID: 57
		private static Color s_PanelColor = new Color(1f, 1f, 1f, 0.392f);

		// Token: 0x0400003A RID: 58
		private static Color s_TextColor = new Color(0.19607843f, 0.19607843f, 0.19607843f, 1f);

		// Token: 0x02000012 RID: 18
		public interface IFactoryControls
		{
			// Token: 0x0600006E RID: 110
			GameObject CreateGameObject(string name, params Type[] components);
		}

		// Token: 0x02000013 RID: 19
		private class DefaultRuntimeFactory : DefaultControls.IFactoryControls
		{
			// Token: 0x0600006F RID: 111 RVA: 0x00004101 File Offset: 0x00002301
			public GameObject CreateGameObject(string name, params Type[] components)
			{
				return new GameObject(name, components);
			}

			// Token: 0x0400003B RID: 59
			public static DefaultControls.IFactoryControls Default = new DefaultControls.DefaultRuntimeFactory();
		}

		// Token: 0x02000014 RID: 20
		public struct Resources
		{
			// Token: 0x0400003C RID: 60
			public Sprite standard;

			// Token: 0x0400003D RID: 61
			public Sprite background;

			// Token: 0x0400003E RID: 62
			public Sprite inputField;

			// Token: 0x0400003F RID: 63
			public Sprite knob;

			// Token: 0x04000040 RID: 64
			public Sprite checkmark;

			// Token: 0x04000041 RID: 65
			public Sprite dropdown;

			// Token: 0x04000042 RID: 66
			public Sprite mask;
		}
	}
}
