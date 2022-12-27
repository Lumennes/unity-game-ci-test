using UnityEngine;
using System.Collections;
namespace CheesyRun
{
  public class Jump : MonoBehaviour
  {
    public Mouse mo;

    public void PressJump()
    {
      mo.MouseJump();
    }

    //void OnMouseDown()
    //{
    //  transform.localScale = new Vector3(0.9f, 0.9f, 1);

    //  mo.MouseJump();
    //}

    //void OnMouseUp()
    //{
    //  transform.localScale = new Vector3(1, 1, 1);
    //}
  }
}
