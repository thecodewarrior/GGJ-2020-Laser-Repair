using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using static GlobalUtils;

namespace Puzzle
{
    public class SocketComponent : MonoBehaviour {
         // The plane the object is currently being dragged on
         public SocketGrid grid;
         public Vector3 liftOffset;
     
         // The difference between where the mouse is on the drag plane and 
         // where the origin of the object is on the drag plane
         private Vector3 offset;
     
         private Camera mainCamera;
     
         void Start()
         {
             if (!grid)
             {
                 grid = SocketGrid.Find("Components");
             }
             mainCamera = Camera.main; // Camera.main is expensive ; cache it here
         }

         private Vector3 RaycastPlane()
         {
             Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition); 
     
             float planeDist;
             grid.GridPlane.Raycast(camRay, out planeDist);
             return camRay.GetPoint(planeDist);
         }
         
         void OnMouseDown()
         {
             if (!grid) return;
             offset = transform.position - RaycastPlane();
             UpdatePosition(true);
         }
     
         void OnMouseDrag()
         {
             if (!grid) return;
             UpdatePosition(true);
         }

         private void OnMouseUp()
         {
             if (!grid) return;
             UpdatePosition(false);
         }

         private void UpdatePosition(bool lifted)
         {
             var rayHit = RaycastPlane();
             transform.position = grid.Snap(rayHit + offset) +
                                  (lifted ? transform.TransformVector(liftOffset) : new Vector3(0, 0, 0));
         }
    }
}