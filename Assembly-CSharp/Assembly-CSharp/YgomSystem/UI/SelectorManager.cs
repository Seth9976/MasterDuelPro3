using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace YgomSystem.UI
{
	// Token: 0x020005E9 RID: 1513
	public class SelectorManager : MonoBehaviour
	{
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06003052 RID: 12370 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003053 RID: 12371 RVA: 0x0000216D File Offset: 0x0000036D
		public static SelectorCluster currentCluster
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

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x0000216A File Offset: 0x0000036A
		public static SelectionItem currentItem
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06003055 RID: 12373 RVA: 0x0000216A File Offset: 0x0000036A
		private GamePad pad
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x0000216A File Offset: 0x0000036A
		private KeyInputInfo keyInputInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06003057 RID: 12375 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float padInputRepeatStartTime
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06003058 RID: 12376 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float padInputRepeatIntervalTime
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06003059 RID: 12377 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float dragStartThreshold
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600305A RID: 12378 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float flickThreshold
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600305B RID: 12379 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float dragDistance
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600305C RID: 12380 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600305D RID: 12381 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionItem pressedItem
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

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600305E RID: 12382 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600305F RID: 12383 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionItem draggingItem
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

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003061 RID: 12385 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionItem holdingItem
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

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06003062 RID: 12386 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003063 RID: 12387 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionItem currentPointedItem
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

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003065 RID: 12389 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionItem preClickedItem
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

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x000F2204 File Offset: 0x000F0404
		// (set) Token: 0x06003067 RID: 12391 RVA: 0x0000216D File Offset: 0x0000036D
		public static Vector2 flickSpeed
		{
			[CompilerGenerated]
			get
			{
				return default(Vector2);
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06003068 RID: 12392 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003069 RID: 12393 RVA: 0x0000216D File Offset: 0x0000036D
		public int padInputBlocker
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600306A RID: 12394 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600306B RID: 12395 RVA: 0x0000216D File Offset: 0x0000036D
		public int mouseButtonInputBlocker
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600306C RID: 12396 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600306D RID: 12397 RVA: 0x0000216D File Offset: 0x0000036D
		public static Dictionary<SelectorManager.KeyType, int> noCountKeyInput
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

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600306E RID: 12398 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600306F RID: 12399 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool acceptKeyboadInput
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

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06003070 RID: 12400 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool anyInput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003072 RID: 12402 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool pointingInput
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

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06003073 RID: 12403 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003074 RID: 12404 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool padInputDirection
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

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06003075 RID: 12405 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003076 RID: 12406 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool padInputKey
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

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06003077 RID: 12407 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003078 RID: 12408 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool padInputAnalog
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

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06003079 RID: 12409 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600307A RID: 12410 RVA: 0x0000216D File Offset: 0x0000036D
		public static SelectorManager.InputDevice currentInputDevice
		{
			[CompilerGenerated]
			get
			{
				return SelectorManager.InputDevice.PointingDevice;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600307B RID: 12411 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600307C RID: 12412 RVA: 0x0000216D File Offset: 0x0000036D
		public static SelectorManager.ChangeDeviceEvent onDeviceChangeEvent
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

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600307D RID: 12413 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600307E RID: 12414 RVA: 0x0000216D File Offset: 0x0000036D
		public static int deviceLockCounter
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600307F RID: 12415 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003080 RID: 12416 RVA: 0x0000216D File Offset: 0x0000036D
		public static Action onBackKeyNoActionCallback
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

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06003081 RID: 12417 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool useKeyboardPadKey
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SelectorManager.KeyType onPushKeyType
		{
			get
			{
				return SelectorManager.KeyType.None;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06003083 RID: 12419 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SelectorManager.KeyType pushedKeyType
		{
			get
			{
				return SelectorManager.KeyType.None;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SelectorManager.KeyType onReleaseKeyType
		{
			get
			{
				return SelectorManager.KeyType.None;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SelectorManager.KeyType subKeyType
		{
			get
			{
				return SelectorManager.KeyType.None;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SelectorManager.MouseType onPushMouseType
		{
			get
			{
				return SelectorManager.MouseType.None;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06003087 RID: 12423 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SelectorManager.MouseType pushedMouseType
		{
			get
			{
				return SelectorManager.MouseType.None;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SelectorManager.MouseType onReleaseMouseType
		{
			get
			{
				return SelectorManager.MouseType.None;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SelectorManager.AnalogType analogType
		{
			get
			{
				return SelectorManager.AnalogType.None;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isGamePadInput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isPointingInput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x0000216A File Offset: 0x0000036A
		private static SelectionItem GetCurrentItem()
		{
			return null;
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x0000216A File Offset: 0x0000036A
		private static SelectorCluster GetCluster(int priority)
		{
			return null;
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x0000216A File Offset: 0x0000036A
		public static SelectorCluster GetCluster(SelectorGroup group)
		{
			return null;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetGroupPriority(SelectorGroup group)
		{
			return 0;
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x0000216A File Offset: 0x0000036A
		public static SelectorManager Create(string name, Transform parent)
		{
			return null;
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x0000216D File Offset: 0x0000036D
		private static void CreateInstance()
		{
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000F221A File Offset: 0x000F041A
		public static SelectorGroup AddSelector(string group_label, Selector selector, int new_group_priority, out bool is_new_group)
		{
			is_new_group = false;
			return null;
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RemoveSelector(string group_label, Selector selector)
		{
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ActivateClusterHighestPriority()
		{
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCallbackManagerClusterPriority()
		{
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000F2220 File Offset: 0x000F0420
		public static ValueTuple<int, SelectorCluster> GetHighestPriorityCluster(int maxPriority = -1)
		{
			return default(ValueTuple<int, SelectorCluster>);
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ChangeGroupPriority(SelectorGroup group, int priority)
		{
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DeselectItemActiveCluster()
		{
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DeselectItemActiveCluster(SelectorCluster targetCluster)
		{
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x0000216A File Offset: 0x0000036A
		public static SelectorCluster GetHigherActiveCluster(SelectorCluster targetCluster)
		{
			return null;
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x0000216A File Offset: 0x0000036A
		public static SelectorCluster GetHigherCluster(SelectorCluster targetCluster)
		{
			return null;
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateInputInfo()
		{
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCurrentKeyInfo()
		{
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangeInputDevice(SelectorManager.InputDevice inputDevice)
		{
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LockDeviceChange()
		{
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnlockDeviceChange()
		{
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateDirectionInput(SelectorCluster target_cluster)
		{
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ChangeSelectionItem(Vector2 position, Vector2 normalized_direction, SelectionItem baseItem)
		{
			return false;
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateScreenInput(SelectorManager.ScreenInputInfo current, SelectorCluster rootCluster, bool blockClick)
		{
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateScreenInput(SelectorCluster target_cluster, bool blockClick, bool reset)
		{
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RequestScreenInputUpdate()
		{
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool GetScreenInputInfo(ref SelectorManager.ScreenInputInfo res)
		{
			return false;
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool GetScreenInputInfoTouch(ref SelectorManager.ScreenInputInfo res)
		{
			return false;
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool GetScreenInputInfoMouse(ref SelectorManager.ScreenInputInfo res)
		{
			return false;
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsOnScreen(Vector2 point)
		{
			return false;
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SelectorManager.KeyStatus GetKeyStatus(SelectorManager.KeyType key_type)
		{
			return SelectorManager.KeyStatus.Released;
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SelectorManager.CombiKeyStatus GetKeyStatus(SelectorManager.KeyType key_type_main, SelectorManager.KeyType key_type_sub)
		{
			return SelectorManager.CombiKeyStatus.SubOffMainReleased;
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetKeyStatusFlag(SelectorManager.KeyStatus keyStatus, ref bool isReleased, ref bool isOnPush, ref bool isPushed, ref bool isOnRelease)
		{
			return false;
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000F2238 File Offset: 0x000F0438
		public static Vector2 GetAnalogInput(SelectorManager.AnalogType analog_type)
		{
			return default(Vector2);
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SelectorManager.KeyStatus GetMouseStatus(SelectorManager.MouseType mouse_type)
		{
			return SelectorManager.KeyStatus.Released;
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x0000216D File Offset: 0x0000036D
		public static void BlockInput(int block_priority)
		{
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DeblockInput(int block_priority)
		{
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearInputBlockCounter()
		{
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Reboot()
		{
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x0000216D File Offset: 0x0000036D
		private static void UpdateHighestInputBlockPriority()
		{
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x0000216D File Offset: 0x0000036D
		public void BlockPadInput()
		{
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x0000216D File Offset: 0x0000036D
		public void DeblockPadInput()
		{
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsPadInputBlocked()
		{
			return false;
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x0000216D File Offset: 0x0000036D
		public void BlockMouseButtonInput()
		{
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x0000216D File Offset: 0x0000036D
		public void DeblockMouseButtonInput()
		{
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMouseButtonInputBlocked()
		{
			return false;
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetNoCountKeyInput(SelectorManager.KeyType keyType, bool regist)
		{
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsNoCountKeyInput(SelectorManager.KeyType keyType)
		{
			return false;
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x0000216A File Offset: 0x0000036A
		public static SelectionItem GetItem(Vector2 view_position)
		{
			return null;
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x0000216A File Offset: 0x0000036A
		public static SelectionItem GetItem(Vector2 view_position, int cluster_priority)
		{
			return null;
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddProvisionalRegistedItem(SelectionItem item)
		{
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RegistAllProvisionalRegistedItem()
		{
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddProvisionalSelectedItem(SelectionItem item)
		{
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SelectAllProvisionalSelectedItem()
		{
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddProvisionalRegistedSelector(Selector selector)
		{
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RegistAllProvisionalRegistedSelector()
		{
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetGamePadKeyConfig(SelectorManager.KeyType key_type)
		{
			return 0;
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000F2250 File Offset: 0x000F0450
		public static ValueTuple<int, int> GetGamePadKeyConfig(SelectorManager.AnalogType analog_type)
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x0000216A File Offset: 0x0000036A
		public static KeyCode[] GetKeyboardKeyConfig(SelectorManager.KeyType key_type)
		{
			return null;
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x0000216D File Offset: 0x0000036D
		public static void BlockKeyboardPadKey(bool block)
		{
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x0000216A File Offset: 0x0000036A
		public static int[] GetGamePadAnalogKeyConfig(SelectorManager.AnalogType analog_type)
		{
			return null;
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetMouseKeyConfig(SelectorManager.MouseType mouse_type)
		{
			return 0;
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000F2268 File Offset: 0x000F0468
		public static Vector2 GetCurrentScreenPoint()
		{
			return default(Vector2);
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsScreenInputActive()
		{
			return false;
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddClusterGoThroughCounter(int clusterPriority)
		{
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RemoveClusterGoThroughCounter(int clusterPriority)
		{
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0000216A File Offset: 0x0000036A
		private static SelectorCluster ActionForHighestPriorityCluster(SelectorManager.ClusterFunc callback)
		{
			return null;
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SelectorManager.KeyType ButtonTypeToKeyType(int buttonType)
		{
			return SelectorManager.KeyType.None;
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint AddSelectedCallback(SelectionItem item, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog, Func<bool> callback)
		{
			return 0U;
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<uint> RemoveSelectedCallback(SelectionItem item, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog, Func<bool> callback)
		{
			return null;
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<uint> RemoveSelectedCallback(SelectionItem item, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog)
		{
			return null;
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<uint> ClearSelectedCallback(SelectionItem item)
		{
			return null;
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<uint> RemoveCallback(uint id)
		{
			return null;
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint AddShortcutCallback(SelectionItem item, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog, Func<bool> callback)
		{
			return 0U;
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<uint> RemoveShortcutCallback(SelectionItem item, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog, Func<bool> callback)
		{
			return null;
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<uint> RemoveShortcutCallback(SelectionItem item, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog)
		{
			return null;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<uint> ClearShortcutCallback(SelectionItem item)
		{
			return null;
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint AddShortcutCallback(int priority, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog, Func<bool> callback)
		{
			return 0U;
		}

		// Token: 0x04002CD9 RID: 11481
		private static Dictionary<int, SelectorCluster> clusters;

		// Token: 0x04002CDA RID: 11482
		private static SelectorManager.ClusterFunc selectCurrentItemCallback;

		// Token: 0x04002CDB RID: 11483
		private static SelectorManager.ClusterFunc selectHighestPriorityItemCallback;

		// Token: 0x04002CDC RID: 11484
		private static SelectorManager.ClusterFunc getCurrentItemCallback;

		// Token: 0x04002CDD RID: 11485
		private static SelectorManager.ClusterFunc protectActivationClusterCallback;

		// Token: 0x04002CDE RID: 11486
		private static SelectorManager.ClusterFunc activateClusterCallback;

		// Token: 0x04002CDF RID: 11487
		private static SelectorManager.ClusterFunc resetCallbackManagerPriorityCallback;

		// Token: 0x04002CE0 RID: 11488
		private static SelectorManager.ClusterFunc setCallbackManagerPriorityCallback;

		// Token: 0x04002CE1 RID: 11489
		private GamePad _pad;

		// Token: 0x04002CE2 RID: 11490
		private KeyInputInfo _keyInputInfo;

		// Token: 0x04002CE3 RID: 11491
		[SerializeField]
		private float _padInputRepeatStartTime;

		// Token: 0x04002CE4 RID: 11492
		[SerializeField]
		private float _padInputRepeatIntervalTime;

		// Token: 0x04002CE5 RID: 11493
		private SelectorManager.ScreenInputInfo currentInputInfo;

		// Token: 0x04002CE6 RID: 11494
		private SelectorManager.ScreenInputInfo screenInputInfo;

		// Token: 0x04002CE7 RID: 11495
		[SerializeField]
		private float _dragStartThreshold;

		// Token: 0x04002CE8 RID: 11496
		[SerializeField]
		private float _flickThreshold;

		// Token: 0x04002CE9 RID: 11497
		private Vector3 preMousePosition;

		// Token: 0x04002CEA RID: 11498
		private bool dragging;

		// Token: 0x04002CEB RID: 11499
		private bool dragSuccess;

		// Token: 0x04002CEC RID: 11500
		private float holdTime;

		// Token: 0x04002CED RID: 11501
		private bool holding;

		// Token: 0x04002CEE RID: 11502
		private bool holdCheck;

		// Token: 0x04002CEF RID: 11503
		private Vector2 pressedPoint;

		// Token: 0x04002CF0 RID: 11504
		private const float clickInterval = 0.2f;

		// Token: 0x04002CF1 RID: 11505
		private float preClickedTime;

		// Token: 0x04002CF2 RID: 11506
		private static bool screenInputUpdateRequest;

		// Token: 0x04002CF3 RID: 11507
		private static Dictionary<int, int> inputBlockCounter;

		// Token: 0x04002CF4 RID: 11508
		private static int highestInputBlockPriority;

		// Token: 0x04002CF5 RID: 11509
		private static List<SelectionItem> provisionalRegistedItem;

		// Token: 0x04002CF6 RID: 11510
		private static List<SelectionItem> provisionalSelectedItem;

		// Token: 0x04002CF7 RID: 11511
		private static List<Selector> provisionalRegistedSelector;

		// Token: 0x04002CF8 RID: 11512
		private SelectorCallbackManager callbackManager;

		// Token: 0x04002CF9 RID: 11513
		public static SelectorManager instance;

		// Token: 0x04002CFA RID: 11514
		public static SelectorManager.InputDevice defaultInputDevice;

		// Token: 0x04002CFB RID: 11515
		private static bool deviceLock;

		// Token: 0x04002CFC RID: 11516
		private static bool usePointingDeviceInput;

		// Token: 0x04002CFD RID: 11517
		private static bool useGamePadInput;

		// Token: 0x04002CFE RID: 11518
		private static int useKeyboardPadKeyBlockCounter;

		// Token: 0x04002CFF RID: 11519
		private static Dictionary<SelectorManager.KeyType, int> gamePadButtonKeyConfig;

		// Token: 0x04002D00 RID: 11520
		private static Dictionary<SelectorManager.AnalogType, int[]> gamePadAnalogKeyConfig;

		// Token: 0x04002D01 RID: 11521
		private static Dictionary<SelectorManager.KeyType, KeyCode[]> keyboardButtonKeyConfig;

		// Token: 0x04002D02 RID: 11522
		private static Dictionary<SelectorManager.MouseType, int> mouseKeyConfig;

		// Token: 0x04002D03 RID: 11523
		private SelectorManager.KeyType currentOnPushKeyType;

		// Token: 0x04002D04 RID: 11524
		private SelectorManager.KeyType currentPushedKeyType;

		// Token: 0x04002D05 RID: 11525
		private SelectorManager.KeyType currentOnReleaseKeyType;

		// Token: 0x04002D06 RID: 11526
		private SelectorManager.KeyType currentSubKeyType;

		// Token: 0x04002D07 RID: 11527
		private SelectorManager.MouseType currentOnPushMouseType;

		// Token: 0x04002D08 RID: 11528
		private SelectorManager.MouseType currentPushedMouseType;

		// Token: 0x04002D09 RID: 11529
		private SelectorManager.MouseType currentOnReleaseMouseType;

		// Token: 0x04002D0A RID: 11530
		private SelectorManager.AnalogType currentAnalogType;

		// Token: 0x020005EA RID: 1514
		public enum KeyType
		{
			// Token: 0x04002D0C RID: 11532
			None,
			// Token: 0x04002D0D RID: 11533
			Accept,
			// Token: 0x04002D0E RID: 11534
			Cancel,
			// Token: 0x04002D0F RID: 11535
			Sub1,
			// Token: 0x04002D10 RID: 11536
			Sub2,
			// Token: 0x04002D11 RID: 11537
			Left1,
			// Token: 0x04002D12 RID: 11538
			Left2,
			// Token: 0x04002D13 RID: 11539
			Left3,
			// Token: 0x04002D14 RID: 11540
			Right1,
			// Token: 0x04002D15 RID: 11541
			Right2,
			// Token: 0x04002D16 RID: 11542
			Right3,
			// Token: 0x04002D17 RID: 11543
			Option1,
			// Token: 0x04002D18 RID: 11544
			Option2,
			// Token: 0x04002D19 RID: 11545
			DirectionUp,
			// Token: 0x04002D1A RID: 11546
			DirectionDown,
			// Token: 0x04002D1B RID: 11547
			DirectionLeft,
			// Token: 0x04002D1C RID: 11548
			DirectionRight,
			// Token: 0x04002D1D RID: 11549
			BackKey,
			// Token: 0x04002D1E RID: 11550
			Any
		}

		// Token: 0x020005EB RID: 1515
		public enum AnalogType
		{
			// Token: 0x04002D20 RID: 11552
			None,
			// Token: 0x04002D21 RID: 11553
			Main,
			// Token: 0x04002D22 RID: 11554
			Sub,
			// Token: 0x04002D23 RID: 11555
			Wheel
		}

		// Token: 0x020005EC RID: 1516
		public enum MouseType
		{
			// Token: 0x04002D25 RID: 11557
			None,
			// Token: 0x04002D26 RID: 11558
			Main,
			// Token: 0x04002D27 RID: 11559
			Sub1,
			// Token: 0x04002D28 RID: 11560
			Sub2
		}

		// Token: 0x020005ED RID: 1517
		public enum KeyStatus
		{
			// Token: 0x04002D2A RID: 11562
			Released,
			// Token: 0x04002D2B RID: 11563
			OnPush,
			// Token: 0x04002D2C RID: 11564
			Pushed,
			// Token: 0x04002D2D RID: 11565
			OnRelease
		}

		// Token: 0x020005EE RID: 1518
		public enum CombiKeyStatus
		{
			// Token: 0x04002D2F RID: 11567
			SubOffMainReleased,
			// Token: 0x04002D30 RID: 11568
			SubOffMainOnPush,
			// Token: 0x04002D31 RID: 11569
			SubOffMainPushed,
			// Token: 0x04002D32 RID: 11570
			SubOffMainOnRelease,
			// Token: 0x04002D33 RID: 11571
			SubOnMainReleased,
			// Token: 0x04002D34 RID: 11572
			SubOnMainOnPush = 8,
			// Token: 0x04002D35 RID: 11573
			SubOnMainPushed = 16,
			// Token: 0x04002D36 RID: 11574
			SubOnMainOnRelease = 32
		}

		// Token: 0x020005EF RID: 1519
		public enum KeyTypeMask
		{
			// Token: 0x04002D38 RID: 11576
			None,
			// Token: 0x04002D39 RID: 11577
			Accept = 2,
			// Token: 0x04002D3A RID: 11578
			Cancel = 4,
			// Token: 0x04002D3B RID: 11579
			Sub1 = 8,
			// Token: 0x04002D3C RID: 11580
			Sub2 = 16,
			// Token: 0x04002D3D RID: 11581
			Left1 = 32,
			// Token: 0x04002D3E RID: 11582
			Left2 = 64,
			// Token: 0x04002D3F RID: 11583
			Left3 = 128,
			// Token: 0x04002D40 RID: 11584
			Right1 = 256,
			// Token: 0x04002D41 RID: 11585
			Right2 = 512,
			// Token: 0x04002D42 RID: 11586
			Right3 = 1024,
			// Token: 0x04002D43 RID: 11587
			Option1 = 2048,
			// Token: 0x04002D44 RID: 11588
			Option2 = 4096,
			// Token: 0x04002D45 RID: 11589
			DirectionUp = 8192,
			// Token: 0x04002D46 RID: 11590
			DirectionDown = 16384,
			// Token: 0x04002D47 RID: 11591
			DirectionLeft = 32768,
			// Token: 0x04002D48 RID: 11592
			DirectionRight = 65536,
			// Token: 0x04002D49 RID: 11593
			BackKey = 131072
		}

		// Token: 0x020005F0 RID: 1520
		public enum AnalogTypeFlag
		{
			// Token: 0x04002D4B RID: 11595
			None,
			// Token: 0x04002D4C RID: 11596
			Main = 2,
			// Token: 0x04002D4D RID: 11597
			Sub = 4,
			// Token: 0x04002D4E RID: 11598
			Wheel = 8
		}

		// Token: 0x020005F1 RID: 1521
		public struct ScreenInputInfo
		{
			// Token: 0x060030DD RID: 12509 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsChanged(SelectorManager.ScreenInputInfo info)
			{
				return false;
			}

			// Token: 0x04002D4F RID: 11599
			public bool onScreen;

			// Token: 0x04002D50 RID: 11600
			public Vector2 screenPoint;

			// Token: 0x04002D51 RID: 11601
			public bool pressMain;

			// Token: 0x04002D52 RID: 11602
			public bool pressSub1;

			// Token: 0x04002D53 RID: 11603
			public bool pressSub2;
		}

		// Token: 0x020005F2 RID: 1522
		public enum InputDevice
		{
			// Token: 0x04002D55 RID: 11605
			PointingDevice,
			// Token: 0x04002D56 RID: 11606
			GamePad
		}

		// Token: 0x020005F3 RID: 1523
		public class ChangeDeviceEvent : UnityEvent<SelectorManager.InputDevice>
		{
		}

		// Token: 0x020005F4 RID: 1524
		// (Invoke) Token: 0x060030E0 RID: 12512
		private delegate bool ClusterFunc(SelectorCluster cluster);
	}
}
