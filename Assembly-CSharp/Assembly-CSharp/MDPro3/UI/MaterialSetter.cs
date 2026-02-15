using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001403 RID: 5123
	public class MaterialSetter : MonoBehaviour
	{
		// Token: 0x06009455 RID: 37973 RVA: 0x001525AB File Offset: 0x001507AB
		private void Awake()
		{
			this.SetMaterialAsync();
		}

		// Token: 0x06009456 RID: 37974 RVA: 0x001525B4 File Offset: 0x001507B4
		private async UniTask SetMaterialAsync()
		{
			Material returnValue = await MaterialLoader.LoadMaterialByNameAsync(this.materialName);
			if (this.needInstantiate)
			{
				returnValue = global::UnityEngine.Object.Instantiate<Material>(returnValue);
			}
			Image image;
			RawImage rawImage;
			Renderer renderer;
			if (base.TryGetComponent<Image>(out image))
			{
				image.material = returnValue;
				if (this.moveImageSpriteToMaterial)
				{
					image.material.mainTexture = image.sprite.texture;
					image.sprite = null;
				}
			}
			else if (base.TryGetComponent<RawImage>(out rawImage))
			{
				rawImage.material = returnValue;
			}
			else if (base.TryGetComponent<Renderer>(out renderer))
			{
				renderer.material = returnValue;
			}
			this.setted = true;
		}

		// Token: 0x06009457 RID: 37975 RVA: 0x001525F7 File Offset: 0x001507F7
		public void SetMaterialAction(Action<Material> action)
		{
			this.SetMaterialActionAsync(action);
		}

		// Token: 0x06009458 RID: 37976 RVA: 0x00152604 File Offset: 0x00150804
		private async UniTask SetMaterialActionAsync(Action<Material> action)
		{
			await UniTask.WaitUntil(() => this.setted, PlayerLoopTiming.Update, default(CancellationToken), false);
			Image image;
			Material mat;
			RawImage rawImage;
			if (base.TryGetComponent<Image>(out image))
			{
				mat = image.material;
			}
			else if (base.TryGetComponent<RawImage>(out rawImage))
			{
				mat = rawImage.material;
			}
			else
			{
				mat = base.GetComponent<Renderer>().material;
			}
			action(mat);
		}

		// Token: 0x0400D295 RID: 53909
		public string materialName;

		// Token: 0x0400D296 RID: 53910
		public bool needInstantiate;

		// Token: 0x0400D297 RID: 53911
		public bool moveImageSpriteToMaterial;

		// Token: 0x0400D298 RID: 53912
		private bool setted;
	}
}
