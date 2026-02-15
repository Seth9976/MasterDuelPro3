using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000029 RID: 41
	[AddComponentMenu("UI/Dropdown - TextMeshPro", 35)]
	[RequireComponent(typeof(RectTransform))]
	public class TMP_Dropdown : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003D61 File Offset: 0x00001F61
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003D69 File Offset: 0x00001F69
		public RectTransform template
		{
			get
			{
				return this.m_Template;
			}
			set
			{
				this.m_Template = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003D78 File Offset: 0x00001F78
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003D80 File Offset: 0x00001F80
		public TMP_Text captionText
		{
			get
			{
				return this.m_CaptionText;
			}
			set
			{
				this.m_CaptionText = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003D8F File Offset: 0x00001F8F
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003D97 File Offset: 0x00001F97
		public Image captionImage
		{
			get
			{
				return this.m_CaptionImage;
			}
			set
			{
				this.m_CaptionImage = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003DA6 File Offset: 0x00001FA6
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003DAE File Offset: 0x00001FAE
		public Graphic placeholder
		{
			get
			{
				return this.m_Placeholder;
			}
			set
			{
				this.m_Placeholder = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003DBD File Offset: 0x00001FBD
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003DC5 File Offset: 0x00001FC5
		public TMP_Text itemText
		{
			get
			{
				return this.m_ItemText;
			}
			set
			{
				this.m_ItemText = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003DD4 File Offset: 0x00001FD4
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00003DDC File Offset: 0x00001FDC
		public Image itemImage
		{
			get
			{
				return this.m_ItemImage;
			}
			set
			{
				this.m_ItemImage = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003DEB File Offset: 0x00001FEB
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public List<TMP_Dropdown.OptionData> options
		{
			get
			{
				return this.m_Options.options;
			}
			set
			{
				this.m_Options.options = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003E0C File Offset: 0x0000200C
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00003E14 File Offset: 0x00002014
		public TMP_Dropdown.DropdownEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003E1D File Offset: 0x0000201D
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003E25 File Offset: 0x00002025
		public float alphaFadeSpeed
		{
			get
			{
				return this.m_AlphaFadeSpeed;
			}
			set
			{
				this.m_AlphaFadeSpeed = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003E2E File Offset: 0x0000202E
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00003E36 File Offset: 0x00002036
		public int value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.SetValue(value, true);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003E40 File Offset: 0x00002040
		public void SetValueWithoutNotify(int input)
		{
			this.SetValue(input, false);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003E4C File Offset: 0x0000204C
		private void SetValue(int value, bool sendCallback = true)
		{
			if (Application.isPlaying && (value == this.m_Value || this.options.Count == 0))
			{
				return;
			}
			if (this.m_MultiSelect)
			{
				this.m_Value = value;
			}
			else
			{
				this.m_Value = Mathf.Clamp(value, this.m_Placeholder ? (-1) : 0, this.options.Count - 1);
			}
			this.RefreshShownValue();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Dropdown.value", this);
				this.m_OnValueChanged.Invoke(this.m_Value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003ED7 File Offset: 0x000020D7
		public bool IsExpanded
		{
			get
			{
				return this.m_Dropdown != null;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003EE5 File Offset: 0x000020E5
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00003EED File Offset: 0x000020ED
		public bool MultiSelect
		{
			get
			{
				return this.m_MultiSelect;
			}
			set
			{
				this.m_MultiSelect = value;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003EF6 File Offset: 0x000020F6
		protected TMP_Dropdown()
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003F2C File Offset: 0x0000212C
		protected override void Awake()
		{
			if (this.m_CaptionImage)
			{
				this.m_CaptionImage.enabled = this.m_CaptionImage.sprite != null && this.m_CaptionImage.color.a > 0f;
			}
			if (this.m_Template)
			{
				this.m_Template.gameObject.SetActive(false);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003F9C File Offset: 0x0000219C
		protected override void Start()
		{
			this.m_AlphaTweenRunner = new TweenRunner<FloatTween>();
			this.m_AlphaTweenRunner.Init(this);
			base.Start();
			this.RefreshShownValue();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003FC1 File Offset: 0x000021C1
		protected override void OnDisable()
		{
			this.ImmediateDestroyDropdownList();
			if (this.m_Blocker != null)
			{
				this.DestroyBlocker(this.m_Blocker);
			}
			this.m_Blocker = null;
			base.OnDisable();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003FF0 File Offset: 0x000021F0
		public void RefreshShownValue()
		{
			TMP_Dropdown.OptionData data = TMP_Dropdown.s_NoOptionData;
			if (this.options.Count > 0)
			{
				if (this.m_MultiSelect)
				{
					int firstActiveFlag = TMP_Dropdown.FirstActiveFlagIndex(this.m_Value);
					if (this.m_Value == 0 || firstActiveFlag >= this.options.Count)
					{
						data = TMP_Dropdown.k_NothingOption;
					}
					else if (TMP_Dropdown.IsEverythingValue(this.options.Count, this.m_Value))
					{
						data = TMP_Dropdown.k_EverythingOption;
					}
					else if (Mathf.IsPowerOfTwo(this.m_Value) && this.m_Value > 0)
					{
						data = this.options[firstActiveFlag];
					}
					else
					{
						data = TMP_Dropdown.k_MixedOption;
					}
				}
				else if (this.m_Value >= 0)
				{
					data = this.options[Mathf.Clamp(this.m_Value, 0, this.options.Count - 1)];
				}
			}
			if (this.m_CaptionText)
			{
				if (data != null && data.text != null)
				{
					this.m_CaptionText.text = data.text;
				}
				else
				{
					this.m_CaptionText.text = "";
				}
			}
			if (this.m_CaptionImage)
			{
				this.m_CaptionImage.sprite = data.image;
				this.m_CaptionImage.color = data.color;
				this.m_CaptionImage.enabled = this.m_CaptionImage.sprite != null && this.m_CaptionImage.color.a > 0f;
			}
			if (this.m_Placeholder)
			{
				this.m_Placeholder.enabled = this.options.Count == 0 || this.m_Value == -1;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004194 File Offset: 0x00002394
		public void AddOptions(List<TMP_Dropdown.OptionData> options)
		{
			this.options.AddRange(options);
			this.RefreshShownValue();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000041A8 File Offset: 0x000023A8
		public void AddOptions(List<string> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				this.options.Add(new TMP_Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000041E4 File Offset: 0x000023E4
		public void AddOptions(List<Sprite> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				this.options.Add(new TMP_Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000421F File Offset: 0x0000241F
		public void ClearOptions()
		{
			this.options.Clear();
			this.m_Value = (this.m_Placeholder ? (-1) : 0);
			this.RefreshShownValue();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000424C File Offset: 0x0000244C
		private void SetupTemplate()
		{
			this.validTemplate = false;
			if (!this.m_Template)
			{
				Debug.LogError("The dropdown template is not assigned. The template needs to be assigned and must have a child GameObject with a Toggle component serving as the item.", this);
				return;
			}
			GameObject templateGo = this.m_Template.gameObject;
			templateGo.SetActive(true);
			Toggle itemToggle = this.m_Template.GetComponentInChildren<Toggle>();
			this.validTemplate = true;
			if (!itemToggle || itemToggle.transform == this.template)
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The template must have a child GameObject with a Toggle component serving as the item.", this.template);
			}
			else if (!(itemToggle.transform.parent is RectTransform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The child GameObject with a Toggle component (the item) must have a RectTransform on its parent.", this.template);
			}
			else if (this.itemText != null && !this.itemText.transform.IsChildOf(itemToggle.transform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The Item Text must be on the item GameObject or children of it.", this.template);
			}
			else if (this.itemImage != null && !this.itemImage.transform.IsChildOf(itemToggle.transform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The Item Image must be on the item GameObject or children of it.", this.template);
			}
			if (!this.validTemplate)
			{
				templateGo.SetActive(false);
				return;
			}
			TMP_Dropdown.DropdownItem dropdownItem = itemToggle.gameObject.AddComponent<TMP_Dropdown.DropdownItem>();
			dropdownItem.text = this.m_ItemText;
			dropdownItem.image = this.m_ItemImage;
			dropdownItem.toggle = itemToggle;
			dropdownItem.rectTransform = (RectTransform)itemToggle.transform;
			Canvas parentCanvas = null;
			Transform parentTransform = this.m_Template.parent;
			while (parentTransform != null)
			{
				parentCanvas = parentTransform.GetComponent<Canvas>();
				if (parentCanvas != null)
				{
					break;
				}
				parentTransform = parentTransform.parent;
			}
			Canvas orAddComponent = TMP_Dropdown.GetOrAddComponent<Canvas>(templateGo);
			orAddComponent.overrideSorting = true;
			orAddComponent.sortingOrder = 30000;
			if (parentCanvas != null)
			{
				Component[] components2 = parentCanvas.GetComponents<BaseRaycaster>();
				Component[] components = components2;
				for (int i = 0; i < components.Length; i++)
				{
					Type raycasterType = components[i].GetType();
					if (templateGo.GetComponent(raycasterType) == null)
					{
						templateGo.AddComponent(raycasterType);
					}
				}
			}
			else
			{
				TMP_Dropdown.GetOrAddComponent<GraphicRaycaster>(templateGo);
			}
			TMP_Dropdown.GetOrAddComponent<CanvasGroup>(templateGo);
			templateGo.SetActive(false);
			this.validTemplate = true;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000447C File Offset: 0x0000267C
		private static T GetOrAddComponent<T>(GameObject go) where T : Component
		{
			T comp = go.GetComponent<T>();
			if (!comp)
			{
				comp = go.AddComponent<T>();
			}
			return comp;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000044A5 File Offset: 0x000026A5
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Show();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000044A5 File Offset: 0x000026A5
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Show();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000044AD File Offset: 0x000026AD
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Hide();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000044B8 File Offset: 0x000026B8
		public void Show()
		{
			if (this.m_Coroutine != null)
			{
				base.StopCoroutine(this.m_Coroutine);
				this.ImmediateDestroyDropdownList();
			}
			if (!this.IsActive() || !this.IsInteractable() || this.m_Dropdown != null)
			{
				return;
			}
			List<Canvas> list = TMP_ListPool<Canvas>.Get();
			base.gameObject.GetComponentsInParent<Canvas>(false, list);
			if (list.Count == 0)
			{
				return;
			}
			Canvas rootCanvas = list[list.Count - 1];
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].isRootCanvas)
				{
					rootCanvas = list[i];
					break;
				}
			}
			TMP_ListPool<Canvas>.Release(list);
			if (!this.validTemplate)
			{
				this.SetupTemplate();
				if (!this.validTemplate)
				{
					return;
				}
			}
			this.m_Template.gameObject.SetActive(true);
			this.m_Template.GetComponent<Canvas>().sortingLayerID = rootCanvas.sortingLayerID;
			this.m_Dropdown = this.CreateDropdownList(this.m_Template.gameObject);
			this.m_Dropdown.name = "Dropdown List";
			this.m_Dropdown.SetActive(true);
			RectTransform dropdownRectTransform = this.m_Dropdown.transform as RectTransform;
			dropdownRectTransform.SetParent(this.m_Template.transform.parent, false);
			TMP_Dropdown.DropdownItem itemTemplate = this.m_Dropdown.GetComponentInChildren<TMP_Dropdown.DropdownItem>();
			RectTransform contentRectTransform = itemTemplate.rectTransform.parent.gameObject.transform as RectTransform;
			itemTemplate.rectTransform.gameObject.SetActive(true);
			Rect dropdownContentRect = contentRectTransform.rect;
			Rect itemTemplateRect = itemTemplate.rectTransform.rect;
			Vector2 offsetMin = itemTemplateRect.min - dropdownContentRect.min + itemTemplate.rectTransform.localPosition;
			Vector2 offsetMax = itemTemplateRect.max - dropdownContentRect.max + itemTemplate.rectTransform.localPosition;
			Vector2 itemSize = itemTemplateRect.size;
			this.m_Items.Clear();
			Toggle prev = null;
			if (this.m_MultiSelect && this.options.Count > 0)
			{
				TMP_Dropdown.DropdownItem item2 = this.AddItem(TMP_Dropdown.k_NothingOption, this.value == 0, itemTemplate, this.m_Items);
				if (item2.image != null)
				{
					item2.image.gameObject.SetActive(false);
				}
				Toggle nothingToggle = item2.toggle;
				nothingToggle.isOn = this.value == 0;
				nothingToggle.onValueChanged.AddListener(delegate(bool x)
				{
					this.OnSelectItem(nothingToggle);
				});
				prev = nothingToggle;
				bool isEverythingValue = TMP_Dropdown.IsEverythingValue(this.options.Count, this.value);
				item2 = this.AddItem(TMP_Dropdown.k_EverythingOption, isEverythingValue, itemTemplate, this.m_Items);
				if (item2.image != null)
				{
					item2.image.gameObject.SetActive(false);
				}
				Toggle everythingToggle = item2.toggle;
				everythingToggle.isOn = isEverythingValue;
				everythingToggle.onValueChanged.AddListener(delegate(bool x)
				{
					this.OnSelectItem(everythingToggle);
				});
				if (prev != null)
				{
					Navigation prevNav = prev.navigation;
					Navigation toggleNav = item2.toggle.navigation;
					prevNav.mode = Navigation.Mode.Explicit;
					toggleNav.mode = Navigation.Mode.Explicit;
					prevNav.selectOnDown = item2.toggle;
					prevNav.selectOnRight = item2.toggle;
					toggleNav.selectOnLeft = prev;
					toggleNav.selectOnUp = prev;
					prev.navigation = prevNav;
					item2.toggle.navigation = toggleNav;
				}
			}
			for (int j = 0; j < this.options.Count; j++)
			{
				TMP_Dropdown.OptionData data = this.options[j];
				TMP_Dropdown.DropdownItem item = this.AddItem(data, this.value == j, itemTemplate, this.m_Items);
				if (!(item == null))
				{
					if (this.m_MultiSelect)
					{
						item.toggle.isOn = (this.value & (1 << j)) != 0;
					}
					else
					{
						item.toggle.isOn = this.value == j;
					}
					item.toggle.onValueChanged.AddListener(delegate(bool x)
					{
						this.OnSelectItem(item.toggle);
					});
					if (item.toggle.isOn)
					{
						item.toggle.Select();
					}
					if (prev != null)
					{
						Navigation prevNav2 = prev.navigation;
						Navigation toggleNav2 = item.toggle.navigation;
						prevNav2.mode = Navigation.Mode.Explicit;
						toggleNav2.mode = Navigation.Mode.Explicit;
						prevNav2.selectOnDown = item.toggle;
						prevNav2.selectOnRight = item.toggle;
						toggleNav2.selectOnLeft = prev;
						toggleNav2.selectOnUp = prev;
						prev.navigation = prevNav2;
						item.toggle.navigation = toggleNav2;
					}
					prev = item.toggle;
				}
			}
			Vector2 sizeDelta = contentRectTransform.sizeDelta;
			sizeDelta.y = itemSize.y * (float)this.m_Items.Count + offsetMin.y - offsetMax.y;
			contentRectTransform.sizeDelta = sizeDelta;
			float extraSpace = dropdownRectTransform.rect.height - contentRectTransform.rect.height;
			if (extraSpace > 0f)
			{
				dropdownRectTransform.sizeDelta = new Vector2(dropdownRectTransform.sizeDelta.x, dropdownRectTransform.sizeDelta.y - extraSpace);
			}
			Vector3[] corners = new Vector3[4];
			dropdownRectTransform.GetWorldCorners(corners);
			RectTransform rootCanvasRectTransform = rootCanvas.transform as RectTransform;
			Rect rootCanvasRect = rootCanvasRectTransform.rect;
			for (int axis = 0; axis < 2; axis++)
			{
				bool outside = false;
				for (int k = 0; k < 4; k++)
				{
					Vector3 corner = rootCanvasRectTransform.InverseTransformPoint(corners[k]);
					if ((corner[axis] < rootCanvasRect.min[axis] && !Mathf.Approximately(corner[axis], rootCanvasRect.min[axis])) || (corner[axis] > rootCanvasRect.max[axis] && !Mathf.Approximately(corner[axis], rootCanvasRect.max[axis])))
					{
						outside = true;
						break;
					}
				}
				if (outside)
				{
					RectTransformUtility.FlipLayoutOnAxis(dropdownRectTransform, axis, false, false);
				}
			}
			for (int l = 0; l < this.m_Items.Count; l++)
			{
				RectTransform itemRect = this.m_Items[l].rectTransform;
				itemRect.anchorMin = new Vector2(itemRect.anchorMin.x, 0f);
				itemRect.anchorMax = new Vector2(itemRect.anchorMax.x, 0f);
				itemRect.anchoredPosition = new Vector2(itemRect.anchoredPosition.x, offsetMin.y + itemSize.y * (float)(this.m_Items.Count - 1 - l) + itemSize.y * itemRect.pivot.y);
				itemRect.sizeDelta = new Vector2(itemRect.sizeDelta.x, itemSize.y);
			}
			this.AlphaFadeList(this.m_AlphaFadeSpeed, 0f, 1f);
			this.m_Template.gameObject.SetActive(false);
			itemTemplate.gameObject.SetActive(false);
			this.m_Blocker = this.CreateBlocker(rootCanvas);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004C9C File Offset: 0x00002E9C
		private static bool IsEverythingValue(int count, int value)
		{
			bool result = true;
			for (int i = 0; i < count; i++)
			{
				if ((value & (1 << i)) == 0)
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004CC4 File Offset: 0x00002EC4
		private static int EverythingValue(int count)
		{
			int result = 0;
			for (int i = 0; i < count; i++)
			{
				result |= 1 << i;
			}
			return result;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004CEC File Offset: 0x00002EEC
		protected virtual GameObject CreateBlocker(Canvas rootCanvas)
		{
			GameObject blocker = new GameObject("Blocker");
			blocker.layer = rootCanvas.gameObject.layer;
			RectTransform rectTransform = blocker.AddComponent<RectTransform>();
			rectTransform.SetParent(rootCanvas.transform, false);
			rectTransform.anchorMin = Vector3.zero;
			rectTransform.anchorMax = Vector3.one;
			rectTransform.sizeDelta = Vector2.zero;
			Canvas canvas = blocker.AddComponent<Canvas>();
			canvas.overrideSorting = true;
			Canvas dropdownCanvas = this.m_Dropdown.GetComponent<Canvas>();
			canvas.sortingLayerID = dropdownCanvas.sortingLayerID;
			canvas.sortingOrder = dropdownCanvas.sortingOrder - 1;
			Canvas parentCanvas = null;
			Transform parentTransform = this.m_Template.parent;
			while (parentTransform != null)
			{
				parentCanvas = parentTransform.GetComponent<Canvas>();
				if (parentCanvas != null)
				{
					break;
				}
				parentTransform = parentTransform.parent;
			}
			if (parentCanvas != null)
			{
				Component[] components2 = parentCanvas.GetComponents<BaseRaycaster>();
				Component[] components = components2;
				for (int i = 0; i < components.Length; i++)
				{
					Type raycasterType = components[i].GetType();
					if (blocker.GetComponent(raycasterType) == null)
					{
						blocker.AddComponent(raycasterType);
					}
				}
			}
			else
			{
				TMP_Dropdown.GetOrAddComponent<GraphicRaycaster>(blocker);
			}
			blocker.AddComponent<Image>().color = Color.clear;
			blocker.AddComponent<Button>().onClick.AddListener(new UnityAction(this.Hide));
			blocker.AddComponent<CanvasGroup>().ignoreParentGroups = true;
			return blocker;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004E43 File Offset: 0x00003043
		protected virtual void DestroyBlocker(GameObject blocker)
		{
			global::UnityEngine.Object.Destroy(blocker);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004E4B File Offset: 0x0000304B
		protected virtual GameObject CreateDropdownList(GameObject template)
		{
			return global::UnityEngine.Object.Instantiate<GameObject>(template);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004E43 File Offset: 0x00003043
		protected virtual void DestroyDropdownList(GameObject dropdownList)
		{
			global::UnityEngine.Object.Destroy(dropdownList);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004E53 File Offset: 0x00003053
		protected virtual TMP_Dropdown.DropdownItem CreateItem(TMP_Dropdown.DropdownItem itemTemplate)
		{
			return global::UnityEngine.Object.Instantiate<TMP_Dropdown.DropdownItem>(itemTemplate);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected virtual void DestroyItem(TMP_Dropdown.DropdownItem item)
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004E5C File Offset: 0x0000305C
		private TMP_Dropdown.DropdownItem AddItem(TMP_Dropdown.OptionData data, bool selected, TMP_Dropdown.DropdownItem itemTemplate, List<TMP_Dropdown.DropdownItem> items)
		{
			TMP_Dropdown.DropdownItem item = this.CreateItem(itemTemplate);
			item.rectTransform.SetParent(itemTemplate.rectTransform.parent, false);
			item.gameObject.SetActive(true);
			item.gameObject.name = "Item " + items.Count.ToString() + ((data.text != null) ? (": " + data.text) : "");
			if (item.toggle != null)
			{
				item.toggle.isOn = false;
			}
			if (item.text)
			{
				item.text.text = data.text;
			}
			if (item.image)
			{
				item.image.sprite = data.image;
				item.image.color = data.color;
				item.image.enabled = item.image.sprite != null && data.color.a > 0f;
			}
			items.Add(item);
			return item;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004F7C File Offset: 0x0000317C
		private void AlphaFadeList(float duration, float alpha)
		{
			CanvasGroup group = this.m_Dropdown.GetComponent<CanvasGroup>();
			this.AlphaFadeList(duration, group.alpha, alpha);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004FA4 File Offset: 0x000031A4
		private void AlphaFadeList(float duration, float start, float end)
		{
			if (end.Equals(start))
			{
				return;
			}
			FloatTween tween = new FloatTween
			{
				duration = duration,
				startValue = start,
				targetValue = end
			};
			tween.AddOnChangedCallback(new UnityAction<float>(this.SetAlpha));
			tween.ignoreTimeScale = true;
			this.m_AlphaTweenRunner.StartTween(tween);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005005 File Offset: 0x00003205
		private void SetAlpha(float alpha)
		{
			if (!this.m_Dropdown)
			{
				return;
			}
			this.m_Dropdown.GetComponent<CanvasGroup>().alpha = alpha;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005028 File Offset: 0x00003228
		public void Hide()
		{
			if (this.m_Coroutine == null)
			{
				if (this.m_Dropdown != null)
				{
					this.AlphaFadeList(this.m_AlphaFadeSpeed, 0f);
					if (this.IsActive())
					{
						this.m_Coroutine = base.StartCoroutine(this.DelayedDestroyDropdownList(this.m_AlphaFadeSpeed));
					}
				}
				if (this.m_Blocker != null)
				{
					this.DestroyBlocker(this.m_Blocker);
				}
				this.m_Blocker = null;
				this.Select();
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000050A3 File Offset: 0x000032A3
		private IEnumerator DelayedDestroyDropdownList(float delay)
		{
			yield return new WaitForSecondsRealtime(delay);
			this.ImmediateDestroyDropdownList();
			yield break;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000050BC File Offset: 0x000032BC
		private void ImmediateDestroyDropdownList()
		{
			for (int i = 0; i < this.m_Items.Count; i++)
			{
				if (this.m_Items[i] != null)
				{
					this.DestroyItem(this.m_Items[i]);
				}
			}
			this.m_Items.Clear();
			if (this.m_Dropdown != null)
			{
				this.DestroyDropdownList(this.m_Dropdown);
			}
			if (this.m_AlphaTweenRunner != null)
			{
				this.m_AlphaTweenRunner.StopTween();
			}
			this.m_Dropdown = null;
			this.m_Coroutine = null;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000514C File Offset: 0x0000334C
		private void OnSelectItem(Toggle toggle)
		{
			int selectedIndex = -1;
			Transform tr = toggle.transform;
			Transform parent = tr.parent;
			for (int i = 1; i < parent.childCount; i++)
			{
				if (parent.GetChild(i) == tr)
				{
					selectedIndex = i - 1;
					break;
				}
			}
			if (selectedIndex < 0)
			{
				return;
			}
			if (this.m_MultiSelect)
			{
				if (selectedIndex != 0)
				{
					if (selectedIndex != 1)
					{
						int flagValue = 1 << selectedIndex - 2;
						bool wasSelected = (this.value & flagValue) != 0;
						toggle.SetIsOnWithoutNotify(!wasSelected);
						if (wasSelected)
						{
							this.value &= ~flagValue;
						}
						else
						{
							this.value |= flagValue;
						}
					}
					else
					{
						this.value = TMP_Dropdown.EverythingValue(this.options.Count);
						for (int j = 3; j < parent.childCount; j++)
						{
							Toggle toggleComponent = parent.GetChild(j).GetComponentInChildren<Toggle>();
							if (toggleComponent)
							{
								toggleComponent.SetIsOnWithoutNotify(j > 2);
							}
						}
					}
				}
				else
				{
					this.value = 0;
					for (int k = 3; k < parent.childCount; k++)
					{
						Toggle toggleComponent2 = parent.GetChild(k).GetComponentInChildren<Toggle>();
						if (toggleComponent2)
						{
							toggleComponent2.SetIsOnWithoutNotify(false);
						}
					}
					toggle.isOn = true;
				}
			}
			else
			{
				if (!toggle.isOn)
				{
					toggle.SetIsOnWithoutNotify(true);
				}
				this.value = selectedIndex;
			}
			this.Hide();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000052AC File Offset: 0x000034AC
		private static int FirstActiveFlagIndex(int value)
		{
			if (value == 0)
			{
				return 0;
			}
			for (int i = 0; i < 32; i++)
			{
				if ((value & (1 << i)) != 0)
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x040000AA RID: 170
		private static readonly TMP_Dropdown.OptionData k_NothingOption = new TMP_Dropdown.OptionData
		{
			text = "Nothing"
		};

		// Token: 0x040000AB RID: 171
		private static readonly TMP_Dropdown.OptionData k_EverythingOption = new TMP_Dropdown.OptionData
		{
			text = "Everything"
		};

		// Token: 0x040000AC RID: 172
		private static readonly TMP_Dropdown.OptionData k_MixedOption = new TMP_Dropdown.OptionData
		{
			text = "Mixed..."
		};

		// Token: 0x040000AD RID: 173
		[SerializeField]
		private RectTransform m_Template;

		// Token: 0x040000AE RID: 174
		[SerializeField]
		private TMP_Text m_CaptionText;

		// Token: 0x040000AF RID: 175
		[SerializeField]
		private Image m_CaptionImage;

		// Token: 0x040000B0 RID: 176
		[SerializeField]
		private Graphic m_Placeholder;

		// Token: 0x040000B1 RID: 177
		[Space]
		[SerializeField]
		private TMP_Text m_ItemText;

		// Token: 0x040000B2 RID: 178
		[SerializeField]
		private Image m_ItemImage;

		// Token: 0x040000B3 RID: 179
		[Space]
		[SerializeField]
		private int m_Value;

		// Token: 0x040000B4 RID: 180
		[SerializeField]
		private bool m_MultiSelect;

		// Token: 0x040000B5 RID: 181
		[Space]
		[SerializeField]
		private TMP_Dropdown.OptionDataList m_Options = new TMP_Dropdown.OptionDataList();

		// Token: 0x040000B6 RID: 182
		[Space]
		[SerializeField]
		private TMP_Dropdown.DropdownEvent m_OnValueChanged = new TMP_Dropdown.DropdownEvent();

		// Token: 0x040000B7 RID: 183
		[SerializeField]
		private float m_AlphaFadeSpeed = 0.15f;

		// Token: 0x040000B8 RID: 184
		private GameObject m_Dropdown;

		// Token: 0x040000B9 RID: 185
		private GameObject m_Blocker;

		// Token: 0x040000BA RID: 186
		private List<TMP_Dropdown.DropdownItem> m_Items = new List<TMP_Dropdown.DropdownItem>();

		// Token: 0x040000BB RID: 187
		private TweenRunner<FloatTween> m_AlphaTweenRunner;

		// Token: 0x040000BC RID: 188
		private bool validTemplate;

		// Token: 0x040000BD RID: 189
		private Coroutine m_Coroutine;

		// Token: 0x040000BE RID: 190
		private static TMP_Dropdown.OptionData s_NoOptionData = new TMP_Dropdown.OptionData();

		// Token: 0x0200002A RID: 42
		protected internal class DropdownItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, ICancelHandler
		{
			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000532E File Offset: 0x0000352E
			// (set) Token: 0x060000F9 RID: 249 RVA: 0x00005336 File Offset: 0x00003536
			public TMP_Text text
			{
				get
				{
					return this.m_Text;
				}
				set
				{
					this.m_Text = value;
				}
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060000FA RID: 250 RVA: 0x0000533F File Offset: 0x0000353F
			// (set) Token: 0x060000FB RID: 251 RVA: 0x00005347 File Offset: 0x00003547
			public Image image
			{
				get
				{
					return this.m_Image;
				}
				set
				{
					this.m_Image = value;
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000FC RID: 252 RVA: 0x00005350 File Offset: 0x00003550
			// (set) Token: 0x060000FD RID: 253 RVA: 0x00005358 File Offset: 0x00003558
			public RectTransform rectTransform
			{
				get
				{
					return this.m_RectTransform;
				}
				set
				{
					this.m_RectTransform = value;
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000FE RID: 254 RVA: 0x00005361 File Offset: 0x00003561
			// (set) Token: 0x060000FF RID: 255 RVA: 0x00005369 File Offset: 0x00003569
			public Toggle toggle
			{
				get
				{
					return this.m_Toggle;
				}
				set
				{
					this.m_Toggle = value;
				}
			}

			// Token: 0x06000100 RID: 256 RVA: 0x00005372 File Offset: 0x00003572
			public virtual void OnPointerEnter(PointerEventData eventData)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}

			// Token: 0x06000101 RID: 257 RVA: 0x00005384 File Offset: 0x00003584
			public virtual void OnCancel(BaseEventData eventData)
			{
				TMP_Dropdown dropdown = base.GetComponentInParent<TMP_Dropdown>();
				if (dropdown)
				{
					dropdown.Hide();
				}
			}

			// Token: 0x040000BF RID: 191
			[SerializeField]
			private TMP_Text m_Text;

			// Token: 0x040000C0 RID: 192
			[SerializeField]
			private Image m_Image;

			// Token: 0x040000C1 RID: 193
			[SerializeField]
			private RectTransform m_RectTransform;

			// Token: 0x040000C2 RID: 194
			[SerializeField]
			private Toggle m_Toggle;
		}

		// Token: 0x0200002B RID: 43
		[Serializable]
		public class OptionData
		{
			// Token: 0x1700003D RID: 61
			// (get) Token: 0x06000103 RID: 259 RVA: 0x000053AE File Offset: 0x000035AE
			// (set) Token: 0x06000104 RID: 260 RVA: 0x000053B6 File Offset: 0x000035B6
			public string text
			{
				get
				{
					return this.m_Text;
				}
				set
				{
					this.m_Text = value;
				}
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x06000105 RID: 261 RVA: 0x000053BF File Offset: 0x000035BF
			// (set) Token: 0x06000106 RID: 262 RVA: 0x000053C7 File Offset: 0x000035C7
			public Sprite image
			{
				get
				{
					return this.m_Image;
				}
				set
				{
					this.m_Image = value;
				}
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x06000107 RID: 263 RVA: 0x000053D0 File Offset: 0x000035D0
			// (set) Token: 0x06000108 RID: 264 RVA: 0x000053D8 File Offset: 0x000035D8
			public Color color
			{
				get
				{
					return this.m_Color;
				}
				set
				{
					this.m_Color = value;
				}
			}

			// Token: 0x06000109 RID: 265 RVA: 0x000053E1 File Offset: 0x000035E1
			public OptionData()
			{
			}

			// Token: 0x0600010A RID: 266 RVA: 0x000053F4 File Offset: 0x000035F4
			public OptionData(string text)
			{
				this.text = text;
			}

			// Token: 0x0600010B RID: 267 RVA: 0x0000540E File Offset: 0x0000360E
			public OptionData(Sprite image)
			{
				this.image = image;
			}

			// Token: 0x0600010C RID: 268 RVA: 0x00005428 File Offset: 0x00003628
			public OptionData(string text, Sprite image, Color color)
			{
				this.text = text;
				this.image = image;
				this.color = color;
			}

			// Token: 0x040000C3 RID: 195
			[SerializeField]
			private string m_Text;

			// Token: 0x040000C4 RID: 196
			[SerializeField]
			private Sprite m_Image;

			// Token: 0x040000C5 RID: 197
			[SerializeField]
			private Color m_Color = Color.white;
		}

		// Token: 0x0200002C RID: 44
		[Serializable]
		public class OptionDataList
		{
			// Token: 0x17000040 RID: 64
			// (get) Token: 0x0600010D RID: 269 RVA: 0x00005450 File Offset: 0x00003650
			// (set) Token: 0x0600010E RID: 270 RVA: 0x00005458 File Offset: 0x00003658
			public List<TMP_Dropdown.OptionData> options
			{
				get
				{
					return this.m_Options;
				}
				set
				{
					this.m_Options = value;
				}
			}

			// Token: 0x0600010F RID: 271 RVA: 0x00005461 File Offset: 0x00003661
			public OptionDataList()
			{
				this.options = new List<TMP_Dropdown.OptionData>();
			}

			// Token: 0x040000C6 RID: 198
			[SerializeField]
			private List<TMP_Dropdown.OptionData> m_Options;
		}

		// Token: 0x0200002D RID: 45
		[Serializable]
		public class DropdownEvent : UnityEvent<int>
		{
		}
	}
}
