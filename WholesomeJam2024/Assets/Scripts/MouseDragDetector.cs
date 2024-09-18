using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragDetector : MonoBehaviour
{
    [SerializeField]
    private Camera m_camera;

    private GameObject m_objectToDrag;
    private Collider2D m_draggedObjectCollider;
    private Vector2 m_draggedObjectOriginalPos;
    private Coroutine m_dragCoroutine;
    private Collider2D[] m_overlapBuffer = new Collider2D[5];

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }
    }

    private void StartDragging()
    {
        if (null == m_objectToDrag && null == m_dragCoroutine)
        {
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
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

    private void StopDragging()
    {
        if (null != m_dragCoroutine)
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
                for (int i = 0; i < numOverlaps; ++i)
                {
                    if (null != m_overlapBuffer[i])
                    {
                        if (m_overlapBuffer[i].CompareTag("MotorcycleBase") || m_overlapBuffer[i].CompareTag("AttachedObject"))
                        {
                            overlapsMotorcycle = true;
                            motorCycleTransform = m_overlapBuffer[i].transform;
                            break;
                        }
                        if (m_overlapBuffer[i].CompareTag("Wheel"))
                        {
                            break;
                        }
                    }
                }

                if (overlapsMotorcycle)
                {
                    m_objectToDrag.transform.parent = motorCycleTransform;
                    m_objectToDrag.transform.tag = "AttachedObject";
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
            Vector2 mouseWorldPos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            m_objectToDrag.transform.position = mouseWorldPos;

            yield return null;
        }
    }
}