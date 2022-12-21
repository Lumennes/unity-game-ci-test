using UnityEngine;
using System.Collections;
namespace CheesyRun
{
  public class ObjectMove : MonoBehaviour
  {
    public float speed;
    public float limit;

    private GameManager gm;

    void Start()
    {
      gm = GameObject.FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
      if (gm != null && Time.timeScale > 0)
        transform.localPosition = new Vector3(transform.localPosition.x - (speed + gm.increaseSpeed), transform.localPosition.y, 0);

      if (transform.localPosition.x <= limit)
      {
        Destroy(gameObject);
      }
    }
  }
}
