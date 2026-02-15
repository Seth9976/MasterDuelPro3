using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200006D RID: 109
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineBlending.html")]
	[Serializable]
	public sealed class CinemachineBlenderSettings : ScriptableObject
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x000122B0 File Offset: 0x000104B0
		public CinemachineBlendDefinition GetBlendForVirtualCameras(string fromCameraName, string toCameraName, CinemachineBlendDefinition defaultBlend)
		{
			bool gotAnyToMe = false;
			bool gotMeToAny = false;
			CinemachineBlendDefinition anyToMe = defaultBlend;
			CinemachineBlendDefinition meToAny = defaultBlend;
			if (this.m_CustomBlends != null)
			{
				for (int i = 0; i < this.m_CustomBlends.Length; i++)
				{
					CinemachineBlenderSettings.CustomBlend blendParams = this.m_CustomBlends[i];
					if (blendParams.m_From == fromCameraName && blendParams.m_To == toCameraName)
					{
						return blendParams.m_Blend;
					}
					if (blendParams.m_From == "**ANY CAMERA**")
					{
						if (!string.IsNullOrEmpty(toCameraName) && blendParams.m_To == toCameraName)
						{
							if (!gotAnyToMe)
							{
								anyToMe = blendParams.m_Blend;
							}
							gotAnyToMe = true;
						}
						else if (blendParams.m_To == "**ANY CAMERA**")
						{
							defaultBlend = blendParams.m_Blend;
						}
					}
					else if (blendParams.m_To == "**ANY CAMERA**" && !string.IsNullOrEmpty(fromCameraName) && blendParams.m_From == fromCameraName)
					{
						if (!gotMeToAny)
						{
							meToAny = blendParams.m_Blend;
						}
						gotMeToAny = true;
					}
				}
			}
			if (gotAnyToMe)
			{
				return anyToMe;
			}
			if (gotMeToAny)
			{
				return meToAny;
			}
			return defaultBlend;
		}

		// Token: 0x04000291 RID: 657
		[Tooltip("The array containing explicitly defined blends between two Virtual Cameras")]
		public CinemachineBlenderSettings.CustomBlend[] m_CustomBlends;

		// Token: 0x04000292 RID: 658
		public const string kBlendFromAnyCameraLabel = "**ANY CAMERA**";

		// Token: 0x0200006E RID: 110
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		[Serializable]
		public struct CustomBlend
		{
			// Token: 0x04000293 RID: 659
			[Tooltip("When blending from this camera")]
			public string m_From;

			// Token: 0x04000294 RID: 660
			[Tooltip("When blending to this camera")]
			public string m_To;

			// Token: 0x04000295 RID: 661
			[CinemachineBlendDefinitionProperty]
			[Tooltip("Blend curve definition")]
			public CinemachineBlendDefinition m_Blend;
		}
	}
}
