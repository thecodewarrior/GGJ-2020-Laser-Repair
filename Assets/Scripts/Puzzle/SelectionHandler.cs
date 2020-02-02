using System;
using UnityEngine;

namespace Puzzle
{
    public class SelectionHandler : MonoBehaviour
    {
        public SocketComponent Selected { get; private set; }
        public GameObject cursor;

        private void OnMouseDown()
        {
            Select(null);
        }

        private void FixedUpdate()
        {
            if (Selected)
            {
                cursor.transform.position = Selected.transform.position;
            }
            else
            {
                cursor.transform.position = transform.position;
            }

        }

        public void Select(SocketComponent component)
        {
            var oldSelected = Selected;
            Selected = component;
            if(oldSelected)
                oldSelected.UpdateSelectionState();
            if(component)
                component.UpdateSelectionState();
        }
    }
}