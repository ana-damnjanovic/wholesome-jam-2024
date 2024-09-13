//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/InputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Build"",
            ""id"": ""b1ba4ee4-67ee-410f-8138-bf107e9685dd"",
            ""actions"": [
                {
                    ""name"": ""Drag"",
                    ""type"": ""Button"",
                    ""id"": ""a64b65f5-456c-4964-a7e7-86bde191fb5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mouse Position"",
                    ""type"": ""Value"",
                    ""id"": ""09de8e1d-b249-4f1d-8d77-c371f448b40b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a6885154-f4a7-4e35-8091-76c1b9502082"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""160e3e9b-c0ff-4cf2-bc17-f4081e3e15c2"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Build
        m_Build = asset.FindActionMap("Build", throwIfNotFound: true);
        m_Build_Drag = m_Build.FindAction("Drag", throwIfNotFound: true);
        m_Build_MousePosition = m_Build.FindAction("Mouse Position", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Build
    private readonly InputActionMap m_Build;
    private List<IBuildActions> m_BuildActionsCallbackInterfaces = new List<IBuildActions>();
    private readonly InputAction m_Build_Drag;
    private readonly InputAction m_Build_MousePosition;
    public struct BuildActions
    {
        private @InputActions m_Wrapper;
        public BuildActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Drag => m_Wrapper.m_Build_Drag;
        public InputAction @MousePosition => m_Wrapper.m_Build_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_Build; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BuildActions set) { return set.Get(); }
        public void AddCallbacks(IBuildActions instance)
        {
            if (instance == null || m_Wrapper.m_BuildActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BuildActionsCallbackInterfaces.Add(instance);
            @Drag.started += instance.OnDrag;
            @Drag.performed += instance.OnDrag;
            @Drag.canceled += instance.OnDrag;
            @MousePosition.started += instance.OnMousePosition;
            @MousePosition.performed += instance.OnMousePosition;
            @MousePosition.canceled += instance.OnMousePosition;
        }

        private void UnregisterCallbacks(IBuildActions instance)
        {
            @Drag.started -= instance.OnDrag;
            @Drag.performed -= instance.OnDrag;
            @Drag.canceled -= instance.OnDrag;
            @MousePosition.started -= instance.OnMousePosition;
            @MousePosition.performed -= instance.OnMousePosition;
            @MousePosition.canceled -= instance.OnMousePosition;
        }

        public void RemoveCallbacks(IBuildActions instance)
        {
            if (m_Wrapper.m_BuildActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBuildActions instance)
        {
            foreach (var item in m_Wrapper.m_BuildActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BuildActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BuildActions @Build => new BuildActions(this);
    public interface IBuildActions
    {
        void OnDrag(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}
