// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player 1"",
            ""id"": ""7fa5d23c-fc31-4a62-be4d-5f49f883eee5"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""fd05256c-0cc4-4cf8-b791-b754ccb2c9f8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Thrust"",
                    ""type"": ""Button"",
                    ""id"": ""bb5cdfbc-8e7a-45c9-a46e-d187cea86f15"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Steer"",
                    ""type"": ""Button"",
                    ""id"": ""dcc6c9e3-f142-4489-83f1-440014810d6c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fd32285f-3885-4724-b0f3-3b8f9a2943b7"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""a94241c0-5345-42ae-a07a-7182fe3417c9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""26cfa3cb-91f7-49b0-894f-7d080d462fdd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0f8d0d69-135d-4111-bd1d-987ce1ab0dd6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""fac1216d-7f8a-42e7-84cc-85e79376ff65"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steer"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d9a31eaa-b00d-4ddf-a117-a2033bdf0c22"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""66633d5c-2ccc-4a53-a498-cad5f1121fcd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Player 2"",
            ""id"": ""2fc0f43a-9d54-40f7-94f4-b8a54c284c58"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""1eda2474-75d4-495f-84ce-cd50cf422db1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Thrust"",
                    ""type"": ""Button"",
                    ""id"": ""b0d45d2f-dd41-4ecf-958b-03f463377b91"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Steer"",
                    ""type"": ""Button"",
                    ""id"": ""d5d77f7e-4e66-4501-ba79-76c92e353399"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""31d3cf26-cecc-44b1-97c5-4bf6fcaedaf8"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""d36e708c-069b-4557-82c7-66d7931b03bc"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""fed3db5d-c45e-4879-ad70-87ae22bdaba1"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""a6931bc8-1ac8-466c-915f-48cb157dd2f5"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steer"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""18cc3343-a700-4621-8e41-3aec4129ba86"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""edf1aa34-cb94-4af8-af6e-3210b11f071b"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player 1
        m_Player1 = asset.FindActionMap("Player 1", throwIfNotFound: true);
        m_Player1_Fire = m_Player1.FindAction("Fire", throwIfNotFound: true);
        m_Player1_Thrust = m_Player1.FindAction("Thrust", throwIfNotFound: true);
        m_Player1_Steer = m_Player1.FindAction("Steer", throwIfNotFound: true);
        // Player 2
        m_Player2 = asset.FindActionMap("Player 2", throwIfNotFound: true);
        m_Player2_Fire = m_Player2.FindAction("Fire", throwIfNotFound: true);
        m_Player2_Thrust = m_Player2.FindAction("Thrust", throwIfNotFound: true);
        m_Player2_Steer = m_Player2.FindAction("Steer", throwIfNotFound: true);
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

    // Player 1
    private readonly InputActionMap m_Player1;
    private IPlayer1Actions m_Player1ActionsCallbackInterface;
    private readonly InputAction m_Player1_Fire;
    private readonly InputAction m_Player1_Thrust;
    private readonly InputAction m_Player1_Steer;
    public struct Player1Actions
    {
        private @Controls m_Wrapper;
        public Player1Actions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_Player1_Fire;
        public InputAction @Thrust => m_Wrapper.m_Player1_Thrust;
        public InputAction @Steer => m_Wrapper.m_Player1_Steer;
        public InputActionMap Get() { return m_Wrapper.m_Player1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer1Actions instance)
        {
            if (m_Wrapper.m_Player1ActionsCallbackInterface != null)
            {
                @Fire.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnFire;
                @Thrust.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnThrust;
                @Thrust.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnThrust;
                @Thrust.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnThrust;
                @Steer.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSteer;
                @Steer.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSteer;
                @Steer.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSteer;
            }
            m_Wrapper.m_Player1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Thrust.started += instance.OnThrust;
                @Thrust.performed += instance.OnThrust;
                @Thrust.canceled += instance.OnThrust;
                @Steer.started += instance.OnSteer;
                @Steer.performed += instance.OnSteer;
                @Steer.canceled += instance.OnSteer;
            }
        }
    }
    public Player1Actions @Player1 => new Player1Actions(this);

    // Player 2
    private readonly InputActionMap m_Player2;
    private IPlayer2Actions m_Player2ActionsCallbackInterface;
    private readonly InputAction m_Player2_Fire;
    private readonly InputAction m_Player2_Thrust;
    private readonly InputAction m_Player2_Steer;
    public struct Player2Actions
    {
        private @Controls m_Wrapper;
        public Player2Actions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_Player2_Fire;
        public InputAction @Thrust => m_Wrapper.m_Player2_Thrust;
        public InputAction @Steer => m_Wrapper.m_Player2_Steer;
        public InputActionMap Get() { return m_Wrapper.m_Player2; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player2Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer2Actions instance)
        {
            if (m_Wrapper.m_Player2ActionsCallbackInterface != null)
            {
                @Fire.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnFire;
                @Thrust.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnThrust;
                @Thrust.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnThrust;
                @Thrust.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnThrust;
                @Steer.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnSteer;
                @Steer.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnSteer;
                @Steer.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnSteer;
            }
            m_Wrapper.m_Player2ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Thrust.started += instance.OnThrust;
                @Thrust.performed += instance.OnThrust;
                @Thrust.canceled += instance.OnThrust;
                @Steer.started += instance.OnSteer;
                @Steer.performed += instance.OnSteer;
                @Steer.canceled += instance.OnSteer;
            }
        }
    }
    public Player2Actions @Player2 => new Player2Actions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayer1Actions
    {
        void OnFire(InputAction.CallbackContext context);
        void OnThrust(InputAction.CallbackContext context);
        void OnSteer(InputAction.CallbackContext context);
    }
    public interface IPlayer2Actions
    {
        void OnFire(InputAction.CallbackContext context);
        void OnThrust(InputAction.CallbackContext context);
        void OnSteer(InputAction.CallbackContext context);
    }
}
