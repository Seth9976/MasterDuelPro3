using System;
using System.Collections.Generic;
using MDPro3.Duel.YGOSharp;
using MDPro3.UI;
using MDPro3.Utility;
using UnityEngine;

namespace MDPro3
{
	// Token: 0x0200129B RID: 4763
	[CreateAssetMenu]
	public class TextureContainer : ScriptableObject
	{
		// Token: 0x06008BA3 RID: 35747 RVA: 0x0011E03C File Offset: 0x0011C23C
		public List<Sprite> GetRankSprites(int rank)
		{
			List<Sprite> returnValue = new List<Sprite>();
			if (rank < this.rankRange[1])
			{
				returnValue.Add(this.rankBG01);
				returnValue.Add(this.rankIcon01);
				returnValue.Add(this.GetRankTier(this.rankRange[0], this.rankRange[1], rank));
				returnValue.Add(this.transparent);
				returnValue.Add(this.transparent);
			}
			else if (rank < this.rankRange[2])
			{
				returnValue.Add(this.rankBG02);
				returnValue.Add(this.rankIcon02);
				returnValue.Add(this.GetRankTier(this.rankRange[1], this.rankRange[2], rank));
				returnValue.Add(this.transparent);
				returnValue.Add(this.transparent);
			}
			else if (rank < this.rankRange[3])
			{
				returnValue.Add(this.rankBG03);
				returnValue.Add(this.rankIcon03);
				returnValue.Add(this.GetRankTier(this.rankRange[2], this.rankRange[3], rank));
				returnValue.Add(this.transparent);
				returnValue.Add(this.transparent);
			}
			else if (rank < this.rankRange[4])
			{
				returnValue.Add(this.rankBG04);
				returnValue.Add(this.rankIcon04);
				returnValue.Add(this.GetRankTier(this.rankRange[3], this.rankRange[4], rank));
				returnValue.Add(this.transparent);
				returnValue.Add(this.transparent);
			}
			else if (rank < this.rankRange[5])
			{
				returnValue.Add(this.rankBG05);
				returnValue.Add(this.rankIcon05);
				returnValue.Add(this.transparent);
				returnValue.Add(this.GetRankTier(this.rankRange[4], this.rankRange[5], rank));
				returnValue.Add(this.transparent);
			}
			else if (rank < this.rankRange[6])
			{
				returnValue.Add(this.rankBG06);
				returnValue.Add(this.rankIcon06);
				returnValue.Add(this.transparent);
				returnValue.Add(this.GetRankTier(this.rankRange[5], this.rankRange[6], rank));
				returnValue.Add(this.transparent);
			}
			else if (rank < this.rankRange[7])
			{
				returnValue.Add(this.rankBG07);
				returnValue.Add(this.rankIcon07);
				returnValue.Add(this.transparent);
				returnValue.Add(this.transparent);
				returnValue.Add(this.GetRankTier(this.rankRange[6], this.rankRange[7], rank));
			}
			else
			{
				returnValue.Add(this.rankBG08);
				returnValue.Add(this.rankIcon08);
				returnValue.Add(this.transparent);
				returnValue.Add(this.transparent);
				returnValue.Add(this.transparent);
			}
			return returnValue;
		}

		// Token: 0x06008BA4 RID: 35748 RVA: 0x0011E314 File Offset: 0x0011C514
		private Sprite GetRankTier(int rankStart, int rankEnd, int rank)
		{
			if (rank > rankEnd)
			{
				return this.rankTier05;
			}
			if (rank < rankStart)
			{
				return this.rankTier01;
			}
			int segmentSize = (rankEnd - rankStart) / 5;
			switch ((int)Math.Floor((double)(rank - rankStart) / (double)segmentSize))
			{
			case 0:
				return this.rankTier01;
			case 1:
				return this.rankTier02;
			case 2:
				return this.rankTier03;
			case 3:
				return this.rankTier04;
			case 4:
				return this.rankTier05;
			default:
				return this.rankTier01;
			}
		}

		// Token: 0x06008BA5 RID: 35749 RVA: 0x0011E390 File Offset: 0x0011C590
		public Sprite GetChainNumSprite(int num)
		{
			switch (num)
			{
			case 0:
				return this.chainNumSet0;
			case 1:
				return this.chainNumSet1;
			case 2:
				return this.chainNumSet2;
			case 3:
				return this.chainNumSet3;
			case 4:
				return this.chainNumSet4;
			case 5:
				return this.chainNumSet5;
			case 6:
				return this.chainNumSet6;
			case 7:
				return this.chainNumSet7;
			case 8:
				return this.chainNumSet8;
			case 9:
				return this.chainNumSet9;
			default:
				return this.chainNumSet0;
			}
		}

		// Token: 0x06008BA6 RID: 35750 RVA: 0x0011E41C File Offset: 0x0011C61C
		public Sprite GetGamepadIcon(ShortcutIcon.GamePadButton button)
		{
			Sprite sprite2;
			switch (button)
			{
			case ShortcutIcon.GamePadButton.ButtonSouth:
			{
				Sprite sprite;
				switch (UserInput.gamepadType)
				{
				case UserInput.GamepadType.Xbox:
					sprite = this.gamepad_ButtonSouth_Xbox;
					break;
				case UserInput.GamepadType.PlayStation:
					sprite = this.gamepad_ButtonSouth_PlayStation;
					break;
				case UserInput.GamepadType.Nintendo:
					sprite = this.gamepad_ButtonSouth_Nintendo;
					break;
				default:
					sprite = null;
					break;
				}
				sprite2 = sprite;
				break;
			}
			case ShortcutIcon.GamePadButton.ButtonEast:
			{
				Sprite sprite;
				switch (UserInput.gamepadType)
				{
				case UserInput.GamepadType.Xbox:
					sprite = this.gamepad_ButtonEast_Xbox;
					break;
				case UserInput.GamepadType.PlayStation:
					sprite = this.gamepad_ButtonEast_PlayStation;
					break;
				case UserInput.GamepadType.Nintendo:
					sprite = this.gamepad_ButtonEast_Nintendo;
					break;
				default:
					sprite = null;
					break;
				}
				sprite2 = sprite;
				break;
			}
			case ShortcutIcon.GamePadButton.ButtonWest:
			{
				Sprite sprite;
				switch (UserInput.gamepadType)
				{
				case UserInput.GamepadType.Xbox:
					sprite = this.gamepad_ButtonWest_Xbox;
					break;
				case UserInput.GamepadType.PlayStation:
					sprite = this.gamepad_ButtonWest_PlayStation;
					break;
				case UserInput.GamepadType.Nintendo:
					sprite = this.gamepad_ButtonWest_Nintendo;
					break;
				default:
					sprite = null;
					break;
				}
				sprite2 = sprite;
				break;
			}
			case ShortcutIcon.GamePadButton.ButtonNorth:
			{
				Sprite sprite;
				switch (UserInput.gamepadType)
				{
				case UserInput.GamepadType.Xbox:
					sprite = this.gamepad_ButtonNorth_Xbox;
					break;
				case UserInput.GamepadType.PlayStation:
					sprite = this.gamepad_ButtonNorth_PlayStation;
					break;
				case UserInput.GamepadType.Nintendo:
					sprite = this.gamepad_ButtonNorth_Nintendo;
					break;
				default:
					sprite = null;
					break;
				}
				sprite2 = sprite;
				break;
			}
			case ShortcutIcon.GamePadButton.LeftShoulder:
			{
				Sprite sprite;
				switch (UserInput.gamepadType)
				{
				case UserInput.GamepadType.Xbox:
					sprite = this.gamepad_LeftShoulder_Xbox;
					break;
				case UserInput.GamepadType.PlayStation:
					sprite = this.gamepad_LeftShoulder_PlayStation;
					break;
				case UserInput.GamepadType.Nintendo:
					sprite = this.gamepad_LeftShoulder_Nintendo;
					break;
				default:
					sprite = null;
					break;
				}
				sprite2 = sprite;
				break;
			}
			case ShortcutIcon.GamePadButton.RightShoulder:
			{
				Sprite sprite;
				switch (UserInput.gamepadType)
				{
				case UserInput.GamepadType.Xbox:
					sprite = this.gamepad_RightShoulder_Xbox;
					break;
				case UserInput.GamepadType.PlayStation:
					sprite = this.gamepad_RightShoulder_PlayStation;
					break;
				case UserInput.GamepadType.Nintendo:
					sprite = this.gamepad_RightShoulder_Nintendo;
					break;
				default:
					sprite = null;
					break;
				}
				sprite2 = sprite;
				break;
			}
			case ShortcutIcon.GamePadButton.LeftTrigger:
			{
				Sprite sprite;
				switch (UserInput.gamepadType)
				{
				case UserInput.GamepadType.Xbox:
					sprite = this.gamepad_LeftTrigger_Xbox;
					break;
				case UserInput.GamepadType.PlayStation:
					sprite = this.gamepad_LeftTrigger_PlayStation;
					break;
				case UserInput.GamepadType.Nintendo:
					sprite = this.gamepad_LeftTrigger_Nintendo;
					break;
				default:
					sprite = null;
					break;
				}
				sprite2 = sprite;
				break;
			}
			case ShortcutIcon.GamePadButton.RightTrigger:
			{
				Sprite sprite;
				switch (UserInput.gamepadType)
				{
				case UserInput.GamepadType.Xbox:
					sprite = this.gamepad_RightTrigger_Xbox;
					break;
				case UserInput.GamepadType.PlayStation:
					sprite = this.gamepad_RightTrigger_PlayStation;
					break;
				case UserInput.GamepadType.Nintendo:
					sprite = this.gamepad_RightTrigger_Nintendo;
					break;
				default:
					sprite = null;
					break;
				}
				sprite2 = sprite;
				break;
			}
			case ShortcutIcon.GamePadButton.LeftStick:
				sprite2 = this.gamepad_LeftStick;
				break;
			case ShortcutIcon.GamePadButton.RightStick:
				sprite2 = this.gamepad_RightStick;
				break;
			case ShortcutIcon.GamePadButton.Select:
			{
				Sprite sprite;
				switch (UserInput.gamepadType)
				{
				case UserInput.GamepadType.Xbox:
					sprite = this.gamepad_Select_Xbox;
					break;
				case UserInput.GamepadType.PlayStation:
					sprite = this.gamepad_Select_PlayStation;
					break;
				case UserInput.GamepadType.Nintendo:
					sprite = this.gamepad_Select_Nintendo;
					break;
				default:
					sprite = null;
					break;
				}
				sprite2 = sprite;
				break;
			}
			case ShortcutIcon.GamePadButton.Start:
			{
				Sprite sprite;
				switch (UserInput.gamepadType)
				{
				case UserInput.GamepadType.Xbox:
					sprite = this.gamepad_Start_Xbox;
					break;
				case UserInput.GamepadType.PlayStation:
					sprite = this.gamepad_Start_PlayStation;
					break;
				case UserInput.GamepadType.Nintendo:
					sprite = this.gamepad_Start_Nintendo;
					break;
				default:
					sprite = null;
					break;
				}
				sprite2 = sprite;
				break;
			}
			default:
				sprite2 = null;
				break;
			}
			return sprite2;
		}

