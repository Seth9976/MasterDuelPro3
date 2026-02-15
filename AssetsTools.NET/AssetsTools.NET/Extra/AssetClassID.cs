using System;

namespace AssetsTools.NET.Extra
{
	// Token: 0x02000074 RID: 116
	public enum AssetClassID
	{
		// Token: 0x04000278 RID: 632
		Object,
		// Token: 0x04000279 RID: 633
		GameObject,
		// Token: 0x0400027A RID: 634
		Component,
		// Token: 0x0400027B RID: 635
		LevelGameManager,
		// Token: 0x0400027C RID: 636
		Transform,
		// Token: 0x0400027D RID: 637
		TimeManager,
		// Token: 0x0400027E RID: 638
		GlobalGameManager,
		// Token: 0x0400027F RID: 639
		Behaviour = 8,
		// Token: 0x04000280 RID: 640
		GameManager,
		// Token: 0x04000281 RID: 641
		AudioManager = 11,
		// Token: 0x04000282 RID: 642
		ParticleAnimator,
		// Token: 0x04000283 RID: 643
		InputManager,
		// Token: 0x04000284 RID: 644
		EllipsoidParticleEmitter = 15,
		// Token: 0x04000285 RID: 645
		Pipeline = 17,
		// Token: 0x04000286 RID: 646
		EditorExtension,
		// Token: 0x04000287 RID: 647
		Physics2DSettings,
		// Token: 0x04000288 RID: 648
		Camera,
		// Token: 0x04000289 RID: 649
		Material,
		// Token: 0x0400028A RID: 650
		MeshRenderer = 23,
		// Token: 0x0400028B RID: 651
		Renderer = 25,
		// Token: 0x0400028C RID: 652
		ParticleRenderer,
		// Token: 0x0400028D RID: 653
		Texture,
		// Token: 0x0400028E RID: 654
		Texture2D,
		// Token: 0x0400028F RID: 655
		OcclusionCullingSettings,
		// Token: 0x04000290 RID: 656
		GraphicsSettings,
		// Token: 0x04000291 RID: 657
		MeshFilter = 33,
		// Token: 0x04000292 RID: 658
		OcclusionPortal = 41,
		// Token: 0x04000293 RID: 659
		Mesh = 43,
		// Token: 0x04000294 RID: 660
		Skybox = 45,
		// Token: 0x04000295 RID: 661
		QualitySettings = 47,
		// Token: 0x04000296 RID: 662
		Shader,
		// Token: 0x04000297 RID: 663
		TextAsset,
		// Token: 0x04000298 RID: 664
		Rigidbody2D,
		// Token: 0x04000299 RID: 665
		Physics2DManager,
		// Token: 0x0400029A RID: 666
		NotificationManager,
		// Token: 0x0400029B RID: 667
		Collider2D,
		// Token: 0x0400029C RID: 668
		Rigidbody,
		// Token: 0x0400029D RID: 669
		PhysicsManager,
		// Token: 0x0400029E RID: 670
		Collider,
		// Token: 0x0400029F RID: 671
		Joint,
		// Token: 0x040002A0 RID: 672
		CircleCollider2D,
		// Token: 0x040002A1 RID: 673
		HingeJoint,
		// Token: 0x040002A2 RID: 674
		PolygonCollider2D,
		// Token: 0x040002A3 RID: 675
		BoxCollider2D,
		// Token: 0x040002A4 RID: 676
		PhysicsMaterial2D,
		// Token: 0x040002A5 RID: 677
		MeshCollider = 64,
		// Token: 0x040002A6 RID: 678
		BoxCollider,
		// Token: 0x040002A7 RID: 679
		CompositeCollider2D,
		// Token: 0x040002A8 RID: 680
		EdgeCollider2D = 68,
		// Token: 0x040002A9 RID: 681
		PolygonColliderBase2D,
		// Token: 0x040002AA RID: 682
		CapsuleCollider2D,
		// Token: 0x040002AB RID: 683
		AnimationManager,
		// Token: 0x040002AC RID: 684
		ComputeShader,
		// Token: 0x040002AD RID: 685
		AnimationClip = 74,
		// Token: 0x040002AE RID: 686
		ConstantForce,
		// Token: 0x040002AF RID: 687
		WorldParticleCollider,
		// Token: 0x040002B0 RID: 688
		TagManager = 78,
		// Token: 0x040002B1 RID: 689
		AudioListener = 81,
		// Token: 0x040002B2 RID: 690
		AudioSource,
		// Token: 0x040002B3 RID: 691
		AudioClip,
		// Token: 0x040002B4 RID: 692
		RenderTexture,
		// Token: 0x040002B5 RID: 693
		CustomRenderTexture = 86,
		// Token: 0x040002B6 RID: 694
		MeshParticleEmitter,
		// Token: 0x040002B7 RID: 695
		ParticleEmitter,
		// Token: 0x040002B8 RID: 696
		Cubemap,
		// Token: 0x040002B9 RID: 697
		Avatar,
		// Token: 0x040002BA RID: 698
		AnimatorController,
		// Token: 0x040002BB RID: 699
		GUILayer,
		// Token: 0x040002BC RID: 700
		RuntimeAnimatorController,
		// Token: 0x040002BD RID: 701
		ScriptMapper,
		// Token: 0x040002BE RID: 702
		Animator,
		// Token: 0x040002BF RID: 703
		TrailRenderer,
		// Token: 0x040002C0 RID: 704
		DelayedCallManager = 98,
		// Token: 0x040002C1 RID: 705
		TextMesh = 102,
		// Token: 0x040002C2 RID: 706
		RenderSettings = 104,
		// Token: 0x040002C3 RID: 707
		Light = 108,
		// Token: 0x040002C4 RID: 708
		CGProgram,
		// Token: 0x040002C5 RID: 709
		BaseAnimationTrack,
		// Token: 0x040002C6 RID: 710
		Animation,
		// Token: 0x040002C7 RID: 711
		MonoBehaviour = 114,
		// Token: 0x040002C8 RID: 712
		MonoScript,
		// Token: 0x040002C9 RID: 713
		MonoManager,
		// Token: 0x040002CA RID: 714
		Texture3D,
		// Token: 0x040002CB RID: 715
		NewAnimationTrack,
		// Token: 0x040002CC RID: 716
		Projector,
		// Token: 0x040002CD RID: 717
		LineRenderer,
		// Token: 0x040002CE RID: 718
		Flare,
		// Token: 0x040002CF RID: 719
		Halo,
		// Token: 0x040002D0 RID: 720
		LensFlare,
		// Token: 0x040002D1 RID: 721
		FlareLayer,
		// Token: 0x040002D2 RID: 722
		HaloLayer,
		// Token: 0x040002D3 RID: 723
		NavMeshProjectSettings,
		// Token: 0x040002D4 RID: 724
		HaloManager,
		// Token: 0x040002D5 RID: 725
		Font,
		// Token: 0x040002D6 RID: 726
		PlayerSettings,
		// Token: 0x040002D7 RID: 727
		NamedObject,
		// Token: 0x040002D8 RID: 728
		GUITexture,
		// Token: 0x040002D9 RID: 729
		GUIText,
		// Token: 0x040002DA RID: 730
		GUIElement,
		// Token: 0x040002DB RID: 731
		PhysicMaterial,
		// Token: 0x040002DC RID: 732
		SphereCollider,
		// Token: 0x040002DD RID: 733
		CapsuleCollider,
		// Token: 0x040002DE RID: 734
		SkinnedMeshRenderer,
		// Token: 0x040002DF RID: 735
		FixedJoint,
		// Token: 0x040002E0 RID: 736
		RaycastCollider = 140,
		// Token: 0x040002E1 RID: 737
		BuildSettings,
		// Token: 0x040002E2 RID: 738
		AssetBundle,
		// Token: 0x040002E3 RID: 739
		CharacterController,
		// Token: 0x040002E4 RID: 740
		CharacterJoint,
		// Token: 0x040002E5 RID: 741
		SpringJoint,
		// Token: 0x040002E6 RID: 742
		WheelCollider,
		// Token: 0x040002E7 RID: 743
		ResourceManager,
		// Token: 0x040002E8 RID: 744
		NetworkView,
		// Token: 0x040002E9 RID: 745
		NetworkManager,
		// Token: 0x040002EA RID: 746
		PreloadData,
		// Token: 0x040002EB RID: 747
		MovieTexture = 152,
		// Token: 0x040002EC RID: 748
		ConfigurableJoint,
		// Token: 0x040002ED RID: 749
		TerrainCollider,
		// Token: 0x040002EE RID: 750
		MasterServerInterface,
		// Token: 0x040002EF RID: 751
		TerrainData,
		// Token: 0x040002F0 RID: 752
		LightmapSettings,
		// Token: 0x040002F1 RID: 753
		WebCamTexture,
		// Token: 0x040002F2 RID: 754
		EditorSettings,
		// Token: 0x040002F3 RID: 755
		InteractiveCloth,
		// Token: 0x040002F4 RID: 756
		ClothRenderer,
		// Token: 0x040002F5 RID: 757
		EditorUserSettings,
		// Token: 0x040002F6 RID: 758
		SkinnedCloth,
		// Token: 0x040002F7 RID: 759
		AudioReverbFilter,
		// Token: 0x040002F8 RID: 760
		AudioHighPassFilter,
		// Token: 0x040002F9 RID: 761
		AudioChorusFilter,
		// Token: 0x040002FA RID: 762
		AudioReverbZone,
		// Token: 0x040002FB RID: 763
		AudioEchoFilter,
		// Token: 0x040002FC RID: 764
		AudioLowPassFilter,
		// Token: 0x040002FD RID: 765
		AudioDistortionFilter,
		// Token: 0x040002FE RID: 766
		SparseTexture,
		// Token: 0x040002FF RID: 767
		AudioBehaviour = 180,
		// Token: 0x04000300 RID: 768
		AudioFilter,
		// Token: 0x04000301 RID: 769
		WindZone,
		// Token: 0x04000302 RID: 770
		Cloth,
		// Token: 0x04000303 RID: 771
		SubstanceArchive,
		// Token: 0x04000304 RID: 772
		ProceduralMaterial,
		// Token: 0x04000305 RID: 773
		ProceduralTexture,
		// Token: 0x04000306 RID: 774
		Texture2DArray,
		// Token: 0x04000307 RID: 775
		CubemapArray,
		// Token: 0x04000308 RID: 776
		OffMeshLink = 191,
		// Token: 0x04000309 RID: 777
		OcclusionArea,
		// Token: 0x0400030A RID: 778
		Tree,
		// Token: 0x0400030B RID: 779
		NavMeshObsolete,
		// Token: 0x0400030C RID: 780
		NavMeshAgent,
		// Token: 0x0400030D RID: 781
		NavMeshSettings,
		// Token: 0x0400030E RID: 782
		LightProbesLegacy,
		// Token: 0x0400030F RID: 783
		ParticleSystem,
		// Token: 0x04000310 RID: 784
		ParticleSystemRenderer,
		// Token: 0x04000311 RID: 785
		ShaderVariantCollection,
		// Token: 0x04000312 RID: 786
		LODGroup = 205,
		// Token: 0x04000313 RID: 787
		BlendTree,
		// Token: 0x04000314 RID: 788
		Motion,
		// Token: 0x04000315 RID: 789
		NavMeshObstacle,
		// Token: 0x04000316 RID: 790
		SortingGroup = 210,
		// Token: 0x04000317 RID: 791
		SpriteRenderer = 212,
		// Token: 0x04000318 RID: 792
		Sprite,
		// Token: 0x04000319 RID: 793
		CachedSpriteAtlas,
		// Token: 0x0400031A RID: 794
		ReflectionProbe,
		// Token: 0x0400031B RID: 795
		ReflectionProbes,
		// Token: 0x0400031C RID: 796
		Terrain = 218,
		// Token: 0x0400031D RID: 797
		LightProbeGroup = 220,
		// Token: 0x0400031E RID: 798
		AnimatorOverrideController,
		// Token: 0x0400031F RID: 799
		CanvasRenderer,
		// Token: 0x04000320 RID: 800
		Canvas,
		// Token: 0x04000321 RID: 801
		RectTransform,
		// Token: 0x04000322 RID: 802
		CanvasGroup,
		// Token: 0x04000323 RID: 803
		BillboardAsset,
		// Token: 0x04000324 RID: 804
		BillboardRenderer,
		// Token: 0x04000325 RID: 805
		SpeedTreeWindAsset,
		// Token: 0x04000326 RID: 806
		AnchoredJoint2D,
		// Token: 0x04000327 RID: 807
		Joint2D,
		// Token: 0x04000328 RID: 808
		SpringJoint2D,
		// Token: 0x04000329 RID: 809
		DistanceJoint2D,
		// Token: 0x0400032A RID: 810
		HingeJoint2D,
		// Token: 0x0400032B RID: 811
		SliderJoint2D,
		// Token: 0x0400032C RID: 812
		WheelJoint2D,
		// Token: 0x0400032D RID: 813
		ClusterInputManager,
		// Token: 0x0400032E RID: 814
		BaseVideoTexture,
		// Token: 0x0400032F RID: 815
		NavMeshData,
		// Token: 0x04000330 RID: 816
		AudioMixer = 240,
		// Token: 0x04000331 RID: 817
		AudioMixerController,
		// Token: 0x04000332 RID: 818
		AudioMixerGroupController = 243,
		// Token: 0x04000333 RID: 819
		AudioMixerEffectController,
		// Token: 0x04000334 RID: 820
		AudioMixerSnapshotController,
		// Token: 0x04000335 RID: 821
		PhysicsUpdateBehaviour2D,
		// Token: 0x04000336 RID: 822
		ConstantForce2D,
		// Token: 0x04000337 RID: 823
		Effector2D,
		// Token: 0x04000338 RID: 824
		AreaEffector2D,
		// Token: 0x04000339 RID: 825
		PointEffector2D,
		// Token: 0x0400033A RID: 826
		PlatformEffector2D,
		// Token: 0x0400033B RID: 827
		SurfaceEffector2D,
		// Token: 0x0400033C RID: 828
		BuoyancyEffector2D,
		// Token: 0x0400033D RID: 829
		RelativeJoint2D,
		// Token: 0x0400033E RID: 830
		FixedJoint2D,
		// Token: 0x0400033F RID: 831
		FrictionJoint2D,
		// Token: 0x04000340 RID: 832
		TargetJoint2D,
		// Token: 0x04000341 RID: 833
		LightProbes,
		// Token: 0x04000342 RID: 834
		LightProbeProxyVolume,
		// Token: 0x04000343 RID: 835
		SampleClip = 271,
		// Token: 0x04000344 RID: 836
		AudioMixerSnapshot,
		// Token: 0x04000345 RID: 837
		AudioMixerGroup,
		// Token: 0x04000346 RID: 838
		NScreenBridge = 280,
		// Token: 0x04000347 RID: 839
		AssetBundleManifest = 290,
		// Token: 0x04000348 RID: 840
		UnityAdsManager = 292,
		// Token: 0x04000349 RID: 841
		RuntimeInitializeOnLoadManager = 300,
		// Token: 0x0400034A RID: 842
		CloudWebServicesManager,
		// Token: 0x0400034B RID: 843
		CloudServiceHandlerBehaviour,
		// Token: 0x0400034C RID: 844
		UnityAnalyticsManager,
		// Token: 0x0400034D RID: 845
		CrashReportManager,
		// Token: 0x0400034E RID: 846
		PerformanceReportingManager,
		// Token: 0x0400034F RID: 847
		UnityConnectSettings = 310,
		// Token: 0x04000350 RID: 848
		AvatarMask = 319,
		// Token: 0x04000351 RID: 849
		PlayableDirector,
		// Token: 0x04000352 RID: 850
		VideoPlayer = 328,
		// Token: 0x04000353 RID: 851
		VideoClip,
		// Token: 0x04000354 RID: 852
		ParticleSystemForceField,
		// Token: 0x04000355 RID: 853
		SpriteMask,
		// Token: 0x04000356 RID: 854
		WorldAnchor = 362,
		// Token: 0x04000357 RID: 855
		OcclusionCullingData,
		// Token: 0x04000358 RID: 856
		SmallestEditorClassID = 1000,
		// Token: 0x04000359 RID: 857
		PrefabInstance,
		// Token: 0x0400035A RID: 858
		EditorExtensionImpl,
		// Token: 0x0400035B RID: 859
		AssetImporter,
		// Token: 0x0400035C RID: 860
		AssetDatabaseV1,
		// Token: 0x0400035D RID: 861
		Mesh3DSImporter,
		// Token: 0x0400035E RID: 862
		TextureImporter,
		// Token: 0x0400035F RID: 863
		ShaderImporter,
		// Token: 0x04000360 RID: 864
		ComputeShaderImporter,
		// Token: 0x04000361 RID: 865
		AudioImporter = 1020,
		// Token: 0x04000362 RID: 866
		HierarchyState = 1026,
		// Token: 0x04000363 RID: 867
		GUIDSerializer,
		// Token: 0x04000364 RID: 868
		AssetMetaData,
		// Token: 0x04000365 RID: 869
		DefaultAsset,
		// Token: 0x04000366 RID: 870
		DefaultImporter,
		// Token: 0x04000367 RID: 871
		TextScriptImporter,
		// Token: 0x04000368 RID: 872
		SceneAsset,
		// Token: 0x04000369 RID: 873
		NativeFormatImporter = 1034,
		// Token: 0x0400036A RID: 874
		MonoImporter,
		// Token: 0x0400036B RID: 875
		AssetServerCache = 1037,
		// Token: 0x0400036C RID: 876
		LibraryAssetImporter,
		// Token: 0x0400036D RID: 877
		ModelImporter = 1040,
		// Token: 0x0400036E RID: 878
		FBXImporter,
		// Token: 0x0400036F RID: 879
		TrueTypeFontImporter,
		// Token: 0x04000370 RID: 880
		MovieImporter = 1044,
		// Token: 0x04000371 RID: 881
		EditorBuildSettings,
		// Token: 0x04000372 RID: 882
		DDSImporter,
		// Token: 0x04000373 RID: 883
		InspectorExpandedState = 1048,
		// Token: 0x04000374 RID: 884
		AnnotationManager,
		// Token: 0x04000375 RID: 885
		PluginImporter,
		// Token: 0x04000376 RID: 886
		EditorUserBuildSettings,
		// Token: 0x04000377 RID: 887
		PVRImporter,
		// Token: 0x04000378 RID: 888
		ASTCImporter,
		// Token: 0x04000379 RID: 889
		KTXImporter,
		// Token: 0x0400037A RID: 890
		IHVImageFormatImporter,
		// Token: 0x0400037B RID: 891
		AnimatorStateTransition = 1101,
		// Token: 0x0400037C RID: 892
		AnimatorState,
		// Token: 0x0400037D RID: 893
		HumanTemplate = 1105,
		// Token: 0x0400037E RID: 894
		AnimatorStateMachine = 1107,
		// Token: 0x0400037F RID: 895
		PreviewAnimationClip,
		// Token: 0x04000380 RID: 896
		AnimatorTransition,
		// Token: 0x04000381 RID: 897
		SpeedTreeImporter,
		// Token: 0x04000382 RID: 898
		AnimatorTransitionBase,
		// Token: 0x04000383 RID: 899
		SubstanceImporter,
		// Token: 0x04000384 RID: 900
		LightmapParameters,
		// Token: 0x04000385 RID: 901
		LightingDataAsset = 1120,
		// Token: 0x04000386 RID: 902
		GISRaster,
		// Token: 0x04000387 RID: 903
		GISRasterImporter,
		// Token: 0x04000388 RID: 904
		CadImporter,
		// Token: 0x04000389 RID: 905
		SketchUpImporter,
		// Token: 0x0400038A RID: 906
		BuildReport,
		// Token: 0x0400038B RID: 907
		PackedAssets,
		// Token: 0x0400038C RID: 908
		VideoClipImporter,
		// Token: 0x0400038D RID: 909
		ActivationLogComponent = 2000,
		// Token: 0x0400038E RID: 910
		@int = 100000,
		// Token: 0x0400038F RID: 911
		@bool,
		// Token: 0x04000390 RID: 912
		@float,
		// Token: 0x04000391 RID: 913
		MonoObject,
		// Token: 0x04000392 RID: 914
		Collision,
		// Token: 0x04000393 RID: 915
		Vector3f,
		// Token: 0x04000394 RID: 916
		RootMotionData,
		// Token: 0x04000395 RID: 917
		Collision2D,
		// Token: 0x04000396 RID: 918
		AudioMixerLiveUpdateFloat,
		// Token: 0x04000397 RID: 919
		AudioMixerLiveUpdateBool,
		// Token: 0x04000398 RID: 920
		Polygon2D,
		// Token: 0x04000399 RID: 921
		@void,
		// Token: 0x0400039A RID: 922
		TilemapCollider2D = 19719996,
		// Token: 0x0400039B RID: 923
		AssetImporterLog = 41386430,
		// Token: 0x0400039C RID: 924
		VFXRenderer = 73398921,
		// Token: 0x0400039D RID: 925
		SerializableManagedRefTestClass = 76251197,
		// Token: 0x0400039E RID: 926
		Grid = 156049354,
		// Token: 0x0400039F RID: 927
		ScenesUsingAssets = 156483287,
		// Token: 0x040003A0 RID: 928
		ArticulationBody = 171741748,
		// Token: 0x040003A1 RID: 929
		Preset = 181963792,
		// Token: 0x040003A2 RID: 930
		EmptyObject = 277625683,
		// Token: 0x040003A3 RID: 931
		IConstraint = 285090594,
		// Token: 0x040003A4 RID: 932
		TestObjectWithSpecialLayoutOne = 293259124,
		// Token: 0x040003A5 RID: 933
		AssemblyDefinitionReferenceImporter = 294290339,
		// Token: 0x040003A6 RID: 934
		SiblingDerived = 334799969,
		// Token: 0x040003A7 RID: 935
		TestObjectWithSerializedMapStringNonAlignedStruct = 342846651,
		// Token: 0x040003A8 RID: 936
		SubDerived = 367388927,
		// Token: 0x040003A9 RID: 937
		AssetImportInProgressProxy = 369655926,
		// Token: 0x040003AA RID: 938
		PluginBuildInfo = 382020655,
		// Token: 0x040003AB RID: 939
		EditorProjectAccess = 426301858,
		// Token: 0x040003AC RID: 940
		PrefabImporter = 468431735,
		// Token: 0x040003AD RID: 941
		TestObjectWithSerializedArray = 478637458,
		// Token: 0x040003AE RID: 942
		TestObjectWithSerializedAnimationCurve,
		// Token: 0x040003AF RID: 943
		TilemapRenderer = 483693784,
		// Token: 0x040003B0 RID: 944
		ScriptableCamera = 488575907,
		// Token: 0x040003B1 RID: 945
		SpriteAtlasAsset = 612988286,
		// Token: 0x040003B2 RID: 946
		SpriteAtlasDatabase = 638013454,
		// Token: 0x040003B3 RID: 947
		AudioBuildInfo = 641289076,
		// Token: 0x040003B4 RID: 948
		CachedSpriteAtlasRuntimeData = 644342135,
		// Token: 0x040003B5 RID: 949
		RendererFake = 646504946,
		// Token: 0x040003B6 RID: 950
		AssemblyDefinitionReferenceAsset = 662584278,
		// Token: 0x040003B7 RID: 951
		BuiltAssetBundleInfoSet = 668709126,
		// Token: 0x040003B8 RID: 952
		SpriteAtlas = 687078895,
		// Token: 0x040003B9 RID: 953
		RayTracingShaderImporter = 747330370,
		// Token: 0x040003BA RID: 954
		PreviewImporter = 815301076,
		// Token: 0x040003BB RID: 955
		RayTracingShader = 825902497,
		// Token: 0x040003BC RID: 956
		LightingSettings = 850595691,
		// Token: 0x040003BD RID: 957
		PlatformModuleSetup = 877146078,
		// Token: 0x040003BE RID: 958
		VersionControlSettings = 890905787,
		// Token: 0x040003BF RID: 959
		AimConstraint = 895512359,
		// Token: 0x040003C0 RID: 960
		VFXManager = 937362698,
		// Token: 0x040003C1 RID: 961
		VisualEffectSubgraph = 994735392,
		// Token: 0x040003C2 RID: 962
		RuleSetFileAsset = 954905827,
		// Token: 0x040003C3 RID: 963
		VisualEffectSubgraphOperator = 994735403,
		// Token: 0x040003C4 RID: 964
		VisualEffectSubgraphBlock,
		// Token: 0x040003C5 RID: 965
		Prefab = 1001480554,
		// Token: 0x040003C6 RID: 966
		LocalizationImporter = 1027052791,
		// Token: 0x040003C7 RID: 967
		Derived = 1091556383,
		// Token: 0x040003C8 RID: 968
		PropertyModificationsTargetTestObject = 1111377672,
		// Token: 0x040003C9 RID: 969
		ReferencesArtifactGenerator = 1114811875,
		// Token: 0x040003CA RID: 970
		AssemblyDefinitionAsset = 1152215463,
		// Token: 0x040003CB RID: 971
		SceneVisibilityState = 1154873562,
		// Token: 0x040003CC RID: 972
		LookAtConstraint = 1183024399,
		// Token: 0x040003CD RID: 973
		SpriteAtlasImporter = 1210832254,
		// Token: 0x040003CE RID: 974
		MultiArtifactTestImporter = 1223240404,
		// Token: 0x040003CF RID: 975
		GameObjectRecorder = 1268269756,
		// Token: 0x040003D0 RID: 976
		LightingDataAssetParent = 1325145578,
		// Token: 0x040003D1 RID: 977
		PresetManager = 1386491679,
		// Token: 0x040003D2 RID: 978
		TestObjectWithSpecialLayoutTwo = 1392443030,
		// Token: 0x040003D3 RID: 979
		StreamingManager = 1403656975,
		// Token: 0x040003D4 RID: 980
		LowerResBlitTexture = 1480428607,
		// Token: 0x040003D5 RID: 981
		StreamingController = 1542919678,
		// Token: 0x040003D6 RID: 982
		RenderPassAttachment = 1571458007,
		// Token: 0x040003D7 RID: 983
		TestObjectVectorPairStringBool = 1628831178,
		// Token: 0x040003D8 RID: 984
		GridLayout = 1742807556,
		// Token: 0x040003D9 RID: 985
		AssemblyDefinitionImporter = 1766753193,
		// Token: 0x040003DA RID: 986
		ParentConstraint = 1773428102,
		// Token: 0x040003DB RID: 987
		FakeComponent = 1803986026,
		// Token: 0x040003DC RID: 988
		RuleSetFileImporter = 1777034230,
		// Token: 0x040003DD RID: 989
		PositionConstraint = 1818360608,
		// Token: 0x040003DE RID: 990
		RotationConstraint,
		// Token: 0x040003DF RID: 991
		ScaleConstraint,
		// Token: 0x040003E0 RID: 992
		Tilemap = 1839735485,
		// Token: 0x040003E1 RID: 993
		PackageManifest = 1896753125,
		// Token: 0x040003E2 RID: 994
		PackageManifestImporter,
		// Token: 0x040003E3 RID: 995
		TerrainLayer = 1953259897,
		// Token: 0x040003E4 RID: 996
		SpriteShapeRenderer = 1971053207,
		// Token: 0x040003E5 RID: 997
		NativeObjectType = 1977754360,
		// Token: 0x040003E6 RID: 998
		TestObjectWithSerializedMapStringBool = 1981279845,
		// Token: 0x040003E7 RID: 999
		SerializableManagedHost = 1995898324,
		// Token: 0x040003E8 RID: 1000
		VisualEffectAsset = 2058629509,
		// Token: 0x040003E9 RID: 1001
		VisualEffectImporter,
		// Token: 0x040003EA RID: 1002
		VisualEffectResource,
		// Token: 0x040003EB RID: 1003
		VisualEffectObject = 2059678085,
		// Token: 0x040003EC RID: 1004
		VisualEffect = 2083052967,
		// Token: 0x040003ED RID: 1005
		LocalizationAsset = 2083778819,
		// Token: 0x040003EE RID: 1006
		ScriptedImporter = 2089858483,
		// Token: 0x040003EF RID: 1007
		TilemapEditorUserSettings = 2126867596
	}
}
