using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace YgomSystem.UI
{
	// Token: 0x020005D9 RID: 1497
	public class Selector : MonoBehaviour
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06002F95 RID: 12181 RVA: 0x0000216A File Offset: 0x0000036A
		public string groupLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002F97 RID: 12183 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectorGroup group
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06002F98 RID: 12184 RVA: 0x000029CC File Offset: 0x00000BCC
		public int groupPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002F9A RID: 12186 RVA: 0x0000216D File Offset: 0x0000036D
		public Camera viewCamera
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06002F9B RID: 12187 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002F9C RID: 12188 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isDisabledDepthCheck
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

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x000029CC File Offset: 0x00000BCC
		public Selector.OverAreaDirectionMode overAreaDrectionMode
		{
			get
			{
				return Selector.OverAreaDirectionMode.Clamp;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002F9F RID: 12191 RVA: 0x0000216D File Offset: 0x0000036D
		public Selector.MaskMode maskMode
		{
			get
			{
				return Selector.MaskMode.None;
			}
			set
			{
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002FA1 RID: 12193 RVA: 0x0000216D File Offset: 0x0000036D
		public Selector.MaskOperation maskOperation
		{
			get
			{
				return (Selector.MaskOperation)0;
			}
			set
			{
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x000F2124 File Offset: 0x000F0324
		public RaycastHit currentRaycastHit
		{
			get
			{
				return default(RaycastHit);
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002FA4 RID: 12196 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isRaycastHit
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

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06002FA5 RID: 12197 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002FA6 RID: 12198 RVA: 0x0000216D File Offset: 0x0000036D
		public int hierarchyIndexCache
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

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002FA8 RID: 12200 RVA: 0x0000216D File Offset: 0x0000036D
		private bool isDirty
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

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06002FA9 RID: 12201 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002FAA RID: 12202 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isActivated
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

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002FAC RID: 12204 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<bool> onSelectorSelectoredFunc
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06002FAD RID: 12205 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002FAE RID: 12206 RVA: 0x0000216D File Offset: 0x0000036D
		public event Selector.InputCallback upInputCallback
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

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06002FAF RID: 12207 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002FB0 RID: 12208 RVA: 0x0000216D File Offset: 0x0000036D
		public event Selector.InputCallback downInputCallback
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

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06002FB1 RID: 12209 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002FB2 RID: 12210 RVA: 0x0000216D File Offset: 0x0000036D
		public event Selector.InputCallback rightInputCallback
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

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06002FB3 RID: 12211 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002FB4 RID: 12212 RVA: 0x0000216D File Offset: 0x0000036D
		public event Selector.InputCallback leftInputCallback
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

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06002FB5 RID: 12213 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002FB6 RID: 12214 RVA: 0x0000216D File Offset: 0x0000036D
		public event Selector.SelectedCallback selectedCallback
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

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06002FB7 RID: 12215 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002FB8 RID: 12216 RVA: 0x0000216D File Offset: 0x0000036D
		public event Selector.SelectedCallback deselectedCallback
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

		// Token: 0x06002FB9 RID: 12217 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddItem(SelectionItem item)
		{
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveItem(SelectionItem item)
		{
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000029CC File Offset: 0x00000BCC
		public SelectionItem.UpdateItemStatus UpdateAllItems()
		{
			return SelectionItem.UpdateItemStatus.Unknown;
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000F213C File Offset: 0x000F033C
		public ValueTuple<SelectionItem, float> GetSelectionItem(Vector2 current_position, Vector2 direction, float angle, SelectionItem ignore_item = null)
		{
			return default(ValueTuple<SelectionItem, float>);
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetHiestPrioritySelectableItem()
		{
			return null;
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000F2154 File Offset: 0x000F0354
		public ValueTuple<SelectionItem, float> GetSelectionItem(Vector2 view_position)
		{
			return default(ValueTuple<SelectionItem, float>);
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ContainCurrentItem()
		{
			return false;
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsContainsItem(GameObject obj)
		{
			return false;
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionItem GetItem(GameObject obj)
		{
			return null;
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x000F216A File Offset: 0x000F036A
		private bool GetViewRect(out Vector2 rect_point0, out Vector2 rect_point1, out Vector2 rect_point2, out Vector2 rect_point3)
		{
			rect_point0 = default(Vector2);
			rect_point1 = default(Vector2);
			rect_point2 = default(Vector2);
			rect_point3 = default(Vector2);
			return false;
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsContainsPoint(Vector2 point)
		{
			return false;
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDefaultItem(SelectionItem item)
		{
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetViewCamera(Camera view_camera)
		{
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000F218C File Offset: 0x000F038C
		public Vector2 GetViewPosition(Vector3 item_position)
		{
			return default(Vector2);
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Select(SelectionItem selection_item, bool initializeSelection = false, bool force = false)
		{
			return false;
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Select(bool initializeSelection = false)
		{
			return false;
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectPreSelectedItem(bool initializeSelection = false)
		{
			return false;
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectDeufaltItem(bool initializeSelection = false)
		{
			return false;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectParentSelector(bool initializeSelection = false)
		{
			return false;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectChildSelector(bool initializeSelection = false)
		{
			return false;
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetGroupPriority(int group_priority)
		{
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetGroupPriorityInCluster(int priority_in_cluster)
		{
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeGroupLabel(string group_label)
		{
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000029CC File Offset: 0x00000BCC
		public Selector.InputMode GetInputMode(PadInputDirection direction)
		{
			return (Selector.InputMode)0;
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetInputMode(PadInputDirection direction, Selector.InputMode mode)
		{
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeInputCallback(PadInputDirection direction)
		{
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddInputCallback(PadInputDirection direction, Selector.InputCallback callback)
		{
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveInputCallback(PadInputDirection direction, Selector.InputCallback callback)
		{
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveInputCallbackAll(PadInputDirection direction)
		{
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeSelectedCallback(SelectionItem item)
		{
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeDeselectedCallback(SelectionItem item)
		{
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetSelectedItem()
		{
			return null;
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsSelected()
		{
			return false;
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetDefaultItem()
		{
			return null;
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAsDefaultItem(SelectionItem target)
		{
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAsSelectionTargetItem(SelectionItem target)
		{
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupItemDepth(bool force = false)
		{
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupItemDepth(SelectionItem targetItem)
		{
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDirty()
		{
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddClusterActivateCallback(UnityAction callback)
		{
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveClusterActivateCallback(UnityAction callback)
		{
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveAllClusterActivateCallback()
		{
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddClusterDeactivateCallback(UnityAction callback)
		{
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveClusterDeactivateCallback(UnityAction callback)
		{
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveAllClusterDeactivateCallback(UnityAction callback)
		{
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeClusterActivateCallback()
		{
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeClusterDeactivateCallback()
		{
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetResistStatus(Selector.RegistStatus regist_status)
		{
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x0000216A File Offset: 0x0000036A
		public List<SelectionItem> GetItems()
		{
			return null;
		}

		// Token: 0x04002C87 RID: 11399
		private List<SelectionItem> items;

		// Token: 0x04002C88 RID: 11400
		private SelectionItem preSelectedItem;

		// Token: 0x04002C89 RID: 11401
		[SerializeField]
		private string _groupLabel;

		// Token: 0x04002C8A RID: 11402
		[SerializeField]
		private int storedGroupPriority;

		// Token: 0x04002C8B RID: 11403
		private int storedGroupPriorityInCluster;

		// Token: 0x04002C8C RID: 11404
		private Camera _viewCamera;

		// Token: 0x04002C8D RID: 11405
		[SerializeField]
		protected Selector.OverAreaDirectionMode _overAreaDirectionMode;

		// Token: 0x04002C8E RID: 11406
		[SerializeField]
		private Selector.InputMode upInputMode;

		// Token: 0x04002C8F RID: 11407
		[SerializeField]
		private Selector.InputMode downInputMode;

		// Token: 0x04002C90 RID: 11408
		[SerializeField]
		private Selector.InputMode rightInputMode;

		// Token: 0x04002C91 RID: 11409
		[SerializeField]
		private Selector.InputMode leftInputMode;

		// Token: 0x04002C92 RID: 11410
		[SerializeField]
		private Selector.MaskMode _maskMode;

		// Token: 0x04002C93 RID: 11411
		[SerializeField]
		private Selector.MaskOperation _maskOperation;

		// Token: 0x04002C94 RID: 11412
		private Vector3[] rectWorldCorners;

		// Token: 0x04002C95 RID: 11413
		private RaycastHit _currentRaycastHit;

		// Token: 0x04002C96 RID: 11414
		private UnityEvent onActivatedCallback;

		// Token: 0x04002C97 RID: 11415
		private UnityEvent onDeactivatedCallback;

		// Token: 0x04002C98 RID: 11416
		public Selector selectionParentSelector;

		// Token: 0x04002C99 RID: 11417
		public List<Selector> selectionChildrenSelector;

		// Token: 0x04002C9A RID: 11418
		[SerializeField]
		private SelectionItem selectionTargetItem;

		// Token: 0x04002C9B RID: 11419
		private Selector.RegistStatus registStatus;

		// Token: 0x020005DA RID: 1498
		public enum OverAreaDirectionMode
		{
			// Token: 0x04002C9D RID: 11421
			Clamp,
			// Token: 0x04002C9E RID: 11422
			Loop
		}

		// Token: 0x020005DB RID: 1499
		[Flags]
		public enum InputMode
		{
			// Token: 0x04002CA0 RID: 11424
			Select = 1,
			// Token: 0x04002CA1 RID: 11425
			Callback = 2
		}

		// Token: 0x020005DC RID: 1500
		// (Invoke) Token: 0x06002FEF RID: 12271
		public delegate void InputCallback();

		// Token: 0x020005DD RID: 1501
		// (Invoke) Token: 0x06002FF3 RID: 12275
		public delegate void SelectedCallback(SelectionItem item);

		// Token: 0x020005DE RID: 1502
		public enum MaskMode
		{
			// Token: 0x04002CA3 RID: 11427
			None,
			// Token: 0x04002CA4 RID: 11428
			RectTransformArea,
			// Token: 0x04002CA5 RID: 11429
			RectTransformAreaAllPoints
		}

		// Token: 0x020005DF RID: 1503
		[Flags]
		public enum MaskOperation
		{
			// Token: 0x04002CA7 RID: 11431
			ScreenInput = 1,
			// Token: 0x04002CA8 RID: 11432
			KeyInput = 2
		}

		// Token: 0x020005E0 RID: 1504
		public enum RegistStatus
		{
			// Token: 0x04002CAA RID: 11434
			None,
			// Token: 0x04002CAB RID: 11435
			Previsional,
			// Token: 0x04002CAC RID: 11436
			Registed
		}
	}
}
