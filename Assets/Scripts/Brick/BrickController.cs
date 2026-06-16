using Core;
using Managers;
using UnityEngine;

namespace Brick
{
    public class BrickController : MyBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ball")) BrickManager.Instance.ReturnBrickToPool(this);
        }
    }
}