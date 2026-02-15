using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MDPro3
{
	// Token: 0x02001241 RID: 4673
	[RequireComponent(typeof(Renderer))]
	public class ShaderReplacer : MonoBehaviour
	{
		// Token: 0x06008A0C RID: 35340 RVA: 0x0010F3C2 File Offset: 0x0010D5C2
		private void Awake()
		{
			this.ReplaceShader();
		}

		// Token: 0x06008A0D RID: 35341 RVA: 0x0010F3CA File Offset: 0x0010D5CA
		private void ReplaceShader()
		{
			if (this._renderer == null)
			{
				this._renderer = base.GetComponent<Renderer>();
			}
			if (this._renderer == null || string.IsNullOrEmpty(this.shaderName))
			{
				return;
			}
			this.ReplaceShaderAsync();
		}

		// Token: 0x06008A0E RID: 35342 RVA: 0x0010F40C File Offset: 0x0010D60C
		private async UniTask ReplaceShaderAsync()
		{
			this._renderer.enabled = false;
			Material material = this._renderer.material;
			Shader shader = await MaterialLoader.LoadShaderByNameAsync(this.shaderName);
			material.shader = shader;
			material = null;
			this._renderer.enabled = true;
		}

		// Token: 0x0400C545 RID: 50501
		public string shaderName;

		// Token: 0x0400C546 RID: 50502
		private Renderer _renderer;

		// Token: 0x0400C547 RID: 50503
		private Coroutine coroutine;
	}
}
