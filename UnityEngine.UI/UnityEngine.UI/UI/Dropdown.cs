using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using UnityEngine.UI.CoroutineTween;

namespace UnityEngine.UI
{
	// Token: 0x02000015 RID: 21
	[AddComponentMenu("UI/Legacy/Dropdown", 102)]
	[RequireComponent(typeof(RectTransform))]
	public class Dropdown : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004116 File Offset: 0x00002316
		// (set) Token: 0x06000073 RID: 115 RVA: 0x0000411E File Offset: 0x0000231E
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000412D File Offset: 0x0000232D
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00004135 File Offset: 0x00002335
		public Text captionText
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004144 File Offset: 0x00002344
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000414C File Offset: 0x0000234C
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000415B File Offset: 0x0000235B
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00004163 File Offset: 0x00002363
		public Text itemText
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004172 File Offset: 0x00002372
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000417A File Offset: 0x0000237A
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004189 File Offset: 0x00002389
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00004196 File Offset: 0x00002396
		public List<Dropdown.OptionData> options
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000041AA File Offset: 0x000023AA
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000041B2 File Offset: 0x000023B2
		public Dropdown.DropdownEvent onValueChanged
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000041BB File Offset: 0x000023BB
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000041C3 File Offset: 0x000023C3
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000041CC File Offset: 0x000023CC
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000041D4 File Offset: 0x000023D4
		public int value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.Set(value, true);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000041DE File Offset: 0x000023DE
		public void SetValueWithoutNotify(int input)
		{
			this.Set(input, false);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000041E8 File Offset: 0x000023E8
		private void Set(int value, bool sendCallback = true)
		{
			if (Application.isPlaying && (value == this.m_Value || this.options.Count == 0))
			{
				return;
			}
			this.m_Value = Mathf.Clamp(value, 0, this.options.Count - 1);
			this.RefreshShownValue();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Dropdown.value", this);
				this.m_OnValueChanged.Invoke(this.m_Value);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004252 File Offset: 0x00002452
		protected Dropdown()
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004288 File Offset: 0x00002488
		protected override void Awake()
		{
			if (this.m_CaptionImage)
			{
				this.m_CaptionImage.enabled = this.m_CaptionImage.sprite != null;
			}
			if (this.m_Template)
			{
				this.m_Template.gameObject.SetActive(false);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000042DC File Offset: 0x000024DC
		protected override void Start()
		{
			this.m_AlphaTweenRunner = new TweenRunner<FloatTween>();
			this.m_AlphaTweenRunner.Init(this);
			base.Start();
			this.RefreshShownValue();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004301 File Offset: 0x00002501
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

		// Token: 0x0600008A RID: 138 RVA: 0x00004330 File Offset: 0x00002530
		public void RefreshShownValue()
		{
			Dropdown.OptionData data = Dropdown.s_NoOptionData;
			if (this.options.Count > 0)
			{
				data = this.options[Mathf.Clamp(this.m_Value, 0, this.options.Count - 1)];
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
				if (data != null)
				{
					this.m_CaptionImage.sprite = data.image;
				}
				else
				{
					this.m_CaptionImage.sprite = null;
				}
				this.m_CaptionImage.enabled = this.m_CaptionImage.sprite != null;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000043FC File Offset: 0x000025FC
		public void AddOptions(List<Dropdown.OptionData> options)
		{
			this.options.AddRange(options);
			this.RefreshShownValue();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004410 File Offset: 0x00002610
		public void AddOptions(List<string> options)
		{
			int optionsCount = options.Count;
			for (int i = 0; i < optionsCount; i++)
			{
				this.options.Add(new Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004450 File Offset: 0x00002650
		public void AddOptions(List<Sprite> options)
		{
			int optionsCount = options.Count;
			for (int i = 0; i < optionsCount; i++)
			{
				this.options.Add(new Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000448D File Offset: 0x0000268D
		public void ClearOptions()
		{
			this.options.Clear();
			this.m_Value = 0;
			this.RefreshShownValue();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000044A8 File Offset: 0x000026A8
		private void SetupTemplate(Canvas rootCanvas)
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
			Dropdown.DropdownItem dropdownItem = itemToggle.gameObject.AddComponent<Dropdown.DropdownItem>();
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
			Canvas canvas;
			if (!templateGo.TryGetComponent<Canvas>(out canvas))
			{
				Canvas canvas2 = templateGo.AddComponent<Canvas>();
				canvas2.overrideSorting = true;
				canvas2.sortingOrder = 30000;
				canvas2.sortingLayerID = rootCanvas.sortingLayerID;
			}
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
				Dropdown.GetOrAddComponent<GraphicRaycaster>(templateGo);
			}
			Dropdown.GetOrAddComponent<CanvasGroup>(templateGo);
			templateGo.SetActive(false);
			this.validTemplate = true;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000046EC File Offset: 0x000028EC
		private static T GetOrAddComponent<T>(GameObject go) where T : Component
		{
			T comp = go.GetComponent<T>();
			if (!comp)
			{
				comp = go.AddComponent<T>();
			}
			return comp;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004715 File Offset: 0x00002915
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Show();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004715 File Offset: 0x00002915
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Show();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000471D File Offset: 0x0000291D
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Hide();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004728 File Offset: 0x00002928
		public void Show()
		{
			if (!this.IsActive() || !this.IsInteractable() || this.m_Dropdown != null)
			{
				return;
			}
			List<Canvas> list = CollectionPool<List<Canvas>, Canvas>.Get();
			base.gameObject.GetComponentsInParent<Canvas>(false, list);
			if (list.Count == 0)
			{
				return;
			}
			int listCount = list.Count;
			Canvas rootCanvas = list[listCount - 1];
			for (int i = 0; i < listCount; i++)
			{
				if (list[i].isRootCanvas || list[i].overrideSorting)
				{
					rootCanvas = list[i];
					break;
				}
			}
			CollectionPool<List<Canvas>, Canvas>.Release(list);
			if (!this.validTemplate)
			{
				this.SetupTemplate(rootCanvas);
				if (!this.validTemplate)
				{
					return;
				}
			}
			this.m_Template.gameObject.SetActive(true);
			this.m_Dropdown = this.CreateDropdownList(this.m_Template.gameObject);
			this.m_Dropdown.name = "Dropdown List";
			this.m_Dropdown.SetActive(true);
			RectTransform dropdownRectTransform = this.m_Dropdown.transform as RectTransform;
			dropdownRectTransform.SetParent(this.m_Template.transform.parent, false);
			Dropdown.DropdownItem itemTemplate = this.m_Dropdown.GetComponentInChildren<Dropdown.DropdownItem>();
			RectTransform contentRectTransform = itemTemplate.rectTransform.parent.gameObject.transform as RectTransform;
			itemTemplate.rectTransform.gameObject.SetActive(true);
			Rect dropdownContentRect = contentRectTransform.rect;
			Rect itemTemplateRect = itemTemplate.rectTransform.rect;
			Vector2 offsetMin = itemTemplateRect.min - dropdownContentRect.min + itemTemplate.rectTransform.localPosition;
			Vector2 offsetMax = itemTemplateRect.max - dropdownContentRect.max + itemTemplate.rectTransform.localPosition;
			Vector2 itemSize = itemTemplateRect.size;
			this.m_Items.Clear();
			Toggle prev = null;
			int optionsCount = this.options.Count;
			for (int j = 0; j < optionsCount; j++)
			{
				Dropdown.OptionData data = this.options[j];
				Dropdown.DropdownItem item = this.AddItem(data, this.value == j, itemTemplate, this.m_Items);
				if (!(item == null))
				{
					item.toggle.isOn = this.value == j;
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
						Navigation prevNav = prev.navigation;
						Navigation toggleNav = item.toggle.navigation;
						prevNav.mode = Navigation.Mode.Explicit;
						toggleNav.mode = Navigation.Mode.Explicit;
						prevNav.selectOnDown = item.toggle;
						prevNav.selectOnRight = item.toggle;
						toggleNav.selectOnLeft = prev;
						toggleNav.selectOnUp = prev;
						prev.navigation = prevNav;
						item.toggle.navigation = toggleNav;
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
			int itemsCount = this.m_Items.Count;
			for (int l = 0; l < itemsCount; l++)
			{
				RectTransform itemRect = this.m_Items[l].rectTransform;
				itemRect.anchorMin = new Vector2(itemRect.anchorMin.x, 0f);
				itemRect.anchorMax = new Vector2(itemRect.anchorMax.x, 0f);
				itemRect.anchoredPosition = new Vector2(itemRect.anchoredPosition.x, offsetMin.y + itemSize.y * (float)(itemsCount - 1 - l) + itemSize.y * itemRect.pivot.y);
				itemRect.sizeDelta = new Vector2(itemRect.sizeDelta.x, itemSize.y);
			}
			this.AlphaFadeList(this.m_AlphaFadeSpeed, 0f, 1f);
			this.m_Template.gameObject.SetActive(false);
			itemTemplate.gameObject.SetActive(false);
			this.m_Blocker = this.CreateBlocker(rootCanvas);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004D14 File Offset: 0x00002F14
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
				Dropdown.GetOrAddComponent<GraphicRaycaster>(blocker);
			}
			blocker.AddComponent<Image>().color = Color.clear;
			blocker.AddComponent<Button>().onClick.AddListener(new UnityAction(this.Hide));
			blocker.AddComponent<CanvasGroup>().ignoreParentGroups = true;
			return blocker;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004E6B File Offset: 0x0000306B
		protected virtual void DestroyBlocker(GameObject blocker)
		{
			Object.Destroy(blocker);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004E73 File Offset: 0x00003073
		protected virtual GameObject CreateDropdownList(GameObject template)
		{
			return Object.Instantiate<GameObject>(template);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004E6B File Offset: 0x0000306B
		protected virtual void DestroyDropdownList(GameObject dropdownList)
		{
			Object.Destroy(dropdownList);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004E7B File Offset: 0x0000307B
		protected virtual Dropdown.DropdownItem CreateItem(Dropdown.DropdownItem itemTemplate)
		{
			return Object.Instantiate<Dropdown.DropdownItem>(itemTemplate);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002209 File Offset: 0x00000409
		protected virtual void DestroyItem(Dropdown.DropdownItem item)
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004E84 File Offset: 0x00003084
		private Dropdown.DropdownItem AddItem(Dropdown.OptionData data, bool selected, Dropdown.DropdownItem itemTemplate, List<Dropdown.DropdownItem> items)
		{
			Dropdown.DropdownItem item = this.CreateItem(itemTemplate);
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
				item.image.enabled = item.image.sprite != null;
			}
			items.Add(item);
			return item;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004F7C File Offset: 0x0000317C
		private void AlphaFadeList(float duration, float alpha)
		{
			CanvasGroup group = this.m_Dropdown.GetComponent<CanvasGroup>();
			this.AlphaFadeList(duration, group.alpha, alpha);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004FA4 File Offset: 0x000031A4
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

		// Token: 0x0600009E RID: 158 RVA: 0x00005005 File Offset: 0x00003205
		private void SetAlpha(float alpha)
		{
			if (!this.m_Dropdown)
			{
				return;
			}
			this.m_Dropdown.GetComponent<CanvasGroup>().alpha = alpha;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005028 File Offset: 0x00003228
		public void Hide()
		{
			if (this.m_Dropdown != null)
			{
				this.AlphaFadeList(this.m_AlphaFadeSpeed, 0f);
				if (this.IsActive())
				{
					base.StartCoroutine(this.DelayedDestroyDropdownList(this.m_AlphaFadeSpeed));
				}
			}
			if (this.m_Blocker != null)
			{
				this.DestroyBlocker(this.m_Blocker);
			}
			this.m_Blocker = null;
			this.Select();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005096 File Offset: 0x00003296
		private IEnumerator DelayedDestroyDropdownList(float delay)
		{
			yield return new WaitForSecondsRealtime(delay);
			this.ImmediateDestroyDropdownList();
			yield break;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000050AC File Offset: 0x000032AC
		private void ImmediateDestroyDropdownList()
		{
			int itemsCount = this.m_Items.Count;
			for (int i = 0; i < itemsCount; i++)
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
			this.m_Dropdown = null;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005124 File Offset: 0x00003324
		private void OnSelectItem(Toggle toggle)
		{
			if (!toggle.isOn)
			{
				toggle.isOn = true;
			}
			int selectedIndex = -1;
			Transform tr = toggle.transform;
			Transform parent = tr.parent;
			for (int i = 0; i < parent.childCount; i++)
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
			this.value = selectedIndex;
			this.Hide();
		}

		// Token: 0x04000043 RID: 67
		[SerializeField]
		private RectTransform m_Template;

		// Token: 0x04000044 RID: 68
		[SerializeField]
		private Text m_CaptionText;

		// Token: 0x04000045 RID: 69
		[SerializeField]
		private Image m_CaptionImage;

		// Token: 0x04000046 RID: 70
		[Space]
		[SerializeField]
		private Text m_ItemText;

		// Token: 0x04000047 RID: 71
		[SerializeField]
		private Image m_ItemImage;

		// Token: 0x04000048 RID: 72
		[Space]
		[SerializeField]
		private int m_Value;

		// Token: 0x04000049 RID: 73
		[Space]
		[SerializeField]
		private Dropdown.OptionDataList m_Options = new Dropdown.OptionDataList();

		// Token: 0x0400004A RID: 74
		[Space]
		[SerializeField]
		private Dropdown.DropdownEvent m_OnValueChanged = new Dropdown.DropdownEvent();

		// Token: 0x0400004B RID: 75
		[SerializeField]
		private float m_AlphaFadeSpeed = 0.15f;

		// Token: 0x0400004C RID: 76
		private GameObject m_Dropdown;

		// Token: 0x0400004D RID: 77
		private GameObject m_Blocker;

		// Token: 0x0400004E RID: 78
		private List<Dropdown.DropdownItem> m_Items = new List<Dropdown.DropdownItem>();

		// Token: 0x0400004F RID: 79
		private TweenRunner<FloatTween> m_AlphaTweenRunner;

		// Token: 0x04000050 RID: 80
		private bool validTemplate;

		// Token: 0x04000051 RID: 81
		private const int kHighSortingLayer = 30000;

		// Token: 0x04000052 RID: 82
		private static Dropdown.OptionData s_NoOptionData = new Dropdown.OptionData();

		// Token: 0x02000016 RID: 22
		protected internal class DropdownItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, ICancelHandler
		{
			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000A4 RID: 164 RVA: 0x00005194 File Offset: 0x00003394
			// (set) Token: 0x060000A5 RID: 165 RVA: 0x0000519C File Offset: 0x0000339C
			public Text text
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

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000A6 RID: 166 RVA: 0x000051A5 File Offset: 0x000033A5
			// (set) Token: 0x060000A7 RID: 167 RVA: 0x000051AD File Offset: 0x000033AD
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

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000A8 RID: 168 RVA: 0x000051B6 File Offset: 0x000033B6
			// (set) Token: 0x060000A9 RID: 169 RVA: 0x000051BE File Offset: 0x000033BE
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

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000AA RID: 170 RVA: 0x000051C7 File Offset: 0x000033C7
			// (set) Token: 0x060000AB RID: 171 RVA: 0x000051CF File Offset: 0x000033CF
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

			// Token: 0x060000AC RID: 172 RVA: 0x000051D8 File Offset: 0x000033D8
			public virtual void OnPointerEnter(PointerEventData eventData)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}

			// Token: 0x060000AD RID: 173 RVA: 0x000051EC File Offset: 0x000033EC
			public virtual void OnCancel(BaseEventData eventData)
			{
				Dropdown dropdown = base.GetComponentInParent<Dropdown>();
				if (dropdown)
				{
					dropdown.Hide();
				}
			}

			// Token: 0x04000053 RID: 83
			[SerializeField]
			private Text m_Text;

			// Token: 0x04000054 RID: 84
			[SerializeField]
			private Image m_Image;

			// Token: 0x04000055 RID: 85
			[SerializeField]
			private RectTransform m_RectTransform;

			// Token: 0x04000056 RID: 86
			[SerializeField]
			private Toggle m_Toggle;
		}

		// Token: 0x02000017 RID: 23
		[Serializable]
		public class OptionData
		{
			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000AF RID: 175 RVA: 0x00005216 File Offset: 0x00003416
			// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000521E File Offset: 0x0000341E
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

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060000B1 RID: 177 RVA: 0x00005227 File Offset: 0x00003427
			// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000522F File Offset: 0x0000342F
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

			// Token: 0x060000B3 RID: 179 RVA: 0x000020BB File Offset: 0x000002BB
			public OptionData()
			{
			}

			// Token: 0x060000B4 RID: 180 RVA: 0x00005238 File Offset: 0x00003438
			public OptionData(string text)
			{
				this.text = text;
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x00005247 File Offset: 0x00003447
			public OptionData(Sprite image)
			{
				this.image = image;
			}

			// Token: 0x060000B6 RID: 182 RVA: 0x00005256 File Offset: 0x00003456
			public OptionData(string text, Sprite image)
			{
				this.text = text;
				this.image = image;
			}

			// Token: 0x04000057 RID: 87
			[SerializeField]
			private string m_Text;

			// Token: 0x04000058 RID: 88
			[SerializeField]
			private Sprite m_Image;
		}

		// Token: 0x02000018 RID: 24
		[Serializable]
		public class OptionDataList
		{
			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000526C File Offset: 0x0000346C
			// (set) Token: 0x060000B8 RID: 184 RVA: 0x00005274 File Offset: 0x00003474
			public List<Dropdown.OptionData> options
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

			// Token: 0x060000B9 RID: 185 RVA: 0x0000527D File Offset: 0x0000347D
			public OptionDataList()
			{
				this.options = new List<Dropdown.OptionData>();
			}

			// Token: 0x04000059 RID: 89
			[SerializeField]
			private List<Dropdown.OptionData> m_Options;
		}

		// Token: 0x02000019 RID: 25
		[Serializable]
		public class DropdownEvent : UnityEvent<int>
		{
		}
	}
}