		// Token: 0x06008BA7 RID: 35751 RVA: 0x0011E6F8 File Offset: 0x0011C8F8
		public List<Sprite> GetLocationIcons(GPS p)
		{
			List<Sprite> returnValue = new List<Sprite>();
			if (p.InLocation(CardLocation.Onfield) && !p.InLocation(CardLocation.Overlay))
			{
				if (p.InLocation(CardLocation.SpellZone) && p.sequence == 5U)
				{
					returnValue.Add(this.locationFieldMagic);
					returnValue.Add(p.InMyControl() ? this.controllerMe : this.controllerOp);
					return returnValue;
				}
				if (p.InLocation(CardLocation.MonsterZone))
				{
					switch (p.sequence)
					{
					case 0U:
						returnValue.Add(p.InMyControl() ? this.locationMyMZone0 : this.locationOpMZone0);
						break;
					case 1U:
						returnValue.Add(p.InMyControl() ? this.locationMyMZone1 : this.locationOpMZone1);
						break;
					case 2U:
						returnValue.Add(p.InMyControl() ? this.locationMyMZone2 : this.locationOpMZone2);
						break;
					case 3U:
						returnValue.Add(p.InMyControl() ? this.locationMyMZone3 : this.locationOpMZone3);
						break;
					case 4U:
						returnValue.Add(p.InMyControl() ? this.locationMyMZone4 : this.locationOpMZone4);
						break;
					case 5U:
						returnValue.Add(p.InMyControl() ? this.locationMyMZone5 : this.locationOpMZone5);
						break;
					case 6U:
						returnValue.Add(p.InMyControl() ? this.locationMyMZone6 : this.locationOpMZone6);
						break;
					}
				}
				else
				{
					switch (p.sequence)
					{
					case 0U:
						returnValue.Add(p.InMyControl() ? this.locationMySZone0 : this.locationOpSZone0);
						break;
					case 1U:
						returnValue.Add(p.InMyControl() ? this.locationMySZone1 : this.locationOpSZone1);
						break;
					case 2U:
						returnValue.Add(p.InMyControl() ? this.locationMySZone2 : this.locationOpSZone2);
						break;
					case 3U:
						returnValue.Add(p.InMyControl() ? this.locationMySZone3 : this.locationOpSZone3);
						break;
					case 4U:
						returnValue.Add(p.InMyControl() ? this.locationMySZone4 : this.locationOpSZone4);
						break;
					}
				}
			}
			else
			{
				if ((p.location & 128U) > 0U)
				{
					returnValue.Add(this.locationOverlay);
				}
				else if ((p.location & 1U) > 0U)
				{
					returnValue.Add(this.locationDeck);
				}
				else if ((p.location & 64U) > 0U)
				{
					returnValue.Add(this.locationExtra);
				}
				else if ((p.location & 2U) > 0U)
				{
					returnValue.Add(this.locationHand);
				}
				else if ((p.location & 16U) > 0U)
				{
					returnValue.Add(this.locationGrave);
				}
				else if ((p.location & 32U) > 0U)
				{
					returnValue.Add(this.locationRemoved);
				}
				returnValue.Add((p.controller == 0U) ? this.controllerMe : this.controllerOp);
			}
			return returnValue;
		}

		// Token: 0x06008BA8 RID: 35752 RVA: 0x0011EA08 File Offset: 0x0011CC08
		public Sprite GetCardRegulationIcon(int code, Banlist banlist)
		{
			Sprite sprite;
			switch (banlist.GetQuantity(code))
			{
			case 1:
				sprite = this.limit1;
				break;
			case 2:
				sprite = this.limit2;
				break;
			case 3:
				sprite = this.typeNone;
				break;
			default:
				sprite = this.banned;
				break;
			}
			return sprite;
		}

