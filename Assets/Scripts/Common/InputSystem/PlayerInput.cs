// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Common/InputSystem/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""b1ddd4cc-ec58-42ed-8b9a-d874a83612ab"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""0b6c6935-eb26-44f9-b73b-1e5202707645"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""cc12ada1-9b6c-4de8-9cc7-a2400e207db9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackHold"",
                    ""type"": ""Button"",
                    ""id"": ""43845cb0-52f8-4f2d-bf1a-425b0f669721"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackCancled"",
                    ""type"": ""Button"",
                    ""id"": ""742755cb-39de-4312-8b3a-9039602a2261"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""3ca124ac-8da5-45f7-8c5a-f5c22e6042dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill1"",
                    ""type"": ""Button"",
                    ""id"": ""1c444c64-9719-420e-8e3d-3d6d28180d26"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Skill1Hold"",
                    ""type"": ""Button"",
                    ""id"": ""43083780-bebb-411b-9f0f-4021cd15271e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Skill2"",
                    ""type"": ""Button"",
                    ""id"": ""bbf99c3d-2b50-4ebf-94fc-908079914999"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""CameraRotate"",
                    ""type"": ""Value"",
                    ""id"": ""53b85a1c-84f7-4c23-942d-bed4604f32d5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraDistance"",
                    ""type"": ""Value"",
                    ""id"": ""5bfcb733-afa6-41ff-9d13-9ccb69f9237e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""4f52d627-7a84-4f13-afa1-a71185b51722"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b4c5896c-1804-44e9-82b8-7db83eee6c12"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""11b24dfb-994d-43ce-b1ff-4219c40a634d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6dc3c551-a699-41f6-b6b7-01b8c26d5f30"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""01f61b3b-ae01-42cc-81c1-b58ec5b3d508"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""84724e5d-36e0-47cc-b281-342de17cd797"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""be4cd7ba-aac8-44e1-98f0-c320c86d03ca"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5dc7f517-1b42-4b89-a5e6-5c06c9fee563"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f2cc243-84d4-4c38-bcab-ac5dabf515ae"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d038dedd-3287-4fff-b535-f3013c2e2d95"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db700cc5-ec9e-406c-b6c7-a7c065e61ca7"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraDistance"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0097c4d3-6a5b-49e5-9c70-1216a503b280"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0af126eb-0300-4b24-b08a-77960c7466a9"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6b5c241-aaaf-430b-b3e1-a0339059aee9"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ad328df-4cfc-4991-8851-8752bfef28a0"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill1Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""802f3e83-29ac-481a-b7cf-f7318b81fc0d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackCancled"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_AttackHold = m_Player.FindAction("AttackHold", throwIfNotFound: true);
        m_Player_AttackCancled = m_Player.FindAction("AttackCancled", throwIfNotFound: true);
        m_Player_Dodge = m_Player.FindAction("Dodge", throwIfNotFound: true);
        m_Player_Skill1 = m_Player.FindAction("Skill1", throwIfNotFound: true);
        m_Player_Skill1Hold = m_Player.FindAction("Skill1Hold", throwIfNotFound: true);
        m_Player_Skill2 = m_Player.FindAction("Skill2", throwIfNotFound: true);
        m_Player_CameraRotate = m_Player.FindAction("CameraRotate", throwIfNotFound: true);
        m_Player_CameraDistance = m_Player.FindAction("CameraDistance", throwIfNotFound: true);
        m_Player_MousePosition = m_Player.FindAction("MousePosition", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_AttackHold;
    private readonly InputAction m_Player_AttackCancled;
    private readonly InputAction m_Player_Dodge;
    private readonly InputAction m_Player_Skill1;
    private readonly InputAction m_Player_Skill1Hold;
    private readonly InputAction m_Player_Skill2;
    private readonly InputAction m_Player_CameraRotate;
    private readonly InputAction m_Player_CameraDistance;
    private readonly InputAction m_Player_MousePosition;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @AttackHold => m_Wrapper.m_Player_AttackHold;
        public InputAction @AttackCancled => m_Wrapper.m_Player_AttackCancled;
        public InputAction @Dodge => m_Wrapper.m_Player_Dodge;
        public InputAction @Skill1 => m_Wrapper.m_Player_Skill1;
        public InputAction @Skill1Hold => m_Wrapper.m_Player_Skill1Hold;
        public InputAction @Skill2 => m_Wrapper.m_Player_Skill2;
        public InputAction @CameraRotate => m_Wrapper.m_Player_CameraRotate;
        public InputAction @CameraDistance => m_Wrapper.m_Player_CameraDistance;
        public InputAction @MousePosition => m_Wrapper.m_Player_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @AttackHold.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackHold;
                @AttackHold.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackHold;
                @AttackHold.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackHold;
                @AttackCancled.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackCancled;
                @AttackCancled.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackCancled;
                @AttackCancled.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackCancled;
                @Dodge.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Skill1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill1;
                @Skill1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill1;
                @Skill1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill1;
                @Skill1Hold.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill1Hold;
                @Skill1Hold.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill1Hold;
                @Skill1Hold.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill1Hold;
                @Skill2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill2;
                @Skill2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill2;
                @Skill2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill2;
                @CameraRotate.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotate;
                @CameraRotate.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotate;
                @CameraRotate.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotate;
                @CameraDistance.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraDistance;
                @CameraDistance.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraDistance;
                @CameraDistance.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraDistance;
                @MousePosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @AttackHold.started += instance.OnAttackHold;
                @AttackHold.performed += instance.OnAttackHold;
                @AttackHold.canceled += instance.OnAttackHold;
                @AttackCancled.started += instance.OnAttackCancled;
                @AttackCancled.performed += instance.OnAttackCancled;
                @AttackCancled.canceled += instance.OnAttackCancled;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @Skill1.started += instance.OnSkill1;
                @Skill1.performed += instance.OnSkill1;
                @Skill1.canceled += instance.OnSkill1;
                @Skill1Hold.started += instance.OnSkill1Hold;
                @Skill1Hold.performed += instance.OnSkill1Hold;
                @Skill1Hold.canceled += instance.OnSkill1Hold;
                @Skill2.started += instance.OnSkill2;
                @Skill2.performed += instance.OnSkill2;
                @Skill2.canceled += instance.OnSkill2;
                @CameraRotate.started += instance.OnCameraRotate;
                @CameraRotate.performed += instance.OnCameraRotate;
                @CameraRotate.canceled += instance.OnCameraRotate;
                @CameraDistance.started += instance.OnCameraDistance;
                @CameraDistance.performed += instance.OnCameraDistance;
                @CameraDistance.canceled += instance.OnCameraDistance;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnAttackHold(InputAction.CallbackContext context);
        void OnAttackCancled(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnSkill1(InputAction.CallbackContext context);
        void OnSkill1Hold(InputAction.CallbackContext context);
        void OnSkill2(InputAction.CallbackContext context);
        void OnCameraRotate(InputAction.CallbackContext context);
        void OnCameraDistance(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}
