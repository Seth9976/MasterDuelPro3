using System;
using Cysharp.Threading.Tasks;
using MDPro3;
using UnityEngine;

namespace YgomSystem.Effect
{
	// Token: 0x02000788 RID: 1928
	public class SpriteScaler : MonoBehaviour
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06003BE4 RID: 15332 RVA: 0x000F3F8A File Offset: 0x000F218A
		// (set) Token: 0x06003BE5 RID: 15333 RVA: 0x000F3F92 File Offset: 0x000F2192
		public bool isApplyOnUpdate
		{
			get
			{
				return this.applyOnUpdate;
			}
			set
			{
				this.applyOnUpdate = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06003BE6 RID: 15334 RVA: 0x000F3F9B File Offset: 0x000F219B
		// (set) Token: 0x06003BE7 RID: 15335 RVA: 0x000F3FA3 File Offset: 0x000F21A3
		public bool useDirectSizeSetting
		{
			get
			{
				return this._useDirectSizeSetting;
			}
			set
			{
				this._useDirectSizeSetting = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06003BE8 RID: 15336 RVA: 0x000F3FAC File Offset: 0x000F21AC
		// (set) Token: 0x06003BE9 RID: 15337 RVA: 0x000F3FB4 File Offset: 0x000F21B4
		public Vector2 directSizeSetting
		{
			get
			{
				return this._directSizeSetting;
			}
			set
			{
				this._directSizeSetting = value;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06003BEA RID: 15338 RVA: 0x000F3FBD File Offset: 0x000F21BD
		// (set) Token: 0x06003BEB RID: 15339 RVA: 0x000F3FC5 File Offset: 0x000F21C5
		public bool changePosition
		{
			get
			{
				return this._changePosition;
			}
			set
			{
				this._changePosition = value;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06003BEC RID: 15340 RVA: 0x000F3FCE File Offset: 0x000F21CE
		// (set) Token: 0x06003BED RID: 15341 RVA: 0x000F3FD6 File Offset: 0x000F21D6
		public bool useFixedDepth
		{
			get
			{
				return this._isUseFixedDepth;
			}
			set
			{
				this._isUseFixedDepth = value;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06003BEE RID: 15342 RVA: 0x000F3FDF File Offset: 0x000F21DF
		// (set) Token: 0x06003BEF RID: 15343 RVA: 0x000F3FE7 File Offset: 0x000F21E7
		public float fixedDepth
		{
			get
			{
				return this._fixedDepth;
			}
			set
			{
				this._fixedDepth = value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x000F3FF0 File Offset: 0x000F21F0
		// (set) Token: 0x06003BF1 RID: 15345 RVA: 0x000F3FF8 File Offset: 0x000F21F8
		public Camera viewCamera { get; private set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x000F4001 File Offset: 0x000F2201
		// (set) Token: 0x06003BF3 RID: 15347 RVA: 0x000F4009 File Offset: 0x000F2209
		public bool scaleYtoZ
		{
			get
			{
				return this.m_ScaleYtoZ;
			}
			set
			{
				this.m_ScaleYtoZ = value;
			}
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x000F4012 File Offset: 0x000F2212
		public void SetFitMode(SpriteScaler.FitMode fitMode)
		{
			this.fitMode = fitMode;
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x000F401B File Offset: 0x000F221B
		public SpriteScaler.FitMode GetFitMode()
		{
			return this.fitMode;
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x000F4023 File Offset: 0x000F2223
		public Sprite TryGetTargetSprite()
		{
			if (this.targetSprite != null)
			{
				return this.targetSprite.sprite;
			}
			return null;
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x000F4040 File Offset: 0x000F2240
		private void OnEnable()
		{
			this.SetupAsync();
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x000F404C File Offset: 0x000F224C
		private async UniTask SetupAsync()
		{
			if (!(this.targetSprite == null) || base.gameObject.TryGetComponent<SpriteRenderer>(out this.targetSprite))
			{
				this.targetSprite.enabled = false;
				await UniTask.Yield(base.destroyCancellationToken, false);
				await UniTask.Yield(base.destroyCancellationToken, false);
				this.targetSprite.enabled = true;
				int layer = base.gameObject.layer;
				Camera camera;
				if (layer != 3)
				{
					switch (layer)
					{
					case 16:
						camera = Program.instance.camera_.cameraDuelOverlay3D;
						break;
					case 17:
						camera = Program.instance.camera_.cameraDuelOverlayEffect3D;
						break;
					case 18:
						camera = Program.instance.camera_.cameraDuelOverlay2D;
						break;
					case 19:
						camera = Program.instance.camera_.cameraDuelOverlayEffect2D;
						break;
					default:
						camera = Program.instance.camera_.cameraMain;
						break;
					}
				}
				else
				{
					camera = Program.instance.camera_.camera2D;
				}
				Camera viewCamera = camera;
				this.Setup(viewCamera);
			}
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x000F408F File Offset: 0x000F228F
		public void Setup(Camera view_camera)
		{
			this.viewCamera = view_camera;
			this.Apply();
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x000F40A0 File Offset: 0x000F22A0
		public void Apply()
		{
			if (this.viewCamera == null)
			{
				return;
			}
			float depth = Vector3.Dot(base.transform.position - this.viewCamera.transform.position, this.viewCamera.transform.forward);
			if (this._isUseFixedDepth)
			{
				depth = this._fixedDepth;
			}
			if (this._changePosition)
			{
				base.transform.position = this.viewCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, depth));
				base.transform.eulerAngles = this.viewCamera.transform.eulerAngles;
			}
			float screenWidth;
			float screenHeight;
			if (this.viewCamera.orthographic)
			{
				Vector3 bottomLeft = this.viewCamera.ViewportToWorldPoint(new Vector3(0f, 0f, depth));
				Vector3 vector = this.viewCamera.ViewportToWorldPoint(new Vector3(1f, 1f, depth));
				screenWidth = vector.x - bottomLeft.x;
				screenHeight = vector.y - bottomLeft.y;
			}
			else
			{
				float halfFOV = this.viewCamera.fieldOfView * 0.5f * 0.017453292f;
				screenHeight = 2f * depth * Mathf.Tan(halfFOV);
				screenWidth = screenHeight * this.viewCamera.aspect;
			}
			this.Apply(screenWidth, screenHeight);
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x000F41EC File Offset: 0x000F23EC
		public void Apply(float screenWidth, float screenHeight)
		{
			if (this.isApplied && this.appliedScreenSize.x == screenWidth && this.appliedScreenSize.y == screenHeight)
			{
				return;
			}
			if (this.targetSprite == null)
			{
				this.targetSprite = base.GetComponent<SpriteRenderer>();
			}
			if (this.targetSprite == null)
			{
				return;
			}
			Sprite sprite = this.TryGetTargetSprite();
			if (sprite == null)
			{
				return;
			}
			Vector2 spriteSize = sprite.rect.size / sprite.pixelsPerUnit;
			Vector2 targetSize;
			if (this._useDirectSizeSetting)
			{
				targetSize = this._directSizeSetting;
			}
			else
			{
				switch (this.fitMode)
				{
				case SpriteScaler.FitMode.None:
					targetSize = spriteSize;
					break;
				case SpriteScaler.FitMode.FitWidth:
					targetSize = new Vector2(screenWidth, spriteSize.y);
					break;
				case SpriteScaler.FitMode.FitHeight:
					targetSize = new Vector2(spriteSize.x, screenHeight);
					break;
				case SpriteScaler.FitMode.FitWidthMaintainAspectRatio:
				{
					float widthScale = screenWidth / spriteSize.x;
					targetSize = spriteSize * widthScale;
					break;
				}
				case SpriteScaler.FitMode.FitHeightMaintainAspectRatio:
				{
					float heightScale = screenHeight / spriteSize.y;
					targetSize = spriteSize * heightScale;
					break;
				}
				case SpriteScaler.FitMode.FitWidthHeight:
					targetSize = new Vector2(screenWidth, screenHeight);
					break;
				case SpriteScaler.FitMode.FitHighestResolutionMaintainAspectRatio:
				{
					float maxScale = Mathf.Max(screenWidth / spriteSize.x, screenHeight / spriteSize.y);
					targetSize = spriteSize * maxScale;
					break;
				}
				case SpriteScaler.FitMode.FitLowestResolutionMaintainAspectRatio:
				{
					float minScale = Mathf.Min(screenWidth / spriteSize.x, screenHeight / spriteSize.y);
					targetSize = spriteSize * minScale;
					break;
				}
				default:
					targetSize = spriteSize;
					break;
				}
			}
			Vector3 newScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
			newScale += this.offsetScale;
			if (this.m_ScaleYtoZ)
			{
				newScale.z = newScale.y;
			}
			base.transform.localScale = newScale;
			this.isApplied = true;
			this.appliedScreenSize = new Vector2(screenWidth, screenHeight);
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x000F43C8 File Offset: 0x000F25C8
		public void Reapply()
		{
			this.isApplied = false;
			this.Apply();
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x000F43D7 File Offset: 0x000F25D7
		private void Update()
		{
			if (this.applyOnUpdate && this.viewCamera != null)
			{
				this.Apply();
			}
		}

		// Token: 0x040034BE RID: 13502
		[SerializeField]
		private bool applyOnUpdate;

		// Token: 0x040034BF RID: 13503
		[SerializeField]
		private SpriteScaler.FitMode fitMode;

		// Token: 0x040034C0 RID: 13504
		[SerializeField]
		public Vector3 offsetScale;

		// Token: 0x040034C1 RID: 13505
		[SerializeField]
		private bool _useDirectSizeSetting;

		// Token: 0x040034C2 RID: 13506
		[SerializeField]
		private Vector2 _directSizeSetting;

		// Token: 0x040034C3 RID: 13507
		[SerializeField]
		private bool _changePosition;

		// Token: 0x040034C4 RID: 13508
		[SerializeField]
		private bool _isUseFixedDepth;

		// Token: 0x040034C5 RID: 13509
		[SerializeField]
		private float _fixedDepth;

		// Token: 0x040034C6 RID: 13510
		private SpriteRenderer targetSprite;

		// Token: 0x040034C7 RID: 13511
		private SpriteMask targetMask;

		// Token: 0x040034C8 RID: 13512
		private bool isApplied;

		// Token: 0x040034C9 RID: 13513
		private bool applyOnCustomSize;

		// Token: 0x040034CA RID: 13514
		private bool m_ScaleYtoZ;

		// Token: 0x040034CB RID: 13515
		private Vector2 appliedScreenSize;

		// Token: 0x02000789 RID: 1929
		public enum FitMode
		{
			// Token: 0x040034CE RID: 13518
			None,
			// Token: 0x040034CF RID: 13519
			FitWidth,
			// Token: 0x040034D0 RID: 13520
			FitHeight,
			// Token: 0x040034D1 RID: 13521
			FitWidthMaintainAspectRatio,
			// Token: 0x040034D2 RID: 13522
			FitHeightMaintainAspectRatio,
			// Token: 0x040034D3 RID: 13523
			FitWidthHeight,
			// Token: 0x040034D4 RID: 13524
			FitHighestResolutionMaintainAspectRatio,
			// Token: 0x040034D5 RID: 13525
			FitLowestResolutionMaintainAspectRatio
		}
	}
}
