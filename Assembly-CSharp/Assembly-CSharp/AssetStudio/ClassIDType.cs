using System;

namespace AssetStudio
{
	// Token: 0x02000098 RID: 152
	public enum ClassIDType
	{
		// Token: 0x040003CC RID: 972
		UnknownType = -1,
		// Token: 0x040003CD RID: 973
		Object,
		// Token: 0x040003CE RID: 974
		GameObject,
		// Token: 0x040003CF RID: 975
		Component,
		// Token: 0x040003D0 RID: 976
		LevelGameManager,
		// Token: 0x040003D1 RID: 977
		Transform,
		// Token: 0x040003D2 RID: 978
		TimeManager,
		// Token: 0x040003D3 RID: 979
		GlobalGameManager,
		// Token: 0x040003D4 RID: 980
		Behaviour = 8,
		// Token: 0x040003D5 RID: 981
		GameManager,
		// Token: 0x040003D6 RID: 982
		AudioManager = 11,
		// Token: 0x040003D7 RID: 983
		ParticleAnimator,
		// Token: 0x040003D8 RID: 984
		InputManager,
		// Token: 0x040003D9 RID: 985
		EllipsoidParticleEmitter = 15,
		// Token: 0x040003DA RID: 986
		Pipeline = 17,
		// Token: 0x040003DB RID: 987
		EditorExtension,
		// Token: 0x040003DC RID: 988
		Physics2DSettings,
		// Token: 0x040003DD RID: 989
		Camera,
		// Token: 0x040003DE RID: 990
		Material,
		// Token: 0x040003DF RID: 991
		MeshRenderer = 23,
		// Token: 0x040003E0 RID: 992
		Renderer = 25,
		// Token: 0x040003E1 RID: 993
		ParticleRenderer,
		// Token: 0x040003E2 RID: 994
		Texture,
		// Token: 0x040003E3 RID: 995
		Texture2D,
		// Token: 0x040003E4 RID: 996
		OcclusionCullingSettings,
		// Token: 0x040003E5 RID: 997
		GraphicsSettings,
		// Token: 0x040003E6 RID: 998
		MeshFilter = 33,
		// Token: 0x040003E7 RID: 999
		OcclusionPortal = 41,
		// Token: 0x040003E8 RID: 1000
		Mesh = 43,
		// Token: 0x040003E9 RID: 1001
		Skybox = 45,
		// Token: 0x040003EA RID: 1002
		QualitySettings = 47,
		// Token: 0x040003EB RID: 1003
		Shader,
		// Token: 0x040003EC RID: 1004
		TextAsset,
		// Token: 0x040003ED RID: 1005
		Rigidbody2D,
		// Token: 0x040003EE RID: 1006
		Physics2DManager,
		// Token: 0x040003EF RID: 1007
		Collider2D = 53,
		// Token: 0x040003F0 RID: 1008
		Rigidbody,
		// Token: 0x040003F1 RID: 1009
		PhysicsManager,
		// Token: 0x040003F2 RID: 1010
		Collider,
		// Token: 0x040003F3 RID: 1011
		Joint,
		// Token: 0x040003F4 RID: 1012
		CircleCollider2D,
		// Token: 0x040003F5 RID: 1013
		HingeJoint,
		// Token: 0x040003F6 RID: 1014
		PolygonCollider2D,
		// Token: 0x040003F7 RID: 1015
		BoxCollider2D,
		// Token: 0x040003F8 RID: 1016
		PhysicsMaterial2D,
		// Token: 0x040003F9 RID: 1017
		MeshCollider = 64,
		// Token: 0x040003FA RID: 1018
		BoxCollider,
		// Token: 0x040003FB RID: 1019
		CompositeCollider2D,
		// Token: 0x040003FC RID: 1020
		EdgeCollider2D = 68,
		// Token: 0x040003FD RID: 1021
		CapsuleCollider2D = 70,
		// Token: 0x040003FE RID: 1022
		ComputeShader = 72,
		// Token: 0x040003FF RID: 1023
		AnimationClip = 74,
		// Token: 0x04000400 RID: 1024
		ConstantForce,
		// Token: 0x04000401 RID: 1025
		WorldParticleCollider,
		// Token: 0x04000402 RID: 1026
		TagManager = 78,
		// Token: 0x04000403 RID: 1027
		AudioListener = 81,
		// Token: 0x04000404 RID: 1028
		AudioSource,
		// Token: 0x04000405 RID: 1029
		AudioClip,
		// Token: 0x04000406 RID: 1030
		RenderTexture,
		// Token: 0x04000407 RID: 1031
		CustomRenderTexture = 86,
		// Token: 0x04000408 RID: 1032
		MeshParticleEmitter,
		// Token: 0x04000409 RID: 1033
		ParticleEmitter,
		// Token: 0x0400040A RID: 1034
		Cubemap,
		// Token: 0x0400040B RID: 1035
		Avatar,
		// Token: 0x0400040C RID: 1036
		AnimatorController,
		// Token: 0x0400040D RID: 1037
		GUILayer,
		// Token: 0x0400040E RID: 1038
		RuntimeAnimatorController,
		// Token: 0x0400040F RID: 1039
		ScriptMapper,
		// Token: 0x04000410 RID: 1040
		Animator,
		// Token: 0x04000411 RID: 1041
		TrailRenderer,
		// Token: 0x04000412 RID: 1042
		DelayedCallManager = 98,
		// Token: 0x04000413 RID: 1043
		TextMesh = 102,
		// Token: 0x04000414 RID: 1044
		RenderSettings = 104,
		// Token: 0x04000415 RID: 1045
		Light = 108,
		// Token: 0x04000416 RID: 1046
		CGProgram,
		// Token: 0x04000417 RID: 1047
		BaseAnimationTrack,
		// Token: 0x04000418 RID: 1048
		Animation,
		// Token: 0x04000419 RID: 1049
		MonoBehaviour = 114,
		// Token: 0x0400041A RID: 1050
		MonoScript,
		// Token: 0x0400041B RID: 1051
		MonoManager,
		// Token: 0x0400041C RID: 1052
		Texture3D,
		// Token: 0x0400041D RID: 1053
		NewAnimationTrack,
		// Token: 0x0400041E RID: 1054
		Projector,
		// Token: 0x0400041F RID: 1055
		LineRenderer,
		// Token: 0x04000420 RID: 1056
		Flare,
		// Token: 0x04000421 RID: 1057
		Halo,
		// Token: 0x04000422 RID: 1058
		LensFlare,
		// Token: 0x04000423 RID: 1059
		FlareLayer,
		// Token: 0x04000424 RID: 1060
		HaloLayer,
		// Token: 0x04000425 RID: 1061
		NavMeshAreas,
		// Token: 0x04000426 RID: 1062
		NavMeshProjectSettings = 126,
		// Token: 0x04000427 RID: 1063
		HaloManager,
		// Token: 0x04000428 RID: 1064
		Font,
		// Token: 0x04000429 RID: 1065
		PlayerSettings,
		// Token: 0x0400042A RID: 1066
		NamedObject,
		// Token: 0x0400042B RID: 1067
		GUITexture,
		// Token: 0x0400042C RID: 1068
		GUIText,
		// Token: 0x0400042D RID: 1069
		GUIElement,
		// Token: 0x0400042E RID: 1070
		PhysicMaterial,
		// Token: 0x0400042F RID: 1071
		SphereCollider,
		// Token: 0x04000430 RID: 1072
		CapsuleCollider,
		// Token: 0x04000431 RID: 1073
		SkinnedMeshRenderer,
		// Token: 0x04000432 RID: 1074
		FixedJoint,
		// Token: 0x04000433 RID: 1075
		RaycastCollider = 140,
		// Token: 0x04000434 RID: 1076
		BuildSettings,
		// Token: 0x04000435 RID: 1077
		AssetBundle,
		// Token: 0x04000436 RID: 1078
		CharacterController,
		// Token: 0x04000437 RID: 1079
		CharacterJoint,
		// Token: 0x04000438 RID: 1080
		SpringJoint,
		// Token: 0x04000439 RID: 1081
		WheelCollider,
		// Token: 0x0400043A RID: 1082
		ResourceManager,
		// Token: 0x0400043B RID: 1083
		NetworkView,
		// Token: 0x0400043C RID: 1084
		NetworkManager,
		// Token: 0x0400043D RID: 1085
		PreloadData,
		// Token: 0x0400043E RID: 1086
		MovieTexture = 152,
		// Token: 0x0400043F RID: 1087
		ConfigurableJoint,
		// Token: 0x04000440 RID: 1088
		TerrainCollider,
		// Token: 0x04000441 RID: 1089
		MasterServerInterface,
		// Token: 0x04000442 RID: 1090
		TerrainData,
		// Token: 0x04000443 RID: 1091
		LightmapSettings,
		// Token: 0x04000444 RID: 1092
		WebCamTexture,
		// Token: 0x04000445 RID: 1093
		EditorSettings,
		// Token: 0x04000446 RID: 1094
		InteractiveCloth,
		// Token: 0x04000447 RID: 1095
		ClothRenderer,
		// Token: 0x04000448 RID: 1096
		EditorUserSettings,
		// Token: 0x04000449 RID: 1097
		SkinnedCloth,
		// Token: 0x0400044A RID: 1098
		AudioReverbFilter,
		// Token: 0x0400044B RID: 1099
		AudioHighPassFilter,
		// Token: 0x0400044C RID: 1100
		AudioChorusFilter,
		// Token: 0x0400044D RID: 1101
		AudioReverbZone,
		// Token: 0x0400044E RID: 1102
		AudioEchoFilter,
		// Token: 0x0400044F RID: 1103
		AudioLowPassFilter,
		// Token: 0x04000450 RID: 1104
		AudioDistortionFilter,
		// Token: 0x04000451 RID: 1105
		SparseTexture,
		// Token: 0x04000452 RID: 1106
		AudioBehaviour = 180,
		// Token: 0x04000453 RID: 1107
		AudioFilter,
		// Token: 0x04000454 RID: 1108
		WindZone,
		// Token: 0x04000455 RID: 1109
		Cloth,
		// Token: 0x04000456 RID: 1110
		SubstanceArchive,
		// Token: 0x04000457 RID: 1111
		ProceduralMaterial,
		// Token: 0x04000458 RID: 1112
		ProceduralTexture,
		// Token: 0x04000459 RID: 1113
		Texture2DArray,
		// Token: 0x0400045A RID: 1114
		CubemapArray,
		// Token: 0x0400045B RID: 1115
		OffMeshLink = 191,
		// Token: 0x0400045C RID: 1116
		OcclusionArea,
		// Token: 0x0400045D RID: 1117
		Tree,
		// Token: 0x0400045E RID: 1118
		NavMeshObsolete,
		// Token: 0x0400045F RID: 1119
		NavMeshAgent,
		// Token: 0x04000460 RID: 1120
		NavMeshSettings,
		// Token: 0x04000461 RID: 1121
		LightProbesLegacy,
		// Token: 0x04000462 RID: 1122
		ParticleSystem,
		// Token: 0x04000463 RID: 1123
		ParticleSystemRenderer,
		// Token: 0x04000464 RID: 1124
		ShaderVariantCollection,
		// Token: 0x04000465 RID: 1125
		LODGroup = 205,
		// Token: 0x04000466 RID: 1126
		BlendTree,
		// Token: 0x04000467 RID: 1127
		Motion,
		// Token: 0x04000468 RID: 1128
		NavMeshObstacle,
		// Token: 0x04000469 RID: 1129
		SortingGroup = 210,
		// Token: 0x0400046A RID: 1130
		SpriteRenderer = 212,
		// Token: 0x0400046B RID: 1131
		Sprite,
		// Token: 0x0400046C RID: 1132
		CachedSpriteAtlas,
		// Token: 0x0400046D RID: 1133
		ReflectionProbe,
		// Token: 0x0400046E RID: 1134
		ReflectionProbes,
		// Token: 0x0400046F RID: 1135
		Terrain = 218,
		// Token: 0x04000470 RID: 1136
		LightProbeGroup = 220,
		// Token: 0x04000471 RID: 1137
		AnimatorOverrideController,
		// Token: 0x04000472 RID: 1138
		CanvasRenderer,
		// Token: 0x04000473 RID: 1139
		Canvas,
		// Token: 0x04000474 RID: 1140
		RectTransform,
		// Token: 0x04000475 RID: 1141
		CanvasGroup,
		// Token: 0x04000476 RID: 1142
		BillboardAsset,
		// Token: 0x04000477 RID: 1143
		BillboardRenderer,
		// Token: 0x04000478 RID: 1144
		SpeedTreeWindAsset,
		// Token: 0x04000479 RID: 1145
		AnchoredJoint2D,
		// Token: 0x0400047A RID: 1146
		Joint2D,
		// Token: 0x0400047B RID: 1147
		SpringJoint2D,
		// Token: 0x0400047C RID: 1148
		DistanceJoint2D,
		// Token: 0x0400047D RID: 1149
		HingeJoint2D,
		// Token: 0x0400047E RID: 1150
		SliderJoint2D,
		// Token: 0x0400047F RID: 1151
		WheelJoint2D,
		// Token: 0x04000480 RID: 1152
		ClusterInputManager,
		// Token: 0x04000481 RID: 1153
		BaseVideoTexture,
		// Token: 0x04000482 RID: 1154
		NavMeshData,
		// Token: 0x04000483 RID: 1155
		AudioMixer = 240,
		// Token: 0x04000484 RID: 1156
		AudioMixerController,
		// Token: 0x04000485 RID: 1157
		AudioMixerGroupController = 243,
		// Token: 0x04000486 RID: 1158
		AudioMixerEffectController,
		// Token: 0x04000487 RID: 1159
		AudioMixerSnapshotController,
		// Token: 0x04000488 RID: 1160
		PhysicsUpdateBehaviour2D,
		// Token: 0x04000489 RID: 1161
		ConstantForce2D,
		// Token: 0x0400048A RID: 1162
		Effector2D,
		// Token: 0x0400048B RID: 1163
		AreaEffector2D,
		// Token: 0x0400048C RID: 1164
		PointEffector2D,
		// Token: 0x0400048D RID: 1165
		PlatformEffector2D,
		// Token: 0x0400048E RID: 1166
		SurfaceEffector2D,
		// Token: 0x0400048F RID: 1167
		BuoyancyEffector2D,
		// Token: 0x04000490 RID: 1168
		RelativeJoint2D,
		// Token: 0x04000491 RID: 1169
		FixedJoint2D,
		// Token: 0x04000492 RID: 1170
		FrictionJoint2D,
		// Token: 0x04000493 RID: 1171
		TargetJoint2D,
		// Token: 0x04000494 RID: 1172
		LightProbes,
		// Token: 0x04000495 RID: 1173
		LightProbeProxyVolume,
		// Token: 0x04000496 RID: 1174
		SampleClip = 271,
		// Token: 0x04000497 RID: 1175
		AudioMixerSnapshot,
		// Token: 0x04000498 RID: 1176
		AudioMixerGroup,
		// Token: 0x04000499 RID: 1177
		NScreenBridge = 280,
		// Token: 0x0400049A RID: 1178
		AssetBundleManifest = 290,
		// Token: 0x0400049B RID: 1179
		UnityAdsManager = 292,
		// Token: 0x0400049C RID: 1180
		RuntimeInitializeOnLoadManager = 300,
		// Token: 0x0400049D RID: 1181
		CloudWebServicesManager,
		// Token: 0x0400049E RID: 1182
		UnityAnalyticsManager = 303,
		// Token: 0x0400049F RID: 1183
		CrashReportManager,
		// Token: 0x040004A0 RID: 1184
		PerformanceReportingManager,
		// Token: 0x040004A1 RID: 1185
		UnityConnectSettings = 310,
		// Token: 0x040004A2 RID: 1186
		AvatarMask = 319,
		// Token: 0x040004A3 RID: 1187
		PlayableDirector,
		// Token: 0x040004A4 RID: 1188
		VideoPlayer = 328,
		// Token: 0x040004A5 RID: 1189
		VideoClip,
		// Token: 0x040004A6 RID: 1190
		ParticleSystemForceField,
		// Token: 0x040004A7 RID: 1191
		SpriteMask,
		// Token: 0x040004A8 RID: 1192
		WorldAnchor = 362,
		// Token: 0x040004A9 RID: 1193
		OcclusionCullingData,
		// Token: 0x040004AA RID: 1194
		SmallestEditorClassID = 1000,
		// Token: 0x040004AB RID: 1195
		PrefabInstance,
		// Token: 0x040004AC RID: 1196
		EditorExtensionImpl,
		// Token: 0x040004AD RID: 1197
		AssetImporter,
		// Token: 0x040004AE RID: 1198
		AssetDatabaseV1,
		// Token: 0x040004AF RID: 1199
		Mesh3DSImporter,
		// Token: 0x040004B0 RID: 1200
		TextureImporter,
		// Token: 0x040004B1 RID: 1201
		ShaderImporter,
		// Token: 0x040004B2 RID: 1202
		ComputeShaderImporter,
		// Token: 0x040004B3 RID: 1203
		AudioImporter = 1020,
		// Token: 0x040004B4 RID: 1204
		HierarchyState = 1026,
		// Token: 0x040004B5 RID: 1205
		GUIDSerializer,
		// Token: 0x040004B6 RID: 1206
		AssetMetaData,
		// Token: 0x040004B7 RID: 1207
		DefaultAsset,
		// Token: 0x040004B8 RID: 1208
		DefaultImporter,
		// Token: 0x040004B9 RID: 1209
		TextScriptImporter,
		// Token: 0x040004BA RID: 1210
		SceneAsset,
		// Token: 0x040004BB RID: 1211
		NativeFormatImporter = 1034,
		// Token: 0x040004BC RID: 1212
		MonoImporter,
		// Token: 0x040004BD RID: 1213
		AssetServerCache = 1037,
		// Token: 0x040004BE RID: 1214
		LibraryAssetImporter,
		// Token: 0x040004BF RID: 1215
		ModelImporter = 1040,
		// Token: 0x040004C0 RID: 1216
		FBXImporter,
		// Token: 0x040004C1 RID: 1217
		TrueTypeFontImporter,
		// Token: 0x040004C2 RID: 1218
		MovieImporter = 1044,
		// Token: 0x040004C3 RID: 1219
		EditorBuildSettings,
		// Token: 0x040004C4 RID: 1220
		DDSImporter,
		// Token: 0x040004C5 RID: 1221
		InspectorExpandedState = 1048,
		// Token: 0x040004C6 RID: 1222
		AnnotationManager,
		// Token: 0x040004C7 RID: 1223
		PluginImporter,
		// Token: 0x040004C8 RID: 1224
		EditorUserBuildSettings,
		// Token: 0x040004C9 RID: 1225
		PVRImporter,
		// Token: 0x040004CA RID: 1226
		ASTCImporter,
		// Token: 0x040004CB RID: 1227
		KTXImporter,
		// Token: 0x040004CC RID: 1228
		IHVImageFormatImporter,
		// Token: 0x040004CD RID: 1229
		AnimatorStateTransition = 1101,
		// Token: 0x040004CE RID: 1230
		AnimatorState,
		// Token: 0x040004CF RID: 1231
		HumanTemplate = 1105,
		// Token: 0x040004D0 RID: 1232
		AnimatorStateMachine = 1107,
		// Token: 0x040004D1 RID: 1233
		PreviewAnimationClip,
		// Token: 0x040004D2 RID: 1234
		AnimatorTransition,
		// Token: 0x040004D3 RID: 1235
		SpeedTreeImporter,
		// Token: 0x040004D4 RID: 1236
		AnimatorTransitionBase,
		// Token: 0x040004D5 RID: 1237
		SubstanceImporter,
		// Token: 0x040004D6 RID: 1238
		LightmapParameters,
		// Token: 0x040004D7 RID: 1239
		LightingDataAsset = 1120,
		// Token: 0x040004D8 RID: 1240
		GISRaster,
		// Token: 0x040004D9 RID: 1241
		GISRasterImporter,
		// Token: 0x040004DA RID: 1242
		CadImporter,
		// Token: 0x040004DB RID: 1243
		SketchUpImporter,
		// Token: 0x040004DC RID: 1244
		BuildReport,
		// Token: 0x040004DD RID: 1245
		PackedAssets,
		// Token: 0x040004DE RID: 1246
		VideoClipImporter,
		// Token: 0x040004DF RID: 1247
		ActivationLogComponent = 2000,
		// Token: 0x040004E0 RID: 1248
		MonoObject = 100003,
		// Token: 0x040004E1 RID: 1249
		Collision,
		// Token: 0x040004E2 RID: 1250
		Vector3f,
		// Token: 0x040004E3 RID: 1251
		RootMotionData,
		// Token: 0x040004E4 RID: 1252
		Collision2D,
		// Token: 0x040004E5 RID: 1253
		AudioMixerLiveUpdateFloat,
		// Token: 0x040004E6 RID: 1254
		AudioMixerLiveUpdateBool,
		// Token: 0x040004E7 RID: 1255
		Polygon2D,
		// Token: 0x040004E8 RID: 1256
		TilemapCollider2D = 19719996,
		// Token: 0x040004E9 RID: 1257
		AssetImporterLog = 41386430,
		// Token: 0x040004EA RID: 1258
		VFXRenderer = 73398921,
		// Token: 0x040004EB RID: 1259
		SerializableManagedRefTestClass = 76251197,
		// Token: 0x040004EC RID: 1260
		Grid = 156049354,
		// Token: 0x040004ED RID: 1261
		ScenesUsingAssets = 156483287,
		// Token: 0x040004EE RID: 1262
		ArticulationBody = 171741748,
		// Token: 0x040004EF RID: 1263
		Preset = 181963792,
		// Token: 0x040004F0 RID: 1264
		EmptyObject = 277625683,
		// Token: 0x040004F1 RID: 1265
		IConstraint = 285090594,
		// Token: 0x040004F2 RID: 1266
		TestObjectWithSpecialLayoutOne = 293259124,
		// Token: 0x040004F3 RID: 1267
		AssemblyDefinitionReferenceImporter = 294290339,
		// Token: 0x040004F4 RID: 1268
		SiblingDerived = 334799969,
		// Token: 0x040004F5 RID: 1269
		TestObjectWithSerializedMapStringNonAlignedStruct = 342846651,
		// Token: 0x040004F6 RID: 1270
		SubDerived = 367388927,
		// Token: 0x040004F7 RID: 1271
		AssetImportInProgressProxy = 369655926,
		// Token: 0x040004F8 RID: 1272
		PluginBuildInfo = 382020655,
		// Token: 0x040004F9 RID: 1273
		EditorProjectAccess = 426301858,
		// Token: 0x040004FA RID: 1274
		PrefabImporter = 468431735,
		// Token: 0x040004FB RID: 1275
		TestObjectWithSerializedArray = 478637458,
		// Token: 0x040004FC RID: 1276
		TestObjectWithSerializedAnimationCurve,
		// Token: 0x040004FD RID: 1277
		TilemapRenderer = 483693784,
		// Token: 0x040004FE RID: 1278
		ScriptableCamera = 488575907,
		// Token: 0x040004FF RID: 1279
		SpriteAtlasAsset = 612988286,
		// Token: 0x04000500 RID: 1280
		SpriteAtlasDatabase = 638013454,
		// Token: 0x04000501 RID: 1281
		AudioBuildInfo = 641289076,
		// Token: 0x04000502 RID: 1282
		CachedSpriteAtlasRuntimeData = 644342135,
		// Token: 0x04000503 RID: 1283
		RendererFake = 646504946,
		// Token: 0x04000504 RID: 1284
		AssemblyDefinitionReferenceAsset = 662584278,
		// Token: 0x04000505 RID: 1285
		BuiltAssetBundleInfoSet = 668709126,
		// Token: 0x04000506 RID: 1286
		SpriteAtlas = 687078895,
		// Token: 0x04000507 RID: 1287
		RayTracingShaderImporter = 747330370,
		// Token: 0x04000508 RID: 1288
		RayTracingShader = 825902497,
		// Token: 0x04000509 RID: 1289
		LightingSettings = 850595691,
		// Token: 0x0400050A RID: 1290
		PlatformModuleSetup = 877146078,
		// Token: 0x0400050B RID: 1291
		VersionControlSettings = 890905787,
		// Token: 0x0400050C RID: 1292
		AimConstraint = 895512359,
		// Token: 0x0400050D RID: 1293
		VFXManager = 937362698,
		// Token: 0x0400050E RID: 1294
		VisualEffectSubgraph = 994735392,
		// Token: 0x0400050F RID: 1295
		VisualEffectSubgraphOperator = 994735403,
		// Token: 0x04000510 RID: 1296
		VisualEffectSubgraphBlock,
		// Token: 0x04000511 RID: 1297
		LocalizationImporter = 1027052791,
		// Token: 0x04000512 RID: 1298
		Derived = 1091556383,
		// Token: 0x04000513 RID: 1299
		PropertyModificationsTargetTestObject = 1111377672,
		// Token: 0x04000514 RID: 1300
		ReferencesArtifactGenerator = 1114811875,
		// Token: 0x04000515 RID: 1301
		AssemblyDefinitionAsset = 1152215463,
		// Token: 0x04000516 RID: 1302
		SceneVisibilityState = 1154873562,
		// Token: 0x04000517 RID: 1303
		LookAtConstraint = 1183024399,
		// Token: 0x04000518 RID: 1304
		SpriteAtlasImporter = 1210832254,
		// Token: 0x04000519 RID: 1305
		MultiArtifactTestImporter = 1223240404,
		// Token: 0x0400051A RID: 1306
		GameObjectRecorder = 1268269756,
		// Token: 0x0400051B RID: 1307
		LightingDataAssetParent = 1325145578,
		// Token: 0x0400051C RID: 1308
		PresetManager = 1386491679,
		// Token: 0x0400051D RID: 1309
		TestObjectWithSpecialLayoutTwo = 1392443030,
		// Token: 0x0400051E RID: 1310
		StreamingManager = 1403656975,
		// Token: 0x0400051F RID: 1311
		LowerResBlitTexture = 1480428607,
		// Token: 0x04000520 RID: 1312
		StreamingController = 1542919678,
		// Token: 0x04000521 RID: 1313
		RenderPassAttachment = 1571458007,
		// Token: 0x04000522 RID: 1314
		TestObjectVectorPairStringBool = 1628831178,
		// Token: 0x04000523 RID: 1315
		GridLayout = 1742807556,
		// Token: 0x04000524 RID: 1316
		AssemblyDefinitionImporter = 1766753193,
		// Token: 0x04000525 RID: 1317
		ParentConstraint = 1773428102,
		// Token: 0x04000526 RID: 1318
		FakeComponent = 1803986026,
		// Token: 0x04000527 RID: 1319
		PositionConstraint = 1818360608,
		// Token: 0x04000528 RID: 1320
		RotationConstraint,
		// Token: 0x04000529 RID: 1321
		ScaleConstraint,
		// Token: 0x0400052A RID: 1322
		Tilemap = 1839735485,
		// Token: 0x0400052B RID: 1323
		PackageManifest = 1896753125,
		// Token: 0x0400052C RID: 1324
		PackageManifestImporter,
		// Token: 0x0400052D RID: 1325
		TerrainLayer = 1953259897,
		// Token: 0x0400052E RID: 1326
		SpriteShapeRenderer = 1971053207,
		// Token: 0x0400052F RID: 1327
		NativeObjectType = 1977754360,
		// Token: 0x04000530 RID: 1328
		TestObjectWithSerializedMapStringBool = 1981279845,
		// Token: 0x04000531 RID: 1329
		SerializableManagedHost = 1995898324,
		// Token: 0x04000532 RID: 1330
		VisualEffectAsset = 2058629509,
		// Token: 0x04000533 RID: 1331
		VisualEffectImporter,
		// Token: 0x04000534 RID: 1332
		VisualEffectResource,
		// Token: 0x04000535 RID: 1333
		VisualEffectObject = 2059678085,
		// Token: 0x04000536 RID: 1334
		VisualEffect = 2083052967,
		// Token: 0x04000537 RID: 1335
		LocalizationAsset = 2083778819,
		// Token: 0x04000538 RID: 1336
		ScriptedImporter = 2089858483
	}
}