		// Token: 0x06008BA9 RID: 35753 RVA: 0x0011EA58 File Offset: 0x0011CC58
		public Sprite GetCardAttributeIcon(Card data, bool render = false)
		{
			bool rushDuel = CardRenderer.NeedRushDuelStyle(data.Id);
			bool needRuby = render && !rushDuel && Language.AttributeNeedRuby();
			if (data.HasType(CardType.Monster))
			{
				if (((long)data.Attribute & 16L) > 0L)
				{
					if (rushDuel && render)
					{
						return this.rd_Attribute_Light;
					}
					if (!needRuby)
					{
						return this.attributeLight;
					}
					return this.attributeLight_Ruby;
				}
				else if (((long)data.Attribute & 32L) > 0L)
				{
					if (rushDuel && render)
					{
						return this.rd_Attribute_Dark;
					}
					if (!needRuby)
					{
						return this.attributeDark;
					}
					return this.attributeDark_Ruby;
				}
				else if (((long)data.Attribute & 2L) > 0L)
				{
					if (rushDuel && render)
					{
						return this.rd_Attribute_Water;
					}
					if (!needRuby)
					{
						return this.attributeWater;
					}
					return this.attributeWater_Ruby;
				}
				else if (((long)data.Attribute & 4L) > 0L)
				{
					if (rushDuel && render)
					{
						return this.rd_Attribute_Fire;
					}
					if (!needRuby)
					{
						return this.attributeFire;
					}
					return this.attributeFire_Ruby;
				}
				else if (((long)data.Attribute & 1L) > 0L)
				{
					if (rushDuel && render)
					{
						return this.rd_Attribute_Earth;
					}
					if (!needRuby)
					{
						return this.attributeEarth;
					}
					return this.attributeEarth_Ruby;
				}
				else if (((long)data.Attribute & 8L) > 0L)
				{
					if (rushDuel && render)
					{
						return this.rd_Attribute_Wind;
					}
					if (!needRuby)
					{
						return this.attributeWind;
					}
					return this.attributeWind_Ruby;
				}
				else
				{
					if (rushDuel && render)
					{
						return this.rd_Attribute_Divine;
					}
					if (!needRuby)
					{
						return this.attributeDivine;
					}
					return this.attributeDivine_Ruby;
				}
			}
			else if (data.HasType(CardType.Spell))
			{
				if (rushDuel && render)
				{
					return this.rd_Attribute_Spell;
				}
				if (!needRuby)
				{
					return this.attributeSpell;
				}
				return this.attributeSpell_Ruby;
			}
			else
			{
				if (rushDuel && render)
				{
					return this.rd_Attribute_Trap;
				}
				if (!needRuby)
				{
					return this.attributeTrap;
				}
				return this.attributeTrap_Ruby;
			}
		}

		// Token: 0x06008BAA RID: 35754 RVA: 0x0011EBF0 File Offset: 0x0011CDF0
		public Sprite GetCardSpellTrapTypeIcon(Card data)
		{
			if (data.HasType(CardType.Monster))
			{
				return null;
			}
			if (data.HasType(CardType.Counter))
			{
				return this.typeCounter;
			}
			if (data.HasType(CardType.Field))
			{
				return this.typeField;
			}
			if (data.HasType(CardType.Equip))
			{
				return this.typeEquip;
			}
			if (data.HasType(CardType.Continuous))
			{
				return this.typeContinuous;
			}
			if (data.HasType(CardType.QuickPlay))
			{
				return this.typeQuickPlay;
			}
			if (data.HasType(CardType.Ritual))
			{
				return this.typeRitual;
			}
			return this.typeNone;
		}

		// Token: 0x06008BAB RID: 35755 RVA: 0x0011EC88 File Offset: 0x0011CE88
		public Sprite GetCardRaceIcon(Card data)
		{
			if (!data.HasType(CardType.Monster))
			{
				return null;
			}
			if (((long)data.Race & 1L) > 0L)
			{
				return this.raceWarrior;
			}
			if (((long)data.Race & 2L) > 0L)
			{
				return this.raceSpellCaster;
			}
			if (((long)data.Race & 4L) > 0L)
			{
				return this.raceFairy;
			}
			if (((long)data.Race & 8L) > 0L)
			{
				return this.raceFiend;
			}
			if (((long)data.Race & 16L) > 0L)
			{
				return this.raceZombie;
			}
			if (((long)data.Race & 32L) > 0L)
			{
				return this.raceMachine;
			}
			if (((long)data.Race & 64L) > 0L)
			{
				return this.raceAqua;
			}
			if (((long)data.Race & 128L) > 0L)
			{
				return this.racePyro;
			}
			if (((long)data.Race & 256L) > 0L)
			{
				return this.raceRock;
			}
			if (((long)data.Race & 512L) > 0L)
			{
				return this.raceWindBeast;
			}
			if (((long)data.Race & 1024L) > 0L)
			{
				return this.racePlant;
			}
			if (((long)data.Race & 2048L) > 0L)
			{
				return this.raceInsect;
			}
			if (((long)data.Race & 4096L) > 0L)
			{
				return this.raceThunder;
			}
			if (((long)data.Race & 8192L) > 0L)
			{
				return this.raceDragon;
			}
			if (((long)data.Race & 16384L) > 0L)
			{
				return this.raceBeast;
			}
			if (((long)data.Race & 32768L) > 0L)
			{
				return this.raceBeastWarrior;
			}
			if (((long)data.Race & 65536L) > 0L)
			{
				return this.raceDinosaur;
			}
			if (((long)data.Race & 131072L) > 0L)
			{
				return this.raceFish;
			}
			if (((long)data.Race & 262144L) > 0L)
			{
				return this.raceSeaSerpent;
			}
			if (((long)data.Race & 524288L) > 0L)
			{
				return this.raceReptile;
			}
			if (((long)data.Race & 1048576L) > 0L)
			{
				return this.racePsycho;
			}
			if (((long)data.Race & 2097152L) > 0L)
			{
				return this.raceDivineBeast;
			}
			if (((long)data.Race & 4194304L) > 0L)
			{
				return this.raceCreatorGod;
			}
			if (((long)data.Race & 8388608L) > 0L)
			{
				return this.raceWyrm;
			}
			if (((long)data.Race & 16777216L) > 0L)
			{
				return this.raceCyberse;
			}
			if (((long)data.Race & 33554432L) > 0L)
			{
				return this.raceIllustion;
			}
			return this.typeNone;
		}

		// Token: 0x06008BAC RID: 35756 RVA: 0x0011EF1C File Offset: 0x0011D11C
		public Sprite GetCardPoolIcon(Card data)
		{
			if (data.isPre)
			{
				return this.cardPoolPRE;
			}
			if ((data.Ot & 4) > 0)
			{
				return this.cardPoolDIY;
			}
			if ((data.Ot & 1) > 0 && (data.Ot & 2) == 0)
			{
				return this.cardPoolOCG;
			}
			if ((data.Ot & 2) > 0 && (data.Ot & 1) == 0)
			{
				return this.cardPoolTCG;
			}
			return this.typeNone;
		}

		// Token: 0x06008BAD RID: 35757 RVA: 0x0011EF88 File Offset: 0x0011D188
		public Texture2D GetCardLoadingTexture(Card data)
		{
			bool rd = CardRenderer.NeedRushDuelStyle(data.Id);
			if (data.HasType(CardType.Pendulum))
			{
				if (data.HasType(CardType.Normal))
				{
					if (!rd)
					{
						return this.cardFramePendulumNormal.texture;
					}
					return this.rd_Loading_PendulumNormal.texture;
				}
				else if (data.HasType(CardType.Xyz))
				{
					if (!rd)
					{
						return this.cardFramePendulumXyz.texture;
					}
					return this.rd_Loading_PendulumXyz.texture;
				}
				else if (data.HasType(CardType.Synchro))
				{
					if (!rd)
					{
						return this.cardFramePendulumSynchro.texture;
					}
					return this.rd_Loading_PendulumSynchro.texture;
				}
				else if (data.HasType(CardType.Fusion))
				{
					if (!rd)
					{
						return this.cardFramePendulumFusion.texture;
					}
					return this.rd_Loading_PendulumFusion.texture;
				}
				else if (data.HasType(CardType.Ritual))
				{
					if (!rd)
					{
						return this.cardFramePendulumRitual.texture;
					}
					return this.rd_Loading_PendulumRitual.texture;
				}
				else
				{
					if (!rd)
					{
						return this.cardFramePendulumEffect.texture;
					}
					return this.rd_Loading_PendulumEffect.texture;
				}
			}
			else if (data.HasType(CardType.Normal))
			{
				if (!rd)
				{
					return this.cardFrameNormal.texture;
				}
				return this.rd_Loading_Normal.texture;
			}
			else if (data.HasType(CardType.Xyz))
			{
				if (!rd)
				{
					return this.cardFrameXyz.texture;
				}
				return this.rd_Loading_Xyz.texture;
			}
			else if (data.HasType(CardType.Synchro))
			{
				if (!rd)
				{
					return this.cardFrameSynchro.texture;
				}
				return this.rd_Loading_Synchro.texture;
			}
			else if (data.HasType(CardType.Fusion))
			{
				if (!rd)
				{
					return this.cardFrameFusion.texture;
				}
				return this.rd_Loading_Fusion.texture;
			}
			else if (data.HasType(CardType.Ritual) && data.HasType(CardType.Monster))
			{
				if (!rd)
				{
					return this.cardFrameRitual.texture;
				}
				return this.rd_Loading_Ritual.texture;
			}
			else if (data.HasType(CardType.Link))
			{
				if (!rd)
				{
					return this.cardFrameLink.texture;
				}
				return this.rd_Loading_Link.texture;
			}
			else if (data.HasType(CardType.Spell))
			{
				if (!rd)
				{
					return this.cardFrameSpell.texture;
				}
				return this.rd_Loading_Spell.texture;
			}
			else if (data.HasType(CardType.Trap))
			{
				if (!rd)
				{
					return this.cardFrameTrap.texture;
				}
				return this.rd_Loading_Trap.texture;
			}
			else if (data.HasType(CardType.Token))
			{
				if (!rd)
				{
					return this.cardFrameToken.texture;
				}
				return this.rd_Loading_Token.texture;
			}
			else
			{
				if (!rd)
				{
					return this.cardFrameEffect.texture;
				}
				return this.rd_Loading_Effect.texture;
			}
		}

