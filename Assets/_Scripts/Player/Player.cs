using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float velocidadCaminando = 5f;
    public float velocidadCorriendo = 6.5f;
    public float fuerzaSalto = 5f;

    private Vector2 inputMovimiento;
    private bool estaCorriendo = false;
    private Rigidbody rb;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    void OnMove(InputValue value)
    {
        inputMovimiento = value.Get<Vector2>();
    }

    void OnSprint(InputValue value)
    {
        estaCorriendo = value.isPressed;
    }
    
    void OnRun(InputValue value) 
    {
        estaCorriendo = value.isPressed;
    }

    void OnJump(InputValue value)
    {
        if (Cursor.visible) return;

        if (value.isPressed && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {

        if (Cursor.visible)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);

            if (animator != null)
                animator.SetFloat("Speed", 0f);

            return;
        }

        float velocidadActual = estaCorriendo ? velocidadCorriendo : velocidadCaminando;
        Vector3 direccion = transform.right * inputMovimiento.x + transform.forward * inputMovimiento.y;
        
        rb.linearVelocity = new Vector3(direccion.x * velocidadActual, rb.linearVelocity.y, direccion.z * velocidadActual);

        float velocidadHorizontal = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).magnitude;

        if (animator != null)
        {
            animator.SetFloat("Speed", velocidadHorizontal);
        }
    }
}