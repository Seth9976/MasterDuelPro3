using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using YgomSystem.UI;

namespace YgomSystem.Utility
{
	// Token: 0x02000520 RID: 1312
	public class GamePadIconUtil
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06002A2A RID: 10794 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isLoaded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadAtlases(bool immediate)
		{
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x0000216D File Offset: 0x0000036D
		private static void LoadAtlas(GamePadIconUtil.Variation variation, bool immediate, Action<SpriteAtlas> onLoaded)
		{
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetAtlasPath(GamePadIconUtil.Variation variation)
		{
			return null;
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x0000216A File Offset: 0x0000036A
		public static Sprite GetButtonIconSprite(int button_id, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00)
		{
			return null;
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetButtonIconSprite(Action<Sprite> on_load, SelectorManager.KeyType keyType, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00)
		{
			return false;
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetButtonIconSprite(Action<Sprite> on_load, int button_id, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00)
		{
			return false;
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetButtonIconName(int button_id)
		{
			return null;
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetMouseIconName(int mouse_button)
		{
			return null;
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetAnalogIconName(int analog_id)
		{
			return null;
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x0000216A File Offset: 0x0000036A
		public static Sprite GetAnalogIconSprite(int analog_id, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00)
		{
			return null;
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetAnalogIconSprite(Action<Sprite> on_load, SelectorManager.AnalogType analogType, bool isHorizontal, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00)
		{
			return false;
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetAnalogIconSprite(Action<Sprite> on_load, int analog_id, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00)
		{
			return false;
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x0000216A File Offset: 0x0000036A
		public static Sprite GetMouseIconSprite(int mouse_button, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00)
		{
			return null;
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetMouseIconSprite(Action<Sprite> on_load, SelectorManager.MouseType mouseType, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00)
		{
			return false;
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetMouseIconSprite(Action<Sprite> on_load, int mouse_button, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00)
		{
			return false;
		}

		// Token: 0x0400297B RID: 10619
		private const string atlasPath = "Images/GamePad/<_PLATFORM_>/ButtonIcon/GamePadAtlasVar{0:00}";

		// Token: 0x0400297C RID: 10620
		private const string buttonIconName = "button{0:000}";

		// Token: 0x0400297D RID: 10621
		private const string analogIconName = "analog{0:000}";

		// Token: 0x0400297E RID: 10622
		private const string mouseIconName = "mouse{0:000}";

		// Token: 0x0400297F RID: 10623
		private static Dictionary<GamePadIconUtil.Variation, SpriteAtlas> atlases;

		// Token: 0x04002980 RID: 10624
		private static Dictionary<int, Sprite> buttonIconSprites;

		// Token: 0x04002981 RID: 10625
		private static Dictionary<int, Sprite> analogIconSprites;

		// Token: 0x04002982 RID: 10626
		private static Dictionary<int, Sprite> mouseIconSprites;

		// Token: 0x04002983 RID: 10627
		public const int extraButtonID_AnyDirectionalKey = 104;

		// Token: 0x02000521 RID: 1313
		public enum Variation
		{
			// Token: 0x04002985 RID: 10629
			Var00,
			// Token: 0x04002986 RID: 10630
			Var01
		}
	}
}
