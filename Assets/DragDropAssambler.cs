using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropAssambler : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    // objective point game object
    public GameObject objectivePoint;

    public ItemTypeEnum.ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
        // Check if the Collider2D component is attached at the start
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError("Collider2D component is missing from this game object.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D collider2D = GetComponent<Collider2D>();

        if (Input.GetMouseButtonDown(0) && collider2D != null && collider2D.OverlapPoint(mousePosition))
        {
            // If the mouse is over the object and the left mouse button is pressed, start dragging
            isDragging = true;
            offset = transform.position - mousePosition;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            // While the left mouse button is held down and we are dragging, update the object's position
            transform.position = mousePosition + offset;

            // if the object is close to the objective point, snap it to the objective point
            // and set the flag to true depending on the object type
            if (Vector3.Distance(transform.position, objectivePoint.transform.position) < 0.5f)
            {
                transform.position = objectivePoint.transform.position;

                if (itemType.Equals(ItemTypeEnum.ItemType.IronPlate))
                    Singleton.instance.ironPlateAssemblerInPlace = true;
                else if (itemType.Equals(ItemTypeEnum.ItemType.CopperPlate))
                    Singleton.instance.copperPlateAssemblerInPlace = true;
                else if (itemType.Equals(ItemTypeEnum.ItemType.Coil))
                    Singleton.instance.coilAssemblerInPlace = true;
                else if (itemType.Equals(ItemTypeEnum.ItemType.Circuit))
                    Singleton.instance.circuitAssemblerInPlace = true;
            }
            // when it is not close to the objective point, set the flag to false
            else
            {
                if (itemType.Equals(ItemTypeEnum.ItemType.IronPlate))
                    Singleton.instance.ironPlateAssemblerInPlace = false;
                else if (itemType.Equals(ItemTypeEnum.ItemType.CopperPlate))
                    Singleton.instance.copperPlateAssemblerInPlace = false;
                else if (itemType.Equals(ItemTypeEnum.ItemType.Coil))
                    Singleton.instance.coilAssemblerInPlace = false;
                else if (itemType.Equals(ItemTypeEnum.ItemType.Circuit))
                    Singleton.instance.circuitAssemblerInPlace = false;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            // When the left mouse button is released, stop dragging
            isDragging = false;
        }

        

    }

}
