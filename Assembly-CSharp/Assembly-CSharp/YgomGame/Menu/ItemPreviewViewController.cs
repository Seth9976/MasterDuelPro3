using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomGame.Bg;
using YgomGame.Duel;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000A9C RID: 2716
	public class ItemPreviewViewController : BaseMenuViewController, IDynamicChangeDispHeaderSupported
	{
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06004F24 RID: 20260 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004F25 RID: 20261 RVA: 0x0000216D File Offset: 0x0000036D
		private void ApplaySettings(Camera camera, ItemPreviewViewController.CameraSettings cameraSettings)
		{
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitMateController()
		{
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitCameraController()
		{
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x0000216D File Offset: 0x0000036D
		private void BindItem()
		{
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x0000216D File Offset: 0x0000036D
		private void BindItemIconRoot(GameObject itemIconRoot, bool visible, bool isPeriod, int itemCategory, int itemId)
		{
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x0000216D File Offset: 0x0000036D
		private void BindTag()
		{
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitMateSettings()
		{
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayMateMotion()
		{
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x0000216D File Offset: 0x0000036D
		private void BindField(Dictionary<string, object> fieldArgs)
		{
		}

		// Token: 0x06004F32 RID: 20274 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateField(Dictionary<string, object> fieldArgs)
		{
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateFieldParts()
		{
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x0000216D File Offset: 0x0000036D
		private void LodPrefab(string prefPath, Vector3 modelPos, Vector3 modelRot, Vector3 modelScale, Vector3 camPos, Vector3 camRot, int renderTexW = 256, int renderTexH = 256, float imageW = -1f, float imageH = -1f)
		{
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x000029CC File Offset: 0x00000BCC
		public HeaderViewController.IsDispHeader IsDispContents()
		{
			return (HeaderViewController.IsDispHeader)0;
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator WallPaperBack()
		{
			return null;
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x0000216D File Offset: 0x0000036D
		private void MoveCamera(Vector3 movePos)
		{
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDrag(SelectionItem.DragStatus dragStatus, Vector2 vec)
		{
		}

		// Token: 0x06004F3A RID: 20282 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputKeyUp()
		{
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputKeyDown()
		{
		}

		// Token: 0x06004F3C RID: 20284 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputKeyLeft()
		{
		}

		// Token: 0x06004F3D RID: 20285 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputKeyRight()
		{
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAnalogKey(Vector2 input)
		{
		}

		// Token: 0x04008CDA RID: 36058
		private readonly string MATE_TRANSFORM_SETTING_PATH;

		// Token: 0x04008CDB RID: 36059
		private readonly string ROOT_ICON_S_LABEL;

		// Token: 0x04008CDC RID: 36060
		private readonly string ROOT_ICON_M_LABEL;

		// Token: 0x04008CDD RID: 36061
		private readonly string ROOT_ICON_L_LABEL;

		// Token: 0x04008CDE RID: 36062
		private readonly string ROOT_ICON_LL_LABEL;

		// Token: 0x04008CDF RID: 36063
		private readonly string ROOT_ICON_PROTECTOR_LABEL;

		// Token: 0x04008CE0 RID: 36064
		private readonly string ROOT_ICON_EFFECT_LABEL;

		// Token: 0x04008CE1 RID: 36065
		private readonly string ROOT_MATE_LABEL;

		// Token: 0x04008CE2 RID: 36066
		private readonly string ROOT_FIELD_LABEL;

		// Token: 0x04008CE3 RID: 36067
		private readonly string ROOT_TAG_LABEL;

		// Token: 0x04008CE4 RID: 36068
		private readonly string ROOT_TAG_IMAGE_LABEL;

		// Token: 0x04008CE5 RID: 36069
		private readonly string ROOT_WALLPAPER_LABEL;

		// Token: 0x04008CE6 RID: 36070
		private readonly string TEXT_ITEM_NUM_LABEL;

		// Token: 0x04008CE7 RID: 36071
		private readonly string TEXT_ITEM_NAME_LABEL;

		// Token: 0x04008CE8 RID: 36072
		private readonly string TEXT_ITEM_CATEGORY_LABEL;

		// Token: 0x04008CE9 RID: 36073
		private readonly string TEXT_ITEM_DESC_LABEL;

		// Token: 0x04008CEA RID: 36074
		private readonly string TMP_TAG_LABEL;

		// Token: 0x04008CEB RID: 36075
		private readonly string TXT_TAG_LABEL;

		// Token: 0x04008CEC RID: 36076
		private readonly string BTN_LABEL;

		// Token: 0x04008CED RID: 36077
		public const string k_ArgKeyIsPeriod = "isPeriod";

		// Token: 0x04008CEE RID: 36078
		public const string k_ArgKeyItemId = "itemId";

		// Token: 0x04008CEF RID: 36079
		public const string k_ArgKeyItemCategory = "itemCategory";

		// Token: 0x04008CF0 RID: 36080
		public const string k_ArgKeyItemNum = "itemNum";

		// Token: 0x04008CF1 RID: 36081
		public const string k_ArgKeyFieldIds = "fieldIds";

		// Token: 0x04008CF2 RID: 36082
		public const string k_ArgKeyOpenAsDialog = "openAsDialog";

		// Token: 0x04008CF3 RID: 36083
		public const string k_ArgKeyField = "field";

		// Token: 0x04008CF4 RID: 36084
		public const string k_ArgKeyFieldOpposite = "fieldOpposite";

		// Token: 0x04008CF5 RID: 36085
		public const string k_ArgKeyAvatarBase = "avatarBase";

		// Token: 0x04008CF6 RID: 36086
		public const string k_ArgKeyAvatarBaseOpposite = "avatarBaseOpposite";

		// Token: 0x04008CF7 RID: 36087
		public const string k_ArgKeyFieldObj = "fieldObj";

		// Token: 0x04008CF8 RID: 36088
		public const string k_ArgKeyFieldObjOpposite = "fieldObjOpposite";

		// Token: 0x04008CF9 RID: 36089
		public const string k_ArgKeyAvatar = "avatar";

		// Token: 0x04008CFA RID: 36090
		public const string k_ArgKeyAvatarOpposite = "avatarOpposite";

		// Token: 0x04008CFB RID: 36091
		private readonly string k_ELabelRoot3D;

		// Token: 0x04008CFC RID: 36092
		private readonly string k_ELabelFieldLocator;

		// Token: 0x04008CFD RID: 36093
		private readonly string k_ELabelScreenTouchButton;

		// Token: 0x04008CFE RID: 36094
		private readonly string k_ELabelBadgeLocator;

		// Token: 0x04008CFF RID: 36095
		private bool m_IsPeriod;

		// Token: 0x04008D00 RID: 36096
		private int m_ItemId;

		// Token: 0x04008D01 RID: 36097
		private int m_ItemCategory;

		// Token: 0x04008D02 RID: 36098
		private Dictionary<string, object> m_FieldArgs;

		// Token: 0x04008D03 RID: 36099
		private int m_ItemNum;

		// Token: 0x04008D04 RID: 36100
		private TextMeshProUGUI m_ItemNumText;

		// Token: 0x04008D05 RID: 36101
		private TextMeshProUGUI m_ItemNameText;

		// Token: 0x04008D06 RID: 36102
		private TextMeshProUGUI m_ItemDescText;

		// Token: 0x04008D07 RID: 36103
		private TextMeshProUGUI m_ItemCategoryText;

		// Token: 0x04008D08 RID: 36104
		private SelectionButton m_CancelButton;

		// Token: 0x04008D09 RID: 36105
		private Character2D chara;

		// Token: 0x04008D0A RID: 36106
		private ItemPreview2D itemPreview;

		// Token: 0x04008D0B RID: 36107
		private Transform m_RootField;

		// Token: 0x04008D0C RID: 36108
		private Transform m_Root3D;

		// Token: 0x04008D0D RID: 36109
		private BgPreview m_BgActor;

		// Token: 0x04008D0E RID: 36110
		private Vector2 m_DragStartVec;

		// Token: 0x04008D0F RID: 36111
		private GameObject currentWallpaperGo;

		// Token: 0x04008D10 RID: 36112
		private GameObject m_BadgeLocator;

		// Token: 0x04008D11 RID: 36113
		private ItemPreviewViewController.CameraSettings fieldCameraSettings;

		// Token: 0x04008D12 RID: 36114
		private ItemPreviewViewController.CameraSettings fieldPartsCameraSettings;

		// Token: 0x04008D13 RID: 36115
		private ItemPreviewViewController.CameraSettings avatarBaseCameraSettings;

		// Token: 0x04008D14 RID: 36116
		private float x_AxisMax;

		// Token: 0x04008D15 RID: 36117
		private float x_AxisMin;

		// Token: 0x04008D16 RID: 36118
		private float y_AxisMax;

		// Token: 0x04008D17 RID: 36119
		private float y_AxisMin;

		// Token: 0x04008D18 RID: 36120
		private const float m_MoveAmountDirectionalKey = 5f;

		// Token: 0x04008D19 RID: 36121
		private const float m_MoveAmountAnalogKey = 5f;

		// Token: 0x04008D1A RID: 36122
		private float m_MoveAmountTouch;

		// Token: 0x04008D1B RID: 36123
		private const float m_MoveAmountTouchField = 20f;

		// Token: 0x04008D1C RID: 36124
		private const float m_MoveAmountTouchParts = 20f;

		// Token: 0x04008D1D RID: 36125
		private bool isInitialized;

		// Token: 0x04008D1E RID: 36126
		private bool m_ItemEffectVisible;

		// Token: 0x04008D1F RID: 36127
		private ItemPreviewViewController.RootIconType m_ItemIconType;

		// Token: 0x02000A9D RID: 2717
		private enum RootIconType
		{
			// Token: 0x04008D21 RID: 36129
			None,
			// Token: 0x04008D22 RID: 36130
			S,
			// Token: 0x04008D23 RID: 36131
			M,
			// Token: 0x04008D24 RID: 36132
			L,
			// Token: 0x04008D25 RID: 36133
			LL,
			// Token: 0x04008D26 RID: 36134
			Protector
		}

		// Token: 0x02000A9E RID: 2718
		private struct CameraSettings
		{
			// Token: 0x04008D27 RID: 36135
			public float xMax;

			// Token: 0x04008D28 RID: 36136
			public float xMin;

			// Token: 0x04008D29 RID: 36137
			public float yMax;

			// Token: 0x04008D2A RID: 36138
			public float yMin;

			// Token: 0x04008D2B RID: 36139
			public float fov;

			// Token: 0x04008D2C RID: 36140
			public float nearClip;

			// Token: 0x04008D2D RID: 36141
			public float farClip;
		}
	}
}
