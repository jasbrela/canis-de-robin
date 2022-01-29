using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput),
        typeof(Rigidbody2D), 
        typeof(Collider2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float force;
        private PlayerInput _input;
        private Rigidbody2D _rb2d;

        private void Start()
        {
            _input = GetComponent<PlayerInput>();
            _rb2d = GetComponent<Rigidbody2D>();

            SetUpControls();
        }

        private void SetUpControls()
        {
            _input.actions[InputActions.MoveLeft.ToString()].performed += _ => Move(Directions.Left);
            _input.actions[InputActions.MoveLeft.ToString()].canceled += _ => StopMovement();
            _input.actions[InputActions.MoveRight.ToString()].performed += _ => Move(Directions.Right);
            _input.actions[InputActions.MoveRight.ToString()].canceled += _ => StopMovement();
        }

        private void Move(Directions dir)
        {
            Vector2 vec = dir == Directions.Left ? Vector2.left : Vector2.right;
            _rb2d.AddRelativeForce(vec * force, ForceMode2D.Force);
        }
        
        private void StopMovement()
        {
            _rb2d.velocity = Vector2.zero;
        }
    }
}