		// Token: 0x0400C7B1 RID: 51121
		[Header("Common")]
		public Sprite black;

		// Token: 0x0400C7B2 RID: 51122
		public Sprite transparent;

		// Token: 0x0400C7B3 RID: 51123
		public Sprite unknownCard;

		// Token: 0x0400C7B4 RID: 51124
		public Sprite unknownArt;

		// Token: 0x0400C7B5 RID: 51125
		public Sprite cardBackDefault;

		// Token: 0x0400C7B6 RID: 51126
		[Header("Card Frame")]
		public Sprite cardFrameNormal;

		// Token: 0x0400C7B7 RID: 51127
		public Sprite cardFrameEffect;

		// Token: 0x0400C7B8 RID: 51128
		public Sprite cardFrameRitual;

		// Token: 0x0400C7B9 RID: 51129
		public Sprite cardFrameFusion;

		// Token: 0x0400C7BA RID: 51130
		public Sprite cardFrameObelisk;

		// Token: 0x0400C7BB RID: 51131
		public Sprite cardFrameOsiris;

		// Token: 0x0400C7BC RID: 51132
		public Sprite cardFrameRa;

		// Token: 0x0400C7BD RID: 51133
		public Sprite cardFrameSpell;

		// Token: 0x0400C7BE RID: 51134
		public Sprite cardFrameTrap;

		// Token: 0x0400C7BF RID: 51135
		public Sprite cardFrameToken;

		// Token: 0x0400C7C0 RID: 51136
		public Sprite cardFrameSynchro;

		// Token: 0x0400C7C1 RID: 51137
		public Sprite cardFrameDarkSynchro;

		// Token: 0x0400C7C2 RID: 51138
		public Sprite cardFrameXyz;

		// Token: 0x0400C7C3 RID: 51139
		public Sprite cardFramePendulumNormal;

		// Token: 0x0400C7C4 RID: 51140
		public Sprite cardFramePendulumEffect;

		// Token: 0x0400C7C5 RID: 51141
		public Sprite cardFramePendulumXyz;

		// Token: 0x0400C7C6 RID: 51142
		public Sprite cardFramePendulumSynchro;

		// Token: 0x0400C7C7 RID: 51143
		public Sprite cardFramePendulumFusion;

		// Token: 0x0400C7C8 RID: 51144
		public Sprite cardFrameLink;

		// Token: 0x0400C7C9 RID: 51145
		public Sprite cardFramePendulumRitual;

		// Token: 0x0400C7CA RID: 51146
		public Sprite cardFrameNormalOF;

		// Token: 0x0400C7CB RID: 51147
		public Sprite cardFrameEffectOF;

		// Token: 0x0400C7CC RID: 51148
		public Sprite cardFrameRitualOF;

		// Token: 0x0400C7CD RID: 51149
		public Sprite cardFrameFusionOF;

		// Token: 0x0400C7CE RID: 51150
		public Sprite cardFrameObeliskOF;

		// Token: 0x0400C7CF RID: 51151
		public Sprite cardFrameOsirisOF;

		// Token: 0x0400C7D0 RID: 51152
		public Sprite cardFrameRaOF;

		// Token: 0x0400C7D1 RID: 51153
		public Sprite cardFrameSpellOF;

		// Token: 0x0400C7D2 RID: 51154
		public Sprite cardFrameTrapOF;

		// Token: 0x0400C7D3 RID: 51155
		public Sprite cardFrameTokenOF;

		// Token: 0x0400C7D4 RID: 51156
		public Sprite cardFrameSynchroOF;

		// Token: 0x0400C7D5 RID: 51157
		public Sprite cardFrameDarkSynchroOF;

		// Token: 0x0400C7D6 RID: 51158
		public Sprite cardFrameXyzOF;

		// Token: 0x0400C7D7 RID: 51159
		public Sprite cardFramePendulumNormalOF;

		// Token: 0x0400C7D8 RID: 51160
		public Sprite cardFramePendulumEffectOF;

		// Token: 0x0400C7D9 RID: 51161
		public Sprite cardFramePendulumXyzOF;

		// Token: 0x0400C7DA RID: 51162
		public Sprite cardFramePendulumSynchroOF;

		// Token: 0x0400C7DB RID: 51163
		public Sprite cardFramePendulumFusionOF;

		// Token: 0x0400C7DC RID: 51164
		public Sprite cardFrameLinkOF;

		// Token: 0x0400C7DD RID: 51165
		public Sprite cardFramePendulumRitualOF;

		// Token: 0x0400C7DE RID: 51166
		[Header("Card Frame Mask")]
		public Texture2D cardFrameMask;

		// Token: 0x0400C7DF RID: 51167
		public Texture2D cardFrameMaskLink;

		// Token: 0x0400C7E0 RID: 51168
		public Texture2D cardFrameMaskPendulum;

		// Token: 0x0400C7E1 RID: 51169
		public Texture2D cardKiraMask;

		// Token: 0x0400C7E2 RID: 51170
		public Texture2D cardKiraMaskLink;

		// Token: 0x0400C7E3 RID: 51171
		public Texture2D cardKiraMaskPendulum;

		// Token: 0x0400C7E4 RID: 51172
		public Texture2D cardNormal;

		// Token: 0x0400C7E5 RID: 51173
		public Texture2D cardNormalLink;

		// Token: 0x0400C7E6 RID: 51174
		public Texture2D cardNormalPendulum;

		// Token: 0x0400C7E7 RID: 51175
		public Texture2D CardKiraNormal03_Millennium;

		// Token: 0x0400C7E8 RID: 51176
		[Header("Card Attribute")]
		public Sprite attributeLight;

		// Token: 0x0400C7E9 RID: 51177
		public Sprite attributeDark;

		// Token: 0x0400C7EA RID: 51178
		public Sprite attributeWater;

		// Token: 0x0400C7EB RID: 51179
		public Sprite attributeFire;

		// Token: 0x0400C7EC RID: 51180
		public Sprite attributeEarth;

		// Token: 0x0400C7ED RID: 51181
		public Sprite attributeWind;

		// Token: 0x0400C7EE RID: 51182
		public Sprite attributeDivine;

		// Token: 0x0400C7EF RID: 51183
		public Sprite attributeSpell;

		// Token: 0x0400C7F0 RID: 51184
		public Sprite attributeTrap;

		// Token: 0x0400C7F1 RID: 51185
		public Sprite attributeLight_Ruby;

		// Token: 0x0400C7F2 RID: 51186
		public Sprite attributeDark_Ruby;

		// Token: 0x0400C7F3 RID: 51187
		public Sprite attributeWater_Ruby;

		// Token: 0x0400C7F4 RID: 51188
		public Sprite attributeFire_Ruby;

		// Token: 0x0400C7F5 RID: 51189
		public Sprite attributeEarth_Ruby;

		// Token: 0x0400C7F6 RID: 51190
		public Sprite attributeWind_Ruby;

		// Token: 0x0400C7F7 RID: 51191
		public Sprite attributeDivine_Ruby;

