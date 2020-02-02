using TMPro;
using UnityEngine;

namespace Puzzle
{
    public class SelectionManipulator : MonoBehaviour
    {
        private SelectionHandler _selectionHandler;

        void Start()
        {
            _selectionHandler = FindObjectOfType<SelectionHandler>();
        }

        public void Delete()
        {
            var selection = _selectionHandler.Selected;
            if (selection)
            {
                Destroy(selection.gameObject);
            }
        }

        public void Rotate()
        {
            var selection = _selectionHandler.Selected;
            if (selection)
            {
                var euler = selection.transform.localRotation.eulerAngles;
                euler.z += 45;
                var rot = selection.transform.localRotation;
                rot.eulerAngles = euler;
                selection.transform.localRotation = rot;
            }
        }

        public TMP_InputField colorField;

        public void SetColor()
        {
            var selection = _selectionHandler.Selected;
            if(selection)
            {
                var colored = selection.GetComponent<ColoredObject>();
                if (colored != null)
                {
                    colored.Color = ColorRegistry.GetColor(colorField.text);
                }
            }
        }
    }
}
