using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseDragDetector : MonoBehaviour
{
    [SerializeField]
    private InputAction m_dragInputAction;

    private GameObject m_objectToDrag;
    private Collider2D m_draggedObjectCollider;
    private Vector2 m_draggedObjectOriginalPos;
    private Coroutine m_dragCoroutine;
    private Collider2D[] m_overlapBuffer = new Collider2D[5];

    private void Awake()
    {
        Enable();
    }

    private void Enable()
    {
        m_dragInputAction.Enable();
        m_dragInputAction.performed += OnDragInputPerformed;
        m_dragInputAction.canceled += OnDragInputCanceled;
    }

    private void OnDragInputPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (null == m_objectToDrag && null == m_dragCoroutine)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit)
                {
                    if (hit.collider.CompareTag("Draggable"))
                    {
                        m_objectToDrag = hit.collider.gameObject;
                        m_draggedObjectCollider = hit.collider;
                        m_draggedObjectCollider.enabled = false;
                        m_draggedObjectOriginalPos = m_objectToDrag.transform.position;
                        m_dragCoroutine = StartCoroutine(DragObject());
                    }
                }
            }
        }
    }

    private void OnDragInputCanceled(InputAction.CallbackContext context)
    {
        if (context.canceled && null != m_dragCoroutine)
        {
            StopCoroutine(m_dragCoroutine);

            m_dragCoroutine = null;

            ContactFilter2D test = new();
            test.NoFilter();
            m_draggedObjectCollider.enabled = true;
            int numOverlaps = Physics2D.OverlapCollider(m_draggedObjectCollider, test, m_overlapBuffer);
            if (0 < numOverlaps)
            {
                bool overlapsMotorcycle = false;
                Transform motorCycleTransform = null;
                for (int i = 0; i < m_overlapBuffer.Length; ++i)
                {
                    if (m_overlapBuffer[i].CompareTag("MotorcycleBase"))
                    {
                        overlapsMotorcycle = true;
                        motorCycleTransform = m_overlapBuffer[i].transform;
                        break;
                    }
                }

                if (overlapsMotorcycle)
                {
                    m_objectToDrag.transform.parent = motorCycleTransform;
                    m_draggedObjectCollider = null;
                    m_objectToDrag = null;
                }
                else
                {
                    ResetDraggedObject();
                }
            }
            else
            {
                ResetDraggedObject();
            }
        }
    }

    private void ResetDraggedObject()
    {
        m_objectToDrag.transform.position = m_draggedObjectOriginalPos;
        m_draggedObjectCollider = null;
        m_objectToDrag = null;
    }

    private IEnumerator DragObject()
    {
        while (null != m_objectToDrag)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_objectToDrag.transform.position = mouseWorldPos;

            yield return null; 
        }
    }
}
