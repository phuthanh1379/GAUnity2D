using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLerp : MonoBehaviour
{
    [SerializeField] private GameObject cube;

    [SerializeField] private Transform posA;
    [SerializeField] private Transform posB;
    [SerializeField] private Transform posC;

    [SerializeField] private int quantity;
    private float zStep = 0.01f;
    private float _duration = 0.15f;
    private Vector3 basePosition;

    private Sequence _sequence; 

    private void Awake()
    {
        _sequence = DOTween.Sequence();
        _sequence.SetAutoKill(false);
        
        for (var i = 0; i < quantity; i++)
        {
            var t = (float)i / quantity;
            var x1 = Mathf.Lerp(posA.position.x, posB.position.x, t);
            var x2 = Mathf.Lerp(posB.position.x, posC.position.x, t);

            var y1 = Mathf.Lerp(posA.position.y, posB.position.y, t);
            var y2 = Mathf.Lerp(posB.position.y, posC.position.y, t);

            var x = Mathf.Lerp(x1, x2, t);
            var y = Mathf.Lerp(y1, y2, t);
            var rot = new Vector3(0f, 10f, 0f);

            if (i == 0)
            {
                basePosition = new Vector3(x, y, zStep * i);
                Instantiate(cube, basePosition, Quaternion.identity);
            }
            else
            {
                var go = Instantiate(cube, basePosition, Quaternion.identity);

                var seq = DOTween.Sequence();
                var move = go.transform.DOMove(new Vector3(x, y, zStep * i), _duration);
                var rotate = go.transform.DORotate(rot, _duration);
                seq
                    .Append(move)
                    .Join(rotate)
                    ;

                _sequence.Append(seq);
            }

        }
    }

    private void OnDrawGizmos()
    {
        if (posA != null)
            Gizmos.DrawWireSphere(posA.position, 0.1f);

        if (posB != null)
            Gizmos.DrawWireSphere(posB.position, 0.1f);

        if (posC != null)
            Gizmos.DrawWireSphere(posC.position, 0.1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _sequence.Restart();
    }
}
