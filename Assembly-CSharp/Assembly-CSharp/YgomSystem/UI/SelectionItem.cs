using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace YgomSystem.UI
{
	// Token: 0x020005CC RID: 1484
	public class SelectionItem : MonoBehaviour
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x0000216A File Offset: 0x0000036A
		public Selector selector
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06002EDC RID: 11996 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isDefaultItem
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06002EDD RID: 11997 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06002EDE RID: 11998 RVA: 0x0000216D File Offset: 0x0000036D
		public float selectionAngle
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06002EDF RID: 11999 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002EE0 RID: 12000 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isSelected
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06002EE1 RID: 12001 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002EE2 RID: 12002 RVA: 0x0000216D File Offset: 0x0000036D
		public bool selectable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06002EE3 RID: 12003 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002EE4 RID: 12004 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual bool interactable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06002EE5 RID: 12005 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isActivate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06002EE6 RID: 12006 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isClusterActivated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002EE8 RID: 12008 RVA: 0x0000216D File Offset: 0x0000036D
		public bool useDoubleClick
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06002EE9 RID: 12009 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isRegisted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06002EEA RID: 12010 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06002EEB RID: 12011 RVA: 0x0000216D File Offset: 0x0000036D
		public float priority
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06002EEC RID: 12012 RVA: 0x000F20E4 File Offset: 0x000F02E4
		public virtual Vector2 viewCenterPosition
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06002EED RID: 12013 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002EEE RID: 12014 RVA: 0x0000216D File Offset: 0x0000036D
		public event SelectionItem.InputCallback upInputCallback
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

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06002EEF RID: 12015 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002EF0 RID: 12016 RVA: 0x0000216D File Offset: 0x0000036D
		public event SelectionItem.InputCallback downInputCallback
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

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06002EF1 RID: 12017 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002EF2 RID: 12018 RVA: 0x0000216D File Offset: 0x0000036D
		public event SelectionItem.InputCallback rightInputCallback
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

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06002EF3 RID: 12019 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002EF4 RID: 12020 RVA: 0x0000216D File Offset: 0x0000036D
		public event SelectionItem.InputCallback leftInputCallback
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

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06002EF5 RID: 12021 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002EF6 RID: 12022 RVA: 0x0000216D File Offset: 0x0000036D
		public event SelectionItem.InputCallback upTransitionFailedCallback
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

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06002EF7 RID: 12023 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002EF8 RID: 12024 RVA: 0x0000216D File Offset: 0x0000036D
		public event SelectionItem.InputCallback downTransitionFailedCallback
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

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06002EF9 RID: 12025 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002EFA RID: 12026 RVA: 0x0000216D File Offset: 0x0000036D
		public event SelectionItem.InputCallback rightTransitionFailedCallback
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

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06002EFB RID: 12027 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002EFC RID: 12028 RVA: 0x0000216D File Offset: 0x0000036D
		public event SelectionItem.InputCallback leftTransitionFailedCallback
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

		// Token: 0x06002EFD RID: 12029 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Awaked()
		{
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Enabled()
		{
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Disabled()
		{
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Destroyed()
		{
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual SelectionItem.UpdateItemStatus UpdateItem()
		{
			return SelectionItem.UpdateItemStatus.Unknown;
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual SelectionItem.UpdateItemStatus UpdateSelectedItem()
		{
			return SelectionItem.UpdateItemStatus.Unknown;
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x000F20FC File Offset: 0x000F02FC
		public virtual Vector2 GetClosestPoint(Vector2 base_position, Vector2 direction, bool contains_check = true)
		{
			return default(Vector2);
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool IsRectContains(Vector2 rect_point0, Vector2 rect_point1, Vector2 rect_point2, Vector2 rect_point3, bool containedComplete)
		{
			return false;
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool IsContainsPoint(Vector2 view_position)
		{
			return false;
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Select(bool initializeSelection = false, bool force = false)
		{
			return false;
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Reselect(bool initializeSelection = false)
		{
			return false;
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnSelected(bool initializeSelection = false)
		{
			return false;
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnDeselected()
		{
			return false;
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedCallback(UnityAction on_selected_callback)
		{
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveAllOnSelectedCallback()
		{
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnDeselectedCallback(UnityAction on_deselected_callback)
		{
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveAllOnDeselectedCallback()
		{
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetKeyCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, Func<bool> callback, SelectorManager.KeyStatus keyStatus, bool isSelected)
		{
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ClearKeyCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, Func<bool> callback, SelectorManager.KeyStatus keyStatus, bool isSelected)
		{
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ClearKeyCallbackAll(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, SelectorManager.KeyStatus keyStatus, bool isSelected)
		{
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedKeyDownCallback(SelectorManager.KeyType key_type, Func<bool> callback)
		{
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedKeyDownCallback(SelectorManager.KeyType key_type, UnityAction callback, bool exclusiveFunc = false)
		{
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedKeyDownCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, Func<bool> callback)
		{
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedKeyDownCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, UnityAction callback, bool exclusiveFunc = false)
		{
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnSelectedKeyDownCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, Func<bool> callback)
		{
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnSelectedKeyDownCallbackAll(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub)
		{
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedKeyCallback(SelectorManager.KeyType key_type, Func<bool> callback)
		{
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedKeyCallback(SelectorManager.KeyType key_type, UnityAction callback, bool exclusiveFunc = false)
		{
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedKeyCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, Func<bool> callback)
		{
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedKeyCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, UnityAction callback, bool exclusiveFunc = false)
		{
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnSelectedKeyCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, Func<bool> callback)
		{
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnSelectedKeyCallbackAll(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub)
		{
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedKeyUpCallback(SelectorManager.KeyType key_type, Func<bool> callback)
		{
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedKeyUpCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, Func<bool> callback)
		{
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnSelectedKeyUpCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, Func<bool> callback)
		{
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnSelectedKeyUpCallbackAll(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub)
		{
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutKeyDownCallback(SelectorManager.KeyType key_type, Func<bool> callback)
		{
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutKeyDownCallback(SelectorManager.KeyType key_type, UnityAction callback, bool exclusiveFunc = false)
		{
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutKeyDownCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, Func<bool> callback)
		{
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutKeyDownCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, UnityAction callback, bool exclusiveFunc = false)
		{
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnShortCutKeyDownCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, Func<bool> callback)
		{
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnShortCutKeyDownCallbackAll(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub)
		{
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutKeyCallback(SelectorManager.KeyType key_type, Func<bool> callback)
		{
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutKeyCallback(SelectorManager.KeyType key_type, UnityAction callback, bool exclusiveFunc = false)
		{
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutKeyCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, Func<bool> callback)
		{
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutKeyCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, UnityAction callback, bool exclusiveFunc = false)
		{
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnShortCutKeyCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, Func<bool> callback)
		{
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnShortCutKeyCallbackAll(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub)
		{
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutKeyUpCallback(SelectorManager.KeyType key_type, Func<bool> callback)
		{
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutKeyUpCallback(SelectorManager.KeyType key_type, UnityAction callback, bool exclusiveFunc = false)
		{
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutKeyUpCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, Func<bool> callback)
		{
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutKeyUpCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, UnityAction callback, bool exclusiveFunc = false)
		{
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnShortCutKeyUpCallback(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub, Func<bool> callback)
		{
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnShortCutKeyUpCallbackAll(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub)
		{
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedAnalogInputCallback(SelectorManager.AnalogType analog_type, Func<Vector2, bool> callback)
		{
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedAnalogInputCallback(SelectorManager.AnalogType analog_type, Action<Vector2> callback, bool exclusiveFunc = false)
		{
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedAnalogInputCallback(SelectorManager.AnalogType analog_type, SelectorManager.KeyType subKeyType, Action<Vector2> callback, bool exclusiveFunc = false)
		{
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedAnalogInputCallback(SelectorManager.AnalogType analog_type, SelectorManager.KeyType subKeyType, Func<Vector2, bool> callback)
		{
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool InvokeOnSelectedAnalogInputCallback()
		{
			return false;
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutAnalogInputCallback(SelectorManager.AnalogType analog_type, Func<Vector2, bool> callback)
		{
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutAnalogInputCallback(SelectorManager.AnalogType analog_type, Action<Vector2> callback, bool exclusiveFunc = false)
		{
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutAnalogInputCallback(SelectorManager.AnalogType analog_type, SelectorManager.KeyType subKeyType, Action<Vector2> callback, bool exclusiveFunc = false)
		{
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutAnalogInputCallback(SelectorManager.AnalogType analog_type, SelectorManager.KeyType subKeyType, Func<Vector2, bool> callback)
		{
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool InvokeOnShortCutAnalogInputCallback()
		{
			return false;
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedMouseClickCallback(SelectorManager.MouseType mouse_type, UnityAction<bool> callback, bool exclusiveFunc = true)
		{
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedMouseClickCallback(SelectorManager.MouseType mouse_type, Func<bool, bool> callback)
		{
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedMousePushCallback(SelectorManager.MouseType mouse_type, UnityAction<bool> callback, bool exclusiveFunc = true)
		{
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedMousePushCallback(SelectorManager.MouseType mouse_type, Func<bool, bool> callback)
		{
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutMouseClickCallback(SelectorManager.MouseType mouse_type, UnityAction<bool> callback, bool exclusiveFunc = true)
		{
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutMouseClickCallback(SelectorManager.MouseType mouse_type, Func<bool, bool> callback)
		{
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutMousePushCallback(SelectorManager.MouseType mouse_type, UnityAction<bool> callback, bool exclusiveFunc = true)
		{
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnShortCutMousePushCallback(SelectorManager.MouseType mouse_type, Func<bool, bool> callback)
		{
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMouseCallback(SelectorManager.KeyStatus status, SelectorManager.MouseType mouse_type, Func<bool, bool> callback, bool isSelected)
		{
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ClearMouseCallback(SelectorManager.MouseType mouse_type, Func<bool> callback, SelectorManager.KeyStatus keyStatus, bool isSelected)
		{
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ClearMouseCallbackAll(SelectorManager.MouseType mouse_type, SelectorManager.KeyStatus keyStatus, bool isSelected)
		{
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnPointerEnterCallback(UnityAction callback)
		{
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnPointerEnter()
		{
			return false;
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnPointerExitCallback(UnityAction callback)
		{
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnPointerExit()
		{
			return false;
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnPointerDownCallback(UnityAction callback)
		{
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnPointerDown()
		{
			return false;
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnPointerClickCallback(UnityAction callback)
		{
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnPointerClick()
		{
			return false;
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnPointerDoubleClick()
		{
			return false;
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnPointerUpCallback(UnityAction callback)
		{
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnPointerUp()
		{
			return false;
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDragStarter(Func<Vector2, Vector2, bool> drag_starter)
		{
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDragStart(Vector2 screen_point, Vector2 pressed_point)
		{
			return false;
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnDragCallback(UnityAction<SelectionItem.DragStatus, Vector2> callback)
		{
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnDragCallback()
		{
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnDrag(SelectionItem.DragStatus drag_status, Vector2 screen_position)
		{
			return false;
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAsVerticalScrollItem(float dragThreshold = 0.01f)
		{
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAsHorizontalScrollItem(float dragThreshold = 0.01f)
		{
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAsMultiScrollItem(float dragThreshold = 0.01f)
		{
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAsScrollItem(bool verticalScroll, bool horizontalScroll, float dragThreshold = 0.01f)
		{
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetHoldStarter(Func<float, bool> hold_starter)
		{
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsHoldStart(float holdTime)
		{
			return false;
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnHoldCallback(UnityAction<SelectionItem.HoldStatus, Vector2> callback)
		{
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnHoldCallback()
		{
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnHold(SelectionItem.HoldStatus hold_status, Vector2 screen_position)
		{
			return false;
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000029CC File Offset: 0x00000BCC
		public SelectionItem.TransitionMode GetTransitionMode(PadInputDirection direction)
		{
			return SelectionItem.TransitionMode.Automatic;
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTransitionMode(PadInputDirection direction, SelectionItem.TransitionMode mode)
		{
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetManualTransitionItem(PadInputDirection direction)
		{
			return null;
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetManualTransitionItem(PadInputDirection direction, SelectionItem item, bool set_mode_manual = true)
		{
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x0000216A File Offset: 0x0000036A
		public Selector GetManualTransitionSelector(PadInputDirection direction)
		{
			return null;
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetManualTransitionSelector(PadInputDirection direction, Selector selector, bool set_mode_manual = true)
		{
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeInputCallback(PadInputDirection direction)
		{
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddInputCallback(PadInputDirection direction, SelectionItem.InputCallback callback)
		{
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveInputCallback(PadInputDirection direction, SelectionItem.InputCallback callback)
		{
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveInputCallbackAll(PadInputDirection direction)
		{
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddTransitionFailedCallback(PadInputDirection direction, SelectionItem.InputCallback callback)
		{
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveTransitionFailedCallback(PadInputDirection direction, SelectionItem.InputCallback callback)
		{
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveTransitionFailedCallbackAll(PadInputDirection direction)
		{
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeTransitionFailedCallback(PadInputDirection direction)
		{
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x0000216D File Offset: 0x0000036D
		public void InputDirection(PadInputDirection direction)
		{
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetResistStatus(SelectionItem.RegistStatus regist_status)
		{
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAsDefaultItem(bool isDefaultItem)
		{
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupDepth()
		{
		}

		// Token: 0x04002C3D RID: 11325
		private Selector _selector;

		// Token: 0x04002C3E RID: 11326
		[SerializeField]
		private bool defaultItem;

		// Token: 0x04002C3F RID: 11327
		[SerializeField]
		private float _selectionAngle;

		// Token: 0x04002C40 RID: 11328
		protected UnityEvent onSelectedCallback;

		// Token: 0x04002C41 RID: 11329
		protected UnityEvent onDeselectedCallback;

		// Token: 0x04002C42 RID: 11330
		protected Dictionary<ValueTuple<SelectorManager.AnalogType, SelectorManager.KeyType>, SelectionItem.AnalogEvent> onSelectedAnalogInputCallback;

		// Token: 0x04002C43 RID: 11331
		protected Dictionary<ValueTuple<SelectorManager.AnalogType, SelectorManager.KeyType>, SelectionItem.AnalogEvent> onShortCutAnalogInputCallback;

		// Token: 0x04002C44 RID: 11332
		protected UnityEvent onPointerEnterCallback;

		// Token: 0x04002C45 RID: 11333
		protected UnityEvent onPointerExitCallback;

		// Token: 0x04002C46 RID: 11334
		protected UnityEvent onPointerDownCallback;

		// Token: 0x04002C47 RID: 11335
		protected UnityEvent onPointerClickCallback;

		// Token: 0x04002C48 RID: 11336
		protected UnityEvent onPointerDounbleClickCallback;

		// Token: 0x04002C49 RID: 11337
		protected UnityEvent onPointerUpCallback;

		// Token: 0x04002C4A RID: 11338
		protected Func<Vector2, Vector2, bool> dragStarter;

		// Token: 0x04002C4B RID: 11339
		protected SelectionItem.DragEvent onDragCallback;

		// Token: 0x04002C4C RID: 11340
		protected Func<float, bool> holdStarter;

		// Token: 0x04002C4D RID: 11341
		protected SelectionItem.HoldEvent onHoldCallback;

		// Token: 0x04002C4E RID: 11342
		[SerializeField]
		protected bool _selectable;

		// Token: 0x04002C4F RID: 11343
		[SerializeField]
		protected bool _interactable;

		// Token: 0x04002C50 RID: 11344
		protected bool pointerDown;

		// Token: 0x04002C51 RID: 11345
		protected bool pointerEntering;

		// Token: 0x04002C52 RID: 11346
		[SerializeField]
		protected bool _useDoubleClick;

		// Token: 0x04002C53 RID: 11347
		[SerializeField]
		protected SelectionItem.TransitionMode upMode;

		// Token: 0x04002C54 RID: 11348
		[SerializeField]
		protected SelectionItem.TransitionMode downMode;

		// Token: 0x04002C55 RID: 11349
		[SerializeField]
		protected SelectionItem.TransitionMode rightMode;

		// Token: 0x04002C56 RID: 11350
		[SerializeField]
		protected SelectionItem.TransitionMode leftMode;

		// Token: 0x04002C57 RID: 11351
		[SerializeField]
		protected SelectionItem upItem;

		// Token: 0x04002C58 RID: 11352
		[SerializeField]
		protected SelectionItem downItem;

		// Token: 0x04002C59 RID: 11353
		[SerializeField]
		protected SelectionItem rightItem;

		// Token: 0x04002C5A RID: 11354
		[SerializeField]
		protected SelectionItem leftItem;

		// Token: 0x04002C5B RID: 11355
		[SerializeField]
		protected Selector upSelector;

		// Token: 0x04002C5C RID: 11356
		[SerializeField]
		protected Selector downSelector;

		// Token: 0x04002C5D RID: 11357
		[SerializeField]
		protected Selector rightSelector;

		// Token: 0x04002C5E RID: 11358
		[SerializeField]
		protected Selector leftSelector;

		// Token: 0x04002C5F RID: 11359
		private SelectionItem.RegistStatus registStatus;

		// Token: 0x04002C60 RID: 11360
		protected float _priority;

		// Token: 0x04002C61 RID: 11361
		private CanvasRenderer canvasRenderer;

		// Token: 0x04002C62 RID: 11362
		protected List<uint> callbackIDList;

		// Token: 0x020005CD RID: 1485
		public enum DragStatus
		{
			// Token: 0x04002C64 RID: 11364
			Begin,
			// Token: 0x04002C65 RID: 11365
			Dragging,
			// Token: 0x04002C66 RID: 11366
			End
		}

		// Token: 0x020005CE RID: 1486
		public enum HoldStatus
		{
			// Token: 0x04002C68 RID: 11368
			Begin,
			// Token: 0x04002C69 RID: 11369
			Holding,
			// Token: 0x04002C6A RID: 11370
			End
		}

		// Token: 0x020005CF RID: 1487
		public class DragEvent : UnityEvent<SelectionItem.DragStatus, Vector2>
		{
		}

		// Token: 0x020005D0 RID: 1488
		public class HoldEvent : UnityEvent<SelectionItem.HoldStatus, Vector2>
		{
		}

		// Token: 0x020005D1 RID: 1489
		public class AnalogEvent
		{
			// Token: 0x06002F7A RID: 12154 RVA: 0x0000216D File Offset: 0x0000036D
			public void Add(Func<Vector2, bool> func)
			{
			}

			// Token: 0x06002F7B RID: 12155 RVA: 0x0000216D File Offset: 0x0000036D
			public void Remove(Func<Vector2, bool> func)
			{
			}

			// Token: 0x06002F7C RID: 12156 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x06002F7D RID: 12157 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool Invoke(Vector2 analogInput)
			{
				return false;
			}

			// Token: 0x04002C6B RID: 11371
			private List<Func<Vector2, bool>> callbackList;
		}

		// Token: 0x020005D2 RID: 1490
		// (Invoke) Token: 0x06002F80 RID: 12160
		public delegate void InputCallback();

		// Token: 0x020005D3 RID: 1491
		public enum TransitionMode
		{
			// Token: 0x04002C6D RID: 11373
			Automatic,
			// Token: 0x04002C6E RID: 11374
			Manual,
			// Token: 0x04002C6F RID: 11375
			None,
			// Token: 0x04002C70 RID: 11376
			SelectorParent,
			// Token: 0x04002C71 RID: 11377
			SelectorChild,
			// Token: 0x04002C72 RID: 11378
			SelectorManual
		}

		// Token: 0x020005D4 RID: 1492
		public enum RegistStatus
		{
			// Token: 0x04002C74 RID: 11380
			None,
			// Token: 0x04002C75 RID: 11381
			Previsional,
			// Token: 0x04002C76 RID: 11382
			Registed
		}

		// Token: 0x020005D5 RID: 1493
		public enum UpdateItemStatus
		{
			// Token: 0x04002C78 RID: 11384
			Unknown,
			// Token: 0x04002C79 RID: 11385
			None,
			// Token: 0x04002C7A RID: 11386
			Inactive,
			// Token: 0x04002C7B RID: 11387
			InvokeShortcutCallback = 4,
			// Token: 0x04002C7C RID: 11388
			InvokeSelectedCallback = 8,
			// Token: 0x04002C7D RID: 11389
			InvokeClickCallback = 16,
			// Token: 0x04002C7E RID: 11390
			Invalid = 3,
			// Token: 0x04002C7F RID: 11391
			InvokeNotClickCallback = 12,
			// Token: 0x04002C80 RID: 11392
			InvokeAnyCallback = 28
		}
	}
}
