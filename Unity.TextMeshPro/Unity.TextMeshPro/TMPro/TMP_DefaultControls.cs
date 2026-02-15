using System;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000027 RID: 39
	public static class TMP_DefaultControls
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000031F6 File Offset: 0x000013F6
		private static GameObject CreateUIElementRoot(string name, Vector2 size)
		{
			GameObject gameObject = new GameObject(name);
			gameObject.AddComponent<RectTransform>().sizeDelta = size;
			return gameObject;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000320A File Offset: 0x0000140A
		private static GameObject CreateUIObject(string name, GameObject parent)
		{
			GameObject gameObject = new GameObject(name);
			gameObject.AddComponent<RectTransform>();
			TMP_DefaultControls.SetParentAndAlign(gameObject, parent);
			return gameObject;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003220 File Offset: 0x00001420
		private static void SetDefaultTextValues(TMP_Text lbl)
		{
			lbl.color = TMP_DefaultControls.s_TextColor;
			lbl.fontSize = 14f;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003238 File Offset: 0x00001438
		private static void SetDefaultColorTransitionValues(Selectable slider)
		{
			ColorBlock colors = slider.colors;
			colors.highlightedColor = new Color(0.882f, 0.882f, 0.882f);
			colors.pressedColor = new Color(0.698f, 0.698f, 0.698f);
			colors.disabledColor = new Color(0.521f, 0.521f, 0.521f);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000329D File Offset: 0x0000149D
		private static void SetParentAndAlign(GameObject child, GameObject parent)
		{
			if (parent == null)
			{
				return;
			}
			child.transform.SetParent(parent.transform, false);
			TMP_DefaultControls.SetLayerRecursively(child, parent.layer);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000032C8 File Offset: 0x000014C8
		private static void SetLayerRecursively(GameObject go, int layer)
		{
			go.layer = layer;
			Transform t = go.transform;
			for (int i = 0; i < t.childCount; i++)
			{
				TMP_DefaultControls.SetLayerRecursively(t.GetChild(i).gameObject, layer);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003308 File Offset: 0x00001508
		public static GameObject CreateScrollbar(TMP_DefaultControls.Resources resources)
		{
			GameObject scrollbarRoot = TMP_DefaultControls.CreateUIElementRoot("Scrollbar", TMP_DefaultControls.s_ThinElementSize);
			GameObject sliderArea = TMP_DefaultControls.CreateUIObject("Sliding Area", scrollbarRoot);
			GameObject gameObject = TMP_DefaultControls.CreateUIObject("Handle", sliderArea);
			Image image = TMP_DefaultControls.AddComponent<Image>(scrollbarRoot);
			image.sprite = resources.background;
			image.type = Image.Type.Sliced;
			image.color = TMP_DefaultControls.s_DefaultSelectableColor;
			Image handleImage = TMP_DefaultControls.AddComponent<Image>(gameObject);
			handleImage.sprite = resources.standard;
			handleImage.type = Image.Type.Sliced;
			handleImage.color = TMP_DefaultControls.s_DefaultSelectableColor;
			RectTransform component = sliderArea.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(-20f, -20f);
			component.anchorMin = Vector2.zero;
			component.anchorMax = Vector2.one;
			RectTransform handleRect = gameObject.GetComponent<RectTransform>();
			handleRect.sizeDelta = new Vector2(20f, 20f);
			Scrollbar scrollbar = TMP_DefaultControls.AddComponent<Scrollbar>(scrollbarRoot);
			scrollbar.handleRect = handleRect;
			scrollbar.targetGraphic = handleImage;
			TMP_DefaultControls.SetDefaultColorTransitionValues(scrollbar);
			return scrollbarRoot;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000033EC File Offset: 0x000015EC
		public static GameObject CreateButton(TMP_DefaultControls.Resources resources)
		{
			GameObject buttonRoot = TMP_DefaultControls.CreateUIElementRoot("Button", TMP_DefaultControls.s_ThickElementSize);
			GameObject gameObject = new GameObject("Text (TMP)");
			gameObject.AddComponent<RectTransform>();
			TMP_DefaultControls.SetParentAndAlign(gameObject, buttonRoot);
			Image image = TMP_DefaultControls.AddComponent<Image>(buttonRoot);
			image.sprite = resources.standard;
			image.type = Image.Type.Sliced;
			image.color = TMP_DefaultControls.s_DefaultSelectableColor;
			TMP_DefaultControls.SetDefaultColorTransitionValues(TMP_DefaultControls.AddComponent<Button>(buttonRoot));
			TextMeshProUGUI textMeshProUGUI = TMP_DefaultControls.AddComponent<TextMeshProUGUI>(gameObject);
			textMeshProUGUI.text = "Button";
			textMeshProUGUI.alignment = TextAlignmentOptions.Center;
			TMP_DefaultControls.SetDefaultTextValues(textMeshProUGUI);
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.anchorMin = Vector2.zero;
			component.anchorMax = Vector2.one;
			component.sizeDelta = Vector2.zero;
			return buttonRoot;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003496 File Offset: 0x00001696
		public static GameObject CreateText(TMP_DefaultControls.Resources resources)
		{
			GameObject gameObject = TMP_DefaultControls.CreateUIElementRoot("Text (TMP)", TMP_DefaultControls.s_TextElementSize);
			gameObject.AddComponent<TextMeshProUGUI>();
			return gameObject;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000034B0 File Offset: 0x000016B0
		public static GameObject CreateInputField(TMP_DefaultControls.Resources resources)
		{
			GameObject root = TMP_DefaultControls.CreateUIElementRoot("InputField (TMP)", TMP_DefaultControls.s_ThickElementSize);
			GameObject textArea = TMP_DefaultControls.CreateUIObject("Text Area", root);
			GameObject childPlaceholder = TMP_DefaultControls.CreateUIObject("Placeholder", textArea);
			GameObject gameObject = TMP_DefaultControls.CreateUIObject("Text", textArea);
			Image image = TMP_DefaultControls.AddComponent<Image>(root);
			image.sprite = resources.inputField;
			image.type = Image.Type.Sliced;
			image.color = TMP_DefaultControls.s_DefaultSelectableColor;
			TMP_InputField inputField = TMP_DefaultControls.AddComponent<TMP_InputField>(root);
			TMP_DefaultControls.SetDefaultColorTransitionValues(inputField);
			TMP_DefaultControls.AddComponent<RectMask2D>(textArea).padding = new Vector4(-8f, -5f, -8f, -5f);
			RectTransform textAreaRectTransform = textArea.GetComponent<RectTransform>();
			textAreaRectTransform.anchorMin = Vector2.zero;
			textAreaRectTransform.anchorMax = Vector2.one;
			textAreaRectTransform.sizeDelta = Vector2.zero;
			textAreaRectTransform.offsetMin = new Vector2(10f, 6f);
			textAreaRectTransform.offsetMax = new Vector2(-10f, -7f);
			TextMeshProUGUI text = TMP_DefaultControls.AddComponent<TextMeshProUGUI>(gameObject);
			text.text = "";
			text.textWrappingMode = TextWrappingModes.NoWrap;
			text.extraPadding = true;
			text.richText = true;
			TMP_DefaultControls.SetDefaultTextValues(text);
			TextMeshProUGUI placeholder = TMP_DefaultControls.AddComponent<TextMeshProUGUI>(childPlaceholder);
			placeholder.text = "Enter text...";
			placeholder.fontSize = 14f;
			placeholder.fontStyle = FontStyles.Italic;
			placeholder.textWrappingMode = TextWrappingModes.NoWrap;
			placeholder.extraPadding = true;
			Color placeholderColor = text.color;
			placeholderColor.a *= 0.5f;
			placeholder.color = placeholderColor;
			TMP_DefaultControls.AddComponent<LayoutElement>(placeholder.gameObject).ignoreLayout = true;
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.anchorMin = Vector2.zero;
			component.anchorMax = Vector2.one;
			component.sizeDelta = Vector2.zero;
			component.offsetMin = new Vector2(0f, 0f);
			component.offsetMax = new Vector2(0f, 0f);
			RectTransform component2 = childPlaceholder.GetComponent<RectTransform>();
			component2.anchorMin = Vector2.zero;
			component2.anchorMax = Vector2.one;
			component2.sizeDelta = Vector2.zero;
			component2.offsetMin = new Vector2(0f, 0f);
			component2.offsetMax = new Vector2(0f, 0f);
			inputField.textViewport = textAreaRectTransform;
			inputField.textComponent = text;
			inputField.placeholder = placeholder;
			inputField.fontAsset = text.font;
			return root;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003700 File Offset: 0x00001900
		public static GameObject CreateDropdown(TMP_DefaultControls.Resources resources)
		{
			GameObject root = TMP_DefaultControls.CreateUIElementRoot("Dropdown", TMP_DefaultControls.s_ThickElementSize);
			GameObject gameObject = TMP_DefaultControls.CreateUIObject("Label", root);
			GameObject arrow = TMP_DefaultControls.CreateUIObject("Arrow", root);
			GameObject template = TMP_DefaultControls.CreateUIObject("Template", root);
			GameObject viewport = TMP_DefaultControls.CreateUIObject("Viewport", template);
			GameObject content = TMP_DefaultControls.CreateUIObject("Content", viewport);
			GameObject item = TMP_DefaultControls.CreateUIObject("Item", content);
			GameObject itemBackground = TMP_DefaultControls.CreateUIObject("Item Background", item);
			GameObject itemCheckmark = TMP_DefaultControls.CreateUIObject("Item Checkmark", item);
			GameObject itemLabel = TMP_DefaultControls.CreateUIObject("Item Label", item);
			GameObject gameObject2 = TMP_DefaultControls.CreateScrollbar(resources);
			gameObject2.name = "Scrollbar";
			TMP_DefaultControls.SetParentAndAlign(gameObject2, template);
			Scrollbar scrollbarScrollbar = gameObject2.GetComponent<Scrollbar>();
			scrollbarScrollbar.SetDirection(Scrollbar.Direction.BottomToTop, true);
			RectTransform component = gameObject2.GetComponent<RectTransform>();
			component.anchorMin = Vector2.right;
			component.anchorMax = Vector2.one;
			component.pivot = Vector2.one;
			component.sizeDelta = new Vector2(component.sizeDelta.x, 0f);
			TextMeshProUGUI itemLabelText = TMP_DefaultControls.AddComponent<TextMeshProUGUI>(itemLabel);
			TMP_DefaultControls.SetDefaultTextValues(itemLabelText);
			itemLabelText.alignment = TextAlignmentOptions.Left;
			Image itemBackgroundImage = TMP_DefaultControls.AddComponent<Image>(itemBackground);
			itemBackgroundImage.color = new Color32(245, 245, 245, byte.MaxValue);
			Image itemCheckmarkImage = TMP_DefaultControls.AddComponent<Image>(itemCheckmark);
			itemCheckmarkImage.sprite = resources.checkmark;
			Toggle toggle = TMP_DefaultControls.AddComponent<Toggle>(item);
			toggle.targetGraphic = itemBackgroundImage;
			toggle.graphic = itemCheckmarkImage;
			toggle.isOn = true;
			Image image = TMP_DefaultControls.AddComponent<Image>(template);
			image.sprite = resources.standard;
			image.type = Image.Type.Sliced;
			ScrollRect scrollRect = TMP_DefaultControls.AddComponent<ScrollRect>(template);
			scrollRect.content = (RectTransform)content.transform;
			scrollRect.viewport = (RectTransform)viewport.transform;
			scrollRect.horizontal = false;
			scrollRect.movementType = ScrollRect.MovementType.Clamped;
			scrollRect.verticalScrollbar = scrollbarScrollbar;
			scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			scrollRect.verticalScrollbarSpacing = -3f;
			TMP_DefaultControls.AddComponent<Mask>(viewport).showMaskGraphic = false;
			Image image2 = TMP_DefaultControls.AddComponent<Image>(viewport);
			image2.sprite = resources.mask;
			image2.type = Image.Type.Sliced;
			TextMeshProUGUI labelText = TMP_DefaultControls.AddComponent<TextMeshProUGUI>(gameObject);
			TMP_DefaultControls.SetDefaultTextValues(labelText);
			labelText.alignment = TextAlignmentOptions.Left;
			TMP_DefaultControls.AddComponent<Image>(arrow).sprite = resources.dropdown;
			Image backgroundImage = TMP_DefaultControls.AddComponent<Image>(root);
			backgroundImage.sprite = resources.standard;
			backgroundImage.color = TMP_DefaultControls.s_DefaultSelectableColor;
			backgroundImage.type = Image.Type.Sliced;
			TMP_Dropdown tmp_Dropdown = TMP_DefaultControls.AddComponent<TMP_Dropdown>(root);
			tmp_Dropdown.targetGraphic = backgroundImage;
			TMP_DefaultControls.SetDefaultColorTransitionValues(tmp_Dropdown);
			tmp_Dropdown.template = template.GetComponent<RectTransform>();
			tmp_Dropdown.captionText = labelText;
			tmp_Dropdown.itemText = itemLabelText;
			itemLabelText.text = "Option A";
			tmp_Dropdown.options.Add(new TMP_Dropdown.OptionData
			{
				text = "Option A"
			});
			tmp_Dropdown.options.Add(new TMP_Dropdown.OptionData
			{
				text = "Option B"
			});
			tmp_Dropdown.options.Add(new TMP_Dropdown.OptionData
			{
				text = "Option C"
			});
			tmp_Dropdown.RefreshShownValue();
			RectTransform component2 = gameObject.GetComponent<RectTransform>();
			component2.anchorMin = Vector2.zero;
			component2.anchorMax = Vector2.one;
			component2.offsetMin = new Vector2(10f, 6f);
			component2.offsetMax = new Vector2(-25f, -7f);
			RectTransform component3 = arrow.GetComponent<RectTransform>();
			component3.anchorMin = new Vector2(1f, 0.5f);
			component3.anchorMax = new Vector2(1f, 0.5f);
			component3.sizeDelta = new Vector2(20f, 20f);
			component3.anchoredPosition = new Vector2(-15f, 0f);
			RectTransform component4 = template.GetComponent<RectTransform>();
			component4.anchorMin = new Vector2(0f, 0f);
			component4.anchorMax = new Vector2(1f, 0f);
			component4.pivot = new Vector2(0.5f, 1f);
			component4.anchoredPosition = new Vector2(0f, 2f);
			component4.sizeDelta = new Vector2(0f, 150f);
			RectTransform component5 = viewport.GetComponent<RectTransform>();
			component5.anchorMin = new Vector2(0f, 0f);
			component5.anchorMax = new Vector2(1f, 1f);
			component5.sizeDelta = new Vector2(-18f, 0f);
			component5.pivot = new Vector2(0f, 1f);
			RectTransform component6 = content.GetComponent<RectTransform>();
			component6.anchorMin = new Vector2(0f, 1f);
			component6.anchorMax = new Vector2(1f, 1f);
			component6.pivot = new Vector2(0.5f, 1f);
			component6.anchoredPosition = new Vector2(0f, 0f);
			component6.sizeDelta = new Vector2(0f, 28f);
			RectTransform component7 = item.GetComponent<RectTransform>();
			component7.anchorMin = new Vector2(0f, 0.5f);
			component7.anchorMax = new Vector2(1f, 0.5f);
			component7.sizeDelta = new Vector2(0f, 20f);
			RectTransform component8 = itemBackground.GetComponent<RectTransform>();
			component8.anchorMin = Vector2.zero;
			component8.anchorMax = Vector2.one;
			component8.sizeDelta = Vector2.zero;
			RectTransform component9 = itemCheckmark.GetComponent<RectTransform>();
			component9.anchorMin = new Vector2(0f, 0.5f);
			component9.anchorMax = new Vector2(0f, 0.5f);
			component9.sizeDelta = new Vector2(20f, 20f);
			component9.anchoredPosition = new Vector2(10f, 0f);
			RectTransform component10 = itemLabel.GetComponent<RectTransform>();
			component10.anchorMin = Vector2.zero;
			component10.anchorMax = Vector2.one;
			component10.offsetMin = new Vector2(20f, 1f);
			component10.offsetMax = new Vector2(-10f, -2f);
			template.SetActive(false);
			return root;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003CD1 File Offset: 0x00001ED1
		private static T AddComponent<T>(GameObject go) where T : Component
		{
			return go.AddComponent<T>();
		}

		// Token: 0x0400009B RID: 155
		private const float kWidth = 160f;

		// Token: 0x0400009C RID: 156
		private const float kThickHeight = 30f;

		// Token: 0x0400009D RID: 157
		private const float kThinHeight = 20f;

		// Token: 0x0400009E RID: 158
		private static Vector2 s_TextElementSize = new Vector2(100f, 100f);

		// Token: 0x0400009F RID: 159
		private static Vector2 s_ThickElementSize = new Vector2(160f, 30f);

		// Token: 0x040000A0 RID: 160
		private static Vector2 s_ThinElementSize = new Vector2(160f, 20f);

		// Token: 0x040000A1 RID: 161
		private static Color s_DefaultSelectableColor = new Color(1f, 1f, 1f, 1f);

		// Token: 0x040000A2 RID: 162
		private static Color s_TextColor = new Color(0.19607843f, 0.19607843f, 0.19607843f, 1f);

		// Token: 0x02000028 RID: 40
		public struct Resources
		{
			// Token: 0x040000A3 RID: 163
			public Sprite standard;

			// Token: 0x040000A4 RID: 164
			public Sprite background;

			// Token: 0x040000A5 RID: 165
			public Sprite inputField;

			// Token: 0x040000A6 RID: 166
			public Sprite knob;

			// Token: 0x040000A7 RID: 167
			public Sprite checkmark;

			// Token: 0x040000A8 RID: 168
			public Sprite dropdown;

			// Token: 0x040000A9 RID: 169
			public Sprite mask;
		}
	}
}