		// Token: 0x0400C7F8 RID: 51192
		public Sprite attributeSpell_Ruby;

		// Token: 0x0400C7F9 RID: 51193
		public Sprite attributeTrap_Ruby;

		// Token: 0x0400C7FA RID: 51194
		[Header("CardType")]
		public Sprite typeNone;

		// Token: 0x0400C7FB RID: 51195
		public Sprite typeCounter;

		// Token: 0x0400C7FC RID: 51196
		public Sprite typeField;

		// Token: 0x0400C7FD RID: 51197
		public Sprite typeEquip;

		// Token: 0x0400C7FE RID: 51198
		public Sprite typeContinuous;

		// Token: 0x0400C7FF RID: 51199
		public Sprite typeQuickPlay;

		// Token: 0x0400C800 RID: 51200
		public Sprite typeRitual;

		// Token: 0x0400C801 RID: 51201
		public Sprite typeLevel;

		// Token: 0x0400C802 RID: 51202
		public Sprite typeRank;

		// Token: 0x0400C803 RID: 51203
		public Sprite typePendulum;

		// Token: 0x0400C804 RID: 51204
		public Sprite typeLink;

		// Token: 0x0400C805 RID: 51205
		public Sprite typeLevelOff;

		// Token: 0x0400C806 RID: 51206
		public Sprite typeLinkOff;

		// Token: 0x0400C807 RID: 51207
		public Sprite typeLevelNone;

		// Token: 0x0400C808 RID: 51208
		public Sprite typeLevelRank;

		// Token: 0x0400C809 RID: 51209
		[Header("CardLimit")]
		public Sprite banned;

		// Token: 0x0400C80A RID: 51210
		public Sprite limit1;

		// Token: 0x0400C80B RID: 51211
		public Sprite limit2;

		// Token: 0x0400C80C RID: 51212
		[Header("CardRace")]
		public Sprite raceDragon;

		// Token: 0x0400C80D RID: 51213
		public Sprite raceZombie;

		// Token: 0x0400C80E RID: 51214
		public Sprite raceFiend;

		// Token: 0x0400C80F RID: 51215
		public Sprite racePyro;

		// Token: 0x0400C810 RID: 51216
		public Sprite raceSeaSerpent;

		// Token: 0x0400C811 RID: 51217
		public Sprite raceRock;

		// Token: 0x0400C812 RID: 51218
		public Sprite raceMachine;

		// Token: 0x0400C813 RID: 51219
		public Sprite raceFish;

		// Token: 0x0400C814 RID: 51220
		public Sprite raceDinosaur;

		// Token: 0x0400C815 RID: 51221
		public Sprite raceInsect;

		// Token: 0x0400C816 RID: 51222
		public Sprite raceBeast;

		// Token: 0x0400C817 RID: 51223
		public Sprite raceBeastWarrior;

		// Token: 0x0400C818 RID: 51224
		public Sprite racePlant;

		// Token: 0x0400C819 RID: 51225
		public Sprite raceAqua;

		// Token: 0x0400C81A RID: 51226
		public Sprite raceWarrior;

		// Token: 0x0400C81B RID: 51227
		public Sprite raceWindBeast;

		// Token: 0x0400C81C RID: 51228
		public Sprite raceFairy;

		// Token: 0x0400C81D RID: 51229
		public Sprite raceSpellCaster;

		// Token: 0x0400C81E RID: 51230
		public Sprite raceThunder;

		// Token: 0x0400C81F RID: 51231
		public Sprite raceReptile;

		// Token: 0x0400C820 RID: 51232
		public Sprite racePsycho;

		// Token: 0x0400C821 RID: 51233
		public Sprite raceWyrm;

		// Token: 0x0400C822 RID: 51234
		public Sprite raceCyberse;

		// Token: 0x0400C823 RID: 51235
		public Sprite raceDivineBeast;

		// Token: 0x0400C824 RID: 51236
		public Sprite raceIllustion;

		// Token: 0x0400C825 RID: 51237
		public Sprite raceCreatorGod;

		// Token: 0x0400C826 RID: 51238
		[Header("CardCounter")]
		public Sprite counterAlien;

		// Token: 0x0400C827 RID: 51239
		public Sprite counterAthlete;

		// Token: 0x0400C828 RID: 51240
		public Sprite counterBalloon;

		// Token: 0x0400C829 RID: 51241
		public Sprite counterBarrel;

		// Token: 0x0400C82A RID: 51242
		public Sprite counterBF;

		// Token: 0x0400C82B RID: 51243
		public Sprite counterBurn;

		// Token: 0x0400C82C RID: 51244
		public Sprite counterBushido;

		// Token: 0x0400C82D RID: 51245
		public Sprite counterChaos;

		// Token: 0x0400C82E RID: 51246
		public Sprite counterChronicle;

		// Token: 0x0400C82F RID: 51247
		public Sprite counterClock;

		// Token: 0x0400C830 RID: 51248
		public Sprite counterD;

		// Token: 0x0400C831 RID: 51249
		public Sprite counterDeath;

		// Token: 0x0400C832 RID: 51250
		public Sprite counterDefect;

		// Token: 0x0400C833 RID: 51251
		public Sprite counterDeformer;

		// Token: 0x0400C834 RID: 51252
		public Sprite counterDestiny;

		// Token: 0x0400C835 RID: 51253
		public Sprite counterDonguri;

		// Token: 0x0400C836 RID: 51254
		public Sprite counterDouble;

		// Token: 0x0400C837 RID: 51255
		public Sprite counterDragonic;

		// Token: 0x0400C838 RID: 51256
		public Sprite counterEarthBind;

		// Token: 0x0400C839 RID: 51257
		public Sprite counterEM;

		// Token: 0x0400C83A RID: 51258
		public Sprite counterFireStar;

		// Token: 0x0400C83B RID: 51259
		public Sprite counterFlower;

		// Token: 0x0400C83C RID: 51260
		public Sprite counterFog;

		// Token: 0x0400C83D RID: 51261
		public Sprite counterGardna;

		// Token: 0x0400C83E RID: 51262
		public Sprite counterGate;

		// Token: 0x0400C83F RID: 51263
		public Sprite counterGem;

		// Token: 0x0400C840 RID: 51264
		public Sprite counterGenex;

		// Token: 0x0400C841 RID: 51265
		public Sprite counterGG;

		// Token: 0x0400C842 RID: 51266
		public Sprite counterGirl;

		// Token: 0x0400C843 RID: 51267
		public Sprite counterGreed;

		// Token: 0x0400C844 RID: 51268
		public Sprite counterGuard;

		// Token: 0x0400C845 RID: 51269
		public Sprite counterGuard2;

		// Token: 0x0400C846 RID: 51270
		public Sprite counterHopeSlash;

		// Token: 0x0400C847 RID: 51271
		public Sprite counterHoukai;

		// Token: 0x0400C848 RID: 51272
		public Sprite counterHyper;

		// Token: 0x0400C849 RID: 51273
		public Sprite counterIce;

		// Token: 0x0400C84A RID: 51274
		public Sprite counterIllusion;

		// Token: 0x0400C84B RID: 51275
		public Sprite counterJunk;

		// Token: 0x0400C84C RID: 51276
		public Sprite counterKaiju;

		// Token: 0x0400C84D RID: 51277
		public Sprite counterKarakuri;

		// Token: 0x0400C84E RID: 51278
		public Sprite counterKattobing;

		// Token: 0x0400C84F RID: 51279
		public Sprite counterKyoumei;

		// Token: 0x0400C850 RID: 51280
		public Sprite counterMagic;

		// Token: 0x0400C851 RID: 51281
		public Sprite counterNormal;

		// Token: 0x0400C852 RID: 51282
		public Sprite counterOcean;

		// Token: 0x0400C853 RID: 51283
		public Sprite counterOrbital;

		// Token: 0x0400C854 RID: 51284
		public Sprite counterOtoshidama;

		// Token: 0x0400C855 RID: 51285
		public Sprite counterOunokagi;

		// Token: 0x0400C856 RID: 51286
		public Sprite counterPhantasm;

