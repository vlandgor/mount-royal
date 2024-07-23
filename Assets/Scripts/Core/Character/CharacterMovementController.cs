using Third_Party_Assets.Tank___Healer_Studio.Ultimate_Joystick.Scripts;
using UnityEngine;

namespace Core.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private GameObject characterModel;
        [SerializeField] private UltimateJoystick joystick;
        
        [Header("Settings")]
        [SerializeField] private float movementSpeed = 3.0f;

        private CharacterController characterController;

        private void OnValidate()
        {
            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }
        }

        void Update()
        {
            if (joystick == null) return;

            float horizontalInput = joystick.GetHorizontalAxis();
            float verticalInput = joystick.GetVerticalAxis();
            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

            characterController.Move(movement * movementSpeed * Time.deltaTime);

            if (movement != Vector3.zero)
            {
                characterModel.transform.rotation = Quaternion.LookRotation(movement);
            }
        }
    }
}