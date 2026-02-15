using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace YgomSystem.UI
{
	// Token: 0x020005C7 RID: 1479
	public class SelectionButton : RectSelectionItem
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06002E8D RID: 11917 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002E8E RID: 11918 RVA: 0x0000216D File Offset: 0x0000036D
		public int colorContainerIndex
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06002E8F RID: 11919 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002E90 RID: 11920 RVA: 0x0000216D File Offset: 0x0000036D
		public string soundLabelClick
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06002E91 RID: 11921 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002E92 RID: 11922 RVA: 0x0000216D File Offset: 0x0000036D
		public string soundLabelClickInactive
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002E94 RID: 11924 RVA: 0x0000216D File Offset: 0x0000036D
		public string soundLabelPointerEnter
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06002E95 RID: 11925 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002E96 RID: 11926 RVA: 0x0000216D File Offset: 0x0000036D
		public string soundLabelSelectedGamePad
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06002E97 RID: 11927 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002E98 RID: 11928 RVA: 0x0000216D File Offset: 0x0000036D
		public override bool interactable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06002E99 RID: 11929 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002E9A RID: 11930 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isClickExclusive
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06002E9B RID: 11931 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002E9C RID: 11932 RVA: 0x0000216D File Offset: 0x0000036D
		public event SelectionButton.PointerCallback onEnter
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06002E9D RID: 11933 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002E9E RID: 11934 RVA: 0x0000216D File Offset: 0x0000036D
		public event SelectionButton.PointerCallback onExit
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06002E9F RID: 11935 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002EA0 RID: 11936 RVA: 0x0000216D File Offset: 0x0000036D
		public event SelectionButton.PointerCallback onDown
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06002EA1 RID: 11937 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002EA2 RID: 11938 RVA: 0x0000216D File Offset: 0x0000036D
		public event SelectionButton.PointerCallback onUp
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Awaked()
		{
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Destroyed()
		{
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x000029CC File Offset: 0x00000BCC
		public override SelectionItem.UpdateItemStatus UpdateItem()
		{
			return SelectionItem.UpdateItemStatus.Unknown;
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000029CC File Offset: 0x00000BCC
		public override SelectionItem.UpdateItemStatus UpdateSelectedItem()
		{
			return SelectionItem.UpdateItemStatus.Unknown;
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupColorContainer()
		{
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetColorContainerColor(ColorContainer.SelectMode select_mode, ColorContainer.StatusMode status_mode)
		{
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetColorContainerIndex(int index)
		{
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnPointerClick()
		{
			return false;
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnPointerDoubleClick()
		{
			return false;
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnPointerDown()
		{
			return false;
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x0000216D File Offset: 0x0000036D
		public void PointerDownEffect()
		{
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnPointerEnter()
		{
			return false;
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x0000216D File Offset: 0x0000036D
		public void PointerEnterEffect(bool pointerDown)
		{
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnPointerExit()
		{
			return false;
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x0000216D File Offset: 0x0000036D
		public void PointerExitEffect(bool pointerDown)
		{
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnPointerUp()
		{
			return false;
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x0000216D File Offset: 0x0000036D
		public void PointerUpEffect()
		{
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnSelected(bool initializeSelection = false)
		{
			return false;
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectEffect(bool initializeSelection = false)
		{
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnDeselected()
		{
			return false;
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x0000216D File Offset: 0x0000036D
		public void DeselectEffect()
		{
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClick()
		{
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDoubleClick()
		{
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x0000216D File Offset: 0x0000036D
		public void Click()
		{
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool ClickFunc()
		{
			return false;
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayTween(string play_label, string stop_label = null)
		{
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlaySound(string label)
		{
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetClickShortCutKeyDown(int gamepad_key_type)
		{
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetClickShortCutKeyDown(SelectorManager.KeyType key_type)
		{
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetClickShortCutKeyDown(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub)
		{
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveClickShortCutKeyDown(SelectorManager.KeyType key_type)
		{
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveClickShortCutKeyDown(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub)
		{
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetClickShortCutKeyRelease(SelectorManager.KeyType key_type)
		{
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetClickShortCutKeyRelease(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub)
		{
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveClickShortCutKeyRelease(SelectorManager.KeyType key_type)
		{
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveClickShortCutKeyRelease(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub)
		{
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetClickShortCutMouseRelease(SelectorManager.MouseType mouse_type)
		{
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveClickShortCutMouseRelease(SelectorManager.MouseType mouse_type)
		{
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetClickShortCutMouseDown(SelectorManager.MouseType mouse_type)
		{
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveClickShortCutMouseDown(SelectorManager.MouseType mouse_type)
		{
		}

		// Token: 0x04002C25 RID: 11301
		private Dictionary<GameObject, List<ColorContainer>> colorContainers;

		// Token: 0x04002C26 RID: 11302
		[SerializeField]
		private string tweenLabelInitialize;

		// Token: 0x04002C27 RID: 11303
		[SerializeField]
		private string tweenLabelEnter;

		// Token: 0x04002C28 RID: 11304
		[SerializeField]
		private string tweenLabelDown;

		// Token: 0x04002C29 RID: 11305
		[SerializeField]
		private string tweenLabelClick;

		// Token: 0x04002C2A RID: 11306
		[SerializeField]
		private string tweenLabelExit;

		// Token: 0x04002C2B RID: 11307
		[SerializeField]
		private string tweenLabelSelected;

		// Token: 0x04002C2C RID: 11308
		[SerializeField]
		private string tweenLabelDeselected;

		// Token: 0x04002C2D RID: 11309
		[SerializeField]
		public bool playTweenInitialize;

		// Token: 0x04002C2E RID: 11310
		[SerializeField]
		public bool playTweenEnter;

		// Token: 0x04002C2F RID: 11311
		[SerializeField]
		public bool playTweenDown;

		// Token: 0x04002C30 RID: 11312
		[SerializeField]
		public bool playTweenClick;

		// Token: 0x04002C31 RID: 11313
		[SerializeField]
		public bool playTweenExit;

		// Token: 0x04002C32 RID: 11314
		[SerializeField]
		public bool playTweenSelected;

		// Token: 0x04002C33 RID: 11315
		[SerializeField]
		public bool playTweenDeselected;

		// Token: 0x04002C34 RID: 11316
		[SerializeField]
		public bool playSound;

		// Token: 0x04002C35 RID: 11317
		[SerializeField]
		private string _soundLabelClick;

		// Token: 0x04002C36 RID: 11318
		[SerializeField]
		private string _soundLabelClickInactive;

		// Token: 0x04002C37 RID: 11319
		[SerializeField]
		private string _soundLabelPointerEnter;

		// Token: 0x04002C38 RID: 11320
		[SerializeField]
		private string _soundLabelSelectedGamePad;

		// Token: 0x04002C39 RID: 11321
		private ColorContainer.SelectMode currentSelectMode;

		// Token: 0x04002C3A RID: 11322
		private ColorContainer.StatusMode currentStatusMode;

		// Token: 0x04002C3B RID: 11323
		public SelectionButton.OnClickEvent onClick;

		// Token: 0x04002C3C RID: 11324
		public SelectionButton.OnClickEvent onDoubleClick;

		// Token: 0x020005C8 RID: 1480
		[Serializable]
		public class OnClickEvent : UnityEvent
		{
		}

		// Token: 0x020005C9 RID: 1481
		// (Invoke) Token: 0x06002ECE RID: 11982
		public delegate void PointerCallback(SelectionItem this_item);
	}
}