		// Token: 0x0400C857 RID: 51287
		public Sprite counterPiece;

		// Token: 0x0400C858 RID: 51288
		public Sprite counterPlant;

		// Token: 0x0400C859 RID: 51289
		public Sprite counterPolice;

		// Token: 0x0400C85A RID: 51290
		public Sprite counterPredator;

		// Token: 0x0400C85B RID: 51291
		public Sprite counterPsycho;

		// Token: 0x0400C85C RID: 51292
		public Sprite counterPumpkin;

		// Token: 0x0400C85D RID: 51293
		public Sprite counterRabbit;

		// Token: 0x0400C85E RID: 51294
		public Sprite counterScales;

		// Token: 0x0400C85F RID: 51295
		public Sprite counterShark;

		// Token: 0x0400C860 RID: 51296
		public Sprite counterShine;

		// Token: 0x0400C861 RID: 51297
		public Sprite counterSignal;

		// Token: 0x0400C862 RID: 51298
		public Sprite counterSound;

		// Token: 0x0400C863 RID: 51299
		public Sprite counterStone;

		// Token: 0x0400C864 RID: 51300
		public Sprite counterString;

		// Token: 0x0400C865 RID: 51301
		public Sprite counterSummon;

		// Token: 0x0400C866 RID: 51302
		public Sprite counterThunder;

		// Token: 0x0400C867 RID: 51303
		public Sprite counterVenemy;

		// Token: 0x0400C868 RID: 51304
		public Sprite counterVenom;

		// Token: 0x0400C869 RID: 51305
		public Sprite counterWedge;

		// Token: 0x0400C86A RID: 51306
		public Sprite counterWorm;

		// Token: 0x0400C86B RID: 51307
		public Sprite counterYosen;

		// Token: 0x0400C86C RID: 51308
		public Sprite counterZushin;

		// Token: 0x0400C86D RID: 51309
		public Sprite counterKyouai;

		// Token: 0x0400C86E RID: 51310
		public Sprite counterAccess;

		// Token: 0x0400C86F RID: 51311
		public Sprite counterShukudai;

		// Token: 0x0400C870 RID: 51312
		public Sprite counterShiki;

		// Token: 0x0400C871 RID: 51313
		public Sprite counterC;

		// Token: 0x0400C872 RID: 51314
		public Sprite counterDish;

		// Token: 0x0400C873 RID: 51315
		public Sprite counterKyuzai;

		// Token: 0x0400C874 RID: 51316
		public Sprite counterT;

		// Token: 0x0400C875 RID: 51317
		[Header("Button Icon")]
		public Sprite[] battle;

		// Token: 0x0400C876 RID: 51318
		public Sprite[] select;

		// Token: 0x0400C877 RID: 51319
		public Sprite[] spSummon;

		// Token: 0x0400C878 RID: 51320
		public Sprite[] activate;

		// Token: 0x0400C879 RID: 51321
		public Sprite[] summon;

		// Token: 0x0400C87A RID: 51322
		public Sprite[] setSpell;

		// Token: 0x0400C87B RID: 51323
		public Sprite[] setMonster;

		// Token: 0x0400C87C RID: 51324
		public Sprite[] toAttack;

		// Token: 0x0400C87D RID: 51325
		public Sprite[] toDefense;

		// Token: 0x0400C87E RID: 51326
		public Sprite[] setPendulum;

		// Token: 0x0400C87F RID: 51327
		public Sprite[] penSummon;

		// Token: 0x0400C880 RID: 51328
		public Sprite[] cancel;

		// Token: 0x0400C881 RID: 51329
		public Sprite[] decide;

		// Token: 0x0400C882 RID: 51330
		public Sprite[] onTiming;

		// Token: 0x0400C883 RID: 51331
		public Sprite[] offTiming;

		// Token: 0x0400C884 RID: 51332
		public Sprite[] autoTiming;

		// Token: 0x0400C885 RID: 51333
		public Sprite[] onLog;

		// Token: 0x0400C886 RID: 51334
		public Sprite[] offLog;

		// Token: 0x0400C887 RID: 51335
		[Header("Location Icon")]
		public Sprite locationDeck;

		// Token: 0x0400C888 RID: 51336
		public Sprite locationExtra;

		// Token: 0x0400C889 RID: 51337
		public Sprite locationHand;

		// Token: 0x0400C88A RID: 51338
		public Sprite locationGrave;

		// Token: 0x0400C88B RID: 51339
		public Sprite locationRemoved;

		// Token: 0x0400C88C RID: 51340
		public Sprite locationFieldMagic;

		// Token: 0x0400C88D RID: 51341
		public Sprite locationOverlay;

		// Token: 0x0400C88E RID: 51342
		public Sprite locationSearch;

		// Token: 0x0400C88F RID: 51343
		public Sprite locationMyField;

		// Token: 0x0400C890 RID: 51344
		public Sprite locationMyMZone0;

		// Token: 0x0400C891 RID: 51345
		public Sprite locationMyMZone1;

		// Token: 0x0400C892 RID: 51346
		public Sprite locationMyMZone2;

		// Token: 0x0400C893 RID: 51347
		public Sprite locationMyMZone3;

		// Token: 0x0400C894 RID: 51348
		public Sprite locationMyMZone4;

		// Token: 0x0400C895 RID: 51349
		public Sprite locationMyMZone5;

		// Token: 0x0400C896 RID: 51350
		public Sprite locationMyMZone6;

		// Token: 0x0400C897 RID: 51351
		public Sprite locationMySZone0;

		// Token: 0x0400C898 RID: 51352
		public Sprite locationMySZone1;

		// Token: 0x0400C899 RID: 51353
		public Sprite locationMySZone2;

		// Token: 0x0400C89A RID: 51354
		public Sprite locationMySZone3;

		// Token: 0x0400C89B RID: 51355
		public Sprite locationMySZone4;

		// Token: 0x0400C89C RID: 51356
		public Sprite locationOpField;

		// Token: 0x0400C89D RID: 51357
		public Sprite locationOpMZone0;

		// Token: 0x0400C89E RID: 51358
		public Sprite locationOpMZone1;

		// Token: 0x0400C89F RID: 51359
		public Sprite locationOpMZone2;

		// Token: 0x0400C8A0 RID: 51360
		public Sprite locationOpMZone3;

		// Token: 0x0400C8A1 RID: 51361
		public Sprite locationOpMZone4;

		// Token: 0x0400C8A2 RID: 51362
		public Sprite locationOpMZone5;

		// Token: 0x0400C8A3 RID: 51363
		public Sprite locationOpMZone6;

		// Token: 0x0400C8A4 RID: 51364
		public Sprite locationOpSZone0;

		// Token: 0x0400C8A5 RID: 51365
		public Sprite locationOpSZone1;

		// Token: 0x0400C8A6 RID: 51366
		public Sprite locationOpSZone2;

		// Token: 0x0400C8A7 RID: 51367
		public Sprite locationOpSZone3;

		// Token: 0x0400C8A8 RID: 51368
		public Sprite locationOpSZone4;

		// Token: 0x0400C8A9 RID: 51369
		[Header("Card Controller Icon")]
		public Sprite controllerMe;

		// Token: 0x0400C8AA RID: 51370
		public Sprite controllerOp;

		// Token: 0x0400C8AB RID: 51371
		public Sprite controllerOther;

		// Token: 0x0400C8AC RID: 51372
		public Sprite controllerOther2;

		// Token: 0x0400C8AD RID: 51373
		[Header("Card List Location Icon")]
		public Sprite listMyDeck;

		// Token: 0x0400C8AE RID: 51374
		public Sprite listOpDeck;

		// Token: 0x0400C8AF RID: 51375
		public Sprite listMyExtra;

		// Token: 0x0400C8B0 RID: 51376
		public Sprite listOpExtra;

		// Token: 0x0400C8B1 RID: 51377
		public Sprite listMyGrave;

		// Token: 0x0400C8B2 RID: 51378
		public Sprite listOpGrave;

		// Token: 0x0400C8B3 RID: 51379
		public Sprite listMyRemoved;

		// Token: 0x0400C8B4 RID: 51380
		public Sprite listOpRemoved;

