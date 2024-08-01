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
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float jumpHeight = 2f;

        private CharacterController characterController;
        private Vector3 velocity;

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

            // Check if the character is grounded
            bool isGrounded = characterController.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Slightly negative value to ensure character sticks to the ground
            }

            // Jumping
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            // Apply gravity
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
    }
}