using System; 
using UnityEngine; 
using System.Collections; 
using System.Collections.Generic;

[ExecuteInEditMode]				
public class ES2Init : MonoBehaviour
{
	public void Awake()
	{
		Init();
	}
	
	public void Start()
	{
		if(Application.isEditor)
			GameObject.DestroyImmediate(gameObject);
		else
			GameObject.Destroy(gameObject);
	}

	public static void Init()
	{
		ES2TypeManager.types = new Dictionary<Type, ES2Type>();
				ES2TypeManager.types[typeof(UnityEngine.Vector2)] = new ES2_Vector2();
		ES2TypeManager.types[typeof(UnityEngine.Vector3)] = new ES2_Vector3();
		ES2TypeManager.types[typeof(UnityEngine.Vector4)] = new ES2_Vector4();
		ES2TypeManager.types[typeof(UnityEngine.Texture2D)] = new ES2_Texture2D();
		ES2TypeManager.types[typeof(UnityEngine.Quaternion)] = new ES2_Quaternion();
		ES2TypeManager.types[typeof(UnityEngine.Mesh)] = new ES2_Mesh();
		ES2TypeManager.types[typeof(UnityEngine.Color)] = new ES2_Color();
		ES2TypeManager.types[typeof(UnityEngine.Color32)] = new ES2_Color32();
		ES2TypeManager.types[typeof(UnityEngine.Material)] = new ES2_Material();
		ES2TypeManager.types[typeof(UnityEngine.Rect)] = new ES2_Rect();
		ES2TypeManager.types[typeof(UnityEngine.Bounds)] = new ES2_Bounds();
		ES2TypeManager.types[typeof(UnityEngine.Transform)] = new ES2_Transform();
		ES2TypeManager.types[typeof(UnityEngine.BoxCollider)] = new ES2_BoxCollider();
		ES2TypeManager.types[typeof(UnityEngine.CapsuleCollider)] = new ES2_CapsuleCollider();
		ES2TypeManager.types[typeof(UnityEngine.SphereCollider)] = new ES2_SphereCollider();
		ES2TypeManager.types[typeof(UnityEngine.MeshCollider)] = new ES2_MeshCollider();
		ES2TypeManager.types[typeof(System.Boolean)] = new ES2_bool();
		ES2TypeManager.types[typeof(System.Byte)] = new ES2_byte();
		ES2TypeManager.types[typeof(System.Char)] = new ES2_char();
		ES2TypeManager.types[typeof(System.Decimal)] = new ES2_decimal();
		ES2TypeManager.types[typeof(System.Double)] = new ES2_double();
		ES2TypeManager.types[typeof(System.Single)] = new ES2_float();
		ES2TypeManager.types[typeof(System.Int32)] = new ES2_int();
		ES2TypeManager.types[typeof(System.Int64)] = new ES2_long();
		ES2TypeManager.types[typeof(System.Int16)] = new ES2_short();
		ES2TypeManager.types[typeof(System.String)] = new ES2_string();
		ES2TypeManager.types[typeof(System.UInt32)] = new ES2_uint();
		ES2TypeManager.types[typeof(System.UInt64)] = new ES2_ulong();
		ES2TypeManager.types[typeof(System.UInt16)] = new ES2_ushort();
		ES2TypeManager.types[typeof(System.Enum)] = new ES2_Enum();
		ES2TypeManager.types[typeof(UnityEngine.Matrix4x4)] = new ES2_Matrix4x4();
		ES2TypeManager.types[typeof(UnityEngine.BoneWeight)] = new ES2_BoneWeight();
		ES2TypeManager.types[typeof(System.SByte)] = new ES2_sbyte();
		ES2TypeManager.types[typeof(UnityEngine.GradientAlphaKey)] = new ES2_GradientAlphaKey();
		ES2TypeManager.types[typeof(UnityEngine.GradientColorKey)] = new ES2_GradientColorKey();
		ES2TypeManager.types[typeof(UnityEngine.Gradient)] = new ES2_Gradient();
		ES2TypeManager.types[typeof(UnityEngine.Sprite)] = new ES2_Sprite();
		ES2TypeManager.types[typeof(ES2AutoSaveManager)] = new ES2_ES2AutoSaveManager();
		ES2TypeManager.types[typeof(System.DateTime)] = new ES2_DateTime();
		ES2TypeManager.types[typeof(UnityEngine.PolygonCollider2D)] = new ES2_PolygonCollider2D();
		ES2TypeManager.types[typeof(System.Object)] = new ES2_object();
		ES2TypeManager.types[typeof(UnityEngine.Texture)] = new ES2_Texture();
		ES2TypeManager.types[typeof(AimTarget)] = new ES2UserType_AimTarget();
		ES2TypeManager.types[typeof(Com.LuisPedroFonseca.ProCamera2D.ProCamera2DTriggerZoom)] = new ES2UserType_ComLuisPedroFonsecaProCamera2DProCamera2DTriggerZoom();
		ES2TypeManager.types[typeof(CutSceneUnit)] = new ES2UserType_CutSceneUnit();
		ES2TypeManager.types[typeof(Door)] = new ES2UserType_Door();
		ES2TypeManager.types[typeof(EnemyDummyAnimation)] = new ES2UserType_EnemyDummyAnimation();
		ES2TypeManager.types[typeof(EnemyInfo)] = new ES2UserType_EnemyInfo();
		ES2TypeManager.types[typeof(EquiptInfo)] = new ES2UserType_EquiptInfo();
		ES2TypeManager.types[typeof(Extinguisher)] = new ES2UserType_Extinguisher();
		ES2TypeManager.types[typeof(ExtinguisherParticles)] = new ES2UserType_ExtinguisherParticles();
		ES2TypeManager.types[typeof(GhostingEffect)] = new ES2UserType_GhostingEffect();
		ES2TypeManager.types[typeof(InteractConditionChecker)] = new ES2UserType_InteractConditionChecker();
		ES2TypeManager.types[typeof(NPC)] = new ES2UserType_NPC();
		ES2TypeManager.types[typeof(NPCBubbleChat)] = new ES2UserType_NPCBubbleChat();
		ES2TypeManager.types[typeof(NPCDialogueChat)] = new ES2UserType_NPCDialogueChat();
		ES2TypeManager.types[typeof(OutsideInfo)] = new ES2UserType_OutsideInfo();
		ES2TypeManager.types[typeof(PlayerAction)] = new ES2UserType_PlayerAction();
		ES2TypeManager.types[typeof(PlayerInfo)] = new ES2UserType_PlayerInfo();
		ES2TypeManager.types[typeof(PlayerShoulderAction)] = new ES2UserType_PlayerShoulderAction();
		ES2TypeManager.types[typeof(SelfConditionChecker)] = new ES2UserType_SelfConditionChecker();
		ES2TypeManager.types[typeof(SwitchableLamp)] = new ES2UserType_SwitchableLamp();
		ES2TypeManager.types[typeof(TimeRecallable)] = new ES2UserType_TimeRecallable();
		ES2TypeManager.types[typeof(UnityEngine.Animator)] = new ES2UserType_UnityEngineAnimator();
		ES2TypeManager.types[typeof(UnityEngine.Light)] = new ES2UserType_UnityEngineLight();
		ES2TypeManager.types[typeof(UnityEngine.ParticleSystem)] = new ES2UserType_UnityEngineParticleSystem();
		ES2TypeManager.types[typeof(UnityEngine.ParticleSystemRenderer)] = new ES2UserType_UnityEngineParticleSystemRenderer();
		ES2TypeManager.types[typeof(UnityEngine.Rigidbody)] = new ES2UserType_UnityEngineRigidbody();
		ES2TypeManager.types[typeof(UnityEngine.SpriteRenderer)] = new ES2UserType_UnityEngineSpriteRenderer();
		ES2TypeManager.types[typeof(Weapon)] = new ES2UserType_Weapon();
		ES2TypeManager.types[typeof(UnityEngine.AudioClip)] = new ES2_AudioClip();
		ES2TypeManager.types[typeof(UnityEngine.GameObject)] = new ES2_GameObject();
		ES2TypeManager.types[typeof(SellmaCutSceneAnimation)] = new ES2UserType_SellmaCutSceneAnimation();

		ES2.initialised = true;
	}
}