		// Token: 0x0400C8B5 RID: 51381
		public Sprite listMyXyz;

		// Token: 0x0400C8B6 RID: 51382
		public Sprite listOpXyz;

		// Token: 0x0400C8B7 RID: 51383
		[Header("Card Affect")]
		public Sprite CardAffectDisable;

		// Token: 0x0400C8B8 RID: 51384
		public Sprite CardAffectEquip;

		// Token: 0x0400C8B9 RID: 51385
		public Sprite CardAffectField;

		// Token: 0x0400C8BA RID: 51386
		public Sprite CardAffectPermanent;

		// Token: 0x0400C8BB RID: 51387
		public Sprite CardAffectPower;

		// Token: 0x0400C8BC RID: 51388
		public Sprite CardAffectTarget;

		// Token: 0x0400C8BD RID: 51389
		[Header("Link Count")]
		public Sprite link1;

		// Token: 0x0400C8BE RID: 51390
		public Sprite link2;

		// Token: 0x0400C8BF RID: 51391
		public Sprite link3;

		// Token: 0x0400C8C0 RID: 51392
		public Sprite link4;

		// Token: 0x0400C8C1 RID: 51393
		public Sprite link5;

		// Token: 0x0400C8C2 RID: 51394
		public Sprite link6;

		// Token: 0x0400C8C3 RID: 51395
		public Sprite link1R;

		// Token: 0x0400C8C4 RID: 51396
		public Sprite link2R;

		// Token: 0x0400C8C5 RID: 51397
		public Sprite link3R;

		// Token: 0x0400C8C6 RID: 51398
		public Sprite link4R;

		// Token: 0x0400C8C7 RID: 51399
		public Sprite link5R;

		// Token: 0x0400C8C8 RID: 51400
		public Sprite link6R;

		// Token: 0x0400C8C9 RID: 51401
		public Sprite link7R;

		// Token: 0x0400C8CA RID: 51402
		public Sprite link8R;

		// Token: 0x0400C8CB RID: 51403
		[Header("Card Pool")]
		public Sprite cardPoolOCG;

		// Token: 0x0400C8CC RID: 51404
		public Sprite cardPoolTCG;

		// Token: 0x0400C8CD RID: 51405
		public Sprite cardPoolDIY;

		// Token: 0x0400C8CE RID: 51406
		public Sprite cardPoolPRE;

		// Token: 0x0400C8CF RID: 51407
		[Header("Chain Circle Num")]
		public Sprite chainCircleNum0;

		// Token: 0x0400C8D0 RID: 51408
		public Sprite chainCircleNum1;

		// Token: 0x0400C8D1 RID: 51409
		public Sprite chainCircleNum2;

		// Token: 0x0400C8D2 RID: 51410
		public Sprite chainCircleNum3;

		// Token: 0x0400C8D3 RID: 51411
		public Sprite chainCircleNum4;

		// Token: 0x0400C8D4 RID: 51412
		public Sprite chainCircleNum5;

		// Token: 0x0400C8D5 RID: 51413
		public Sprite chainCircleNum6;

		// Token: 0x0400C8D6 RID: 51414
		public Sprite chainCircleNum7;

		// Token: 0x0400C8D7 RID: 51415
		public Sprite chainCircleNum8;

		// Token: 0x0400C8D8 RID: 51416
		public Sprite chainCircleNum9;

		// Token: 0x0400C8D9 RID: 51417
		[Header("Chain Num Set")]
		public Sprite chainNumSet0;

		// Token: 0x0400C8DA RID: 51418
		public Sprite chainNumSet1;

		// Token: 0x0400C8DB RID: 51419
		public Sprite chainNumSet2;

		// Token: 0x0400C8DC RID: 51420
		public Sprite chainNumSet3;

		// Token: 0x0400C8DD RID: 51421
		public Sprite chainNumSet4;

		// Token: 0x0400C8DE RID: 51422
		public Sprite chainNumSet5;

		// Token: 0x0400C8DF RID: 51423
		public Sprite chainNumSet6;

		// Token: 0x0400C8E0 RID: 51424
		public Sprite chainNumSet7;

		// Token: 0x0400C8E1 RID: 51425
		public Sprite chainNumSet8;

		// Token: 0x0400C8E2 RID: 51426
		public Sprite chainNumSet9;

		// Token: 0x0400C8E3 RID: 51427
		[Header("Window")]
		public Sprite toggleM;

		// Token: 0x0400C8E4 RID: 51428
		public Sprite toggleM_On;

		// Token: 0x0400C8E5 RID: 51429
		public Sprite toggleM_Over;

		// Token: 0x0400C8E6 RID: 51430
		[Header("Rank")]
		public Sprite rankBG01;

		// Token: 0x0400C8E7 RID: 51431
		public Sprite rankBG02;

		// Token: 0x0400C8E8 RID: 51432
		public Sprite rankBG03;

		// Token: 0x0400C8E9 RID: 51433
		public Sprite rankBG04;

		// Token: 0x0400C8EA RID: 51434
		public Sprite rankBG05;

		// Token: 0x0400C8EB RID: 51435
		public Sprite rankBG06;

		// Token: 0x0400C8EC RID: 51436
		public Sprite rankBG07;

		// Token: 0x0400C8ED RID: 51437
		public Sprite rankBG08;

		// Token: 0x0400C8EE RID: 51438
		public Sprite rankIcon01;

		// Token: 0x0400C8EF RID: 51439
		public Sprite rankIcon02;

		// Token: 0x0400C8F0 RID: 51440
		public Sprite rankIcon03;

		// Token: 0x0400C8F1 RID: 51441
		public Sprite rankIcon04;

		// Token: 0x0400C8F2 RID: 51442
		public Sprite rankIcon05;

		// Token: 0x0400C8F3 RID: 51443
		public Sprite rankIcon06;

		// Token: 0x0400C8F4 RID: 51444
		public Sprite rankIcon07;

		// Token: 0x0400C8F5 RID: 51445
		public Sprite rankIcon08;

		// Token: 0x0400C8F6 RID: 51446
		public Sprite rankTier01;

		// Token: 0x0400C8F7 RID: 51447
		public Sprite rankTier02;

		// Token: 0x0400C8F8 RID: 51448
		public Sprite rankTier03;

		// Token: 0x0400C8F9 RID: 51449
		public Sprite rankTier04;

		// Token: 0x0400C8FA RID: 51450
		public Sprite rankTier05;

		// Token: 0x0400C8FB RID: 51451
		[Header("Rush Duel")]
		public Sprite rd_Arrow_B;

		// Token: 0x0400C8FC RID: 51452
		public Sprite rd_Arrow_BL;

		// Token: 0x0400C8FD RID: 51453
		public Sprite rd_Arrow_BR;

		// Token: 0x0400C8FE RID: 51454
		public Sprite rd_Arrow_L;

		// Token: 0x0400C8FF RID: 51455
		public Sprite rd_Arrow_R;

		// Token: 0x0400C900 RID: 51456
		public Sprite rd_Arrow_U;

		// Token: 0x0400C901 RID: 51457
		public Sprite rd_Arrow_UL;

		// Token: 0x0400C902 RID: 51458
		public Sprite rd_Arrow_UR;

		// Token: 0x0400C903 RID: 51459
		public Sprite rd_Attribute_Dark;

		// Token: 0x0400C904 RID: 51460
		public Sprite rd_Attribute_Divine;

		// Token: 0x0400C905 RID: 51461
		public Sprite rd_Attribute_Earth;

		// Token: 0x0400C906 RID: 51462
		public Sprite rd_Attribute_Fire;

		// Token: 0x0400C907 RID: 51463
		public Sprite rd_Attribute_Light;

		// Token: 0x0400C908 RID: 51464
		public Sprite rd_Attribute_Spell;

		// Token: 0x0400C909 RID: 51465
		public Sprite rd_Attribute_Trap;

		// Token: 0x0400C90A RID: 51466
		public Sprite rd_Attribute_Water;

		// Token: 0x0400C90B RID: 51467
		public Sprite rd_Attribute_Wind;

