using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	internal class PixelPerfectCameraInternal : ISerializationCallbackReceiver
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00010800 File Offset: 0x0000EA00
		internal PixelPerfectCameraInternal(IPixelPerfectCamera component)
		{
			this.m_Component = component;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00010833 File Offset: 0x0000EA33
		public void OnBeforeSerialize()
		{
			this.m_SerializableComponent = this.m_Component as PixelPerfectCamera;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00010846 File Offset: 0x0000EA46
		public void OnAfterDeserialize()
		{
			if (this.m_SerializableComponent != null)
			{
				this.m_Component = this.m_SerializableComponent;
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00010864 File Offset: 0x0000EA64
		internal void CalculateCameraProperties(int screenWidth, int screenHeight)
		{
			int assetsPPU = this.m_Component.assetsPPU;
			int refResolutionX = this.m_Component.refResolutionX;
			int refResolutionY = this.m_Component.refResolutionY;
			bool upscaleRT = this.m_Component.upscaleRT;
			bool pixelSnapping = this.m_Component.pixelSnapping;
			bool cropFrameX = this.m_Component.cropFrameX;
			bool cropFrameY = this.m_Component.cropFrameY;
			bool stretchFill = this.m_Component.stretchFill;
			this.cropFrameXAndY = cropFrameY && cropFrameX;
			this.cropFrameXOrY = cropFrameY || cropFrameX;
			this.useStretchFill = this.cropFrameXAndY && stretchFill;
			this.requiresUpscaling = this.useStretchFill;
			int verticalZoom = screenHeight / refResolutionY;
			int horizontalZoom = screenWidth / refResolutionX;
			this.zoom = Math.Max(1, Math.Min(verticalZoom, horizontalZoom));
			this.useOffscreenRT = false;
			this.offscreenRTWidth = 0;
			this.offscreenRTHeight = 0;
			if (this.cropFrameXOrY)
			{
				this.useOffscreenRT = true;
				if (!upscaleRT)
				{
					if (this.cropFrameXAndY)
					{
						this.offscreenRTWidth = this.zoom * refResolutionX;
						this.offscreenRTHeight = this.zoom * refResolutionY;
					}
					else if (cropFrameY)
					{
						this.offscreenRTWidth = screenWidth;
						this.offscreenRTHeight = this.zoom * refResolutionY;
					}
					else
					{
						this.offscreenRTWidth = this.zoom * refResolutionX;
						this.offscreenRTHeight = screenHeight;
					}
				}
				else if (this.cropFrameXAndY)
				{
					this.offscreenRTWidth = refResolutionX;
					this.offscreenRTHeight = refResolutionY;
				}
				else if (cropFrameY)
				{
					this.offscreenRTWidth = screenWidth / this.zoom / 2 * 2;
					this.offscreenRTHeight = refResolutionY;
				}
				else
				{
					this.offscreenRTWidth = refResolutionX;
					this.offscreenRTHeight = screenHeight / this.zoom / 2 * 2;
				}
			}
			else if (upscaleRT && this.zoom > 1)
			{
				this.useOffscreenRT = true;
				this.offscreenRTWidth = screenWidth / this.zoom / 2 * 2;
				this.offscreenRTHeight = screenHeight / this.zoom / 2 * 2;
			}
			if (this.useOffscreenRT)
			{
				this.pixelRect = new Rect(0f, 0f, (float)this.offscreenRTWidth, (float)this.offscreenRTHeight);
			}
			else
			{
				this.pixelRect = Rect.zero;
			}
			if (cropFrameY)
			{
				this.orthoSize = (float)refResolutionY * 0.5f / (float)assetsPPU;
			}
			else if (cropFrameX)
			{
				float aspect = ((this.pixelRect == Rect.zero) ? ((float)screenWidth / (float)screenHeight) : (this.pixelRect.width / this.pixelRect.height));
				this.orthoSize = (float)refResolutionX / aspect * 0.5f / (float)assetsPPU;
			}
			else if (upscaleRT && this.zoom > 1)
			{
				this.orthoSize = (float)this.offscreenRTHeight * 0.5f / (float)assetsPPU;
			}
			else
			{
				float pixelHeight = ((this.pixelRect == Rect.zero) ? ((float)screenHeight) : this.pixelRect.height);
				this.orthoSize = pixelHeight * 0.5f / (float)(this.zoom * assetsPPU);
			}
			if (upscaleRT || (!upscaleRT && pixelSnapping))
			{
				this.unitsPerPixel = 1f / (float)assetsPPU;
				return;
			}
			this.unitsPerPixel = 1f / (float)(this.zoom * assetsPPU);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00010B68 File Offset: 0x0000ED68
		internal Rect CalculateFinalBlitPixelRect(int screenWidth, int screenHeight)
		{
			Rect pixelRect = default(Rect);
			if (this.useStretchFill)
			{
				float screenAspect = (float)screenWidth / (float)screenHeight;
				float cameraAspect = (float)this.m_Component.refResolutionX / (float)this.m_Component.refResolutionY;
				if (screenAspect > cameraAspect)
				{
					pixelRect.height = (float)screenHeight;
					pixelRect.width = (float)screenHeight * cameraAspect;
					pixelRect.x = (float)((screenWidth - (int)pixelRect.width) / 2);
					pixelRect.y = 0f;
				}
				else
				{
					pixelRect.width = (float)screenWidth;
					pixelRect.height = (float)screenWidth / cameraAspect;
					pixelRect.y = (float)((screenHeight - (int)pixelRect.height) / 2);
					pixelRect.x = 0f;
				}
				if (screenWidth % this.m_Component.refResolutionX == 0)
				{
					this.requiresUpscaling = cameraAspect < screenAspect;
				}
				else if (screenHeight % this.m_Component.refResolutionY == 0)
				{
					this.requiresUpscaling = cameraAspect > screenAspect;
				}
			}
			else
			{
				if (this.m_Component.upscaleRT)
				{
					pixelRect.height = (float)(this.zoom * this.offscreenRTHeight);
					pixelRect.width = (float)(this.zoom * this.offscreenRTWidth);
				}
				else
				{
					pixelRect.height = (float)this.offscreenRTHeight;
					pixelRect.width = (float)this.offscreenRTWidth;
				}
				pixelRect.x = (float)((screenWidth - (int)pixelRect.width) / 2);
				pixelRect.y = (float)((screenHeight - (int)pixelRect.height) / 2);
			}
			return pixelRect;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00010CD0 File Offset: 0x0000EED0
		internal float CorrectCinemachineOrthoSize(float targetOrthoSize)
		{
			float correctedOrthoSize;
			if (this.m_Component.upscaleRT)
			{
				this.cinemachineVCamZoom = Math.Max(1, Mathf.RoundToInt(this.orthoSize / targetOrthoSize));
				correctedOrthoSize = this.orthoSize / (float)this.cinemachineVCamZoom;
			}
			else
			{
				this.cinemachineVCamZoom = Math.Max(1, Mathf.RoundToInt((float)this.zoom * this.orthoSize / targetOrthoSize));
				correctedOrthoSize = (float)this.zoom * this.orthoSize / (float)this.cinemachineVCamZoom;
			}
			if (!this.m_Component.upscaleRT && !this.m_Component.pixelSnapping)
			{
				this.unitsPerPixel = 1f / (float)(this.cinemachineVCamZoom * this.m_Component.assetsPPU);
			}
			return correctedOrthoSize;
		}

		// Token: 0x040001A1 RID: 417
		[NonSerialized]
		private IPixelPerfectCamera m_Component;

		// Token: 0x040001A2 RID: 418
		private PixelPerfectCamera m_SerializableComponent;

		// Token: 0x040001A3 RID: 419
		internal float originalOrthoSize;

		// Token: 0x040001A4 RID: 420
		internal bool hasPostProcessLayer;

		// Token: 0x040001A5 RID: 421
		internal bool cropFrameXAndY;

		// Token: 0x040001A6 RID: 422
		internal bool cropFrameXOrY;

		// Token: 0x040001A7 RID: 423
		internal bool useStretchFill;

		// Token: 0x040001A8 RID: 424
		internal int zoom = 1;

		// Token: 0x040001A9 RID: 425
		internal bool useOffscreenRT;

		// Token: 0x040001AA RID: 426
		internal int offscreenRTWidth;

		// Token: 0x040001AB RID: 427
		internal int offscreenRTHeight;

		// Token: 0x040001AC RID: 428
		internal Rect pixelRect = Rect.zero;

		// Token: 0x040001AD RID: 429
		internal float orthoSize = 1f;

		// Token: 0x040001AE RID: 430
		internal float unitsPerPixel;

		// Token: 0x040001AF RID: 431
		internal int cinemachineVCamZoom = 1;

		// Token: 0x040001B0 RID: 432
		internal bool requiresUpscaling;
	}
}
