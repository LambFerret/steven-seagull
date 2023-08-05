using UnityEngine;

namespace Faktori.CameraTools {
	[ExecuteInEditMode]
	public class CameraShaderPass : MonoBehaviour
	{
		public Material material;
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, material);
		}
	}
}