		// Token: 0x0400C90C RID: 51468
		public Sprite rd_Frame_Effect;

		// Token: 0x0400C90D RID: 51469
		public Sprite rd_Frame_Fusion;

		// Token: 0x0400C90E RID: 51470
		public Sprite rd_Frame_Link;

		// Token: 0x0400C90F RID: 51471
		public Sprite rd_Frame_Normal;

		// Token: 0x0400C910 RID: 51472
		public Sprite rd_Frame_Obelisk;

		// Token: 0x0400C911 RID: 51473
		public Sprite rd_Frame_PendulumEffect;

		// Token: 0x0400C912 RID: 51474
		public Sprite rd_Frame_PendulumFusion;

		// Token: 0x0400C913 RID: 51475
		public Sprite rd_Frame_PendulumLink;

		// Token: 0x0400C914 RID: 51476
		public Sprite rd_Frame_PendulumNormal;

		// Token: 0x0400C915 RID: 51477
		public Sprite rd_Frame_PendulumRitual;

		// Token: 0x0400C916 RID: 51478
		public Sprite rd_Frame_PendulumSynchro;

		// Token: 0x0400C917 RID: 51479
		public Sprite rd_Frame_PendulumXyz;

		// Token: 0x0400C918 RID: 51480
		public Sprite rd_Frame_Ra;

		// Token: 0x0400C919 RID: 51481
		public Sprite rd_Frame_Ritual;

		// Token: 0x0400C91A RID: 51482
		public Sprite rd_Frame_Slifer;

		// Token: 0x0400C91B RID: 51483
		public Sprite rd_Frame_Spell;

		// Token: 0x0400C91C RID: 51484
		public Sprite rd_Frame_Synchro;

		// Token: 0x0400C91D RID: 51485
		public Sprite rd_Frame_Token;

		// Token: 0x0400C91E RID: 51486
		public Sprite rd_Frame_Trap;

		// Token: 0x0400C91F RID: 51487
		public Sprite rd_Frame_Xyz;

		// Token: 0x0400C920 RID: 51488
		public Sprite rd_Loading_Effect;

		// Token: 0x0400C921 RID: 51489
		public Sprite rd_Loading_Fusion;

		// Token: 0x0400C922 RID: 51490
		public Sprite rd_Loading_Link;

		// Token: 0x0400C923 RID: 51491
		public Sprite rd_Loading_Normal;

		// Token: 0x0400C924 RID: 51492
		public Sprite rd_Loading_Obelisk;

		// Token: 0x0400C925 RID: 51493
		public Sprite rd_Loading_PendulumEffect;

		// Token: 0x0400C926 RID: 51494
		public Sprite rd_Loading_PendulumFusion;

		// Token: 0x0400C927 RID: 51495
		public Sprite rd_Loading_PendulumLink;

		// Token: 0x0400C928 RID: 51496
		public Sprite rd_Loading_PendulumNormal;

		// Token: 0x0400C929 RID: 51497
		public Sprite rd_Loading_PendulumRitual;

		// Token: 0x0400C92A RID: 51498
		public Sprite rd_Loading_PendulumSynchro;

		// Token: 0x0400C92B RID: 51499
		public Sprite rd_Loading_PendulumXyz;

		// Token: 0x0400C92C RID: 51500
		public Sprite rd_Loading_Ra;

		// Token: 0x0400C92D RID: 51501
		public Sprite rd_Loading_Ritual;

		// Token: 0x0400C92E RID: 51502
		public Sprite rd_Loading_Slifer;

		// Token: 0x0400C92F RID: 51503
		public Sprite rd_Loading_Spell;

		// Token: 0x0400C930 RID: 51504
		public Sprite rd_Loading_Synchro;

		// Token: 0x0400C931 RID: 51505
		public Sprite rd_Loading_Token;

		// Token: 0x0400C932 RID: 51506
		public Sprite rd_Loading_Trap;

		// Token: 0x0400C933 RID: 51507
		public Sprite rd_Loading_Xyz;

		// Token: 0x0400C934 RID: 51508
		public Texture2D rd_Mask;

		// Token: 0x0400C935 RID: 51509
		public Texture2D rd_KiraMask;

		// Token: 0x0400C936 RID: 51510
		public Texture2D rd_KiraMaskPendulum;

		// Token: 0x0400C937 RID: 51511
		public Texture2D rd_CardAttributeSet;

		// Token: 0x0400C938 RID: 51512
		public Texture2D rd_CardNormal;

		// Token: 0x0400C939 RID: 51513
		[Header("Gamepad Icon")]
		public Sprite gamepad_ButtonSouth_Xbox;

		// Token: 0x0400C93A RID: 51514
		public Sprite gamepad_ButtonEast_Xbox;

		// Token: 0x0400C93B RID: 51515
		public Sprite gamepad_ButtonWest_Xbox;

		// Token: 0x0400C93C RID: 51516
		public Sprite gamepad_ButtonNorth_Xbox;

		// Token: 0x0400C93D RID: 51517
		public Sprite gamepad_ButtonSouth_PlayStation;

		// Token: 0x0400C93E RID: 51518
		public Sprite gamepad_ButtonEast_PlayStation;

		// Token: 0x0400C93F RID: 51519
		public Sprite gamepad_ButtonWest_PlayStation;

		// Token: 0x0400C940 RID: 51520
		public Sprite gamepad_ButtonNorth_PlayStation;

		// Token: 0x0400C941 RID: 51521
		public Sprite gamepad_ButtonSouth_Nintendo;

		// Token: 0x0400C942 RID: 51522
		public Sprite gamepad_ButtonEast_Nintendo;

		// Token: 0x0400C943 RID: 51523
		public Sprite gamepad_ButtonWest_Nintendo;

		// Token: 0x0400C944 RID: 51524
		public Sprite gamepad_ButtonNorth_Nintendo;

		// Token: 0x0400C945 RID: 51525
		public Sprite gamepad_LeftShoulder_Xbox;

		// Token: 0x0400C946 RID: 51526
		public Sprite gamepad_RightShoulder_Xbox;

		// Token: 0x0400C947 RID: 51527
		public Sprite gamepad_LeftTrigger_Xbox;

		// Token: 0x0400C948 RID: 51528
		public Sprite gamepad_RightTrigger_Xbox;

		// Token: 0x0400C949 RID: 51529
		public Sprite gamepad_LeftShoulder_PlayStation;

		// Token: 0x0400C94A RID: 51530
		public Sprite gamepad_RightShoulder_PlayStation;

		// Token: 0x0400C94B RID: 51531
		public Sprite gamepad_LeftTrigger_PlayStation;

		// Token: 0x0400C94C RID: 51532
		public Sprite gamepad_RightTrigger_PlayStation;

		// Token: 0x0400C94D RID: 51533
		public Sprite gamepad_LeftShoulder_Nintendo;

		// Token: 0x0400C94E RID: 51534
		public Sprite gamepad_RightShoulder_Nintendo;

		// Token: 0x0400C94F RID: 51535
		public Sprite gamepad_LeftTrigger_Nintendo;

		// Token: 0x0400C950 RID: 51536
		public Sprite gamepad_RightTrigger_Nintendo;

		// Token: 0x0400C951 RID: 51537
		public Sprite gamepad_LeftStick;

		// Token: 0x0400C952 RID: 51538
		public Sprite gamepad_RightStick;

		// Token: 0x0400C953 RID: 51539
		public Sprite gamepad_Select_Xbox;

		// Token: 0x0400C954 RID: 51540
		public Sprite gamepad_Start_Xbox;

		// Token: 0x0400C955 RID: 51541
		public Sprite gamepad_Select_PlayStation;

		// Token: 0x0400C956 RID: 51542
		public Sprite gamepad_Start_PlayStation;

		// Token: 0x0400C957 RID: 51543
		public Sprite gamepad_Select_Nintendo;

		// Token: 0x0400C958 RID: 51544
		public Sprite gamepad_Start_Nintendo;

		// Token: 0x0400C959 RID: 51545
		private int[] rankRange = new int[] { 1000, 1100, 1200, 1300, 1400, 1500, 1600, 1700 };
	}
}
